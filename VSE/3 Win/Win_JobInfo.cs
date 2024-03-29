using Lxc.VisionPlus.ImageView;
using System;
using System.Linq;
using System.Windows.Forms;
using VControls;
using VSE.Core;

namespace VSE
{
    public partial class Win_JobInfo :FormBase
    {
        public Win_JobInfo()
        {
            InitializeComponent();
            this.BackColor = VControls.VUI.WinTitleBarBackColor;
            comboBox1.BackColorNormal = VControls.VUI.WinBackColor;
            cbx_imageWindowList.BackColorNormal = VControls.VUI.WinBackColor;
            cComboBox1.BackColorNormal = VControls.VUI.WinBackColor;
           taskList3.ImageList= Win_ToolBox.Instance.imageList;
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Win_JobInfo _instance;
        public static Win_JobInfo Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_JobInfo();
                return _instance;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                //修改流程名
                if (XXXXXXXXXXXXXlbl_title.Text.Trim() != Win_Job.Instance.JobEdit_JobName.Text.Split('：')[1])
                {
                    //修改所有工具里面的流程名
                    for (int i = 0; i < Job.FindJobByName(Win_Job.Instance.JobEdit_JobName.Text.Split('：')[1]).L_toolList.Count; i++)
                    {
                        Job.FindJobByName(Win_Job.Instance.JobEdit_JobName.Text.Split('：')[1]).L_toolList[i].tool.jobName = XXXXXXXXXXXXXlbl_title.Text.Trim();
                    }

                    Job.FindJobByName(Win_Job.Instance.JobEdit_JobName.Text.Split('：')[1]).jobName = XXXXXXXXXXXXXlbl_title.Text.Trim();
                    
                    Win_Job.Instance.listView1.SelectedNode.Text = XXXXXXXXXXXXXlbl_title.Text.Trim();
                    Win_Job.Instance.JobEdit_JobName.Text = "当前流程："+ XXXXXXXXXXXXXlbl_title.Text.Trim();
                    
                }

                Job.FindJobByName(XXXXXXXXXXXXXlbl_title.Text.Trim()).imageWindowName = cbx_imageWindowList.Text;
                Job.FindJobByName(XXXXXXXXXXXXXlbl_title.Text.Trim()).debugImageWindow = comboBox1.Text;
                this.Close();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            Win_Monitor.UpdateOutlist(true);
        }
        

       
        private void cComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            Job.FindJobByName(Win_Job.Instance.JobEdit_JobName.Text.Split('：')[1]).jobRunMode = (JobRunMode)cComboBox1.SelectedIndex;
        }

