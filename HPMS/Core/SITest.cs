using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

using HPMS.Config;
using HPMS.Draw;
using HPMS.Util;
using NationalInstruments.Restricted;
using Tool;
using VirtualSwitch;
using VirtualVNA.NetworkAnalyzer;
using _32p_analyze;
using Convert = System.Convert;

namespace HPMS.Core
{
    public struct ConsumerParams
    {
        public Dictionary<string, object> ChartDic;
        public AChart AChart;
        public FormUi FormUi;
        public TestConfig[] TestConfigs;
        public Dictionary<string, plotData> Spec;
        public string SaveTxtPath;

    }

    public struct ProducerParams
    {
        public FormUi FormUi;
        public TestConfig[] TestConfigs;
        public string TestDataPath;
     
    }

    public struct PairData
    {
        public string PairName;
        public float[] YData;
        public float[] XData;
    }
    public class SITest
    {
        private Project _project;
        private ISwitch _switch;
        private NetworkAnalyzer _analyzer;
        private string _snpFolder;
        private object myLock = new object();
        private Queue<TaskSnp> TaskQueue = new Queue<TaskSnp>();
        //private Semaphore TaskSemaphore = new Semaphore(0, 1);
        private bool stop = false;
        bool _totalResult = true;
        private Dictionary<string,string>_information=new Dictionary<string, string>();

        private Dictionary<string, List<PairData>> _testData = new Dictionary<string, List<PairData>>();
        private Dictionary<string,Dictionary<string,bool>>_testResult=new Dictionary<string, Dictionary<string, bool>>();
        private Dictionary<string, string> _itemTestResult = new Dictionary<string, string>();
        public Dictionary<string,string>_specLine=new Dictionary<string, string>();


        public SITest(ISwitch iSwitch,NetworkAnalyzer iNetworkAnalyzer,Dictionary<string,string>information)
        {
            this._switch = iSwitch;
            this._analyzer = iNetworkAnalyzer;
            this._information = information;
        }


        private  class TaskSnp
        {
            public string SnpPath { set; get; }
            public ItemType ItemType { set; get; }
            public List<string>AnalyzeItem { set; get; }
            public string PairName { set; get; }
            public int ProgressValue { set; get; }

            
        }

        private void InsertTestData(plotData plotData,string testItem,string pairName)
        {
            if (_testData.ContainsKey(testItem))
            {
                PairData pairData=new PairData();
                pairData.YData = plotData.yData;
                pairData.XData = plotData.xData;
                pairData.PairName = pairName;
                _testData[testItem].Add(pairData);
                //cpParams.addStatus(testItem + ":num:" + _testData[testItem].Count);
            }
            else
            {
                List<PairData> temps = new List<PairData>();
                PairData pairData = new PairData();
                pairData.YData = plotData.yData;
                pairData.XData = plotData.xData;
                pairData.PairName = pairName;
                temps.Add(pairData);
                _testData.Add(testItem, temps);
            }
        }

        private void DrawSpec(string testItem, ConsumerParams cpParams, object tempChart)
        {
            if (!_specLine.ContainsKey(testItem + "_UPPER"))
            {
                _specLine.Add(testItem + "_UPPER", testItem + "_UPPER");
                if (cpParams.Spec.ContainsKey(testItem + "_UPPER"))
                {
                    cpParams.AChart.DrawLine(tempChart, cpParams.Spec[testItem + "_UPPER"], "UPPER", LineType.Spec);
                }

            }
            if (!_specLine.ContainsKey(testItem + "_LOWER"))
            {
                _specLine.Add(testItem + "_LOWER", testItem + "_LOWER");
                if (cpParams.Spec.ContainsKey(testItem + "_LOWER"))
                {
                    cpParams.AChart.DrawLine(tempChart, cpParams.Spec[testItem + "_LOWER"], "LOWER", LineType.Spec);
                }
            }
        }

        private void InsertTestJudge(string testItem,bool result,string pairName)
        {
            if (_testResult.ContainsKey(testItem))
            {
                _testResult[testItem].Add(pairName, result);
            }
            else
            {
                Dictionary<string, bool> tResult = new Dictionary<string, bool>();
                tResult.Add(pairName, result);
                _testResult.Add(testItem, tResult);
            }
        }

