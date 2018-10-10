using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using HPMS.Languange;
using HPMS.Log;
using HPMS.Splash;
using HPMS.Test;

namespace HPMS
{
    public partial class Form1 : Office2007Form
    {
        private readonly Dictionary<string, ToolStripMenuItem> styleItems = new Dictionary<string, ToolStripMenuItem>();

        public Form1()
        {
            EnableGlass = false;
            InitializeComponent();
            Splasher.Status = "正在展示相关的内容";
            System.Threading.Thread.Sleep(3000);
            System.Threading.Thread.Sleep(50);

            Splasher.Close();
            StyleMenuAdd();
        }


        #region Languange & Style

        private void LanguangeMenuItem_Click(object sender, EventArgs e)
        {
            var toolStripMenuItem = (ToolStripMenuItem) sender;
            中文ToolStripMenuItem.Checked = false;
            englishToolStripMenuItem.Checked = false;
            toolStripMenuItem.Checked = true;
            var languangeSourceFile = toolStripMenuItem.Text == "中文" ? "lang/中文.json" : "Resources/lang/english.json";
            LanguageHelper.SetResources(languangeSourceFile);
            foreach (Control VARIABLE in Controls) LanguageHelper.SetControlLanguageText(VARIABLE);
        }

        private void StyleMenuAdd()
        {
            foreach (eStyle suit in Enum.GetValues(typeof(eStyle)))
            {
                //添加菜单一 
                ToolStripMenuItem subItem;
                subItem = AddContextMenu(suit.ToString(), 风格ToolStripMenuItem.DropDownItems, StyleMenuClicked);
                styleItems.Add(suit.ToString(), subItem);
                // cmbSkin.Items.Add(suit.ToString());
            }

            ToolStripMenuItem subItemCustomer;
            subItemCustomer = AddContextMenu("-", 风格ToolStripMenuItem.DropDownItems, null);
            subItemCustomer = AddContextMenu("自定义", 风格ToolStripMenuItem.DropDownItems, StyleMenuClicked);
        }

        private ToolStripMenuItem AddContextMenu(string text, ToolStripItemCollection cms, EventHandler callback)
        {
            if (text == "-")
            {
                var tsp = new ToolStripSeparator();
                cms.Add(tsp);
                return null;
            }

            if (!string.IsNullOrEmpty(text))
            {
                var tsmi = new ToolStripMenuItem(text);
                tsmi.Tag = text + "TAG";
                if (callback != null) tsmi.Click += callback;
                cms.Add(tsmi);
                return tsmi;
            }

            return null;
        }

        private void StyleMenuClicked(object sender, EventArgs e)
        {
            var styleClickedItem = (ToolStripMenuItem) sender;
            var currentStyle = styleManager1.ManagerStyle;
            if (styleClickedItem.Tag.ToString() == "自定义TAG")
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    var color = colorDialog1.Color;
                    StyleManager.ChangeStyle(currentStyle, color);
                }
            }
            else
            {
                styleItems[currentStyle.ToString()].Checked = false;
                styleClickedItem.Checked = true;
                styleManager1.ManagerStyle = (eStyle) Enum.Parse(typeof(eStyle), styleClickedItem.Text);
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            Thread.CurrentPrincipal =
                new GenericPrincipal(new GenericIdentity("Administrator","Figo"),
                    new[] { "ADMIN","Add"});

            //var role = "Anyone";
            //var operation = new FileOperations();
            //// 可以正常调用Read
            //OperationInvoker.Invoke(operation, role, "Read", null);
            //// 但是不能调用Write
            //OperationInvoker.Invoke(operation, role, "Write", null);

            CalculatorHandler bb=new CalculatorHandler();
           double kk= bb.Add(3, 5);
            MessageBox.Show(kk.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10000; i++)
            {
                LogHelper.WriteLog("AAA");
                LogHelper.WriteLog("BBB", new Exception("Test")); 
            }
           
        }
    }
}