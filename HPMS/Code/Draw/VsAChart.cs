using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using DevComponents.DotNetBar;
using HPMS.Code.Utility;
using TabControl = DevComponents.DotNetBar.TabControl;

namespace HPMS.Code.Draw
{
    class VsAChart : AChart
    {
        private TabControl doneTabControl;
        private EventHandler tim_Click;

        public VsAChart(TabControl tabControl, EventHandler timClick)
        {
            this.doneTabControl = tabControl;
            this.tim_Click = timClick;
        }

        public VsAChart(TabControl tabControl)
        {

            this.doneTabControl = tabControl;
        }

        public override object ChartAdd(string testItem)
        {
            TabItem tim = doneTabControl.CreateTab(testItem);
            if (tim_Click != null)
            {
                tim.Click += tim_Click;
            }
            
            tim.Name = testItem;
            Chart chart = new Chart();
           // chart.Palette = ChartColorPalette.Bright;
            chart.Palette = ChartColorPalette.None;
            chart.PaletteCustomColors = new Color[] {  Color.Blue,  Color.Green, Color.Black, Color.Brown, Color.Cyan, Color.Magenta,
                Color.LightGreen, Color.Orange, Color.Peru, Color.DarkRed, Color.LightBlue, Color.SlateGray, Color.Red};
            chart.Name = testItem;
            chart.Dock = DockStyle.Fill;
            //chart.Width = 999;
            //chart.Height = 336;
            chart.Location = new Point(4, 0);
            if (testItem.StartsWith("T"))
            {
                chart.GetToolTipText += chart_GetToolTipTextTime;
            }
            else
            {
                chart.GetToolTipText += chart_GetToolTipTextFre;
            }
           


            ChartArea chartArea = new ChartArea("ChartArea1");
            chartArea.CursorX.IsUserEnabled = true;
            chartArea.CursorX.IsUserSelectionEnabled = true;
            chartArea.CursorX.LineDashStyle = ChartDashStyle.DashDotDot;
            chartArea.CursorY.IsUserEnabled = true;
            chartArea.CursorY.IsUserSelectionEnabled = true;
            chartArea.CursorY.LineDashStyle = ChartDashStyle.DashDotDot;
            //chartArea.Position.Auto = false;
            //chartArea.Position.Height = 98;
            //chartArea.Position.Width = 98;

            //chartArea.BackColor = Color.DarkGoldenrod;
            //chartArea.AxisX.MajorGrid.LineColor = Color.LawnGreen;
            //chartArea.AxisY.MajorGrid.LineColor = Color.LawnGreen;
            //chartArea.AxisX.MajorGrid.
            //chartArea.AxisX.MajorGrid.in
            chart.ChartAreas.Add(chartArea);

            Legend legend = new Legend("legend");
            legend.Title = "Legend";
            legend.Font = new Font("Arial", 9F,
                System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //legend.InsideChartArea = "ChartArea1";

            legend.CellColumns.Clear();

            legend.CellColumns.Add(new LegendCellColumn()
            {
                Name = "chbx",
                ColumnType = LegendCellColumnType.Text,
                Text = "#CUSTOMPROPERTY(CHECK)",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.Transparent
            });

            legend.CellColumns.Add(new LegendCellColumn()
            {
                Name = "symbol",
                ColumnType = LegendCellColumnType.SeriesSymbol,
                BackColor = Color.Transparent
            });
            LegendCellColumn a=new LegendCellColumn();
            //a.BackColor = Color.Transparent;
            
            legend.CellColumns.Add(new LegendCellColumn()
            {
                Name = "title",
                ColumnType = LegendCellColumnType.Text,
                Text = "#LEGENDTEXT",
                Alignment = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent
            });
            chart.Legends.Add(legend);
            chart.MouseDown+=chart_MouseDown;
            tim.AttachedControl.Controls.Add(chart);
            chart.ChartAreas[0].AxisY.IsStartedFromZero = false;
            chart.Series.Clear();

          
            return chart;
        }

        private void chart_GetToolTipTextTime(object sender, ToolTipEventArgs e)
        {
            //判断鼠标是否移动到数据标记点，是则显示提示信息
            if (e.HitTestResult.ChartElementType == ChartElementType.DataPoint)
            {
                int i = e.HitTestResult.PointIndex;
                DataPoint dp = e.HitTestResult.Series.Points[i];
                //分别显示x轴和y轴的数值，其中{1:F3},表示显示的是float类型，精确到小数点后3位。                     
                e.Text = string.Format("time:{0:F2}ns\nimpedance:{1:F2}Ω ", dp.XValue, dp.YValues[0]);

            
            }

          
        }

        private void chart_GetToolTipTextFre(object sender, ToolTipEventArgs e)
        {
            //判断鼠标是否移动到数据标记点，是则显示提示信息
            if (e.HitTestResult.ChartElementType == ChartElementType.DataPoint)
            {
                int i = e.HitTestResult.PointIndex;
                DataPoint dp = e.HitTestResult.Series.Points[i];
                //分别显示x轴和y轴的数值，其中{1:F3},表示显示的是float类型，精确到小数点后3位。                     
                e.Text = string.Format("fre:{0:F2}Ghz\nloss:{1:F2}dB ", dp.XValue/1000000000, dp.YValues[0]);


            }


        }


        private void chart_MouseDown(object sender, MouseEventArgs e)
        {
            Chart chart = (Chart) sender;
            HitTestResult result = chart.HitTest(e.X, e.Y);
            if (result != null && result.Object != null)
            {
                // When user hits the LegendItem
                if (result.Object is LegendItem)
                {
                    // Legend item result
                    LegendItem legendItem = (LegendItem)result.Object;
                    Series series = chart.Series[legendItem.SeriesName];

                    if (series.GetCustomProperty("CHECK").Equals("☑"))
                    {
                        series.SetCustomProperty("CHECK", "☐");
                        series.Color = Color.FromArgb(0, series.Color);
                        
                    }
                    else
                    {
                        series.SetCustomProperty("CHECK", "☑");
                        //series.Color = ColorTranslator.FromHtml(
                        //    series.GetCustomProperty("COLOR"));
                        series.Color = Color.FromArgb(255, series.Color);
                    }
                }
            }
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
                currentSeries.SetCustomProperty("CHECK", "☑");
                //chart.Titles[index].Alignment = System.Drawing.ContentAlignment.TopRight;
                currentSeries.XValueType = ChartValueType.Single;  //设置X轴上的值类型
                //currentSeries.Label = "#VAL";                //设置显示X Y的值    
                //currentSeries.LabelForeColor = Color.Black;
               // currentSeries.ToolTip = "#VALX:#VAL";     //鼠标移动到对应点显示数值
                currentSeries.ToolTip = string.Format("X - {0}/{2} , Y - {1}",  "#VALX",
                    "#VALY{F2}",1000);     //鼠标移动到对应点显示数值
                currentSeries.ChartType = SeriesChartType.FastLine;    //图类型(折线)
                //currentSeries.ChartType = SeriesChartType.Line;    //图类型(折线)
                currentSeries.IsValueShownAsLabel = false;
                currentSeries.LegendText = seriName;
                currentSeries.IsVisibleInLegend = true;
                currentSeries.BorderWidth = 1;
                
                //chart.Legends[seriName].Enabled = true;
                //chart.Legends[seriName].MaximumAutoSize = 15;
                //chart.Series[0].IsValueShownAsLabel = true;

                // currentSeries.LabelForeColor = Color.Black;

                // currentSeries.CustomProperties = "DrawingStyle = Cylinder";
                if (lineType == LineType.Spec)
                {
                    plotData trimPlotData = TrimNaN(temp);
                    currentSeries.Points.DataBindXY(trimPlotData.xData, trimPlotData.yData);
                }
                else
                {
                    currentSeries.Points.DataBindXY(temp.xData, temp.yData); 
                }
                
                
                //currentSeries.Points.AddXY(temp.xData, temp.yData);
                //CalloutAnnotation annotation = new CalloutAnnotation();
                //annotation.Text = seriName + ":" + temp.yData[0];
                //chart.Annotations.Add(annotation);
                //int anCount = chart.Annotations.Count;
                //var aa = currentSeries.Points;
                //chart.Annotations[anCount-1].AnchorDataPoint = currentSeries.Points[0];

                switch (lineType)
                {
                    case LineType.Fre:
                        for (int i = 1; i < 10; i++)
                        {
                            CustomLabel label = new CustomLabel();
                            label.Text = (i * 5).ToString() + "Ghz";
                            label.ToPosition = i * 10000000000;
                            chart.ChartAreas[0].AxisX.CustomLabels.Add(label);
                            label.GridTicks = GridTickTypes.TickMark;
                        }
                        break;
                    case LineType.Time:
                        for (int i = 1; i < 10; i++)
                        {
                            CustomLabel label = new CustomLabel();
                            label.Text = (i * 1).ToString() + "ns";
                            label.ToPosition = (float)i * 2;
                            chart.ChartAreas[0].AxisX.CustomLabels.Add(label);
                            label.GridTicks = GridTickTypes.TickMark;
                        }
                        break;
                    case LineType.Spec:
                        currentSeries.Color = Color.Red;
                        currentSeries.BorderWidth = 2;
                       
                        break;

                }
                Extende.SelectTab(chart);

                //chart.Visible = true;
            }

        }

        private plotData TrimNaN(plotData plotData)
        {
            Draw.plotData ret=new plotData();
            int length = plotData.yData.Length;
            List<float>x=new List<float>();
            List<float>y=new List<float>();
            for (int i = 0; i < length; i++)
            {
                if (!float.IsNaN(plotData.yData[i]))
                {
                    x.Add(plotData.xData[i]);
                    y.Add(plotData.yData[i]);
                }
            }

            ret.xData = x.ToArray();
            ret.yData = y.ToArray();
            return ret;
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

        delegate void ChartClearCallback();
        public override void ChartClear()
        {
            if (doneTabControl.InvokeRequired)
            {
                ChartClearCallback d=new ChartClearCallback(ChartClear);
                doneTabControl.Invoke(d);
            }
            doneTabControl.Tabs.Clear();
        }
    }
}
