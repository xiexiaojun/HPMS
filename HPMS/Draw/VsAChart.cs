using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using DevComponents.DotNetBar;
using TabControl = DevComponents.DotNetBar.TabControl;

namespace HPMS.Draw
{
    class VsAChart : AChart
    {
        private TabControl doneTabControl;

        public VsAChart(TabControl tabControl)
        {
            this.doneTabControl = tabControl;
        }

        public override object ChartAdd(string testItem)
        {
            TabItem tim = doneTabControl.CreateTab(testItem);
            tim.Name = testItem;
            Chart chart = new Chart();
            chart.Name = testItem;
            chart.Dock = DockStyle.Fill;
            //chart.Width = 999;
            //chart.Height = 336;
            chart.Location = new Point(4, 0);
            Legend legend = new Legend("legend");
            legend.Title = "Legend";
            legend.Font = new Font("Consolas", 11F,
                System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chart.Legends.Add(legend);


            ChartArea chartArea = new ChartArea("ChartArea1");
            chartArea.CursorX.IsUserEnabled = true;
            chartArea.CursorX.IsUserSelectionEnabled = true;
            chartArea.CursorX.LineDashStyle = ChartDashStyle.DashDotDot;
            chartArea.CursorY.IsUserEnabled = true;
            chartArea.CursorY.IsUserSelectionEnabled = true;
            chartArea.CursorY.LineDashStyle = ChartDashStyle.DashDotDot;
            chart.ChartAreas.Add(chartArea);
            tim.AttachedControl.Controls.Add(chart);
            chart.ChartAreas[0].AxisY.IsStartedFromZero = false;
            chart.Series.Clear();
            return chart;
        }

        public override bool ChartDel(string testItem)
        {
            // chartDic.Remove(testItem);
            TabItem timRemove = doneTabControl.Tabs[testItem];
            doneTabControl.Tabs.Remove(timRemove);
            return true;
        }
        delegate void SetDrawLineCallBack(Chart chart, plotData temp, string serialName, LineType lineType);
        public override void DrawLine(object oChart, plotData temp, string seriName, LineType lineType)
        {
            Chart chart = (Chart)oChart;
            if (chart.InvokeRequired)
            {

                SetDrawLineCallBack d = DrawLine;
                chart.Invoke(d, new object[] { chart, temp, seriName, lineType });
            }
            else
            {
                //绑定数据
                int index = chart.Series.Count;


                chart.Series.Add(seriName);

                Series currentSeries = chart.Series[index];
                //chart.Titles[index].Alignment = System.Drawing.ContentAlignment.TopRight;
                currentSeries.XValueType = ChartValueType.Single;  //设置X轴上的值类型
                //currentSeries.Label = "#VAL";                //设置显示X Y的值    
                //currentSeries.LabelForeColor = Color.Black;
                currentSeries.ToolTip = "#VALX:#VAL";     //鼠标移动到对应点显示数值
                currentSeries.ChartType = SeriesChartType.FastLine;    //图类型(折线)
                //currentSeries.ChartType = SeriesChartType.Line;    //图类型(折线)
                currentSeries.IsValueShownAsLabel = false;
                currentSeries.LegendText = seriName;
                currentSeries.IsVisibleInLegend = true;
                //chart.Legends[seriName].Enabled = true;
                //chart.Legends[seriName].MaximumAutoSize = 15;
                //chart.Series[0].IsValueShownAsLabel = true;

                // currentSeries.LabelForeColor = Color.Black;

                // currentSeries.CustomProperties = "DrawingStyle = Cylinder";
                currentSeries.Points.DataBindXY(temp.xData, temp.yData);

                switch (lineType)
                {
                    case LineType.Fre:
                        for (int i = 1; i < 10; i++)
                        {
                            CustomLabel label = new CustomLabel();
                            label.Text = (i * 5).ToString() + "Ghz";
                            label.ToPosition = i * 10000000000;
                            chart.ChartAreas[0].AxisX.CustomLabels.Add(label);
                            label.GridTicks = GridTickTypes.Gridline;
                        }
                        break;
                    case LineType.Time:
                        for (int i = 1; i < 10; i++)
                        {
                            CustomLabel label = new CustomLabel();
                            label.Text = (i * 1).ToString() + "ns";
                            label.ToPosition = (float)i * 2;
                            chart.ChartAreas[0].AxisX.CustomLabels.Add(label);
                            label.GridTicks = GridTickTypes.Gridline;
                        }
                        break;

                }

                //chart.Visible = true;
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
        delegate void SetchartClearCallback(Chart chart);
        public override void ChartClear(object tChart)
        {
            Chart chart = (Chart)tChart;
            if (chart.InvokeRequired)
            {
                SetchartClearCallback d = new SetchartClearCallback(ChartClear);
                chart.Invoke(d, new object[] { chart });
            }
            else
            {
                chart.ChartAreas[0].AxisX.CustomLabels.Clear();
                chart.DataBindings.Clear();
                chart.Series.Clear();
                
               
            }
        }
    }
}
