using VSE.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using VControls;
using VSE.Core;

namespace VSE
{
    internal partial class Win_IOConfig : FormBase
    { /// <summary>
      /// 当前工具所属的流程
      /// </summary>
        public string jobName = string.Empty;
        /// <summary>
        /// 当前工具名
        /// </summary>
        public string toolName = string.Empty;
        internal Win_IOConfig()
        {
            InitializeComponent();
            this.TopMost = true;
        }

        void checkBox1_stateChanged(bool state)
        {
            MessageBox.Show(state.ToString());
        }

        internal static ToolParBase result1 = new ToolParBase();

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Win_IOConfig _instance;
        public static Win_IOConfig Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_IOConfig();
                return _instance;
            }
        }
        /// <summary>
        /// 选择结果
        /// </summary>
        internal ConfirmBoxResult result = ConfirmBoxResult.Cancel;


        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      
        public void LoadIO()
        {


            treeView1.Nodes.Clear();
            GetValue(treeView1.Nodes, result1);
            treeView1.ExpandAll();
            treeView1.SelectedNode = treeView1.Nodes[0];
        }


        public object GetValue(TreeNodeCollection nodes, object obj)
        {
    
            foreach (PropertyInfo pi in obj.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public))
            {
                //节点上要显示参数名、参数类型和参数当前值
                string temp = Regex.Split(pi.ToString(), " ")[0];


                TreeNode node = new TreeNode();
                if (temp=="VSE.XYU")
                {
                    node = nodes.Add("", "<List<XYU>>  " + pi.Name);
                    node.Tag = DataType.Pose;
                    node.ForeColor = Color.Black;
                }
                else if (temp == "HalconDotNet.HObject")
                {
                    node = nodes.Add("", "<HObject>  " + pi.Name);
                    node.Tag = DataType.Image;
                    node.ForeColor = Color.Black;
                }
                else if (temp == "VMPro.XY")
                {
                    node = nodes.Add("", "<XY>  " + pi.Name);
                    node.Tag = DataType.XY;
                    node.ForeColor = Color.Black;
                }
                else if (temp == "Double")
                {
                    node = nodes.Add("", "<Double>  " + pi.Name + "=" + pi.GetValue(obj, null));
                    node.Tag = DataType.String;
                    node.ForeColor = Color.Black;
                }
                else if (temp == "System.String")
                {
                    node = nodes.Add("", "<String>  " + pi.Name + "=" + pi.GetValue(obj, null));
                    node.Tag = DataType.String;
                    node.ForeColor = Color.Black;
                }
                else if (temp == "HalconDotNet.HRegion")
                {
                    node = nodes.Add("", "<HRegion>  " + pi.Name);
                    node.Tag = DataType.Region;
                    node.ForeColor = Color.Black;
                }
                else if (temp == "Int32")
                {
                    node = nodes.Add("", "<Int32>  " + pi.Name + "=" + pi.GetValue(obj, null));
                    node.Tag = DataType.String;
                    node.ForeColor = Color.Black;
                }
                else if (temp == "System.Collections.Generic.List`1[VMPro.XY]")
                {
                    node = nodes.Add("", "<List<XY>>  " + pi.Name);
                    node.Tag = DataType.XY;
                    node.ForeColor = Color.Black;
                }
                else if (temp == "System.Collections.Generic.List`1[VSE.XYU]")
                {
                    node = nodes.Add("", "<List<XYU>>  " + pi.Name);
                    node.Tag = DataType.XY;
                    node.ForeColor = Color.Black ;
                }
                else
                {
                    node = nodes.Add("", pi.Name);
                    node.ForeColor  = Color.DarkGray ;
                }
                




                //string temp = Regex.Split(pi.ToString(), " ")[0];
                //if (temp == "HalconDotNet.HObject"
                //    || temp == "VisionAndMotionPro.XYU"
                //    )
                //{
                object value = pi.GetValue(obj, null);

                if (value == null || value.ToString() == "HalconDotNet.HObject" || value.ToString() == "HalconDotNet.HRegion")
                    continue;

                if (pi.PropertyType.IsValueType || pi.PropertyType.Name.StartsWith("String"))
                {

                }
                else if (pi.ToString().Contains("System.Collections.Generic.List`1[VSE.XYU]"))
                {
                    for (int i = 0; i < ((List<XYU>)value).Count; i++)
                    {
                        GetValue(node.Nodes, ((List<XYU>)value)[i]);
                    }
                }
                else if (pi.ToString().Contains("System.Collections.Generic.List`1[VMPro.XY]"))
                {
                    for (int i = 0; i < ((List<XY>)value).Count; i++)
                    {
                        GetValue(node.Nodes, ((List<XY>)value)[i]);
                    }
                }
                else
                {
                    GetValue(node.Nodes, value);
                }
            }
            return new object();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
          
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Level == 0)
                return;

            TreeNode node = this.treeView1.SelectedNode;
            while (node.Parent != null)
            {
                node = node.Parent;
            }

            if (node.Index == 0 || node.Index == 1)
            {
                Project.Instance.curEngine.FindJobByName(jobName).Add_input(treeView1.SelectedNode, comboBox1.Text);
            }
            else
            {

                Project.Instance.curEngine.FindJobByName(jobName).Add_output(treeView1.SelectedNode, comboBox1.Text);
            }
            treeView1.Focus();
        }
        internal static bool b = true;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (b)
            {
                Project.Instance.curEngine.FindJobByName(jobName).ShowIOEdit(comboBox1.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
                return;
            comboBox1.SelectedIndex -= 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == comboBox1.Items.Count  - 1)
                return;
            comboBox1.SelectedIndex += 1;
        }

        private void comboBox1_SelectedIndexChanged()
        {
            if (b)
            {
                Project.Instance.curEngine.FindJobByName(jobName).ShowIOEdit(comboBox1.Text );
            }
        }

        private void Win_IOConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
