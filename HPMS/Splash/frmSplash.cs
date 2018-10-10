using System.Windows.Forms;

namespace HPMS.Splash
{
    public partial class frmSplash : Form,ISplashForm
    {
        public frmSplash()
        {
            InitializeComponent();
        }

        #region ISplashForm

        void ISplashForm.SetStatusInfo(string NewStatusInfo)
        {
            lbStatusInfo.Text = NewStatusInfo;
        }

        #endregion
    }
}