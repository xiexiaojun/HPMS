using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using ZedGraph;
using TabControl = DevComponents.DotNetBar.TabControl;

namespace HPMS.Draw
{
    class ZedAChart : AChart
    {
        private TabControl doneTabControl;

        public ZedAChart(TabControl tabControl)
        {
            this.doneTabControl = tabControl;
        }
        public override object ChartAdd(string testItem)
        {
            TabItem tim = doneTabControl.CreateTab(testItem);
            tim.Name = testItem;
            ZedGraphControl zedGraph = new ZedGraphControl();
            zedGraph.AutoSizeMode = AutoSizeMode.GrowOnly;
            zedGraph.Height = 237;
            zedGraph.Width = 751;
            zedGraph.Location = new Point(2, -1);


            GraphPane myPane = zedGraph.GraphPane;

            // Set the titles and axis labels

            myPane.Title.Text = testItem;
            myPane.Title.IsVisible = false;
            //myPane.Title.FontSpec=
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.XAxis.MajorGrid.DashOn = 1000;
            myPane.YAxis.MajorGrid.DashOn = 1000;

            myPane.YAxis.MajorGrid.IsVisible = true;
            myPane.XAxis.Title.IsVisible = false;
            // myPane.XAxis.Title.Text = "X Value Figo";
            myPane.Chart.Border.IsVisible = false;//首先设置边框为无
            myPane.XAxis.MajorTic.IsOpposite = false;//然后设置X轴对面轴大间隔为无
            myPane.XAxis.MinorTic.IsOpposite = false;//然后设置X轴对面轴大间隔为无
            myPane.YAxis.MajorTic.IsOpposite = false;//设置Y轴对面轴大间隔为无
            myPane.YAxis.MinorTic.IsOpposite = false;//设置Y轴对面轴小间隔为无
            myPane.YAxis.Title.IsVisible = false;

            myPane.XAxis.MinorGrid.IsVisible = false;

            if (testItem.StartsWith("T"))
            {
                myPane.XAxis.Scale.MajorStep = 1;
                myPane.XAxis.Scale.Max = 5.0;
            }
            else
            {
                myPane.XAxis.Scale.MajorStep = 5000000000;
                myPane.XAxis.Scale.Max = 26500000000;
            }


            myPane.XAxis.ScaleFormatEvent += XAxis_ScaleFormatEvent;
            myPane.CurveList.Clear();
            tim.AttachedControl.Controls.Add(zedGraph);
            return zedGraph;
        }

        public override bool ChartDel(string testItem)
        {
            // chartDic.Remove(testItem);
            TabItem timRemove = doneTabControl.Tabs[testItem];
            doneTabControl.Tabs.Remove(timRemove);
            return true;
        }
        delegate void SetDrawLineCallBack(ZedGraphControl chart, plotData temp, string serialName, LineType lineType);
        public override void DrawLine(object tChart, plotData temp, string seriName, LineType lineType)
        {
            ZedGraphControl chart = (ZedGraphControl)tChart;
            GraphPane myPane = chart.GraphPane;
            if (chart.InvokeRequired)
            {

                SetDrawLineCallBack d = DrawLine;
                chart.Invoke(d, new object[] { chart, temp, seriName, lineType });
            }
            else
            {


                PointPairList list = new PointPairList();
                //int length = temp.xData.Length;

                //for (int i = 0; i < length; i++)
                //{
                //    list.Add(temp.xData[i],temp.yData[i]);

                //}
                //double[]a=new double[100];
                // Generate a blue curve with circle symbols, and "My Curve 2" in the legend
                LineItem myCurve1 =
                    new LineItem(seriName, temp.xData.Select(x => (double)x).ToArray(), temp.yData.Select(x => (double)x).ToArray(),
                        lineType == LineType.Spec ? Color.Red : GetRandomColor(), SymbolType.None, 0.1f);
                myCurve1.Line.IsSmooth = true;
                myCurve1.Line.SmoothTension = 0.1F;
                myCurve1.Line.GradientFill.Type = FillType.Brush;
                myPane.CurveList.Add(myCurve1);
                // myPane.AddCurve(seriName, temp.xData.Select(x => (double)x).ToArray(), temp.yData.Select(x => (double)x).ToArray(), lineType == LineType.Spec ? Color.Red : Util.getRandomColor(), SymbolType.None);

                myPane.Legend.Position = LegendPos.InsideBotRight;



                chart.AxisChange();
                //chart.IsAntiAlias = true;
                chart.Refresh();
            }
        }

        public override void DrawSpec(string itemName, Dictionary<string, plotData> spec, object tChart)
        {
            if (spec.ContainsKey(itemName + "_UPPER"))
            {
                DrawLine(tChart, spec[itemName + "_UPPER"], itemName + "_UPPER", LineType.Spec);
            }
            if (spec.ContainsKey(itemName + "_LOWER"))
            {
                DrawLine(tChart, spec[itemName + "_LOWER"], itemName + "_LOWER", LineType.Spec);
            }
        }
        delegate void SetchartClearCallback(ZedGraphControl chart);
        public override void ChartClear(object tChart)
        {
            ZedGraphControl chart = (ZedGraphControl)tChart;
            if (chart.InvokeRequired)
            {
                SetchartClearCallback d = new SetchartClearCallback(ChartClear);
                chart.Invoke(d, new object[] { chart });
            }
            else
            {
                chart.GraphPane.CurveList.Clear();
                chart.Refresh();
            }
        }

        public override void ChartClear()
        {
            doneTabControl.Tabs.Clear();
        }

        string XAxis_ScaleFormatEvent(GraphPane pane, Axis axis, double val, int index)
        {
            string name = pane.Title.Text;
            if (name.StartsWith("T"))
            {
                return (index + "ns");
            }
            else
            {
                return (index * 5 + "Ghz");
            }
            //根据 val值 返回你需要的 string

        }
    }
}
