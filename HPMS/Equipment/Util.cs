using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
using HPMS.Config;
using HPMS.Core;
using VirtualSwitch;
using VirtualVNA.Enum;
using VirtualVNA.NetworkAnalyzer;
using NetworkAnalyzer = VirtualVNA.NetworkAnalyzer.NetworkAnalyzer;

namespace HPMS.Equipment
{
    public class Util
    {
        /// <summary>
        /// 返回visa资源列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetVisaList()
        {
            Ivi.Visa.Interop.ResourceManager resourceManagerIv = new Ivi.Visa.Interop.ResourceManager();
            List<string>ret=new List<string>();
            //string[] resources = ResourceManager.GetLocalManager().FindResources("?*");
            string[] resources = resourceManagerIv.FindRsrc("?*");
            foreach (string s in resources)
            {
                ret.Add(s);
            }
            if (ret.Count == 0)
            {
                ret.Add("无Visa设备");
            }
            return ret;
        }

  

        /// <summary>
        /// 返回串口列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetSerialPortsList()
        {
            List<string>ret=new List<string>();
            
            foreach (string vPortName in SerialPort.GetPortNames())
            {
                ret.Add(vPortName);
            }

            if (ret.Count == 0)
            {
                ret.Add("无串口");
            }

            return ret;
        }

        /// <summary>
        /// 返回开关盒子对象
        /// </summary>
        /// <param name="hardware"></param>
        /// <returns></returns>
        public static ISwitch GetSwitch(Hardware hardware)
        {
            ISwitch retSwitch = null;
            switch (hardware.SwitchBox)
            {
                case SwitchBox.MCU:
                    retSwitch=new SwitchMcu(hardware.VisaSwitchBox);
                    break;
            }

            return retSwitch;
        }

        public static List<byte[]> GetSwitchMap(string strSwitchFilePath)
        {
            try
            {
                if (!File.Exists(strSwitchFilePath))
                {
                    return null;
                }

                return File.ReadAllLines(strSwitchFilePath).Where(s => !(s.StartsWith("T") || s.StartsWith("R") || s.Trim().Length == 0)).Select(ConvertSwitchLabelToIndex).ToList();

            }
            catch (Exception e)
            {
                Log.LogHelper.WriteLog("解析开关文件错误", e);
                return null;
            }
          

        }

