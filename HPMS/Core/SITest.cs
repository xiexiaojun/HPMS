using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

using HPMS.Config;
using HPMS.Draw;
using NationalInstruments.Restricted;
using VirtualSwitch;
using VirtualVNA.NetworkAnalyzer;
using _32p_analyze;

namespace HPMS.Core
{
    public struct ConsumerParams
    {
        public Dictionary<string, object> ChartDic;
        public AChart AChart;
        public Action<string> AddStatus;
        public Action<int, bool> DisplayProgress;
        public TestConfig[] TestConfigs;
        public Dictionary<string, plotData> Spec;
     
    }

    public struct ProducerParams
    {
        public Action<int, bool> DisplayProgress;
        public Action<string> AddStatus;
        public TestConfig[] TestConfigs;
        public string TestDataPath;
     
    }
    public class SITest
    {
        private Project _project;
        private ISwitch _switch;
        private INetworkAnalyzer _analyzer;
        private string _snpFolder;
        private object myLock = new object();
        private Queue<TaskSnp> TaskQueue = new Queue<TaskSnp>();
        //private Semaphore TaskSemaphore = new Semaphore(0, 1);
        private bool stop = false;

        private Dictionary<string, List<float[]>> _testData=new Dictionary<string, List<float[]>>();
        public Dictionary<string,string>_specLine=new Dictionary<string, string>();


        public SITest(ISwitch iSwitch,INetworkAnalyzer iNetworkAnalyzer)
        {
            this._switch = iSwitch;
            this._analyzer = iNetworkAnalyzer;
        }


        private  class TaskSnp
        {
            public string SnpPath { set; get; }
            public ItemType ItemType { set; get; }
            public List<string>AnalyzeItem { set; get; }
            public string PairName { set; get; }
            public int ProgressValue { set; get; }

            
        }

