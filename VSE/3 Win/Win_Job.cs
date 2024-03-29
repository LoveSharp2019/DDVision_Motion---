using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WinFormsUI.Docking;
using VSE.Properties;
using VSE.Core;
using VControls;

namespace VSE
{
    internal partial class Win_Job : DockContent
    {
        internal Win_Job()
        {
            InitializeComponent();
            this.BackColor = VControls.VUI.WinBackColor;
            JobEdit_JobName.BackColor = VControls.VUI.WinTitleBarBackColor;
            ToolBoxEdit_ProjectName.BackColor = VControls.VUI.WinTitleBarBackColor;
            listView1.BackColor = VControls.VUI.WinBackColor;
            Job.FlushJobList += Job_FlushJobList;

        }
        private void Job_FlushJobList(object sender, EventArgs e)
        {
            listView1.Nodes.Clear();
            int count = Project.Instance.curEngine.L_jobList.Count;
            for (int i = 0; i < count; i++)
            {
                listView1.Nodes.Add(Project.Instance.curEngine.L_jobList[i].jobName);
            }
            if (Project.Instance.curEngine.L_jobList.Count>0)
            {
                Job.LoadJob(Project.Instance.curEngine.L_jobList[0]);
            }
            else
            {
                ToolBoxEdit_ProjectName.Text = Win_Job.Instance.ToolBoxEdit_ProjectName.Text.Split('-')[0] + "-- 空";
            }
            
        }
        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Win_Job _instance;
        public static Win_Job Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_Job();
                return _instance;
            }
        }
        public void EnableRun(bool flag,bool IsJobRun)
        {
            listView1.Enabled = !flag;
            tbc_jobs.Enabled = !flag;

            if (IsJobRun)
            {
                tsb_createJob.Enabled = !flag;
                tsb_deleteJob.Enabled = !flag;
                tsb_jobInfo.Enabled = !flag;
                tsb_foldJob.Enabled = !flag;
                tsb_expandJob.Enabled = !flag;
            }
            else
            {
                toolStrip1.Enabled = !flag;
            }
           
        }
        private void btn_jobLoopRun_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Admin))
                    return;

                //hzy20221121,add cam hard trigger Fool-proofing
                //if (Job.isCameraHardTrigger())
                //    return;

                string jobName = "";

                //hzy20220517,取消运行当前Job选择项
                //if (Win_Job.Instance.listView1.SelectedNode == null)
                //{

                //    jobName = Win_Job.Instance.JobEdit_JobName.Text.Substring(5);
                //}
                //else
                //{
                //    jobName= Win_Job.Instance.JobEdit_JobName.Text.Substring(5);
                //}

                //hzy20220517,Job list内所有job都运行
                int iJobCount = Win_Job.Instance.listView1.Nodes.Count;
                if (btn_runLoop.Text == "连续运行")
                {
                    //Job.FindJobByName(jobName).LoopRun(true);
                    //Win_Main.Instance.EnableRun(true,true);

                    if (iJobCount > 0)
                    {
                        for (int i = 0; i < iJobCount; i++)
                        {
                            string strJobName = Win_Job.Instance.listView1.Nodes[i].Text;
                            Job.FindJobByName(strJobName).LoopRun(true);
                            Win_Main.Instance.EnableRun(true, true);

                        }

                        btn_runOnce.Enabled = false;

                        Start.ImgEnqueueFlagOp = true;
                    }
                }
                else
                {
                    //Job.FindJobByName(jobName).LoopRun(false);
                    //Win_Main.Instance.EnableRun(false,true);

                    if (iJobCount > 0)
                    {
                        for (int i = 0; i < iJobCount; i++)
                        {
                            string strJobName = Win_Job.Instance.listView1.Nodes[i].Text;
                            Job.FindJobByName(strJobName).LoopRun(false);
                            Win_Main.Instance.EnableRun(false, true);
                        }

                        btn_runOnce.Enabled = true;

                        Start.ImgEnqueueFlagOp = false;

                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private void Win_Job_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        private void Win_Job_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
      
        private void tsb_createJob_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;
           
            Job.CreateJob();
            if (listView1.Nodes.Count > 3)
            {
                panel1.Size = new Size(panel1.Size.Width, 200);
            }
            else
            {
                panel1.Size = new Size(panel1.Size.Width, 135);
            }
        }
        private void tsb_expandJob_Click(object sender, EventArgs e)
        {
            if (Win_Job.Instance.listView1.Nodes.Count < 1)
                return;
            Job.GetJobTreeStatic().ExpandAll();
            //job.DrawLine();

        }
        private void tsb_foldJob_Click(object sender, EventArgs e)
        {
            try
            {
                if (Win_Job.Instance.listView1.Nodes.Count < 1)
                    return;
                string jobName = JobEdit_JobName.Text.Substring(5);
                Job job = Job.FindJobByName(jobName);
                Job.GetJobTreeStatic().CollapseAll();
               // job.DrawLine();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        internal void tsb_deleteJob_Click(object sender, EventArgs e)
        {
            if (Job.FindJobByName(JobEdit_JobName.Text.Substring(5)).isRunLoop)
            {
                Win_MessageBox.Instance.MessageBoxShow("当前流程正在运行，请先停止运行", TipType.Error);
                return;
            }
            if (listView1.Nodes.Count==0)
            {
                return;
            }
            Job.DeleteJob();
            foreach (Job item in Project.Instance.curEngine.L_jobList)
            {
                
                if (item.jobName == Win_Job.Instance.JobEdit_JobName.Text.Substring(5))
                {
                    Job.LoadJob(item);

                    break;
                }

            }
            if (listView1.Nodes.Count > 3)
            {
                panel1.Size = new Size(panel1.Size.Width, 200);
            }
            else
            {
                panel1.Size = new Size(panel1.Size.Width, 135);
            }
        }
        public void tsb_jobInfo_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Admin))
                    return;
                if (Win_Job.Instance.listView1.Nodes.Count == 0)
                {
                    Win_Main.Instance.OutputMsg("当前无可用流程，不可打开流程属性页面",Win_Log.InfoType.tip);
                    return;
                }
                string jobName = "";
                if (Win_Job.Instance.listView1.SelectedNode!=null)
                {
                    jobName = Win_Job.Instance.JobEdit_JobName.Text.Substring(5);
                }
                else
                {
                    jobName = Win_Job.Instance.JobEdit_JobName.Text.Substring(5);
                }
                Win_JobInfo.Instance.XXXXXXXXXXXXXlbl_title.Text = jobName;
                Win_JobInfo.Instance.StartPosition = FormStartPosition.Manual;
                Win_JobInfo.Instance.Location =new Point(PointToScreen(ToolBoxEdit_ProjectName.Location).X, PointToScreen(ToolBoxEdit_ProjectName.Location).Y+80 );
                ;
                Win_JobInfo.Instance.Show();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        private object objlock1 = new object();

        private void btn_runOnce_Click(object sender, EventArgs e)
        {
            lock (objlock1)
            {
                if (!Permission.CheckPermission(PermissionLevel.Admin))
                    return;

                //hzy20221121,add cam hard trigger Fool-proofing
                //if (Job.isCameraHardTrigger())
                //    return;

                Start.ImgEnqueueFlagOp = true;
                Start.RunOnceStopMark = true;

                Thread.Sleep(250);
                Thread th = new Thread(() =>
                {
                    Job.RunCurJob();
                    Thread.Sleep(100);
                });
                th.IsBackground = true;
                th.Start();

                while (true)
                {
                    if (!Start.RunOnceStopMark)
                    {
                        Start.ImgEnqueueFlagOp = false;
                        break;
                    }
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(10);
                }
            }
        }

        private void listView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            foreach (Job item in Project.Instance.curEngine.L_jobList)
            {
                if (e.Node == null)
                {
                    return;
                }
                if (item.jobName == e.Node.Text)
                {
                    Job.LoadJob(item);
                  
                    break;
                }

            }
        }

    }
}
