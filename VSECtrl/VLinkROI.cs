using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VControls
{
    public partial class VLinkROI : UserControl
    {
        public VLinkROI()
        {
            InitializeComponent();
        }
       public string NoUseROI = "";
       public TreeNodeCollection Nodes;
       public event EventHandler CallBackNodes;
        public event EventHandler ROIChange;
        public string ROIText {
            get { return vTextBox1.TextStr; }
            set { vTextBox1.TextStr = value;}
        }
        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                base.BackColor = value;
                vTextBox1.BackColor = value;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (CallBackNodes != null)
            {
                CallBackNodes(this,null);
            }
            else
            {
                return;
            }
            contextMenuStrip1.Items.Clear();
            foreach (TreeNode toolNode1 in Nodes)
            {
                if (toolNode1.Text == NoUseROI)       
                    continue;
                if (((TreeNode)toolNode1).Text != "输出项")
                {
                    foreach (TreeNode itemNode1 in ((TreeNode)toolNode1).Nodes)
                    {
                        string sourceType = itemNode1.Tag.ToString();
                        if (sourceType == "Region")
                        {
                            if (((TreeNode)itemNode1).SelectedImageIndex == 3)
                            {
                                string resultStr = toolNode1.Text + "." + itemNode1.Text;
                                ToolStripItem item1 = contextMenuStrip1.Items.Add(resultStr);
                                item1.Name = resultStr;
                                item1.Click += new EventHandler(ConnectSource);
                            }
                        }
                    }
                }
               
            }
            contextMenuStrip1.Show(vTextBox1, new Point(0, vTextBox1.Height+5));
        }
        private void ConnectSource(object sender, EventArgs e)
        {
            vTextBox1.TextStr = (sender as ToolStripItem).Text;
            if (ROIChange != null)
            {
                ROIChange(this,null);
            }
          
        }
    }
}
