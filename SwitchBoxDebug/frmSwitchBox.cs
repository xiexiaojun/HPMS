using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using WindowsFormsControlLibrary1;
using Tool;
using VirtualSwitch;

namespace SwitchBoxDebug
{
    public partial class frmSwitchBox : Office2007Muti
    {
        private int _bloclNum = 0;
        private Dictionary<int, RoundButton> leds = new Dictionary<int, RoundButton>();
        private ISwitch iSwitch = null;
        private Panel[] _panelBlocks = new Panel[8];
        private string[] _clickedLeds=new string[4];//点亮的led灯标记，最多4个
        public frmSwitchBox()
        {
            EnableGlass = false;
            InitializeComponent();
        }

        private void frmSwitchBox_Load(object sender, EventArgs e)
        {
            _panelBlocks = new[]
            {
                panel1, panel2, panel3, panel4, 
                panel5,panel6,panel7,panel8
            };
            //dgvBasic.AutoSizeRowsMode = DataGridViewAutoSizeRowMode.;
           // dgvBasic.RowHeadersDefaultCellStyle.Padding = new Padding(dgvBasic.RowHeadersWidth);
            foreach (var variable in _panelBlocks)
            {
                variable.Resize += panel_Resize;
               // variable.BackColor = Color.Aquamarine;
                variable.BorderStyle = BorderStyle.FixedSingle;
            }

            cmbBoxType.SelectedIndex = 2;
            cmbMapType.SelectedIndex = 0;
            cmbPair.SelectedIndex = 0;
            cmbSerialVisa_update();
            //SerialUpdate();
        }

        private void cmbBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBoxType.SelectedIndex != 2)
            {
                Ui.MessageBoxMuti("未检测到此种类型的开关盒子");
                cmbBoxType.SelectedIndex = 2;
                return;
            }

