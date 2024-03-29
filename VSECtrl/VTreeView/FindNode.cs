


//TreeView查获节点并选中节点
//用此方法可以选中 TREEVIEW最后一次选中的节点 及其状态
namespace System
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using System.Windows.Forms;

    public class FindNode
    {
        public FindNode(TreeView tree)
        {
            treeView1 = tree;
        }


        public TreeView treeView1;

        private List<TreeNode> CurrentNodeMatches = new List<TreeNode>();
        private int LastNodeIndex = 0;
        private string LastSearchText;

        private void SearchNodes(string SearchText, TreeNode node)
        {
            while (node != null)
            {
                if (node.FullPath.ToLower() == SearchText.ToLower())
                {
                    CurrentNodeMatches.Add(node);
                }

                if (node.Nodes.Count != 0)
                {
                    SearchNodes(SearchText, node.Nodes[0]);//Recursive Search 
                }
                node = node.NextNode;
            }
        }

        /// <summary>
        /// TreeNode.FullPath
        /// </summary>
        /// <param name="searchText">TreeNode.FullPath</param>
        public bool SelectByNodeFullPath(string searchText)
        {
            bool result = false;
            if (searchText == "")
                result =false;

            if (LastSearchText != searchText)
            {
                CurrentNodeMatches.Clear();
                LastSearchText = searchText;
                LastNodeIndex = 0;
                SearchNodes(searchText, treeView1.Nodes[0]);
            }

            if (LastNodeIndex >= 0 && CurrentNodeMatches.Count > 0 && LastNodeIndex < CurrentNodeMatches.Count)
            {
                TreeNode selectedNode = CurrentNodeMatches[LastNodeIndex];
                LastNodeIndex++;
                this.treeView1.SelectedNode = selectedNode;
                this.treeView1.Select();
                result = true;
            }
            else
                result = false;

            return result;

        }
    }
}
