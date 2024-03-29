using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using VSE.Core;
using WinFormsUI.Docking;

namespace VSE
{
    internal partial class Win_Monitor : DockContent
    {
        internal Win_Monitor()
        {
            InitializeComponent();
            this.BackColor = VControls.VUI.WinBackColor;
            VSE.Start.JobRunDoneEvent += Start_JobRunDoneEvent;
        }

       

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Win_Monitor _instance;
        public static Win_Monitor Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                    _instance = new Win_Monitor();
                return _instance;
            }
        }


        private void Win_Monitor_Load(object sender, EventArgs e)
        {

          
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!Win_GlobalVariable.Instance.Visible)
                {
                    UpdateGlobelVariablelist();
                }
                
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        public string TranslateOut(OutT T)
        {
            string ValueText = "";
            switch (T.MDataType)
            {
                case DataType.Bool:
                    ValueText = T.GetValueB().ToString();
                    break;
                case DataType.Double:
                    ValueText = String.Format("{0:f3}", T.GetValueD());
                    break;
                case DataType.String:
                    ValueText = T.GetValueS().ToString();
                    break;
                case DataType.Int:
                    ValueText = T.GetValueI().ToString() ;
                    break;
                case DataType.Line:
                    Line L = T.GetValueLine();
                    ValueText = String.Format("起点[{0}  {1}] 终点[{2}  {3}]", L.起点.X.ToString(), L.起点.Y.ToString(), L.终点.X.ToString(), L.终点.Y.ToString());
                    break;
                case DataType.Circle:
                    ValueText = T.GetValueCircle().ToString();
                    break;
                case DataType.Pose:
                    if (T.GetValuePose() == null)
                    {
                        ValueText = "未匹配到模板";
                    }
                    else
                    {
                        XYU p = T.GetValuePose();
                        ValueText = String.Format("XYR[{0}  {1}  {2}]", p.Point.X, p.Point.Y, p.U);
                    }

                    break;
                case DataType.XY:
                    if (T.GetValuePointD() == null)
                    {
                        ValueText += "未匹配到模板";
                    }
                    else
                    {
                        XY pp = T.GetValuePointD();
                        ValueText = String.Format("XY[{0}  {1}]", pp.X, pp.Y);

                    }

                    break;
                default:
                    ValueText = T.ToString();
                    break;

            }
            return ValueText;
        }
        string jobName = "";
        string schemeName = "";
        public static void UpdateOutlist(bool ForceUpdate = false)
        {
            //流程输出
            if (Win_Monitor.Instance.lxcRadioButton4.Checked)
            {
                if (Win_Job.Instance.JobEdit_JobName.Text.Split('：').Length < 2)
                {
                    return;
                }
                Job jobTemp = Job.FindJobByName(Win_Job.Instance.JobEdit_JobName.Text.Split('：')[1]);
                if (ForceUpdate || Win_Monitor.Instance.jobName != jobTemp.jobName)
                {

                    Win_Monitor.Instance.jobName = jobTemp.jobName;

                    //hzy20220505，优化控件，在某个线程上创建的控件不能成为另一个线程上创建控件的父级
                    int iCount = 0;
                    Win_Monitor.Instance.displayTable1.BeginInvoke(new MethodInvoker(() => 
                    {
                        Win_Monitor.Instance.displayTable1.RowCount = jobTemp.OutPutList.Count;
                        iCount= jobTemp.OutPutList.Count;
                    }));
                    if (iCount == 0)
                    {
                        return;
                    }

                    //Win_Monitor.Instance.displayTable1.RowCount = jobTemp.OutPutList.Count;
                    //if (Win_Monitor.Instance.displayTable1.RowCount == 0)
                    //{
                    //    return;
                    //}
                }
                if (jobTemp.OutPutList.Count == 0)
                {
                    return;
                }
                object obj = Win_Monitor.Instance;
                Graphics g = Win_Monitor.Instance.CreateGraphics();
                float[] vs = new float[Win_Monitor.Instance.displayTable1.RowCount];
                string[] content = new string[Win_Monitor.Instance.displayTable1.RowCount];
                string ValueText = "";
                for (int j = 0; j < Win_Monitor.Instance.displayTable1.RowCount; j++)
                {
                    vs[j] = g.MeasureString(jobTemp.OutPutList[j].MPath, Win_Monitor.Instance.displayTable1.Items[0].Font).Width;
                }
                float max = vs.Max();
                for (int j1 = 0; j1 < Win_Monitor.Instance.displayTable1.RowCount; j1++)
                {
                    content[j1] = jobTemp.OutPutList[j1].MPath;
                    for (float i = 0; i < (max - vs[j1]) / 4.2; i++)
                    {
                        content[j1] += " ";
                    }
                }
                for (int i = 0; i < Win_Monitor.Instance.displayTable1.RowCount; i++)
                {
                    ValueText = Win_Monitor.Instance.TranslateOut(jobTemp.OutPutList[i]);
                    Win_Monitor.Instance.displayTable1.Items[i].CellText = String.Format("{0}: {1}   {2}", (i + 1), content[i], ValueText);
                }
            }
        }
        public static void UpdateGlobelVariablelist(bool ForceUpdate = false)
        {
            //全局变量
            if (Win_Monitor.Instance.lxcRadioButton1.Checked)
            {
                Scheme SchemeTemp = Project.Instance.curEngine;
                if (ForceUpdate || Win_Monitor.Instance.schemeName != SchemeTemp.schemeName)
                {

                    Win_Monitor.Instance.schemeName = SchemeTemp.schemeName;

                    Win_Monitor.Instance.displayTable1.RowCount = Project.Instance.curEngine.globelVariable.GetGlobalVariableCount();
                    if (Win_Monitor.Instance.displayTable1.RowCount == 0)
                    {
                        return;
                    }
                }
                Graphics g = Win_Monitor.Instance.CreateGraphics();
                float[] vs = new float[Win_Monitor.Instance.displayTable1.RowCount];
                float[] vs1 = new float[Win_Monitor.Instance.displayTable1.RowCount];
                string[] content = new string[Win_Monitor.Instance.displayTable1.RowCount];
                string[] content1 = new string[Win_Monitor.Instance.displayTable1.RowCount];
                string ValueText = "";
                for (int j = 0; j < Win_Monitor.Instance.displayTable1.RowCount; j++)
                {
                    vs[j] = g.MeasureString(SchemeTemp.globelVariable.GetGlobalVariable(j).name, Win_Monitor.Instance.displayTable1.Items[0].Font).Width;
                    vs1[j] = g.MeasureString(SchemeTemp.globelVariable.GetGlobalVariable(j).type, Win_Monitor.Instance.displayTable1.Items[0].Font).Width;
                }
                float max = vs.Max();
                float max1 = vs1.Max();
                for (int j1 = 0; j1 < Win_Monitor.Instance.displayTable1.RowCount; j1++)
                {
                    content[j1] = SchemeTemp.globelVariable.GetGlobalVariable(j1).name;
                    for (float i = 0; i < (max - vs[j1]) / 4.2; i++)
                    {
                        content[j1] += " ";
                    }
                    content1[j1] = SchemeTemp.globelVariable.GetGlobalVariable(j1).type;
                    for (int i = 0; i < (max1 - vs1[j1]) / 4; i++)
                    {
                        content1[j1] += " ";
                    }
                }
                for (int i = 0; i < Win_Monitor.Instance.displayTable1.RowCount; i++)
                {
                    ValueText =  SchemeTemp.globelVariable.GetGlobalVariable(i).value.ToString();
                    Win_Monitor.Instance.displayTable1.Items[i].CellText = String.Format("{0}: {1}   {2}  {3}", (i + 1), content[i], content1[i], ValueText);
                }
            }
        }
        private void Start_JobRunDoneEvent(JobRunDoneEventArgs e)
        {
            UpdateOutlist();
        }
        private void Win_Monitor_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (lxcRadioButton1.Checked)
                {
                    VSE.Start.JobRunDoneEvent -= Start_JobRunDoneEvent;
                    UpdateGlobelVariablelist(true);

                }
                else
                {
                    VSE.Start.JobRunDoneEvent -= Start_JobRunDoneEvent;
                    UpdateOutlist(true);
                    VSE.Start.JobRunDoneEvent += Start_JobRunDoneEvent;
                }
            }
            else
            {
                VSE.Start.JobRunDoneEvent -= Start_JobRunDoneEvent;
            }
        }

        private void lxcRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (lxcRadioButton1.Checked)
            {
                
                VSE.Start.JobRunDoneEvent -= Start_JobRunDoneEvent;
                UpdateGlobelVariablelist(true);
            }
            else
            {
                VSE.Start.JobRunDoneEvent -= Start_JobRunDoneEvent;
                UpdateOutlist(true);
                VSE.Start.JobRunDoneEvent += Start_JobRunDoneEvent;
            }
            
        }
    }
}
