using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Charts;
using DevComponents.DotNetBar.Charts.Style;
using TabControl = DevComponents.DotNetBar.TabControl;

namespace HPMS.Draw
{
    public class DotnetChart:AChart
    {
        private TabControl doneTabControl;

        public DotnetChart(TabControl tabControl)
        {
            this.doneTabControl = tabControl;
        }
        public override object ChartAdd(string testItem)
        {
            TabItem tim = doneTabControl.CreateTab(testItem);
            tim.Name = testItem;
            ChartControl donetChart = new ChartControl();
            donetChart.Name = testItem;
            donetChart.Dock = DockStyle.Fill;
            
            ChartXy chartXy=new ChartXy(testItem);
            chartXy.Legend.Visible = true;
            chartXy.MatrixDisplayOrder = 0;
            //chartXy.MatrixDisplayBounds = new Rectangle(0, 0, 8, 6);

            // The following tells the chart control to align the start
            // and ending bounding columns with other charts that start/end
            // in the same columns.
           // ChartXy bb = (ChartXy)donetChart.ChartPanel.ChartContainers[0];
           
            chartXy.MatrixAlignStartColumn = true;
            chartXy.MatrixAlignEndColumn = true;

            // Setup our Crosshair display.

            chartXy.ChartCrosshair.AxisOrientation = AxisOrientation.X;

            chartXy.ChartCrosshair.ShowValueXLine = true;
            chartXy.ChartCrosshair.ShowValueYLine = true;
            chartXy.ChartCrosshair.ShowValueYLabels = true;

            chartXy.ChartCrosshair.ShowCrosshairLabels = true;
            chartXy.ChartCrosshair.CrosshairLabelMode = CrosshairLabelMode.NearestSeries;
            SetupChartStyle(chartXy);
            SetupContainerStyle(chartXy);
            SetupChartAxes(chartXy);
            SetupChartLegend(chartXy);
            donetChart.ChartPanel.ChartContainers.Add(chartXy);
            tim.AttachedControl.Controls.Add(donetChart);
            return chartXy;



            throw new NotImplementedException();

        }

        private void SetupChartStyle(ChartXy chartXy)
        {
            ChartXyVisualStyle cstyle = chartXy.ChartVisualStyle;

            cstyle.Background = new Background(Color.White);
            cstyle.BorderThickness = new Thickness(1);
            cstyle.BorderColor = new BorderColor(Color.Black);

            cstyle.Padding = new DevComponents.DotNetBar.Charts.Style.Padding(6);

            ChartSeriesVisualStyle sstyle = chartXy.ChartSeriesVisualStyle;
            PointMarkerVisualStyle pstyle = sstyle.MarkerHighlightVisualStyle;

            pstyle.Background = new Background(Color.Yellow);
            pstyle.Type = PointMarkerType.Ellipse;
            pstyle.Size = new Size(15, 15);

            CrosshairVisualStyle chstyle = chartXy.ChartCrosshair.CrosshairVisualStyle;

            chstyle.ValueXLineStyle.LineColor = Color.Navy;
            chstyle.ValueXLineStyle.LinePattern = LinePattern.Dot;

            chstyle.ValueYLineStyle.LineColor = Color.Navy;
            chstyle.ValueYLineStyle.LinePattern = LinePattern.Dot;
        }

        private void SetupContainerStyle(ChartXy chartXy)
        {
            ContainerVisualStyle dstyle = chartXy.ContainerVisualStyles.Default;

            dstyle.Background = new Background(Color.White);
            dstyle.BorderColor = new BorderColor(Color.DimGray);
            dstyle.BorderThickness = new Thickness(1);

            dstyle.DropShadow.Enabled = Tbool.True;
            dstyle.Padding = new DevComponents.DotNetBar.Charts.Style.Padding(6);

            // Add an image of our fowl featherd friends
            // at the top right of the container.

            // dstyle.Image = ShellServices.LoadBitmap("LinePlot_Appetites.Resources.Chicken2.png");
            dstyle.ImageAlignment = Alignment.TopRight;
            dstyle.ImagePadding = new DevComponents.DotNetBar.Charts.Style.Padding(0, 20, 20, 0);
        }

        private void SetupChartLegend(ChartXy chartXy)
        {
            ChartLegend legend = chartXy.Legend;

            legend.ShowCheckBoxes = true;

            legend.Placement = Placement.Inside;
            legend.Alignment = Alignment.TopLeft;
            legend.Direction = Direction.LeftToRight;

            // Align vertical items, and permit the legend to only use
            // up to 50% of the available chart width;

            legend.AlignVerticalItems = true;
            legend.MaxHorizontalPct = 50;

            ChartLegendVisualStyle lstyle = legend.ChartLegendVisualStyles.Default;

            lstyle.BorderThickness = new Thickness(1);
            lstyle.BorderColor = new BorderColor(Color.Crimson);

            lstyle.Margin = new DevComponents.DotNetBar.Charts.Style.Padding(8);
            lstyle.Padding = new DevComponents.DotNetBar.Charts.Style.Padding(4);

            lstyle.Background = new Background(Color.FromArgb(200, Color.White));
        }

