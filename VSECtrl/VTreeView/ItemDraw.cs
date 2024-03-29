using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Drawing; 
 

namespace System
{

    /// <summary>
    /// 节点重绘 美化TREE
    /// </summary>
    public class ItemDraw
    {
        public ItemDraw(TreeView tree)
        {
            treeView1 = tree;
        }


 
        private void treeView1_MouseMove(object sender, MouseEventArgs e)
        {
            //改变光标成小手
            //光标在节点的图标上 或者文本上
            TreeViewHitTestInfo info = treeView1.HitTest(e.Location);
            if (info.Location == TreeViewHitTestLocations.Image 
                || info.Location == TreeViewHitTestLocations.Label)
                treeView1.Cursor = Win32API.Hand;
            else
                treeView1.Cursor = Cursors.Arrow;
        }


        private TreeView treeView1;
 
    }
}