        private void Win_JobInfo_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                try
                {
                    this.TopMost = true;
                    label2.BringToFront();
                    //获取编辑界面所有的窗体控件
                    cbx_imageWindowList.Items.Clear();
                    comboBox1.Items.Clear();
                    if (!cbx_imageWindowList.Items.Contains("不绑定"))
                        cbx_imageWindowList.Items.Add("不绑定");
                    if (!comboBox1.Items.Contains("不绑定"))
                        comboBox1.Items.Add("不绑定");

                    Job jobtemp = Job.FindJobByName(Win_Job.Instance.JobEdit_JobName.Text.Split('：')[1]);

                    foreach (Control item in Start.UserImage)
                    {
                        if (item.GetType() == typeof(ImgView))
                        {
                            cbx_imageWindowList.Items.Add(item.Name.ToString());
                        }
                    }

                    cbx_imageWindowList.Text = jobtemp.imageWindowName;
                    if (cbx_imageWindowList.Text == "")
                    {
                        cbx_imageWindowList.Text = "不绑定";
                    }
                    for (int i = 0; i < Win_ImageWindow.HDisplayCtrs.Keys.Count; i++)
                    {
                        comboBox1.Items.Add(Win_ImageWindow.HDisplayCtrs.Keys.ToArray()[i]);
                    }
                    comboBox1.Text = jobtemp.debugImageWindow;
                    int k = (int)jobtemp.jobRunMode;
                    cComboBox1.Text = cComboBox1.Items[k].ToString();
                    this.cComboBox1.SelectedIndexChanged += new System.EventHandler(this.cComboBox1_SelectedIndexChanged);
                    //加载工具
                    label5.Text = "流程："+this.XXXXXXXXXXXXXlbl_title.Text + "--工具列表";
                    taskList1.NodeMouseClick -= TaskList1_NodeMouseClick;
                    taskList1.Nodes.Clear();
                    taskList1.ImageList = jobtemp.GetJobTree().ImageList;
                   
                    foreach (TreeNode item in jobtemp.GetJobTree().Nodes)
                    {
                       taskList1.Nodes.Add(item.Text, item.Text,7);
                        foreach (TreeNode item1 in item.Nodes)
                        {
                            if (item1.ImageIndex==3)
                            {
                                taskList1.Nodes[taskList1.Nodes.Count - 1].Nodes.Add(item1.Text, item1.Text, item1.ImageIndex);
                            }
                            
                        }
                       
                    }
                    UpdateOutList2UI();
                    taskList1.NodeMouseClick += TaskList1_NodeMouseClick;
                    taskList1.SelectedNode = null;
                    taskList2.SelectedNode = null;
                    taskList3.SelectedNode = null;
                    button1.Enabled = false;
                    button2.Enabled = false;    
                    //JobName ToolName, 输出节点
                    // Job.FindJobByName(jobName).FindToolInfoByName(vLinkImage1.ImageText.Split('.')[0]).GetOutput(vLinkImage1.ImageText.Split('.')[1]).value;
                }
                catch (Exception ex)
                {
                    this.cComboBox1.SelectedIndexChanged -= new System.EventHandler(this.cComboBox1_SelectedIndexChanged);
                    taskList1.NodeMouseClick -= TaskList1_NodeMouseClick;
                    Log.SaveError(Project.Instance.configuration.dataPath,ex);
                }
            }
            else
            {
                this.cComboBox1.SelectedIndexChanged -= new System.EventHandler(this.cComboBox1_SelectedIndexChanged);
            }
        }


        private void TaskList1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level==0)
            {
                taskList2.Nodes.Clear();
                taskList1.SelectedNode=e.Node;
                label6.Text = e.Node.Text + "--输出";
                label6.Image = taskList1.ImageList.Images[e.Node.ImageIndex];
                foreach (TreeNode item in e.Node.Nodes)
                {
                    taskList2.Nodes.Add(item.Text, item.Text,0);
                }
            }
            button1.Enabled = (taskList1.SelectedNode != null && taskList2.SelectedNode != null);
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (taskList1.SelectedNode!=null&&taskList2.SelectedNode!=null)
            {
               // string text = (taskList3.Nodes.Count + 1) + ":" + taskList1.SelectedNode.Text + "." + taskList2.SelectedNode.Text;
               // taskList3.Nodes.Add(text,text,taskList1.SelectedNode.ImageIndex);
                //添加到输出集合
                Job jobTemp = Job.FindJobByName(Win_Job.Instance.JobEdit_JobName.Text.Split('：')[1]);
                jobTemp.AddToolOut2List(taskList1.SelectedNode.Text + "." + taskList2.SelectedNode.Text, taskList1.SelectedNode.ImageIndex);
                UpdateOutList2UI();
              
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            if (taskList3.SelectedNode != null)
            {
                //从集合移除输出项
                Job jobTemp = Job.FindJobByName(Win_Job.Instance.JobEdit_JobName.Text.Split('：')[1]);
                jobTemp.RemoveToolOutFromList(taskList3.SelectedNode.Index);
                UpdateOutList2UI();
            }
        }
        private void UpdateOutList2UI()
        {
            Job jobTemp = Job.FindJobByName(Win_Job.Instance.JobEdit_JobName.Text.Split('：')[1]);
            taskList3.Nodes.Clear();
            foreach (OutT item in jobTemp.OutPutList)
            {
                taskList3.Nodes.Add((taskList3.Nodes.Count + 1) + ":" +item.MPath, (taskList3.Nodes.Count + 1) + ":" +item.MPath, item.MIconIndex);
            }
            button4.Enabled = taskList3.Nodes.Count > 0;
        }
        private void taskList2_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            taskList2.SelectedNode = e.Node;
            button1.Enabled = (taskList1.SelectedNode != null && taskList2.SelectedNode != null);
        }

        private void taskList3_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            taskList3.SelectedNode = e.Node;

            button2.Enabled = true; 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button2.Enabled=false;
            Job jobTemp = Job.FindJobByName(Win_Job.Instance.JobEdit_JobName.Text.Split('：')[1]);
            jobTemp.OutPutList.Clear();
            UpdateOutList2UI();

        }
    }
}
