using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using VSE.Core;
using WinFormsUI.Docking;

namespace VSE
{
    [Serializable]
    public class ToolBase
    {
        /// <summary>
        /// 工具锁
        /// </summary>
        internal object obj = new object();
        /// <summary>
        /// 流程名
        /// </summary>
        internal string jobName = string.Empty;
        /// <summary>
        /// 工具运行状态
        /// </summary>
        internal ToolRunStatu toolRunStatu = ( ToolRunStatu.未运行);
        /// <summary>
        /// 运行工具
        /// </summary>
        public virtual void Run(bool updateImage, bool runTool, string toolName) { }
        /// <summary>
        /// 图像窗体锁
        /// </summary>
        private object obj11 = new object();

        internal object GetValue(object obj, string name)
        {
            foreach (PropertyInfo pi in obj.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public))
            {
                if (pi.Name == name)
                {


                    return pi.GetValue(obj, null);
                }
            }
            return new object();
        }

        /// <summary>
        /// 通过流程名获取窗体句柄
        /// </summary>
        /// <param name="jobName"></param>
        /// <returns></returns>
        internal Lxc.VisionPlus.ImageView.ImgView GetImageWindowControl()
        {
            try
            {
                if (Machine.curFormMode == FormMode.VisionForm)
                {
                    foreach (KeyValuePair<string, Lxc.VisionPlus.ImageView.ImgView> item in Win_ImageWindow.HDisplayCtrs)
                    {
                        if (item.Key == Job.FindJobByName(jobName).debugImageWindow)
                        {
                            return item.Value;
                        }
                    }
                    return Win_ImageWindow.HDisplayCtrs["0"];
                }
                else
                {
                    foreach (var item in Start.UserImage)
                    {
                        if (item.Name== Job.FindJobByName(jobName).imageWindowName)
                        {
                            return item as Lxc.VisionPlus.ImageView.ImgView;
                        }
                    }
                    return Start.UserImage[0];
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
                return null;
            }
        }
        /// <summary>
        /// 通过流程名获取窗体句柄，与上一个函数的使用场合不同，此函数用于工具构造函数里面，因为创建工具时还没有流程名，所以只能用这个函数
        /// </summary>
        /// <param name="jobName"></param>
        /// <returns></returns>
        internal Lxc.VisionPlus.ImageView.ImgView GetImageWindowControl(string jobName)
        {
            try
            {
                foreach (KeyValuePair<string, Lxc.VisionPlus.ImageView.ImgView> item in Win_ImageWindow.HDisplayCtrs)
                {
                    if (item.Key == Job.FindJobByName(jobName).debugImageWindow)
                    {
                        IDockContent temp = Win_Main.Instance.dockPanel.ActiveDocument;
                        Win_ImageWindow ff = temp as Win_ImageWindow;
                        if ((Machine.machineRunStatu != MachineRunStatu.Running && !Job.FindJobByName(jobName).isRunLoop && ff == null)
                            || (Machine.machineRunStatu != MachineRunStatu.Running && !Job.FindJobByName(jobName).isRunLoop && ff.Text != Job.FindJobByName(jobName).debugImageWindow)
                      || (Machine.machineRunStatu != MachineRunStatu.Running && Job.FindJobByName(jobName).isRunLoop) && Job.FindJobByName(jobName).jobName == Win_Job.Instance.JobEdit_JobName.Text.Split('：')[1])
                            item.Value.Show();              //切换到当前图像窗体
                        return item.Value;
                    }
                }
                return Win_ImageWindow.HDisplayCtrs["0"];
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
                return null;
            }
        }

        /// <summary>
        /// 获取图片源Job
        /// </summary>
        /// <param name="sourceFrom"></param>
        /// <param name="sourceToolName"></param>
        /// <param name="toolItem"></param>
        /// <param name="thisJob"></param>
        /// <param name="myJob"></param>
        /// <param name="hImage"></param>
        internal void getOriginalJob(string sourceFrom, string sourceToolName, string toolItem, bool isRunJob, int iToolAddress, Job thisJob, out HImage hImage)
        {
            //获取图片连接源Job
            Job myJob = new Job();
            hImage=new HImage();    
            if (sourceFrom.Substring(2, 1) == "[")
            {
                foreach (Job job in Project.Instance.curEngine.L_jobList)
                {
                    if (job.jobName == sourceFrom.Split(']')[0].Substring(3))
                    {
                        sourceToolName = sourceFrom.Split('.')[0].Split(']')[1];
                        myJob = job;
                        hImage = myJob.FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as HImage;
                        break;
                    }
                }
            }
            else
            {
                myJob = thisJob;
                hImage = myJob.FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as HImage;
            }
        }

        /// <summary>
        /// 获取图片源Job取像工具
        /// </summary>
        /// <param name="sourceFrom"></param>
        /// <param name="sourceToolName"></param>
        /// <param name="thisJob">当前Job</param>
        /// <param name="isLocal">判断相源本地/异地</param>
        /// <param name="vTool">相源工具</param>
        internal void getOriginalJobAcquisition(string sourceFrom, string sourceToolName, Job thisJob, out bool isLocal, out ToolBase vTool)
        {
            vTool = new ToolBase();
            if (sourceFrom.Substring(2, 1) == "[")
            {
                foreach (Job job in Project.Instance.curEngine.L_jobList)
                {
                    if (job.jobName == sourceFrom.Split(']')[0].Substring(3))
                    {
                        sourceToolName = sourceFrom.Split('.')[0].Split(']')[1];
                        vTool = job.FindToolByName(sourceToolName);
                        isLocal = false;//判定Job是不是有相源Job,否,false
                        break;
                    }
                }
                isLocal = false;
            }
            else
            {
                isLocal = true;//判定Job是不是有相源Job,是,True
                vTool = thisJob.FindToolByName(sourceToolName);
            }
        }

        /// <summary>
        /// 显示图像
        /// </summary>
        /// <param name="iToolAddress"></param>
        /// <param name="hImage"></param>
        internal void DisplayImage(int iToolAddress, HImage hImage)
        {
            if (iToolAddress == 0)
            {
                GetImageWindowControl().BeginInvoke(new MethodInvoker(() =>
                {
                    GetImageWindowControl().Image = hImage;
                }));
            }
        }

    }
    

}