        private void SetupChartAxes(ChartXy chartXy)
        {
            // X Axis

            ChartAxis axis = chartXy.AxisX;

            //axis.GridSpacing = 5;

            axis.AxisMargins = 20;
            axis.MinGridInterval = 50;

            axis.MinorTickmarks.TickmarkCount = 0;
            axis.MajorTickmarks.StaggerLabels = true;
            axis.MaxValue = 26.5;

            axis.MajorGridLines.GridLinesVisualStyle.LineColor = Color.Gainsboro;
            axis.MinorGridLines.GridLinesVisualStyle.LineColor = Color.WhiteSmoke;

            // Set our alternate background to a nice MidnightBlue.

            axis.ChartAxisVisualStyle.AlternateBackground =
                new Background(Color.FromArgb(20, Color.MidnightBlue));

            axis.UseAlternateBackground = true;
           
            

            // Y Axis

            axis = chartXy.AxisY;

            axis.AxisMargins = 20;
            axis.GridSpacing = 25;

            axis.AxisAlignment = AxisAlignment.Far;
            axis.MinorTickmarks.TickmarkCount = 0;

            // Set our axis title appropriately.

            axis.Title.Text = "Percent Change";
            axis.Title.ChartTitleVisualStyle.Padding = new DevComponents.DotNetBar.Charts.Style.Padding(4, 0, 4, 0);
            axis.Title.ChartTitleVisualStyle.Font = new Font("Georgia", 10);
            axis.Title.ChartTitleVisualStyle.Alignment = Alignment.MiddleCenter;

            axis.MajorGridLines.GridLinesVisualStyle.LineColor = Color.Gainsboro;
            axis.MinorGridLines.GridLinesVisualStyle.LineColor = Color.WhiteSmoke;

            axis.ChartAxisVisualStyle.AlternateBackground = new Background(Color.FromArgb(30, Color.DarkKhaki));
        }

      

        public override bool ChartDel(string testItem)
        {
            throw new NotImplementedException();
        }

        delegate void SetDrawLineCallBack(ChartXy chart, plotData temp, string serialName, LineType lineType);

        public override void DrawLine(object tChart, plotData temp, string seriName, LineType lineType)
        {
            ChartXy chart = (ChartXy) tChart;
            if (chart.ChartControl.InvokeRequired)
            {

                SetDrawLineCallBack d = DrawLine;
                chart.ChartControl.Invoke(d, new object[] {chart, temp, seriName, lineType});
            }
            else
            {
                ChartSeries series = new ChartSeries(seriName, SeriesType.Line);
                //绑定数据
                for (int i = 0; i < temp.xData.Length; i++)
                {

                    SeriesPoint sp = new SeriesPoint(temp.xData[i], temp.yData[i]);
                 

                    series.SeriesPoints.Add(sp);
                 
                }



               // series.ChartSeriesVisualStyle.LineStyle.LineColor = Color.Maroon;
                
                series.ChartSeriesVisualStyle.LineStyle.LineWidth = 2;
                series.ChartSeriesVisualStyle.LineStyle.LinePattern = LinePattern.Solid;
               

                chart.ChartSeries.Add(series);
              


                //switch (lineType)
                //{
                //    case LineType.Fre:
                //        for (int i = 1; i < 10; i++)
                //        {
                //            CustomLabel label = new CustomLabel();
                //            label.Text = (i * 5).ToString() + "Ghz";
                //            label.ToPosition = i * 10000000000;
                //            chart.ChartAreas[0].AxisX.CustomLabels.Add(label);
                //            label.GridTicks = GridTickTypes.Gridline;
                //        }

                //        break;
                //    case LineType.Time:
                //        for (int i = 1; i < 10; i++)
                //        {
                //            CustomLabel label = new CustomLabel();
                //            label.Text = (i * 1).ToString() + "ns";
                //            label.ToPosition = (float) i * 2;
                //            chart.ChartAreas[0].AxisX.CustomLabels.Add(label);
                //            label.GridTicks = GridTickTypes.Gridline;
                //        }

                //        break;

                //}
            }
        }

        public override void DrawSpec(string itemName, Dictionary<string, plotData> spec, object tChart)
        {
            throw new NotImplementedException();
        }


        delegate void SetchartClearCallback(ChartXy chart);
        public override void ChartClear(object tChart)
        {
            ChartXy chart = (ChartXy)tChart;
            //var bb = chart.Name;
            //var aa = chart.chartc
            //var cc = aa.Parent;

            if (chart.ChartControl.InvokeRequired)
            {
                SetchartClearCallback d = new SetchartClearCallback(ChartClear);
                chart.ChartControl.Invoke(d, new object[] { chart });
            }
            else
            {
                chart.ChartSeries.Clear();
                
                //chart.ChartAreas[0].AxisX.CustomLabels.Clear();
                //chart.DataBindings.Clear();
                //chart.Series.Clear();


            }
        }

        public override void ChartClear()
        {
            doneTabControl.Tabs.Clear();
        }
    }
}
