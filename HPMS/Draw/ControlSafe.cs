﻿using System;
using System.Drawing;
using System.Windows.Forms;
//using System.Linq;

namespace HPMS.Draw
{
    public class ControlSafe
    {
        //定义delegate以便Invoke时使用   
        private delegate void SetControlEnable(Control control,bool value);
        //跟SetProgressBarValue委托相匹配的方法   
        public static void Setcontrolenable(Control control, bool value)  
        {
            //control.Enabled = value;
            if (control.InvokeRequired)
            {
                SetControlEnable d = Setcontrolenable;
                control.BeginInvoke(d, new object[] { control, value });
            }
            else
            {
                control.Enabled = value;
            }  

        }
        private delegate void SetControlFouce(Control control);
        //跟SetProgressBarValue委托相匹配的方法   
        public static void SetcontrolFouce(Control control)
        {
            //control.Enabled = value;
            if (control.InvokeRequired)
            {
                SetControlFouce d = SetcontrolFouce;
                control.BeginInvoke(d, new object[] { control });
            }
            else
            {
                control.Focus();
                control.Text = "";
            }

        }

        private delegate void DSetListbox(ListBox listBox, string textValue);
        public static void SetListbox(ListBox listBox,string textValue)
        {
            if (listBox.InvokeRequired)
            {
                DSetListbox dSetListbox = SetListbox;
                listBox.BeginInvoke(dSetListbox, new object[] { listBox,textValue});
            }
            else
            {
                listBox.SelectedIndex = listBox.FindString(textValue);
            }
        }
        //定义delegate以便Invoke时使用   
        private delegate void SetControlInfo(Control control, string value, object ForeColor, object BackColor);
        //跟SetProgressBarValue委托相匹配的方法   
        public static void SetcontrolInfo(Control control, string value, object ForeColor, object BackColor)
        {
            //control.Enabled = value;
            if (control.InvokeRequired)
            {
                SetControlInfo d = SetcontrolInfo;
                control.BeginInvoke(d, new object[] { control, value, ForeColor, BackColor });
            }
            else
            {
                control.ForeColor = (Color)ForeColor;              
                control.BackColor = (Color)BackColor;
                control.Text = value;
            }

        }
        public static DataGridView DataGridView1 = null;
        public static void ActDataGridView(DataGridView XX_Name)
        {
            DataGridView1 = XX_Name;
        }
        private delegate void SetDataGridViewDelegate(/*DataGridView DataGridView,*/ Color color, string info);
        public static void SetDataGridViewValueAndColor1(/*DataGridView DataGridView,*/ Color color, string info)
        {
            if (DataGridView1 == null)
                return;
            if (DataGridView1.InvokeRequired)
            {
                SetDataGridViewDelegate d = SetDataGridViewValueAndColor1;
                DataGridView1.BeginInvoke(d, new object[] { color, info });
            }
            else
            {
                //DataGridView1.BackColor = color;           
                //DataGridView.Text = info;
                AddResultToGridView(ref DataGridView1, info);
                DataGridView1.Rows[0].DefaultCellStyle.BackColor = color;
                Application.DoEvents();
                //DataGridView1.Update();
            }
        }
        public static void AddResultToGridView(ref DataGridView dView, string result)
        {
            string[] temp = new string[20];
            try
            {
                int ii = dView.RowCount;
                //dView.RowCount++;
                temp = result.Split(new char[] { ',' });                
               // dView.Rows.Insert(ii-1, temp);// Add(temp);                
                dView.Rows.Add(temp);// Add(temp); 
                dView.Rows[ii].Selected = true; 
                //if (result.Contains("Fail"))
                //    dView.DefaultCellStyle.BackColor = Color.Red;
                //else if (result.Contains("OK"))
                //    dView.DefaultCellStyle.BackColor = Color.Green;
                temp = null;
            }
            catch (Exception ex)
            {
                
                temp = null;
            }
        }

    }
    
}