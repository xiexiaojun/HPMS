using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using Tool;

namespace HPMS
{
    public partial class frmVisaLists : Office2007Muti
    {
        private TextBoxX _textBoxX;
        public frmVisaLists(TextBoxX textBoxX)
        {
            EnableGlass = false;
            _textBoxX = textBoxX;
            InitializeComponent();
        }

        private void frmVisaLists_Load(object sender, EventArgs e)
        {
            
            foreach (string s in Equipment.Util.GetVisaList())
            {
                cmbVisaLists.Items.Add(s);
            }

            cmbVisaLists.SelectedIndex = 0;
        }

      

        private void frmVisaLists_Paint(object sender, PaintEventArgs e)
        {
           
            this.BackColor=Color.Bisque;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            _textBoxX.Text = cmbVisaLists.Text;
            //frmSetting f1 = (frmSetting)this.Owner;//将本窗体的拥有者强制设为Form1类的实例f1
            //f1.Controls["textBoxX1"].Text = cmbVisaLists.Text;
            this.Close();
        }
    }
}
