using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using HPMS.Languange;

namespace HPMS
{
    public partial class Form1 : Office2007Form
    {
        private readonly Dictionary<string, ToolStripMenuItem> styleItems = new Dictionary<string, ToolStripMenuItem>();

        public Form1()
        {
            EnableGlass = false;
            InitializeComponent();
            StyleMenuAdd();
        }


        #region Languange & Style

        private void LanguangeMenuItem_Click(object sender, EventArgs e)
        {
            var toolStripMenuItem = (ToolStripMenuItem) sender;
            中文ToolStripMenuItem.Checked = false;
            englishToolStripMenuItem.Checked = false;
            toolStripMenuItem.Checked = true;
            var languangeSourceFile = toolStripMenuItem.Text == "中文" ? "lang/中文.json" : "lang/english.json";
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
    }
}