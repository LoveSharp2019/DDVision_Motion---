using HalconDotNet;
using Lxc.VisionPlus.ImageView;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using VControls;
using VSE.Core;

namespace VSE
{
    public static class Start
    {
        public static WinLoadUI Win_Welcome = new WinLoadUI();
        public static Form Main = new Form();
        public static List<ImgView> UserImage = new List<ImgView>();
        public static event MachineStateChangeEventHandler MachineStateChangeEvent;
        public static event JobRunDoneEventHandler JobRunDoneEvent;
        public static event ProductNewsClearEventHandler ProductNewsClearEvent;
        public static event UpdateJobNameListEventHandler UpdateJobNameListEvent;
        public static event SwitchProject SwitchProjectEvent;
        public static event NewProject NewProjectEvent;
        public static bool ImgEnqueueFlag = false;
        public static bool ImgEnqueueFlagOp = false;
        public static bool ImaEnqueueTool = false;
        public static bool RunOnceStopMark = false;
        public static bool JobRunMarkM = false;
        public static bool JobRunMark1 = false;

        public static bool JobSwitchMark_Hard = false;
        public static bool JobSwitchMark_Soft = false;
        public static bool ImgEnqueueSwitch = false;

        public static bool isFirstLoadJob = false;
        public static bool isCamBreakWhile = false;//判断设备停止时，相机跳出循环

        private static void GetUserImageWin()
        {
            foreach (var item in Main.Controls)
            {
                if (item.GetType()==typeof(ImgView))
                {
                    UserImage.Add(item as ImgView);
                }
            }
        
        }
        /// <summary>
        /// 外部调用获取全局变量的值
        /// </summary>
        /// <param name="name">变量名称</param>
        /// <param name="V">获取到的值</param>
        /// <returns>返回的信息 读成功返回 OK</returns>
        public static string GetVariable(string name,ref object V)
        { 
            try
            {
                return Project.Instance.curEngine.globelVariable.ReadVariableValue(name, ref V);
            }
            catch (Exception e)
            {

                return "异常:"+e.Message;
            }
        }

        public static void SetResultImage(string JobName,HImage img)
        {
            Job.FindJobByName(JobName).L_toolList[0].GetOutput("图像").value = img;

            Win_Main.TempImage = img;
            Win_Main.TempJobName = JobName;
            

        }



        /// <summary>
        /// 外部调用写入全局变量的值
        /// </summary>
        /// <param name="name">变量名称</param>
        /// <param name="Value">写入的值</param>
        /// <returns>返回的信息 写成功返回 OK</returns>
        
        public static string SetVariable(string name,object Value)
        {
            try
            {
                return Project.Instance.curEngine.globelVariable.WriteVariableValue(name, Value);
            }
            catch (Exception e)
            {

                return "异常:" + e.Message;
            }
           
        }
        /// <summary>
        /// 初始化框架，一般在程序启动时调用一次
        /// </summary>
        /// <param name="showStartWindow">指示是否显示程序启动时的欢迎窗体</param>
    
        public static void Init(Form Main)
        {
            try
            {
                if (Dog.CheckActive() != 0)
                {
                    return;
                }
                if (Dog.GetStates())
                {

                }
                Application.EnableVisualStyles();
               // Application.SetCompatibleTextRenderingDefault(false);
                Start.Main = Main;
                Start.Main.TopLevel = false;
                Start.Main.Dock = DockStyle.Fill;
                GetUserImageWin();
                Win_Main frm = Win_Main.Instance;
                Thread th = new Thread(Machine.InitAll)
                {
                    IsBackground = true
                };
                th.Start();
                while (Machine.loading == true)
                {
                    Thread.Sleep(5);
                    System.Windows.Forms.Application.DoEvents();
                }
               
                Application.Run(frm);
            }
            catch (Exception ex)
            {
              Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        public static void OnMachineStateChange(MachineStateChangeEventArgs e)
        {
            if (Start.MachineStateChangeEvent != null)
            {
                MachineStateChangeEvent(e);
            }
        }
        public static void OnJobRunDone(JobRunDoneEventArgs e)
        {
            if (Start.JobRunDoneEvent != null)
            {
                JobRunDoneEvent(e);
            }
        }

        public static void OnProductNewsClear()
        {
            if (Start.ProductNewsClearEvent != null)
            {
                ProductNewsClearEvent();
            }
        }

        public static void OnUpdateJobNameList(JobNameListEventArgs e)
        {
            if (Start.UpdateJobNameListEvent != null)
            {
                UpdateJobNameListEvent(e);
            }
        }
        public static void OnSwitchProject(string e)
        {
            if (Start.SwitchProjectEvent != null)
            {
                SwitchProjectEvent(e);
            }
        }
        public static void OnNewProject(string e)
        {
            if (Start.NewProjectEvent != null)
            {
                NewProjectEvent(e);
            }
        }
        public static void SetVSEMachineState(state MacState)
        {
           Win_Main.Instance.MachineState(MacState);
        }

    }
    
    public delegate void MachineStateChangeEventHandler(MachineStateChangeEventArgs e);
    public delegate void JobRunDoneEventHandler(JobRunDoneEventArgs e);
    public delegate void ProductNewsClearEventHandler();
    public delegate void UpdateJobNameListEventHandler(JobNameListEventArgs e);
    public delegate void SwitchProject(string ProjectName);
    public delegate void NewProject(string ProjectName);
    public enum state
    {
        start,
        stop,
        pause,
        reset
    }
    public class MachineStateChangeEventArgs : EventArgs
    {
      
        private state macState;

        public state MacState
        {
            get
            {
                return this.macState;
            }
        }

        public MachineStateChangeEventArgs(state State)
        {
            macState = State;
        }

    }
    public class JobRunDoneEventArgs : EventArgs
    {
      
        private string jobName;
        private double elapsedTime;
        private HImage image;
        private List<OutT> mResult;
        private bool bJobStatus;
        private bool bProStatus;
        private int iJobCount;

        public string JobName
        {
            get { return this.jobName; }
        }
        public List<OutT> Result
        {
            get { return this.mResult; }
        }
        public HImage Image 
        {
            get { return image; } 
        }
        public double ElapsedTime
        {
            get { return this.elapsedTime; }
        }
        public bool BJobStatus
        { 
            get { return this.bJobStatus; }
        }
        public bool BProStatus 
        { 
            get { return this.bProStatus; }
        }

        public int JobCount
        {
            get { return this.iJobCount; }
        }

        public JobRunDoneEventArgs(string jobName, double elapsedTime, HImage image, List<OutT> mResult, bool bJobStatus, bool bProStatus,int iJobCount)
        {
            this.jobName = jobName;
            this.elapsedTime = elapsedTime;
            this.image = image;
            this.mResult = mResult;
            this.bJobStatus = bJobStatus;   
            this.bProStatus = bProStatus;   
            this.iJobCount = iJobCount;
        }

    }


    public class JobNameListEventArgs : EventArgs
    {
        private string[] jobNameList;

        public string[] JobNameList
        {
            get { return this.jobNameList; }
        }

        public JobNameListEventArgs(string[] strJobNameList)
        {
            this.jobNameList = strJobNameList;
        }

    }
}