        private void Analyze(TaskSnp taskSnp, ConsumerParams cpParams)
        {
       
            SNP temp = new SNP(taskSnp.SnpPath, SNPPort.X1324);
           // Thread.Sleep(3000);
           
            foreach (var testItem in taskSnp.AnalyzeItem)
            {
                object tempChart = cpParams.ChartDic[testItem];
                plotData plotData = GetPlotdata(temp, testItem, taskSnp.ItemType, cpParams);
                string savePath = cpParams.SaveTxtPath + "\\" + testItem + ".txt";
                SaveTxt(plotData,savePath);
                cpParams.FormUi.AddStatus(savePath + "写入成功");

                InsertTestData(plotData, testItem, taskSnp.PairName);

                DrawSpec(testItem, cpParams, tempChart);
                bool result = HPMS.Util.Convert.Judge(cpParams.Spec, plotData, testItem);
                cpParams.FormUi.AddStatus(testItem + " " + taskSnp.PairName + ":" + (result ? "PASS" : "Fail"));
                InsertTestJudge(testItem, result, taskSnp.PairName);

                cpParams.FormUi.SetCheckItem(testItem, ClbType.TestItem);
                cpParams.AChart.DrawLine(tempChart, plotData, taskSnp.PairName, testItem.StartsWith("TDD") ? LineType.Time : LineType.Fre);

            }
        }

        private void SaveTxt(plotData plotData,string saveFilePath)
        {
            if (File.Exists(saveFilePath))
            {
                var allLines = File.ReadAllLines(saveFilePath);
                int length = allLines.Length;
                string[]addedLines=new string[length];
                for (int i = 0; i < length; i++)
                {
                    addedLines[i] = allLines[i] + "\t" + plotData.yData[i];
                }
                File.WriteAllLines(saveFilePath,addedLines);
            }
            else
            {
                int length = plotData.xData.Length;
                string[] addedLines = new string[length];
                for (int i = 0; i < length; i++)
                {
                    addedLines[i] = plotData.xData[i] + "\t" + plotData.yData[i];
                }
                File.WriteAllLines(saveFilePath, addedLines);

            }
        }


        private plotData GetPlotdata(SNP snp,string itemName,ItemType itemType,ConsumerParams cpParams)
        {
            plotData dataSeries = new plotData();
            List<TdrParam> tdrParams = cpParams.TestConfigs[0].TdrParams;
            switch (itemType)
            {
                case   ItemType.Loss:
                    if (itemName.StartsWith("S"))
                    {
                        string[] outPairs = new string[1];
                        var data = snp.EasyGetfreData(itemName, out outPairs);
                        float[] x = SConvert.indexArray(data.dB, 0, false);
                        dataSeries.xData = data.fre;
                        dataSeries.yData = x;
                    }
                    else if(itemName.StartsWith("TDD"))
                    {
                        TdrParam tdrParam = tdrParams[int.Parse(itemName.Substring(3, 1))-1];
                        string[] outPairs = new string[1];
                        double timeStep = (tdrParam.EndTime - tdrParam.StartTime) / (tdrParam.Points - 1);
                        var data = snp.EasyGetTimeData(itemName, out outPairs, tdrParam.RiseTime, timeStep, tdrParam.Points, tdrParam.Offset);
                        float[] x = SConvert.indexArray(data.resistance, 0, false);
                        dataSeries.xData = data.time;
                        dataSeries.yData = x;
                    }
                   break;
                case ItemType.Next:
                    if (itemName.StartsWith("NEXT"))
                    {
                        string[] outPairs = new string[1];
                        var data = snp.EasyGetfreData("SDD21", out outPairs);
                        float[] x = SConvert.indexArray(data.dB, 0, false);
                        dataSeries.xData = data.fre;
                        dataSeries.yData = x;
                    }
                   break;
                case ItemType.Fext:
                    if (itemName.StartsWith("FEXT"))
                    {
                        string[] outPairs = new string[1];
                        var data = snp.EasyGetfreData("SDD21", out outPairs);
                        float[] x = SConvert.indexArray(data.dB, 0, false);
                        dataSeries.xData = data.fre;
                        dataSeries.yData = x;
                    }
                    break;
               
            }

            return dataSeries;
        }

