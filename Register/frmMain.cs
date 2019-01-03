using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using HslCommunication.BasicFramework;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using Tool;

namespace Register
{
    public sealed partial class frmRegister : Office2007Muti
    {
        SoftAuthorize softAuthorize = new HslCommunication.BasicFramework.SoftAuthorize();
        private NodeUtil _nodeUtil ;
        private string _filePath = "config/funcNodes.xml";
        public frmRegister()
        {
            EnableGlass = false;
            InitializeComponent();
        }

     
        private void btnGetMachineCode_Click(object sender, EventArgs e)
        {
            //Tool.Register aa=new Tool.Register();
            //aa.SubKey = "SOFTWARE\\FigoAA\\";
            //aa.CreateSubKey("SOFTWARE\\FigoAA\\");
            //aa.WriteRegeditKey("value", 1);
            //aa.WriteRegeditKey("value", "Test");
            
            ////RegistryKey aimdir = Registry.LocalMachine.CreateSubKey("SOFTWARE\\TagReceiver\\Params\\SerialPort");
            ////aimdir.SetValue( "PortName", "Figo2018"); 
            ////  aimdir.Close();
            
            txtMachineCode.Text = softAuthorize.GetMachineCodeString();
        }

        private void btnCalcuCode_Click(object sender, EventArgs e)
        {
            if (txtKey.Text.Trim().Length != 8)
            {
                MessageBoxEx.Show("秘钥必须为8位字母或数字");
                return;
            }

            Dictionary<string,string>funcList=new Dictionary<string, string>();
            GetFuncs(treeView1.Nodes, funcList);
            var funcJson=new JObject();
            foreach (var variable in funcList)
            {
                funcJson.Add(variable.Key,variable.Value);
            }
       
            
            var licJObjectobj=new JObject
            {
                {"dateType",chkDateType.Checked},
                {"licDate",DateTime.Now},
                {"lastDate",DateTime.Now},
                {"expireDate",dateTimeExpire.Value}
            };

            var sourceJObjectobj = new JObject { 
                {"softName", cmbSoftName.SelectedItem.ToString() }, 
                {"lic",licJObjectobj},
                {"softVersion", funcJson},
                {"machineCode",txtMachineCode.Text } };
           
            txtCode.Text = SoftSecurity.MD5Encrypt(sourceJObjectobj.ToString(), txtKey.Text);

           
        }

        private void GetFuncs(TreeNodeCollection nodes,Dictionary<string,string>func)
        {
            foreach (TreeNode variable in nodes)
            {
                if (variable.Nodes.Count == 0)
                {
                    if (variable.Checked)
                    {
                        func.Add(variable.Tag.ToString(), variable.Text);
                    }
                  
                }
                else
                {
                    GetFuncs(variable.Nodes,func);
                }
            }
        }


        private void btnAddfoot_Click(object sender, EventArgs e)
        {
            TreeNode node = new TreeNode();
            node.Text = textBoxX1.Text;
            node.Tag = textBoxX2.Text;
            node.ToolTipText = textBoxX2.Text;
            treeView1.Nodes.Add(node);
            textBoxX1.Text = "";
            textBoxX2.Text = "";
        }

        private void btnAddchild_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            if (selectedNode == null)
            {
                Ui.MessageBoxMuti("没有选择节点");
                return;
            }
            TreeNode node = new TreeNode();
            node.Text = textBoxX1.Text;
            node.Tag = textBoxX2.Text;
            node.ToolTipText = textBoxX2.Text;
            selectedNode.Nodes.Add(node);
            textBoxX1.Text = "";
            textBoxX2.Text = "";
        }

        private void btnMoveup_Click(object sender, EventArgs e)
        {
            _nodeUtil.MovUp(treeView1.SelectedNode);

        }

        private void btnSavetree_Click(object sender, EventArgs e)
        {
            _nodeUtil.SaveTree(_filePath);
        }

        private void btnLoadtree_Click(object sender, EventArgs e)
        {
            _nodeUtil.LoadTree(_filePath);
        }

     

        private void btnDelnode_Click(object sender, EventArgs e)
        {
            TreeNode treeNode = treeView1.SelectedNode;
            if (treeNode == null)
            {
                return;
            }
            treeView1.Nodes.Remove(treeNode);
        }

        private void btnMovedown_Click(object sender, EventArgs e)
        {
            _nodeUtil.MovDown(treeView1.SelectedNode);
        }

        private void frmRegister_Load(object sender, EventArgs e)
        {
            cmbSoftName.SelectedIndex = 0;
            dateTimeExpire.Value=DateTime.Now;
            //cmbSoftVersion.SelectedIndex = 0;
            _nodeUtil = new NodeUtil(treeView1);
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            NodeUtil.CheckControl(e);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            Dictionary<string,string>temDictionary=new Dictionary<string, string>();
            GetFuncs(treeView1.Nodes,temDictionary);
        }

      
       
      

        

       
    }
}