            BoxType boxType = (BoxType)Enum.Parse(typeof(BoxType), ((ComboBox)sender).SelectedItem.ToString());
            SetBoxInterface(boxType);
        }

        private void SetBoxInterface(BoxType boxType)
        {

            switch (boxType)
            {
                case BoxType.Switch40:
                    _bloclNum = 10;
                    break;
                case BoxType.Switch48:
                    _bloclNum = 6;
                    break;
                case BoxType.Switch80:
                    _bloclNum = 10;
                    break;
            }


            leds.Clear();
            Array.Clear(_clickedLeds,0,4);
            foreach (var panel in _panelBlocks)
            {
                panel.Controls.Clear();
            }
            for (int i = 0; i < _bloclNum; i++)
            {
                for (int j = 0; j < _panelBlocks.Length; j++)
                {
                    int rDictance = GetRdistance(_panelBlocks[j]);
                    SetLed(_bloclNum, i, rDictance, _panelBlocks[j], j % 2 == 0 ? "A" : "B");
                }


            }
            foreach (var panel in _panelBlocks)
            {
                SetSquare(panel);
            }

            if (boxType == BoxType.Switch40)
            {
                for (int j = 0; j < _panelBlocks.Length; j++)
                {
                    _panelBlocks[j].Visible = j % 2 == 0;
                }
            }
            else
            {
                for (int j = 0; j < _panelBlocks.Length; j++)
                {
                    _panelBlocks[j].Visible = true;
                }
            }
        }

        private void SetSquare(Panel panel)
        {
            int centerX = (int)(panel.Width * 0.5);
            int centerY = (int)(panel.Height * 0.5);
            //int centerX = 85;
            //int centerY = 95;
            RoundButton led = new RoundButton();
            // led.LedStyle = LedStyle.Square3D;
            led.Height = 30;
            led.Width = 30;

            led.Location = new Point(centerX - 15, centerY - 15);
            //led.InteractionMode = BooleanInteractionMode.Indicator;
            int block = int.Parse(panel.Name.Substring(5));
            //led.Name = (80 + block - 1).ToString();
            panel.Controls.Add(led);

            Label label=new Label();
            label.AutoSize = true;
            label.Text = "S"+block;
            label.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            label.Location = new Point(centerX - 5, centerY -25);
            label.Location = new Point(0, 0);
            label.ForeColor = Color.Crimson;
            panel.Controls.Add(label);
            
            led.Anchor = AnchorStyles.None;
            leds.Add(80 + block - 1, led);

        }

        private void SetLed(int total, int num, int r, Panel panel, string group)
        {

            int centerX = (int)(panel.Width * 0.5);
            int centerY = (int)(panel.Height * 0.5);

            RoundButton led = new RoundButton();

            double angel = 2.0 / total * Math.PI * (num + 1) - Math.PI * 0.5;

            led.Height = 30;
            led.Width = 30;

            int block = int.Parse(panel.Name.Substring(5));
            led.Name = ((block - 1) * 10 + num).ToString();
            led.Tag = group + block + num;
            SetLedlocation(led, centerX, centerY, r);
            led.Click += led_Click;

            Label label = new Label();
            label.AutoSize = true;
            label.Text = group + block + num;
            label.Font = new System.Drawing.Font("Arial", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            
            SetLabellocation(label, centerX, centerY, r);
            panel.Controls.Add(led);
            panel.Controls.Add(label);

            leds.Add((block - 1) * 10 + num, led);
        }

        private void led_Click(object sender, EventArgs e)
        {
            RoundButton led = (RoundButton)sender;
            string name = led.Name;
            int ledKey = int.Parse(name);
            int block = ledKey / 20;


            int start = 0 + block * 20;
            int end = 19 + block * 20;
            int squareOn = 80 + ledKey / 10;
            int squareOff = squareOn % 2 == 0 ? squareOn + 1 : squareOn - 1;
            if (e == EventArgs.Empty)
            {
               // led.Value = !led.Value;
                led.Value = true;
            }

            if (led.Value)
            {
                ClickedLedsRegister(led, block);
                foreach (var VARIABLE in leds)
                {
                    if ((VARIABLE.Key <= end) && (VARIABLE.Key >= start) && (VARIABLE.Key != ledKey))
                    {
                        VARIABLE.Value.Value = false;
                        //VARIABLE.Value.PerformClick();
                    }
                }

                leds[squareOn].Value = true;
                leds[squareOff].Value = false;

            }


        }

        private void ClickedLedsRegister(RoundButton led,int block)
        {
            _clickedLeds[block] = (string)led.Tag;
        }

      

        private void panel_Resize(object sender, EventArgs e)
        {
            Panel panel = (Panel)sender;
            int centerX = (int)(panel.Width * 0.5);
            int centerY = (int)(panel.Height * 0.5);
            int rDistance = GetRdistance(panel);
            foreach (var variable in panel.Controls)
            {
                if (variable is RoundButton)
                {
                    RoundButton led = (RoundButton)variable;
                    string name = led.Name;
                    if (name != "")
                    {
                        SetLedlocation(led, centerX, centerY, rDistance);
                    }
                }

                if (variable is Label)
                {
                    
                    Label label = (Label)variable;
                    if (label.Text.StartsWith("P"))
                    {
                        SetLabellocation(label, centerX, centerY, rDistance);
                    }
                    else
                    {
                        //label.Location = new Point(centerX - 5, centerY - 25);
                    }
                    
                   
                }
            }

        }

        private void SetLedlocation(RoundButton led, int centerX, int centerY, int rDistance)
        {
            string name = led.Name;
            int num = int.Parse(name.Substring(name.Length - 1, 1));
            double angel = 2.0 / _bloclNum * Math.PI * (num + 1) - Math.PI * 1.2;
            led.Location = new Point(centerX + (int)(Math.Cos(angel) * rDistance) - 15, centerY + (int)(Math.Sin(angel) * rDistance) - 15);
        }

        private void SetLabellocation(Label label, int centerX, int centerY, int rDistance)
        {
            string text = label.Text;
            int num = int.Parse(text.Substring(text.Length - 1, 1));
            double angel = 2.0 / _bloclNum * Math.PI * (num + 1) - Math.PI * 1.2;
            label.Text = "P" +( num+1);
            label.Location = new Point(centerX + (int)(Math.Cos(angel) * rDistance) - 10, centerY + (int)(Math.Sin(angel) * rDistance) - 25);
        }

        private int GetRdistance(Panel panel)
        {
            int centerX = (int)(panel.Width * 0.5);
            int centerY = (int)(panel.Height * 0.5);
            int rDistance = centerY >= centerX ? centerX : centerY;
            return (int)(rDistance * 0.72);
        }

        private void btnRefreshVisa_Click(object sender, EventArgs e)
        {
            cmbSerialVisa_update();
        }
        private void cmbSerialVisa_update()
        {
            string[] resources = SwitchUtil.GetResource();
            cmbSerialVisa.Items.Clear();
            foreach (string s in resources)
            {
                cmbSerialVisa.Items.Add(s);
            }
        }

        private void btnDirect_Click(object sender, EventArgs e)
        {
            string[,] TABLE = Util.GetDirectConfig(int.Parse(cmbPair.SelectedItem.ToString()), (MapType)cmbMapType.SelectedIndex, _bloclNum);

            dgvBasic.Rows.Clear();
            for (int m = 0; m < TABLE.GetLength(0); m++)
            {
                dgvBasic.Rows.Add();
                for (int n = 0; n < TABLE.GetLength(1); n++)
                {
                    dgvBasic.Rows[m].Cells[n].Value = TABLE[m, n].ToString();
                    dgvBasic.Rows[m].DefaultCellStyle.BackColor=Color.Aquamarine;
                }
            }
        }

    

        private void dgvBasic_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentBackground);

            e.Handled = true;
        }

        private void dgvBasic_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            
            if (e.RowIndex % 2 == 1)
            {
                return;
            }
            var rowIdx = ((e.RowIndex + 1)/2+1).ToString();
            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            string[,] current = Util.ToStringArray(dgvBasic, false);
            if (chkNext.Checked)
                GetNext(current);

            if (chkFext.Checked)
                GetFext(current);
        }

        private void GetNext(string[,] current)
        {
            string pair = cmbPair.SelectedItem.ToString();
            string[][] next = Util.GetExt(current, pair, Ext.Next);
            Display(next,Color.Bisque);
        }

        private void GetFext(string[,] current)
        {
            string pair = cmbPair.SelectedItem.ToString();
            if (pair == "2")
            {
                Ui.MessageBoxMuti("2 pair has no fext");
                return;
            }
            string[][] next = Util.GetExt(current, pair, Ext.Fext);
            Display(next, Color.DarkSeaGreen);
        }

        private void Display(string[][]source,Color color)
        {
            int row = dgvBasic.RowCount;
            for (int m = 0; m < source.Length; m++)
            {
                dgvBasic.Rows.Add();
                for (int n = 0; n < source[m].Length; n++)
                {
                    dgvBasic.Rows[m + row-1].Cells[n].Value = source[m][n].ToString();
                    dgvBasic.Rows[m + row-1].DefaultCellStyle.BackColor = color;
                }
            }
        }

        private void dgvBasic_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i % 2 != 0)
            {
                for (int j = 0; j < 4; j++)
                {
                    string switchLabel = dgvBasic.Rows[i].Cells[j].Value.ToString();
                    int switchkey = (int.Parse(switchLabel.Substring(1))) - 10;
                    callOnClick(leds[switchkey]);

                }
            }
            else
            {

            }
        }

        private void callOnClick(RoundButton led)
        {
            //建立一个类型  
            Type t = typeof(RoundButton);
            //参数对象  
            object[] p = new object[1];
            //产生方法  
            MethodInfo m = t.GetMethod("OnClick", BindingFlags.NonPublic | BindingFlags.Instance);
            //参数赋值。传入函数  
            p[0] = EventArgs.Empty;
            //调用  
            m.Invoke(led, p);
            return;
        }

        private void btnRowEdit_Click(object sender, EventArgs e)
        {
            if (dgvBasic.SelectedRows.Count == 1)
            {
                var currentRow = dgvBasic.SelectedRows[0];
                if (currentRow.Index % 2 == 0)
                {
                    Ui.MessageBoxMuti("必须选中索引行"); 
                }
                else
                {
                    for (int i = 0; i < currentRow.Cells.Count; i++)
                    {
                        currentRow.Cells[i].Value = _clickedLeds[i];
                    }
                }
                
            }
            else
            {
               Ui.MessageBoxMuti("必须选中一行"); 
            }
            
        }

        private void btnExeSwitch_Click(object sender, EventArgs e)
        {
            if (iSwitch == null)
            {
                Ui.MessageBoxMuti("请选择开关盒子的地址");
                cmbSerialVisa.Focus();
                return;
            }
            List<byte> temp = new List<byte>();
            foreach (var VARIABLE in leds)
            {
                if (VARIABLE.Value.Value)
                {
                    temp.Add((byte)(VARIABLE.Key + 1));
                }
            }

            string msg = "";
            if (!iSwitch.Open(temp.ToArray(), ref msg))
            {
               
                MessageBox.Show(msg);
            }
            else
            {
                MessageBox.Show("成功");
            }
        }

        private void cmbSerialVisa_SelectedIndexChanged(object sender, EventArgs e)
        {
            iSwitch = new SwitchMcu(cmbSerialVisa.SelectedItem.ToString());
        }

        private void btn_RightExe_Click(object sender, EventArgs e)
        {
            btnExeSwitch.PerformClick();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sFd = new SaveFileDialog();
            sFd.AddExtension = true;
            sFd.Filter = "Switch File|*.sw";
            string saveFilename = "";
           // sFd.ShowDialog();
            if (DialogResult.OK == sFd.ShowDialog())
            {
                //将选择的文件的全路径赋值给文本框
                saveFilename = sFd.FileName;
                StringBuilder sb = new StringBuilder();
                foreach (DataGridViewRow dr in dgvBasic.Rows)
                {
                    string temp = string.Empty;
                    for (int i = 0; i < dgvBasic.ColumnCount; i++)
                    {
                        temp += dr.Cells[i].Value + "\t";
                    }

                    if (temp.Trim() != "")
                    {
                        sb.AppendLine(temp);
                    }
                   
                }
                File.WriteAllText(saveFilename, sb.ToString());
                Ui.MessageBoxMuti("保存成功");
            }


            
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFd=new OpenFileDialog();
            oFd.Filter = "Switch File|*.sw";
            string openFilename = "";
            // sFd.ShowDialog();
            if (DialogResult.OK == oFd.ShowDialog())
            {
                //将选择的文件的全路径赋值给文本框
                openFilename = oFd.FileName;
                dgvBasic.Rows.Clear();
                string[][]lines=File.ReadAllLines(openFilename).Select(t=>t.Split(new []{'\t'},StringSplitOptions.RemoveEmptyEntries)).ToArray();

                Display(lines, Color.DarkSeaGreen);
               
               // File.WriteAllText(saveFilename, sb.ToString());
                Ui.MessageBoxMuti("导入成功");
            }
        }

        private void cmbSerial_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SerialPort sp=new SerialPort();
            //sp.PortName = cmbSerial.SelectedItem.ToString();
            //iSwitch = new SwitchMcu(sp);
        }

        //private void SerialUpdate()
        //{
        //    string[] str = SerialPort.GetPortNames();
        //    if (str == null)
        //    {
        //        MessageBox.Show("本机没有串口！", "Error");
        //        return;
        //    }
        //    if (str.Length == 0)
        //    {
        //        MessageBox.Show("本机没有串口！", "Error");
        //        return;
        //    }
        //    Array.Sort(str);
        //    //添加串口项目  
        //    foreach (string s in str)
        //    {//获取有多少个COM口  
        //        cmbSerial.Items.Add(s);
        //    }

        //    //串口设置默认选择项  
        //    cmbSerial.SelectedIndex = 0;   
        //}
     
    }
}
