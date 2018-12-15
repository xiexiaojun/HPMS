using System;
using System.Collections.Generic;
using System.Drawing;


namespace HPMS.Draw
{
    public enum LineType
    {
        Fre,
        Time,
        Spec
    }
    public struct plotData
    {
        public float[] xData;
        public float[] yData;
    }

    public abstract class AChart
    {
      
        public abstract object ChartAdd(string testItem);
        public abstract bool ChartDel(string testItem);
        public abstract void DrawLine(object tChart, plotData temp, string seriName, LineType lineType);
        public abstract void DrawSpec(string itemName, Dictionary<string, plotData> spec, object tChart);
        public abstract void ChartClear(object tChart);
        public abstract void ChartClear();

       

        public  Color GetRandomColor()
        {
            Color[] colors =
            {
                Color.Aqua,Color.Aquamarine,Color.Black,Color.Blue,Color.BlueViolet,
                Color.Brown,Color.BurlyWood,Color.CadetBlue,Color.Chartreuse,Color.Chocolate,
                Color.Coral,Color.CornflowerBlue,Color.Crimson,Color.Cyan,Color.DarkBlue,
                Color.DarkCyan,Color.DarkGoldenrod,Color.DarkGreen,Color.DarkMagenta,Color.DarkOliveGreen,
            };
          
            return MarkColor(20, 180);
            //return GetDarkerColor(System.Drawing.Color.FromArgb(int_Red, int_Green, int_Blue));
        }

        public static Color GetDarkerColor(Color color)
        {
            const int max = 255;
            int increase = new Random(Guid.NewGuid().GetHashCode()).Next(30, 255); //还可以根据需要调整此处的值


            int r = Math.Abs(Math.Min(color.R - increase, max));
            int g = Math.Abs(Math.Min(color.G - increase, max));
            int b = Math.Abs(Math.Min(color.B - increase, max));


            return Color.FromArgb(r, g, b);
        }


        private static Color MarkColor(int start, int end)
        {

            if (start < 0 || start > 255) throw new Exception("起始数值只能为0-255之间的数字");
            if (end < 0 || end > 255) throw new Exception("结束数值只能为0-255之间的数字");
            if (start > end) throw new Exception("起始数值不能大于结束数值");


            Random ran = new Random(Guid.NewGuid().GetHashCode());

            int R, G, B;
            double Y;
            bool result;

            do
            {
                R = ran.Next(0, 255);
                G = ran.Next(0, 255);
                B = ran.Next(0, 255);

                //Y值计算公式
                Y = 0.299 * R + 0.587 * G + 0.114 * B;

                result = Y >= start && Y <= end;
            } while (!result);

            return Color.FromArgb(R, G, B);
        }
    }
}
