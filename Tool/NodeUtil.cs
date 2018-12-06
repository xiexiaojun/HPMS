using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Tool
{
    public class NodeUtil
    {
        private TreeView treeView;

        public NodeUtil(TreeView treeView)
        {
            this.treeView = treeView;
        }
        public void MovUp(TreeNode ObjNode)
        {
            //----节点的向上移动
            if (ObjNode != null)
            {
                TreeNode newnode = new TreeNode();
                //--------如果选中节点为最顶节点
                if (ObjNode.Index == 0)
                {
                    //-------------
                }
                else if (ObjNode.Index != 0)
                {
                    newnode = (TreeNode)ObjNode.Clone();
                    //-------------若选中节点为根节点
                    if (ObjNode.Level == 0)
                    {
                        treeView.Nodes.Insert(ObjNode.PrevNode.Index, newnode);
                    }
                    //-------------若选中节点并非根节点
                    else if (ObjNode.Level != 0)
                    {
                        ObjNode.Parent.Nodes.Insert(ObjNode.PrevNode.Index, newnode);
                    }
                    ObjNode.Remove();
                    ObjNode = newnode;
                }

            }
        }

        public void MovDown(TreeNode ObjNode)
        {
            //----节点的向下移动
            if (ObjNode != null)
            {
                TreeNode newnode = new TreeNode();
                //-------------如果选中的是根节点
                if (ObjNode.Level == 0)
                {
                    //---------如果选中节点为最底节点
                    if (ObjNode.Index == treeView.Nodes.Count - 1)
                    {
                        //---------------
                    }
                    //---------如果选中的不是最底的节点
                    else
                    {
                        newnode = (TreeNode)ObjNode.Clone();
                        treeView.Nodes.Insert(ObjNode.NextNode.Index + 1, newnode);
                        ObjNode.Remove();
                        ObjNode = newnode;

                    }
                }
                //-------------如果选中节点不是根节点
                else if (ObjNode.Level != 0)
                {
                    //---------如果选中最底的节点
                    if (ObjNode.Index == ObjNode.Parent.Nodes.Count - 1)
                    {
                        //-----------
                    }
                    //---------如果选中的不是最低的节点
                    else
                    {
                        newnode = (TreeNode)ObjNode.Clone();
                        ObjNode.Parent.Nodes.Insert(ObjNode.NextNode.Index + 1, newnode);
                        ObjNode.Remove();
                        ObjNode = newnode;
                    }
                }

            }
        }

        public void MovLeft(TreeNode ObjNode)
        {
            //-----节点向左移动（即成为原父节点的同级节点）
            if (ObjNode != null)
            {
                TreeNode newnode = new TreeNode();
                //-----如果接点是根节点
                if (ObjNode.Parent == null)
                {
                    //---------
                }
                //-----如果选中节点不是根节点
                else
                {
                    newnode = (TreeNode)ObjNode.Clone();
                    //-----如果选中的节点是第一级子节点
                    if (ObjNode.Level == 1)
                    {
                        treeView.Nodes.Insert(ObjNode.Parent.Index + 1, newnode);
                    }
                    else
                    {
                        ObjNode.Parent.Parent.Nodes.Insert(ObjNode.Parent.Index + 1, newnode);
                    }
                    ObjNode.Remove();
                    ObjNode = newnode;

                }
            }

        }

        public void MovRight(TreeNode ObjNode)
        {
            //-----节点的向右移动（即成为上一个同级节点的最后一个子节点）
            if (ObjNode != null)
            {
                TreeNode newnode = new TreeNode();
                //-----如果节点是叶子节点
                if (ObjNode.Nodes.Count == 0 && ObjNode.PrevNode == null)
                {
                    //--------
                }
                //-----如果节点是第一个根节点
                else if (ObjNode.Parent == null && ObjNode.Index == 0)
                {
                    //--------
                }
                //-----
                else
                {
                    newnode = (TreeNode)ObjNode.Clone();
                    ObjNode.PrevNode.Nodes.Insert(ObjNode.PrevNode.Nodes.Count, newnode);
                    ObjNode.Remove();
                    ObjNode = newnode;
                }
            }
        }

        #region Save (saveTree, saveNode)
        /// <summary>
        /// Save the TreeView content
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>Errorcode as int</returns>
        public  int SaveTree(string filename)
        {
            // Neues Array anlegen
            ArrayList al = new ArrayList();
            foreach (TreeNode tn in treeView.Nodes)
            {
                // jede RootNode im TreeView sichern ...
                al.Add(tn);
            }

            // Datei anlegen
            Stream file = File.Open(filename, FileMode.Create);
            // Bin鋜-Formatierer init.
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                // Serialisieren des Arrays
                bf.Serialize(file, al);
            }
            catch (System.Runtime.Serialization.SerializationException e)
            {
                MessageBox.Show("Serialization failed : {0}", e.Message);
                return -1; // ERROR
            }

            // Datei schliessen
            file.Close();

            return 0; // OKAY
        }


        public string SaveTreeS()
        {
            // Neues Array anlegen
            ArrayList al = new ArrayList();
            foreach (TreeNode tn in treeView.Nodes)
            {
                // jede RootNode im TreeView sichern ...
                al.Add(tn);
            }

            return JsonConvert.SerializeObject(al);
        }
        #endregion


        #region Load (loadTree, searchNode)
        /// <summary>
        /// Load the TreeView content
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>Errorcode as int</returns>
        public  int LoadTree(string filename)
        {
            if (File.Exists(filename))
            {
                // Datei 鰂fnen
                Stream file = File.Open(filename, FileMode.Open);
                // Bin鋜-Formatierer init.
                BinaryFormatter bf = new BinaryFormatter();
                // Object var. init.
                object obj = null;
                try
                {
                    // Daten aus der Datei deserialisieren
                    obj = bf.Deserialize(file);
                }
                catch (System.Runtime.Serialization.SerializationException e)
                {
                    MessageBox.Show("De-Serialization failed : {0}", e.Message);
                    return -1;
                }
                // Datei schliessen
                file.Close();

                // Neues Array erstellen
                ArrayList nodeList = obj as ArrayList;

                // load Root-Nodes
                foreach (TreeNode node in nodeList)
                {
                    treeView.Nodes.Add(node);
                }
                return 0;

            }
            else return -2; // File existiert nicht
        }

        public void LoadTreeS(string jsonObj)
        {

            ArrayList nodeList = JsonConvert.DeserializeObject<ArrayList>(jsonObj);
              
              
                // load Root-Nodes
                foreach (TreeNode node in nodeList)
                {
                    treeView.Nodes.Add(node);
                }
             
           
        }
        #endregion


        /// <summary>
        /// 系列节点 Checked 属性控制
        /// </summary>
        /// <param name="e"></param>
        public static void CheckControl(TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node != null && !Convert.IsDBNull(e.Node))
                {
                    CheckParentNode(e.Node);
                    if (e.Node.Nodes.Count > 0)
                    {
                        CheckAllChildNodes(e.Node, e.Node.Checked);
                    }
                }
            }
        }

        #region 私有方法

        //改变所有子节点的状态
        private static void CheckAllChildNodes(TreeNode pn, bool IsChecked)
        {
            foreach (TreeNode tn in pn.Nodes)
            {
                tn.Checked = IsChecked;

                if (tn.Nodes.Count > 0)
                {
                    CheckAllChildNodes(tn, IsChecked);
                }
            }
        }

        //改变父节点的选中状态，此处为所有子节点不选中时才取消父节点选中，可以根据需要修改
        private static void CheckParentNode(TreeNode curNode)
        {
            bool bChecked = false;

            if (curNode.Parent != null)
            {
                foreach (TreeNode node in curNode.Parent.Nodes)
                {
                    if (node.Checked)
                    {
                        bChecked = true;
                        break;
                    }
                }

                if (bChecked)
                {
                    curNode.Parent.Checked = true;
                    CheckParentNode(curNode.Parent);
                }
                else
                {
                    curNode.Parent.Checked = false;
                    CheckParentNode(curNode.Parent);
                }
            }
        }

        #endregion
    }


}
