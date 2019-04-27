using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CustomerControl;
using DevComponents.DotNetBar.Controls;

namespace EEPROM.Code.View
{
    public class FormUI
    {
        public static void SetControlState(Control ctl,bool enable)
        {
            if (ctl.InvokeRequired)
            {
                Action<Control, bool> d = SetControlState;
                ctl.Invoke(d, new object[] { ctl,enable });
            }
            else
            {
                ctl.Enabled = enable;
            }
        }
        public static string FormatMsg(string msgIn)
        {
            return DateTime.Now.ToString() + "    " + msgIn;
        }
        public static void AddStatus(RichTextBox rTextStatus,string msg)
        {
            if (rTextStatus.InvokeRequired)
            {
                Action<RichTextBox,string> d = AddStatus;
                rTextStatus.Invoke(d, new object[] { rTextStatus,msg });
            }
            else
            {
                rTextStatus.AppendText(FormatMsg(msg) + "\n");
            }

        }

        public static void AddStatus(ToolStripStatusLabel rStatusLabel, string msg)
        {
            var parent = rStatusLabel.Owner;
            if (parent.InvokeRequired)
            {
                Action<ToolStripStatusLabel, string> d = AddStatus;
                parent.Invoke(d, new object[] { rStatusLabel,msg });
            }
            else
            {
                rStatusLabel.Text=FormatMsg(msg);
            }

        }

        public static void SetProgress(ProgressBar pgbTest, int value, bool step)
        {
            if (pgbTest.InvokeRequired)
            {
                Action<ProgressBar,int, bool> d = SetProgress;
                pgbTest.Invoke(d, new object[] { pgbTest,value, step });
            }
            else
            {
                if (step)
                {
                    pgbTest.Value = pgbTest.Value + value;
                }
                else
                {
                    pgbTest.Value = value;
                }

            }
        }

        public static void SetResult(BlinkLabel labelResult, string msg)
        {
            if (labelResult.InvokeRequired)
            {
                Action<BlinkLabel,string> d = SetResult;
                labelResult.Invoke(d, new object[] { labelResult, msg });
            }
            else
            {
                labelResult.Text = msg;
                if (msg == "TEST")
                {
                    labelResult.ForeColor = Color.Blue;
                    labelResult.TextMove = true;
                    labelResult.Blink = true;
                    labelResult.BlinkColor = Color.DarkSeaGreen;
                    labelResult.Interval = 400;


                }
                if (msg == "PASS")
                {
                    labelResult.ForeColor = Color.Green;
                    labelResult.Blink = false;
                }
                if (msg == "FAIL")
                {
                    labelResult.ForeColor = Color.Red;
                    labelResult.Blink = false;
                }

            }
        }

        public static void SetGrid(DataGridViewX dgvX,byte[]data)
        {
            if (dgvX.InvokeRequired)
            {
                Action<DataGridViewX, byte[]> d = SetGrid;
                dgvX.Invoke(d, new object[] { dgvX ,data});
            }
            else
            {
                int length = data.Length;
                for (int i = 0; i < length; i++)
                {
                    int row = i / 16;
                    int column = i % 16;
                    dgvX[column, row].Value = Convert.ToString(data[i], 16).PadLeft(2, '0').ToUpper();
                }
            }
           
        }

       

        public static void SetListView(ListView lstView,string[][]value)
        {
            if (lstView.InvokeRequired)
            {
                Action<ListView, string[][]> d = SetListView;
                lstView.Invoke(d, new object[] { lstView, value });
            }
            else
            {
                lstView.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
                lstView.Items.Clear();

                for (int i = 0; i < value.Length; i++)   //添加10行数据
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = value[i][0];
                    lvi.SubItems.Add(value[i][1]);
                    lvi.SubItems.Add(value[i][2]);
                    lvi.SubItems.Add(value[i][3]);
                    lvi.SubItems.Add(value[i][4]);
                    lvi.SubItems.Add(value[i][5]);
                    lvi.SubItems.Add(value[i][6]);
                    lstView.Items.Add(lvi);
                }

                lstView.EndUpdate();  //结束数据处理，UI界面一次性绘制。
            }
        }
    }
}