        public void DoTest(TestConfig[] testConfigs,Dictionary<string, object> chartDic, 
            AChart _aChart, FormUi formUi,Dictionary<string, plotData> spec, Dictionary<string,float[]>keyPoint,Savepath savepath)
        {
            System.Diagnostics.Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            formUi.ProgressDisplay(0, false);
            formUi.AddStatus("测试开始");
            formUi.SetResult("TEST");
            _testData.Clear();
            _testResult.Clear();
            _specLine.Clear();
            chartDic.ForEach(t =>
            {
                formUi.AddStatus("清除图形" + t.Key);
                _aChart.ChartClear(t.Value);
            });
            StartProducer(testConfigs, formUi, savepath.SnpFilePath);
            Thread.Sleep(2000);
            StartConsumer(testConfigs, chartDic, _aChart, formUi, spec, savepath.TxtFilePath);

            SetTestResult(formUi.AddStatus,formUi.SetResult);
            var aa = GetKeyValue(keyPoint);
            formUi.SetKeyPointList(aa);
            SetInformation(savepath.Sn);
            SetResult();
            TestUtil.SaveResult_Sample(savepath.XmlPath, _itemTestResult, _information);
            stopwatch.Stop(); //  停止监视
            TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间
            formUi.AddStatus("测试结束");
            formUi.AddStatus("测试用时:" + timespan.TotalSeconds);
           
        }

        private void SetInformation(string sn)
        {
            _information.Addsafe("reportNo","HF-" + sn);
            _information.Addsafe("sampleNo", sn);
            _information.Addsafe("date", DateTime.Now.ToString("yyyy-MM-dd"));
         }

        private void SetResult()
        {
            _itemTestResult.Clear();
            foreach (var variable in _testResult)
            {
                bool itemResult = true;
                foreach (var pair in variable.Value)
                {
                    itemResult = itemResult && pair.Value;
                }
                _itemTestResult.Add(variable.Key,itemResult?"OK":"NG");
            }
        }

        #region 获取关键频点数据

       
        private Dictionary<string, List<PairData>>  GetKeyValue(Dictionary<string, float[]> keyPoint)
        {
            Dictionary<string, List<PairData>> testItemData = new Dictionary<string, List<PairData>>();
            foreach (var testItem in keyPoint)
            {
                if (_testData.ContainsKey(testItem.Key))
                {
                    List<PairData> temp = _testData[testItem.Key];
                    List<PairData>testItemPairData=new List<PairData>();
                    foreach (var pair in temp)
                    {
                        PairData keyPairData = GetPair(pair, testItem.Value);
                        testItemPairData.Add(keyPairData);
                    }
                    testItemData.Add(testItem.Key, testItemPairData);
                }
            }

            return testItemData;
        }

        private PairData GetPair(PairData source,float[]keyPoint)
        {
            PairData ret=new PairData();
            int length = keyPoint.Length;
            float[]valueY=new float[length];
            for (int i = 0; i < length; i++)
            {
                int findedIndex = FindIndex(source.XData, keyPoint[i]);
                valueY[i] = source.YData[findedIndex];
            }

            ret.PairName = source.PairName;
            ret.XData = keyPoint;
            ret.YData = valueY;
            return ret;
        }

        private int FindIndex(float[] data, float value)
        {
            return Convert.ToInt32((value - data[0]) / (data[1] - data[0]));
        }


        #endregion
        private void SetTestResult(Action<string> addStatus,Action<string>setResult)
        {
            bool TotalResult = true;
            foreach (var variable in _testResult)
            {
               bool result= variable.Value.Select(t => t.Value).Aggregate((a, b) => a && b);
               addStatus(variable.Key + ":" + (result ? "PASS" : "FAIL"));
                TotalResult = TotalResult && result;
            }

            addStatus("Test result:" + (TotalResult && _totalResult ? "PASS" : "FAIL"));
            setResult(TotalResult && _totalResult ? "PASS" : "FAIL");
        }

