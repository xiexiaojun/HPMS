using System.Windows.Forms;
using HPMS.Draw;

namespace HPMS
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip_Main = new System.Windows.Forms.MenuStrip();
            this.模式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Auto = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Manual = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_Set_hardware = new System.Windows.Forms.ToolStripMenuItem();
            this.m_Set_profile = new System.Windows.Forms.ToolStripMenuItem();
            this.m_Set_user = new System.Windows.Forms.ToolStripMenuItem();
            this.语言ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.中文ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Skin_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.office2016ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metroOffice2013ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.office2010ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.office2010BlackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visualStudio2010ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windows7ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.office2007BlueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.office2007BlackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.office2007SilverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vistaGlassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visualStudio2012LightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.自定义ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sNP转换ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eEPROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cOM计算ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchMatrixBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助文档ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtSN = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.chkList_TestItem = new System.Windows.Forms.CheckedListBox();
            this.tab_pair = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkList_LossPair = new System.Windows.Forms.CheckedListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chkList_NextPair = new System.Windows.Forms.CheckedListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.chkList_FextPair = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_PN = new System.Windows.Forms.TextBox();
            this.panelEx7 = new DevComponents.DotNetBar.PanelEx();
            this.chkStop = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btnTest = new DevComponents.DotNetBar.ButtonX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx6 = new DevComponents.DotNetBar.PanelEx();
            this.tabControlChart = new DevComponents.DotNetBar.TabControl();
            this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
            this.lsvKeyPoint = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.rTextStatus = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.labelResult = new DevComponents.DotNetBar.LabelX();
            this.pgbTest = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.panelEx5 = new DevComponents.DotNetBar.PanelEx();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblCaption = new DevComponents.DotNetBar.LabelX();
            this.picLogoLeft = new System.Windows.Forms.PictureBox();
            this.menuStrip_Main.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tab_pair.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panelEx7.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.panelEx6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlChart)).BeginInit();
            this.expandablePanel1.SuspendLayout();
            this.panelEx3.SuspendLayout();
            this.panelEx4.SuspendLayout();
            this.panelEx5.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogoLeft)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip_Main
            // 
            this.menuStrip_Main.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.模式ToolStripMenuItem,
            this.设置ToolStripMenuItem,
            this.语言ToolStripMenuItem,
            this.Skin_ToolStripMenuItem,
            this.工具ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            resources.ApplyResources(this.menuStrip_Main, "menuStrip_Main");
            this.menuStrip_Main.Name = "menuStrip_Main";
            // 
            // 模式ToolStripMenuItem
            // 
            this.模式ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Auto,
            this.ToolStripMenuItem_Manual,
            this.toolStripMenuItem4});
            resources.ApplyResources(this.模式ToolStripMenuItem, "模式ToolStripMenuItem");
            this.模式ToolStripMenuItem.Name = "模式ToolStripMenuItem";
            // 
            // ToolStripMenuItem_Auto
            // 
            this.ToolStripMenuItem_Auto.Checked = true;
            this.ToolStripMenuItem_Auto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToolStripMenuItem_Auto.Name = "ToolStripMenuItem_Auto";
            resources.ApplyResources(this.ToolStripMenuItem_Auto, "ToolStripMenuItem_Auto");
            // 
            // ToolStripMenuItem_Manual
            // 
            resources.ApplyResources(this.ToolStripMenuItem_Manual, "ToolStripMenuItem_Manual");
            this.ToolStripMenuItem_Manual.Name = "ToolStripMenuItem_Manual";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_Set_hardware,
            this.m_Set_profile,
            this.m_Set_user});
            resources.ApplyResources(this.设置ToolStripMenuItem, "设置ToolStripMenuItem");
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            // 
            // m_Set_hardware
            // 
            this.m_Set_hardware.Name = "m_Set_hardware";
            resources.ApplyResources(this.m_Set_hardware, "m_Set_hardware");
            this.m_Set_hardware.Click += new System.EventHandler(this.m_Set_hardware_Click);
            // 
            // m_Set_profile
            // 
            this.m_Set_profile.Name = "m_Set_profile";
            resources.ApplyResources(this.m_Set_profile, "m_Set_profile");
            this.m_Set_profile.Click += new System.EventHandler(this.m_Set_profile_Click);
            // 
            // m_Set_user
            // 
            this.m_Set_user.Name = "m_Set_user";
            resources.ApplyResources(this.m_Set_user, "m_Set_user");
            this.m_Set_user.Click += new System.EventHandler(this.m_Set_user_Click);
            // 
            // 语言ToolStripMenuItem
            // 
            this.语言ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.中文ToolStripMenuItem,
            this.englishToolStripMenuItem});
            resources.ApplyResources(this.语言ToolStripMenuItem, "语言ToolStripMenuItem");
            this.语言ToolStripMenuItem.Name = "语言ToolStripMenuItem";
            // 
            // 中文ToolStripMenuItem
            // 
            this.中文ToolStripMenuItem.Name = "中文ToolStripMenuItem";
            resources.ApplyResources(this.中文ToolStripMenuItem, "中文ToolStripMenuItem");
            this.中文ToolStripMenuItem.Click += new System.EventHandler(this.LanguangeMenuItem_Click);
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            resources.ApplyResources(this.englishToolStripMenuItem, "englishToolStripMenuItem");
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.LanguangeMenuItem_Click);
            // 
            // Skin_ToolStripMenuItem
            // 
            this.Skin_ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.office2016ToolStripMenuItem,
            this.metroOffice2013ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.office2010ToolStripMenuItem,
            this.office2010BlackToolStripMenuItem,
            this.visualStudio2010ToolStripMenuItem,
            this.windows7ToolStripMenuItem,
            this.office2007BlueToolStripMenuItem,
            this.office2007BlackToolStripMenuItem,
            this.office2007SilverToolStripMenuItem,
            this.vistaGlassToolStripMenuItem,
            this.visualStudio2012LightToolStripMenuItem,
            this.toolStripMenuItem2,
            this.自定义ToolStripMenuItem});
            resources.ApplyResources(this.Skin_ToolStripMenuItem, "Skin_ToolStripMenuItem");
            this.Skin_ToolStripMenuItem.Name = "Skin_ToolStripMenuItem";
            // 
            // office2016ToolStripMenuItem
            // 
            this.office2016ToolStripMenuItem.CheckOnClick = true;
            this.office2016ToolStripMenuItem.Name = "office2016ToolStripMenuItem";
            resources.ApplyResources(this.office2016ToolStripMenuItem, "office2016ToolStripMenuItem");
            this.office2016ToolStripMenuItem.Tag = "Office2016";
            this.office2016ToolStripMenuItem.Click += new System.EventHandler(this.menuStyle_Click);
            // 
            // metroOffice2013ToolStripMenuItem
            // 
            this.metroOffice2013ToolStripMenuItem.Name = "metroOffice2013ToolStripMenuItem";
            resources.ApplyResources(this.metroOffice2013ToolStripMenuItem, "metroOffice2013ToolStripMenuItem");
            this.metroOffice2013ToolStripMenuItem.Tag = "Metro";
            this.metroOffice2013ToolStripMenuItem.Click += new System.EventHandler(this.menuStyle_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Tag = "Office2010Blue";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.menuStyle_Click);
            // 
            // office2010ToolStripMenuItem
            // 
            this.office2010ToolStripMenuItem.Name = "office2010ToolStripMenuItem";
            resources.ApplyResources(this.office2010ToolStripMenuItem, "office2010ToolStripMenuItem");
            this.office2010ToolStripMenuItem.Tag = "Office2010Silver";
            this.office2010ToolStripMenuItem.Click += new System.EventHandler(this.menuStyle_Click);
            // 
            // office2010BlackToolStripMenuItem
            // 
            this.office2010BlackToolStripMenuItem.Name = "office2010BlackToolStripMenuItem";
            resources.ApplyResources(this.office2010BlackToolStripMenuItem, "office2010BlackToolStripMenuItem");
            this.office2010BlackToolStripMenuItem.Tag = "Office2010Black";
            this.office2010BlackToolStripMenuItem.Click += new System.EventHandler(this.menuStyle_Click);
            // 
            // visualStudio2010ToolStripMenuItem
            // 
            this.visualStudio2010ToolStripMenuItem.Name = "visualStudio2010ToolStripMenuItem";
            resources.ApplyResources(this.visualStudio2010ToolStripMenuItem, "visualStudio2010ToolStripMenuItem");
            this.visualStudio2010ToolStripMenuItem.Tag = "VisualStudio2010Blue";
            this.visualStudio2010ToolStripMenuItem.Click += new System.EventHandler(this.menuStyle_Click);
            // 
            // windows7ToolStripMenuItem
            // 
            this.windows7ToolStripMenuItem.Name = "windows7ToolStripMenuItem";
            resources.ApplyResources(this.windows7ToolStripMenuItem, "windows7ToolStripMenuItem");
            this.windows7ToolStripMenuItem.Tag = "Windows7Blue";
            this.windows7ToolStripMenuItem.Click += new System.EventHandler(this.menuStyle_Click);
            // 
            // office2007BlueToolStripMenuItem
            // 
            this.office2007BlueToolStripMenuItem.Name = "office2007BlueToolStripMenuItem";
            resources.ApplyResources(this.office2007BlueToolStripMenuItem, "office2007BlueToolStripMenuItem");
            this.office2007BlueToolStripMenuItem.Tag = "Office2007Blue";
            this.office2007BlueToolStripMenuItem.Click += new System.EventHandler(this.menuStyle_Click);
            // 
            // office2007BlackToolStripMenuItem
            // 
            this.office2007BlackToolStripMenuItem.Name = "office2007BlackToolStripMenuItem";
            resources.ApplyResources(this.office2007BlackToolStripMenuItem, "office2007BlackToolStripMenuItem");
            this.office2007BlackToolStripMenuItem.Tag = "Office2007Black";
            this.office2007BlackToolStripMenuItem.Click += new System.EventHandler(this.menuStyle_Click);
            // 
            // office2007SilverToolStripMenuItem
            // 
            this.office2007SilverToolStripMenuItem.Name = "office2007SilverToolStripMenuItem";
            resources.ApplyResources(this.office2007SilverToolStripMenuItem, "office2007SilverToolStripMenuItem");
            this.office2007SilverToolStripMenuItem.Tag = "Office2007Silver";
            this.office2007SilverToolStripMenuItem.Click += new System.EventHandler(this.menuStyle_Click);
            // 
            // vistaGlassToolStripMenuItem
            // 
            this.vistaGlassToolStripMenuItem.Name = "vistaGlassToolStripMenuItem";
            resources.ApplyResources(this.vistaGlassToolStripMenuItem, "vistaGlassToolStripMenuItem");
            this.vistaGlassToolStripMenuItem.Tag = "Office2007VistaGlass";
            this.vistaGlassToolStripMenuItem.Click += new System.EventHandler(this.menuStyle_Click);
            // 
            // visualStudio2012LightToolStripMenuItem
            // 
            this.visualStudio2012LightToolStripMenuItem.Name = "visualStudio2012LightToolStripMenuItem";
            resources.ApplyResources(this.visualStudio2012LightToolStripMenuItem, "visualStudio2012LightToolStripMenuItem");
            this.visualStudio2012LightToolStripMenuItem.Tag = "VisualStudio2012Light";
            this.visualStudio2012LightToolStripMenuItem.Click += new System.EventHandler(this.menuStyle_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // 自定义ToolStripMenuItem
            // 
            this.自定义ToolStripMenuItem.Name = "自定义ToolStripMenuItem";
            resources.ApplyResources(this.自定义ToolStripMenuItem, "自定义ToolStripMenuItem");
            this.自定义ToolStripMenuItem.Tag = "customerColor";
            this.自定义ToolStripMenuItem.Click += new System.EventHandler(this.menuStyle_Click);
            // 
            // 工具ToolStripMenuItem
            // 
            this.工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sNP转换ToolStripMenuItem,
            this.eEPROMToolStripMenuItem,
            this.cOM计算ToolStripMenuItem,
            this.switchMatrixBoxToolStripMenuItem});
            resources.ApplyResources(this.工具ToolStripMenuItem, "工具ToolStripMenuItem");
            this.工具ToolStripMenuItem.Name = "工具ToolStripMenuItem";
            // 
            // sNP转换ToolStripMenuItem
            // 
            resources.ApplyResources(this.sNP转换ToolStripMenuItem, "sNP转换ToolStripMenuItem");
            this.sNP转换ToolStripMenuItem.Name = "sNP转换ToolStripMenuItem";
            // 
            // eEPROMToolStripMenuItem
            // 
            resources.ApplyResources(this.eEPROMToolStripMenuItem, "eEPROMToolStripMenuItem");
            this.eEPROMToolStripMenuItem.Name = "eEPROMToolStripMenuItem";
            this.eEPROMToolStripMenuItem.Click += new System.EventHandler(this.eEPROMToolStripMenuItem_Click);
            // 
            // cOM计算ToolStripMenuItem
            // 
            resources.ApplyResources(this.cOM计算ToolStripMenuItem, "cOM计算ToolStripMenuItem");
            this.cOM计算ToolStripMenuItem.Name = "cOM计算ToolStripMenuItem";
            // 
            // switchMatrixBoxToolStripMenuItem
            // 
            this.switchMatrixBoxToolStripMenuItem.Name = "switchMatrixBoxToolStripMenuItem";
            resources.ApplyResources(this.switchMatrixBoxToolStripMenuItem, "switchMatrixBoxToolStripMenuItem");
            this.switchMatrixBoxToolStripMenuItem.Click += new System.EventHandler(this.switchMatrixBoxToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.帮助文档ToolStripMenuItem,
            this.关于ToolStripMenuItem});
            resources.ApplyResources(this.帮助ToolStripMenuItem, "帮助ToolStripMenuItem");
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            // 
            // 帮助文档ToolStripMenuItem
            // 
            this.帮助文档ToolStripMenuItem.Name = "帮助文档ToolStripMenuItem";
            resources.ApplyResources(this.帮助文档ToolStripMenuItem, "帮助文档ToolStripMenuItem");
            this.帮助文档ToolStripMenuItem.Click += new System.EventHandler(this.帮助文档ToolStripMenuItem_Click);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            resources.ApplyResources(this.关于ToolStripMenuItem, "关于ToolStripMenuItem");
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelUser,
            this.toolStripStatusMsg,
            this.toolStripStatusLabelDate});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabelUser
            // 
            this.toolStripStatusLabelUser.Name = "toolStripStatusLabelUser";
            resources.ApplyResources(this.toolStripStatusLabelUser, "toolStripStatusLabelUser");
            // 
            // toolStripStatusMsg
            // 
            this.toolStripStatusMsg.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.toolStripStatusMsg.Name = "toolStripStatusMsg";
            resources.ApplyResources(this.toolStripStatusMsg, "toolStripStatusMsg");
            this.toolStripStatusMsg.Spring = true;
            // 
            // toolStripStatusLabelDate
            // 
            this.toolStripStatusLabelDate.Name = "toolStripStatusLabelDate";
            resources.ApplyResources(this.toolStripStatusLabelDate, "toolStripStatusLabelDate");
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2007Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(163)))), ((int)(((byte)(26))))));
            // 
            // notifyIcon1
            // 
            resources.ApplyResources(this.notifyIcon1, "notifyIcon1");
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.panelEx1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelEx2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelEx3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panelEx5, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.tableLayoutPanel2);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            resources.ApplyResources(this.panelEx1, "panelEx1");
            this.panelEx1.Name = "panelEx1";
            this.tableLayoutPanel1.SetRowSpan(this.panelEx1, 2);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.labelX2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelX3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.txtSN, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.chkList_TestItem, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.tab_pair, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.txt_PN, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.panelEx7, 0, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            resources.ApplyResources(this.labelX2, "labelX2");
            this.labelX2.Name = "labelX2";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            resources.ApplyResources(this.labelX3, "labelX3");
            this.labelX3.Name = "labelX3";
            // 
            // txtSN
            // 
            // 
            // 
            // 
            this.txtSN.Border.Class = "TextBoxBorder";
            this.txtSN.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            resources.ApplyResources(this.txtSN, "txtSN");
            this.txtSN.Name = "txtSN";
            this.txtSN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSN_KeyDown);
            // 
            // chkList_TestItem
            // 
            resources.ApplyResources(this.chkList_TestItem, "chkList_TestItem");
            this.chkList_TestItem.FormattingEnabled = true;
            this.chkList_TestItem.Name = "chkList_TestItem";
            // 
            // tab_pair
            // 
            this.tab_pair.Controls.Add(this.tabPage1);
            this.tab_pair.Controls.Add(this.tabPage2);
            this.tab_pair.Controls.Add(this.tabPage3);
            resources.ApplyResources(this.tab_pair, "tab_pair");
            this.tab_pair.Name = "tab_pair";
            this.tab_pair.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chkList_LossPair);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chkList_LossPair
            // 
            resources.ApplyResources(this.chkList_LossPair, "chkList_LossPair");
            this.chkList_LossPair.FormattingEnabled = true;
            this.chkList_LossPair.Name = "chkList_LossPair";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chkList_NextPair);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chkList_NextPair
            // 
            resources.ApplyResources(this.chkList_NextPair, "chkList_NextPair");
            this.chkList_NextPair.FormattingEnabled = true;
            this.chkList_NextPair.Name = "chkList_NextPair";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.chkList_FextPair);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // chkList_FextPair
            // 
            resources.ApplyResources(this.chkList_FextPair, "chkList_FextPair");
            this.chkList_FextPair.FormattingEnabled = true;
            this.chkList_FextPair.Name = "chkList_FextPair";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txt_PN
            // 
            resources.ApplyResources(this.txt_PN, "txt_PN");
            this.txt_PN.Name = "txt_PN";
            this.txt_PN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_PN_KeyDown);
            // 
            // panelEx7
            // 
            this.panelEx7.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx7.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx7.Controls.Add(this.chkStop);
            this.panelEx7.Controls.Add(this.btnTest);
            this.panelEx7.DisabledBackColor = System.Drawing.Color.Empty;
            resources.ApplyResources(this.panelEx7, "panelEx7");
            this.panelEx7.Name = "panelEx7";
            this.panelEx7.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx7.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx7.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx7.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx7.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx7.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx7.Style.GradientAngle = 90;
            // 
            // chkStop
            // 
            // 
            // 
            // 
            this.chkStop.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            resources.ApplyResources(this.chkStop, "chkStop");
            this.chkStop.Name = "chkStop";
            this.chkStop.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            // 
            // btnTest
            // 
            this.btnTest.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTest.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            resources.ApplyResources(this.btnTest, "btnTest");
            this.btnTest.Name = "btnTest";
            this.btnTest.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.panelEx6);
            this.panelEx2.Controls.Add(this.expandablePanel1);
            this.panelEx2.DisabledBackColor = System.Drawing.Color.Empty;
            resources.ApplyResources(this.panelEx2, "panelEx2");
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            // 
            // panelEx6
            // 
            this.panelEx6.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx6.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx6.Controls.Add(this.tabControlChart);
            this.panelEx6.DisabledBackColor = System.Drawing.Color.Empty;
            resources.ApplyResources(this.panelEx6, "panelEx6");
            this.panelEx6.Name = "panelEx6";
            this.panelEx6.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx6.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx6.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx6.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx6.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx6.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx6.Style.GradientAngle = 90;
            // 
            // tabControlChart
            // 
            this.tabControlChart.BackColor = System.Drawing.Color.Transparent;
            this.tabControlChart.CanReorderTabs = true;
            this.tabControlChart.ColorScheme.TabBackground = System.Drawing.Color.Transparent;
            this.tabControlChart.ColorScheme.TabBorder = System.Drawing.Color.Transparent;
            this.tabControlChart.ColorScheme.TabItemBackground2 = System.Drawing.Color.Transparent;
            this.tabControlChart.ColorScheme.TabItemBackgroundColorBlend.AddRange(new DevComponents.DotNetBar.BackgroundColorBlend[] {
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.Empty, 0F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.Empty, 0.45F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.Empty, 0.45F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.Empty, 1F)});
            this.tabControlChart.ColorScheme.TabItemBorder = System.Drawing.Color.Transparent;
            this.tabControlChart.ColorScheme.TabItemHotBackgroundColorBlend.AddRange(new DevComponents.DotNetBar.BackgroundColorBlend[] {
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.Empty, 0F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.Empty, 0.45F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.Empty, 0.45F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.Empty, 1F)});
            this.tabControlChart.ColorScheme.TabItemSelectedBackground = System.Drawing.SystemColors.GradientActiveCaption;
            this.tabControlChart.ColorScheme.TabItemSelectedBackgroundColorBlend.AddRange(new DevComponents.DotNetBar.BackgroundColorBlend[] {
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.Empty, 0F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.Empty, 0.45F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.Empty, 0.45F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.Empty, 1F)});
            resources.ApplyResources(this.tabControlChart, "tabControlChart");
            this.tabControlChart.ForeColor = System.Drawing.Color.Black;
            this.tabControlChart.Name = "tabControlChart";
            this.tabControlChart.SelectedTabFont = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlChart.SelectedTabIndex = 0;
            this.tabControlChart.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            // 
            // expandablePanel1
            // 
            this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel1.CollapseDirection = DevComponents.DotNetBar.eCollapseDirection.LeftToRight;
            this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expandablePanel1.Controls.Add(this.lsvKeyPoint);
            this.expandablePanel1.DisabledBackColor = System.Drawing.Color.Empty;
            resources.ApplyResources(this.expandablePanel1, "expandablePanel1");
            this.expandablePanel1.ExpandButtonAlignment = DevComponents.DotNetBar.eTitleButtonAlignment.Left;
            this.expandablePanel1.Expanded = false;
            this.expandablePanel1.ExpandedBounds = new System.Drawing.Rectangle(764, 0, 323, 360);
            this.expandablePanel1.HideControlsWhenCollapsed = true;
            this.expandablePanel1.Name = "expandablePanel1";
            this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel1.Style.GradientAngle = 90;
            this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel1.TitleStyle.GradientAngle = 90;
            // 
            // lsvKeyPoint
            // 
            this.lsvKeyPoint.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            resources.ApplyResources(this.lsvKeyPoint, "lsvKeyPoint");
            this.lsvKeyPoint.Name = "lsvKeyPoint";
            this.lsvKeyPoint.UseCompatibleStateImageBehavior = false;
            this.lsvKeyPoint.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // columnHeader3
            // 
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            // 
            // columnHeader4
            // 
            resources.ApplyResources(this.columnHeader4, "columnHeader4");
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.panelEx4);
            this.panelEx3.DisabledBackColor = System.Drawing.Color.Empty;
            resources.ApplyResources(this.panelEx3, "panelEx3");
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.rTextStatus);
            this.panelEx4.Controls.Add(this.labelResult);
            this.panelEx4.Controls.Add(this.pgbTest);
            this.panelEx4.DisabledBackColor = System.Drawing.Color.Empty;
            resources.ApplyResources(this.panelEx4, "panelEx4");
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            // 
            // rTextStatus
            // 
            this.rTextStatus.BackColorRichTextBox = System.Drawing.Color.White;
            // 
            // 
            // 
            this.rTextStatus.BackgroundStyle.Class = "RichTextBoxBorder";
            this.rTextStatus.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            resources.ApplyResources(this.rTextStatus, "rTextStatus");
            this.rTextStatus.Name = "rTextStatus";
            this.rTextStatus.ReadOnly = true;
            this.rTextStatus.Rtf = "{\\rtf1\\ansi\\ansicpg936\\deff0\\deflang1033\\deflangfe2052{\\fonttbl{\\f0\\fnil\\fcharset" +
    "134 \\\'ce\\\'a2\\\'c8\\\'ed\\\'d1\\\'c5\\\'ba\\\'da;}}\r\n\\viewkind4\\uc1\\pard\\lang2052\\f0\\fs18\\pa" +
    "r\r\n}\r\n";
            this.rTextStatus.TextChanged += new System.EventHandler(this.rTextStatus_TextChanged);
            // 
            // labelResult
            // 
            // 
            // 
            // 
            this.labelResult.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            resources.ApplyResources(this.labelResult, "labelResult");
            this.labelResult.Name = "labelResult";
            this.labelResult.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // pgbTest
            // 
            // 
            // 
            // 
            this.pgbTest.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            resources.ApplyResources(this.pgbTest, "pgbTest");
            this.pgbTest.Maximum = 300;
            this.pgbTest.Name = "pgbTest";
            this.pgbTest.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            // 
            // panelEx5
            // 
            this.panelEx5.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.tableLayoutPanel1.SetColumnSpan(this.panelEx5, 2);
            this.panelEx5.Controls.Add(this.tableLayoutPanel3);
            this.panelEx5.DisabledBackColor = System.Drawing.Color.Empty;
            resources.ApplyResources(this.panelEx5, "panelEx5");
            this.panelEx5.Name = "panelEx5";
            this.panelEx5.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx5.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx5.Style.GradientAngle = 90;
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.lblCaption, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.picLogoLeft, 0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // lblCaption
            // 
            // 
            // 
            // 
            this.lblCaption.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            resources.ApplyResources(this.lblCaption, "lblCaption");
            this.lblCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(108)))), ((int)(((byte)(169)))));
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // picLogoLeft
            // 
            resources.ApplyResources(this.picLogoLeft, "picLogoLeft");
            this.picLogoLeft.Image = global::HPMS.Properties.Resources.微信图片_20181215221604;
            this.picLogoLeft.Name = "picLogoLeft";
            this.picLogoLeft.TabStop = false;
            // 
            // frmMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip_Main);
            this.MainMenuStrip = this.menuStrip_Main;
            this.Name = "frmMain";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.SizeChanged += new System.EventHandler(this.frmMain_SizeChanged);
            this.menuStrip_Main.ResumeLayout(false);
            this.menuStrip_Main.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tab_pair.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.panelEx7.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.panelEx6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabControlChart)).EndInit();
            this.expandablePanel1.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            this.panelEx4.ResumeLayout(false);
            this.panelEx5.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogoLeft)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip_Main;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripMenuItem m_Set_hardware;
        private System.Windows.Forms.ToolStripMenuItem m_Set_profile;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem m_Set_user;
        private ToolStripStatusLabel toolStripStatusLabelUser;
        private ToolStripStatusLabel toolStripStatusMsg;
        private ToolStripStatusLabel toolStripStatusLabelDate;
        private Timer timer1;
        private TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.PanelEx panelEx5;
        private ToolStripMenuItem Skin_ToolStripMenuItem;
        private ToolStripMenuItem office2016ToolStripMenuItem;
        private ToolStripMenuItem metroOffice2013ToolStripMenuItem;
        private ToolStripMenuItem office2010ToolStripMenuItem;
        private ToolStripMenuItem office2010BlackToolStripMenuItem;
        private ToolStripMenuItem visualStudio2010ToolStripMenuItem;
        private ToolStripMenuItem windows7ToolStripMenuItem;
        private ToolStripMenuItem office2007BlueToolStripMenuItem;
        private ToolStripMenuItem office2007BlackToolStripMenuItem;
        private ToolStripMenuItem office2007SilverToolStripMenuItem;
        private ToolStripMenuItem vistaGlassToolStripMenuItem;
        private ToolStripMenuItem visualStudio2012LightToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem 自定义ToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem 语言ToolStripMenuItem;
        private ToolStripMenuItem 中文ToolStripMenuItem;
        private ToolStripMenuItem englishToolStripMenuItem;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx rTextStatus;
        private DevComponents.DotNetBar.LabelX labelResult;
        private DevComponents.DotNetBar.Controls.ProgressBarX pgbTest;
        private ToolStripMenuItem 模式ToolStripMenuItem;
        private ToolStripMenuItem ToolStripMenuItem_Auto;
        private ToolStripMenuItem ToolStripMenuItem_Manual;
        private ToolStripSeparator toolStripMenuItem4;
        private ToolStripMenuItem 工具ToolStripMenuItem;
        private ToolStripMenuItem sNP转换ToolStripMenuItem;
        private ToolStripMenuItem eEPROMToolStripMenuItem;
        private ToolStripMenuItem cOM计算ToolStripMenuItem;
        private TableLayoutPanel tableLayoutPanel2;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSN;
        private CheckedListBox chkList_TestItem;
        private TabControl tab_pair;
        private TabPage tabPage1;
        private CheckedListBox chkList_LossPair;
        private TabPage tabPage2;
        private CheckedListBox chkList_NextPair;
        private TabPage tabPage3;
        private CheckedListBox chkList_FextPair;
        private Label label1;
        private TextBox txt_PN;
        private ToolStripMenuItem 帮助ToolStripMenuItem;
        private ToolStripMenuItem 关于ToolStripMenuItem;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.PanelEx panelEx6;
        private DevComponents.DotNetBar.TabControl tabControlChart;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
        private ListView lsvKeyPoint;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private TableLayoutPanel tableLayoutPanel3;
        private DevComponents.DotNetBar.LabelX lblCaption;
        private PictureBox picLogoLeft;
        private DevComponents.DotNetBar.PanelEx panelEx7;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkStop;
        private DevComponents.DotNetBar.ButtonX btnTest;
        private ToolStripMenuItem switchMatrixBoxToolStripMenuItem;
        private ToolStripMenuItem 帮助文档ToolStripMenuItem;

    }
}

