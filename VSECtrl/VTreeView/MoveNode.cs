using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace System
{

    /// <summary>
    /// 上移 下移节点
    /// </summary>
    public class MoveNode
    {
        public MoveNode(TreeView tree)
        {
            treeView1 = tree;
        }

        //是否可以向下移动
        public bool CanMoveUp
        { 
            get{

                return treeView1.SelectedNode != null && treeView1.SelectedNode.Index != 0; 
            }
        }

        ////是否可以向上移动
        public bool CanMoveDown
        {
            get {
                return treeView1.SelectedNode.Level != 0 &&
                    (treeView1.SelectedNode.Index != treeView1.SelectedNode.Parent.Nodes.Count - 1);
            }
        }
      

        /// <summary>
        /// 节点向上移动
        /// </summary>
        public  void MoveUp()
        {
            if (treeView1.SelectedNode != null)
            {
                TreeNode treeNode = new TreeNode();
                if (treeView1.SelectedNode.Index != 0)
                {
                    if (treeView1.SelectedNode.Index != 0)
                    {
                        treeNode = (TreeNode)treeView1.SelectedNode.Clone();
                        if (treeView1.SelectedNode.Level == 0)
                        {
                            treeView1.Nodes.Insert(treeView1.SelectedNode.PrevNode.Index, treeNode);
                        }
                        else
                        {
                            if (treeView1.SelectedNode.Level != 0)
                            {
                                treeView1.SelectedNode.Parent.Nodes.Insert(treeView1.SelectedNode.PrevNode.Index, treeNode);
                            }
                        }
                        treeView1.SelectedNode.Remove();
                        treeView1.SelectedNode = treeNode;
                    }
                }
            }
        }

        /// <summary>
        /// 节点向下移动
        /// </summary>

        public void MoveDown()
        {
            if (treeView1.SelectedNode != null)
            {
                TreeNode treeNode = new TreeNode();
                if (treeView1.SelectedNode.Level == 0)
                {
                    if (treeView1.SelectedNode.Index != treeView1.Nodes.Count - 1)
                    {
                        treeNode = (TreeNode)treeView1.SelectedNode.Clone();
                        treeView1.Nodes.Insert(treeView1.SelectedNode.NextNode.Index + 1, treeNode);
                        treeView1.SelectedNode.Remove();
                        treeView1.SelectedNode = treeNode;
                    }
                }
                else
                {
                    if (treeView1.SelectedNode.Level != 0)
                    {
                        if (treeView1.SelectedNode.Index != treeView1.SelectedNode.Parent.Nodes.Count - 1)
                        {
                            treeNode = (TreeNode)treeView1.SelectedNode.Clone();
                            treeView1.SelectedNode.Parent.Nodes.Insert(treeView1.SelectedNode.NextNode.Index + 1, treeNode);
                            treeView1.SelectedNode.Remove();
                            treeView1.SelectedNode = treeNode;
                        }
                    }
                }
            }
        }

        public TreeView treeView1;
    }
}