        private string GetSnpPath(string parentFolder,string pair,ItemType itemType)
        {
            string snpFolder = "";
            if (itemType == ItemType.Loss)
            {
                snpFolder = "THRU";
            }
            else if (itemType == ItemType.Next)
            {
                snpFolder = "NEXT";
            }
            else
            {
                snpFolder = "FEXT";
            }

            if (!Directory.Exists(parentFolder + "\\" + snpFolder))
            {
                Directory.CreateDirectory(parentFolder + "\\" + snpFolder);
            }
            else if (File.Exists(parentFolder + "\\" + snpFolder + "\\" + pair + ".s4p"))
            {
                if (_analyzer is DemoAnalyzer)
                {
                    //demo模式时不删除S4P文件
                }
                else
                {
                    File.Delete(parentFolder + "\\" + snpFolder + "\\" + pair + ".s4p");
                }
               
            }

            
            return parentFolder + "\\" + snpFolder + "\\" + pair + ".s4p";
        }

        private void SnpProducer(object action)
        {
            ProducerParams producerParams = (ProducerParams)action;
            bool multiChannel = false;
            bool nextByTrace = false;
            ClbType clbType = ClbType.TestItem;
            foreach (TestConfig testConfig in producerParams.TestConfigs)
            {
                if (stop)
                {
                   break;
                }
                switch (testConfig.ItemType)
                {
                    case ItemType.Loss:
                        producerParams.FormUi.AddStatus("直通测试开始");
                        multiChannel = true;
                        clbType = ClbType.DiffPair;
                        break;
                    case ItemType.Next:
                        producerParams.FormUi.AddStatus("近串测试开始");
                        multiChannel = false;
                        nextByTrace = true;
                        clbType = ClbType.NextPair;
                        break;
                    case ItemType.Fext:
                        producerParams.FormUi.AddStatus("远串测试开始");
                        multiChannel = false;
                        nextByTrace = true;
                        clbType = ClbType.FextPair;
                        break;
                    case ItemType.Last:
                        break;
                }

                int pairIndex = 0;
                if (testConfig.Pairs != null)
                {
                    foreach (Pair pair in testConfig.Pairs)
                    {
                        if (stop || producerParams.FormUi.StopEnabbled())
                        {
                            stop = true;
                            _totalResult = false;
                            return;
                        }
                        producerParams.FormUi.AddStatus(pair.PairName + ":start");
                        TaskSnp task = new TaskSnp();
                        task.ItemType = testConfig.ItemType;
                        task.SnpPath = GetSnpPath(producerParams.TestDataPath, pair.PairName, testConfig.ItemType);
                        task.AnalyzeItem = testConfig.AnalyzeItems;
                        task.PairName = pair.PairName;
                        task.ProgressValue = pair.ProgressValue;

                        string switchMsg = "";
                        try
                        {
                            _analyzer.SaveSnp(task.SnpPath, pair.SwitchIndex, pairIndex, multiChannel, nextByTrace, ref switchMsg);
                            producerParams.FormUi.AddStatus(switchMsg);
                        }
                        catch (Exception e)
                        {
                            stop = true;
                            _totalResult = false;
                            //Console.WriteLine(e);
                            producerParams.FormUi.AddStatus(e.Message);
                            return;
                        }

                        if (!stop)
                        {
                            producerParams.FormUi.SetCheckItem(pair.PairName, clbType);
                            producerParams.FormUi.AddStatus("SNP 文件生成成功 路径:" + task.SnpPath);
                            producerParams.FormUi.ProgressDisplay(pair.ProgressValue, true);
                        }



                        lock (myLock)
                        {
                            TaskQueue.Enqueue(task);
                        }

                        pairIndex++;
                    }
                }
                }
               
       
           

            lock (myLock)
            {
                TaskQueue.Enqueue(null);
            }
            //TaskSemaphore.Release(1);
        }

        private void StartProducer(TestConfig[] testConfigs, FormUi formUi, string testDataPath)
        {
            ProducerParams producerParams=new ProducerParams();
            Thread t = new Thread(new ParameterizedThreadStart(SnpProducer));
            producerParams.TestConfigs = testConfigs;
            producerParams.FormUi = formUi;
            producerParams.TestDataPath = testDataPath;
            t.Start(producerParams);
           
        }