        /// <summary>
        /// 转换A13	A33	A57	A77	等开关编号为对应的索引编号
        /// </summary>
        /// <param name="switchLabel"></param>
        /// <returns></returns>
        private static byte[] ConvertSwitchLabelToIndex(string switchLabel)
        {
            List<byte> ret = new List<byte>();
            try
            {
                
                string[] split = { "\t" };
                string[] labels = switchLabel.Split(split, StringSplitOptions.RemoveEmptyEntries).ToArray();
                foreach (string s in labels)
                {
                    int index = int.Parse(s.Substring(1, 2)) - 9;
                    int center = 80 + int.Parse(s.Substring(1, 1));
                    ret.Add((byte)index);
                    ret.Add((byte)center);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
             
                return ret.ToArray();
          
           
          
        }

        public static bool SetTestParams(Project pnProject, ref TestConfig[] testConfigs)
        {
            bool ret = false;
            Dictionary<string, string> testItemGroup = TestUtil.GetTestItem();

            List<byte[]> switchIndex = GetSwitchMap(pnProject.SwitchFilePath);
            if (switchIndex == null)
            {
                return false;
            }

            TestConfig testConfigLoss = new TestConfig();
            TestConfig testConfigNext = new TestConfig();
            TestConfig testConfigFext = new TestConfig();
            TestConfig testConfigLast = new TestConfig();
            List<string> loss = new List<string>();
            List<string> next = new List<string>();
            List<string> fext = new List<string>();
            List<string> last = new List<string>();


            int diffPairNum = pnProject.DiffPair.Count;
            int nextPairNum = pnProject.NextPair.Count;
            int fextPairNum = pnProject.FextPair.Count;
            int pairSum = diffPairNum + nextPairNum + fextPairNum;
            if (switchIndex.Count <pairSum )
            {
                return false;
            }

            List<Pair>lossPairs=new List<Pair>();
            List<Pair> nextPairs = new List<Pair>();
            List<Pair> fextPairs = new List<Pair>();

            List<byte[]> lossSwitch = switchIndex.Take(diffPairNum).ToList();
            List<byte[]> nextSwitch = switchIndex.Skip(diffPairNum).Take(nextPairNum).ToList();
            List<byte[]> fextSwitch = switchIndex.Skip(diffPairNum + nextPairNum).Take(fextPairNum).ToList();


            List<string> lossAll = pnProject.Diff.Concat(pnProject.Tdr).ToList();

            foreach (string s in lossAll)
            {
                if (testItemGroup.ContainsKey(s.ToUpper()))
                {
                    switch (testItemGroup[s.ToUpper()])
                    {
                        case "loss":
                            loss.Add(s);
                            break;
                        case "next":
                            next.Add(s);
                            break;
                        case "fext":
                            fext.Add(s);
                            break;
                        case "last":
                            last.Add(s);
                            break;
                    }
                }
            }

            testConfigLoss.AnalyzeItems = loss;
            testConfigNext.AnalyzeItems = next;
            testConfigFext.AnalyzeItems = fext;
            testConfigLast.AnalyzeItems = last;

            int lossStep = 120 / pairSum ;
            int nextStep = 120 / pairSum;
            int fextStep = 120 / pairSum;
            for (int i = 0; i < diffPairNum; i++)
            {
                Pair pair=new Pair();
                pair.PairName = pnProject.DiffPair[i];
                pair.SwitchIndex = lossSwitch[i];
                pair.ProgressValue = lossStep;
                lossPairs.Add(pair);
            }

            for (int i = 0; i < nextPairNum; i++)
            {
                Pair pair = new Pair();
                pair.PairName = pnProject.NextPair[i];
                pair.SwitchIndex = nextSwitch[i];
                pair.ProgressValue = nextStep;
                nextPairs.Add(pair);
            }

            for (int i = 0; i < fextPairNum; i++)
            {
                Pair pair = new Pair();
                pair.PairName = pnProject.FextPair[i];
                pair.SwitchIndex = fextSwitch[i];
                pair.ProgressValue = fextStep;
                fextPairs.Add(pair);
            }

            testConfigLoss.Pairs = lossPairs;
            testConfigNext.Pairs = nextPairs;
            testConfigFext.Pairs = fextPairs;

            testConfigLoss.ItemType = ItemType.Loss;
            testConfigNext.ItemType = ItemType.Next;
            testConfigFext.ItemType = ItemType.Fext;
            testConfigLast.ItemType = ItemType.Last;

            List<TdrParam> tdrParams = new List<TdrParam>();
            tdrParams.Add(pnProject.Tdd11);
            tdrParams.Add(pnProject.Tdd22);
            testConfigLoss.TdrParams = tdrParams;
            testConfigLoss.Sreverse = pnProject.Srevert;
            testConfigLoss.Treverse = pnProject.Trevert;

         
            testConfigs[0] = testConfigLoss;
            testConfigs[1] = testConfigNext;
            testConfigs[2] = testConfigFext;
            testConfigs[3] = testConfigLast;

            testConfigLoss.IldSpec = pnProject.Ild;

            return true;
        }

        public static bool SetHardware(Hardware hardware,ref ISwitch iswitch,ref NetworkAnalyzer iNetworkAnalyzer, ref string msg,Func<DialogResult>blockedMsg)
        {
            try
            {
                switch (hardware.SwitchBox)
                {
                    case SwitchBox.MCU:
                        iswitch = new SwitchMcu(hardware.VisaSwitchBox,hardware.SwitchResponseTime);
                        break;
                    case SwitchBox.Demo:
                        iswitch = new SwitchDemo();
                        break;
                    case SwitchBox.Manual:
                        iswitch = new SwitchManual((Func<DialogResult>);
                        break;
                }


                switch (hardware.Analyzer)
                {
                    case VirtualVNA.Enum.NetworkAnalyzer.Demo:
                        iNetworkAnalyzer = new DemoAnalyzer(iswitch, hardware.VisaNetWorkAnalyzer, hardware.AnalyzerResponseTime);
                        break;
                    case VirtualVNA.Enum.NetworkAnalyzer.N5224A:
                        iNetworkAnalyzer = new N5224A(iswitch, hardware.VisaNetWorkAnalyzer,hardware.AnalyzerResponseTime);
                        break;
                }

                return true;

            }
            catch (Exception e)
            {
                msg = e.Message;
                return false;
            }


        }

      
    }
}