        private void Analyze(TaskSnp taskSnp, ConsumerParams cpParams)
        {
       
            SNP temp = new SNP(taskSnp.SnpPath, SNPPort.X1234);
            Thread.Sleep(3000);
            double[] tempValue=new double[2000];
            for (int i = 0; i < 2000; i++)
            {
                tempValue[i] = 0;
            }
            foreach (var testItem in taskSnp.AnalyzeItem)
            {
                object tempChart = cpParams.ChartDic[testItem];
                plotData plotData = GetPlotdata(temp, testItem, taskSnp.ItemType, cpParams);
                LineType lineType = testItem.StartsWith("TDD") ? LineType.Time : LineType.Fre;
                if (_testData.ContainsKey(testItem))
                {

                    _testData[testItem].Add(plotData.xData);
                    //cpParams.addStatus(testItem + ":num:" + _testData[testItem].Count);
                }
                else
                {
                    List<float[]> temps = new List<float[]>();
                    temps.Add(plotData.xData);
                    _testData.Add(testItem, temps);
                }
                if (!_specLine.ContainsKey(testItem + "_UPPER"))
                {
                    _specLine.Add(testItem + "_UPPER", testItem + "_UPPER");
                    if (cpParams.Spec.ContainsKey(testItem + "_UPPER"))
                    {
                        cpParams.AChart.DrawLine(tempChart, cpParams.Spec[testItem + "_UPPER"], testItem + "_UPPER", lineType);
                    }
                    
                }
                if (!_specLine.ContainsKey(testItem + "_LOWER"))
                {
                    _specLine.Add(testItem + "_LOWER", testItem + "_LOWER");
                    if (cpParams.Spec.ContainsKey(testItem + "_LOWER"))
                    {
                        cpParams.AChart.DrawLine(tempChart, cpParams.Spec[testItem + "_LOWER"], testItem + "_LOWER", lineType);
                    }
                }


                

                cpParams.AChart.DrawLine(tempChart, plotData, testItem + taskSnp.PairName, lineType);

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
            AChart _aChart, Action<string> addStatus, Action<int, bool> progressDisplay,string testDataPath, Dictionary<string, plotData> spec)
        {
            System.Diagnostics.Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            progressDisplay(0, false);
            addStatus("测试开始");
            _testData.Clear();
            _specLine.Clear();
            chartDic.ForEach(t =>
            {
                addStatus("清除图形"+t.Key);
                _aChart.ChartClear(t.Value);
            });
            StartProducer(testConfigs, addStatus, progressDisplay, testDataPath);
            Thread.Sleep(2000);
            StartConsumer(testConfigs, chartDic, _aChart, addStatus, progressDisplay, spec);
          
          
            stopwatch.Stop(); //  停止监视
            TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间
            addStatus("测试用时:" + timespan.TotalMilliseconds.ToString());
           
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

            return parentFolder + "\\" + snpFolder + "\\" + pair + ".s4p";
        }

        private void Producer(object action)
        {
            ProducerParams producerParams = (ProducerParams)action;
            bool multiChannel = false;
            bool nextByTrace = false;
            foreach (TestConfig testConfig in producerParams.TestConfigs)
            {
                switch (testConfig.ItemType)
                {
                    case ItemType.Loss:
                        producerParams.AddStatus("直通测试开始");
                        multiChannel = true;
                        break;
                    case ItemType.Next:
                        producerParams.AddStatus("近串测试开始");
                        nextByTrace = true;
                        break;
                    case ItemType.Fext:
                        producerParams.AddStatus("远串测试开始");
                        nextByTrace = true;
                        break;
                }

                int pairIndex = 0;
                foreach (Pair pair in testConfig.Pairs)
                {
                    producerParams.AddStatus(pair.PairName+":start");
                    TaskSnp task=new TaskSnp();
                    task.ItemType = testConfig.ItemType;
                    task.SnpPath = GetSnpPath(producerParams.TestDataPath, pair.PairName, testConfig.ItemType);
                    task.AnalyzeItem = testConfig.AnalyzeItems;
                    task.PairName = pair.PairName;
                    task.ProgressValue = pair.ProgressValue;
                    
                    string switchMsg = "";
                    _analyzer.SaveSnp(task.SnpPath, pair.SwitchIndex,pairIndex,multiChannel,nextByTrace, ref switchMsg);
                    producerParams.AddStatus("SNP 文件生成成功 路径:" + task.SnpPath);
                    producerParams.DisplayProgress(pair.ProgressValue, true);
                    lock (myLock)
                    {
                        TaskQueue.Enqueue(task);
                    }

                    pairIndex++;
                }
            }
       
           

            lock (myLock)
            {
                TaskQueue.Enqueue(null);
            }
            //TaskSemaphore.Release(1);
        }

        private void StartProducer(TestConfig[] testConfigs,Action<string> addStatus, Action<int, bool> progressDisplay,string testDataPath)
        {
            ProducerParams producerParams=new ProducerParams();
            Thread t = new Thread(new ParameterizedThreadStart(Producer));
            producerParams.TestConfigs = testConfigs;
            producerParams.AddStatus = addStatus;
            producerParams.DisplayProgress = progressDisplay;
            producerParams.TestDataPath = testDataPath;
            t.Start(producerParams);
           
        }

        private void StartConsumer(TestConfig[] testConfigs,Dictionary<string, object> chartDic,
            AChart _aChart, Action<string> addStatus, Action<int, bool> progressDisplay, Dictionary<string, plotData> spec)
        {
            ConsumerParams cpParams=new ConsumerParams();
            cpParams.ChartDic = chartDic;
            cpParams.AChart = _aChart;
            cpParams.TestConfigs = testConfigs;
            cpParams.AddStatus = addStatus;
            cpParams.DisplayProgress = progressDisplay;
            cpParams.Spec = spec;
            Thread t = new Thread(new ParameterizedThreadStart(this.Consumer));
               t.IsBackground = true;
               t.Start(cpParams);
            t.Join();
            addStatus("测试结束");
            progressDisplay(300, false);

        }

        // 消费者  
        private void Consumer(object data)
        {
            ConsumerParams cpParams = (ConsumerParams)data;
            TaskSnp GetTask = null;
         
          
           

            while (true)
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
                        Analyze(GetTask, cpParams);
                        
                        cpParams.AddStatus("分析SNP文件:" + GetTask.SnpPath + "完毕");
                        cpParams.DisplayProgress(GetTask.ProgressValue, true);
                    }
                }
                
               
              
            }
        }

    }
}