        private void StartConsumer(TestConfig[] testConfigs,Dictionary<string, object> chartDic,
            AChart _aChart, FormUi formUi, Dictionary<string, plotData> spec, string saveTxtPath)
        {
            ConsumerParams cpParams=new ConsumerParams();
            cpParams.ChartDic = chartDic;
            cpParams.AChart = _aChart;
            cpParams.TestConfigs = testConfigs;
            cpParams.FormUi = formUi;
            cpParams.Spec = spec;
            cpParams.SaveTxtPath = saveTxtPath+"\\"+"TXT";
            if (Directory.Exists(cpParams.SaveTxtPath))
            {
                Directory.Delete(cpParams.SaveTxtPath,true);
            }
            Directory.CreateDirectory(cpParams.SaveTxtPath);
            Thread t = new Thread(new ParameterizedThreadStart(this.SnpConsumer));
               t.IsBackground = true;
               t.Start(cpParams);
            t.Join();

            Thread t1 = new Thread(new ParameterizedThreadStart(this.SnpConsumerLast));
            t1.IsBackground = true;
            t1.Start(cpParams);
            t1.Join();
            formUi.ProgressDisplay(300, false);

        }

        // 消费者  
        private void SnpConsumer(object data)
        {
            ConsumerParams cpParams = (ConsumerParams)data;
            TaskSnp GetTask = null;
         
            while (true&&!stop)
            {
                //TaskSemaphore.WaitOne();
                lock (myLock)
                {
                    if (TaskQueue.Count > 0)
                    {
                        GetTask = TaskQueue.Dequeue();
                        if (GetTask == null)
                        {
                            stop = true;
                            return;
                        }
                        string msg = "";
                        int iChecktime = 0;
                        bool bFileExist=Tool.FileCheck.FileIsUsing(GetTask.SnpPath, 10000, 10000, ref msg, ref iChecktime);
                        if (!bFileExist)
                        {
                            stop = true;
                            _totalResult = false;
                            cpParams.FormUi.AddStatus("SNP文件:" + GetTask.SnpPath + "不存在:"+msg);
                            return;
                        }
                        Analyze(GetTask, cpParams);
                        cpParams.FormUi.AddStatus("分析SNP文件:" + GetTask.SnpPath + "完毕");
                        cpParams.FormUi.ProgressDisplay(GetTask.ProgressValue, true);
                    }
                }
               
            }
        }


        private void SnpConsumerLast(object data)
        {
            try
            {
                ConsumerParams cpParams = (ConsumerParams)data;
                foreach (var variable in cpParams.TestConfigs)
                {
                    if (variable.ItemType == ItemType.Last)
                    {
                        foreach (var testItem in variable.AnalyzeItems)
                        {
                            object tempChart = cpParams.ChartDic[testItem];
                            List<PairData> itemPairDatas = new List<PairData>();
                            if (testItem == "MDNEXT")
                            {
                                itemPairDatas = GetMdNext();
                                _testData.Add("MDNEXT", itemPairDatas);

                            }
                            if (testItem == "MDFEXT")
                            {
                                itemPairDatas = GetMdFext();
                                _testData.Add("MDFEXT", itemPairDatas);

                            }
                            if (testItem == "ICR")
                            {
                                itemPairDatas = GetIcr();
                                _testData.Add("ICR", itemPairDatas);

                            }
                            DrawSpec(testItem, cpParams, tempChart);
                            foreach (var pairData in itemPairDatas)
                            {
                                plotData plotData = new plotData();
                                plotData.xData = pairData.XData;
                                plotData.yData = pairData.YData;
                                bool result = HPMS.Util.Convert.Judge(cpParams.Spec, plotData, testItem);
                                cpParams.FormUi.AddStatus(testItem + " " + pairData.PairName + ":" + (result ? "PASS" : "Fail"));
                                InsertTestJudge(testItem, result, pairData.PairName);
                                cpParams.FormUi.SetCheckItem(testItem, ClbType.TestItem);
                                cpParams.AChart.DrawLine(tempChart, plotData, pairData.PairName, testItem.StartsWith("TDD") ? LineType.Time : LineType.Fre);
                                string savePath = cpParams.SaveTxtPath + "\\" + testItem + ".txt";
                                SaveTxt(plotData, savePath);
                                cpParams.FormUi.AddStatus(savePath + "写入成功");
                            }


                        }
                    }
                }
            }
            catch (Exception e)
            {
              Ui.MessageBoxMuti(e.Message);
            }
         
        }

