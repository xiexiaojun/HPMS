using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace Tool
{
    public class Office2007Muti : Office2007Form
    {
        public Office2007Muti()
        {
            //AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Office2007Muti));
            base.TextChanged += Office2007Muti_TextChanged;
            base.Shown += Office2007Muti_Shown;
            this.StartPosition = FormStartPosition.CenterParent;
           this.Icon = Properties.Resources.icon;
        }

        private void Office2007Muti_Shown(object sender, EventArgs e)
        {
            this.Text = LanguageHelper.GetLanguageText(this.Text);
            this.Font = LanguageHelper.GetFont();
        }

        private void Office2007Muti_TextChanged(object sender, EventArgs e)
        {
            LanguageHelper.SetControlLanguageText(this);
            base.ControlAdded += MyStyleFormBase_ControlAdded;
        }
        private void MyStyleFormBase_ControlAdded(object sender, ControlEventArgs e)
        {
            LanguageHelper.SetControlLanguageText(e.Control);
        }
        protected virtual void PerformChildrenChange(Control target)
        {
            LanguageHelper.SetControlLanguageText(target);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Office2007Muti
            // 
            this.ClientSize = new System.Drawing.Size(385, 278);
            this.DoubleBuffered = true;
            this.Name = "Office2007Muti";
            this.Load += new System.EventHandler(this.Office2007Muti_Load);
            this.ResumeLayout(false);

        }

        private void Office2007Muti_Load(object sender, EventArgs e)
        {

        }
        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            AssemblyName assemblyName = new AssemblyName(args.Name);
            return Assembly.LoadFrom("dll");
        }
    }
}
