using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HPMS.Config;
using HPMS.Draw;
using HPMS.Equipment.Enum;
using HPMS.Equipment.NetworkAnalyzer;
using HPMS.Equipment.Switch;
using NationalInstruments.Restricted;
using _32p_analyze;

namespace HPMS.Core
{
    public struct ConsumerParams
    {
        public Dictionary<string, object> chartDic;
        public AChart _aChart;
        public Action<string> addStatus;
        public Action<int, bool> displayProgress;
    }

    public struct ProducerParams
    {
        public Action<int, bool> displayProgress;
        public Action<string> addStatus;
    }
    public class SITest
    {
        private Project _project;
        private ISwitch _switch;
        private INetworkAnalyzer _analyzer=new DemoAnalyzer();
        private string _snpFolder;
        private object myLock = new object();
        private Queue<TaskSnp> TaskQueue = new Queue<TaskSnp>();
        //private Semaphore TaskSemaphore = new Semaphore(0, 1);
        private bool stop = false;

        private Dictionary<string, List<float[]>> _testData=new Dictionary<string, List<float[]>>();

        private  class TaskSnp
        {
            public string SnpPath { set; get; }
            
        }

        private void Analyze(string snpFilePath, ConsumerParams cpParams)
        {
           // Thread.Sleep(1000);
            string[] testItems = { "SDD21","SDD11","SCD11"};
            string[]outPairs=new string[1];
            SNP temp=new SNP(snpFilePath,SNPPort.X1234);
            Thread.Sleep(3000);
            double[] tempValue=new double[2000];
            for (int i = 0; i < 2000; i++)
            {
                tempValue[i] = 0;
            }
            foreach (var testItem in testItems)
            {
                object tempChart = cpParams.chartDic[testItem];
               var data= temp.EasyGetfreData(testItem, out outPairs);
                float[] x = SConvert.indexArray(data.dB, 0, false);
                if (_testData.ContainsKey(testItem))
                {

                    _testData[testItem].Add(x);
                    //cpParams.addStatus(testItem + ":num:" + _testData[testItem].Count);
                }
                else
                {
                    List<float[]> temps = new List<float[]>();
                    temps.Add(x);
                    _testData.Add(testItem, temps);
                }
                plotData dataSeries=new plotData();
                dataSeries.xData = data.fre;
                    dataSeries.yData = x;
                Random rd = new Random();
                cpParams._aChart.DrawLine(tempChart, dataSeries, testItem + rd.Next(1,10000), LineType.Fre);

            }
        }

        public void Start(Dictionary<string, object> chartDic, AChart _aChart, Action<string> addStatus, Action<int, bool> progressDisplay)
        {
            System.Diagnostics.Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            progressDisplay(0, false);
            addStatus("测试开始");
            _testData.Clear();
            chartDic.ForEach(t =>
            {
                addStatus("清除图形"+t.Key);
                _aChart.ChartClear(t.Value);
            });
            StartProducer(addStatus, progressDisplay);
            Thread.Sleep(2000);
            StartConsumer(chartDic, _aChart, addStatus, progressDisplay);
          
          
            stopwatch.Stop(); //  停止监视
            TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间
            addStatus("测试用时:" + timespan.TotalMilliseconds.ToString());
           
        }

        private void Producer(object action)
        {
            ProducerParams uiAction = (ProducerParams)action;
       
            for (int j = 0; j < 8;j++)
            {
                TaskSnp task=new TaskSnp();
                task.SnpPath = "B:\\THRU\\"+(j+1)+".s4p";
                string switchMsg = "";
                _analyzer.SaveSnp(task.SnpPath, j, ref switchMsg);
                uiAction.addStatus("SNP 文件生成成功 路径:" + task.SnpPath);
                uiAction.displayProgress(5, true);
                lock (myLock)
                {
                     TaskQueue.Enqueue(task);
                }
                //TaskSemaphore.Release(1);


            }

            lock (myLock)
            {
                TaskQueue.Enqueue(null);
            }
            //TaskSemaphore.Release(1);
        }

        private void StartProducer(Action<string> addStatus, Action<int, bool> progressDisplay)
        {
            ProducerParams producerParams=new ProducerParams();
            Thread t = new Thread(new ParameterizedThreadStart(Producer));
            producerParams.addStatus = addStatus;
            producerParams.displayProgress = progressDisplay;
            t.Start(producerParams);
           
        }

        private void StartConsumer(Dictionary<string, object> chartDic, AChart _aChart, Action<string> addStatus, Action<int, bool> progressDisplay)
        {
            ConsumerParams cpParams=new ConsumerParams();
            cpParams.chartDic = chartDic;
            cpParams._aChart = _aChart;
            cpParams.addStatus = addStatus;
            cpParams.displayProgress = progressDisplay;
            Thread t = new Thread(new ParameterizedThreadStart(this.Consumer));
               t.IsBackground = true;
               t.Start(cpParams);
            t.Join();
            addStatus("测试结束");
            progressDisplay(100, false);

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
                        Analyze(GetTask.SnpPath, cpParams);
                        cpParams.addStatus("分析SNP文件:" + GetTask.SnpPath + "完毕");
                        cpParams.displayProgress(5, true);
                    }
                }
                
               
              
            }
        }

    }
}