        private List<PairData> GetMdNext()
        {
            if (!_testData.ContainsKey("NEXT"))
            {
                throw new Exception("NEXT should be test first before MDNEXT");
            }
            List<PairData> next = _testData["NEXT"];
            int length = next.Count;
            if (length % 4 != 0)
            {
                throw new Exception("Next data colomn is："+length+" error，should be 16 or its times");
            }
            List<PairData>ret=new List<PairData>();
            for (int i = 0; i < length/4; i++)
            {
                PairData temp=new PairData();
                temp.XData = next[i * 4].XData;
                int points = temp.XData.Length;
                float[]tempYdata=new float[points];
                for (int j = 0; j < points; j++)
                {
                    double tempMd = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        tempMd = tempMd + Math.Pow(10, next[i*4+k].YData[j] / 10);
                    }

                    tempYdata[j] = (float)(10*Math.Log10(tempMd));
                }

                temp.YData = tempYdata;
                temp.PairName = next[i * 4].PairName.Remove(2, 1);
                ret.Add(temp);
            }
            return ret;
        }

        private List<PairData> GetMdFext()
        {
            if (!_testData.ContainsKey("FEXT"))
            {
                throw new Exception("FEXT should be test first before MDFEXT");
            }
            List<PairData> next = _testData["FEXT"];
            int length = next.Count;
            if (length % 3 != 0)
            {
                throw new Exception("Next data colomn is：" + length + " error，should be 12 or its times");
            }
            List<PairData> ret = new List<PairData>();
            for (int i = 0; i < length / 3; i++)
            {
                PairData temp = new PairData();
                temp.XData = next[i * 3].XData;
                int points = temp.XData.Length;
                float[] tempYdata = new float[points];
                for (int j = 0; j < points; j++)
                {
                    float tempMd = 0f;
                    for (int k = 0; k < 3; k++)
                    {
                        tempMd = tempMd + (float)Math.Pow(10, next[i * 3 + k].YData[j] / 10);
                    }

                    tempYdata[j] = (float)(10 * Math.Log10(tempMd));
                }

                temp.YData = tempYdata;
                temp.PairName = next[i * 3].PairName.Remove(2, 1);
                ret.Add(temp);
            }
            return ret;
        }

        private List<PairData> GetIcr()
        {
            if (!_testData.ContainsKey("NEXT")&&!_testData.ContainsKey("FEXT")&&!_testData.ContainsKey("SDD21"))
            {
                throw new Exception("SDD21,NEXT and FEXT should be test first than ICR");
            }
            else
            {
                List<PairData> mdnext = _testData.ContainsKey("MDNEXT") ? _testData["MDNEXT"] : GetMdNext();
                List<PairData> mdfext = _testData.ContainsKey("MDFEXT") ? _testData["MDFEXT"] : GetMdFext();
                List<PairData> sdd21 = _testData["SDD21"];
                int length21 = sdd21.Count;
                int lengthMdnext = mdnext.Count;
                int lengthMdfext = mdfext.Count;
                if (lengthMdfext != lengthMdnext)
                {
                    throw new Exception("mdnext data colomn is：" + lengthMdnext +"mdfext data colomn is：" + lengthMdfext + " error，should be equal");
                }
                if (lengthMdnext * 2 != length21)
                {
                    throw new Exception("sdd21 data colomn is：" + length21 + " error，should be 2 times of mdnext data column");
                }
                List<PairData>ret=new List<PairData>();
                for (int i = 0; i < lengthMdnext; i++)
                {
                    PairData temp=new PairData();
                    temp.XData = mdfext[i].XData;
                    int points = temp.XData.Length;
                    float[]tempYdata=new float[points];
                    for (int j = 0; j < points; j++)
                    {
                        tempYdata[j] =
                            (float)(10 * Math.Log10(Math.Pow(10, mdnext[i].YData[j] / 10) +
                                            Math.Pow(10, mdfext[i].YData[j] / 10)) -
                            20 * Math.Log10(Math.Abs(sdd21[i + length21 / 2].YData[j])));
                    }

                    temp.PairName = mdnext[i].PairName;
                    temp.YData = tempYdata;
                    ret.Add(temp);
                }

                return ret;
            }
        }

    }
}
