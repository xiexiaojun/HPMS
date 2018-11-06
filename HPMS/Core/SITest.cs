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
using _32p_analyze;

namespace HPMS.Core
{
    public struct ConsumerParams
    {
        public Dictionary<string, object> chartDic;
        public AChart _aChart;
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
                }
                else
                {
                    List<float[]>temps=new List<float[]>();
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

        public void Start(Dictionary<string, object> chartDic, AChart _aChart)
        {
            System.Diagnostics.Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start(); 
            StartProducer();
            Thread.Sleep(2000);
            StartConsumer(chartDic,_aChart);
          
            var a = _testData;
            var b = a;
            stopwatch.Stop(); //  停止监视
            TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间
            MessageBox.Show(timespan.TotalMilliseconds.ToString());
        }

        private void Producer()
        {
       
            for (int j = 0; j < 8;j++)
            {
                TaskSnp task=new TaskSnp();
                task.SnpPath = "B:\\THRU\\"+(j+1)+".s4p";
                string switchMsg = "";
                _analyzer.SaveSnp(task.SnpPath, j, ref switchMsg);
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

        private void StartProducer()
        {
             Thread t = new Thread(new ThreadStart(Producer));
                t.Start();
           
        }

        private void StartConsumer(Dictionary<string, object> chartDic, AChart _aChart)
        {
            ConsumerParams cpParams=new ConsumerParams();
            cpParams.chartDic = chartDic;
            cpParams._aChart = _aChart;
            Thread t = new Thread(new ParameterizedThreadStart(this.Consumer));
               t.IsBackground = true;
               t.Start(cpParams);
            t.Join();

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
                    }
                }
                
               
              
            }
        }

    }
}
