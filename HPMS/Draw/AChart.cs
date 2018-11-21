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
            int iSeed = 10;
            Random ro = new Random(10);
            long tick = DateTime.Now.Ticks;
            Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));

            int R = ran.Next(255);
            int G = ran.Next(255);
            int B = ran.Next(255);
            B = (R + G > 400) ? R + G - 400 : B;//0 : 380 - R - G;
            B = (B > 255) ? 255 : B;
            return Color.FromArgb(R, G, B);
        }
    }
}
