
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using VControls;
using VSE.Core;
using VSE.Properties;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace VSE
{
    [Serializable]
    public class Job
    {
        public Job()
        {
            if (rightClickMenuAtBlank.Items.Count == 0)
            {
                rightClickMenu.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                rightClickMenuAtBlank.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                ToolStripItem toolStripItem_折叠流程树 = rightClickMenuAtBlank.Items.Add("折叠流程");
                toolStripItem_折叠流程树.BackColor = Color.White;
                toolStripItem_折叠流程树.Click += toolStripItem_折叠流程树_Click;
                ToolStripItem toolStripItem_展开流程树 = rightClickMenuAtBlank.Items.Add("展开流程");
                toolStripItem_展开流程树.BackColor = Color.White;
                toolStripItem_展开流程树.Click += toolStripItem_展开流程树_Click;
                ToolStripItem toolStripItem_启用全部 = rightClickMenuAtBlank.Items.Add("启用全部");
                toolStripItem_启用全部.BackColor = Color.White;
                toolStripItem_启用全部.Click += toolStripItem_启用全部_Click;
                ToolStripItem toolStripItem_忽略全部 = rightClickMenuAtBlank.Items.Add("禁用全部");
                toolStripItem_忽略全部.BackColor = Color.White;
                toolStripItem_忽略全部.Click += toolStripItem_忽略全部_Click;
                ToolStripItem toolStripItem_粘贴 = rightClickMenuAtBlank.Items.Add("粘贴");
                toolStripItem_粘贴.BackColor = Color.White;
                toolStripItem_粘贴.Click += PasteToolAtLast;
                ToolStripItem toolStripItem_删除当前流程 = rightClickMenuAtBlank.Items.Add("删除流程");
                toolStripItem_删除当前流程.BackColor = Color.White;
                toolStripItem_删除当前流程.Click += toolStripItem_删除流程_Click;
                toolStripItem_删除当前流程.Image = Resources.Delete1;
                ToolStripItem toolStripItem_流程属性 = rightClickMenuAtBlank.Items.Add("流程属性");
                toolStripItem_流程属性.BackColor = Color.White;
                toolStripItem_流程属性.Click += toolStripItem_流程属性_Click;
            }
        }

       
        internal void LoopRun(bool StartRun)
        {
            try
            {
            
                if (Win_Job.Instance.listView1.Nodes.Count == 0)
                {
                    Win_Main.Instance.OutputMsg("没有可运行的流程", Win_Log.InfoType.tip);
                    return;
                }
                
                Win_Job.Instance.btn_runLoop.Enabled = false;
                Win_Job.Instance.btn_runOnce.Enabled = false;
                Application.DoEvents();
                Thread.Sleep(50);

                if (StartRun)
                {
                    Start.JobRunMarkM = false;
                    Start.JobRunMark1 = false;

                    Application.DoEvents();
                    isRunLoop = true;

                    //hzy20220512 
                    //清空图像队列中图片
                    List<ToolBase> listCam = new List<ToolBase>(); //Job中取像工具个数
                    listCam.Clear();
                    for (int i = 0; i < Job.FindJobByName(jobName).L_toolList.Count; i++)
                    {
                        if (Job.FindJobByName(jobName).L_toolList[i].toolType == ToolType.ImageAcq)
                        {
                            listCam.Add(Job.FindJobByName(jobName).FindToolByName(L_toolList[i].toolName));
                        }
                    }
                    if (listCam.Count > 0)
                    {
                        for (int i = 0; i < listCam.Count; i++)
                        {
                            (Job.FindJobByName(jobName).FindToolByName(L_toolList[i].toolName) as AcqImageTool).ClearCallBackBuffer();//清空回调函数缓存图片
                            (Job.FindJobByName(jobName).FindToolByName(L_toolList[i].toolName) as AcqImageTool).QeHImages.Clear();//清空图像队列中的图片
                        }
                    }
                    //////////////////////////////////////////////////////////////////////////////////

                    Thread thread = new Thread(() =>
                    {
                        while (Job.FindJobByName(jobName).isRunLoop)
                        {
                            Job.FindJobByName(jobName).Run();
                            if (Start.isCamBreakWhile)
                            {
                                Start.isCamBreakWhile = false;
                                break;
                            }
                            //流程失败停止循环
                            if (Project.Instance.configuration.failStop)
                            {
                                if (Job.FindJobByName(jobName.ToString()).jobRunStatu != JobRunStatu.Succeed)
                                {
                                    isRunLoop = false;
                                    break;
                                }
                            }
                            //文件夹图像执行一遍后停止循环
                            if (Project.Instance.configuration.endStop)
                            {
                                for (int i = 0; i < Job.FindJobByName(jobName.ToString()).L_toolList.Count; i++)
                                {
                                    if (Job.FindJobByName(jobName.ToString()).L_toolList[i].toolType == ToolType.ImageAcq)
                                    {
                                        if (((AcqImageTool)Job.FindJobByName(jobName.ToString()).L_toolList[i].tool).imageSourceMode == ImageSourceMode.FromDirectory)
                                        {
                                            if (((AcqImageTool)Job.FindJobByName(jobName.ToString()).L_toolList[i].tool).currentImageIndex == ((AcqImageTool)Job.FindJobByName(jobName.ToString()).L_toolList[i].tool).L_images.Count - 1)
                                            {
                                                isRunLoop = false;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            Thread.Sleep(Convert.ToInt16(Project.Instance.configuration.timeBetweenJobRun));
                        }
                    });
                    Thread th_runJob = thread;
                    th_runJob.IsBackground = true;
                    th_runJob.Priority = ThreadPriority.AboveNormal;
                    th_runJob.Start();
             
                    Win_Job.Instance.btn_runLoop.Text = "停止运行";
                 
                    Thread.Sleep(10);
                }
                else
                {
                    Application.DoEvents();

                    //Hzy20220509,保证相源队列中所有图片都被执行完成
                    //检索工具集合内是否存在取像工具
                    List<ToolBase> listAcq = new List<ToolBase>(); //Job中取像工具个数
                    listAcq.Clear();

                    for (int i = 0; i < Job.FindJobByName(jobName).L_toolList.Count; i++)
                    {
                        if (Job.FindJobByName(jobName).L_toolList[i].toolType == ToolType.ImageAcq)
                        {
                            listAcq.Add(Job.FindJobByName(jobName).FindToolByName(L_toolList[i].toolName));
                        }
                    }
                    //  listAcq.Count > 0
                    if (listAcq.Count > 0)
                    {
                        //Job中有取像工具
                        //检索多个取像工具使能状态
                        //1,支持一使能，多禁用
                        //2,支持全部禁用
                        int iSum = 0;
                        for (int i = 0; i < listAcq.Count; i++)
                        {
                            if (Job.FindJobByName(jobName).L_toolList[i].enable)
                            {
                                iSum = iSum + 1;
                            }
                        }
                        if (iSum == 0)
                        {
                            //Job中所有取像工具都被禁用，图像来自其它Job相源队列
                            //判定相源队列中所有图片都被执行完成
                            ToolBase oriCamTool = new ToolBase();
                            for (int i = 0; i < Job.FindJobByName(jobName).L_toolList.Count; i++)
                            {
                                //判断除取像工具外其它所有input项
                                if (Job.FindJobByName(jobName).L_toolList[i].toolType != ToolType.ImageAcq)
                                {
                                    bool bMark = false;
                                    int inputCount = (Job.FindJobByName(jobName).L_toolList[i]).input.Count;//当前工具所有输入端个数
                                    for (int j = 0; j < inputCount; j++)
                                    {
                                        string inputItemName = L_toolList[i].input[j].IOName;
                                        string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                        if (sourceFrom == string.Empty)
                                        {
                                            continue;
                                        }
                                        if (inputItemName == "图像" || inputItemName == "InputImage")
                                        {
                                            string oriJobName = sourceFrom.Split(']')[0].Substring(3);//"模切检测"
                                            string oriCamName = sourceFrom.Split('.')[0].Substring(2).Split(']')[1];//"图像采集"
                                            Job oriJob = Job.FindJobByName(oriJobName);
                                            oriCamTool = oriJob.FindToolByName(oriCamName);
                                            bMark = true;
                                            break;
                                        }
                                    }
                                    if (bMark)
                                    {
                                        break;
                                    }
                                }
                            }
                            Stopwatch StTimeLimit = new Stopwatch();
                            StTimeLimit.Start();
                            while (true)
                            {
                                //等待相源队列中所有图片都被执行完成后停止自动运行
                                if ((oriCamTool as AcqImageTool).QeHImages.Count == 0 || StTimeLimit.ElapsedMilliseconds > 3000)
                                {
                                    Start.JobRunMark1 = true;//关闭从站Job
                                    if (StTimeLimit.ElapsedMilliseconds > 3000)
                                    {
                                        Start.OnProductNewsClear();//异常强制关停时，清空消息队列
                                    }
                                    StTimeLimit.Stop();
                                    (oriCamTool as AcqImageTool).QeHImages.Clear();
                                    break;
                                }
                                Application.DoEvents();
                                System.Threading.Thread.Sleep(20);

                            }
                        }
                        else if (iSum == 1)
                        {
                            //继续后续动作
                        }
                        else if (iSum > 1)
                        {
                            //不支持
                        }
                    }
                    else
                    {
                        //Job中无取像工具，图像来自相源队列
                        //判定相源队列中所有图片都被执行完成
                        ToolBase oriCamTool = new ToolBase();
                        for (int i = 0; i < Job.FindJobByName(jobName).L_toolList.Count; i++)
                        {
                            bool bMark = false;
                            int inputCount = (Job.FindJobByName(jobName).L_toolList[i]).input.Count;//当前工具所有输入端个数
                            for (int j = 0; j < inputCount; j++)
                            {
                                string inputItemName = Job.FindJobByName(jobName).L_toolList[i].input[j].IOName;
                                string sourceFrom = Job.FindJobByName(jobName).L_toolList[i].GetInput(inputItemName).value.ToString();
                                if (sourceFrom == string.Empty)
                                {
                                    continue;
                                }
                                if (inputItemName == "图像" || inputItemName == "InputImage")
                                {
                                    string oriJobName = sourceFrom.Split(']')[0].Substring(3);//"模切检测"
                                    string oriCamName = sourceFrom.Split('.')[0].Substring(2).Split(']')[1];//"图像采集"
                                    Job oriJob = Job.FindJobByName(oriJobName);
                                    oriCamTool = oriJob.FindToolByName(oriCamName);
                                    bMark = true;
                                    break;
                                }
                            }
                            if (bMark)
                            {
                                break;
                            }
                        }

                        Stopwatch StTimeLimit = new Stopwatch();
                        StTimeLimit.Start();    
                        while (true)
                        {
                            //等待相源队列中所有图片都被执行完成后停止自动运行
                            //hzy20220622
                            //if ((oriCamTool as AcqImageTool).QeHImages.Count == 0)
                            if ((oriCamTool as AcqImageTool).QeHImages.Count == 0 || StTimeLimit.ElapsedMilliseconds > 3000)
                            {
                                Start.JobRunMark1 = true;//关闭从站Job
                                if (StTimeLimit.ElapsedMilliseconds > 3000)
                                {
                                    Start.OnProductNewsClear();//异常强制关停时，清空消息队列
                                }
                                StTimeLimit.Stop();
                                (oriCamTool as AcqImageTool).QeHImages.Clear();
                                break;
                            }
                            Application.DoEvents();
                            System.Threading.Thread.Sleep(20);
                        }
                    }

                    isRunLoop = false;//停止Job自动运行线程
                    Thread.Sleep(20);
                    Win_Job.Instance.btn_runLoop.Text = "连续运行";
                    Win_Job.Instance.btn_runOnce.Enabled = true;
                }
                Win_Job.Instance.btn_runLoop.Enabled = true;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        public HTuple www;
        ///输出项集合
        private List<OutT> outPutList;
        public List<OutT> OutPutList
        {
            get
            {
                if (outPutList == null)
                {
                    outPutList = new List<OutT>();
                }
                return outPutList;
            }
            set { outPutList = value; }
        }
        ///当前流程是否处在连续运行状态
        internal bool isRunLoop = false;

        /// <summary>
        /// 当前流程此次运行结果
        /// </summary>
        public JobRunStatu jobRunStatu = JobRunStatu.Fail;
        public bool bJobRunStatus = false;
        public bool bJobProRunStatus = false;
        public string strErrToolName = "";
        /// <summary>
        /// 工具输入项个数
        /// </summary>
        private int inputItemNum = 0;
        /// <summary>
        /// 工具输出项个数
        /// </summary>
        private int outputItemNum = 0;
        /// <summary>
        /// 本流程所绑定的生产窗口的名称
        /// </summary>
        internal string imageWindowName = "无";
        /// <summary>
        /// 流程结果图像所绑定的窗体
        /// </summary>
        internal string debugImageWindow = ("图像");
        /// <summary>
        /// 编辑节点前节点文本，用于修改工具名称
        /// </summary>
        private string nodeTextBeforeEdit = string.Empty;
        /// <summary>
        /// 流程编辑时的右击菜单
        /// </summary>
        internal static ContextMenuStrip rightClickMenu = new ContextMenuStrip();
        /// <summary>
        /// 在空白除右击菜单
        /// </summary>
        public static ContextMenuStrip rightClickMenuAtBlank = new ContextMenuStrip();
        /// <summary>
        /// 流程名
        /// </summary>
        internal string jobName = string.Empty;

        internal double elapsedTime = 0;
        internal JobRunMode jobRunMode = JobRunMode.RunAfterCall;
        /// <summary>
        /// 工具对象集合
        /// </summary>
        internal List<ToolInfo> L_toolList = new List<ToolInfo>();
        /// <summary>
        /// 记录本工具执行完的耗时，用于计算各工具耗时
        /// </summary>
        private double recordElapseTime = 0;

        /// <summary>
        /// 工具编号，此参数的意义在于当流程为机械手定位类流程时，如机械手上安装了多个工具（如吸嘴或夹爪）时，要通过此参数指定工具编号
        /// </summary>
        internal int toolIdx = 1;

        /// <summary>
        /// 获取当前流程所对应的流程树对象
        /// </summary>
        /// <param name="jobName">流程名</param>
        /// <returns>流程树控件对象</returns>
        internal static TaskEdit GetJobTreeStatic()
        {
            try
            {
                if (Win_Job.Instance.listView1.Nodes.Count != 0)
                {
                    return (TaskEdit)(Win_Job.Instance.tbc_jobs.Controls[0]);
                }
                return new TaskEdit();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
                return new TaskEdit();
            }
        }
        /// <summary>
        /// 获取当前流程所对应的流程树对象
        /// </summary>
        /// <param name="jobName">流程名</param>
        /// <returns>流程树控件对象</returns>
        internal TaskEdit GetJobTree()
        {
            try
            {
               
                if (Win_Job.Instance.tbc_jobs.Controls.Count != 0)
                {
                    return (TaskEdit)(Win_Job.Instance.tbc_jobs.Controls[0]);
                }
                return new TaskEdit();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
                return new TaskEdit();
            }
        }
        /// <summary>
        /// 更新工具树图标
        /// </summary>
        /// <param name="jobName">流程名</param>
        internal static void UpdateJobTreeIcon(string jobName)
        {
            try
            {
                TaskEdit taskEdit = GetJobTreeStatic();
                if (taskEdit.ImageList == null)
                {
                    taskEdit.ImageList = Win_ToolBox.Instance.imageList;
                }
                //更新工具树图标
                for (int j = 0; j < taskEdit.Nodes.Count; j++)
                {
                    ToolType ty = Job.FindJobByName(jobName).FindToolInfoByName(taskEdit.Nodes[j].Text).toolType;
                    int iIndex = (int)ty;
                    ////Hzy20220721,图标与数据结构不对应，需要确认Index
                    //if (iIndex >= 15 && iIndex <= 28)
                    //{
                    //    iIndex = iIndex + 1;
                    //}
                    //if (iIndex == 31)
                    //{
                    //    iIndex = 11;
                    //}
                    GetJobTreeStatic().Nodes[j].ImageIndex = GetJobTreeStatic().Nodes[j].SelectedImageIndex = iIndex;

                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        /// <summary>
        /// 通过流程名获取窗体
        /// </summary>
        /// <param name="jobName">流程名</param>
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
                            if (Machine.machineRunStatu != MachineRunStatu.Running)
                            {
                                item.Value.Show();//切换到当前图像窗体
                            }
                            return item.Value;
                        }
                    }
                    return Win_ImageWindow.HDisplayCtrs["0"];
                }
                else
                {
                    foreach (var item in Start.UserImage)
                    {
                        if (item.Name == Job.FindJobByName(jobName).imageWindowName)
                        {
                            return item as Lxc.VisionPlus.ImageView.ImgView;
                        }
                    }
                    return Start.UserImage[0];
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
                return null;
            }
        }

        /// <summary>
        /// 判断流程是否已经存在输出工具，一个流程只能含有一个输出工具
        /// </summary>
        /// <returns></returns>
        internal bool ExistOutputTool()
        {
            try
            {
                for (int i = 0; i < L_toolList.Count; i++)
                {
                    if (L_toolList[i].toolType == ToolType.Output)
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
                return false;
            }
        }
        /// <summary>
        /// 判断TaskEdit是否已经包含某节点
        /// </summary>
        /// <param name="key">节点文本</param>
        /// <returns>是否包含</returns>
        private bool CheckTreeViewContainsKey(string key)
        {
            try
            {
                foreach (TreeNode node in this.GetJobTree().Nodes)
                {
                    if (node.Text == key)
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
                return false;
            }
        }
        /// <summary>
        /// 放弃重命名
        /// </summary>
        private void GiveupRename(object obj)
        {
            try
            {
                Thread.Sleep(20);
                this.GetJobTree().SelectedNode.Text = nodeTextBeforeEdit;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        /// <summary>
        /// 通过工具名获取工具信息
        /// </summary>
        /// <param name="toolName">工具名</param>
        /// <returns>工具信息</returns>
        internal ToolInfo FindToolInfoByName(string toolName)
        {
            try
            {
                for (int i = 0; i < L_toolList.Count; i++)
                {
                    if (L_toolList[i].toolName == toolName)
                    {
                        return L_toolList[i];
                    }
                }
                return new ToolInfo();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
                return new ToolInfo();
            }
        }


        /// <summary>
        /// 通过输出项字符串获取输出项的值
        /// </summary>
        /// <param name="outputItem">输出项字符串</param>
        /// <returns>输出项的值</returns>
        public object GetOutputItemValue(string outputItem)
        {
            try
            {
                //寻找输出工具
                for (int i = 0; i < L_toolList.Count; i++)
                {
                    if (L_toolList[i].toolType == ToolType.Output)
                    {
                        for (int j = 0; j < L_toolList[i].input.Count; j++)
                        {
                            if (L_toolList[i].input[j].IOName == outputItem)
                            {
                                return L_toolList[i].GetInput(outputItem).value;
                            }
                        }
                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
                return string.Empty;
            }
        }
        /// <summary>
        /// 获取指定工具输出项的值
        /// </summary>
        /// <param name="outputItem">输出项字符串</param>
        /// <returns>输出项的值</returns>
        public object GetToolOutputItemValue(string outputItem)
        {
            try
            {
                string toolName = Regex.Split(outputItem, " . -->")[0];
                string itemName = Regex.Split(outputItem, " . -->")[1];
                //寻找输出工具
                for (int i = 0; i < L_toolList.Count; i++)
                {
                    if (L_toolList[i].toolName == toolName)
                    {
                        for (int j = 0; j < L_toolList[i].output.Count; j++)
                        {
                            if (L_toolList[i].output[j].IOName == itemName)
                            {
                                return L_toolList[i].GetOutput(itemName).value;
                            }
                        }
                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
                return string.Empty;
            }
        }
        /// <summary>
        /// 获取指定工具输出项的值及类型
        /// </summary>
        /// <param name="outputItem">输出项字符串</param>
        /// <returns>输出项的值</returns>
        public bool GetToolOutputItemValueAndType(string outputItem, out object Object, out DataType dataType, out ToolType toolType)
        {
            Object = new object();
            dataType = new DataType();
            toolType = new ToolType(); 
            try
            {
                string toolName = outputItem.Split('.')[0];
                string itemName = outputItem.Split('.')[1];

                //寻找输出工具
                for (int i = 0; i < L_toolList.Count; i++)
                {
                    if (L_toolList[i].toolName == toolName) 
                    {
                        toolType = L_toolList[i].toolType;   
                        for (int j = 0; j < L_toolList[i].output.Count; j++)
                        {
                            if (L_toolList[i].output[j].IOName == itemName)
                            {
                                Object = L_toolList[i].GetOutput(itemName).value;
                                dataType = L_toolList[i].output[j].ioType;
                                return true;
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
                return false;
            }
        }
        //添加指定工具输出到输出集合
        public bool AddToolOut2List(string outPutItem, int IconIndex)
        {
            object Object;
            DataType dataType;
            ToolType toolType;
            if (GetToolOutputItemValueAndType(outPutItem, out Object, out dataType,out toolType))
            {
                OutPutList.Add(new OutT(Object, dataType, outPutItem, IconIndex, toolType));
                return true;
            }
            return false;
        }
        //从输出集合移除指定工具输出
        public bool RemoveToolOutFromList(int Index)
        {
            try
            {
                OutPutList.RemoveAt(Index);
                return true;
            }
            catch (Exception)
            {
                return false;

            }

        }
        /// <summary>
        /// 获取工具输入项的个数
        /// </summary>
        private int GetInputItemNum(TreeNode toolNode)
        {
            try
            {
                int num = 0;
                foreach (TreeNode item in toolNode.Nodes)
                {
                    if (item.Text.Substring(0, 3) == "<--")
                    {
                        num++;
                    }
                }
                return num;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
                return 0;
            }
        }
        /// <summary>
        /// 修改工具说明                
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifyTipInfo(object sender, EventArgs e)
        {
            try
            {
                Win_InputMessage Win_inputMessage = new Win_InputMessage();
                Win_inputMessage.XXXXXXXXXXXXXlbl_title.Text = ("请输入工具说明信息");
                Win_inputMessage.btn_confirm.Text = ("确定");
                Win_inputMessage.txt_input.TextStr = FindToolInfoByName(this.GetJobTree().SelectedNode.Text).toolTipInfo;
                Win_inputMessage.TopMost = true;
                Win_inputMessage.ShowDialog();
                if (Win_InputMessage.Instance.input != string.Empty)
                {
                    FindToolInfoByName(this.GetJobTree().SelectedNode.Text).toolTipInfo = Win_InputMessage.Instance.input;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        /// <summary>
        /// 把节点文本添加到剪切板，用于复制粘贴输出项文本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyNodeText(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetDataObject(this.GetJobTree().SelectedNode.Text);
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        /// <summary>
        /// 生成XYU集合的提示信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string FormatShowTip(List<XYU> list)
        {
            try
            {
                string result = string.Empty;
                for (int i = 0; i < list.Count; i++)
                {
                    result += string.Format("{0}  {1} | {2} | {3}\r\n", (i + 1), list[i].Point.X.ToString("000.000"), list[i].Point.Y.ToString("000.000"), list[i].U.ToString("000.000"));
                }
                return result;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
                return string.Empty;
            }
        }
        /// <summary>
        /// 生成Point集合的提示信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string FormatShowTip(object value)
        {
            try
            {
                if (value == null)
                    return "空";

                string temp = value.ToString();
                string result = string.Empty;
                switch (temp)
                {
                    case "VSE.Core.XYU":
                        XYU xyu = value as XYU;
                        result = string.Format("{0} {1} {2}", xyu.Point.X, xyu.Point.Y, xyu.U);
                        return result;

                    case "HObject":
                        return string.Empty;
                    case "VSE.Core.XY":
                        XY point = value as XY;

                        result = string.Format("{0}  {1}", point.X.ToString("0000.000"), point.Y.ToString("0000.000"));
                        return result;
                    case "Double":
                        break;
                    case "HRegion":
                    case "HalconDotNet.HObject":
                        result = "图形变量";
                        return result;
                    case "HalconDotNet.HImage":
                        result = "图形变量";
                        return result;
                    case "System.Collections.Generic.List`1[VSE.Core.XY]":
                       
                        List<XY> L_point = value as List<XY>;

                        for (int i = 0; i < L_point.Count; i++)
                        {
                            result += string.Format("{0} |  {1}  {2}\r\n", (i + 1), L_point[i].X.ToString("0000.000"), L_point[i].Y.ToString("0000.000"));
                        }
                        return result;
                    case "System.Collections.Generic.List`1[VSE.Core.XYU]":
                        
                        List<XYU> L_xyu = value as List<XYU>;
                        result = string.Format("NO|  X              Y             U\r\n");
                        for (int i = 0; i < L_xyu.Count; i++)
                        {
                            result += string.Format("{0}   |  {1}  {2}  {3}\r\n", (i + 1), L_xyu[i].Point.X.ToString("0000.000"), L_xyu[i].Point.Y.ToString("0000.000"), L_xyu[i].U.ToString("0000.000"));
                        }
                        return result;
                    case "VSE.Core.Line":
                        Line line = value as Line;
                        result = string.Format("(线 起点XY：{0},{1}) | 终点XY：({2},{3})", line.起点.X, line.起点.Y, line.终点.X, line.终点.Y);
                        return result;
                    case "VSE.Core.Circle":
                        Circle circle = value as Circle;
                        result = string.Format("(圆XYR:{0},{1},{2})", circle.圆心.X, circle.圆心.Y, circle.半径);
                        return result;
                    default:
                        result = value.ToString();
                        return result;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
                return string.Empty;
            }
        }


        #region 绘制节点连线

        internal void tvw_job_AfterSelect(object sender, TreeViewEventArgs e)
        {
            nodeTextBeforeEdit = Job.GetJobTreeStatic().SelectedNode.Text;
            Job.GetJobTreeStatic().Update();
            // DrawLine();
        }

        /// <summary>
        /// 生成新工具的名称
        /// </summary>
        /// <param name="toolName">工具类型</param>
        /// <returns>工具名称</returns>
        internal string GetNewToolName(string toolType)
        {
            try
            {
                if (!CheckTreeViewContainsKey(toolType))
                {
                    return toolType;
                }
                for (int i = 1; i < 101; i++)
                {
                    if (!CheckTreeViewContainsKey(toolType + "_" + i))
                    {
                        return toolType + "_" + i;
                    }
                }
                Win_MessageBox.Instance.MessageBoxShow("\r\n此工具已添加个数已达到数量上限，无法继续添加", TipType.Error);
                return "TooMuch";
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
                return "Error";
            }
        }

        #endregion

        #region 空白处右击菜单

        void toolStripItem_展开流程树_Click(object sender, EventArgs e)
        {
            try
            {
                if (Win_Job.Instance.tbc_jobs.Controls.Count != 0)
                {
                    string jobName = Win_Job.Instance.JobEdit_JobName.Text.Substring(5);
                    Job job = Job.FindJobByName(jobName);
                    Job.GetJobTreeStatic().ExpandAll();
                }
                return;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);

            }

        }
        void toolStripItem_折叠流程树_Click(object sender, EventArgs e)
        {
            try
            {
                if (Win_Job.Instance.tbc_jobs.Controls.Count != 0)
                {
                    string jobName = Win_Job.Instance.JobEdit_JobName.Text.Substring(5);
                    Job job = Job.FindJobByName(jobName);
                    Job.GetJobTreeStatic().CollapseAll();
                    //job.DrawLine();
                }
                return;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        void toolStripItem_启用全部_Click(object sender, EventArgs e)
        {
            try
            {
                if (Win_Job.Instance.tbc_jobs.Controls.Count == 0)
                {
                    return;
                }

                string jobName = Win_Job.Instance.JobEdit_JobName.Text.Substring(5);


                List<ToolInfo> toolList = FindJobByName(jobName).L_toolList;
                for (int i = 0; i < toolList.Count; i++)
                {
                    toolList[i].enable = true;
                }

                foreach (TreeNode item in GetJobTree().Nodes)
                {
                    item.ForeColor = Color.FromArgb(224, 224, 224);

                }
                GetJobTree().SelectedNode = null;
                Win_Log.Instance.OutputMsg("已启用当前流程中的所有工具", Win_Log.InfoType.tip);
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        void toolStripItem_忽略全部_Click(object sender, EventArgs e)
        {
            try
            {
                if (Win_Job.Instance.tbc_jobs.Controls.Count == 0)
                {
                    return;
                }
                string jobName = Win_Job.Instance.JobEdit_JobName.Text.Split('：')[1];

                List<ToolInfo> toolList = FindJobByName(jobName).L_toolList;
                for (int i = 0; i < toolList.Count; i++)
                {
                    toolList[i].enable = false;
                }

                foreach (TreeNode item in GetJobTree().Nodes)
                {
                    item.ForeColor = Color.DarkGray;
                }
                GetJobTree().SelectedNode = null;
                Win_Log.Instance.OutputMsg("已禁用当前流程中的所有工具", Win_Log.InfoType.tip);
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        void toolStripItem_删除流程_Click(object sender, EventArgs e)
        {
            DeleteJob();
        }
        void toolStripItem_流程属性_Click(object sender, EventArgs e)
        {
            Win_Job.Instance.tsb_jobInfo_Click(null, null);
        }

        #endregion
        public static event EventHandler FlushJobList;
        public static void OnFlushList()
        {
            if (FlushJobList != null)
            {
                FlushJobList(null, null);
            }
        }
        #region 流程操作

        /// <summary>
        /// 添加新流程
        /// </summary>
        internal static void CreateJob()
        {
            try
            {

                if (Project.Instance.L_engineList.Count == 0)
                {
                    Win_MessageBox.Instance.MessageBoxShow("\r\n当前项目未创建任何方案，请先创建方案");
                    Scheme.CreateScheme();
                    return;
                }

            Again:
                Win_InputMessage.Instance.XXXXXXXXXXXXXlbl_title.Text = ("请输入新流程名");
                Win_InputMessage.Instance.btn_confirm.Text = ("确定");

                Win_InputMessage.Instance.txt_input.DefaultText = "请输入新流程名";
                Win_InputMessage.Instance.txt_input.TextStr = string.Empty;

                Win_InputMessage.Instance.StartPosition = FormStartPosition.Manual;
                Win_InputMessage.Instance.Location = new Point(Win_Job.Instance.PointToScreen(Win_Job.Instance.ToolBoxEdit_ProjectName.Location).X, Win_Job.Instance.PointToScreen(Win_Job.Instance.ToolBoxEdit_ProjectName.Location).Y + 80);
                ;
                Win_InputMessage.Instance.ShowDialog();
                string jobName = Win_InputMessage.Instance.input;
                if (jobName == string.Empty)
                    return;

                //检查此名称的流程是否已存在
                if (CheckJobExist(jobName))
                {
                    Win_MessageBox.Instance.MessageBoxShow(("\r\n已存在此名称的流程，流程名不可重复，请重新输入"));
                    goto Again;
                }
                //检查此名称是否含有特殊字符\
                if (jobName.Contains(@"\"))
                {
                    Win_MessageBox.Instance.MessageBoxShow(("\r\n流程名中不能含有 \\ 等特殊字符 ，请重新输入"));
                    goto Again;
                }
                Win_Log.Instance.OutputMsg(string.Format("创建了新流程，流程名为：{0}", jobName), Win_Log.InfoType.tip);

                Job job = new Job();
                HWindowControl dd = new HWindowControl();
                Win_Main.d.Add(dd);
                job.www = dd.HalconID;
                job.jobName = jobName;
                Project.Instance.curEngine.L_jobList.Add(job);
                if (FlushJobList != null)
                {
                    FlushJobList(null, null);
                }
                TaskEdit tvw_job = new TaskEdit
                {
                    isDrawing = true,
                    Scrollable = true,
                    ItemHeight = 26,
                    ShowLines = false,
                    AllowDrop = true,
                    ImageList = Win_ToolBox.Instance.imageList,
                    BackColor = VControls.VUI.WinBackColor,
                    ForeColor = Color.FromArgb(224, 224, 224)

                };
                
                tvw_job.AfterSelect += job.tvw_job_AfterSelect;
                tvw_job.AfterLabelEdit += new NodeLabelEditEventHandler(job.EditNodeText);
                tvw_job.MouseClick += new MouseEventHandler(job.TVW_MouseClick);
                tvw_job.MouseDoubleClick += new MouseEventHandler(job.TVW_DoubleClick);

                //节点间拖拽事件
                tvw_job.ItemDrag += new ItemDragEventHandler(job.tvw_job_ItemDrag);
                tvw_job.DragEnter += new DragEventHandler(job.tvw_job_DragEnter);
                tvw_job.DragDrop += new DragEventHandler(job.tvw_job_DragDrop);
                tvw_job.Dock = DockStyle.Fill;
                tvw_job.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                Win_Job.Instance.tbc_jobs.Controls.Clear();
                Win_Job.Instance.tbc_jobs.Controls.Add(tvw_job);
                Win_Job.Instance.JobEdit_JobName.Text = "当前流程：" + jobName;
                Win_Job.Instance.ToolBoxEdit_ProjectName.Text = Win_Job.Instance.ToolBoxEdit_ProjectName.Text.Split('-')[0] + "--" + jobName;
                Application.DoEvents();

                //默认添加Halcon采集接口工具
                Win_ToolBox.Instance.AddTool("图像采集", null);

                //默认选中第一个工具节点
                tvw_job.SelectedNode = tvw_job.Nodes[0];

                //展开已默认添加的工具的输入输出项
                tvw_job.ExpandAll();

                //添加此流程的系统变量
                Variable variable1 = new Variable(Project.Instance.curEngine.L_jobList.Count * 2 - 1, "String", string.Format("流程[{0}].运行状态", jobName))
                {
                    variableType = 0
                };
                Project.Instance.curEngine.globelVariable.AddGlobalVariableValue(variable1);
                Variable variable2 = new Variable(Project.Instance.curEngine.L_jobList.Count * 2, "Double", string.Format("流程[{0}].运行时间", jobName))
                {
                    variableType = 0
                };
                Project.Instance.curEngine.globelVariable.AddGlobalVariableValue(variable2);
                Win_Monitor.UpdateGlobelVariablelist(true);
                tvw_job.isDrawing = false;

                //hzy20221212,刷新字典集合
                if (Project.Instance.curEngine.L_jobList.Count > 0)
                {
                    List<string> list = new List<string>();
                    for (int i = 0; i < Project.Instance.curEngine.L_jobList.Count; i++)
                    {
                        list.Add(Project.Instance.curEngine.L_jobList[i].jobName);
                    }
                    if(!list.Contains(jobName))
                    {
                        list.Add(jobName);
                    }
                    string[] strJobNmaeList = list.ToArray();
                    Start.OnUpdateJobNameList(new JobNameListEventArgs(strJobNmaeList));
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        /// <summary>
        /// 从本地加载流程到程序中
        /// </summary>
        /// <param name="path">流程文件路径</param>
        public static Job LoadJob(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    Win_Log.Instance.OutputMsg("\r\n流程文件不存在", Win_Log.InfoType.error);
                    return null;
                }

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
                Job job = (Job)formatter.Deserialize(stream);
                stream.Close();

                foreach (TreeNode item in Win_Job.Instance.listView1.Nodes)
                {
                    if (item.Text == job.jobName)
                    {
                        Win_Log.Instance.OutputMsg("\r\n已经存在与此流程名相同的流程，请勿重复添加", Win_Log.InfoType.error);
                        return new Job();
                    }
                }
                job.isRunLoop = false;
                Project.Instance.curEngine.L_jobList.Add(job);

                TaskEdit tvw_job = new TaskEdit
                {
                    Scrollable = true,
                    ItemHeight = 26,
                    ShowLines = false,
                    AllowDrop = true,
                    ImageList = Win_ToolBox.Instance.imageList,
                    TabStop = false,
                    //tvw_job.ShowNodeToolTips = true;
                    BackColor = VControls.VUI.WinBackColor,
                    ForeColor = Color.FromArgb(224, 224, 224)
                };

                
                tvw_job.AfterSelect += job.tvw_job_AfterSelect;
                tvw_job.AfterLabelEdit += new NodeLabelEditEventHandler(job.EditNodeText);
                tvw_job.MouseClick += new MouseEventHandler(job.TVW_MouseClick);
                tvw_job.MouseDoubleClick += new MouseEventHandler(job.TVW_DoubleClick);
                tvw_job.AfterSelect += new TreeViewEventHandler(job.TVW_AfterSelect);

                //节点间拖拽
                tvw_job.ItemDrag += new ItemDragEventHandler(job.tvw_job_ItemDrag);
                tvw_job.DragEnter += new DragEventHandler(job.tvw_job_DragEnter);
                tvw_job.DragDrop += new DragEventHandler(job.tvw_job_DragDrop);
                tvw_job.MouseDown += new MouseEventHandler(job.tvw_tools_MouseDown);

                Win_Job.Instance.tbc_jobs.Controls.Clear();
                Win_Job.Instance.tbc_jobs.Controls.Add(tvw_job);
                Win_Job.Instance.JobEdit_JobName.Text = "当前流程：" + job.jobName;
                Win_Job.Instance.ToolBoxEdit_ProjectName.Text = Win_Job.Instance.ToolBoxEdit_ProjectName.Text.Split('-')[0] + "--" + job.jobName;
                tvw_job.Dock = DockStyle.Fill;
                tvw_job.ShowNodeToolTips = true;
                tvw_job.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                for (int i = 0; i < job.L_toolList.Count; i++)
                {
                    TreeNode node = GetJobTreeStatic().Nodes.Add(job.L_toolList[i].toolName);
                    for (int j = 0; j < job.L_toolList[i].input.Count; j++)
                    {
                        TreeNode treeNode;
                        //因为OutputBox只有源，所以此处特殊处理
                        if (job.L_toolList[i].toolType != ToolType.Output)
                            treeNode = node.Nodes.Add(job.L_toolList[i].input[j].IOName + job.L_toolList[i].input[j].value);
                        else
                            treeNode = node.Nodes.Add(job.L_toolList[i].input[j].IOName);

                        treeNode.Tag = job.L_toolList[i].input[j].ioType;
                        treeNode.ForeColor = Color.LightPink;


                    }
                    for (int k = 0; k < job.L_toolList[i].output.Count; k++)
                    {
                        TreeNode treeNode = node.Nodes.Add(job.L_toolList[i].output[k].IOName);
                        treeNode.Tag = job.L_toolList[i].output[k].ioType;
                        treeNode.ForeColor = Color.LightGreen;
                    }
                }

                UpdateJobTreeIcon(job.jobName);

                //默认选中第一个节点
                if (tvw_job.Nodes.Count > 0)
                    tvw_job.SelectedNode = tvw_job.Nodes[0];
                return job;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
                return null;
            }
        }
        public int m_MouseClicks = 0; //记录鼠标在myTreeView控件上按下的次数
        /// <summary>
        /// 加载指定的流程
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public static Job LoadJob(Job job)
        {
            try
            {
                job.isRunLoop = false;

                TaskEdit tvw_job = new TaskEdit
                {
                    Scrollable = true,
                    ItemHeight = 26,
                    ShowLines = false,
                    AllowDrop = true,
                    ImageList = Win_ToolBox.Instance.imageList,
                    TabStop = false,
                    //tvw_job.ShowNodeToolTips = true;
                    BackColor = VControls.VUI.WinBackColor,
                    ForeColor = Color.FromArgb(224, 224, 224)
                };

                tvw_job.AfterSelect += job.tvw_job_AfterSelect;
                tvw_job.AfterLabelEdit += new NodeLabelEditEventHandler(job.EditNodeText);
                tvw_job.MouseClick += new MouseEventHandler(job.TVW_MouseClick);
                tvw_job.MouseDoubleClick += new MouseEventHandler(job.TVW_DoubleClick);
                tvw_job.AfterSelect += new TreeViewEventHandler(job.TVW_AfterSelect);

                tvw_job.MouseDown += job.tvw_job_MouseDown;
                tvw_job.BeforeCollapse += job.tvw_job_BeforeCollapse;
                tvw_job.BeforeExpand += job.tvw_job_BeforeExpand;

                //节点间拖拽
                tvw_job.ItemDrag += new ItemDragEventHandler(job.tvw_job_ItemDrag);
                tvw_job.DragEnter += new DragEventHandler(job.tvw_job_DragEnter);
                tvw_job.DragDrop += new DragEventHandler(job.tvw_job_DragDrop);
                tvw_job.MouseDown += new MouseEventHandler(job.tvw_tools_MouseDown);

                Win_Job.Instance.tbc_jobs.Controls.Clear();

                Win_Job.Instance.tbc_jobs.Controls.Add(tvw_job);
                Win_Job.Instance.JobEdit_JobName.Text = "当前流程：" + job.jobName;

                Win_Job.Instance.ToolBoxEdit_ProjectName.Text = Win_Job.Instance.ToolBoxEdit_ProjectName.Text.Split('-')[0] + "--" + job.jobName;
                tvw_job.Dock = DockStyle.Fill;
                tvw_job.ShowNodeToolTips = true;
                tvw_job.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                
                //遍历Job内所有工具
                for (int i = 0; i < job.L_toolList.Count; i++)
                {
                    TreeNode node = GetJobTreeStatic().Nodes.Add(job.L_toolList[i].toolName);
                    for (int j = 0; j < job.L_toolList[i].input.Count; j++)
                    {
                        TreeNode treeNode;
                        //因为OutputBox只有源，所以此处特殊处理
                        if (job.L_toolList[i].toolType != ToolType.Output)
                            treeNode = node.Nodes.Add(job.L_toolList[i].input[j].IOName + job.L_toolList[i].input[j].value);
                        else
                            treeNode = node.Nodes.Add(job.L_toolList[i].input[j].IOName);

                        treeNode.Tag = job.L_toolList[i].input[j].ioType;
                        treeNode.ForeColor = Color.LightPink;


                        if (job.L_toolList[i].toolType == ToolType.Output)
                        {
                            treeNode.ImageIndex = treeNode.SelectedImageIndex = 3;
                        }
                        else
                        {
                            treeNode.ImageIndex = treeNode.SelectedImageIndex = 2;
                        }
                    }
                    for (int k = 0; k < job.L_toolList[i].output.Count; k++)
                    {
                        TreeNode treeNode = node.Nodes.Add(job.L_toolList[i].output[k].IOName);
                        treeNode.ImageIndex = treeNode.SelectedImageIndex = 3;
                        treeNode.Tag = job.L_toolList[i].output[k].ioType;
                        treeNode.ForeColor = Color.LightGreen;
                    }
                }

                UpdateJobTreeIcon(job.jobName);

                //默认选中第一个节点
                if (tvw_job.Nodes.Count > 0)
                    tvw_job.SelectedNode = tvw_job.Nodes[0];

                return job;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
                return null;
            }
        }

        void tvw_job_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = (this.m_MouseClicks > 1);
        }

        void tvw_job_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = (m_MouseClicks > 1);
        }

        void tvw_job_MouseDown(object sender, MouseEventArgs e)
        {
            m_MouseClicks = e.Clicks;
        }

        //static object obj221 = new object();

        /// <summary>
        /// 运行当前流程
        /// </summary>
        internal static void RunCurJob()
        {
            try
            {
                Start.JobRunMarkM = false;
                Start.JobRunMark1 = false;

                Application.DoEvents();
                if (Project.Instance.L_engineList.Count == 0)
                {
                    Win_Main.Instance.OutputMsg("当前项目中未添加任何方案，请先新建方案", Win_Log.InfoType.tip);
                    return;
                }

                if (Project.Instance.curEngine.L_jobList.Count == 0)
                {
                    Win_Main.Instance.OutputMsg("当前方案中未添加任何流程，请先新建流程", Win_Log.InfoType.tip);
                    return;
                }

                //Win_Job.Instance.btn_runOnce.Enabled = false;//控件异常，报错

                //hzy20220517,取消运行当前Job选择项
                //if (Win_Job.Instance.listView1.SelectedNode == null)
                //{
                //    Job.FindJobByName(Win_Job.Instance.JobEdit_JobName.Text.Substring(5)).Run(); ;
                //}
                //else
                //{
                //    //针对所选项单独运行对应Job
                //    Job.FindJobByName(Win_Job.Instance.JobEdit_JobName.Text.Substring(5)).Run();
                //}

                //hzy20220517,增加，Job list内所有job都运行一次
                //System.Windows.Forms.TaskList, Nodes.Count: 2, Nodes[0]: TreeNode: 模切检测_缺陷
                int iJobCount = Win_Job.Instance.listView1.Nodes.Count;
                if (iJobCount > 0)
                {
                    List<Job> JobListWithCamera = new List<Job>();//有相机Job集合
                    List<Job> JobListWithoutCamera = new List<Job>();//没有相机Job集合
                    JobListWithCamera.Clear();
                    JobListWithoutCamera.Clear();
                    for (int i = 0; i < iJobCount; i++)
                    {
                        string strJobName = Win_Job.Instance.listView1.Nodes[i].Text;
                        int iCamCount = 0; //Job中取像工具个数
                        for (int j = 0; j < Job.FindJobByName(strJobName).L_toolList.Count; j++)
                        {
                            if (Job.FindJobByName(strJobName).L_toolList[j].toolType == ToolType.ImageAcq)
                            {
                                if (Job.FindJobByName(strJobName).L_toolList[j].enable)
                                {
                                    iCamCount++;
                                }
                                else
                                {
                                    //当有相机，但相机禁用，视为没有相机
                                }
                            }
                        }
                        if (iCamCount > 0)
                        {
                            JobListWithCamera.Add(Job.FindJobByName(strJobName));
                        }
                        else
                        {
                            JobListWithoutCamera.Add(Job.FindJobByName(strJobName));
                        }
                    }
                    if (JobListWithCamera.Count > 0)
                    {
                        for (int i = 0; i < JobListWithCamera.Count; i++)
                        {
                            JobListWithCamera[i].Run();
                        }
                        Thread.Sleep(100);
                        if (JobListWithoutCamera.Count > 0)
                        {
                            for (int j = 0; j < JobListWithoutCamera.Count; j++)
                            {
                                JobListWithoutCamera[j].Run();
                            }
                        }
                    }
                    else
                    {
                        Win_MessageBox.Instance.MessageBoxShow("当前主Job或从Job内均没有或都禁用取像工具，请创建或启用取像工具！");
                    }
                }

                Win_Job.Instance.btn_runOnce.Enabled = true;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        /// <summary>
        /// 通过流程名获取流程
        /// </summary>
        /// <param name="jobName">流程名</param>
        /// <returns>流程</returns>
        public static Job FindJobByName(string jobName)
        {
            try
            {
                for (int i = 0; i < Project.Instance.curEngine.L_jobList.Count; i++)
                {
                    if ((Project.Instance.curEngine.L_jobList[i]).jobName == jobName)
                        return Project.Instance.curEngine.L_jobList[i];
                }
                Win_MessageBox.Instance.MessageBoxShow("未找到名为" + jobName + "的流程（错误代码：00001）");
                return null;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
                return null;
            }
        }

        /// <summary>
        /// 判断是否已经存在此名称的流程
        /// </summary>
        /// <param name="jobName">流程名</param>
        /// <returns>是否已存在</returns>
        internal static bool CheckJobExist(string jobName)
        {
            try
            {
                for (int i = 0; i < Project.Instance.curEngine.L_jobList.Count; i++)
                {
                    if ((Project.Instance.curEngine.L_jobList[i]).jobName == jobName)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
                return true;
            }
        }

        /// <summary>
        /// 导入流程
        /// </summary>
        /// <param name="job">流程对象</param>
        internal static void InportJob(Job job)
        {
            try
            {
                TaskEdit tvw_job = new TaskEdit
                {
                    Scrollable = true,
                    ItemHeight = 26,
                    ShowLines = false,
                    AllowDrop = true,
                    ImageList = Win_ToolBox.Instance.imageList,
                    TabStop = false,
                    //ShowNodeToolTips = true,
                    BackColor = VControls.VUI.WinBackColor,
                    ForeColor = Color.FromArgb(224, 224, 224)
                };

                tvw_job.AfterSelect += job.tvw_job_AfterSelect;
                tvw_job.AfterLabelEdit += new NodeLabelEditEventHandler(job.EditNodeText);
                tvw_job.MouseClick += new MouseEventHandler(job.TVW_MouseClick);
                tvw_job.MouseDoubleClick += new MouseEventHandler(job.TVW_DoubleClick);
                tvw_job.AfterSelect += new TreeViewEventHandler(job.TVW_AfterSelect);

                tvw_job.MouseDown += job.tvw_job_MouseDown;
                tvw_job.BeforeCollapse += job.tvw_job_BeforeCollapse;
                tvw_job.BeforeExpand += job.tvw_job_BeforeExpand;

                //节点间拖拽
                tvw_job.ItemDrag += new ItemDragEventHandler(job.tvw_job_ItemDrag);
                tvw_job.DragEnter += new DragEventHandler(job.tvw_job_DragEnter);
                tvw_job.DragDrop += new DragEventHandler(job.tvw_job_DragDrop);
                tvw_job.MouseDown += new MouseEventHandler(job.tvw_tools_MouseDown);

                Win_Job.Instance.tbc_jobs.Controls.Clear();
                Win_Job.Instance.tbc_jobs.Controls.Add(tvw_job);
                Win_Job.Instance.JobEdit_JobName.Text = "当前流程：" + job.jobName;

                Win_Job.Instance.ToolBoxEdit_ProjectName.Text = Win_Job.Instance.ToolBoxEdit_ProjectName.Text.Split('-')[0] + "--" + job.jobName;
                tvw_job.Dock = DockStyle.Fill;
                tvw_job.ShowNodeToolTips = true;
                tvw_job.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));



                for (int i = 0; i < job.L_toolList.Count; i++)
                {
                    TreeNode node = GetJobTreeStatic().Nodes.Add(job.L_toolList[i].toolName);
                    for (int j = 0; j < job.L_toolList[i].input.Count; j++)
                    {
                        TreeNode treeNode;
                        //因为OutputBox只有源，所以此处特殊处理
                        if (job.L_toolList[i].toolType != ToolType.Output)
                            treeNode = node.Nodes.Add(job.L_toolList[i].input[j].IOName + job.L_toolList[i].input[j].value);
                        else
                            treeNode = node.Nodes.Add(job.L_toolList[i].input[j].IOName);

                        treeNode.Tag = job.L_toolList[i].input[j].ioType;
                        treeNode.ForeColor = Color.LightPink;

                        if (job.L_toolList[i].toolType == ToolType.Output)
                        {
                            treeNode.ImageIndex = treeNode.SelectedImageIndex = 3;
                        }
                        else
                        {
                            treeNode.ImageIndex = treeNode.SelectedImageIndex = 2;
                        }
                    }

                    for (int k = 0; k < job.L_toolList[i].output.Count; k++)
                    {
                        TreeNode treeNode = node.Nodes.Add(job.L_toolList[i].output[k].IOName);
                        treeNode.ImageIndex = treeNode.SelectedImageIndex = 3;
                        treeNode.Tag = job.L_toolList[i].output[k].ioType;
                        treeNode.ForeColor = Color.LightGreen;
                    }
                }

                UpdateJobTreeIcon(job.jobName);

                //默认选中第一个节点
                if (tvw_job.Nodes.Count > 0)
                    tvw_job.SelectedNode = tvw_job.Nodes[0];
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        /// <summary>
        /// 导出流程
        /// </summary>
        internal static void ExportJob()
        {
            try
            {
                if (Project.Instance.curEngine.L_jobList.Count > 0)
                {
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string jobName = Win_Job.Instance.JobEdit_JobName.Text.Split('：')[1];
                    System.Windows.Forms.SaveFileDialog dig_saveFileDialog = new System.Windows.Forms.SaveFileDialog
                    {
                        FileName = jobName,
                        Title = ("请选择流程路径"),
                        Filter = "流程文件|*.job",
                        InitialDirectory = path
                    };
                    if (dig_saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        IFormatter formatter = new BinaryFormatter();
                        Stream stream = new FileStream(dig_saveFileDialog.FileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                        formatter.Serialize(stream, Project.Instance.curEngine.FindJobByName(jobName));
                        stream.Close();

                        //更新结果下拉框
                        Win_Main.Instance.OutputMsg("流程导出成功", Win_Log.InfoType.tip);
                    }
                }
                else
                {
                    Win_Main.Instance.OutputMsg("当前方案尚未添加流程，不可导出", Win_Log.InfoType.error);
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        /// <summary>
        /// 克隆当前流程
        /// </summary>
        internal static void CloneJob()
        {
            try
            {
            Again:
                Win_InputMessage.Instance.XXXXXXXXXXXXXlbl_title.Text = ("请输入新流程名");
                Win_InputMessage.Instance.btn_confirm.Text = ("确定");
                Win_InputMessage.Instance.passwordChar = false;
                Win_InputMessage.Instance.txt_input.TextStr = string.Empty;
                Win_InputMessage.Instance.ShowDialog();
                string newJobName = Win_InputMessage.Instance.input;
                if (newJobName != string.Empty)
                {
                    //检查此名称的流程是否已存在
                    if (CheckJobExist(newJobName))
                    {
                        Win_MessageBox.Instance.MessageBoxShow(("\r\n已存在此名称的流程，流程名不可重复，请重新输入"));
                        goto Again;
                    }
                    Win_Log.Instance.OutputMsg("克隆了新流程，流程名为：" + newJobName, Win_Log.InfoType.tip);

                    string sourceJobName = Win_Job.Instance.JobEdit_JobName.Text.Split('：')[1];
                    Job job = ObjectCopier.Clone(Job.FindJobByName(sourceJobName));
                    job.jobName = newJobName;
                    for (int i = 0; i < job.L_toolList.Count; i++)
                    {
                        job.L_toolList[i].tool.jobName = newJobName;
                    }

                    Project.Instance.curEngine.L_jobList.Add(job);
                    LoadJob(job);

                    Win_Main.Instance.SaveAll();
                    Win_Log.Instance.OutputMsg("流程克隆成功", Win_Log.InfoType.tip);
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        public static class ObjectCopier
        {
            public static Job Clone<Job>(Job source)
            {
                if (!typeof(Job).IsSerializable)
                {
                    throw new ArgumentException("The type must be serializable.", "source");
                }

                if (source == null)
                {
                    return default;
                }

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new MemoryStream();
                using (stream)
                {
                    formatter.Serialize(stream, source);
                    stream.Seek(0, SeekOrigin.Begin);
                    return (Job)formatter.Deserialize(stream);
                }
            }
        }
        /// <summary>
        /// 通过流程名从流程集合中移除流程
        /// </summary>
        /// <param name="jobName">流程名</param>
        internal static void RemoveJobByName(string jobName)
        {
            try
            {
                for (int i = 0; i < Project.Instance.curEngine.L_jobList.Count; i++)
                {
                    if (((Job)Project.Instance.curEngine.L_jobList[i]).jobName == jobName)
                    {
                        Project.Instance.curEngine.L_jobList.RemoveAt(i);
                        break;

                    }
                }
                for (int i = 0; i < Project.Instance.curEngine.globelVariable.GetGlobalVariableCount(); i++)
                {

                    if (Project.Instance.curEngine.globelVariable.GetGlobalVariable(i).name.Split(']')[0].Substring(3) == jobName)
                    {
                        Project.Instance.curEngine.globelVariable.RemoveGlobalVariableValueAt(i);
                        i--;

                    }
                }
                Win_Monitor.UpdateGlobelVariablelist(true);
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        /// <summary>
        /// 删除流程
        /// </summary>
        internal static void DeleteJob()
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Developer))
                    return;



                if (Win_Job.Instance.listView1.Nodes.Count < 1)
                    return;

                Win_ConfirmBox.Instance.lbl_info.Text = (string.Format("确定要删除流程 [{0}] 吗？", Win_Job.Instance.JobEdit_JobName.Text.Substring(5)));
                Win_ConfirmBox.Instance.StartPosition = FormStartPosition.Manual;
                Win_ConfirmBox.Instance.Location = new Point(Win_Job.Instance.PointToScreen(Win_Job.Instance.ToolBoxEdit_ProjectName.Location).X, Win_Job.Instance.PointToScreen(Win_Job.Instance.ToolBoxEdit_ProjectName.Location).Y + 80);
                ;

                Win_ConfirmBox.Instance.ShowDialog();
                if (Win_ConfirmBox.Instance.Result == ConfirmBoxResult.Yes)
                {
                    string jobName = Win_Job.Instance.JobEdit_JobName.Text.Substring(5);
                    Job.RemoveJobByName(jobName);
                    Win_Job.Instance.tbc_jobs.Controls.Clear();
                    if (FlushJobList != null)
                    {
                        FlushJobList(null, null);
                    }
                    string BasePath = string.Format(Application.StartupPath + "\\Config\\Project\\Vision\\{0}\\{1}\\{2}\\",
               Project.Instance.configuration.ProgramTitle, Project.Instance.curEngine.schemeName,
               jobName);
                    if (System.IO.Directory.Exists(BasePath))
                    {
                        Directory.Delete(BasePath);
                    }
                    Win_Main.Instance.OutputMsg(string.Format("流程 [{0}] 删除成功", jobName), Win_Log.InfoType.tip);
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        /// <summary>
        /// 设置Job任务里所有相机触发模式
        /// </summary>
        internal static void SetCamerasInJobTriggerMode()
        {
            //hzy20220512,设置相机触发模式
            for (int i = 0; i < Project.Instance.curEngine.L_jobList.Count; i++)
            {
                Job job = Project.Instance.curEngine.L_jobList[i];
                List<ToolBase> listCam = new List<ToolBase>(); //Job中取像工具个数
                listCam.Clear();

                for (int m = 0; m < job.L_toolList.Count; m++)
                {
                    if (job.L_toolList[m].toolType == ToolType.ImageAcq)
                    {
                        listCam.Add(job.FindToolByName(job.L_toolList[m].toolName));
                    }
                }
                if (listCam.Count > 0)
                {
                    for (int n = 0; n < listCam.Count; n++)
                    {
                        ToolBase tool = job.FindToolByName(job.L_toolList[n].toolName);
                        (tool as AcqImageTool).SetCamTriggerModel((tool as AcqImageTool).triggerModel);
                        (tool as AcqImageTool).isLoad = true;
                    }
                }
            }
        }

        /// <summary>
        /// 清除图像队列中所有缓存
        /// </summary>
        internal static void ClearCamerasBufferInJob()
        {
            //hzy20220512,清除图像队列中所有缓存
            for (int i = 0; i < Project.Instance.curEngine.L_jobList.Count; i++)
            {
                Job job = Project.Instance.curEngine.L_jobList[i];
                List<ToolBase> listCam = new List<ToolBase>(); //Job中取像工具个数
                listCam.Clear();

                for (int m = 0; m < job.L_toolList.Count; m++)
                {
                    if (job.L_toolList[m].toolType == ToolType.ImageAcq)
                    {
                        listCam.Add(job.FindToolByName(job.L_toolList[m].toolName));
                    }
                }
                if (listCam.Count > 0)
                {
                    for (int n = 0; n < listCam.Count; n++)
                    {
                        (job.FindToolByName(job.L_toolList[n].toolName) as AcqImageTool).QeHImages.Clear();
                        (job.FindToolByName(job.L_toolList[n].toolName) as AcqImageTool).isLoad = false;
                    }
                }
            }
        }

        internal static void SetCameraExposure()
        {
            //hzy20220512,清除图像队列中所有缓存
            for (int i = 0; i < Project.Instance.curEngine.L_jobList.Count; i++)
            {
                Job job = Project.Instance.curEngine.L_jobList[i];
                List<ToolBase> listCam = new List<ToolBase>(); //Job中取像工具个数
                listCam.Clear();

                for (int m = 0; m < job.L_toolList.Count; m++)
                {
                    if (job.L_toolList[m].toolType == ToolType.ImageAcq)
                    {
                        listCam.Add(job.FindToolByName(job.L_toolList[m].toolName));
                    }
                }
                if (listCam.Count > 0)
                {
                    for (int n = 0; n < listCam.Count; n++)
                    {
                        (job.FindToolByName(job.L_toolList[n].toolName) as AcqImageTool).SetCamExposure();
                    }
                }
            }
        }


        internal static void SetSoftwareLoadMode()
        {
            //hzy20220512,Job运行之前判定软件是否处于加载状态，是，true；否，false；
            for (int i = 0; i < Project.Instance.curEngine.L_jobList.Count; i++)
            {
                Job job = Project.Instance.curEngine.L_jobList[i];
                List<ToolBase> listCam = new List<ToolBase>(); //Job中取像工具个数
                listCam.Clear();

                for (int m = 0; m < job.L_toolList.Count; m++)
                {
                    if (job.L_toolList[m].toolType == ToolType.ImageAcq)
                    {
                        listCam.Add(job.FindToolByName(job.L_toolList[m].toolName));
                    }
                }
                if (listCam.Count > 0)
                {
                    for (int n = 0; n < listCam.Count; n++)
                    {
                        ToolBase tool = job.FindToolByName(job.L_toolList[n].toolName);
                        (tool as AcqImageTool).isLoad = true;
                    }
                }
            }
        }

        internal static string getBufferImagesCount()
        {
            string strCameraNews = "";
            for (int i = 0; i < Project.Instance.curEngine.L_jobList.Count; i++)
            {
                Job job = Project.Instance.curEngine.L_jobList[i];
                List<ToolBase> listCam = new List<ToolBase>(); //Job中取像工具个数
                listCam.Clear();

                for (int m = 0; m < job.L_toolList.Count; m++)
                {
                    if (job.L_toolList[m].toolType == ToolType.ImageAcq)
                    {
                        listCam.Add(job.FindToolByName(job.L_toolList[m].toolName));
                    }
                }
                if (listCam.Count > 0)
                {
                    for (int n = 0; n < listCam.Count; n++)
                    {
                        ToolBase tool = job.FindToolByName(job.L_toolList[n].toolName);
                        strCameraNews += string.Format("Cam{0}:{1}; ", n, (tool as AcqImageTool).BufferCount);
                    }
                }
            }
            return strCameraNews;   

        }

        #endregion

        #region 流程树拖拽

        /// <summary>
        /// 拖动工具节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void tvw_job_ItemDrag(object sender, ItemDragEventArgs e)//左键拖动  
        {
            try
            {
                Win_ToolBox.DragNode = null;
                if (((TaskEdit)sender).SelectedNode != null)
                {
                    if (((TaskEdit)sender).SelectedNode.Level == 1)          //输入输出不允许拖动
                    {
                        Job.GetJobTreeStatic().DoDragDrop(e.Item, DragDropEffects.Move);
                    }

                    else if (e.Button == MouseButtons.Left)
                    {
                        Job.GetJobTreeStatic().DoDragDrop(e.Item, DragDropEffects.Move);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        /// <summary>
        /// 节点拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void tvw_job_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode"))
                {
                    e.Effect = DragDropEffects.Move;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        /// <summary>
        /// 放开被拖动的节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void tvw_job_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                //需要辨别是工具箱拖过来的，还是流程间内部拖拽，此处只要流程间一旦有拖动动作，就给ToolBox里面DragNode变量赋null，所以此处通过判断ToolBox中的DragNode是否为null来判断属于那种拖动
                if (Win_ToolBox.DragNode != null)
                {
                    if (sender != null && sender is TaskEdit edit)
                    {
                        TaskEdit trv = sender as TaskEdit;

                        MoveTreeView move = (MoveTreeView)Convert.ToInt32(trv.Tag);
                        if (move == Win_ToolBox.MoveTo) { Win_ToolBox.DragNode = null; Win_ToolBox.NodeSource = null; }
                        else
                        {
                            System.Drawing.Point p = trv.PointToClient(new System.Drawing.Point(e.X, e.Y));
                            TreeNode node = trv.GetNodeAt(p);

                            if (node == null)
                            {
                                Win_ToolBox.Instance.AddTool(Win_ToolBox.DragNode.Text, null, L_toolList.Count);
                            }
                            else
                            {
                                if (p.Y > edit.Nodes[edit.Nodes.Count - 1].Bounds.Y)
                                    Win_ToolBox.Instance.AddTool(Win_ToolBox.DragNode.Text, null, L_toolList.Count);
                                else if (node.Level == 0)
                                    Win_ToolBox.Instance.AddTool(Win_ToolBox.DragNode.Text, null, node.Index);
                                else
                                    Win_ToolBox.Instance.AddTool(Win_ToolBox.DragNode.Text, null, node.Parent.Index + 1);
                            }

                            return;
                        }

                    }
                }


                //获得拖放中的节点  
                TreeNode moveNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                //根据鼠标坐标确定要移动到的目标节点  
                System.Drawing.Point pt;
                TreeNode targeNode;
                pt = ((TaskEdit)(sender)).PointToClient(new System.Drawing.Point(e.X, e.Y));
                targeNode = Job.GetJobTreeStatic().GetNodeAt(pt);
                //如果目标节点无子节点则添加为同级节点,反之添加到下级节点的未端  

                if (moveNode == targeNode)       //若是把自己拖放到自己，不可，返回
                    return;

                if (targeNode == null)       //目标节点为null，就是把节点拖到了空白区域，则表示要把节点拖到末尾
                {
                    if (moveNode.Level == 0)        //被拖动的是子节点，也就是工具节点
                    {
                        //if (targeNode.Level == 0)
                        {
                            moveNode.Remove();
                            Job.GetJobTreeStatic().Nodes.Insert(L_toolList.Count, moveNode);

                            ToolInfo temp = new ToolInfo();
                            for (int i = 0; i < L_toolList.Count; i++)
                            {
                                if (L_toolList[i].toolName == moveNode.Text)
                                {
                                    temp = L_toolList[i];
                                    L_toolList.RemoveAt(i);
                                    L_toolList.Insert(L_toolList.Count, temp);
                                    break;
                                }
                            }
                        }
                        
                    }
                    //更新当前拖动的节点选择  
                    Job.GetJobTreeStatic().SelectedNode = moveNode;
                    //展开目标节点,便于显示拖放效果  
                    GetToolNodeByNodeText(moveNode.Text).Expand();
                    return;
                }

             

                if (moveNode.Level == 0)        //被拖动的是子节点，也就是工具节点
                {
                    if (targeNode.Level == 0)
                    {
                        moveNode.Remove();
                        Job.GetJobTreeStatic().Nodes.Insert(targeNode.Index, moveNode);

                        ToolInfo temp = new ToolInfo();
                        for (int i = 0; i < L_toolList.Count; i++)
                        {
                            if (L_toolList[i].toolName == moveNode.Text)
                            {
                                temp = L_toolList[i];
                                L_toolList.RemoveAt(i);
                                L_toolList.Insert(targeNode.Index - 1, temp);
                                break;
                            }
                        }
                    }
                    else
                    {
                        moveNode.Remove();
                        Job.GetJobTreeStatic().Nodes.Insert(targeNode.Parent.Index + 1, moveNode);

                        ToolInfo temp = new ToolInfo();
                        for (int i = 0; i < L_toolList.Count; i++)
                        {
                            if (L_toolList[i].toolName == moveNode.Text)
                            {
                                temp = L_toolList[i];
                                L_toolList.RemoveAt(i);
                                L_toolList.Insert(targeNode.Parent.Index + 1, temp);
                                break;
                            }
                        }
                    }
                }
               
                //更新当前拖动的节点选择  
                Job.GetJobTreeStatic().SelectedNode = moveNode;
                //展开目标节点,便于显示拖放效果  
                targeNode.Expand();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        #endregion

        #region 流程编辑

        /// <summary>
        /// 添加输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Add_input(object sender, string toolName)
        {
            try
            {
                string result = string.Empty;
                DataType ioType;
                string temp = sender.GetType().ToString();
                if (temp == "System.Windows.Forms.ToolStripMenuItem")
                {
                    result = sender.ToString();
                    ioType = (DataType)((ToolStripItem)sender).Tag;
                }
                else
                {
                    if (((TreeNode)sender).Tag == null)
                        result = ((TreeNode)sender).Text;
                    else if ((DataType)((TreeNode)sender).Tag == DataType.String)
                        result = (((TreeNode)sender).Text.Contains("=") ? Regex.Split(((TreeNode)sender).Text.Substring(10), "=")[0] : ((TreeNode)sender).Text);
                    else if ((DataType)((TreeNode)sender).Tag == DataType.Image || (DataType)((TreeNode)sender).Tag == DataType.XY || (DataType)((TreeNode)sender).Tag == DataType.Pose || (DataType)((TreeNode)sender).Tag == DataType.Region)
                    {
                        int idx = ((TreeNode)sender).Text.IndexOf("  ");
                        result = ((TreeNode)sender).Text.Substring(idx + 2, ((TreeNode)sender).Text.Length - 2 - idx);
                    }

                    ioType = (DataType)((TreeNode)sender).Tag;
                }

                //首先检查是否已经有此输入项,若已添加，则返回
                foreach (var item in GetToolNodeByNodeText(toolName).Nodes)
                {
                    string text;
                    if (((TreeNode)item).Text.Contains("《"))
                    {
                        text = Regex.Split(((TreeNode)item).Text, "《")[0];
                    }
                    else
                    {
                        text = ((TreeNode)item).Text;
                    }
                    if (text == result)
                    {
                        Win_Main.Instance.OutputMsg("已存在此输入或输出项，不可重复添加", Win_Log.InfoType.tip);
                        return;
                    }
                }

                int insertPos = GetInputItemNum(GetToolNodeByNodeText(toolName));        //获取插入位置，要保证输入项在前，输出项在后
                TreeNode node = GetToolNodeByNodeText(toolName).Nodes.Insert(insertPos, "", result, 34, 34);
                node.ForeColor = Color.LightPink;
                GetToolNodeByNodeText(toolName).ExpandAll();


                node.Tag = ioType;
                node.Name = result;
                FindToolInfoByName(toolName).input.Add(new ToolIO(result, "", ioType));

                
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        /// <summary>
        /// 添加输出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Add_output(object sender, string toolName)
        {
            try
            {
                string temp = sender.GetType().ToString();
                string text = string.Empty;
                DataType ioType;
                if (temp != "System.Windows.Forms.TreeNode")
                {
                    List<ToolStripItem> list = new List<ToolStripItem>();
                    ToolStripItem dd = (ToolStripItem)sender;
                    list.Add(dd);


                    while (true)
                    {
                        dd = dd.OwnerItem;
                        if (dd.Name == "添加输出项")
                            break;
                        list.Add(dd);
                    }


                    for (int i = list.Count - 1; i >= 0; i--)
                    {
                        text += list[i].Name;
                        if (i != 0)
                            text += " . ";
                    }
                    ioType = (DataType)((ToolStripItem)sender).Tag;
                }
                else
                {
                    List<TreeNode> list = new List<TreeNode>();
                    TreeNode node1 = (TreeNode)sender;
                    list.Add(node1);

                    while (true)
                    {
                        node1 = node1.Parent;
                        if (node1 == null)
                            break;
                        list.Add(node1);
                    }

                    for (int i = list.Count - 2; i >= 0; i--)
                    {
                        if (list[i].Tag == null)
                            text += list[i].Text;
                        else if ((DataType)(list[i].Tag) == DataType.String)
                        //text += (list[i].Text.Contains("=") ? Regex.Split(list[i].Text, "=")[0] : list[i].Text);
                        {
                            int idx = list[i].Text.IndexOf("  ");
                            string temp11 = list[i].Text.Substring(idx + 2, list[i].Text.Length - 2 - idx);
                            text += Regex.Split(temp11, "=")[0];
                        }

                        else if ((DataType)(list[i].Tag) == DataType.Image || (DataType)(list[i].Tag) == DataType.XY || (DataType)(list[i].Tag) == DataType.Pose || (DataType)(list[i].Tag) == DataType.Region)
                        {
                            int idx = list[i].Text.IndexOf("  ");
                            text += list[i].Text.Substring(idx + 2, list[i].Text.Length - 2 - idx);
                        }


                        if (i != 0)
                            text += " . ";
                    }
                    ioType = (DataType)((TreeNode)sender).Tag;
                }




                foreach (var item in GetToolNodeByNodeText(toolName).Nodes)
                {
                    if (((TreeNode)item).Text == text)
                    {
                        return;
                    }
                }
                TreeNode node = GetToolNodeByNodeText(toolName).Nodes.Add("", text, 34, 34);
                node.ForeColor = Color.LightGreen;
                GetToolNodeByNodeText(toolName).ExpandAll();


                //指定输出变量的类型
                if (text == ("输出图像"))
                {
                    node.ToolTipText = ("图形变量不支持显示");
                }

                node.Tag = ioType;
                node.Name = text;
                FindToolInfoByName(GetToolNodeByNodeText(toolName).Text).output.Add(new ToolIO(text, "", ioType));
                node.ToolTipText = ("未运行");
                GetJobTree().ShowNodeToolTips = true;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        /// <summary>
        /// 工具上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveUp(object sender, EventArgs e)
        {
            try
            {
                if (GetJobTree().SelectedNode.Index == 0)
                    return;

                TreeNode Node = GetJobTree().SelectedNode;
                TreeNode PrevNode = Node.PrevNode;
                if (PrevNode != null)
                {
                    TreeNode NewNode = (TreeNode)Node.Clone();
                    if (Node.Parent == null)
                    {
                        GetJobTree().Nodes.Insert(PrevNode.Index, NewNode);
                    }
                    else
                    {
                        Node.Parent.Nodes.Insert(PrevNode.Index, NewNode);
                    }
                    Node.Remove();
                    GetJobTree().SelectedNode = NewNode;
                }

                ToolInfo temp = new ToolInfo();
                for (int i = 0; i < L_toolList.Count; i++)
                {
                    if (L_toolList[i].toolName == GetJobTree().SelectedNode.Text)
                    {
                        temp = L_toolList[i];
                        L_toolList[i] = L_toolList[i - 1];
                        L_toolList[i - 1] = temp;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        /// <summary>
        /// 工具下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveDown(object sender, EventArgs e)
        {
            try
            {
                if (GetJobTree().SelectedNode.Index == GetJobTree().Nodes.Count - 1)
                    return;
                TreeNode Node = GetJobTree().SelectedNode;
                TreeNode NextNode = Node.NextNode;
                if (NextNode != null)
                {
                    TreeNode NewNode = (TreeNode)Node.Clone();
                    if (Node.Parent == null)
                    {
                        GetJobTree().Nodes.Insert(NextNode.Index + 1, NewNode);
                    }
                    else
                    {
                        Node.Parent.Nodes.Insert(NextNode.Index + 1, NewNode);
                    }
                    Node.Remove();
                    GetJobTree().SelectedNode = NewNode;
                }

                ToolInfo temp = new ToolInfo();
                for (int i = 0; i < L_toolList.Count; i++)
                {
                    if (L_toolList[i].toolName == GetJobTree().SelectedNode.Text)
                    {
                        temp = L_toolList[i];
                        L_toolList[i] = L_toolList[i + 1];
                        L_toolList[i + 1] = temp;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        /// <summary>
        /// 删除项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DeleteItem(object sender, EventArgs e)
        {
            try
            {
                if (GetJobTree().SelectedNode == null)
                    return;
                string nodeText = GetJobTree().SelectedNode.Text.ToString();
                int level = GetJobTree().SelectedNode.Level;
                string fatherNodeText = string.Empty;
                if (level == 0)
                {
                    Win_ConfirmBox.Instance.lbl_info.Text = (string.Format("确定要删除工具 [{0}] 吗？", nodeText));
                    Win_ConfirmBox.Instance.ShowDialog();
                    if (Win_ConfirmBox.Instance.Result != ConfirmBoxResult.Yes)
                        return;
                }
                string BasePath = string.Format(Application.StartupPath + "\\Config\\Project\\Vision\\{0}\\{1}\\{2}\\",
                Project.Instance.configuration.ProgramTitle, Project.Instance.curEngine.schemeName,
                jobName);
                if (System.IO.File.Exists(BasePath + nodeText + ".vpp"))
                {
                    File.Delete(BasePath + nodeText + ".vpp");
                    //System.IO.Directory.Delete(BasePath);
                }
               
                foreach (TreeNode toolNode in GetJobTree().Nodes)
                {
                    if (((TreeNode)toolNode).Text == nodeText)
                    {
                        ((TreeNode)toolNode).Remove();
                        break;
                    }
                }

                for (int i = 0; i < L_toolList.Count; i++)
                {
                    if (L_toolList[i].toolName == nodeText)
                    {

                        L_toolList.RemoveAt(i);
                        break;
                    }

                }
                if (outPutList != null)
                {
                    foreach (OutT item in outPutList)
                    {
                        if (item.ToolName == nodeText)
                        {

                            outPutList.Remove(item);

                        }
                    }
                }
                Win_Monitor.UpdateOutlist(true);
                for (int i = 0; i < L_toolList.Count; i++)
                {
                    int k = 0;
                    foreach (var item in L_toolList[i].input)
                    {
                        if (item.value.ToString().Contains("<-" + nodeText + "."))
                        {
                            item.value = "";
                            Job.GetJobTreeStatic().Nodes[i].Nodes[k].Text = item.IOName;
                           
                        }
                        k++;
                    }

                }
                GetJobTree().Focus();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        /// <summary>
        /// 工具重命名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void EditNodeText(object sender, NodeLabelEditEventArgs e)
        {
            try
            {
                string newToolName = e.Label;
                if (newToolName == "" || newToolName == null)       //放弃工具重命名
                {
                    ThreadPool.QueueUserWorkItem(GiveupRename);
                    return;
                }

                //检查是否已经存在此名称的工具
                for (int i = 0; i < L_toolList.Count; i++)
                {
                    if (L_toolList[i].toolName == newToolName)
                    {
                        ((TaskEdit)sender).SelectedNode.Text = nodeTextBeforeEdit;
                        Application.DoEvents();
                        Win_MessageBox.Instance.MessageBoxShow("\r\n已存在此名称的工具，工具名不可重复");
                        ((TaskEdit)sender).SelectedNode.BeginEdit();
                        return;
                    }
                }
                
                for (int i = 0; i < L_toolList.Count; i++)
                {
                    if (L_toolList[i].toolName == nodeTextBeforeEdit)
                    {
                      
                        string BasePath = string.Format(Application.StartupPath + "\\Config\\Project\\Vision\\{0}\\{1}\\{2}\\",
                     Project.Instance.configuration.ProgramTitle, Project.Instance.curEngine.schemeName,
                     jobName);
                        if (File.Exists(BasePath + L_toolList[i].toolName + ".vpp"))
                        {
                            File.Move(BasePath + L_toolList[i].toolName + ".vpp", BasePath + newToolName + ".vpp");
                        }
               
                L_toolList[i].toolName = newToolName;

                break;
                    }
                }

                for (int i = 0; i < L_toolList.Count; i++)
                {
                    //对OutputBox特殊处理
                    if (L_toolList[i].toolType == ToolType.Output)
                    {
                        for (int j = 0; j < L_toolList[i].input.Count; j++)
                        {
                            string sourceFromItem = L_toolList[i].input[j].IOName;
                            string sourceFromToolName = Regex.Split(sourceFromItem.Substring(2), ".")[0];
                            if (sourceFromToolName == nodeTextBeforeEdit)
                            {
                                string oldKey = L_toolList[i].input[j].IOName;
                                string value = L_toolList[i].input[j].value.ToString();
                                L_toolList[i].RemoveInputIO(oldKey);
                                string newKey = newToolName + "-" + Regex.Split(sourceFromItem.Substring(2), ".")[1];
                                L_toolList[i].input.Add(new ToolIO(newKey, value, DataType.String));
                                //修改节点文本
                                TreeNode toolNode = GetToolNodeByNodeText(L_toolList[i].toolName);
                                string nodeText = oldKey;
                                foreach (TreeNode item in toolNode.Nodes)
                                {
                                    if (((TreeNode)item).Text == nodeText)
                                    {
                                        ((TreeNode)item).Text = L_toolList[i].input[j].IOName;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < L_toolList[i].input.Count; j++)
                        {
                            if (L_toolList[i].input[j].value.ToString() != string.Empty)
                            {
                                string sourceFromItem = L_toolList[i].input[j].value.ToString();
                                string sourceFromToolName =sourceFromItem.Substring(2).Split('.')[0];
                                if (sourceFromToolName == nodeTextBeforeEdit)
                                {
                                    L_toolList[i].input[j].value = "<-" + newToolName + "." + sourceFromItem.Substring(2).Split('.')[1];
                                    //修改节点文本
                                    TreeNode toolNode = GetToolNodeByNodeText(L_toolList[i].toolName);
                                    string nodeText = L_toolList[i].input[j].value.ToString().Split('.')[1] + sourceFromItem;
                                    foreach (TreeNode item in toolNode.Nodes)
                                    {
                                        if (((TreeNode)item).Text == nodeText)
                                        {
                                            ((TreeNode)item).Text = L_toolList[i].input[j].value.ToString().Split('.')[1] + L_toolList[i].input[j].value.ToString();
                                            break;
                                        }
                                    }
                                    foreach (OutT item in outPutList)
                                    {
                                        if (item.ToolName == sourceFromToolName)
                                        {

                                            item.MPath = newToolName+"."+ item.MPath.Split('.')[1];
     
                                        }
                                    }
                                    Win_Monitor.UpdateOutlist(true);
                                    break;
                                }
                            }
                        }
                    }
                }
               
                Job.GetJobTreeStatic().Show();
                Job.GetJobTreeStatic().LabelEdit = false;
                //DrawLine();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        /// <summary>
        /// 连接源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ConnectSource(object sender, EventArgs e)
        {
            try
            {
                string nodeText = string.Empty;
                if (Job.GetJobTreeStatic().SelectedNode.Level == 1)
                {
                    nodeText = Job.GetJobTreeStatic().SelectedNode.Parent.Text;
                }
                else
                {
                    nodeText = Job.GetJobTreeStatic().SelectedNode.Text;
                }

                string input = Job.GetJobTreeStatic().SelectedNode.Text;
                if (Job.GetJobTreeStatic().SelectedNode.Text.Contains("<-"))       //表示已经连接了源
                {
                    string oldSource = Regex.Split(input, "<-")[1].Trim();
                    string oldSourceTool = oldSource.Split('.')[0];
                    string oldSourceIO = oldSource.Split('.')[1];

                    string newSource = sender.ToString();
                    string newSourceTool = newSource.Split('.')[0];
                    string newSourceIO = newSource.Split('.')[1];

                    Job.GetJobTreeStatic().SelectedNode.Text = Regex.Split(input, "<-")[0] + "<-" + sender.ToString();
                    FindToolInfoByName(nodeText).GetInput(Regex.Split(input, "<-")[0]).value = "<-" + sender.ToString();

                    Application.DoEvents();

                }
                else
                {
                    // FindToolInfoByName(nodeText).GetInput(input).value = sender.ToString();
                    Job.GetJobTreeStatic().SelectedNode.Text = input + "<-" + sender.ToString();
                    FindToolInfoByName(nodeText).GetInput(input).value = "<-" + sender.ToString();


                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        public void DisSource(string ToolName, string OldSource, string NodeName = "图像")
        {
            string nodeText = ToolName;
            string input = NodeName + OldSource;

            //int SubNodeIndex = -1;
            TaskEdit T = Job.GetJobTreeStatic();
            foreach (TreeNode item in T.Nodes)
            {
                if (item.Text == ToolName)
                {

                    foreach (TreeNode sn in item.Nodes)
                    {
                        if (sn.Text == input)
                        {
                            sn.Text = NodeName;
                            break;
                        }
                    }
                    break;
                }
            }
            //Job.GetJobTreeStatic().Nodes[ToolIndex].Nodes[SubNodeIndex].Text = Regex.Split(input, "<-")[0] + "<-" + NewSource.ToString();
            FindToolInfoByName(nodeText).GetInput(Regex.Split(input, "<-")[0]).value = "";


        }
        public void ConnectSource(string ToolName, string NewSource, string OldSource, string NodeName = "图像")
        {
            try
            {
                string nodeText = ToolName;
                string input = NodeName + OldSource;
                //寻找工具节点
                TaskEdit T = Job.GetJobTreeStatic();
                int ToolIndex = -1;
                int SubNodeIndex = -1;
                foreach (TreeNode item in T.Nodes)
                {
                    if (item.Text == ToolName)
                    {
                        ToolIndex = item.Index;
                        foreach (TreeNode sn in item.Nodes)
                        {
                            if (sn.Text == input)
                            {
                                SubNodeIndex = sn.Index;
                                break;
                            }
                        }
                        break;
                    }
                }
                if (ToolIndex == -1 || SubNodeIndex == -1)
                {
                    return;
                }
                if (OldSource.Contains("<-"))       //表示已经连接了源
                {
                    string oldSource = Regex.Split(input, "<-")[1].Trim();
                    string oldSourceTool = oldSource.Split('.')[0];
                    string oldSourceIO = oldSource.Split('.')[1];

                    string newSource = NewSource.ToString();
                    string newSourceTool = newSource.Split('.')[0];
                    string newSourceIO = newSource.Split('.')[1];


                    T.Nodes[ToolIndex].Nodes[SubNodeIndex].Text = Regex.Split(input, "<-")[0] + "<-" + NewSource.ToString();
                    FindToolInfoByName(nodeText).GetInput(Regex.Split(input, "<-")[0]).value = "<-" + NewSource.ToString();
                    Application.DoEvents();
                }
                else
                {
                    T.Nodes[ToolIndex].Nodes[SubNodeIndex].Text = input + "<-" + NewSource.ToString();
                    FindToolInfoByName(nodeText).GetInput(input).value = "<-" + NewSource.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        #endregion

        #region 工具相关

        /// <summary>
        /// 运行工具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunTool(object sender, EventArgs e)
        {
            try
            {
                FindToolByName(jobName, Job.GetJobTreeStatic().SelectedNode.Text).Run(true, true, string.Empty);
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        /// <summary>
        /// 重命名工具                
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RenameTool(object sender, EventArgs e)
        {
            try
            {
                nodeTextBeforeEdit = Job.GetJobTreeStatic().SelectedNode.Text;
                GetJobTree().LabelEdit = true;
                GetJobTree().SelectedNode.BeginEdit();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        /// <summary>
        /// 被复制的工具
        /// </summary>
        private ToolInfo toolInfoCopied = new ToolInfo();
        public static class ObjectCopierToolInfo
        {
            public static ToolInfo Clone<ToolInfo>(ToolInfo source)
            {
                if (!typeof(ToolInfo).IsSerializable)
                {
                    throw new ArgumentException("The type must be serializable.", "source");
                }

                if (source == null)
                {
                    return default;
                }

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new MemoryStream();
                using (stream)
                {
                    formatter.Serialize(stream, source);
                    stream.Seek(0, SeekOrigin.Begin);
                    return (ToolInfo)formatter.Deserialize(stream);
                }
            }
        }
        /// <summary>
        /// 复制工具                
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyTool(object sender, EventArgs e)
        {
            try
            {
                toolInfoCopied = ObjectCopierToolInfo.Clone(FindToolInfoByName(GetJobTree().SelectedNode.Text));               //此处应该是深拷贝
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        /* 利用反射实现深拷贝*/
        public static object DeepCopy(object _object)
        {
            Type T = _object.GetType();
            object o = Activator.CreateInstance(T);
            PropertyInfo[] PI = T.GetProperties();
            for (int i = 0; i < PI.Length; i++)
            {
                PropertyInfo P = PI[i];
                P.SetValue(o, P.GetValue(_object, null), null);
            }
            return o;
        }
        /// <summary>
        /// 粘贴工具                
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasteTool(object sender, EventArgs e)
        {
            try
            {
                Win_ToolBox.Instance.AddTool(toolInfoCopied.toolType.ToString(), toolInfoCopied, GetJobTree().SelectedNode.Index);
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        /// <summary>
        /// 粘贴工具                
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasteToolAtLast(object sender, EventArgs e)
        {
            try
            {
                Win_ToolBox.Instance.AddTool(toolInfoCopied.toolType.ToString(), toolInfoCopied, GetJobTree().Nodes.Count - 1);
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        /// <summary>
        /// 插入工具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsertTool(object sender, EventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Developer))
                    return;
                Win_ToolBox.Instance.AddTool(((ToolStripItem)sender).Text, null, Job.GetJobTreeStatic().SelectedNode.Index);
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        /// <summary>
        /// 启用/忽略工具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowIOForm(object sender, EventArgs e)
        {
            Win_IOConfig.b = false;
            Win_IOConfig.Instance.comboBox1.Items.Clear();
            for (int i = 0; i < Project.Instance.curEngine.FindJobByName(jobName).L_toolList.Count; i++)
            {
                Win_IOConfig.Instance.comboBox1.Items.Add(Project.Instance.curEngine.FindJobByName(jobName).L_toolList[i].toolName);
            }
            string s = Job.GetJobTreeStatic().SelectedNode.Text;
            ShowIOEdit(s);
            Win_IOConfig.b = true;
        }
        internal void ShowIOEdit(string toolName)
        {
            try
            {
                ToolInfo toolInfo = Project.Instance.curEngine.FindJobByName(jobName).FindToolInfoByName(toolName);
                switch (toolInfo.toolType)
                {
                    case ToolType.ImageAcq:
                        Win_IOConfig.result1 = ((AcqImageTool)toolInfo.tool).toolPar;
                        break;
                    case ToolType.Match:
                        Win_IOConfig.result1 = ((MatchTool)toolInfo.tool).toolPar;
                        break;
                        //case ToolType.CreateROI:
                        //    Win_IOConfig.result1 = ((CreateROITool)toolInfo.tool).toolPar;
                        //    break;
                        //case ToolType.BlobAnalyse:
                        //    Win_IOConfig.result1 = ((BlobAnalyseTool)toolInfo.tool).toolPar;
                        //    break;
                        //case ToolType.FindCircle:
                        //    Win_IOConfig.result1 = ((FindCircleTool)toolInfo.tool).toolPar;
                        //    break;
                        //case ToolType.FindLine:
                        //    Win_IOConfig.result1 = ((FindLineTool)toolInfo.tool).toolPar;
                        //    break;
                        //case ToolType.EyeHandCalib:
                        //    Win_IOConfig.result1 = ((EyeHandCalibTool)toolInfo.tool).toolPar;
                        //    break;
                        //case ToolType.PointOffset:
                        //    Win_IOConfig.result1 = ((PointOffsetTool)toolInfo.tool).toolPar;
                        //    break;
                        //case ToolType.ToStr:
                        //    Win_IOConfig.result1 = ((ToStrTool)toolInfo.tool).toolPar;
                        //    break;
                        //case ToolType.SaveImage:
                        //    Win_IOConfig.result1 = ((SaveImageTool)toolInfo.tool).toolPar;
                        //    break;
                        //case ToolType.QuoteTrans:
                        //    Win_IOConfig.result1 = ((QuoteTransTool)toolInfo.tool).toolPar;
                        //    break;
                        //case ToolType.CenterOfPP:
                        //    Win_IOConfig.result1 = ((CenterOfPP)toolInfo.tool).toolPar;
                        //    break;
                        //case ToolType.EthernetSend:
                        //    Win_IOConfig.result1 = ((EthernetSendTool)toolInfo.tool).toolPar;
                        //    break;
                        //case ToolType.UpCamAlign:
                        //    Win_IOConfig.result1 = ((UpCamAlignTool)toolInfo.tool).toolPar;
                        //    break;
                        //case ToolType.DataAnalyse:
                        //    Win_IOConfig.result1 = ((DataAnalyseTool)toolInfo.tool).toolPar;
                        //    break;
                        //case ToolType.CreateLine:
                        //    Win_IOConfig.result1 = ((CreateLineTool)toolInfo.tool).toolPar;
                        //    break;
                        //case ToolType.CreatePosition:
                        //    Win_IOConfig.result1 = ((CreatePositionTool)toolInfo.tool).toolPar;
                        //    break;
                        //case ToolType.AlignFit:
                        //    Win_IOConfig.result1 = ((AlignFitTool)toolInfo.tool).toolPar;
                        //    break;
                }
                Win_IOConfig.Instance.jobName = this.jobName;
                Win_IOConfig.Instance.Show();

                Win_IOConfig.Instance.comboBox1.Text = toolName;
                Win_IOConfig.Instance.LoadIO();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        /// <summary>
        /// 启用/忽略工具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnableOrDisenableTool(object sender, EventArgs e)
        {
            try
            {
                string jobName = Win_Job.Instance.JobEdit_JobName.Text.Split('：')[1];
                FindToolInfoByName(Job.GetJobTreeStatic().SelectedNode.Text).enable = !FindToolInfoByName(Job.GetJobTreeStatic().SelectedNode.Text).enable;
                if (FindToolInfoByName(Job.GetJobTreeStatic().SelectedNode.Text).enable)
                    (Job.GetJobTreeStatic().SelectedNode).ForeColor = Color.Black;
                else
                    (Job.GetJobTreeStatic().SelectedNode).ForeColor = Color.DarkGray;

                GetJobTree().SelectedNode = null;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        /// <summary>
        /// 通过流程名和工具名获取工具
        /// </summary>
        /// <param name="jobName">流程名</param>
        /// <param name="toolName">工具名</param>
        /// <returns></returns>
        internal static ToolBase FindToolByName(string jobName, string toolName)
        {
            try
            {
                Job job = new Job();
                for (int i = 0; i < Project.Instance.curEngine.L_jobList.Count; i++)
                {
                    if (Project.Instance.curEngine.L_jobList[i].jobName == jobName)
                    {
                        job = Project.Instance.curEngine.L_jobList[i];
                        break;
                    }
                }
                for (int i = 0; i < job.L_toolList.Count; i++)
                {
                    if (job.L_toolList[i].toolName == toolName)
                    {
                        return job.L_toolList[i].tool;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
                return null;
            }
        }
        /// <summary>
        /// 查找指定工具
        /// </summary>
        /// <param name="outputItem">工具名称</param>
        public ToolBase FindToolByName(string toolName)
        {
            try
            {
                //寻找输出工具
                for (int i = 0; i < L_toolList.Count; i++)
                {
                    if (L_toolList[i].toolName == toolName)
                    {
                        return L_toolList[i].tool;
                    }
                }
                return new ToolBase();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
                return new ToolBase();
            }
        }
        /// <summary>
        /// 通过TreeNode节点文本获取节点
        /// </summary>
        /// <param name="nodeText">节点文本</param>
        /// <returns>节点对象</returns>
        internal TreeNode GetToolNodeByNodeText(string nodeText)
        {
            try
            {
                foreach (TreeNode toolNode in Job.GetJobTreeStatic().Nodes)
                {
                    if (((TreeNode)toolNode).Text != nodeText)
                    {
                        foreach (TreeNode itemNode in ((TreeNode)toolNode).Nodes)
                        {
                            if (((TreeNode)itemNode).Text == nodeText)
                            {
                                return itemNode;
                            }
                        }
                    }
                    else
                    {
                        return toolNode;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
                return null;
            }
        }
        /// <summary>
        /// 通过TreeNode节点文本获取输入输出节点
        /// </summary>
        /// <param name="toolName">工具名称</param>
        /// <returns>IO名称</returns>
        internal TreeNode GetToolIONodeByNodeText(string toolJobName, string toolName, string toolIOName, bool IsInPut = true)
        {
            try
            {
                foreach (TreeNode toolNode in Job.GetJobTreeStatic().Nodes)
                {
                    if (toolNode.Text == toolName)
                    {
                        foreach (TreeNode itemNode in ((TreeNode)toolNode).Nodes)
                        {
                            if (((TreeNode)itemNode).Text == toolIOName)
                            {
                                if (IsInPut)
                                {
                                    if (itemNode.SelectedImageIndex == 2)
                                    {
                                        return itemNode;
                                    }
                                }
                                else
                                {
                                    if (itemNode.SelectedImageIndex == 3)
                                    {
                                        return itemNode;
                                    }
                                }

                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
                return null;
            }
        }

        #endregion


    


        private void tvw_tools_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (isRunLoop)
                {
                    GetJobTree().ContextMenuStrip = null;
                    return;
                }

                //if (e.Button == MouseButtons.Right)
                //{
                TreeNode tn = GetJobTree().GetNodeAt(e.X, e.Y);
                if (tn != null)
                {
                    GetJobTree().SelectedNode = tn;
                }
                //}

                if (e.Y > GetJobTree().Nodes[GetJobTree().Nodes.Count - 1].Bounds.Y + 10)
                {
                    GetJobTree().ContextMenuStrip = rightClickMenuAtBlank;
                }


            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }


        bool doubleClick = false;

        public static void Delay(double t)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Restart();
            while (stopWatch.Elapsed.TotalMilliseconds < t)
            {
                Application.DoEvents();
            }
        }



        internal void TVW_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (isRunLoop)
                    return;

                Thread th = new Thread(() =>
                {

                    doubleClick = false;
                    Thread.Sleep(500);
                    if (doubleClick)
                        return;

                    TreeNode node = ((TaskEdit)sender).SelectedNode;
                    if (node == null)
                        return;

                    string toolName = ((TaskEdit)sender).SelectedNode.Text;

                    for (int i = 0; i < L_toolList.Count; i++)
                    {
                        if (L_toolList[i].toolName == toolName)
                        {
                            switch (L_toolList[i].toolType)
                            {
                                #region ImageAcq
                                case ToolType.ImageAcq:
                                    AcqImageTool acqImageTool = (AcqImageTool)L_toolList[i].tool;
                                    //当前图像显示
                                    if (acqImageTool.toolPar.ResultPar.图像 != null)
                                        GetImageWindowControl().HobjectToHimage(acqImageTool.toolPar.ResultPar.图像);


                                    if (!acqImageTool.displayAllImageRegion)
                                    {
                                        GetImageWindowControl().displayHRegion(acqImageTool.L_regions[0].getRegion());
                                    }
                                    break;
                                #endregion

                                //#region ImagePreprocessing
                                //case ToolType.ImagePreprocessing:

                                //    ImageProprecessingTool imageProprecessingTool = (ImageProprecessingTool)L_toolList[i].tool;







                                //    if (imageProprecessingTool.inputImage != null)
                                //        Win_ImageProprecessingTool.Instance.hWindow_Final1.HobjectToHimage(imageProprecessingTool.inputImage);


                                //    Win_ImageProprecessingTool.Instance.dataGridView1.Rows.Clear();
                                //    for (int j = 0; j < imageProprecessingTool.L_item.Count; j++)
                                //    {
                                //        int idx = Win_ImageProprecessingTool.Instance.dataGridView1.Rows.Add();
                                //        ((DataGridViewCheckBoxCell)Win_ImageProprecessingTool.Instance.dataGridView1.Rows[idx].Cells[0]).Value = imageProprecessingTool.L_item[j].enable;
                                //        Win_ImageProprecessingTool.Instance.dataGridView1.Rows[idx].Cells[1].Value = imageProprecessingTool.L_item[j].type;
                                //        Win_ImageProprecessingTool.Instance.dataGridView1.Rows[idx].Cells[2].Value = imageProprecessingTool.L_item[j].itemName;
                                //    }

                                //    if (Win_ImageProprecessingTool.Instance.dataGridView1.Rows.Count > 0)
                                //        Win_ImageProprecessingTool.Instance.dataGridView1.Rows[0].Selected = true;

                                //    //if (imageProprecessingTool.SearchRegion != null)
                                //    //{
                                //    //    Win_ShapeMatchTool.Instance.hWindow_Final1.viewWindow.displayROI(imageProprecessingTool.L_regions);
                                //    //    Win_ShapeMatchTool.Instance.regions = imageProprecessingTool.L_regions;
                                //    //}

                                //    ////显示模板
                                //    //try
                                //    //{
                                //    //    if (imageProprecessingTool.modelID != -1)
                                //    //    {
                                //    //        HTuple row, col, row1, col1;
                                //    //        HOperatorSet.SmallestRectangle1(imageProprecessingTool.totalRegion, out row, out col, out row1, out col1);
                                //    //        HObject outRectangle1;
                                //    //        HOperatorSet.GenRectangle1(out outRectangle1, row - 30, col - 30, row1 + 30, col1 + 30);
                                //    //        HObject imageReduced;
                                //    //        HOperatorSet.ReduceDomain(imageProprecessingTool.standardImage, outRectangle1, out imageReduced);
                                //    //        HOperatorSet.SetPart(Win_ShapeMatchTool.Instance.hwc_template.HalconWindow, row - 30, col - 30, row1 + 30, col1 + 30);
                                //    //        HOperatorSet.displayHRegion(imageReduced, Win_ShapeMatchTool.Instance.hwc_template.HalconWindow);
                                //    //        HOperatorSet.SetDraw(Win_ShapeMatchTool.Instance.hwc_template.HalconWindow, new HTuple("margin"));
                                //    //        HOperatorSet.SetColor(Win_ShapeMatchTool.Instance.hwc_template.HalconWindow, new HTuple("green"));
                                //    //        HOperatorSet.displayHRegion(imageProprecessingTool.templateRegion, Win_ShapeMatchTool.Instance.hwc_template.HalconWindow);

                                //    //        int statu = imageProprecessingTool.CreateTemplate();
                                //    //        if (statu != 0)
                                //    //            return;
                                //    //        HObject contour;
                                //    //        HOperatorSet.GetShapeModelContours(out contour, imageProprecessingTool.modelID, (HTuple)1);
                                //    //        HTuple area1, row2, column2;
                                //    //        HOperatorSet.AreaCenter(imageProprecessingTool.totalRegion, out area1, out row2, out column2);
                                //    //        HTuple homMat2D;
                                //    //        HOperatorSet.HomMat2dIdentity(out homMat2D);
                                //    //        HOperatorSet.HomMat2dTranslate(homMat2D, row2, column2, out homMat2D);
                                //    //        HOperatorSet.AffineTransContourXld(contour, out contour, homMat2D);
                                //    //        HOperatorSet.SetColor(Win_ShapeMatchTool.Instance.hwc_template.HalconWindow, new HTuple("orange"));
                                //    //        HOperatorSet.displayHRegion(contour, Win_ShapeMatchTool.Instance.hwc_template.HalconWindow);
                                //    //    }
                                //    //    else
                                //    //    {
                                //    //        Win_ShapeMatchTool.Instance.hwc_template.HalconWindow.ClearWindow();
                                //    //    }
                                //    //}
                                //    //catch { }

                                //    //将对象信息更新到界面
                                //    //Win_ShapeMatchTool.Instance.pictureBox2.Image = L_toolList[i].enable ? Resources.开 : Resources.关;
                                //    //Win_ShapeMatchTool.Instance.ckb_showCross.Checked = shapeMatchTool.showCross;
                                //    //Win_ShapeMatchTool.Instance.cbx_showTemplate.Checked = shapeMatchTool.showTemplate;
                                //    //Win_ShapeMatchTool.Instance.ckb_showFeature.Checked = shapeMatchTool.showFeature;
                                //    //Win_ShapeMatchTool.Instance.cbx_searchRegionType.Text = (shapeMatchTool.searchRegionType == RegionType.None ? "" : shapeMatchTool.searchRegionType.ToString());
                                //    //Win_ShapeMatchTool.Instance.nud_minScore.Value = Convert.ToDecimal(shapeMatchTool.minScore);
                                //    //Win_ShapeMatchTool.Instance.nud_matchNum.Value = Convert.ToDecimal(shapeMatchTool.matchNum);
                                //    //Win_ShapeMatchTool.Instance.nud_angleStart.Value = Convert.ToDecimal(shapeMatchTool.startAngle);
                                //    //Win_ShapeMatchTool.Instance.nud_angleRange.Value = Convert.ToDecimal(shapeMatchTool.angleRange);
                                //    //Win_ShapeMatchTool.Instance.nud_angleStep.Value = Convert.ToDecimal(shapeMatchTool.angleStep);
                                //    //Win_ShapeMatchTool.Instance.tkb_contrast.Value = Convert.ToInt16(shapeMatchTool.contrast);
                                //    //Win_ShapeMatchTool.Instance.cbx_polarity.Text = shapeMatchTool.polarity;
                                //    //Win_ShapeMatchTool.Instance.comboBox1.SelectedIndex = (int)shapeMatchTool.sortMode;
                                //    //Win_ShapeMatchTool.Instance.checkBox1.Checked = shapeMatchTool.showIndex;
                                //    //Win_ShapeMatchTool.Instance.textBox1.Text = shapeMatchTool.spanPixelNum.ToString();

                                //    //Win_ShapeMatchTool.Instance.pictureBox3.Image = (shapeMatchTool.showTemplate ? Resources.复选框 : Resources.去复选框);
                                //    //Win_ShapeMatchTool.Instance.pictureBox4.Image = (shapeMatchTool.showCross ? Resources.复选框 : Resources.去复选框);
                                //    //Win_ShapeMatchTool.Instance.pictureBox5.Image = (shapeMatchTool.showFeature ? Resources.复选框 : Resources.去复选框);
                                //    //Win_ShapeMatchTool.Instance.pictureBox8.Image = (shapeMatchTool.showIndex ? Resources.复选框 : Resources.去复选框);
                                //    //Win_ShapeMatchTool.Instance.pictureBox9.Image = (shapeMatchTool.showSearchRegion ? Resources.复选框 : Resources.去复选框);

                                //    //if (shapeMatchTool.angleStep == 0)
                                //    //{
                                //    //    Win_ShapeMatchTool.Instance.nud_angleStep.Enabled = false;
                                //    //    Win_ShapeMatchTool.Instance.ckb_autoStep.Checked = true;
                                //    //}
                                //    //else
                                //    //{
                                //    //    Win_ShapeMatchTool.Instance.ckb_autoStep.Checked = false;
                                //    //}

                                //    //Win_ShapeMatchTool.Instance.tbc_shapeMatch.SelectedIndex = 0;


                                //    //if (shapeMatchTool.modelID == -1)
                                //    //{
                                //    //    Win_ShapeMatchTool.Instance.panel4.Visible = false;
                                //    //    Win_ShapeMatchTool.Instance.panel17.Visible = false;
                                //    //    Win_ShapeMatchTool.Instance.button5.Visible = false;
                                //    //    Win_ShapeMatchTool.Instance.button9.Visible = false;
                                //    //}
                                //    //else
                                //    //{
                                //    //    Win_ShapeMatchTool.Instance.panel4.Visible = true;
                                //    //    Win_ShapeMatchTool.Instance.panel17.Visible = true;
                                //    //    Win_ShapeMatchTool.Instance.button5.Visible = true;
                                //    //    Win_ShapeMatchTool.Instance.button9.Visible = true;
                                //    //}
                                //    //Win_ShapeMatchTool.Instance.hWindow_Final1.DispImageFit();




                                //    break;
                                //#endregion

                                //////#region ColorToRGB
                                //////case ToolType.ColorToRGB:
                                //////    Win_ColorToRGBTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("彩图转RGB图    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //Win_ColorToRGBTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //Win_ColorToRGBTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_ShapeMatchTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_ColorToRGBTool.Instance.TopMost = true;
                                //////    Win_ColorToRGBTool.Instance.Activate();

                                //////    Win_ColorToRGBTool.Instance.jobName = this.jobName;
                                //////    Win_ColorToRGBTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_ColorToRGBTool.Instance.Show();
                                //////    Win_ColorToRGBTool.Instance.WindowState = FormWindowState.Normal;
                                //////    Win_ColorToRGBTool.Instance.btn_runColorToRGBTool.Focus();
                                //////    ColorToRGBTool colorToRGBTool = (ColorToRGBTool)(L_toolList[i].tool);
                                //////    Win_ColorToRGBTool.colorToRGBTool = colorToRGBTool;
                                //////    Application.DoEvents();

                                //////    if (colorToRGBTool.inputImage != null)
                                //////        colorToRGBTool.ShowImage(colorToRGBTool.inputImage);
                                //////    else
                                //////    { }
                                //////    //////colorToRGBTool.ClearWindow(this.jobName);

                                //////    //将对象信息更新到界面
                                //////    break;
                                //////#endregion

                                //////#region SaveImage
                                //////case ToolType.SaveImage:
                                //////    Win_SaveImageTool.Instance.XXXXXXXXXXXXXlbl_title.Text = ( "SDK_PointGray - " : string.Format("存储图像    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName));
                                //////    //Win_SaveImageTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //Win_SaveImageTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_SaveImageTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_SaveImageTool.Instance.TopMost = true;
                                //////    Win_SaveImageTool.Instance.Activate();
                                //////    Win_SaveImageTool.Instance.jobName = this.jobName;
                                //////    Win_SaveImageTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_SaveImageTool.saveImageTool = (SaveImageTool)FindToolByName(L_toolList[i].toolName);
                                //////    Win_SaveImageTool.Instance.jobName = this.jobName;
                                //////    Win_SaveImageTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_SaveImageTool.saveImageTool = ((SaveImageTool)Job.FindToolByName(this.jobName, L_toolList[i].toolName));
                                //////    Win_SaveImageTool.Instance.Show();
                                //////    Win_SaveImageTool.Instance.WindowState = FormWindowState.Normal;
                                //////    ////Win_SaveImageTool.Instance.btn_runSDKHIKVisionTool.Focus();
                                //////    SaveImageTool saveImageTool = (SaveImageTool)(L_toolList[i].tool);
                                //////    Application.DoEvents();




                                //////    //将对象信息更新到界面
                                //////    Win_SaveImageTool.Instance.pictureBox2.Image = L_toolList[i].enable ? Resources.开 : Resources.关;
                                //////    Win_SaveImageTool.Instance.tbx_imageSavePath.Text = saveImageTool.imageSavePath;
                                //////    Win_SaveImageTool.Instance.comboBox1.Text = saveImageTool.imageFormat;
                                //////    Win_SaveImageTool.Instance.textBox1.Value = saveImageTool.saveDays;
                                //////    Win_SaveImageTool.Instance.checkBox1.Checked = saveImageTool.expandTime;
                                //////    Win_SaveImageTool.Instance.checkBox2.Checked = saveImageTool.autoClear;
                                //////    Win_SaveImageTool.Instance.textBox2.Text = saveImageTool.imageName;
                                //////    Win_SaveImageTool.Instance.checkBox3.Checked = saveImageTool.autoCreateDirectory;
                                //////    Win_SaveImageTool.Instance.radioButton1.Checked = (saveImageTool.imageSource == ImageSource.InputImage ? true : false);
                                //////    Win_SaveImageTool.Instance.radioButton2.Checked = (saveImageTool.imageSource == ImageSource.InputImage ? false : true);
                                //////    Win_SaveImageTool.Instance.pictureBox8.Image = (saveImageTool.imageSource == ImageSource.InputImage ? Resources.勾选 : Resources.去勾选);
                                //////    Win_SaveImageTool.Instance.pictureBox7.Image = (saveImageTool.imageSource == ImageSource.WindowImage ? Resources.勾选 : Resources.去勾选);
                                //////    break;
                                //////#endregion

                                //#region ShapeMatch
                                //case ToolType.Match:
                                //    MatchTool matchTool = (MatchTool)L_toolList[i].tool;

                                //    if (matchTool.toolPar.InputPar.图像 != null)
                                //        Win_ShapeMatchTool.Instance.hWindow_Final1.HobjectToHimage(matchTool.toolPar.InputPar.图像);
                                //    else
                                //        Win_ShapeMatchTool.Instance.hWindow_Final1.ClearWindow();

                                //    if (matchTool.SearchRegion != null)
                                //    {
                                //        Win_ShapeMatchTool.Instance.hWindow_Final1.displayHRegion(matchTool.L_regions[0].getRegion());
                                //    }

                                //    matchTool.Run(true, false, toolName);


                                //    break;
                                //#endregion

                                //////#region BlobAnalyse
                                //////case ToolType.BlobAnalyse:
                                //////    Win_BlobAnalyseTool.Instance.pictureBox1.Image = Resources.BlobAnalyseTool;
                                //////    Win_BlobAnalyseTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("斑点分析    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //Win_BlobAnalyseTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //Win_BlobAnalyseTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_BlobAnalyseTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_BlobAnalyseTool.Instance.TopMost = true;
                                //////    Win_BlobAnalyseTool.Instance.Activate();
                                //////    Win_BlobAnalyseTool.Instance.jobName = this.jobName;
                                //////    Win_BlobAnalyseTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_BlobAnalyseTool.Instance.Show();
                                //////    Win_BlobAnalyseTool.Instance.WindowState = FormWindowState.Normal;
                                //////    //////Win_BlobAnalyseTool.Instance.btn_runTool.Focus();
                                //////    BlobAnalyseTool blobAnalyseTool = (BlobAnalyseTool)(L_toolList[i].tool);
                                //////    Win_BlobAnalyseTool.blobAnalyseTool = blobAnalyseTool;
                                //////    Win_ProcessingItem.blobAnalyseTool = blobAnalyseTool;
                                //////    Win_ProcessingItem1.blobAnalyseTool = blobAnalyseTool;
                                //////    Application.DoEvents();

                                //////    if (blobAnalyseTool.toolPar.InputPar.图像 != null)
                                //////        Win_BlobAnalyseTool.Instance.hWindow_Final1.HobjectToHimage(blobAnalyseTool.toolPar.InputPar.图像);
                                //////  

                                //////    Win_BlobAnalyseTool.Instance.hWindow_Final1.viewWindow.displayROI(blobAnalyseTool.L_regions);

                                //////    Win_BlobAnalyseTool.Instance.dgv_selectItem.Rows.Clear();
                                //////    for (int j = 0; j < blobAnalyseTool.L_select.Count; j++)
                                //////    {
                                //////        int index = Win_BlobAnalyseTool.Instance.dgv_selectItem.Rows.Add();
                                //////        Win_BlobAnalyseTool.Instance.dgv_selectItem.Rows[index].Cells[0].Value = blobAnalyseTool.L_select[j].SelectType;
                                //////        Win_BlobAnalyseTool.Instance.dgv_selectItem.Rows[index].Cells[1].Value = blobAnalyseTool.L_select[j].AreaDownLimit;
                                //////        Win_BlobAnalyseTool.Instance.dgv_selectItem.Rows[index].Cells[2].Value = blobAnalyseTool.L_select[j].AreaUpLimit;
                                //////    }

                                //////    //将预处理项更新到窗体
                                //////    Win_BlobAnalyseTool.Instance.dgv_processingItem.Rows.Clear();
                                //////    for (int j = 0; j < blobAnalyseTool.L_prePorcessing.Count; j++)
                                //////    {
                                //////        int index = Win_BlobAnalyseTool.Instance.dgv_processingItem.Rows.Add();
                                //////        Win_BlobAnalyseTool.Instance.dgv_processingItem.Rows[index].Cells[0].Value = blobAnalyseTool.L_prePorcessing[j].PreProcessingType;
                                //////        ((DataGridViewCheckBoxCell)Win_BlobAnalyseTool.Instance.dgv_processingItem.Rows[index].Cells[1]).Value = blobAnalyseTool.L_prePorcessing[j].Enable;
                                //////    }

                                //////    //////Win_BlobAnalyseTool.Instance.ckb_toolEnable.Checked = L_toolList[i].enable;
                                //////    Win_BlobAnalyseTool.Instance.ckb_displaySearchRegion.Checked = blobAnalyseTool.displaySearchRegion;
                                //////    Win_BlobAnalyseTool.Instance.ckb_displayCross.Checked = blobAnalyseTool.displayCross;
                                //////    Win_BlobAnalyseTool.Instance.tbx_lineWidth.Text = blobAnalyseTool.lineWidth.ToString();
                                //////    Win_BlobAnalyseTool.Instance.rdo_outCircleFillMode.Checked = blobAnalyseTool.outCircleDrawMode == FillMode.Fill ? true : false;
                                //////    Win_BlobAnalyseTool.Instance.rdo_outCircleMarginMode.Checked = blobAnalyseTool.outCircleDrawMode == FillMode.Fill ? false : true;
                                //////    Win_BlobAnalyseTool.Instance.rdo_regionFillMode.Checked = blobAnalyseTool.regionDrawMode == FillMode.Fill ? true : false;
                                //////    Win_BlobAnalyseTool.Instance.rdo_regionMarginMode.Checked = blobAnalyseTool.regionDrawMode == FillMode.Fill ? false : true;
                                //////    Win_BlobAnalyseTool.Instance.ckb_displayRegion.Checked = blobAnalyseTool.displayRegion;
                                //////    Win_BlobAnalyseTool.Instance.rdo_outCircleFillMode.Checked = blobAnalyseTool.outCircleDrawMode == FillMode.Fill ? true : false;
                                //////    Win_BlobAnalyseTool.Instance.ckb_DisplayOutCircle.Checked = blobAnalyseTool.displayOutCircle;
                                //////    Win_BlobAnalyseTool.Instance.comboBox1.SelectedIndex = (int)blobAnalyseTool.sortMode;
                                //////    Win_BlobAnalyseTool.Instance.textBox1.Value = blobAnalyseTool.spanPixelNum;
                                //////    Win_BlobAnalyseTool.Instance.cbx_searchRegionType.Text = blobAnalyseTool.searchRegionType.ToString();
                                //////    Win_BlobAnalyseTool.Instance.trackBar1.Value = (blobAnalyseTool.minThreshold);
                                //////    Win_BlobAnalyseTool.Instance.trackBar2.Value = (blobAnalyseTool.maxThreshold);
                                //////    Win_BlobAnalyseTool.Instance.numericUpDown1.Value = blobAnalyseTool.minThreshold;
                                //////    Win_BlobAnalyseTool.Instance.numericUpDown2.Value = blobAnalyseTool.maxThreshold;
                                //////    Win_BlobAnalyseTool.Instance.rdo_regionFillMode.Checked = blobAnalyseTool.regionDrawMode == FillMode.Fill ? true : false;
                                //////    break;
                                //////#endregion


                                //////#region ApplyTrans
                                //////case ToolType.QuoteTrans:
                                //////    Win_ApplyTransTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("引用标定    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //////Win_EyeHandCalibTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //////Win_EyeHandCalibTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_EyeHandCalibTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_EyeHandCalibTool.Instance.TopMost = true;
                                //////    Win_ApplyTransTool.Instance.Activate();
                                //////    Win_ApplyTransTool.Instance.jobName = this.jobName;
                                //////    Win_ApplyTransTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_ApplyTransTool.Instance.Show();
                                //////    Win_ApplyTransTool.Instance.WindowState = FormWindowState.Normal;
                                //////    Win_ApplyTransTool.Instance.Focus();
                                //////    QuoteTransTool applyTransTool = (QuoteTransTool)(L_toolList[i].tool);
                                //////    Win_ApplyTransTool.applyTransTool = applyTransTool;
                                //////    Application.DoEvents();


                                //////    //将对象信息更新到界面
                                //////    Win_ApplyTransTool.Instance.pictureBox8.Image = L_toolList[i].enable ? Resources.Enable : Resources.Disable;


                                //////    Win_ApplyTransTool.Instance.cbx_jobList.Items.Clear();
                                //////    for (int j = 0; j < Project.Instance.curEngine.L_jobList.Count; j++)
                                //////    {
                                //////        for (int k = 0; k < Project.Instance.curEngine.L_jobList[j].L_toolList.Count; k++)
                                //////        {
                                //////            if (Project.Instance.curEngine.L_jobList[j].L_toolList[k].toolType == ToolType.EyeHandCalib)
                                //////            {
                                //////                Win_ApplyTransTool.Instance.cbx_jobList.Items.Add(Project.Instance.curEngine.L_jobList[j].jobName + " . " + Project.Instance.curEngine.L_jobList[j].L_toolList[k].toolName);
                                //////            }
                                //////        }
                                //////    }
                                //////    Win_ApplyTransTool.Instance.cbx_jobList.Text = applyTransTool.cliperNum;
                                //////    Win_ApplyTransTool.Instance.textBox6.Text = applyTransTool.photoPos.ToString();
                                //////    break;
                                //////#endregion

                                //////#region OneKeyEyeHandCalib
                                //////case ToolType.OneKeyEyeHandCalib:
                                //////    Win_OneKeyEyeHandCalibTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("一键手眼标定    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //Win_OneKeyEyeHandCalibTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //Win_OneKeyEyeHandCalibTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_OneKeyEyeHandCalibTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_OneKeyEyeHandCalibTool.Instance.TopMost = true;
                                //////    Win_OneKeyEyeHandCalibTool.Instance.Activate();
                                //////    Win_OneKeyEyeHandCalibTool.Instance.jobName = this.jobName;
                                //////    Win_OneKeyEyeHandCalibTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_OneKeyEyeHandCalibTool.Instance.Show();
                                //////    Win_OneKeyEyeHandCalibTool.Instance.WindowState = FormWindowState.Normal;
                                //////    Win_OneKeyEyeHandCalibTool.Instance.Focus();
                                //////    OneKeyEyeHandCalibTool oneKeyEyeHandCalibTool = (OneKeyEyeHandCalibTool)(L_toolList[i].tool);
                                //////    Win_OneKeyEyeHandCalibTool.oneKeyEyeHandCalibTool = oneKeyEyeHandCalibTool;
                                //////    Application.DoEvents();

                                //////    if (oneKeyEyeHandCalibTool.inputImage != null)
                                //////        oneKeyEyeHandCalibTool.ShowImage(oneKeyEyeHandCalibTool.inputImage);
                                //////    else
                                //////        oneKeyEyeHandCalibTool.ClearWindow();

                                //////    //将对象信息更新到界面
                                //////    Win_OneKeyEyeHandCalibTool.Instance.ckb_toolEnable.Checked = L_toolList[i].enable;
                                //////    Win_OneKeyEyeHandCalibTool.Instance.cbo_calibType.Text = (oneKeyEyeHandCalibTool.calibType == CalibType.Four_Point ? "四点标定" : "九点标定");
                                //////    Win_OneKeyEyeHandCalibTool.Instance.tbx_translateX.Text = oneKeyEyeHandCalibTool.TranslateX.ToString();
                                //////    Win_OneKeyEyeHandCalibTool.Instance.tbx_translateY.Text = oneKeyEyeHandCalibTool.TranslateY.ToString();
                                //////    Win_OneKeyEyeHandCalibTool.Instance.tbx_scaleX.Text = oneKeyEyeHandCalibTool.ScanX.ToString();
                                //////    Win_OneKeyEyeHandCalibTool.Instance.tbx_scaleY.Text = oneKeyEyeHandCalibTool.ScanY.ToString();
                                //////    Win_OneKeyEyeHandCalibTool.Instance.tbx_rotation.Text = oneKeyEyeHandCalibTool.Rotation.ToString();
                                //////    Win_OneKeyEyeHandCalibTool.Instance.tbx_theta.Text = oneKeyEyeHandCalibTool.Theta.ToString();
                                //////    Application.DoEvents();

                                //////    //显示标定数据
                                //////    for (int j = 0; j < oneKeyEyeHandCalibTool.L_calibData.Count; j++)
                                //////    {
                                //////        for (int k = 0; k < 4; k++)
                                //////        {
                                //////            Win_OneKeyEyeHandCalibTool.Instance.dgv_calibData.Rows[j].Cells[k].Value = oneKeyEyeHandCalibTool.L_calibData[j][k];
                                //////        }
                                //////    }

                                //////    Win_OneKeyEyeHandCalibTool.Instance.cbx_jobList.Items.Clear();
                                //////    for (int j = 0; j < Project.Instance.curEngine.L_jobList.Count; j++)
                                //////    {
                                //////        Win_OneKeyEyeHandCalibTool.Instance.cbx_jobList.Items.Add(Project.Instance.curEngine.L_jobList[j].jobName);
                                //////    }
                                //////    Win_OneKeyEyeHandCalibTool.Instance.cbx_jobList.Text = oneKeyEyeHandCalibTool.calibJobName;
                                //////    Win_OneKeyEyeHandCalibTool.Instance.cbx_outputItemList.Text = oneKeyEyeHandCalibTool.calibItemName;
                                //////    break;
                                //////#endregion

                                //////#region OneDimensionalCalib
                                //////case ToolType.OneDimensionalCalib:
                                //////    Win_OneDimensionalCalibTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("一维标定    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //Win_OneDimensionalCalibTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //Win_OneDimensionalCalibTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_OneDimensionalCalibTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_OneDimensionalCalibTool.Instance.TopMost = true;
                                //////    Win_OneDimensionalCalibTool.Instance.Activate();
                                //////    Win_OneDimensionalCalibTool.Instance.jobName = this.jobName;
                                //////    Win_OneDimensionalCalibTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_OneDimensionalCalibTool.Instance.Show();
                                //////    Win_OneDimensionalCalibTool.Instance.WindowState = FormWindowState.Normal;
                                //////    Win_OneDimensionalCalibTool.Instance.Focus();
                                //////    OneDimensionalCalibTool oneDimensionalCalibrationTool = (OneDimensionalCalibTool)(L_toolList[i].tool);
                                //////    Win_OneDimensionalCalibTool.oneDimensionalCalibTool = oneDimensionalCalibrationTool;
                                //////    Application.DoEvents();

                                //////    //将对象信息更新到界面
                                //////    Win_OneDimensionalCalibTool.Instance.ckb_toolEnable.Checked = L_toolList[i].enable;
                                //////    Win_OneDimensionalCalibTool.Instance.tbx_translate.Text = oneDimensionalCalibrationTool.Translate.ToString();
                                //////    Win_OneDimensionalCalibTool.Instance.tbx_scale.Text = oneDimensionalCalibrationTool.Scan.ToString();
                                //////    Application.DoEvents();

                                //////    //显示标定数据
                                //////    for (int j = 0; j < ((OneDimensionalCalibTool)L_toolList[i].tool).L_calibData.Count; j++)
                                //////    {
                                //////        for (int k = 0; k < 2; k++)
                                //////        {
                                //////            Win_OneDimensionalCalibTool.Instance.dgv_calibrateData.Rows[j].Cells[k].Value = oneDimensionalCalibrationTool.L_calibData[j][k];
                                //////        }
                                //////    }

                                //////    break;
                                //////#endregion

                                //////#region UpCamAlign
                                //////case ToolType.UpCamAlign:
                                //////    Win_UpCamAlignTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("上相机定位    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //Win_UpCamAlignTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //Win_UpCamAlignTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_UpCamAlignTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_UpCamAlignTool.Instance.TopMost = true;
                                //////    Win_UpCamAlignTool.Instance.Activate();
                                //////    Win_UpCamAlignTool.Instance.jobName = this.jobName;
                                //////    Win_UpCamAlignTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_UpCamAlignTool.Instance.Show();
                                //////    Win_UpCamAlignTool.Instance.WindowState = FormWindowState.Normal;
                                //////    Win_UpCamAlignTool.Instance.btn_runTool.Focus();
                                //////    UpCamAlignTool upCamAlignTool = (UpCamAlignTool)(L_toolList[i].tool);
                                //////    Win_UpCamAlignTool.upCamAlignTool = upCamAlignTool;
                                //////    Application.DoEvents();

                                //////    Win_UpCamAlignTool.Instance.cbx_toolEnable.Checked = L_toolList[i].enable;

                                //////    Win_UpCamAlignTool.Instance.cbx_toolList.Items.Clear();
                                //////    Win_UpCamAlignTool.Instance.cbx_toolList.Items.AddRange(upCamAlignTool.L_toolName.ToArray());
                                //////    Win_UpCamAlignTool.Instance.cbx_toolList.SelectedIndex = upCamAlignTool.toolIdx;

                                //////    Win_UpCamAlignTool.Instance.tbx_inputPosX.Text = upCamAlignTool.toolPar.InputPar.位置.Point.X.ToString();
                                //////    Win_UpCamAlignTool.Instance.tbx_inputPosY.Text = upCamAlignTool.toolPar.InputPar.位置.Point.Y.ToString();
                                //////    Win_UpCamAlignTool.Instance.tbx_inputPosU.Text = upCamAlignTool.toolPar.InputPar.位置.U.ToString();

                                //////    Win_UpCamAlignTool.Instance.tbx_pickPosX.Text = upCamAlignTool.L_pickPos[upCamAlignTool.toolIdx].Point.X.ToString();
                                //////    Win_UpCamAlignTool.Instance.tbx_pickPosY.Text = upCamAlignTool.L_pickPos[upCamAlignTool.toolIdx].Point.Y.ToString();
                                //////    Win_UpCamAlignTool.Instance.tbx_pickPosU.Text = upCamAlignTool.L_pickPos[upCamAlignTool.toolIdx].U.ToString();

                                //////    Win_UpCamAlignTool.Instance.tbx_featureX.Text = upCamAlignTool.L_featurePos[upCamAlignTool.toolIdx].Point.X.ToString();
                                //////    Win_UpCamAlignTool.Instance.tbx_featureY.Text = upCamAlignTool.L_featurePos[upCamAlignTool.toolIdx].Point.Y.ToString();
                                //////    Win_UpCamAlignTool.Instance.tbx_featureU.Text = upCamAlignTool.L_featurePos[upCamAlignTool.toolIdx].U.ToString();

                                //////    Win_UpCamAlignTool.Instance.tbx_resultPosX.Text = upCamAlignTool.toolPar.ResultPar.位置.Point.X.ToString();
                                //////    Win_UpCamAlignTool.Instance.tbx_resultPosY.Text = upCamAlignTool.toolPar.ResultPar.位置.Point.Y.ToString();
                                //////    Win_UpCamAlignTool.Instance.tbx_resultPosU.Text = upCamAlignTool.toolPar.ResultPar.位置.U.ToString();

                                //////    Win_UpCamAlignTool.Instance.tbx_pickPosOffsetX.Text = upCamAlignTool.L_pickPosOffset[upCamAlignTool.toolIdx].Point.X.ToString();
                                //////    Win_UpCamAlignTool.Instance.tbx_pickPosOffsetY.Text = upCamAlignTool.L_pickPosOffset[upCamAlignTool.toolIdx].Point.Y.ToString();
                                //////    Win_UpCamAlignTool.Instance.tbx_pickPosOffsetU.Text = upCamAlignTool.L_pickPosOffset[upCamAlignTool.toolIdx].U.ToString();

                                //////    Win_UpCamAlignTool.Instance.tbx_saftyRangeX.Text = upCamAlignTool.L_safetyRange[upCamAlignTool.toolIdx].Point.X.ToString();
                                //////    Win_UpCamAlignTool.Instance.tbx_saftyRangeY.Text = upCamAlignTool.L_safetyRange[upCamAlignTool.toolIdx].Point.Y.ToString();
                                //////    Win_UpCamAlignTool.Instance.tbx_saftyRangeU.Text = upCamAlignTool.L_safetyRange[upCamAlignTool.toolIdx].U.ToString();

                                //////    break;
                                //////#endregion

                                //////#region DownCamAlign
                                //////case ToolType.DownCamAlign:
                                //////    Win_DownCamAlignTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("下相机定位    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //Win_DownCamAlignTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //Win_DownCamAlignTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_DownCamAlignTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_DownCamAlignTool.Instance.TopMost = true;
                                //////    Win_DownCamAlignTool.Instance.Activate();
                                //////    Win_DownCamAlignTool.Instance.jobName = this.jobName;
                                //////    Win_DownCamAlignTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_DownCamAlignTool.Instance.Show();
                                //////    Win_DownCamAlignTool.Instance.WindowState = FormWindowState.Normal;
                                //////    Win_DownCamAlignTool.Instance.btn_runTool.Focus();
                                //////    DownCamAlignTool robotDownCamAlignTool = (DownCamAlignTool)(L_toolList[i].tool);
                                //////    Win_DownCamAlignTool.robotDownCamAlignTool = robotDownCamAlignTool;
                                //////    Application.DoEvents();

                                //////    Win_DownCamAlignTool.Instance.cbx_toolEnable.Checked = L_toolList[i].enable;

                                //////    Win_DownCamAlignTool.Instance.tbx_inputPosX.Text = robotDownCamAlignTool.inputPos.Point.X.ToString();
                                //////    Win_DownCamAlignTool.Instance.tbx_inputPosY.Text = robotDownCamAlignTool.inputPos.Point.Y.ToString();
                                //////    Win_DownCamAlignTool.Instance.tbx_inputPosU.Text = robotDownCamAlignTool.inputPos.U.ToString();

                                //////    Win_DownCamAlignTool.Instance.tbx_photoPosX.Text = robotDownCamAlignTool.photoPos.Point.X.ToString();
                                //////    Win_DownCamAlignTool.Instance.tbx_photoPosY.Text = robotDownCamAlignTool.photoPos.Point.Y.ToString();
                                //////    Win_DownCamAlignTool.Instance.tbx_photoPosU.Text = robotDownCamAlignTool.photoPos.U.ToString();

                                //////    Win_DownCamAlignTool.Instance.tbx_featurePosX.Text = robotDownCamAlignTool.featurePos.Point.X.ToString();
                                //////    Win_DownCamAlignTool.Instance.tbx_featurePosY.Text = robotDownCamAlignTool.featurePos.Point.Y.ToString();
                                //////    Win_DownCamAlignTool.Instance.tbx_featurePosU.Text = robotDownCamAlignTool.featurePos.U.ToString();

                                //////    Win_DownCamAlignTool.Instance.tbx_resultPosX.Text = robotDownCamAlignTool.resultPos.Point.X.ToString();
                                //////    Win_DownCamAlignTool.Instance.tbx_resultPosY.Text = robotDownCamAlignTool.resultPos.Point.Y.ToString();
                                //////    Win_DownCamAlignTool.Instance.tbx_resultPosU.Text = robotDownCamAlignTool.resultPos.U.ToString();

                                //////    Win_DownCamAlignTool.Instance.tbx_placePosX.Text = robotDownCamAlignTool.placePos.Point.X.ToString();
                                //////    Win_DownCamAlignTool.Instance.tbx_placePosY.Text = robotDownCamAlignTool.placePos.Point.Y.ToString();
                                //////    Win_DownCamAlignTool.Instance.tbx_placePosU.Text = robotDownCamAlignTool.placePos.U.ToString();

                                //////    break;
                                //////#endregion

                                //////#region RotatePlatform
                                //////case ToolType.RotatePlatform:
                                //////    Win_RotatePlatformTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("旋转平台    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //Win_RotatePlatformTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //Win_RotatePlatformTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_RotatePlatformTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_RotatePlatformTool.Instance.TopMost = true;
                                //////    Win_RotatePlatformTool.Instance.Activate();
                                //////    Win_RotatePlatformTool.Instance.jobName = this.jobName;
                                //////    Win_RotatePlatformTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_RotatePlatformTool.Instance.Show();
                                //////    Win_RotatePlatformTool.Instance.WindowState = FormWindowState.Normal;
                                //////    Win_RotatePlatformTool.Instance.btn_runTool.Focus();
                                //////    RotatePlatformTool rotatePlatformTool = (RotatePlatformTool)(L_toolList[i].tool);
                                //////    Win_RotatePlatformTool.rotatePlatformTool = rotatePlatformTool;
                                //////    Application.DoEvents();

                                //////    Win_RotatePlatformTool.Instance.ckb_toolEnable.Checked = L_toolList[i].enable;

                                //////    Win_RotatePlatformTool.Instance.tbx_rotateCenterX.Text = rotatePlatformTool.rotateCenter.Point.X.ToString();
                                //////    Win_RotatePlatformTool.Instance.tbx_rotateCenterY.Text = rotatePlatformTool.rotateCenter.Point.Y.ToString();

                                //////    Win_RotatePlatformTool.Instance.tbx_inputPointX.Text = rotatePlatformTool.inputPos.Point.X.ToString();
                                //////    Win_RotatePlatformTool.Instance.tbx_inputPointY.Text = rotatePlatformTool.inputPos.Point.Y.ToString();
                                //////    Win_RotatePlatformTool.Instance.tbx_inputPointU.Text = rotatePlatformTool.inputPos.U.ToString();

                                //////    Win_RotatePlatformTool.Instance.tbx_outputPointX.Text = rotatePlatformTool.outputPos.Point.X.ToString();
                                //////    Win_RotatePlatformTool.Instance.tbx_outputPointY.Text = rotatePlatformTool.outputPos.Point.Y.ToString();
                                //////    Win_RotatePlatformTool.Instance.tbx_outputPointU.Text = rotatePlatformTool.outputPos.U.ToString();

                                //////    //显示数据
                                //////    for (int j = 0; j < rotatePlatformTool.L_calibData.Count; j++)
                                //////    {
                                //////        for (int k = 0; k < 3; k++)
                                //////        {
                                //////            Win_RotatePlatformTool.Instance.dgv_data.Rows[j].Cells[k].Value = rotatePlatformTool.L_calibData[j][k];
                                //////        }
                                //////    }

                                //////    //流程列表
                                //////    Win_RotatePlatformTool.Instance.cbx_jobList.Items.Clear();
                                //////    for (int j = 0; j < Project.Instance.curEngine.L_jobList.Count; j++)
                                //////    {
                                //////        Win_RotatePlatformTool.Instance.cbx_jobList.Items.Add(Project.Instance.curEngine.L_jobList[j].jobName);
                                //////    }
                                //////    Win_RotatePlatformTool.Instance.cbx_jobList.Text = rotatePlatformTool.calibJobName;
                                //////    Win_RotatePlatformTool.Instance.cbx_outputItemList.Text = rotatePlatformTool.calibItemName;

                                //////    break;
                                //////#endregion

                                //////#region XYPlatform
                                //////case ToolType.XYPlatform:
                                //////    Win_XYPlatformTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("XY平台    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //Win_XYPlatformTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //Win_XYPlatformTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_XYPlatformTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_XYPlatformTool.Instance.TopMost = true;
                                //////    Win_XYPlatformTool.Instance.Activate();
                                //////    Win_XYPlatformTool.Instance.jobName = this.jobName;
                                //////    Win_XYPlatformTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_XYPlatformTool.Instance.Show();
                                //////    Win_XYPlatformTool.Instance.WindowState = FormWindowState.Normal;
                                //////    Win_XYPlatformTool.Instance.btn_runTool.Focus();
                                //////    XYPlatformTool xyPlatformTool = (XYPlatformTool)(L_toolList[i].tool);
                                //////    Win_XYPlatformTool.xyPlatformTool = xyPlatformTool;
                                //////    Application.DoEvents();

                                //////    Win_XYPlatformTool.Instance.ckb_toolEnable.Checked = L_toolList[i].enable;

                                //////    Win_XYPlatformTool.Instance.tbx_pickPosX.Text = xyPlatformTool.pickPos.Point.X.ToString();
                                //////    Win_XYPlatformTool.Instance.tbx_pickPosY.Text = xyPlatformTool.pickPos.Point.Y.ToString();
                                //////    Win_XYPlatformTool.Instance.tbx_pickPosU.Text = xyPlatformTool.pickPos.U.ToString();

                                //////    Win_XYPlatformTool.Instance.tbx_pickPosOffsetX.Text = xyPlatformTool.pickPosOffset.Point.X.ToString();
                                //////    Win_XYPlatformTool.Instance.tbx_pickPosOffsetY.Text = xyPlatformTool.pickPosOffset.Point.Y.ToString();
                                //////    Win_XYPlatformTool.Instance.tbx_pickPosOffsetU.Text = xyPlatformTool.pickPosOffset.U.ToString();

                                //////    Win_XYPlatformTool.Instance.tbx_featureX.Text = xyPlatformTool.featurePos.Point.X.ToString();
                                //////    Win_XYPlatformTool.Instance.tbx_featureY.Text = xyPlatformTool.featurePos.Point.Y.ToString();
                                //////    Win_XYPlatformTool.Instance.tbx_featureU.Text = xyPlatformTool.featurePos.U.ToString();

                                //////    Win_XYPlatformTool.Instance.tbx_inputPointX.Text = xyPlatformTool.inputPos.Point.X.ToString();
                                //////    Win_XYPlatformTool.Instance.tbx_inputPointY.Text = xyPlatformTool.inputPos.Point.Y.ToString();
                                //////    Win_XYPlatformTool.Instance.tbx_inputPointU.Text = xyPlatformTool.inputPos.U.ToString();

                                //////    Win_XYPlatformTool.Instance.tbx_outputPointX.Text = xyPlatformTool.outputPos.Point.X.ToString();
                                //////    Win_XYPlatformTool.Instance.tbx_outputPointY.Text = xyPlatformTool.outputPos.Point.Y.ToString();
                                //////    Win_XYPlatformTool.Instance.tbx_outputPointU.Text = xyPlatformTool.outputPos.U.ToString();

                                //////    break;
                                //////#endregion

                                //////#region 点位引导
                                //////case ToolType.PointAlign:
                                //////    Win_PointAlignTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("点位引导    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //Win_PointAlignTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //Win_PointAlignTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_PointAlignTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_PointAlignTool.Instance.TopMost = true;
                                //////    Win_PointAlignTool.Instance.Activate();
                                //////    Win_PointAlignTool.Instance.jobName = this.jobName;
                                //////    Win_PointAlignTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_PointAlignTool.Instance.Show();
                                //////    Win_PointAlignTool.Instance.WindowState = FormWindowState.Normal;
                                //////    Win_PointAlignTool.Instance.btn_runTool.Focus();
                                //////    PointAlignTool pointAlignTool = (PointAlignTool)(L_toolList[i].tool);
                                //////    Win_PointAlignTool.pointAlignTool = pointAlignTool;
                                //////    Application.DoEvents();

                                //////    Win_PointAlignTool.Instance.cbx_toolEnable.Checked = L_toolList[i].enable;

                                //////    Win_PointAlignTool.Instance.cbx_toolList.Items.Clear();
                                //////    Win_PointAlignTool.Instance.cbx_toolList.Items.AddRange(pointAlignTool.L_toolName.ToArray());
                                //////    Win_PointAlignTool.Instance.cbx_toolList.SelectedIndex = pointAlignTool.toolIdx;

                                //////    Win_PointAlignTool.Instance.tbx_inputPosX.Text = pointAlignTool.inputPos.Point.X.ToString();
                                //////    Win_PointAlignTool.Instance.tbx_inputPosY.Text = pointAlignTool.inputPos.Point.Y.ToString();
                                //////    Win_PointAlignTool.Instance.tbx_inputPosU.Text = pointAlignTool.inputPos.U.ToString();

                                //////    Win_PointAlignTool.Instance.tbx_pickPosX.Text = pointAlignTool.L_workPos[pointAlignTool.toolIdx].Point.X.ToString();
                                //////    Win_PointAlignTool.Instance.tbx_pickPosY.Text = pointAlignTool.L_workPos[pointAlignTool.toolIdx].Point.Y.ToString();

                                //////    Win_PointAlignTool.Instance.tbx_featureX.Text = pointAlignTool.L_featurePos[pointAlignTool.toolIdx].Point.X.ToString();
                                //////    Win_PointAlignTool.Instance.tbx_featureY.Text = pointAlignTool.L_featurePos[pointAlignTool.toolIdx].Point.Y.ToString();
                                //////    Win_PointAlignTool.Instance.tbx_featureU.Text = pointAlignTool.L_featurePos[pointAlignTool.toolIdx].U.ToString();

                                //////    Win_PointAlignTool.Instance.tbx_resultPosX.Text = pointAlignTool.resultPos.Point.X.ToString();
                                //////    Win_PointAlignTool.Instance.tbx_resultPosY.Text = pointAlignTool.resultPos.Point.Y.ToString();

                                //////    Win_PointAlignTool.Instance.tbx_pickPosOffsetX.Text = pointAlignTool.L_workPosOffset[pointAlignTool.toolIdx].Point.X.ToString();
                                //////    Win_PointAlignTool.Instance.tbx_pickPosOffsetY.Text = pointAlignTool.L_workPosOffset[pointAlignTool.toolIdx].Point.Y.ToString();

                                //////    Win_PointAlignTool.Instance.tbx_saftyRangeX.Text = pointAlignTool.L_safetyRange[pointAlignTool.toolIdx].Point.X.ToString();
                                //////    Win_PointAlignTool.Instance.tbx_saftyRangeY.Text = pointAlignTool.L_safetyRange[pointAlignTool.toolIdx].Point.Y.ToString();

                                //////    break;
                                //////#endregion

                                //////#region AlignFit
                                //////case ToolType.AlignFit:
                                //////    Win_AlignFitTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("对位组装    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //////Win_AlignFitTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //////Win_AlignFitTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_UpCamAlignTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_AlignFitTool.Instance.TopMost = true;
                                //////    Win_AlignFitTool.Instance.Activate();
                                //////    Win_AlignFitTool.Instance.jobName = this.jobName;
                                //////    Win_AlignFitTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_AlignFitTool.Instance.Show();
                                //////    Win_AlignFitTool.Instance.WindowState = FormWindowState.Normal;
                                //////    Win_AlignFitTool.Instance.button5.Focus();
                                //////    AlignFitTool alignFitTool = (AlignFitTool)(L_toolList[i].tool);
                                //////    Win_AlignFitTool.alignFitTool = alignFitTool;
                                //////    Application.DoEvents();

                                //////    Win_AlignFitTool.Instance.pictureBox2.Image = L_toolList[i].enable ? Resources.开 : Resources.关;



                                //////    Win_AlignFitTool.Instance.tbx_inputPosX.Text = alignFitTool.inputPos.Point.X.ToString();
                                //////    Win_AlignFitTool.Instance.tbx_inputPosY.Text = alignFitTool.inputPos.Point.Y.ToString();
                                //////    Win_AlignFitTool.Instance.tbx_inputPosU.Text = alignFitTool.inputPos.U.ToString();

                                //////    Win_AlignFitTool.Instance.tbx_pickPosX.Text = alignFitTool.TemplateElementPlacePos.Point.X.ToString();
                                //////    Win_AlignFitTool.Instance.tbx_pickPosY.Text = alignFitTool.TemplateElementPlacePos.Point.Y.ToString();
                                //////    Win_AlignFitTool.Instance.tbx_pickPosU.Text = alignFitTool.TemplateElementPlacePos.U.ToString();

                                //////    Win_AlignFitTool.Instance.tbx_featureX.Text = alignFitTool.TemplateBelowBoardPos.Point.X.ToString();
                                //////    Win_AlignFitTool.Instance.tbx_featureY.Text = alignFitTool.TemplateBelowBoardPos.Point.Y.ToString();
                                //////    Win_AlignFitTool.Instance.tbx_featureU.Text = alignFitTool.TemplateBelowBoardPos.U.ToString();

                                //////    Win_AlignFitTool.Instance.tbx_resultPosX.Text = alignFitTool.resultPos.Point.X.ToString();
                                //////    Win_AlignFitTool.Instance.tbx_resultPosY.Text = alignFitTool.resultPos.Point.Y.ToString();
                                //////    Win_AlignFitTool.Instance.tbx_resultPosU.Text = alignFitTool.resultPos.U.ToString();

                                //////    Win_AlignFitTool.Instance.tbx_pickPosOffsetX.Text = alignFitTool.TemplateElementPlacePosOffset.Point.X.ToString();
                                //////    Win_AlignFitTool.Instance.tbx_pickPosOffsetY.Text = alignFitTool.TemplateElementPlacePosOffset.Point.Y.ToString();
                                //////    Win_AlignFitTool.Instance.tbx_pickPosOffsetU.Text = alignFitTool.TemplateElementPlacePosOffset.U.ToString();

                                //////    Win_AlignFitTool.Instance.tbx_saftyRangeX.Text = alignFitTool.L_safetyRange.Point.X.ToString();
                                //////    Win_AlignFitTool.Instance.tbx_saftyRangeY.Text = alignFitTool.L_safetyRange.Point.Y.ToString();
                                //////    Win_AlignFitTool.Instance.tbx_saftyRangeU.Text = alignFitTool.L_safetyRange.U.ToString();

                                //////    break;
                                //////#endregion

                                //////#region AlignWithoutCalibRotateCenter
                                //////case ToolType.AlignWithoutCalibRotateCenter:
                                //////    Win_AlignWithoutCalibRotateCenterTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("上相机定位    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //Win_UpCamAlignTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //Win_UpCamAlignTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_UpCamAlignTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_UpCamAlignTool.Instance.TopMost = true;
                                //////    Win_AlignWithoutCalibRotateCenterTool.Instance.Activate();
                                //////    Win_AlignWithoutCalibRotateCenterTool.Instance.jobName = this.jobName;
                                //////    Win_AlignWithoutCalibRotateCenterTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_AlignWithoutCalibRotateCenterTool.Instance.Show();
                                //////    Win_AlignWithoutCalibRotateCenterTool.Instance.WindowState = FormWindowState.Normal;
                                //////    Win_AlignWithoutCalibRotateCenterTool.Instance.btn_runTool.Focus();
                                //////    AlignWithoutCalibRotateCenterTool alignWithoutCalibRotateCenterTool = (AlignWithoutCalibRotateCenterTool)(L_toolList[i].tool);
                                //////    Win_AlignWithoutCalibRotateCenterTool.alignWithoutCalibRotateCenterTool = alignWithoutCalibRotateCenterTool;
                                //////    Application.DoEvents();

                                //////    Win_UpCamAlignTool.Instance.cbx_toolEnable.Checked = L_toolList[i].enable;

                                //////    //////Win_UpCamAlignTool.Instance.cbx_toolList.Items.Clear();
                                //////    //////Win_UpCamAlignTool.Instance.cbx_toolList.Items.AddRange(upCamAlignTool.L_toolName.ToArray());
                                //////    //////Win_UpCamAlignTool.Instance.cbx_toolList.SelectedIndex = upCamAlignTool.toolIdx;

                                //////    //////Win_UpCamAlignTool.Instance.tbx_inputPosX.Text = upCamAlignTool.inputPos.Point.X.ToString();
                                //////    //////Win_UpCamAlignTool.Instance.tbx_inputPosY.Text = upCamAlignTool.inputPos.Point.Y.ToString();
                                //////    //////Win_UpCamAlignTool.Instance.tbx_inputPosU.Text = upCamAlignTool.inputPos.U.ToString();

                                //////    //////Win_UpCamAlignTool.Instance.tbx_pickPosX.Text = upCamAlignTool.L_pickPos[upCamAlignTool.toolIdx].Point.X.ToString();
                                //////    //////Win_UpCamAlignTool.Instance.tbx_pickPosY.Text = upCamAlignTool.L_pickPos[upCamAlignTool.toolIdx].Point.Y.ToString();
                                //////    //////Win_UpCamAlignTool.Instance.tbx_pickPosU.Text = upCamAlignTool.L_pickPos[upCamAlignTool.toolIdx].U.ToString();

                                //////    //////Win_UpCamAlignTool.Instance.tbx_featureX.Text = upCamAlignTool.L_featurePos[upCamAlignTool.toolIdx].Point.X.ToString();
                                //////    //////Win_UpCamAlignTool.Instance.tbx_featureY.Text = upCamAlignTool.L_featurePos[upCamAlignTool.toolIdx].Point.Y.ToString();
                                //////    //////Win_UpCamAlignTool.Instance.tbx_featureU.Text = upCamAlignTool.L_featurePos[upCamAlignTool.toolIdx].U.ToString();

                                //////    //////Win_UpCamAlignTool.Instance.tbx_resultPosX.Text = upCamAlignTool.resultPos.Point.X.ToString();
                                //////    //////Win_UpCamAlignTool.Instance.tbx_resultPosY.Text = upCamAlignTool.resultPos.Point.Y.ToString();
                                //////    //////Win_UpCamAlignTool.Instance.tbx_resultPosU.Text = upCamAlignTool.resultPos.U.ToString();

                                //////    //////Win_UpCamAlignTool.Instance.tbx_pickPosOffsetX.Text = upCamAlignTool.L_pickPosOffset[upCamAlignTool.toolIdx].Point.X.ToString();
                                //////    //////Win_UpCamAlignTool.Instance.tbx_pickPosOffsetY.Text = upCamAlignTool.L_pickPosOffset[upCamAlignTool.toolIdx].Point.Y.ToString();
                                //////    //////Win_UpCamAlignTool.Instance.tbx_pickPosOffsetU.Text = upCamAlignTool.L_pickPosOffset[upCamAlignTool.toolIdx].U.ToString();

                                //////    //////Win_UpCamAlignTool.Instance.tbx_saftyRangeX.Text = upCamAlignTool.L_safetyRange[upCamAlignTool.toolIdx].Point.X.ToString();
                                //////    //////Win_UpCamAlignTool.Instance.tbx_saftyRangeY.Text = upCamAlignTool.L_safetyRange[upCamAlignTool.toolIdx].Point.Y.ToString();
                                //////    //////Win_UpCamAlignTool.Instance.tbx_saftyRangeU.Text = upCamAlignTool.L_safetyRange[upCamAlignTool.toolIdx].U.ToString();

                                //////    break;
                                //////#endregion

                                //#region FindLine
                                //case ToolType.FindLine:




                                //    FindLineTool findLineTool = (FindLineTool)L_toolList[i].tool;

                                //    if (findLineTool.toolPar.InputPar.图像 != null)
                                //         GetImageWindowControl().HobjectToHimage(findLineTool.toolPar.InputPar.图像);


                                //    if (findLineTool.toolPar.InputPar.图像 != null)
                                //         GetImageWindowControl().HobjectToHimage(findLineTool.toolPar.InputPar.图像);



                                //    findLineTool.Run(true, false, L_toolList[i].toolName);





                                //    break;
                                //#endregion

                                //#region FindCircle
                                //case ToolType.FindCircle:

                                //    FindCircleTool findCircleTool = (FindCircleTool)L_toolList[i].tool;

                                //    if (findCircleTool.toolPar.InputPar.图像 != null)
                                //         GetImageWindowControl().HobjectToHimage(findCircleTool.toolPar.InputPar.图像);


                                //    if (findCircleTool.toolPar.InputPar.图像 != null)
                                //         GetImageWindowControl().HobjectToHimage(findCircleTool.toolPar.InputPar.图像);



                                //    findCircleTool.Run(true, false, L_toolList[i].toolName);










                                //    break;
                                //#endregion

                                //////#region SubImage
                                //////case ToolType.SubImage:
                                //////    Win_SubImageTool.Instance.pictureBox1.Image = Resources.SubImageTool;
                                //////    Win_SubImageTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("减图像    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //Win_SubImageTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //Win_SubImageTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_SubImageTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_SubImageTool.Instance.TopMost = true;
                                //////    Win_SubImageTool.Instance.Activate();
                                //////    Win_SubImageTool.Instance.jobName = Win_ToolBox.Instance.listView1.SelectedItems[0].Text;
                                //////    Win_SubImageTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_SubImageTool.Instance.Show();
                                //////    Win_SubImageTool.Instance.WindowState = FormWindowState.Normal;
                                //////    Win_SubImageTool.Instance.btn_runImageSubTool.Focus();
                                //////    SubImageTool subImageTool = (SubImageTool)(L_toolList[i].tool);
                                //////    Win_SubImageTool.subImageTool = subImageTool;
                                //////    Application.DoEvents();

                                //////    if (subImageTool.inputImage != null)
                                //////        subImageTool.ShowImage(subImageTool.inputImage);
                                //////    else
                                //////        //////subImageTool.ClearWindow(jobName);

                                //////        Win_SubImageTool.Instance.ckb_subImageToolEnable.Checked = L_toolList[i].enable;
                                //////    Win_SubImageTool.Instance.cbx_standardImage.Text = subImageTool.standardImageName;
                                //////    break;
                                //////#endregion

                                //////#region CreateROI
                                //////case ToolType.CreateROI:
                                //////    Win_CreateROITool.Instance.pictureBox1.Image = Resources.CreateROITool;
                                //////    Win_CreateROITool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("创建ROI    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //Win_CreateROITool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //Win_CreateROITool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_CreateROITool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_CreateROITool.Instance.TopMost = true;
                                //////    Win_CreateROITool.Instance.Activate();
                                //////    Win_CreateROITool.Instance.jobName = this.jobName;
                                //////    Win_CreateROITool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_CreateROITool.Instance.Show();
                                //////    Win_CreateROITool.Instance.WindowState = FormWindowState.Normal;
                                //////    //////Win_CreateROITool.Instance.btn_runFindCircleTool.Focus();
                                //////    CreateROITool createROITool = (CreateROITool)(L_toolList[i].tool);
                                //////    Win_CreateROITool.createROITool = createROITool;
                                //////    Application.DoEvents();


                                //////    inputItemNum = (L_toolList[i]).input.Count;
                                //////    for (int j = 0; j < inputItemNum; j++)
                                //////    {
                                //////        string inputItem = L_toolList[i].input[j].IOName;
                                //////        string sourceFrom = L_toolList[i].GetInput(inputItem).value.ToString(); if (inputItem == string.Empty)
                                //////        {

                                //////        }

                                //////        //////createROITool.inputPose = null;
                                //////        if (inputItem == "左上点行")
                                //////        {
                                //////            string sourceToolName = sourceFrom.Split(new char[] { '.' })[0];
                                //////            sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, "->")[0].Length - 3);
                                //////            string toolItem = Regex.Split(sourceFrom, "->")[1];
                                //////            createROITool.leftTopRow = Convert.ToInt16(Convert.ToDouble(FindToolInfoByName(sourceToolName).GetOutput(toolItem).value));
                                //////        }
                                //////        else if (inputItem == "左上点列")
                                //////        {
                                //////            string sourceToolName = sourceFrom.Split(new char[] { '.' })[0];
                                //////            sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, "->")[0].Length - 3);
                                //////            string toolItem = Regex.Split(sourceFrom, "->")[1];
                                //////            createROITool.leftTopCol = Convert.ToInt16(Convert.ToDouble(FindToolInfoByName(sourceToolName).GetOutput(toolItem).value));
                                //////        }
                                //////        else if (inputItem == "右下点行" || inputItem == "ExpectCircleCenterX")
                                //////        {
                                //////            string sourceToolName = Regex.Split(sourceFrom, "->")[0];
                                //////            sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, "->")[0].Length - 3);
                                //////            string toolItem = Regex.Split(sourceFrom, "->")[1];
                                //////            createROITool.rightDownRow = Convert.ToInt16(Convert.ToDouble(FindToolInfoByName(sourceToolName).GetOutput(toolItem).value));
                                //////        }
                                //////        else if (inputItem == "右下点列" || inputItem == "ExpectCircleCenterY")
                                //////        {
                                //////            string sourceToolName = Regex.Split(sourceFrom, "->")[0];
                                //////            sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, "->")[0].Length - 3);
                                //////            string toolItem = Regex.Split(sourceFrom, "->")[1];
                                //////            createROITool.rightDownCol = Convert.ToInt16(Convert.ToDouble(FindToolInfoByName(sourceToolName).GetOutput(toolItem).value));
                                //////        }
                                //////        else if (inputItem == "跟随" || inputItem == "ExpectCircleCenterY")
                                //////        {
                                //////            string sourceToolName = Regex.Split(sourceFrom, "->")[0];
                                //////            sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, "->")[0].Length - 3);
                                //////            string toolItem = Regex.Split(sourceFrom, "->")[1];
                                //////            createROITool.inputPose = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as XYU;
                                //////        }
                                //////        else if (inputItem == "图像" || inputItem == "ExpectC信息ircleCenterY")
                                //////        {
                                //////            string sourceToolName = Regex.Split(sourceFrom, "->")[0];
                                //////            sourceToolName = sourceToolName.Substring(3, Regex.Split(sourceFrom, "->")[0].Length - 3);
                                //////            string toolItem = Regex.Split(sourceFrom, "->")[1];
                                //////            createROITool.toolPar.InputPar.图像 = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as HObject;
                                //////        }
                                //////    }




                                //////    //显示背景图
                                //////    Win_CreateROITool.Instance.hWindow_Final1.HobjectToHimage(createROITool.toolPar.InputPar.图像);


                                //////    Win_CreateROITool.Instance.hWindow_Final1.viewWindow.displayROI(createROITool.regions);
                                //////    Win_CreateROITool.regions = createROITool.regions;

                                //////    Win_CreateROITool.binDataGridView(Win_CreateROITool.Instance.dgv_ROI, createROITool.regions);


                                //////    //将对象信息更新到界面
                                //////    //////Win_CreateROITool.Instance.ckb_createROIToolEnable.Checked = L_toolList[i].enable;
                                //////    //////Win_CreateROITool.Instance.tbx_leftTopRow.Text = createROITool.leftTopRow.ToString();
                                //////    //////Win_CreateROITool.Instance.tbx_leftTopCol.Text = createROITool.leftTopCol.ToString();
                                //////    //////Win_CreateROITool.Instance.tbx_rightDownRow.Text = createROITool.rightDownRow.ToString();
                                //////    //////Win_CreateROITool.Instance.tbx_rightDownCol.Text = createROITool.rightDownCol.ToString();
                                //////    break;
                                //////#endregion

                                //////#region ArrayRegion
                                //////case ToolType.ArrayRegion:
                                //////    Win_ArrayRegionTool.Instance.pictureBox1.Image = Resources.RegionArrayTool;
                                //////    Win_ArrayRegionTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("阵列区域    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //Win_ArrayRegionTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //Win_ArrayRegionTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_ArrayRegionTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_ArrayRegionTool.Instance.TopMost = true;
                                //////    Win_ArrayRegionTool.Instance.Activate();
                                //////    Win_ArrayRegionTool.Instance.jobName = this.jobName;
                                //////    Win_ArrayRegionTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_ArrayRegionTool.Instance.Show();
                                //////    Win_ArrayRegionTool.Instance.WindowState = FormWindowState.Normal;
                                //////    //////Win_CreateROITool.Instance.btn_runDistancePLTool.Focus();
                                //////    ArrayRegionTool arrayRegionTool = (ArrayRegionTool)(L_toolList[i].tool);
                                //////    Win_ArrayRegionTool.arrayRegionTool = arrayRegionTool;
                                //////    Application.DoEvents();

                                //////    //将对象信息更新到界面
                                //////    if (arrayRegionTool.inputImage != null)
                                //////        arrayRegionTool.ShowImage(arrayRegionTool.inputImage);
                                //////    else
                                //////        arrayRegionTool.ClearWindow();

                                //////    if (arrayRegionTool.outputRegion != null)
                                //////    {
                                //////        GetImageWindowControl().hwc_imageWindow.viewWindow.displayROI(arrayRegionTool.regions);
                                //////        GetImageWindowControl().regions = arrayRegionTool.regions;
                                //////    }

                                //////    Win_ArrayRegionTool.Instance.ckb_shapeMatchToolEnable.Checked = L_toolList[i].enable;
                                //////    Win_ArrayRegionTool.Instance.textBox1.Text = arrayRegionTool.rowNum.ToString();
                                //////    Win_ArrayRegionTool.Instance.textBox2.Text = arrayRegionTool.colNum.ToString();
                                //////    Win_ArrayRegionTool.Instance.textBox3.Text = arrayRegionTool.rowSpan.ToString();
                                //////    Win_ArrayRegionTool.Instance.textBox4.Text = arrayRegionTool.colSpan.ToString();

                                //////    break;
                                //////#endregion

                                //////#region Mark
                                //////case ToolType.Mark:
                                //////    Win_MarkTool.Instance.pictureBox1.Image = Resources.MarkTool;
                                //////    Win_MarkTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("标记点    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //Win_MarkTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //Win_MarkTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_MarkTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_MarkTool.Instance.TopMost = true;
                                //////    Win_MarkTool.Instance.Activate();
                                //////    Win_MarkTool.Instance.jobName = this.jobName;
                                //////    Win_MarkTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_MarkTool.Instance.Show();
                                //////    Win_MarkTool.Instance.WindowState = FormWindowState.Normal;
                                //////    //////Win_MarkTool.Instance.btn_runDownCamAlignTool.Focus();
                                //////    MarkTool markTool = (MarkTool)(L_toolList[i].tool);
                                //////    Win_MarkTool.markTool = markTool;
                                //////    Application.DoEvents();

                                //////    Win_MarkTool.Instance.ckb_shapeMatchToolEnable.Checked = L_toolList[i].enable;
                                //////    Win_MarkTool.Instance.tbx_caputurePosX.Text = markTool.inputPoint.X.ToString();
                                //////    Win_MarkTool.Instance.tbx_caputurePosY.Text = markTool.inputPoint.Y.ToString();

                                //////    break;
                                //////#endregion

                                //////#region PoseToStr
                                //////case ToolType.ToStr:
                                //////    Win_PoseToStrTool.Instance.pictureBox1.Image = Resources.UnknownTool;
                                //////    Win_PoseToStrTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("转文本    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //////Win_PoseToStrTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //////Win_PoseToStrTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_PoseToStrTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_PoseToStrTool.Instance.TopMost = true;
                                //////    Win_PoseToStrTool.Instance.Activate();
                                //////    Win_PoseToStrTool.Instance.jobName = this.jobName;
                                //////    Win_PoseToStrTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_PoseToStrTool.Instance.Show();
                                //////    Win_PoseToStrTool.Instance.WindowState = FormWindowState.Normal;
                                //////    //////Win_MarkTool.Instance.btn_runDownCamAlignTool.Focus();
                                //////    ToStrTool poseToStrTool = (ToStrTool)(L_toolList[i].tool);
                                //////    Win_PoseToStrTool.poseToStrTool = poseToStrTool;
                                //////    Application.DoEvents();

                                //////    //////Win_PoseToStrTool.Instance.ckb_toolEnable.Checked = L_toolList[i].enable;
                                //////    Win_PoseToStrTool.Instance.textBox1.Text = poseToStrTool.splitChar;

                                //////    break;
                                //////#endregion

                                //////#region DistancePL
                                //////case ToolType.DistancePL:
                                //////    Win_DistancePLTool.Instance.pictureBox1.Image = Resources.DistancePLTool;
                                //////    Win_DistancePLTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("点线距离    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //////Win_DistancePLTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //////Win_DistancePLTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_DistancePLTool.Instance.Width - 2, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_DistancePLTool.Instance.TopMost = true;
                                //////    Win_DistancePLTool.Instance.Activate();
                                //////    Win_DistancePLTool.Instance.jobName = this.jobName;
                                //////    Win_DistancePLTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_DistancePLTool.Instance.Show();
                                //////    Win_DistancePLTool.Instance.WindowState = FormWindowState.Normal;
                                //////    //  Win_DistancePointLineTool.Instance.dd.Focus();
                                //////    DistancePLTool distancePLTool = (DistancePLTool)(L_toolList[i].tool);
                                //////    Application.DoEvents();

                                //////    if (((DistancePLTool)(L_toolList[i].tool)).inputImage != null)
                                //////        GetImageWindowControl().Display_Image(((DistancePLTool)(L_toolList[i].tool)).inputImage);
                                //////    else
                                //////        HOperatorSet.ClearWindow(Win_ImageWindow.Instance.WindowHandle);

                                //////    //将对象信息更新到界面
                                //////    Win_DistancePLTool.Instance.ckb_distancePLToolEnable.Checked = L_toolList[i].enable;

                                //////    break;
                                //////#endregion

                                //////#region DistanceSS
                                //////case ToolType.DistanceSS:
                                //////    Win_DistanceLLTool.Instance.pictureBox1.Image = Resources.UnknownTool;
                                //////    Win_DistanceLLTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("线段与线段距离    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //////Win_DistanceLLTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //////Win_DistanceLLTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_DistanceLLTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_DistanceLLTool.Instance.TopMost = true;
                                //////    Win_DistanceLLTool.Instance.Activate();
                                //////    Win_DistanceLLTool.Instance.jobName = this.jobName;
                                //////    Win_DistanceLLTool.Instance.toolName = L_toolList[i].toolName;
                                //////    ////// Win_DistanceSegmentAndSegmentTool.Instance.Show();
                                //////    Win_DistanceLLTool.Instance.WindowState = FormWindowState.Normal;
                                //////    //////SharpEdit.Form1.Instance.fctb.Focus();
                                //////    DistanceLLTool distanceSSTool = (DistanceLLTool)(L_toolList[i].tool);
                                //////    Application.DoEvents();

                                //////    //将对象信息更新到界面
                                //////    Win_MessageBox.Instance.MessageBoxShow("\r\n本工具为无窗体工具！");
                                //////    break;
                                //////#endregion

                                //#region LLPoint
                                //case ToolType.LLIntersect:
                                //    LLIntersectTool llIntersectTool = (LLIntersectTool)L_toolList[i].tool;


                                //    llIntersectTool.Run(true, false, L_toolList[i].toolName);

                                //    break;
                                //#endregion



                                //////#region RegionFeature
                                //////case ToolType.RegionFeature:
                                //////    Win_RegionFeatureTool.Instance.pictureBox1.Image = Resources.RegionFeatureTool;
                                //////    Win_RegionFeatureTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("区域特征    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //Win_RegionFeatureTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //Win_RegionFeatureTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_RegionFeatureTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_RegionFeatureTool.Instance.TopMost = true;
                                //////    Win_RegionFeatureTool.Instance.Activate();
                                //////    Win_RegionFeatureTool.Instance.jobName = this.jobName;
                                //////    Win_RegionFeatureTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_RegionFeatureTool.Instance.Show();
                                //////    Win_RegionFeatureTool.Instance.WindowState = FormWindowState.Normal;
                                //////    //////Win_RegionFeatureTool.Instance.btn_runFindBarcodeTool.Focus();
                                //////    RegionFeatureTool regionFeatureTool = (RegionFeatureTool)(L_toolList[i].tool);
                                //////    Win_RegionFeatureTool.regionFeatureTool = regionFeatureTool;
                                //////    Application.DoEvents();

                                //////    break;
                                //////#endregion

                                //////#region OCR
                                //////case ToolType.OCR:
                                //////    Win_OCRTool.Instance.pictureBox1.Image = Resources.OCRTool;
                                //////    Win_OCRTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("OCR    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //Win_OCRTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //Win_OCRTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_ShapeMatchTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_OCRTool.Instance.TopMost = true;
                                //////    Win_OCRTool.Instance.Activate();
                                //////    Win_OCRTool.Instance.jobName = this.jobName;
                                //////    Win_OCRTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_OCRTool.Instance.Show();
                                //////    Win_OCRTool.Instance.WindowState = FormWindowState.Normal;
                                //////    Win_OCRTool.Instance.btn_runOCRTool.Focus();
                                //////    OCRTool ocrTool = (OCRTool)(L_toolList[i].tool);
                                //////    Win_OCRTool.ocrTool = ocrTool;
                                //////    Application.DoEvents();

                                //////    if (ocrTool.inputImage != null)
                                //////        ocrTool.ShowImage(ocrTool.inputImage);
                                //////    else
                                //////        ////////ocrTool.ClearWindow(this.jobName);

                                //////        if (ocrTool.searchRegion != null)
                                //////        {
                                //////            ocrTool.SetColor(this.jobName, "blue");
                                //////            //////ocrTool.ShowObj(this.jobName, ocrTool.searchRegion);
                                //////        }

                                //////    Win_OCRTool.Instance.lbl_threshold.Text = ocrTool.threshold.ToString();
                                //////    Win_OCRTool.Instance.tkb_threshold.Value = ocrTool.threshold;
                                //////    Win_OCRTool.Instance.ckb_OCRToolEnable.Checked = L_toolList[i].enable;
                                //////    Win_OCRTool.Instance.cbx_searchRegionType.Text = ocrTool.searchRegionType.ToString();
                                //////    Win_OCRTool.Instance.cbx_templateRegionType.Text = ocrTool.templateRegionType.ToString();
                                //////    Win_OCRTool.Instance.tbx_resultStr.Text = ocrTool.outputStr;
                                //////    Win_OCRTool.Instance.cbx_charType.SelectedIndex = (ocrTool.charType == CharType.BlackChar ? 0 : 1);
                                //////    Win_OCRTool.Instance.tbx_dilationSize.Text = ocrTool.dilationSize.ToString();
                                //////    Win_OCRTool.Instance.tbx_standardCharList.Text = ocrTool.standardCharList;

                                //////    break;
                                //////#endregion

                                //////#region Barcode
                                //////case ToolType.Barcode:
                                //////    Win_BarcodeTool.Instance.pictureBox1.Image = Resources.BarCodeTool;
                                //////    Win_BarcodeTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("条码    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //Win_BarcodeTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //Win_BarcodeTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_BarcodeTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_BarcodeTool.Instance.TopMost = true;
                                //////    Win_BarcodeTool.Instance.Activate();
                                //////    Win_BarcodeTool.Instance.jobName = this.jobName;
                                //////    Win_BarcodeTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_BarcodeTool.Instance.Show();
                                //////    Win_BarcodeTool.Instance.WindowState = FormWindowState.Normal;
                                //////    Win_BarcodeTool.Instance.btn_runFindBarcodeTool.Focus();
                                //////    BarcodeTool barcodeTool = (BarcodeTool)(L_toolList[i].tool);
                                //////    Win_BarcodeTool.barcodeTool = barcodeTool;
                                //////    Application.DoEvents();

                                //////    break;
                                //////#endregion

                                //////#region CodeEdit
                                //////case ToolType.CodeEdit:
                                //////    //////Win_CodeEditTool.Instance.Text = "脚本编辑 - " + this.jobName + "." + L_toolList[i].toolName;
                                //////    //////Win_CodeEditTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //////Win_CodeEditTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_CodeEditTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //////Win_CodeEditTool.Instance.TopMost = true;
                                //////    //////Win_CodeEditTool.Instance.Show();
                                //////    //////Win_CodeEditTool.Instance.WindowState = FormWindowState.Normal;
                                //////    //////Win_CodeEditTool.Instance.tbx_code.Focus();
                                //////    //////CodeEditTool codeEditTool = (CodeEditTool)(L_toolList[i].tool);
                                //////    //////Win_CodeEditTool.codeEditTool = codeEditTool;
                                //////    //////Application.DoEvents();

                                //////    //////Win_CodeEditTool.Instance.tbx_code.Text = ((CodeEditTool)L_toolList[i].tool).sourceCode;
                                //////    break;
                                //////#endregion

                                //////#region DataAnalyse
                                //////case ToolType.DataAnalyse:
                                //////    Win_DataAnalyseTool.Instance.pictureBox1.Image = Resources.LabelTool;
                                //////    Win_DataAnalyseTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("显示文本    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //Win_LabelTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //Win_LabelTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_LabelTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_LabelTool.Instance.TopMost = true;
                                //////    Win_DataAnalyseTool.Instance.Activate();
                                //////    Win_DataAnalyseTool.Instance.jobName = this.jobName;
                                //////    Win_DataAnalyseTool.Instance.toolName = L_toolList[i].toolName; ;
                                //////    Win_DataAnalyseTool.Instance.Show();
                                //////    Win_DataAnalyseTool.Instance.WindowState = FormWindowState.Normal;
                                //////    //Win_LabelTool.Instance.btn_runTool.Focus();
                                //////    DataAnalyseTool dataAnalyseTool = (DataAnalyseTool)(L_toolList[i].tool);
                                //////    Win_DataAnalyseTool.dataAnalyseTool = dataAnalyseTool;
                                //////    Application.DoEvents();

                                //////    int itemCount = ((DataAnalyseTool)L_toolList[i].tool).L_items.Count;
                                //////    Win_DataAnalyseTool.Instance.dgv_outputItem.Rows.Clear();
                                //////    for (int j = 0; j < itemCount; j++)
                                //////    {
                                //////        int index = Win_DataAnalyseTool.Instance.dgv_outputItem.Rows.Add();
                                //////        Win_DataAnalyseTool.Instance.dgv_outputItem.Rows[index].Cells[0].Value = dataAnalyseTool.L_items[j].inputItem;
                                //////        Win_DataAnalyseTool.Instance.dgv_outputItem.Rows[index].Cells[1].Value = dataAnalyseTool.L_items[j].downLimit.ToString().ToString();
                                //////        Win_DataAnalyseTool.Instance.dgv_outputItem.Rows[index].Cells[2].Value = dataAnalyseTool.L_items[j].upLimit.ToString();
                                //////        Win_DataAnalyseTool.Instance.dgv_outputItem.Rows[index].Cells[3].Value = dataAnalyseTool.L_items[j].inResult;
                                //////        Win_DataAnalyseTool.Instance.dgv_outputItem.Rows[index].Cells[4].Value = dataAnalyseTool.L_items[j].outResult;
                                //////    }

                                //////    //Win_LabelTool.Instance.ckb_toolEnable.Checked = ((ToolInfo)L_toolList[i]).enable;
                                //////    break;
                                //////#endregion

                                //////#region OPTLight
                                //////case ToolType.Light_OPT:
                                //////    Win_OPTLightTool.Instance.pictureBox1.Image = Resources.LightTool;
                                //////    Win_OPTLightTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("奥普特光源控制    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //Win_OPTLightTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //Win_OPTLightTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_OPTLightTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_OPTLightTool.Instance.TopMost = true;
                                //////    Win_OPTLightTool.Instance.Activate();
                                //////    Win_OPTLightTool.Instance.jobName = this.jobName;
                                //////    Win_OPTLightTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Light_OPTTool optLightTool = (Light_OPTTool)(L_toolList[i].tool);
                                //////    Win_OPTLightTool.optLightTool = optLightTool;
                                //////    Win_OPTLightTool.Instance.Show();
                                //////    Win_OPTLightTool.Instance.WindowState = FormWindowState.Normal;
                                //////    //Win_OPTLightTool.Instance.btn_runShapeMatchTool.Focus();
                                //////    Application.DoEvents();
                                //////    break;
                                //////#endregion

                                //////#region OPTLightControl
                                //////case ToolType.OPTLightControl:
                                //////    Win_OptLightControlTool.Instance.pictureBox1.Image = Resources.LightTool;
                                //////    Win_OptLightControlTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("光源控制    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //Win_OptLightControlTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //Win_OptLightControlTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_OptLightControlTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_OptLightControlTool.Instance.TopMost = true;
                                //////    Win_OptLightControlTool.Instance.Activate();
                                //////    Win_OptLightControlTool.Instance.jobName = this.jobName;
                                //////    Win_OptLightControlTool.Instance.toolName = L_toolList[i].toolName;
                                //////    OptLightControlTool optLightControlTool = (OptLightControlTool)(L_toolList[i].tool);
                                //////    Win_OptLightControlTool.optLightControlTool = optLightControlTool;
                                //////    Win_OptLightControlTool.Instance.Show();
                                //////    Win_OptLightControlTool.Instance.WindowState = FormWindowState.Normal;
                                //////    //Win_OPTLightTool.Instance.btn_runShapeMatchTool.Focus();
                                //////    Application.DoEvents();

                                //////    Win_OptLightControlTool.Instance.comboBox1.SelectedIndex = (optLightControlTool.controlMode ? 0 : 1);
                                //////    break;
                                //////#endregion

                                //////#region BatteryFirstAlign
                                //////case ToolType.batteryFirstAlign:
                                //////    Win_BatteryFirstAlignTool.Instance.pictureBox1.Image = Resources.OCRTool;
                                //////    Win_BatteryFirstAlignTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("电池初定位    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //Win_BatteryFirstAlignTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //Win_BatteryFirstAlignTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_BatteryFirstAlignTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_BatteryFirstAlignTool.Instance.TopMost = true;
                                //////    Win_BatteryFirstAlignTool.Instance.Activate();
                                //////    Win_BatteryFirstAlignTool.Instance.jobName = this.jobName;
                                //////    Win_BatteryFirstAlignTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_BatteryFirstAlignTool.Instance.Show();
                                //////    Win_BatteryFirstAlignTool.Instance.WindowState = FormWindowState.Normal;
                                //////    //////Win_BatteryFirstAlignTool.Instance.btn_runShapeMatchTool.Focus();
                                //////    BatteryFirstAlignTool batteryFirstAlignTool = (BatteryFirstAlignTool)(L_toolList[i].tool);
                                //////    Win_BatteryFirstAlignTool.batteryFirstAlignTool = batteryFirstAlignTool;
                                //////    Application.DoEvents();

                                //////    if (batteryFirstAlignTool.inputImage != null)
                                //////        batteryFirstAlignTool.ShowImage(batteryFirstAlignTool.inputImage);
                                //////    else
                                //////        //////batteryFirstAlignTool.ClearWindow(this.jobName);

                                //////        if (batteryFirstAlignTool.SearchRegion != null)
                                //////        {
                                //////            GetImageWindowControl().hwc_imageWindow.viewWindow.displayROI(batteryFirstAlignTool.regions);
                                //////            GetImageWindowControl().regions = batteryFirstAlignTool.regions;

                                //////        }

                                //////    if (batteryFirstAlignTool.regions.Count == 0)
                                //////    {
                                //////        GetImageWindowControl().hwc_imageWindow.viewWindow.genRect1(200.0, 200.0, 600.0, 800.0, ref batteryFirstAlignTool.regions);
                                //////        GetImageWindowControl().regions = batteryFirstAlignTool.regions;
                                //////    }
                                //////    else
                                //////    {
                                //////        GetImageWindowControl().hwc_imageWindow.viewWindow.displayROI(batteryFirstAlignTool.regions);
                                //////        GetImageWindowControl().regions = batteryFirstAlignTool.regions;
                                //////    }

                                //////    //将对象信息更新到界面
                                //////    Win_BatteryFirstAlignTool.Instance.ckb_findLineToolEnable.Checked = L_toolList[i].enable;
                                //////    Win_BatteryFirstAlignTool.Instance.nud_minThreshold.Value = batteryFirstAlignTool.minThreshold;
                                //////    Win_BatteryFirstAlignTool.Instance.nud_maxThreshold.Value = batteryFirstAlignTool.maxThreshold;
                                //////    Win_BatteryFirstAlignTool.Instance.numericUpDown3.Value = batteryFirstAlignTool.dilationAndErosionSize;
                                //////    Win_BatteryFirstAlignTool.Instance.numericUpDown1.Value = Convert.ToDecimal(batteryFirstAlignTool.minArea);
                                //////    Win_BatteryFirstAlignTool.Instance.numericUpDown2.Value = Convert.ToDecimal(batteryFirstAlignTool.maxArea);
                                //////    Win_BatteryFirstAlignTool.Instance.cbx_edgeSelect.Text = "点" + batteryFirstAlignTool.pointIndex;
                                //////    Win_BatteryFirstAlignTool.Instance.comboBox1.Text = "边" + batteryFirstAlignTool.lineIndex;

                                //////    //Win_ShapeMatchTool.Instance.nud_angleStart.Value = Convert.ToDecimal(shapeMatchTool.startAngle);
                                //////    //Win_ShapeMatchTool.Instance.nud_angleRange.Value = Convert.ToDecimal(shapeMatchTool.angleRange);
                                //////    //Win_ShapeMatchTool.Instance.nud_angleStep.Value = Convert.ToDecimal(shapeMatchTool.angleStep);
                                //////    //Win_ShapeMatchTool.Instance.tkb_contrast.Value = Convert.ToInt16(shapeMatchTool.contrast);
                                //////    //Win_ShapeMatchTool.Instance.cbx_polarity.Text = shapeMatchTool.polarity;
                                //////    //if (shapeMatchTool.angleStep == 0)
                                //////    //{
                                //////    //    Win_ShapeMatchTool.Instance.nud_angleStep.Enabled = false;
                                //////    //    Win_ShapeMatchTool.Instance.ckb_angleStep.Checked = true;
                                //////    //}
                                //////    //else
                                //////    //{
                                //////    //    Win_ShapeMatchTool.Instance.ckb_angleStep.Checked = false;
                                //////    //}
                                //////    break;
                                //////#endregion

                                //////#region BuChang
                                //////case ToolType.BuChang:
                                //////    Win_BuChangTool.Instance.pictureBox1.Image = Resources.UnknownTool;
                                //////    Win_BuChangTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("补偿    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //Win_BuChangTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //Win_BuChangTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_BuChangTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_BuChangTool.Instance.TopMost = true;
                                //////    Win_BuChangTool.Instance.Activate();
                                //////    Win_BuChangTool.Instance.jobName = this.jobName;
                                //////    Win_BuChangTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_BuChangTool.Instance.Show();
                                //////    Win_BuChangTool.Instance.WindowState = FormWindowState.Normal;
                                //////    ////Win_BuChangTool.Instance.btn_runDownCamAlignTool.Focus();
                                //////    BuChangTool buChangTool = (BuChangTool)(L_toolList[i].tool);
                                //////    Win_BuChangTool.buChangTool = buChangTool;
                                //////    Application.DoEvents();

                                //////    Win_BuChangTool.Instance.tbx_caputurePosX.Text = buChangTool.templatePos.Point.X.ToString();
                                //////    Win_BuChangTool.Instance.tbx_caputurePosY.Text = buChangTool.templatePos.Point.Y.ToString();
                                //////    Win_BuChangTool.Instance.tbx_caputurePosU.Text = buChangTool.templatePos.U.ToString();

                                //////    Win_BuChangTool.Instance.tbx_pickPosX.Text = buChangTool.workPos.Point.X.ToString();
                                //////    Win_BuChangTool.Instance.tbx_pickPosY.Text = buChangTool.workPos.Point.Y.ToString();
                                //////    Win_BuChangTool.Instance.tbx_pickPosU.Text = buChangTool.workPos.U.ToString();


                                //////    Win_BuChangTool.Instance.tbx_pickPosOffsetX.Text = buChangTool.buchang.Point.X.ToString();
                                //////    Win_BuChangTool.Instance.tbx_pickPosOffsetY.Text = buChangTool.buchang.Point.Y.ToString();
                                //////    Win_BuChangTool.Instance.tbx_pickPosOffsetU.Text = buChangTool.buchang.U.ToString();

                                //////    Win_BuChangTool.Instance.ckb_distancePLToolEnable.Checked = L_toolList[i].enable;
                                //////    break;
                                //////#endregion

                                //////#region PointOffset
                                //////case ToolType.PointOffset:
                                //////    Win_PointOffsetTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("补偿    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //Win_PointOffsetTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //Win_PointOffsetTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_PointOffsetTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_PointOffsetTool.Instance.TopMost = true;
                                //////    Win_PointOffsetTool.Instance.Activate();
                                //////    Win_PointOffsetTool.Instance.jobName = this.jobName;
                                //////    Win_PointOffsetTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_PointOffsetTool.Instance.Show();
                                //////    Win_PointOffsetTool.Instance.WindowState = FormWindowState.Normal;
                                //////    ////Win_BuChangTool.Instance.btn_runDownCamAlignTool.Focus();
                                //////    PointOffsetTool pointOffsetTool = (PointOffsetTool)(L_toolList[i].tool);
                                //////    Win_PointOffsetTool.pointOffsetTool = pointOffsetTool;
                                //////    Application.DoEvents();

                                //////    Win_PointOffsetTool.Instance.comboBox1.Clear();

                                //////    if (pointOffsetTool.toolPar.InputPar.点.ToString() == "System.Collections.Generic.List`1[VisionAndMotionPro.Point]")
                                //////    {
                                //////        for (int j = 0; j < ((List<Point>)pointOffsetTool.toolPar.InputPar.点).Count; j++)
                                //////        {
                                //////            Win_PointOffsetTool.Instance.comboBox1.Add((j + 1).ToString());
                                //////        }
                                //////    }
                                //////    else
                                //////    {

                                //////    }


                                //////    Win_PointOffsetTool.Instance.comboBox1.Text = pointOffsetTool.pointIdx.ToString();

                                //////    Win_PointOffsetTool.Instance.tbx_caputurePosX.Value = pointOffsetTool.templatePos.X.ToString();
                                //////    Win_PointOffsetTool.Instance.tbx_caputurePosY.Value = pointOffsetTool.templatePos.Y.ToString();

                                //////    Win_PointOffsetTool.Instance.tbx_pickPosX.Value = pointOffsetTool.workPos.X.ToString();
                                //////    Win_PointOffsetTool.Instance.tbx_pickPosY.Value = pointOffsetTool.workPos.Y.ToString();


                                //////    Win_PointOffsetTool.Instance.tbx_pickPosOffsetX.Value = pointOffsetTool.buchang.X;
                                //////    Win_PointOffsetTool.Instance.tbx_pickPosOffsetY.Value = pointOffsetTool.buchang.Y;

                                //////    if (pointOffsetTool.toolPar.InputPar.点.ToString() == "")
                                //////    {
                                //////        Win_PointOffsetTool.Instance.tbx_inputPosX.Text = ((List<Point>)pointOffsetTool.toolPar.InputPar.点)[pointOffsetTool.pointIdx - 1].X.ToString();
                                //////        Win_PointOffsetTool.Instance.tbx_inputPosY.Text = ((List<Point>)pointOffsetTool.toolPar.InputPar.点)[pointOffsetTool.pointIdx - 1].Y.ToString();
                                //////    }
                                //////    else
                                //////    {

                                //////    }

                                //////    Win_PointOffsetTool.Instance.tbx_resultPosX.Text = pointOffsetTool.toolPar.ResultPar.点.X.ToString();
                                //////    Win_PointOffsetTool.Instance.tbx_resultPosY.Text = pointOffsetTool.toolPar.ResultPar.点.Y.ToString();

                                //////    Win_PointOffsetTool.Instance.pictureBox8.Image = L_toolList[i].enable ? Resources.Enable : Resources.Disable;

                                //////    if (pointOffsetTool.toolPar.InputPar.点.ToString() == "System.Collections.Generic.List`1[VisionAndMotionPro.Point]")
                                //////        Win_PointOffsetTool.Instance.comboBox1.Visible = true;
                                //////    else
                                //////        Win_PointOffsetTool.Instance.comboBox1.Visible = false;
                                //////    break;
                                //////#endregion

                                //////#region DisplayEdit
                                //////case ToolType.DisplayEdit:
                                //////    Win_DisplayEditTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("显示编辑    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //////    //////Win_ShapeMatchTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    //////Win_ShapeMatchTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_ShapeMatchTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    //Win_ShapeMatchTool.Instance.TopMost = true;
                                //////    Win_DisplayEditTool.Instance.Activate();
                                //////    Win_DisplayEditTool.Instance.jobName = this.jobName;
                                //////    Win_DisplayEditTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_DisplayEditTool.Instance.Show();
                                //////    Win_DisplayEditTool.Instance.WindowState = FormWindowState.Normal;
                                //////    Win_DisplayEditTool.Instance.btn_runTool.Focus();
                                //////    DisplayEditTool displayEditTool = (DisplayEditTool)(L_toolList[i].tool);
                                //////    Win_DisplayEditTool.displayEditTool = displayEditTool;
                                //////    Application.DoEvents();





                                //////    //将对象信息更新到界面

                                //////    break;
                                //////#endregion

                                //////#region EthernetReceive
                                //////case ToolType.EthernetReceive:
                                //////    Win_EthernetReceiveTool.Instance.XXXXXXXXXXXXXlbl_title.Text = ( "SDK_PointGray" : string.Format("采集图像    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName));
                                //////    ////// Win_AcqImageTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    ////// Win_AcqImageTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width -  Win_AcqImageTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    // Win_AcqImageTool.Instance.TopMost = true;
                                //////    Win_EthernetReceiveTool.Instance.Activate();
                                //////    Win_EthernetReceiveTool.Instance.jobName = this.jobName;
                                //////    Win_EthernetReceiveTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_EthernetReceiveTool.ethernetReceiveTool = (EthernetReceiveTool)FindToolByName(L_toolList[i].toolName);
                                //////    Win_EthernetReceiveTool.Instance.Show();
                                //////    Win_EthernetReceiveTool.Instance.WindowState = FormWindowState.Normal;
                                //////    EthernetReceiveTool ethernetReceiveTool = (EthernetReceiveTool)(L_toolList[i].tool);
                                //////    Application.DoEvents();





                                //////    //将对象信息更新到界面
                                //////    Win_EthernetReceiveTool.Instance.pic_onOff.Image = L_toolList[i].enable ? Resources.开 : Resources.关;
                                //////    Win_EthernetReceiveTool.Instance.comboBox1222.Clear();
                                //////    for (int j = 0; j < Project.Instance.L_TCPClient.Count; j++)
                                //////    {
                                //////        Win_EthernetReceiveTool.Instance.comboBox1222.Add(Project.Instance.L_TCPClient[j].Name);
                                //////    }
                                //////    for (int j = 0; j < Project.Instance.L_TCPSever.Count; j++)
                                //////    {
                                //////        Win_EthernetReceiveTool.Instance.comboBox1222.Add(Project.Instance.L_TCPSever[j].Name);
                                //////    }
                                //////    Win_EthernetReceiveTool.Instance.comboBox1222.Text = ethernetReceiveTool.EthernetName;
                                //////    Win_EthernetReceiveTool.Instance.tbx_imageSavePath.Text = ethernetReceiveTool.trigCMD;
                                //////    switch (ethernetReceiveTool.endChar)
                                //////    {
                                //////        case "":
                                //////            Win_EthernetReceiveTool.Instance.btn_endCharNone.BackColor = Color.Gray;
                                //////            Win_EthernetReceiveTool.Instance.btn_endCharEnter.BackColor = Color.Gainsboro;
                                //////            break;
                                //////        case "\r\n":
                                //////            Win_EthernetReceiveTool.Instance.btn_endCharNone.BackColor = Color.Gainsboro;
                                //////            Win_EthernetReceiveTool.Instance.btn_endCharEnter.BackColor = Color.Gray;
                                //////            break;
                                //////    }
                                //////    break;
                                //////#endregion

                                //////#region EthernetSend
                                //////case ToolType.EthernetSend:
                                //////    Win_EthernetSendTool.Instance.XXXXXXXXXXXXXlbl_title.Text = ( "SDK_PointGray" : string.Format("采集图像    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName));
                                //////    ////// Win_AcqImageTool.Instance.StartPosition = FormStartPosition.Manual;
                                //////    ////// Win_AcqImageTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width -  Win_AcqImageTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //////    // Win_AcqImageTool.Instance.TopMost = true;
                                //////    Win_EthernetSendTool.Instance.Activate();
                                //////    Win_EthernetSendTool.Instance.jobName = this.jobName;
                                //////    Win_EthernetSendTool.Instance.toolName = L_toolList[i].toolName;
                                //////    Win_EthernetSendTool.ethernetReceiveTool = (EthernetSendTool)FindToolByName(L_toolList[i].toolName);
                                //////    Win_EthernetSendTool.Instance.Show();
                                //////    Win_EthernetSendTool.Instance.WindowState = FormWindowState.Normal;
                                //////    EthernetSendTool ethernetSendTool = (EthernetSendTool)(L_toolList[i].tool);
                                //////    Application.DoEvents();





                                //////    //将对象信息更新到界面
                                //////    Win_EthernetSendTool.Instance.pic_onOff.Image = L_toolList[i].enable ? Resources.开 : Resources.关;
                                //////    Win_EthernetSendTool.Instance.comboBox1222.Clear();
                                //////    for (int j = 0; j < Project.Instance.L_TCPClient.Count; j++)
                                //////    {
                                //////        Win_EthernetSendTool.Instance.comboBox1222.Add(Project.Instance.L_TCPClient[j].Name);
                                //////    }
                                //////    for (int j = 0; j < Project.Instance.L_TCPSever.Count; j++)
                                //////    {
                                //////        Win_EthernetSendTool.Instance.comboBox1222.Add(Project.Instance.L_TCPSever[j].Name);
                                //////    }
                                //////    Win_EthernetSendTool.Instance.comboBox1222.Text = ethernetSendTool.EthernetName;
                                //////    Win_EthernetSendTool.Instance.tbx_imageSavePath.Text = ethernetSendTool.toolPar.InputPar.消息;
                                //////    switch (ethernetSendTool.endChar)
                                //////    {
                                //////        case "":
                                //////            Win_EthernetSendTool.Instance.btn_endCharNone.BackColor = Color.Gray;
                                //////            Win_EthernetSendTool.Instance.btn_endCharEnter.BackColor = Color.Gainsboro;
                                //////            break;
                                //////        case "\r\n":
                                //////            Win_EthernetSendTool.Instance.btn_endCharNone.BackColor = Color.Gainsboro;
                                //////            Win_EthernetSendTool.Instance.btn_endCharEnter.BackColor = Color.Gray;
                                //////            break;
                                //////    }
                                //////    break;
                                //////#endregion

                                //#region Label
                                //case ToolType.Label:
                                //    LabelTool labelTool = (LabelTool)L_toolList[i].tool;



                                //    labelTool.Run(true, false, L_toolList[i].toolName);
                                //    break;
                                //#endregion

                                //////#region Output
                                //////case ToolType.Output:
                                //////    //Win_LogBoxTool.Instance.Text = "输出 - " + this.jobName + "." + L_toolList[i].toolName;
                                //////    //Win_LogBoxTool.Instance.TopMost = true;
                                //////    //Win_LogBoxTool.Instance.jobName = this.jobName;
                                //////    //Win_LogBoxTool.Instance.toolName = L_toolList[i].toolName; ;
                                //////    //Win_LogBoxTool.Instance.Show();
                                //////    //Win_LogBoxTool.Instance.WindowState = FormWindowState.Normal;
                                //////    //Win_LogBoxTool.Instance.b.Focus();
                                //////    //OutputTool outputTool = (OutputTool)(L_toolList[i].tool);
                                //////    //Application.DoEvents();

                                //////    //将对象信息更新到界面
                                //////    //Win_LogBoxTool.Instance.ckb_outputBoxToolNotRun.Checked = L_toolList[i].enable;
                                //////    Win_MessageBox.Instance.MessageBoxShow( "\r\nThis tool is a form-free tool" : "\r\n本工具为无窗体工具！");
                                //////    break;
                                //////#endregion

                                #region Default
                                default:

                                    break;
                                    #endregion
                            }







                        }
                    }
                })
                {
                    IsBackground = true
                };
                th.Start();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        /// <summary>
        /// 流程树右击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void TVW_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Developer))
                    return;

                if (isRunLoop)
                {
                    GetJobTree().ContextMenuStrip = null;
                    return;
                }


                if (GetJobTree().SelectedNode == null)
                    return;

               

                    if (e.Button == MouseButtons.Left)      //如果是鼠标左击，就改工具名
                {
                    if ( GetJobTree().SelectedNode != null)
                    {
                        TaskEdit treeView = (TaskEdit)sender;
                        if (e.Button == MouseButtons.Right)
                        {
                            if (treeView.SelectedNode != null)
                                treeView.SelectedNode.BeginEdit();
                        }
                    }
                   
                    return;
                }
                if (e.Button != MouseButtons.Right)
                    return;
                //判断是否在节点单击
                TreeViewHitTestInfo test = GetJobTree().HitTest(e.X, e.Y);
                if (test.Node.ImageIndex == 3)
                {
                    GetJobTree().ContextMenuStrip = null;
                    return;//输出节点返回
                }
                if (test.Node == null || test.Location == TreeViewHitTestLocations.None)       //单击空白
                {
                    GetJobTree().ContextMenuStrip = rightClickMenuAtBlank;
                    rightClickMenuAtBlank.Show(e.X, e.Y);
                    return;
                }
                else
                {
                    GetJobTree().ContextMenuStrip = rightClickMenu;
                }

                rightClickMenu.Items.Clear();

                rightClickMenu.Items.Add("运行");
                rightClickMenu.Items[0].Click += new EventHandler(RunTool);

                rightClickMenu.Items.Add(FindToolInfoByName(GetJobTree().SelectedNode.Text).enable ? "禁用" : "启用");
                rightClickMenu.Items[1].Click += new EventHandler(EnableOrDisenableTool);
                rightClickMenu.Items.Add("复制");
                rightClickMenu.Items[2].Click += new EventHandler(CopyTool);
                rightClickMenu.Items.Add("粘贴");
                rightClickMenu.Items[3].Click += new EventHandler(PasteTool);
                rightClickMenu.Items.Add("删除");
                rightClickMenu.Items[4].Image = Resources.删_除4;
                rightClickMenu.Items[4].Click += new EventHandler(DeleteItem);
                rightClickMenu.Items.Add("重命名");
                rightClickMenu.Items[5].Click += new EventHandler(RenameTool);
                rightClickMenu.Items.Add("编辑说明");
                rightClickMenu.Items[6].Click += new EventHandler(ModifyTipInfo);

                //如果不是第一个则添加上移选项
                if (GetJobTree().SelectedNode == null)
                    return;
                if (GetJobTree().SelectedNode.Index != 0)
                {
                    rightClickMenu.Items.Add("上移");
                    rightClickMenu.Items[7].Click += new EventHandler(MoveUp);
                    rightClickMenu.Items[7].Image = Resources.MoveUp;
                    if (GetJobTree().SelectedNode.Index != GetJobTree().Nodes.Count - 1)
                    {
                        rightClickMenu.Items.Add("下移");
                        rightClickMenu.Items[8].Click += new EventHandler(MoveDown);
                        rightClickMenu.Items[8].Image = Resources.MoveDown;
                    }
                }
                else
                {
                    rightClickMenu.Items.Add("下移");
                    rightClickMenu.Items[7].Click += new EventHandler(MoveDown);
                    rightClickMenu.Items[7].Image = Resources.MoveDown;
                }
                //白色背景好看
                for (int i = 0; i < rightClickMenu.Items.Count; i++)
                {
                    ((ToolStripItem)rightClickMenu.Items[i]).BackColor = Color.White;
                }

                if (e.Button == MouseButtons.Right&&  e.Clicks == 1)        //如果右击
                {
                    
                    ToolInfo toolInfo = FindToolInfoByName(GetJobTree().SelectedNode.Text);

                    //清空输入，输出下拉选项
                    Application.DoEvents();

                    #region 显示源
                    if (GetJobTree().SelectedNode.Level == 1)
                    {

                        //指定源
                        string nodeText = GetJobTree().SelectedNode.Text;
                        string fatherNodeText = GetJobTree().SelectedNode.Parent.Text;
                        string curNodeType = GetJobTree().SelectedNode.Tag.ToString();
                        ToolStripMenuItem item111 = new ToolStripMenuItem();
                        //当前流程可源项
                        foreach (TreeNode toolNode in GetJobTree().Nodes)
                        {
                            foreach (TreeNode itemNode in ((TreeNode)toolNode).Nodes)
                            {
                                if (((TreeNode)itemNode).Text == nodeText)
                                {
                                    rightClickMenu.Items.Clear();
                                    ToolStripItem sourceFrom = rightClickMenu.Items.Add("源于");
                                    sourceFrom.BackColor = Color.White;
                                    item111 = rightClickMenu.Items[0] as ToolStripMenuItem;
                                    ((ToolStripMenuItem)rightClickMenu.Items[0]).DropDownItems.Clear();
                                    foreach (TreeNode toolNode1 in GetJobTree().Nodes)
                                    {
                                        if (toolNode1.Text == fatherNodeText)        //不能指定自己的输出项为源
                                            continue;
                                        if (((TreeNode)toolNode1).Text != ("输出项"))
                                        {
                                            foreach (TreeNode itemNode1 in ((TreeNode)toolNode1).Nodes)
                                            {
                                                string sourceType = itemNode1.Tag.ToString();
                                                if (sourceType == curNodeType)
                                                {
                                                    if (((TreeNode)itemNode1).SelectedImageIndex == 3)
                                                    {
                                                        string resultStr = toolNode1.Text + "." + itemNode1.Text;
                                                        ToolStripItem item1 = item111.DropDownItems.Add(resultStr);
                                                        item1.Name = resultStr;
                                                        item1.Click += new EventHandler(ConnectSource);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }


                        foreach (Job job in Project.Instance.curEngine.L_jobList)
                        {
                            //job.L_toolList[i].input
                            if (job.jobName == jobName)
                                continue;
                            foreach (ToolInfo toolNode1 in job.L_toolList)
                            {

                                    foreach (ToolIO itemNode1 in toolNode1.output)
                                    {
                                        string sourceType = itemNode1.ioType.ToString();
                                        if (sourceType == curNodeType)
                                        {
                                       
                                                string resultStr = "[" + job.jobName + "]" + toolNode1.toolName + "." + itemNode1.IOName;
                                                ToolStripItem item1 = item111.DropDownItems.Add(resultStr);
                                                item1.Name = resultStr;
                                                item1.Click += new EventHandler(ConnectSource);
                                            
                                        }
                                    }
                                
                            }

                        }
                        //其它流程可源项
                        //foreach (Job job in Project.Instance.curEngine.L_jobList)
                        //{
                        //    //job.L_toolList[i].input
                        //    if (job.jobName == jobName)
                        //        continue;
                        //    foreach (TreeNode toolNode1 in job.GetJobTree().Nodes)
                        //    {
                        //        if (toolNode1.Text == fatherNodeText)        //不能指定自己的输出项为源
                        //            continue;
                        //        if (((TreeNode)toolNode1).Text != ("输出项"))
                        //        {
                        //            foreach (TreeNode itemNode1 in ((TreeNode)toolNode1).Nodes)
                        //            {
                        //                string sourceType = itemNode1.Tag.ToString();
                        //                if (sourceType == curNodeType)
                        //                {
                        //                    if (((TreeNode)itemNode1).SelectedImageIndex == 3)
                        //                    {
                        //                        string resultStr = "[" + job.jobName + "]" + toolNode1.Text + "." + itemNode1.Text;
                        //                        ToolStripItem item1 = item111.DropDownItems.Add(resultStr);
                        //                        item1.Name = resultStr;
                        //                        item1.Click += new EventHandler(ConnectSource);
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }

                        //}


                        //全局变量可源项
                        for (int i = 0; i < Project.Instance.curEngine.globelVariable.GetGlobalVariableCount(); i++)
                        {
                            Variable v = Project.Instance.curEngine.globelVariable.GetGlobalVariable(i);
                            if (v.type== curNodeType && v.variableType==1)
                            {
                                string resultStr = "全局变量." + Project.Instance.curEngine.globelVariable.GetGlobalVariable(i).name;
                                ToolStripItem item1 = item111.DropDownItems.Add(resultStr);
                                item1.Name = resultStr;
                                item1.Click += new EventHandler(ConnectSource);
                            }
                        }
                    }
                    #endregion



                    GetJobTree().ContextMenuStrip = rightClickMenu;


                    rightClickMenu.Show();
                    Application.DoEvents();



                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        /// <summary>
        /// 流程树的双击事件
        /// </summary>
        internal void TVW_DoubleClick(object sender, MouseEventArgs e)
        {
            try
            { 

                doubleClick = true;
                if (!Permission.CheckPermission(PermissionLevel.Admin))
                    return;

                loadForm = true;
                TreeNode treeNode = GetJobTree().SelectedNode;
                if (treeNode == null)           //如果流程正在运行，可能会没有选中节点
                {
                    Win_Main.Instance.OutputMsg("流程正在运行，不可编辑,请先停止运行", Win_Log.InfoType.tip);
                    return;
                }
                string toolName = treeNode.Text;

                for (int i = 0; i < L_toolList.Count; i++)
                {
                    if (L_toolList[i].toolName == toolName)
                    {
                        switch (L_toolList[i].toolType)
                        {
                            #region ImageAcq
                            case ToolType.ImageAcq:
                                Win_AcqImageTool.Instance.XXXXXXXXXXXXXlbl_title.Text = (string.Format("图像采集[{0}.{1}]", this.jobName, L_toolList[i].toolName));
                                Win_AcqImageTool.Instance.Activate();
                                Win_AcqImageTool.Instance.jobName = this.jobName;
                                Win_AcqImageTool.Instance.toolName = L_toolList[i].toolName;
                                Win_AcqImageTool.Instance.acqImageTool = (AcqImageTool)FindToolByName(L_toolList[i].toolName);
                                Win_FromLocal.Instance.jobName = this.jobName;
                                Win_FromLocal.Instance.toolName = L_toolList[i].toolName;
                                Win_FromDevice.Instance.jobName = this.jobName;
                                Win_FromDevice.Instance.toolName = L_toolList[i].toolName;
                                Win_FromDevice.imageAcqTool = ((AcqImageTool)Job.FindToolByName(this.jobName, L_toolList[i].toolName));
                                Win_AcqImageTool.Instance.Show();
                                Win_AcqImageTool.Instance.WindowState = FormWindowState.Normal;
                                AcqImageTool SDK_hikVisionTool = (AcqImageTool)(L_toolList[i].tool);
                                Win_FromLocal.imageAcqTool = SDK_hikVisionTool;
                                Application.DoEvents();

                                //当前图像显示
                                if (SDK_hikVisionTool.toolPar.ResultPar.图像 != null)
                                    Win_AcqImageTool.Instance.PImageWin.HobjectToHimage(SDK_hikVisionTool.toolPar.ResultPar.图像);
                                else
                                // Win_AcqImageTool.Instance.hWindow_Final1.ClearWindow();


                                if (!SDK_hikVisionTool.displayAllImageRegion)
                                {
                                    Win_AcqImageTool.Instance.PImageWin.viewWindow.displayROI(SDK_hikVisionTool.L_regions);
                                    Win_AcqImageTool.Instance.L_regions = SDK_hikVisionTool.L_regions;
                                }

                                if (((AcqImageTool)(L_toolList[i].tool)).imageSourceMode == ImageSourceMode.FromDevice)
                                {
                                    Win_AcqImageTool.Instance.rdo_fromDevice.Checked = true;
                                    Win_AcqImageTool.Instance.rdo_fromDevice.ForeColor = Color.White;
                                    Win_AcqImageTool.Instance.radio_FromLocalFile.ForeColor = Color.White;
                                    Win_AcqImageTool.Instance.rdo_fromLocalDirectory.ForeColor = Color.White;
                                    Win_AcqImageTool.Instance.rdo_fromDevice.Font = new Font(Win_AcqImageTool.Instance.rdo_fromDevice.Font.Name, Win_AcqImageTool.Instance.rdo_fromDevice.Font.Size, FontStyle.Bold);
                                    Win_AcqImageTool.Instance.radio_FromLocalFile.Font = new Font(Win_AcqImageTool.Instance.rdo_fromDevice.Font.Name, Win_AcqImageTool.Instance.rdo_fromDevice.Font.Size, FontStyle.Regular);
                                    Win_AcqImageTool.Instance.rdo_fromLocalDirectory.Font = new Font(Win_AcqImageTool.Instance.rdo_fromDevice.Font.Name, Win_AcqImageTool.Instance.rdo_fromDevice.Font.Size, FontStyle.Regular);
                                    Win_AcqImageTool.Instance.pic_fromDevice.Image = Resources.勾选;
                                    Win_AcqImageTool.Instance.pic_fromLocalFile.Image = Resources.去勾选;
                                    Win_AcqImageTool.Instance.pic_fromLocalDirectory.Image = Resources.去勾选;
                                    Win_AcqImageTool.Instance.pnl_formPanel.Controls.Clear();
                                    Win_FromDevice.Instance.TopLevel = false;
                                    Win_FromDevice.Instance.Parent = Win_AcqImageTool.Instance.pnl_formPanel;
                                    Win_FromDevice.Instance.Dock = DockStyle.Top;
                                    Win_FromDevice.Instance.Show();

                                    Win_AcqImageTool.Instance.ckb_hardware.Visible = true;
                                    Win_AcqImageTool.Instance.ckb_hardware.Location = new System.Drawing.Point(Win_AcqImageTool.Instance.ckb_hardware.Location.X, 366);
                                    Win_AcqImageTool.Instance.ckb_autoSwitch.Visible = false;
                                }
                                else
                                {
                                    if (SDK_hikVisionTool.imageSourceMode == ImageSourceMode.FromDirectory)
                                    {
                                        Win_AcqImageTool.Instance.rdo_fromLocalDirectory.Checked = true;
                                        Win_FromLocal.Instance.pnl_multImage.Visible = true;
                                        Win_AcqImageTool.Instance.pic_fromLocalFile.Image = Resources.去勾选;
                                        Win_AcqImageTool.Instance.pic_fromLocalDirectory.Image = Resources.勾选;
                                        Win_AcqImageTool.Instance.pic_fromDevice.Image = Resources.去勾选;
                                        Win_AcqImageTool.Instance.rdo_fromDevice.ForeColor = Color.White;
                                        Win_AcqImageTool.Instance.radio_FromLocalFile.ForeColor = Color.White;
                                        Win_AcqImageTool.Instance.rdo_fromLocalDirectory.ForeColor = Color.White;
                                        Win_AcqImageTool.Instance.rdo_fromDevice.Font = new Font(Win_AcqImageTool.Instance.rdo_fromDevice.Font.Name, Win_AcqImageTool.Instance.rdo_fromDevice.Font.Size, FontStyle.Regular);
                                        Win_AcqImageTool.Instance.radio_FromLocalFile.Font = new Font(Win_AcqImageTool.Instance.rdo_fromDevice.Font.Name, Win_AcqImageTool.Instance.rdo_fromDevice.Font.Size, FontStyle.Regular);
                                        Win_AcqImageTool.Instance.rdo_fromLocalDirectory.Font = new Font(Win_AcqImageTool.Instance.rdo_fromDevice.Font.Name, Win_AcqImageTool.Instance.rdo_fromDevice.Font.Size, FontStyle.Bold);

                                        Win_AcqImageTool.Instance.ckb_autoSwitch.Visible = true;
                                    }
                                    else
                                    {
                                        Win_AcqImageTool.Instance.radio_FromLocalFile.Checked = true;
                                        Win_FromLocal.Instance.pnl_multImage.Visible = false;
                                        Win_AcqImageTool.Instance.pic_fromLocalDirectory.Image = Resources.去勾选;
                                        Win_AcqImageTool.Instance.pic_fromLocalFile.Image = Resources.勾选;
                                        Win_AcqImageTool.Instance.pic_fromDevice.Image = Resources.去勾选;

                                        Win_AcqImageTool.Instance.rdo_fromDevice.ForeColor = Color.White;
                                        Win_AcqImageTool.Instance.radio_FromLocalFile.ForeColor = Color.White;
                                        Win_AcqImageTool.Instance.rdo_fromLocalDirectory.ForeColor = Color.White;
                                        Win_AcqImageTool.Instance.rdo_fromDevice.Font = new Font(Win_AcqImageTool.Instance.rdo_fromDevice.Font.Name, Win_AcqImageTool.Instance.rdo_fromDevice.Font.Size, FontStyle.Regular);
                                        Win_AcqImageTool.Instance.radio_FromLocalFile.Font = new Font(Win_AcqImageTool.Instance.rdo_fromDevice.Font.Name, Win_AcqImageTool.Instance.rdo_fromDevice.Font.Size, FontStyle.Bold);
                                        Win_AcqImageTool.Instance.rdo_fromLocalDirectory.Font = new Font(Win_AcqImageTool.Instance.rdo_fromDevice.Font.Name, Win_AcqImageTool.Instance.rdo_fromDevice.Font.Size, FontStyle.Regular);

                                        Win_AcqImageTool.Instance.ckb_autoSwitch.Visible = false;
                                    }

                                    Win_AcqImageTool.Instance.pnl_formPanel.Controls.Clear();
                                    Win_FromLocal.Instance.TopLevel = false;
                                    Win_FromLocal.Instance.Parent = Win_AcqImageTool.Instance.pnl_formPanel;
                                    Win_FromLocal.Instance.Dock = DockStyle.Top;
                                    Win_FromLocal.Instance.Show();

                                    Win_AcqImageTool.Instance.ckb_hardware.Visible = false;
                                    Win_AcqImageTool.Instance.ckb_hardware.Location = new System.Drawing.Point(Win_AcqImageTool.Instance.ckb_hardware.Location.X, 316);
                                }

                                //if (SDK_hikVisionTool.deviceInfoStr != string.Empty)
                                //{
                                Win_FromDevice.Instance.tbx_exposure.Value = SDK_hikVisionTool.exposure;
                               // Win_FromDevice.Instance.tkb_exposure.Value = (int)SDK_hikVisionTool.exposure;
                                //}

                                Application.DoEvents();
                                Win_AcqImageTool.Instance.ckb_displayAllImageRegion.Checked = SDK_hikVisionTool.displayAllImageRegion;

                                if (SDK_hikVisionTool.triggerModel == DeviceTriggerModel.hardware)
                                {
                                    Win_AcqImageTool.Instance.ckb_hardware.Checked = true;
                                }
                                else if (SDK_hikVisionTool.triggerModel == DeviceTriggerModel.software)
                                {
                                    Win_AcqImageTool.Instance.ckb_hardware.Checked = false;
                                }

                                Win_AcqImageTool.Instance.ckb_RGBToGray.Checked = SDK_hikVisionTool.RGBToGray;
                                Win_AcqImageTool.Instance.ckb_absPath.Checked = SDK_hikVisionTool.absPath;
                                Win_AcqImageTool.Instance.ckb_autoSwitch.Checked = SDK_hikVisionTool.autoSwitch;

                                Win_AcqImageTool.Instance.Plbl_toolTip.Text = string.Format("状态：成功，当前图像：{0} ({1})", SDK_hikVisionTool.currentImageName, SDK_hikVisionTool.currentImageIndex + 1 + "/" + SDK_hikVisionTool.L_images.Count);
                                Win_FromLocal.Instance.tbx_imagePath.Text = SDK_hikVisionTool.imagePath;
                                Win_FromLocal.Instance.tbx_imageDirectoryPath.Text = SDK_hikVisionTool.imageDirectoryPath;
                                Win_AcqImageTool.Instance.Plbl_toolTip.ForeColor = Color.White;
                                if (SDK_hikVisionTool.L_images.Count != 0)
                                    Win_AcqImageTool.Instance.Plbl_toolTip.Text = string.Format("状态：成功，当前图像：{0} ({1})", SDK_hikVisionTool.currentImageName, SDK_hikVisionTool.currentImageIndex + 1 + "/" + SDK_hikVisionTool.L_images.Count);
                                else
                                    Win_AcqImageTool.Instance.Plbl_toolTip.Text = "状态：成功";
                                Win_AcqImageTool.Instance.ckb_rotateImage.Checked = SDK_hikVisionTool.rotateImage;
                                Win_AcqImageTool.Instance.nud_rotateAngle.Value = SDK_hikVisionTool.rotateAngle;
                                Win_AcqImageTool.Instance.Pbtn_runTool.Focus();
                                break;
                            #endregion
                            #region SaveImage
                            case ToolType.SaveImage:
                                Win_SaveImageTool.Instance.XXXXXXXXXXXXXlbl_title.Text = (string.Format("存储图像[{0}{1}]", this.jobName, L_toolList[i].toolName));
                                Win_SaveImageTool.Instance.Activate();
                                Win_SaveImageTool.Instance.jobName = this.jobName;
                                Win_SaveImageTool.Instance.toolName = L_toolList[i].toolName;
                                Win_SaveImageTool.saveImageTool = (SaveImageTool)FindToolByName(L_toolList[i].toolName);
                                Win_SaveImageTool.Instance.jobName = this.jobName;
                                Win_SaveImageTool.Instance.toolName = L_toolList[i].toolName;
                                Win_SaveImageTool.saveImageTool = ((SaveImageTool)Job.FindToolByName(this.jobName, L_toolList[i].toolName));
                                Win_SaveImageTool.Instance.Show();
                                Win_SaveImageTool.Instance.WindowState = FormWindowState.Normal;
                                ////Win_SaveImageTool.Instance.btn_runSDKHIKVisionTool.Focus();
                                SaveImageTool saveImageTool = (SaveImageTool)(L_toolList[i].tool);
                                Application.DoEvents();
                                //将对象信息更新到界面

                                Win_SaveImageTool.Instance.tbx_imageSavePath.Text = saveImageTool.imageSavePath;
                                Win_SaveImageTool.Instance.comboBox1.Text = saveImageTool.imageFormat;
                                Win_SaveImageTool.Instance.textBox1.Value = saveImageTool.saveDays;
                                Win_SaveImageTool.Instance.checkBox1.Checked = saveImageTool.expandTime;
                                Win_SaveImageTool.Instance.checkBox2.Checked = saveImageTool.autoClear;
                                Win_SaveImageTool.Instance.textBox2.Text = saveImageTool.imageName;
                                Win_SaveImageTool.Instance.checkBox3.Checked = saveImageTool.autoCreateDirectory;
                                Win_SaveImageTool.Instance.radioButton1.Checked = (saveImageTool.imageSource == ImageSource.InputImage ? true : false);
                                Win_SaveImageTool.Instance.radioButton2.Checked = (saveImageTool.imageSource == ImageSource.InputImage ? false : true);
                                Win_SaveImageTool.Instance.pictureBox8.Image = (saveImageTool.imageSource == ImageSource.InputImage ? Resources.勾选 : Resources.去勾选);
                                Win_SaveImageTool.Instance.pictureBox7.Image = (saveImageTool.imageSource == ImageSource.WindowImage ? Resources.勾选 : Resources.去勾选);
                                break;
                            #endregion
                            #region Binarization
                            case ToolType.Binarization:
                                Win_BinarizationTool.Instance.XXXXXXXXXXXXXlbl_title.Text = (string.Format("二值化[{0}.{1}]", this.jobName, L_toolList[i].toolName));
                                Win_BinarizationTool.Instance.Activate();
                                Win_BinarizationTool.Instance.jobName = this.jobName;
                                Win_BinarizationTool.Instance.toolName = L_toolList[i].toolName;

                                Win_BinarizationTool.Instance.WindowState = FormWindowState.Normal;
                                BinarizationTool BinarizationTool = (BinarizationTool)(L_toolList[i].tool);
                                Win_BinarizationTool.BinarizationTool = BinarizationTool;
                                
                                Application.DoEvents();

                                inputItemNum = (L_toolList[i]).input.Count;

                                for (int j = 0; j < inputItemNum; j++)
                                {
                                    string inputItemName = L_toolList[i].input[j].IOName;
                                    string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                    if (sourceFrom == string.Empty)
                                    {
                                        continue;
                                    }
                                    if (inputItemName == "图像" || inputItemName == "InputImage")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];

                                        HImage hImage;
                                        BinarizationTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, false, i, this, out hImage);
                                        BinarizationTool.toolPar.InputPar.图像 = hImage;

                                        Win_BinarizationTool.Instance.vLinkImage1.ImageText = sourceFrom.Substring(2);
                                        //BinarizationTool.toolPar.InputPar.图像 = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as HImage;
                                        if (BinarizationTool.toolPar.InputPar.图像 == null)
                                        {
                                            continue;
                                        }
                                    }
                                }

                                if (BinarizationTool.toolPar.InputPar.图像 != null)
                                {
                                    Win_BinarizationTool.Instance.PImageWin.Image = (BinarizationTool.toolPar.InputPar.图像);

                                }
                                Win_BinarizationTool.Instance.Show();
                                if (BinarizationTool.L_regions != null)
                                {
                                    Win_BinarizationTool.Instance.PImageWin.viewWindow.displayROI(BinarizationTool.L_regions);
                                    Win_BinarizationTool.Instance.regions = BinarizationTool.L_regions;
                                }

                                break;
                            #endregion
                            #region Morphology
                            case ToolType.Morphology:
                                Win_MorphologyTool.Instance.XXXXXXXXXXXXXlbl_title.Text = (string.Format("开闭运算[{0}.{1}]", this.jobName, L_toolList[i].toolName));
                                Win_MorphologyTool.Instance.Activate();
                                Win_MorphologyTool.Instance.jobName = this.jobName;
                                Win_MorphologyTool.Instance.toolName = L_toolList[i].toolName;

                                Win_MorphologyTool.Instance.WindowState = FormWindowState.Normal;
                                MorphologyTool MorphologyTool = (MorphologyTool)(L_toolList[i].tool);
                                Win_MorphologyTool.MorphologyTool = MorphologyTool;
                                Win_MorphologyTool.Instance.Show();
                                Application.DoEvents();

                                inputItemNum = (L_toolList[i]).input.Count;

                                for (int j = 0; j < inputItemNum; j++)
                                {
                                    string inputItemName = L_toolList[i].input[j].IOName;
                                    string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                    if (sourceFrom == string.Empty)
                                    {
                                        continue;
                                    }
                                    if (inputItemName == "图像" || inputItemName == "InputImage")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];

                                        HImage hImage;
                                        MorphologyTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, false, i, this, out hImage);
                                        MorphologyTool.toolPar.InputPar.图像 = hImage;

                                        Win_MorphologyTool.Instance.vLinkImage1.ImageText = sourceFrom.Substring(2);
                                        //MorphologyTool.toolPar.InputPar.图像 = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as HImage;
                                        if (MorphologyTool.toolPar.InputPar.图像 == null)
                                        {
                                            continue;
                                        }
                                    }
                                    else if (inputItemName == "ROI" || inputItemName == "InputImage")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];
                                        MorphologyTool.toolPar.InputPar.搜索区域 = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as HRegion;
                                        Win_MorphologyTool.Instance.vLinkROI1.ROIText = sourceFrom.Substring(2);
                                    }

                                }

                                //

                                if (MorphologyTool.toolPar.InputPar.图像 != null)
                                {
                                    Win_MorphologyTool.Instance.PImageWin.Image = (MorphologyTool.toolPar.InputPar.图像);
                                    if (MorphologyTool.toolPar.InputPar.搜索区域 != null)
                                        Win_MorphologyTool.Instance.PImageWin.displayHRegion(MorphologyTool.toolPar.InputPar.搜索区域, "green", "margin");
                                }

                                if (MorphologyTool.L_regions != null)
                                {
                                    Win_MorphologyTool.Instance.PImageWin.viewWindow.displayROI(MorphologyTool.L_regions);
                                    Win_MorphologyTool.Instance.regions = MorphologyTool.L_regions;
                                }

                                break;
                            #endregion
                            #region FindROI
                            case ToolType.FindROI:
                                Win_FindROITool.Instance.XXXXXXXXXXXXXlbl_title.Text = (string.Format("查找区域1[{0}.{1}]", this.jobName, L_toolList[i].toolName));
                                Win_FindROITool.Instance.Activate();
                                Win_FindROITool.Instance.jobName = this.jobName;
                                Win_FindROITool.Instance.toolName = L_toolList[i].toolName;

                                Win_FindROITool.Instance.WindowState = FormWindowState.Normal;
                                FindROI FindROI = (FindROI)(L_toolList[i].tool);
                                Win_FindROITool.FindROI = FindROI;

                                Application.DoEvents();

                                inputItemNum = (L_toolList[i]).input.Count;

                                for (int j = 0; j < inputItemNum; j++)
                                {
                                    string inputItemName = L_toolList[i].input[j].IOName;
                                    string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                    if (sourceFrom == string.Empty)
                                    {
                                        continue;
                                    }
                                    if (inputItemName == "图像" || inputItemName == "InputImage")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];

                                        HImage hImage;
                                        FindROI.getOriginalJob(sourceFrom, sourceToolName, toolItem, false, i, this, out hImage);
                                        FindROI.toolPar.InputPar.图像 = hImage;

                                        Win_FindROITool.Instance.vLinkImage1.ImageText = sourceFrom.Substring(2);
                                        //FindROI.toolPar.InputPar.图像 = mjob.FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as HImage;
                                        if (FindROI.toolPar.InputPar.图像 == null)
                                        {
                                            continue;
                                        }
                                    }
                                }

                                if (FindROI.toolPar.InputPar.图像 != null)
                                {
                                    Win_FindROITool.Instance.PImageWin.Image = (FindROI.toolPar.InputPar.图像);
                                }

                                if (FindROI.L_regions != null)
                                {
                                    Win_FindROITool.Instance.PImageWin.viewWindow.displayROI(FindROI.L_regions);
                                    Win_FindROITool.Instance.regions = FindROI.L_regions;
                                }
                                Win_FindROITool.Instance.Show();
                                break;
                            #endregion
                            #region FindRegion
                            case ToolType.FindRegion:
                                Win_FindRegionTool.Instance.XXXXXXXXXXXXXlbl_title.Text = (string.Format("查找搜索区域[{0}.{1}]", this.jobName, L_toolList[i].toolName));
                                Win_FindRegionTool.Instance.Activate();
                                Win_FindRegionTool.Instance.jobName = this.jobName;
                                Win_FindRegionTool.Instance.toolName = L_toolList[i].toolName;

                                Win_FindRegionTool.Instance.WindowState = FormWindowState.Normal;
                                FindRegionTool FindRegionTool = (FindRegionTool)(L_toolList[i].tool);
                                Win_FindRegionTool.FindRegionTool = FindRegionTool;
                                Application.DoEvents();
                                inputItemNum = (L_toolList[i]).input.Count;
                                for (int j = 0; j < inputItemNum; j++)
                                {
                                    string inputItemName = L_toolList[i].input[j].IOName;
                                    string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                    if (sourceFrom == string.Empty)
                                    {
                                        continue;
                                    }
                                    if (inputItemName == "图像" || inputItemName == "InputImage")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];
                                        Win_FindRegionTool.Instance.vLinkImage1.ImageText = sourceFrom.Substring(2);
                                        HImage hImage;
                                        FindRegionTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, false, i, this, out hImage);
                                        FindRegionTool.toolPar.InputPar.图像 = hImage;
                                        FindRegionTool.toolPar.RunPar.图像 = FindRegionTool.toolPar.InputPar.图像;
                                        if (FindRegionTool.toolPar.InputPar.图像 == null)
                                        {
                                            continue;
                                        }
                                    }
                                    else if (inputItemName == "跟随位置" || inputItemName == "Pose")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];
                                        FindRegionTool.toolPar.InputPar.Pose = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as List<XYU>;
                                        Win_FindRegionTool.Instance.vLinkPose1.ROIText = sourceFrom.Substring(2);
                                        if (FindRegionTool.toolPar.InputPar.Pose == null)
                                        {
                                            continue;
                                        }
                                    }
                                }
                                if (FindRegionTool.toolPar.InputPar.图像 != null)
                                    Win_FindRegionTool.Instance.PImageWin.Image = (FindRegionTool.toolPar.InputPar.图像);
                                Win_FindRegionTool.Instance.Show();
                                Application.DoEvents();
                                //if (FindRegionTool.toolPar.ResultsPar.L_resultRegion.Count > 0)
                                //{
                                //    Win_FindRegionTool.Instance.PImageWin.displayHRegion(FindRegionTool.toolPar.ResultsPar.resultRegion);
                                //}
                                break;
                            #endregion
                            #region Match
                            case ToolType.Match:
                                Win_MatchTool.Instance.XXXXXXXXXXXXXlbl_title.Text = (string.Format("2D匹配[{0}.{1}]", this.jobName, L_toolList[i].toolName));
                                Win_MatchTool.Instance.Activate();
                                Win_MatchTool.Instance.jobName = this.jobName;
                                Win_MatchTool.Instance.toolName = L_toolList[i].toolName;

                                Win_MatchTool.Instance.WindowState = FormWindowState.Normal;
                                MatchTool MatchTool = (MatchTool)(L_toolList[i].tool);
                                Win_MatchTool.MatchTool = MatchTool;

                                Application.DoEvents();

                                inputItemNum = (L_toolList[i]).input.Count;

                                for (int j = 0; j < inputItemNum; j++)
                                {
                                    string inputItemName = L_toolList[i].input[j].IOName;
                                    string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                    if (sourceFrom == string.Empty)
                                    {
                                        continue;
                                    }
                                    if (inputItemName == "图像" || inputItemName == "InputImage")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];

                                        HImage hImage;
                                        MatchTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, false, i, this, out hImage);
                                        MatchTool.toolPar.InputPar.InPutImage = hImage;

                                        //MatchTool.toolPar.InputPar.InPutImage = mjob.FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as HImage;
                                        if (MatchTool.toolPar.InputPar.InPutImage == null)
                                        {
                                            continue;
                                        }
                                    }

                                }

                                if (MatchTool.toolPar.InputPar.InPutImage != null)
                                    Win_MatchTool.Instance.PImageWin.Image = (MatchTool.toolPar.InputPar.InPutImage);

                                Win_MatchTool.Instance.Show();
                                break;
                            #endregion
                            #region Blob
                            case ToolType.BlobAnalyse:

                                Win_BlobTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("斑点工具[{0}.{1}]", this.jobName, L_toolList[i].toolName);
                                Win_BlobTool.Instance.Activate();
                                Win_BlobTool.Instance.jobName = this.jobName;
                                Win_BlobTool.Instance.toolName = L_toolList[i].toolName;

                                Win_BlobTool.Instance.WindowState = FormWindowState.Normal;
                                //////Win_BlobAnalyseTool.Instance.btn_runTool.Focus();
                                BlobTool BlobTool = (BlobTool)(L_toolList[i].tool);
                                Win_BlobTool.BlobTool = BlobTool;

                                inputItemNum = (L_toolList[i]).input.Count;

                                for (int j = 0; j < inputItemNum; j++)
                                {
                                    string inputItemName = L_toolList[i].input[j].IOName;
                                    string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                    if (sourceFrom == string.Empty)
                                    {
                                        continue;
                                    }
                                    if (inputItemName == "图像" || inputItemName == "InputImage")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];
                                        Win_BlobTool.Instance.vLinkImage1.ImageText = sourceFrom.Substring(2);

                                        HImage hImage;
                                        BlobTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, false, i, this, out hImage);
                                        BlobTool.toolPar.InputPar.图像 = hImage;
                                        BlobTool.toolPar.RunPar.图像 = BlobTool.toolPar.InputPar.图像;
                                        //blobAnalyseTool.toolPar.InputPar.图像 = mjob.FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as HImage;
                                        if (BlobTool.toolPar.InputPar.图像 == null)
                                        {
                                            continue;
                                        }
                                    }
                                    else if (inputItemName == "跟随位置" || inputItemName == "Pose")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];
                                        BlobTool.toolPar.InputPar.Pose = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as List<XYU>;
                                        Win_BlobTool.Instance.vLinkPose1.ROIText = sourceFrom.Substring(2);
                                        if (BlobTool.toolPar.InputPar.Pose == null)
                                        {
                                            continue;
                                        }
                                    }

                                }
                                if (BlobTool.toolPar.InputPar.图像 != null)
                                    Win_BlobTool.Instance.PImageWin.Image = (BlobTool.toolPar.InputPar.图像);
                                Win_BlobTool.Instance.Show();
                                Application.DoEvents();
                                if (BlobTool.toolPar.ResultsPar.L_resultBlob.Count > 0)
                                {
                                    Win_BlobTool.Instance.PImageWin.displayHRegion(BlobTool.toolPar.ResultsPar.resultRegion);
                                }

                                //将预处理项更新到窗体
                                //  Win_BlobTool.Instance.dgv_processingItem.Rows.Clear();
                                //for (int j = 0; j < blobAnalyseTool.L_prePorcessing.Count; j++)
                                //{
                                //    int index = Win_BlobTool.Instance.dgv_processingItem.Rows.Add();
                                //    Win_BlobTool.Instance.dgv_processingItem.Rows[index].Cells[0].Value = blobAnalyseTool.L_prePorcessing[j].PreProcessingType;
                                //    ((DataGridViewCheckBoxCell)Win_BlobTool.Instance.dgv_processingItem.Rows[index].Cells[1]).Value = blobAnalyseTool.L_prePorcessing[j].Enable;
                                //}

                                //////Win_BlobAnalyseTool.Instance.ckb_toolEnable.Checked = L_toolList[i].enable;
                                //Win_BlobTool.Instance.ckb_displaySearchRegion.Checked = blobAnalyseTool.displaySearchRegion;
                                //Win_BlobTool.Instance.ckb_displayCross.Checked = blobAnalyseTool.displayCross;
                                //Win_BlobTool.Instance.tbx_lineWidth.Text = blobAnalyseTool.lineWidth.ToString();
                                //Win_BlobTool.Instance.rdo_outCircleFillMode.Checked = blobAnalyseTool.outCircleDrawMode == FillMode.Fill ? true : false;
                                //Win_BlobTool.Instance.rdo_outCircleMarginMode.Checked = blobAnalyseTool.outCircleDrawMode == FillMode.Fill ? false : true;
                                //Win_BlobTool.Instance.rdo_regionFillMode.Checked = blobAnalyseTool.regionDrawMode == FillMode.Fill ? true : false;
                                //Win_BlobTool.Instance.rdo_regionMarginMode.Checked = blobAnalyseTool.regionDrawMode == FillMode.Fill ? false : true;
                                //Win_BlobTool.Instance.ckb_displayRegion.Checked = blobAnalyseTool.displayRegion;
                                //Win_BlobTool.Instance.rdo_outCircleFillMode.Checked = blobAnalyseTool.outCircleDrawMode == FillMode.Fill ? true : false;
                                //Win_BlobTool.Instance.ckb_DisplayOutCircle.Checked = blobAnalyseTool.displayOutCircle;
                                //Win_BlobTool.Instance.comboBox1.SelectedIndex = (int)blobAnalyseTool.sortMode;
                                //Win_BlobTool.Instance.textBox1.Value = blobAnalyseTool.spanPixelNum;
                                //Win_BlobTool.Instance.cbx_searchRegionType.Text = blobAnalyseTool.searchRegionType.ToString();
                                //Win_BlobTool.Instance.trackBar1.Value = (blobAnalyseTool.minThreshold);
                                //Win_BlobTool.Instance.trackBar2.Value = (blobAnalyseTool.maxThreshold);
                                //Win_BlobTool.Instance.numericUpDown1.Value = blobAnalyseTool.minThreshold;
                                //Win_BlobTool.Instance.numericUpDown2.Value = blobAnalyseTool.maxThreshold;
                                //Win_BlobTool.Instance.rdo_regionFillMode.Checked = blobAnalyseTool.regionDrawMode == FillMode.Fill ? true : false;
                                break;
                            #endregion
                            #region FindLine
                            case ToolType.FindLine:
                                Win_FindLineTool.Instance.XXXXXXXXXXXXXlbl_title.Text = (string.Format("抓边工具[{0}.{1}]", this.jobName, L_toolList[i].toolName));
                                Win_FindLineTool.Instance.Activate();
                                Win_FindLineTool.Instance.jobName = this.jobName;
                                Win_FindLineTool.Instance.toolName = L_toolList[i].toolName;
                                Win_FindLineTool.Instance.WindowState = FormWindowState.Normal;
                                FindLineTool FindLineTool = (FindLineTool)(L_toolList[i].tool);
                                Win_FindLineTool.FindLineTool = FindLineTool;
                                Application.DoEvents();

                                inputItemNum = (L_toolList[i]).input.Count;

                                for (int j = 0; j < inputItemNum; j++)
                                {
                                    string inputItemName = L_toolList[i].input[j].IOName;
                                    string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                    if (sourceFrom == string.Empty)
                                    {
                                        continue;
                                    }
                                    if (inputItemName == "图像" || inputItemName == "InputImage")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];

                                        HImage hImage;
                                        FindLineTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, false, i, this, out hImage);
                                        FindLineTool.toolPar.InputPar.InPuImage = hImage;

                                        //FindLineTool.toolPar.InputPar.InPuImage = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as HImage;
                                        Win_FindLineTool.Instance.vLinkImage1.ImageText = sourceFrom.Substring(2);
                                        if (FindLineTool.toolPar.InputPar.InPuImage == null)
                                        {
                                            continue;
                                        }
                                    }
                                    else if (inputItemName == "跟随位置" || inputItemName == "Pose")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];
                                        FindLineTool.toolPar.InputPar.Pose = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as List<XYU>;
                                        Win_FindLineTool.Instance.vLinkPose1.ROIText = sourceFrom.Substring(2);
                                        if (FindLineTool.toolPar.InputPar.Pose == null)
                                        {
                                            continue;
                                        }
                                    }
                                }

                                if (FindLineTool.toolPar.InputPar.InPuImage != null)
                                    Win_FindLineTool.Instance.PImageWin.Image = (FindLineTool.toolPar.InputPar.InPuImage);

                                Win_FindLineTool.Instance.Show();
                                break;
                            #endregion
                            #region FindCircle
                            case ToolType.FindCircle:
                                Win_FindCircleTool.Instance.XXXXXXXXXXXXXlbl_title.Text = (string.Format("抓圆工具[{0}.{1}]", this.jobName, L_toolList[i].toolName));
                                Win_FindCircleTool.Instance.Activate();
                                Win_FindCircleTool.Instance.jobName = this.jobName;
                                Win_FindCircleTool.Instance.toolName = L_toolList[i].toolName;

                                Win_FindCircleTool.Instance.WindowState = FormWindowState.Normal;
                                FindCircleTool FindCircleTool = (FindCircleTool)(L_toolList[i].tool);
                                Win_FindCircleTool.FindCircleTool = FindCircleTool;

                                Application.DoEvents();

                                inputItemNum = (L_toolList[i]).input.Count;

                                for (int j = 0; j < inputItemNum; j++)
                                {
                                    string inputItemName = L_toolList[i].input[j].IOName;
                                    string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                    if (sourceFrom == string.Empty)
                                    {
                                        continue;
                                    }
                                    if (inputItemName == "图像" || inputItemName == "InputImage")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];

                                        HImage hImage;
                                        FindCircleTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, false, i, this, out hImage);
                                        FindCircleTool.toolPar.InputPar.InPuImage = hImage;

                                        //FindCircleTool.toolPar.InputPar.InPuImage = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as HImage;
                                        Win_FindCircleTool.Instance.vLinkImage1.ImageText = sourceFrom.Substring(2);
                                        if (FindCircleTool.toolPar.InputPar.InPuImage == null)
                                        {
                                            continue;
                                        }
                                    }
                                    else if (inputItemName == "跟随位置" || inputItemName == "Pose")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];
                                        FindCircleTool.toolPar.InputPar.Pose = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as List<XYU>;
                                        Win_FindCircleTool.Instance.vLinkPose1.ROIText = sourceFrom.Substring(2);
                                        if (FindCircleTool.toolPar.InputPar.Pose == null)
                                        {
                                            continue;
                                        }
                                    }

                                }


                                if (FindCircleTool.toolPar.InputPar.InPuImage != null)
                                    Win_FindCircleTool.Instance.PImageWin.Image = (FindCircleTool.toolPar.InputPar.InPuImage);

                                Win_FindCircleTool.Instance.Show();
                                break;
                            #endregion
                            #region ColorToRGB
                            case ToolType.ImageDecomposition:
                                Win_ColorToRGBTool.Instance.XXXXXXXXXXXXXlbl_title.Text = string.Format("彩图转RGB图    [ {0} . {1} ]", this.jobName, L_toolList[i].toolName);
                                //Win_ColorToRGBTool.Instance.StartPosition = FormStartPosition.Manual;
                                //Win_ColorToRGBTool.Instance.Location = new System.Drawing.Point(System.Windows.Forms.SystemInformation.VirtualScreen.Width - Win_ShapeMatchTool.Instance.Width - 20, 200);        //让其显示在右上方，防止挡住图像窗口
                                //Win_ColorToRGBTool.Instance.TopMost = true;
                                Win_ColorToRGBTool.Instance.Activate();

                                Win_ColorToRGBTool.Instance.jobName = this.jobName;
                                Win_ColorToRGBTool.Instance.toolName = L_toolList[i].toolName;
                                Win_ColorToRGBTool.Instance.Show();
                                Win_ColorToRGBTool.Instance.WindowState = FormWindowState.Normal;
                                // Win_ColorToRGBTool.Instance.btn_runColorToRGBTool.Focus();
                                ColorToRGBTool colorToRGBTool = (ColorToRGBTool)(L_toolList[i].tool);
                                Win_ColorToRGBTool.colorToRGBTool = colorToRGBTool;
                                Application.DoEvents();

                                if (colorToRGBTool.inputImage != null)
                                    colorToRGBTool.GetImageWindowControl().hv_window.DispObj(colorToRGBTool.inputImage);
                                else
                                { }
                                //////colorToRGBTool.ClearWindow(this.jobName);

                                //将对象信息更新到界面
                                break;
                            #endregion
                            #region LLIntersect
                            case ToolType.LLIntersect:
                                Win_LLIntersectTool.Instance.XXXXXXXXXXXXXlbl_title.Text = (string.Format("线线相交[{0}.{1}]", this.jobName, L_toolList[i].toolName));
                                Win_LLIntersectTool.Instance.Activate();
                                Win_LLIntersectTool.Instance.jobName = this.jobName;
                                Win_LLIntersectTool.Instance.toolName = L_toolList[i].toolName;

                                Win_LLIntersectTool.Instance.WindowState = FormWindowState.Normal;
                                LLIntersectTool LLIntersectTool = (LLIntersectTool)(L_toolList[i].tool);
                                Win_LLIntersectTool.LLIntersectTool = LLIntersectTool;
                                
                                Application.DoEvents();

                                inputItemNum = (L_toolList[i]).input.Count;

                                for (int j = 0; j < inputItemNum; j++)
                                {
                                    string inputItemName = L_toolList[i].input[j].IOName;
                                    string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                    if (sourceFrom == string.Empty)
                                    {
                                        continue;
                                    }
                                    if (inputItemName == "图像" || inputItemName == "InputImage")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];
                                        Win_LLIntersectTool.Instance.vLinkImage1.ImageText = sourceFrom.Substring(2);

                                        HImage hImage;
                                        LLIntersectTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, false, i, this, out hImage);
                                        LLIntersectTool.toolPar.InputPar.图像 = hImage;

                                        //LLIntersectTool.toolPar.InputPar.图像 = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as HImage;
                                        if (LLIntersectTool.toolPar.InputPar.图像 == null)
                                        {
                                            continue;
                                        }
                                    }
                                    else if (inputItemName == "Line1")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];
                                        LLIntersectTool.toolPar.InputPar.Line1 = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as Line;
                                        Win_LLIntersectTool.Instance.vLinkLine1.LineText = sourceFrom.Substring(2);
                                        if (LLIntersectTool.toolPar.InputPar.Line1 == null)
                                        {
                                            continue;
                                        }

                                    }
                                    else if (inputItemName == "Line2")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];
                                        LLIntersectTool.toolPar.InputPar.Line2= FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as Line;
                                        Win_LLIntersectTool.Instance.vLinkLine2.LineText = sourceFrom.Substring(2);
                                        if (LLIntersectTool.toolPar.InputPar.Line2== null)
                                        {
                                            continue;
                                        }
                                    }

                                }

                                if (LLIntersectTool.toolPar.InputPar.图像 != null)
                                {
                                    Win_LLIntersectTool.Instance.PImageWin.Image = (LLIntersectTool.toolPar.InputPar.图像);

                                }

                                Win_LLIntersectTool.Instance.Show();
                                break;

                            #endregion
                            #region LLAngle
                            case ToolType.LLAngle:
                                Win_LLAngleTool.Instance.XXXXXXXXXXXXXlbl_title.Text = (string.Format("线线相交[{0}.{1}]", this.jobName, L_toolList[i].toolName));
                                Win_LLAngleTool.Instance.Activate();
                                Win_LLAngleTool.Instance.jobName = this.jobName;
                                Win_LLAngleTool.Instance.toolName = L_toolList[i].toolName;

                                Win_LLAngleTool.Instance.WindowState = FormWindowState.Normal;
                                LLAngleTool LLAngleTool = (LLAngleTool)(L_toolList[i].tool);
                                Win_LLAngleTool.LLAngleTool = LLAngleTool;

                                Application.DoEvents();

                                inputItemNum = (L_toolList[i]).input.Count;

                                for (int j = 0; j < inputItemNum; j++)
                                {
                                    string inputItemName = L_toolList[i].input[j].IOName;
                                    string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                    if (sourceFrom == string.Empty)
                                    {
                                        continue;
                                    }
                                    if (inputItemName == "图像" || inputItemName == "InputImage")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];
                                        Win_LLAngleTool.Instance.vLinkImage1.ImageText = sourceFrom.Substring(2);

                                        HImage hImage;
                                        LLAngleTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, false, i, this, out hImage);
                                        LLAngleTool.toolPar.InputPar.图像 = hImage;

                                        //LLAngleTool.toolPar.InputPar.图像 = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as HImage;
                                        if (LLAngleTool.toolPar.InputPar.图像 == null)
                                        {
                                            continue;
                                        }
                                    }
                                    else if (inputItemName == "Line1")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];
                                        LLAngleTool.toolPar.InputPar.Line1 = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as Line;
                                        Win_LLAngleTool.Instance.vLinkLine1.LineText = sourceFrom.Substring(2);
                                        if (LLAngleTool.toolPar.InputPar.Line1 == null)
                                        {
                                            continue;
                                        }

                                    }
                                    else if (inputItemName == "Line2")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];
                                        LLAngleTool.toolPar.InputPar.Line2 = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as Line;
                                        Win_LLAngleTool.Instance.vLinkLine2.LineText = sourceFrom.Substring(2);
                                        if (LLAngleTool.toolPar.InputPar.Line2 == null)
                                        {
                                            continue;
                                        }

                                    }

                                }
                                if (LLAngleTool.toolPar.InputPar.图像 != null)
                                {
                                    Win_LLAngleTool.Instance.PImageWin.Image = (LLAngleTool.toolPar.InputPar.图像);
                                }
                                Win_LLAngleTool.Instance.Show();
                                break;

                            #endregion
                            #region PPDistance
                            case ToolType.P2PDistance:
                                Win_PPDistanceTool.Instance.XXXXXXXXXXXXXlbl_title.Text = (string.Format("点点距离[{0}.{1}]", this.jobName, L_toolList[i].toolName));
                                Win_PPDistanceTool.Instance.Activate();
                                Win_PPDistanceTool.Instance.jobName = this.jobName;
                                Win_PPDistanceTool.Instance.toolName = L_toolList[i].toolName;

                                Win_PPDistanceTool.Instance.WindowState = FormWindowState.Normal;
                                PPDistanceTool PPDistanceTool = (PPDistanceTool)(L_toolList[i].tool);
                                Win_PPDistanceTool.PPDistanceTool = PPDistanceTool;

                                Application.DoEvents();

                                inputItemNum = (L_toolList[i]).input.Count;

                                for (int j = 0; j < inputItemNum; j++)
                                {
                                    string inputItemName = L_toolList[i].input[j].IOName;
                                    string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                    if (sourceFrom == string.Empty)
                                    {
                                        continue;
                                    }
                                    if (inputItemName == "图像" || inputItemName == "InputImage")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];
                                        Win_PPDistanceTool.Instance.vLinkImage1.ImageText = sourceFrom.Substring(2);

                                        HImage hImage;
                                        PPDistanceTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, false, i, this, out hImage);
                                        PPDistanceTool.toolPar.InputPar.图像 = hImage;

                                        //PPDistanceTool.toolPar.InputPar.图像 = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as HImage;
                                        if (PPDistanceTool.toolPar.InputPar.图像 == null)
                                        {
                                            continue;
                                        }
                                    }

                                    else if (inputItemName == "Point1")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];
                                        PPDistanceTool.toolPar.InputPar.Point1 = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as XY;
                                        Win_PPDistanceTool.Instance.vLinkPoint1.PointText = sourceFrom.Substring(2);
                                        if (PPDistanceTool.toolPar.InputPar.Point1 == null)
                                        {
                                            continue;
                                        }
                                    }

                                    else if (inputItemName == "Point2")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];
                                        PPDistanceTool.toolPar.InputPar.Point2 = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as XY;
                                        Win_PPDistanceTool.Instance.vLinkPoint2.PointText = sourceFrom.Substring(2);
                                        if (PPDistanceTool.toolPar.InputPar.Point2 == null)
                                        {
                                            continue;
                                        }
                                    }
                                }

                                if (PPDistanceTool.toolPar.InputPar.图像 != null)
                                {
                                    Win_PPDistanceTool.Instance.PImageWin.Image = (PPDistanceTool.toolPar.InputPar.图像);

                                }

                                Win_PPDistanceTool.Instance.Show();
                                break;

                            #endregion
                            #region PLDistance
                            case ToolType.P2LDistance:
                                Win_PLDistanceTool.Instance.XXXXXXXXXXXXXlbl_title.Text = (string.Format("点线距离[{0}.{1}]", this.jobName, L_toolList[i].toolName));
                                Win_PLDistanceTool.Instance.Activate();
                                Win_PLDistanceTool.Instance.jobName = this.jobName;
                                Win_PLDistanceTool.Instance.toolName = L_toolList[i].toolName;

                                Win_PLDistanceTool.Instance.WindowState = FormWindowState.Normal;
                                PLDistanceTool PLDistanceTool = (PLDistanceTool)(L_toolList[i].tool);
                                Win_PLDistanceTool.PLineDistanceTool = PLDistanceTool;
                                Application.DoEvents();

                                inputItemNum = (L_toolList[i]).input.Count;

                                for (int j = 0; j < inputItemNum; j++)
                                {
                                    string inputItemName = L_toolList[i].input[j].IOName;
                                    string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                    if (sourceFrom == string.Empty)
                                    {
                                        continue;
                                    }
                                    if (inputItemName == "图像" || inputItemName == "InputImage")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];
                                        Win_PLDistanceTool.Instance.vLinkImage1.ImageText = sourceFrom.Substring(2);

                                        HImage hImage;
                                        PLDistanceTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, false, i, this, out hImage);
                                        PLDistanceTool.toolPar.InputPar.图像 = hImage;

                                        //PPDistanceTool.toolPar.InputPar.图像 = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as HImage;
                                        if (PLDistanceTool.toolPar.InputPar.图像 == null)
                                        {
                                            continue;
                                        }
                                    }
                                    else if (inputItemName == "Point1")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];
                                        PLDistanceTool.toolPar.InputPar.Point1 = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as XY;
                                        Win_PLDistanceTool.Instance.vLinkPoint1.PointText = sourceFrom.Substring(2);
                                        if (PLDistanceTool.toolPar.InputPar.Point1 == null)
                                        {
                                            continue;
                                        }
                                    }
                                    else if (inputItemName == "Line")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];
                                        PLDistanceTool.toolPar.InputPar.Line1 = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as Line;
                                        Win_PLDistanceTool.Instance.vLinkLine1.LineText = sourceFrom.Substring(2);
                                        if (PLDistanceTool.toolPar.InputPar.Line1 == null)
                                        {
                                            continue;
                                        }

                                    }

                                }

                                if (PLDistanceTool.toolPar.InputPar.图像 != null)
                                {
                                    Win_PLDistanceTool.Instance.PImageWin.Image = (PLDistanceTool.toolPar.InputPar.图像);
                                }
                                Win_PLDistanceTool.Instance.Show();

                                break;

                            #endregion
                            #region RRegionGraySubtract
                            case ToolType.RRegionGraySubtract:
                                Win_RRegionGraySubtractTool.Instance.XXXXXXXXXXXXXlbl_title.Text = (string.Format("灰度比较工具[{0}.{1}]", this.jobName, L_toolList[i].toolName));
                                Win_RRegionGraySubtractTool.Instance.Activate();
                                Win_RRegionGraySubtractTool.Instance.jobName = this.jobName;
                                Win_RRegionGraySubtractTool.Instance.toolName = L_toolList[i].toolName;
                                Win_RRegionGraySubtractTool.Instance.WindowState = FormWindowState.Normal;
                                RRegionGraySubtractTool RRegionGraySubtractTool = (RRegionGraySubtractTool)(L_toolList[i].tool);
                                Win_RRegionGraySubtractTool.RRegionGraySubtractTool = RRegionGraySubtractTool;
                                Application.DoEvents();
                                inputItemNum = (L_toolList[i]).input.Count;
                                for (int j = 0; j < inputItemNum; j++)
                                {
                                    string inputItemName = L_toolList[i].input[j].IOName;
                                    string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                    if (sourceFrom == string.Empty)
                                    {
                                        continue;
                                    }
                                    if (inputItemName == "图像" || inputItemName == "InputImage")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];
                                        Win_RRegionGraySubtractTool.Instance.vLinkImage1.ImageText = sourceFrom.Substring(2);

                                        HImage hImage;
                                        RRegionGraySubtractTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, false, i, this, out hImage);
                                        RRegionGraySubtractTool.toolPar.InputPar.图像 = hImage;
                                        RRegionGraySubtractTool.toolPar.RunPar.图像 = RRegionGraySubtractTool.toolPar.InputPar.图像;
                                        //RRegionGraySubtractTool.toolPar.InputPar.图像 = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as HImage;
                                        if (RRegionGraySubtractTool.toolPar.InputPar.图像 == null)
                                        {
                                            continue;
                                        }
                                    }
                                    else if (inputItemName == "跟随位置" || inputItemName == "Pose")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];
                                        Win_RRegionGraySubtractTool.Instance.vLinkPose1.ROIText = sourceFrom.Substring(2);
                                        RRegionGraySubtractTool.toolPar.InputPar.Pose = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as List<XYU>;
                                        if (RRegionGraySubtractTool.toolPar.InputPar.Pose == null)
                                        {
                                            continue;
                                        }
                                    }
                                   
                                }
                                if (RRegionGraySubtractTool.toolPar.InputPar.图像 != null)
                                {
                                    Win_RRegionGraySubtractTool.Instance.PImageWin.Image = (RRegionGraySubtractTool.toolPar.InputPar.图像);
                                }
                                Win_RRegionGraySubtractTool.Instance.Show();
                                break;
                            #endregion
                            #region VerdictTool
                            case ToolType.VerdictMeasure:
                                Win_VerdictTool.Instance.XXXXXXXXXXXXXlbl_title.Text = (string.Format("数据判定[{0}.{1}]", this.jobName, L_toolList[i].toolName));
                                Win_VerdictTool.Instance.Activate();
                                Win_VerdictTool.Instance.jobName = this.jobName;
                                Win_VerdictTool.Instance.toolName = L_toolList[i].toolName;

                                Win_VerdictTool.Instance.WindowState = FormWindowState.Normal;
                                VerdictTool verdictTool = (VerdictTool)(L_toolList[i].tool);
                                Win_VerdictTool.verdictTool = verdictTool;
                                Application.DoEvents();

                                inputItemNum = (L_toolList[i]).input.Count;
                                for (int j = 0; j < inputItemNum; j++)
                                {
                                    string inputItemName = L_toolList[i].input[j].IOName;
                                    string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                    if (sourceFrom == string.Empty)
                                    {
                                        continue;
                                    }
                                    if (inputItemName == "图像" || inputItemName == "InputImage")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];
                                        Win_VerdictTool.Instance.vLinkImage1.ImageText = sourceFrom.Substring(2);

                                        HImage hImage;
                                        verdictTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, false, i, this, out hImage);
                                        verdictTool.toolPar.InputPar.图像 = hImage;
                                        if (verdictTool.toolPar.InputPar.图像 == null)
                                        {
                                            continue;
                                        }
                                    }
                                    else if (inputItemName == "量测值")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];
                                        verdictTool.toolPar.InputPar.DValue = Convert.ToDouble(FindToolInfoByName(sourceToolName).GetOutput(toolItem).value);
                                        if (verdictTool.toolPar.InputPar.DValue.ToString() == "") 
                                        {
                                            continue;
                                        }
                                    }
                                }
                                if (verdictTool.toolPar.InputPar.图像 != null)
                                {
                                    Win_VerdictTool.Instance.PImageWin.Image = (verdictTool.toolPar.InputPar.图像);
                                }
                                Win_VerdictTool.Instance.Show();
                                break;

                            #endregion
                            #region DefectDetection
                            case ToolType.DefectDetection:
                                Win_DefectDetectionTool.Instance.XXXXXXXXXXXXXlbl_title.Text = (string.Format("缺陷检测[{0}.{1}]", this.jobName, L_toolList[i].toolName));
                                Win_DefectDetectionTool.Instance.Activate();
                                Win_DefectDetectionTool.Instance.jobName = this.jobName;
                                Win_DefectDetectionTool.Instance.toolName = L_toolList[i].toolName;

                                Win_DefectDetectionTool.Instance.WindowState = FormWindowState.Normal;
                                DefectDetectionTool DefectDetectionTool = (DefectDetectionTool)(L_toolList[i].tool);
                                Win_DefectDetectionTool.DefectDetectionTool = DefectDetectionTool;

                                Application.DoEvents();

                                inputItemNum = (L_toolList[i]).input.Count;

                                for (int j = 0; j < inputItemNum; j++)
                                {
                                    string inputItemName = L_toolList[i].input[j].IOName;
                                    string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                    if (sourceFrom == string.Empty)
                                    {
                                        continue;
                                    }
                                    if (inputItemName == "图像" || inputItemName == "InputImage")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];

                                        HImage hImage;
                                        DefectDetectionTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, false, i, this, out hImage);
                                        DefectDetectionTool.toolPar.InputPar.图像 = hImage;
                                        DefectDetectionTool.toolPar.RunPar.图像 = DefectDetectionTool.toolPar.InputPar.图像;

                                        //FindCircleTool.toolPar.InputPar.InPuImage = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as HImage;
                                        Win_DefectDetectionTool.Instance.vLinkImage1.ImageText = sourceFrom.Substring(2);
                                        if (DefectDetectionTool.toolPar.InputPar.图像 == null)
                                        {
                                            continue;
                                        }
                                    }
                                    else if (inputItemName == "跟随位置" || inputItemName == "Pose")
                                    {
                                        string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                        string toolItem = sourceFrom.Split('.')[1];
                                        DefectDetectionTool.toolPar.InputPar.Pose = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as List<XYU>;
                                        Win_DefectDetectionTool.Instance.vLinkPose1.ROIText = sourceFrom.Substring(2);
                                        if (DefectDetectionTool.toolPar.InputPar.Pose == null)
                                        {
                                            continue;
                                        }
                                    }

                                }


                                if (DefectDetectionTool.toolPar.InputPar.图像 != null)
                                    Win_DefectDetectionTool.Instance.PImageWin.Image = (DefectDetectionTool.toolPar.InputPar.图像);

                                Win_DefectDetectionTool.Instance.Show();
                                break;
                                #endregion
                        }
                    }
                }
                Job.loadForm = false;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        public object GetValue(object obj, string name)
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
        /// 是否启用事件，也就是不执行本次触发的事件
        /// </summary>
        internal static bool loadForm = true;
        /// <summary>
        /// 为防止datagridview出现大红叉，此处申明委托使用创建控件的线程调用控件
        /// </summary>
        public delegate void ShowTestData();

        /// <summary>
        /// 图像存储队列
        /// </summary>
        //private Queue<HImage> QueueImages = new Queue<HImage>();



        object obj1 = new object();
        /// <summary>
        /// 运行流程
        /// </summary>
        /// <param name="initRun">若为程序启动时的第一次运行，相关通讯工具不被执行，通常情况下此参数传True值即可</param>
        /// <returns></returns>
        public List<object> Run(bool initRun = false)
        {
            try
            {
                if (obj1==null)
                {
                    obj1 = new object();
                }
                lock (obj1)
                {
                    bool JobIsVisable = false;
                    //此处使用委托调用，
                    //string d1 = DateTime.Now.ToString(jobName + "起始时间====yyyyMMddhhmmssfff");
                
                    ShowTestData showTestData = delegate () { GetJobTree().ShowNodeToolTips = true; };
                    Win_Job.Instance.tbc_jobs.Invoke(showTestData);
                    string jobname = Win_Job.Instance.JobEdit_JobName.Text.Split('：')[1];
                    JobIsVisable = jobname == jobName;
                    //GetJobTree().ShowNodeToolTips = true;
                    Stopwatch jobElapsedTime = new Stopwatch();
                    jobElapsedTime.Restart();
                    recordElapseTime = 0;
                    bJobProRunStatus = true;
                
                    HImage loadImage = null;
                    ToolBase tool = new ToolBase();
                    bool isAcqExists = false;//判断Job内是否存在取像工具
                    //开始逐个执行各工具
                    jobRunStatu = JobRunStatu.Succeed;
                    List<object> L_result = new List<object>();
                    int toolIndex = -1;
                    Application.DoEvents();

                    for (int i = 0; i < L_toolList.Count; i++)
                    {
                        bool isCamEnable = false;
                        if (isAcqExists || isCamEnable)
                        {
                            isAcqExists = true;
                        }
                        if (L_toolList[i].toolType == ToolType.ImageAcq)
                        {
                            AcqImageTool SDK_hikVisionTool = (AcqImageTool)L_toolList[i].tool;
                            if (!L_toolList[i].enable)
                            {
                                isCamEnable = false;
                                continue;
                            }
                            else
                            {
                                isCamEnable = true;
                                isAcqExists = true;
                            }
                        }
                    }
                    for (int i = 0; i < L_toolList.Count; i++)
                    {
                        //hzy20220622
                        if (Start.JobRunMarkM)
                        {
                            return null;
                        }
                       
                        toolIndex++;
                        TreeNode treeNode = GetToolNodeByNodeText(L_toolList[i].toolName);
                        inputItemNum = (L_toolList[i]).input.Count;//输入端口数量
                        outputItemNum = (L_toolList[i]).output.Count;//输出端口数量
                        bool sourceValueIsEmpty = false;//此变量判断输入源值是否为空，若为空就终止流程执行
                       
                        #region ImageAcq
                        if (L_toolList[i].toolType == ToolType.ImageAcq)
                        {
                            
                            AcqImageTool SDK_hikVisionTool = (AcqImageTool)L_toolList[i].tool;
                            if (!L_toolList[i].enable)
                            {
                                SDK_hikVisionTool.toolRunStatu = (ToolRunStatu.未启用);
                                if (JobIsVisable)
                                {
                                    treeNode.ToolTipText = SDK_hikVisionTool.toolRunStatu.ToString();
                                    treeNode.ForeColor = Color.DarkGray;
                                }
                                continue;
                            }
                            SDK_hikVisionTool.Run(true, false, L_toolList[i].toolName);
                            if (Start.isCamBreakWhile)
                            {
                                return null;
                            }

                            loadImage = SDK_hikVisionTool.toolPar.ResultPar.图像;

                            if (SDK_hikVisionTool.toolRunStatu != (ToolRunStatu.成功))
                            {
                                if (!Configuration.SpeedMode)
                                {
                                    Win_Main.Instance.OutputMsg(string.Format("流程 [{0}] 运行失败，原因：工具 [{1}] {2}", jobName, L_toolList[i].toolName, L_toolList[i].tool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = string.Format("状态：{0}\r\n耗时：{1}ms\r\n说明：{2}", SDK_hikVisionTool.toolRunStatu.ToString(), 0, L_toolList[i].toolTipInfo);
                                    }
                                }
                                if (JobIsVisable)
                                {
                                    treeNode.ForeColor = Color.Red;
                                }
                                jobRunStatu = JobRunStatu.Fail;
                                strErrToolName = L_toolList[i].toolName;
                                break;
                            }
                            else
                            {

                                if (JobIsVisable)
                                {
                                    try
                                    {
                                        treeNode.ForeColor = Color.LightGreen;
                                    }
                                    catch { }

                                }
                            }

                            for (int j = 0; j < this.outputItemNum; j++)
                            {
                                string outputItem = L_toolList[i].output[j].IOName;

                                string[] strs = Regex.Split(outputItem, " . ");
                                ToolParBase result3 = ((AcqImageTool)(L_toolList[i].tool)).toolPar;

                                object value = result3;

                                value = GetValue(value, "ResultPar");
                                for (int k = 0; k < strs.Length; k++)
                                {
                                    value = GetValue(value, strs[k]);
                                }

                                L_toolList[i].GetOutput(outputItem).value = value;
                                if (JobIsVisable)
                                {
                                    try
                                    {
                                        GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, outputItem, false).ToolTipText = FormatShowTip(value);
                                    }
                                    catch { }
                                }
                            }

                        }
                        #endregion

                        #region SaveImage
                        else if (L_toolList[i].toolType == ToolType.SaveImage)
                        {
                            SaveImageTool saveImageTool = (SaveImageTool)L_toolList[i].tool;
                            if (!L_toolList[i].enable)
                            {
                                if (!isAcqExists)
                                {
                                    if (i == L_toolList.Count - 1)
                                    {
                                        (tool as AcqImageTool).ReadOrWriteImage(false, false);//删除图片
                                    }
                                }

                                saveImageTool.toolRunStatu = (ToolRunStatu.未启用);

                                if (JobIsVisable)
                                {
                                    treeNode.ToolTipText = saveImageTool.toolRunStatu.ToString();
                                    treeNode.ForeColor = Color.DarkGray;
                                }

                                continue;
                            }
                            //////saveImageTool.ClearLastInput();

                            //Hzy20220509//////////////////
                            bool isLocal = false;
                            ///////////////////////////////

                            for (int j = 0; j < inputItemNum; j++)
                            {
                                string inputItemName = L_toolList[i].input[j].IOName;
                                string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                if (sourceFrom == string.Empty)
                                {
                                    saveImageTool.toolRunStatu = (ToolRunStatu.未指定输入图像);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = saveImageTool.toolRunStatu.ToString();
                                        treeNode.ForeColor = Color.Red;
                                    }
                                    Win_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, saveImageTool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    return L_result;
                                }

                                string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                string toolItem = sourceFrom.Split('.')[1];
                                if (inputItemName == "图像" || inputItemName == "InputImage")
                                {
                                    //Hzy20220507
                                    saveImageTool.getOriginalJobAcquisition(sourceFrom, sourceToolName, this, out isLocal, out tool);

                                    //非队列模式
                                    HImage hImage;
                                    saveImageTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, true, i, this, out hImage);
                                    saveImageTool.toolPar.InputPar.图像 = hImage;

                                    loadImage = saveImageTool.toolPar.InputPar.图像 as HImage;
                                    saveImageTool.DisplayImage(i, saveImageTool.toolPar.InputPar.图像 as HImage);

                                    //saveImageTool.toolPar.InputPar.图像 = mjob.FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as HObject;
                                    if (JobIsVisable)
                                    {
                                        GetToolNodeByNodeText(inputItemName + sourceFrom).ToolTipText = FormatShowTip(saveImageTool.toolPar.InputPar.图像);
                                        if (saveImageTool.toolPar.InputPar.图像 == null)
                                        {
                                            saveImageTool.toolRunStatu = ToolRunStatu.未指定输入图像;
                                            if (JobIsVisable)
                                            {
                                                treeNode.ToolTipText = saveImageTool.toolRunStatu.ToString();
                                                treeNode.ForeColor = Color.Red;
                                            }
                                            sourceValueIsEmpty = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (sourceValueIsEmpty)
                                break;
                            saveImageTool.Run(false, false, L_toolList[i].toolName);
                            if (saveImageTool.toolRunStatu != (ToolRunStatu.成功))
                            {
                                //hzy20220509//////////////////////
                                if (!isLocal)
                                {
                                    if ((tool as AcqImageTool).QeHImages.Count > 0)
                                    {
                                        saveImageTool.toolPar.InputPar.图像 = (tool as AcqImageTool).ReadOrWriteImage(false, false);
                                    }
                                }
                                ///////////////////////////////////

                                if (!Configuration.SpeedMode)
                                {
                                    Win_Main.Instance.OutputMsg(string.Format("流程 [{0}] 运行失败，原因：工具 [{1}] {2}", jobName, L_toolList[i].toolName, L_toolList[i].tool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = string.Format("状态：{0}\r\n耗时：{1}ms\r\n说明：{2}", saveImageTool.toolRunStatu.ToString(), 0, L_toolList[i].toolTipInfo);
                                    }
                                }
                                if (JobIsVisable)
                                {
                                    treeNode.ForeColor = Color.Red;
                                }
                                jobRunStatu = JobRunStatu.Fail;
                                strErrToolName = L_toolList[i].toolName;
                                break;
                            }
                            else
                            {
                                if (JobIsVisable)
                                {
                                    
                                    try
                                    {
                                        treeNode.ForeColor = Color.LightGreen;
                                    }
                                    catch { }
                                }
                            }



                        }
                        #endregion
                        #region Binarization
                        if (L_toolList[i].toolType == ToolType.Binarization)
                        {
                            BinarizationTool BinarizationTool = (BinarizationTool)L_toolList[i].tool;
                            if (!L_toolList[i].enable)
                            {
                                if (!isAcqExists)
                                {
                                    if (i == L_toolList.Count - 1)
                                    {
                                        (tool as AcqImageTool).ReadOrWriteImage(false, false);//删除图片
                                    }
                                }
                                BinarizationTool.toolRunStatu = (ToolRunStatu.未启用);
                                if (JobIsVisable)
                                {
                                    treeNode.ToolTipText = BinarizationTool.toolRunStatu.ToString();
                                    treeNode.ForeColor = Color.DarkGray;
                                }
                                continue;
                            }

                            //Hzy20220509//////////////////
                            bool isLocal = false;

                            for (int j = 0; j < inputItemNum; j++)
                            {
                                string inputItemName = L_toolList[i].input[j].IOName;
                                string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                if (sourceFrom == string.Empty)
                                {
                                    BinarizationTool.toolRunStatu = (ToolRunStatu.未指定输入图像);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = BinarizationTool.toolRunStatu.ToString();
                                        treeNode.ForeColor = Color.Red;
                                    }
                                    Win_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, BinarizationTool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    return L_result;
                                }
                                if (inputItemName == "图像" || inputItemName == "InputImage")
                                {
                                    string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                    string toolItem = sourceFrom.Split('.')[1];

                                    //Hzy20220507
                                    BinarizationTool.getOriginalJobAcquisition(sourceFrom, sourceToolName, this, out isLocal, out tool);

                                    //非队列模式
                                    HImage hImage;
                                    BinarizationTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, true, i, this, out hImage);
                                    BinarizationTool.toolPar.InputPar.图像 = hImage;

                                    loadImage = BinarizationTool.toolPar.InputPar.图像;
                                    BinarizationTool.DisplayImage(i, BinarizationTool.toolPar.InputPar.图像);

                                    if (JobIsVisable)
                                    {
                                        GetToolNodeByNodeText(inputItemName + sourceFrom).ToolTipText = FormatShowTip(BinarizationTool.toolPar.InputPar.图像);
                                        if (BinarizationTool.toolPar.InputPar.图像 == null)
                                        {
                                            BinarizationTool.toolRunStatu = ToolRunStatu.未指定输入图像;
                                            treeNode.ToolTipText = BinarizationTool.toolRunStatu.ToString();
                                            treeNode.ForeColor = Color.Red;
                                            sourceValueIsEmpty = true;
                                            break;
                                        }
                                    }
                                }
                            }

                            BinarizationTool.Run(true, false, L_toolList[i].toolName);

                            if (BinarizationTool.toolRunStatu != (ToolRunStatu.成功))
                            {
                                //hzy20220509//////////////////////
                                if (!isLocal)
                                {
                                    if ((tool as AcqImageTool).QeHImages.Count > 0)
                                    {
                                        BinarizationTool.toolPar.InputPar.图像 = (tool as AcqImageTool).ReadOrWriteImage(false, false);
                                    }
                                }
                                ///////////////////////////////////

                                if (!Configuration.SpeedMode)
                                {
                                    Win_Main.Instance.OutputMsg(string.Format("流程 [{0}] 运行失败，原因：工具 [{1}] {2}", jobName, L_toolList[i].toolName, L_toolList[i].tool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = string.Format("状态：{0}\r\n耗时：{1}ms\r\n说明：{2}", BinarizationTool.toolRunStatu.ToString(), 0, L_toolList[i].toolTipInfo);
                                    }
                                }
                                if (JobIsVisable)
                                {
                                    treeNode.ForeColor = Color.Red;
                                }
                                jobRunStatu = JobRunStatu.Fail;
                                strErrToolName = L_toolList[i].toolName;
                                break;
                            }
                            else
                            {
                                if (JobIsVisable)
                                {
                                    try
                                    {
                                        treeNode.ForeColor = Color.LightGreen;
                                    }
                                    catch { }
                                }

                            }
                            for (int j = 0; j < this.outputItemNum; j++)
                            {
                                string outputItem = L_toolList[i].output[j].IOName;

                                string[] strs = Regex.Split(outputItem, " . ");
                                ToolParBase result3 = ((BinarizationTool)(L_toolList[i].tool)).toolPar;
                                object value = result3;

                                value = GetValue(value, "ResultPar");
                                for (int k = 0; k < strs.Length; k++)
                                {
                                    value = GetValue(value, strs[k]);
                                }

                                L_toolList[i].GetOutput(outputItem).value = value;
                                if (JobIsVisable)
                                {
                                    try
                                    { 
                                    GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, outputItem, false).ToolTipText = FormatShowTip(value);
                                    }
                                    catch { }
                                }
                            }


                        }
                        #endregion
                        #region Morphology
                        if (L_toolList[i].toolType == ToolType.Morphology)
                        {
                            MorphologyTool MorphologyTool = (MorphologyTool)L_toolList[i].tool;
                            if (!L_toolList[i].enable)
                            {
                                if (!isAcqExists)
                                {
                                    if (i == L_toolList.Count - 1)
                                    {
                                        (tool as AcqImageTool).ReadOrWriteImage(false, false);//删除图片
                                    }
                                }
                                MorphologyTool.toolRunStatu = (ToolRunStatu.未启用);
                                if (JobIsVisable)
                                {
                                    treeNode.ToolTipText = MorphologyTool.toolRunStatu.ToString();
                                    treeNode.ForeColor = Color.DarkGray;
                                }
                                continue;
                            }

                            //Hzy20220509//////////////////
                            bool isLocal = false;
                            ///////////////////////////////

                            for (int j = 0; j < inputItemNum; j++)
                            {
                                string inputItemName = L_toolList[i].input[j].IOName;
                                string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();

                                if (inputItemName == "图像" || inputItemName == "InputImage")
                                {
                                    if (sourceFrom == string.Empty)
                                    {
                                        MorphologyTool.toolRunStatu = (ToolRunStatu.未指定输入图像);

                                        if (JobIsVisable)
                                        {
                                            treeNode.ToolTipText = MorphologyTool.toolRunStatu.ToString();
                                            treeNode.ForeColor = Color.Red;
                                        }
                                        Win_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, MorphologyTool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                        return L_result;
                                    }
                                    string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                    string toolItem = sourceFrom.Split('.')[1];

                                    //Hzy20220507
                                    MorphologyTool.getOriginalJobAcquisition(sourceFrom, sourceToolName, this, out isLocal, out tool);

                                    //非队列模式
                                    HImage hImage;
                                    MorphologyTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, true, i, this, out hImage);
                                    MorphologyTool.toolPar.InputPar.图像 = hImage;

                                    loadImage = MorphologyTool.toolPar.InputPar.图像;
                                    MorphologyTool.DisplayImage(i, MorphologyTool.toolPar.InputPar.图像 as HImage);

                                    if (JobIsVisable)
                                    {
                                        GetToolNodeByNodeText(inputItemName + sourceFrom).ToolTipText = FormatShowTip(MorphologyTool.toolPar.InputPar.图像);
                                        if (MorphologyTool.toolPar.InputPar.图像 == null)
                                        {
                                            MorphologyTool.toolRunStatu = ToolRunStatu.未指定输入图像;
                                            treeNode.ToolTipText = MorphologyTool.toolRunStatu.ToString();
                                            treeNode.ForeColor = Color.Red;
                                            sourceValueIsEmpty = true;
                                            break;
                                        }
                                    }
                                }
                                else if (inputItemName == "ROI" && sourceFrom != "")
                                {
                                    string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                    string toolItem = sourceFrom.Split('.')[1];
                                    MorphologyTool.toolPar.InputPar.搜索区域 = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as HRegion;
                                }
                            }

                            MorphologyTool.Run(true, false, L_toolList[i].toolName);

                            if (MorphologyTool.toolRunStatu != (ToolRunStatu.成功))
                            {
                                //hzy20220509//////////////////////
                                if (!isLocal)
                                {
                                    if ((tool as AcqImageTool).QeHImages.Count > 0)
                                    {
                                        MorphologyTool.toolPar.InputPar.图像 = (tool as AcqImageTool).ReadOrWriteImage(false, false);
                                    }
                                }
                                ///////////////////////////////////

                                if (!Configuration.SpeedMode)
                                {
                                    Win_Main.Instance.OutputMsg(string.Format("流程 [{0}] 运行失败，原因：工具 [{1}] {2}", jobName, L_toolList[i].toolName, L_toolList[i].tool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = string.Format("状态：{0}\r\n耗时：{1}ms\r\n说明：{2}", MorphologyTool.toolRunStatu.ToString(), 0, L_toolList[i].toolTipInfo);
                                    }
                                }
                                if (JobIsVisable)
                                {
                                    treeNode.ForeColor = Color.Red;
                                }
                                jobRunStatu = JobRunStatu.Fail;
                                strErrToolName = L_toolList[i].toolName;
                                break;
                            }
                            else
                            {
                                if (JobIsVisable)
                                {
                                    try
                                    {
                                        treeNode.ForeColor = Color.LightGreen;
                                    }
                                    catch { }
                                }
                            }
                            for (int j = 0; j < this.outputItemNum; j++)
                            {
                                string outputItem = L_toolList[i].output[j].IOName;
                                object value = null;
                                if (L_toolList[i].output[j].ioType == DataType.Region)
                                {
                                    L_toolList[i].GetOutput(outputItem).value = ((MorphologyTool)(L_toolList[i].tool)).toolPar.ResultPar.region;
                                    value = ((MorphologyTool)(L_toolList[i].tool)).toolPar.ResultPar.region;
                                }

                                L_toolList[i].GetOutput(outputItem).value = value;
                                if (JobIsVisable)
                                {
                                    try
                                    {
                                        GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, outputItem, false).ToolTipText = FormatShowTip(value);
                                    }
                                    catch { }
                                }
                            }


                        }
                        #endregion
                        #region FindROI
                        if (L_toolList[i].toolType == ToolType.FindROI)
                        {
                            FindROI FindROI = (FindROI)L_toolList[i].tool;
                            if (!L_toolList[i].enable)
                            {
                                if (!isAcqExists)
                                {
                                    if (i == L_toolList.Count - 1)
                                    {
                                        (tool as AcqImageTool).ReadOrWriteImage(false, false);//删除图片
                                    }
                                }
                                FindROI.toolRunStatu = (ToolRunStatu.未启用);
                                if (JobIsVisable)
                                {
                                    treeNode.ToolTipText = FindROI.toolRunStatu.ToString();
                                    treeNode.ForeColor = Color.DarkGray;
                                }
                                continue;
                            }

                            //Hzy20220509//////////////////
                            bool isLocal = false;
                            ///////////////////////////////

                            for (int j = 0; j < inputItemNum; j++)
                            {
                                string inputItemName = L_toolList[i].input[j].IOName;
                                string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                if (sourceFrom == string.Empty)
                                {
                                    FindROI.toolRunStatu = (ToolRunStatu.未指定输入图像);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = FindROI.toolRunStatu.ToString();
                                        treeNode.ForeColor = Color.Red;
                                    }
                                    Win_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, FindROI.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    return L_result;
                                }
                                if (inputItemName == "图像" || inputItemName == "InputImage")
                                {
                                    string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                    string toolItem = sourceFrom.Split('.')[1];

                                    //Hzy20220507
                                    FindROI.getOriginalJobAcquisition(sourceFrom, sourceToolName, this, out isLocal, out tool);

                                    //非队列模式
                                    HImage hImage;
                                    FindROI.getOriginalJob(sourceFrom, sourceToolName, toolItem, true, i, this, out hImage);
                                    FindROI.toolPar.InputPar.图像 = hImage;

                                    loadImage = FindROI.toolPar.InputPar.图像;
                                    FindROI.DisplayImage(i, FindROI.toolPar.InputPar.图像);

                                    if (JobIsVisable)
                                    {
                                        GetToolNodeByNodeText(inputItemName + sourceFrom).ToolTipText = FormatShowTip(FindROI.toolPar.InputPar.图像);
                                        if (FindROI.toolPar.InputPar.图像 == null)
                                        {
                                            FindROI.toolRunStatu = ToolRunStatu.未指定输入图像;
                                            treeNode.ToolTipText = FindROI.toolRunStatu.ToString();
                                            treeNode.ForeColor = Color.Red;
                                            sourceValueIsEmpty = true;
                                            break;
                                        }
                                    }
                                }
                            }

                            FindROI.Run(true, false, L_toolList[i].toolName);

                            if (FindROI.toolRunStatu != (ToolRunStatu.成功))
                            {
                                //hzy20220509//////////////////////
                                if (!isLocal)
                                {
                                    if ((tool as AcqImageTool).QeHImages.Count > 0)
                                    {
                                        FindROI.toolPar.InputPar.图像 = (tool as AcqImageTool).ReadOrWriteImage(false, false);
                                    }
                                }
                                ///////////////////////////////////

                                if (!Configuration.SpeedMode)
                                {
                                    Win_Main.Instance.OutputMsg(string.Format("流程 [{0}] 运行失败，原因：工具 [{1}] {2}", jobName, L_toolList[i].toolName, L_toolList[i].tool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = string.Format("状态：{0}\r\n耗时：{1}ms\r\n说明：{2}", FindROI.toolRunStatu.ToString(), 0, L_toolList[i].toolTipInfo);
                                    }
                                }
                                if (JobIsVisable)
                                {
                                    treeNode.ForeColor = Color.Red;
                                }
                                jobRunStatu = JobRunStatu.Fail;
                                strErrToolName = L_toolList[i].toolName;
                                break;
                            }
                            else
                            {
                                if (JobIsVisable)
                                {
                                    try
                                    {
                                        treeNode.ForeColor = Color.LightGreen;
                                    }
                                    catch { }
                                }
                            }
                            for (int j = 0; j < this.outputItemNum; j++)
                            {
                                string outputItem = L_toolList[i].output[j].IOName;
                                object value = null;
                                if (L_toolList[i].output[j].ioType == DataType.Region)
                                {
                                    L_toolList[i].GetOutput(outputItem).value = ((FindROI)(L_toolList[i].tool)).toolPar.ResultPar.region;
                                    value = ((FindROI)(L_toolList[i].tool)).toolPar.ResultPar.region;
                                }

                                L_toolList[i].GetOutput(outputItem).value = value;


                                if (JobIsVisable)
                                {
                                    try
                                    {
                                        GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, outputItem, false).ToolTipText = FormatShowTip(value);
                                    }
                                    catch { }
                                }
                            }


                        }
                        #endregion
                        #region FindRegion
                        else if (L_toolList[i].toolType == ToolType.FindRegion)
                        {
                            FindRegionTool FindRegionTool = (FindRegionTool)L_toolList[i].tool;
                            if (!L_toolList[i].enable)
                            {
                                if (!isAcqExists)
                                {
                                    if (i == L_toolList.Count - 1)
                                    {
                                        (tool as AcqImageTool).ReadOrWriteImage(false, false);//删除图片
                                    }
                                }
                                FindRegionTool.toolRunStatu = (ToolRunStatu.未启用);
                                if (JobIsVisable)
                                {
                                    treeNode.ToolTipText = FindRegionTool.toolRunStatu.ToString();
                                    treeNode.ForeColor = Color.DarkGray;
                                }
                                continue;
                            }

                            //Hzy20220509//////////////////
                            bool isLocal = false;
                            ///////////////////////////////

                            for (int j = 0; j < inputItemNum; j++)
                            {
                                string inputItemName = L_toolList[i].input[j].IOName;
                                string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                if (sourceFrom == string.Empty)
                                {
                                    FindRegionTool.toolRunStatu = (ToolRunStatu.未指定输入图像);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = FindRegionTool.toolRunStatu.ToString();
                                        treeNode.ForeColor = Color.Red;
                                    }
                                    Win_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, FindRegionTool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    return L_result;
                                }
                                if (inputItemName == "图像" || inputItemName == "InputImage")
                                {
                                    string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                    string toolItem = sourceFrom.Split('.')[1];

                                    //Hzy20220509
                                    FindRegionTool.getOriginalJobAcquisition(sourceFrom, sourceToolName, this, out isLocal, out tool);

                                    //非队列模式
                                    HImage hImage;
                                    FindRegionTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, true, i, this, out hImage);
                                    FindRegionTool.toolPar.InputPar.图像 = hImage;
                                    FindRegionTool.toolPar.RunPar.图像 = FindRegionTool.toolPar.InputPar.图像;

                                    loadImage = FindRegionTool.toolPar.InputPar.图像;
                                    FindRegionTool.DisplayImage(i, FindRegionTool.toolPar.InputPar.图像);

                                    if (JobIsVisable)
                                    {
                                        GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, inputItemName + sourceFrom).ToolTipText = FormatShowTip(FindRegionTool.toolPar.InputPar.图像);
                                 
                                    }

                                    if (FindRegionTool.toolPar.InputPar.图像 == null)
                                    {
                                        FindRegionTool.toolRunStatu = ToolRunStatu.未指定输入图像;
                                        if (JobIsVisable)
                                        {
                                            treeNode.ToolTipText = FindRegionTool.toolRunStatu.ToString();
                                            treeNode.ForeColor = Color.Red;
                                        }
                                        sourceValueIsEmpty = true;
                                        break;
                                    }
                                }
                                else if (inputItemName == "跟随位置")
                                {
                                    string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                    string toolItem = sourceFrom.Split('.')[1];
                                    FindRegionTool.toolPar.InputPar.Pose = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as List<XYU>;
                                    if (JobIsVisable)
                                    {
                                        try
                                        {
                                            GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, inputItemName + sourceFrom).ToolTipText = FormatShowTip(FindRegionTool.toolPar.InputPar.Pose);
                                        }
                                        catch { }
                                    }
                                }
                            }
                            if (sourceValueIsEmpty)
                                break;

                            FindRegionTool.Run(false, false, L_toolList[i].toolName);

                            if (FindRegionTool.toolRunStatu != (ToolRunStatu.成功))
                            {
                                //Hzy20220509
                                if (!isLocal)
                                {
                                    if ((tool as AcqImageTool).QeHImages.Count > 0)
                                    {
                                        FindRegionTool.toolPar.InputPar.图像 = (tool as AcqImageTool).ReadOrWriteImage(false, false);
                                        FindRegionTool.toolPar.RunPar.图像 = FindRegionTool.toolPar.InputPar.图像;
                                    }
                                }

                                if (!Configuration.SpeedMode)
                                {
                                    Win_Main.Instance.OutputMsg(string.Format("流程 [{0}] 运行失败，原因：工具 [{1}] {2}", jobName, L_toolList[i].toolName, L_toolList[i].tool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = string.Format("状态：{0}\r\n耗时：{1}ms\r\n说明：{2}", FindRegionTool.toolRunStatu.ToString(), 0, L_toolList[i].toolTipInfo);
                                    }
                                }
                                if (JobIsVisable)
                                {
                                    treeNode.ForeColor = Color.Red;
                                }
                                jobRunStatu = JobRunStatu.Fail;
                                strErrToolName = L_toolList[i].toolName;
                                //20220501，斑点工具失败，跳出循环
                                break;
                            }
                            else
                            {
                                if (JobIsVisable)
                                {
                                    try
                                    {
                                        treeNode.ForeColor = Color.LightGreen;
                                    }
                                    catch { }
                                }
                            }

                            for (int j = 0; j < this.outputItemNum; j++)
                            {
                                string outputItem = L_toolList[i].output[j].IOName;
                                object value = null;
                                L_toolList[i].GetOutput(outputItem).value = ((FindRegionTool)(L_toolList[i].tool)).toolPar.ResultsPar.resultRegion;
                                value = ((FindRegionTool)(L_toolList[i].tool)).toolPar.ResultsPar.resultRegion;

                                if (L_toolList[i].output[j].ioType == DataType.Region)
                                {
                                    L_toolList[i].GetOutput(outputItem).value = ((FindRegionTool)(L_toolList[i].tool)).toolPar.ResultsPar.resultRegion;
                                    value = ((FindRegionTool)(L_toolList[i].tool)).toolPar.ResultsPar.resultRegion;
                                }
                                else if (L_toolList[i].output[j].ioType == DataType.Bool)
                                {
                                    L_toolList[i].GetOutput(outputItem).value = ((FindRegionTool)(L_toolList[i].tool)).toolPar.ResultsPar.Consequence;
                                    value = ((FindRegionTool)(L_toolList[i].tool)).toolPar.ResultsPar.Consequence;
                                }
                                if (JobIsVisable)
                                {
                                    try
                                    {
                                        GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, outputItem, false).ToolTipText = FormatShowTip(value);
                                    }
                                    catch { }
                                }
                            }
                        }
                        #endregion
                        #region ColorToRGB
                        else if (L_toolList[i].toolType == ToolType.ImageDecomposition)
                        {
                            ColorToRGBTool colorToRGBTool = (ColorToRGBTool)L_toolList[i].tool;
                            if (!L_toolList[i].enable)
                            {
                                if (!isAcqExists)
                                {
                                    if (i == L_toolList.Count - 1)
                                    {
                                        (tool as AcqImageTool).ReadOrWriteImage(false, false);//删除图片
                                    }
                                }
                                colorToRGBTool.toolRunStatu = (ToolRunStatu.未启用);
                                if (JobIsVisable)
                                {
                                    treeNode.ToolTipText = colorToRGBTool.toolRunStatu.ToString();
                                    treeNode.ForeColor = Color.DarkGray;
                                }
                                continue;
                            }
                            colorToRGBTool.ClearLastInput();

                            //Hzy20220509//////////////////
                            bool isLocal = false;
                            ///////////////////////////////

                            for (int j = 0; j < inputItemNum; j++)
                            {
                                string inputItemName = L_toolList[i].input[j].IOName;
                                string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                if (sourceFrom == string.Empty)
                                {
                                    colorToRGBTool.toolRunStatu = (ToolRunStatu.未指定输入图像);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = colorToRGBTool.toolRunStatu.ToString();
                                        treeNode.ForeColor = Color.Red;
                                    }
                                    Win_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, colorToRGBTool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    return L_result;
                                }
                                if (inputItemName == "输入图像" || inputItemName == "InputImage")
                                {
                                    string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                    string toolItem = sourceFrom.Split('.')[1];

                                    //Hzy20220507
                                    colorToRGBTool.getOriginalJobAcquisition(sourceFrom, sourceToolName, this, out isLocal, out tool);

                                    //非队列模式
                                    HImage hImage;
                                    colorToRGBTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, true, i, this, out hImage);
                                    colorToRGBTool.inputImage = hImage;

                                    loadImage = colorToRGBTool.inputImage as HImage;
                                    colorToRGBTool.DisplayImage(i, colorToRGBTool.inputImage as HImage);

                                    if (JobIsVisable)
                                    {
                                        GetToolNodeByNodeText(inputItemName + sourceFrom).ToolTipText = FormatShowTip(colorToRGBTool.inputImage);
                                    }
                                    if (colorToRGBTool.inputImage == null)
                                    {
                                        colorToRGBTool.toolRunStatu = ToolRunStatu.未指定输入图像;
                                        if (JobIsVisable)
                                        {
                                            treeNode.ToolTipText = colorToRGBTool.toolRunStatu.ToString();
                                            treeNode.ForeColor = Color.Red;
                                        }
                                        sourceValueIsEmpty = true;
                                        break;
                                    }
                                }
                            }
                            if (sourceValueIsEmpty)
                                break;
                            colorToRGBTool.Run(false, false, L_toolList[i].toolName);
                            if (colorToRGBTool.toolRunStatu != (ToolRunStatu.成功))
                            {

                                //hzy20220509//////////////////////
                                if (!isLocal)
                                {
                                    if ((tool as AcqImageTool).QeHImages.Count > 0)
                                    {
                                        colorToRGBTool.inputImage = (tool as AcqImageTool).ReadOrWriteImage(false, false);
                                    }
                                }
                                ///////////////////////////////////

                                if (!Configuration.SpeedMode)
                                {
                                    Win_Main.Instance.OutputMsg(string.Format("流程 [{0}] 运行失败，原因：工具 [{1}] {2}", jobName, L_toolList[i].toolName, L_toolList[i].tool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = string.Format("状态：{0}\r\n耗时：{1}ms\r\n说明：{2}", colorToRGBTool.toolRunStatu.ToString(), 0, L_toolList[i].toolTipInfo);
                                    }
                                }
                                if (JobIsVisable)
                                {
                                    treeNode.ForeColor = Color.Red;
                                }
                                jobRunStatu = JobRunStatu.Fail;
                                strErrToolName = L_toolList[i].toolName;
                                break;
                            }
                            else
                            {
                                if (JobIsVisable)
                                {
                                    try
                                    {
                                        treeNode.ForeColor = Color.LightGreen;
                                    }
                                    catch { }
                                }
                            }
                            for (int j = 0; j < this.outputItemNum; j++)
                            {
                                string outputItem = L_toolList[i].output[j].IOName;
                                switch (outputItem)
                                {
                                    case "红":
                                    case "Red":
                                        L_toolList[i].GetOutput(outputItem).value = colorToRGBTool.outputRed;
                                        if (JobIsVisable)
                                        {
                                            GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, outputItem).ToolTipText = "图形变量暂不支持显示";
                                        }
                                        break; ;
                                    case "绿":
                                    case "Green":
                                        L_toolList[i].GetOutput(outputItem).value = colorToRGBTool.outputGreen;
                                        if (JobIsVisable)
                                        {
                                           GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, outputItem).ToolTipText = "图形变量暂不支持显示";
                                        }
                                        break; ;
                                    case "蓝":
                                    case "Blue":
                                        L_toolList[i].GetOutput(outputItem).value = colorToRGBTool.outputBlue;
                                        if (JobIsVisable)
                                        {
                                            GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, outputItem).ToolTipText = "图形变量暂不支持显示";                            
                                        }
                                        break; ;
                                }
                            }


                        }
                        #endregion
                        #region ShapeMatch
                        else if (L_toolList[i].toolType == ToolType.Match)
                        {
                            MatchTool shapeMatchTool = (MatchTool)L_toolList[i].tool;
                            if (!L_toolList[i].enable)
                            {
                                if (!isAcqExists)
                                {
                                    if (i == L_toolList.Count - 1)
                                    {
                                        (tool as AcqImageTool).ReadOrWriteImage(false, false);//删除图片
                                    }
                                }
                                shapeMatchTool.toolRunStatu = (ToolRunStatu.未启用);
                                if (JobIsVisable)
                                {
                                    treeNode.ToolTipText = shapeMatchTool.toolRunStatu.ToString();
                                    treeNode.ForeColor = Color.DarkGray;
                                }
                                continue;
                            }
                            shapeMatchTool.ClearLastInput();

                            //Hzy20220509//////////////////
                            bool isLocal = false;
                            ///////////////////////////////

                            for (int j = 0; j < inputItemNum; j++)
                            {
                                string inputItemName = L_toolList[i].input[j].IOName;
                                string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                if (sourceFrom == string.Empty)
                                {
                                    shapeMatchTool.toolRunStatu = (ToolRunStatu.未指定输入图像);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = shapeMatchTool.toolRunStatu.ToString();
                                        treeNode.ForeColor = Color.Red;
                                    }
                                    Win_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, shapeMatchTool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    return L_result;
                                }
                                if (inputItemName == "图像" || inputItemName == "InputImage")
                                {
                                    string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                    string toolItem = sourceFrom.Split('.')[1];

                                    //Hzy20220507
                                    shapeMatchTool.getOriginalJobAcquisition(sourceFrom, sourceToolName, this, out isLocal, out tool);

                                    //非队列模式
                                    HImage hImage;
                                    shapeMatchTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, true, i, this, out hImage);
                                    shapeMatchTool.toolPar.InputPar.InPutImage = hImage;

                                    loadImage = shapeMatchTool.toolPar.InputPar.InPutImage;
                                    shapeMatchTool.DisplayImage(i, shapeMatchTool.toolPar.InputPar.InPutImage);

                                    if (JobIsVisable)
                                    {
                                        //hzy20220516,Job切换报错
                                        string strText = "";
                                        if (shapeMatchTool.toolPar.InputPar.InPutImage != null)
                                        {
                                            strText = FormatShowTip(shapeMatchTool.toolPar.InputPar.InPutImage);
                                        }
                                        else
                                        {
                                            strText = "图形变量";
                                        }

                                        try
                                        {
                                            GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, inputItemName + sourceFrom).ToolTipText = strText;
                                        }
                                        catch { }
                                    }
                                    if (shapeMatchTool.toolPar.InputPar.InPutImage == null)
                                    {
                                        shapeMatchTool.toolRunStatu = ToolRunStatu.未指定输入图像;
                                        if (JobIsVisable)
                                        {
                                            treeNode.ToolTipText = shapeMatchTool.toolRunStatu.ToString();
                                            treeNode.ForeColor = Color.Red;
                                        }
                                        sourceValueIsEmpty = true;
                                        break;
                                    }
                                }
                            }

                            if (sourceValueIsEmpty)
                                break;

                            shapeMatchTool.Run(false, false, L_toolList[i].toolName);
                            
                            if (shapeMatchTool.toolRunStatu != (ToolRunStatu.成功))
                            {
                                //hzy20220509//////////////////////
                                if (!isLocal)
                                {
                                    if ((tool as AcqImageTool).QeHImages.Count > 0)
                                    {
                                        shapeMatchTool.toolPar.InputPar.InPutImage = (tool as AcqImageTool).ReadOrWriteImage(false, false);
                                    }
                                }
                                ///////////////////////////////////

                                if (!Configuration.SpeedMode)
                                {
                                    Win_Main.Instance.OutputMsg(string.Format("流程 [{0}] 运行失败，原因：工具 [{1}] {2}", jobName, L_toolList[i].toolName, L_toolList[i].tool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = string.Format("状态：{0}\r\n耗时：{1}ms\r\n说明：{2}", shapeMatchTool.toolRunStatu.ToString(), 0, L_toolList[i].toolTipInfo);
                                    }
                                }
                                if (JobIsVisable)
                                {
                                    treeNode.ForeColor = Color.Red;
                                }
                                jobRunStatu = JobRunStatu.Fail;
                                strErrToolName = L_toolList[i].toolName;
                                bJobProRunStatus = false;
                                break;
                            }
                            else
                            {
                                if (JobIsVisable)
                                {
                                    //hzy20220527
                                    try
                                    {
                                        treeNode.ForeColor = Color.LightGreen;
                                    }
                                    catch { }
                                    //////////////////////////////////////////////////////////////////////////////////////////////////////////
                                }
                            }

                            for (int j = 0; j < this.outputItemNum; j++)
                            {
                                try
                                {
                                    if (j==1)
                                    {

                                    }
                                    string outputItem = L_toolList[i].output[j].IOName;
                                    object value = new object();
                                    if (outputItem == "位置")
                                    {
                                        value = ((MatchTool)(L_toolList[i].tool)).toolPar.Results.Pose;
                                    }

                                    L_toolList[i].GetOutput(outputItem).value = value;
                                    if (JobIsVisable)
                                    {
                                        try
                                        { 
                                            GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, outputItem, false).ToolTipText = FormatShowTip(value);
                                        }
                                        catch { }
                                    }
                                }
                                catch (Exception ex)
                                {

                                }
                                
                            }

                        }
                        #endregion
                        #region Blob
                        else if (L_toolList[i].toolType == ToolType.BlobAnalyse)
                        {
                            BlobTool BlobTool = (BlobTool)L_toolList[i].tool;
                            if (!L_toolList[i].enable)
                            {
                                if (!isAcqExists)
                                {
                                    if (i == L_toolList.Count - 1)
                                    {
                                        (tool as AcqImageTool).ReadOrWriteImage(false, false);//删除图片
                                    }
                                }
                                BlobTool.toolRunStatu = (ToolRunStatu.未启用);
                                if (JobIsVisable)
                                {
                                    treeNode.ToolTipText = BlobTool.toolRunStatu.ToString();
                                    treeNode.ForeColor = Color.DarkGray;
                                }
                                continue;
                            }
                            // BlobTool.ClearLastInput();

                            //Hzy20220509//////////////////
                            bool isLocal = false;
                            ///////////////////////////////

                            for (int j = 0; j < inputItemNum; j++)
                            {
                                string inputItemName = L_toolList[i].input[j].IOName;
                                string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                if (sourceFrom == string.Empty)
                                {
                                    BlobTool.toolRunStatu = (ToolRunStatu.未指定输入图像);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = BlobTool.toolRunStatu.ToString();
                                        treeNode.ForeColor = Color.Red;
                                    }
                                    Win_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, BlobTool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    return L_result;
                                }
                                if (inputItemName == "图像" || inputItemName == "InputImage")
                                {
                                    string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                    string toolItem = sourceFrom.Split('.')[1];

                                    //Hzy20220509
                                    BlobTool.getOriginalJobAcquisition(sourceFrom, sourceToolName, this, out isLocal, out tool);

                                    //非队列模式
                                    HImage hImage;
                                    BlobTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, true, i, this, out hImage);
                                    BlobTool.toolPar.InputPar.图像 = hImage;
                                    BlobTool.toolPar.RunPar.图像 = BlobTool.toolPar.InputPar.图像;

                                    loadImage = BlobTool.toolPar.InputPar.图像;
                                    BlobTool.DisplayImage(i, BlobTool.toolPar.InputPar.图像);

                                    if (JobIsVisable)
                                    {
                                        try
                                        { 
                                        GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, inputItemName + sourceFrom).ToolTipText = FormatShowTip(BlobTool.toolPar.InputPar.图像);
                                        }
                                        catch { }
                                    }

                                    if (BlobTool.toolPar.InputPar.图像 == null)
                                    {
                                        BlobTool.toolRunStatu = ToolRunStatu.未指定输入图像;
                                        if (JobIsVisable)
                                        {
                                            treeNode.ToolTipText = BlobTool.toolRunStatu.ToString();
                                            treeNode.ForeColor = Color.Red;
                                        }
                                        sourceValueIsEmpty = true;
                                        break;
                                    }
                                }
                                else if (inputItemName == "跟随位置")
                                {
                                    string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                    string toolItem = sourceFrom.Split('.')[1];
                                    BlobTool.toolPar.InputPar.Pose = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as List<XYU>;
                                    if (JobIsVisable)
                                    {
                                        try
                                        { 
                                        GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, inputItemName + sourceFrom).ToolTipText = FormatShowTip(BlobTool.toolPar.InputPar.Pose);
                                        }
                                        catch { }
                                    }
                                }
                            }
                            if (sourceValueIsEmpty)
                                break;

                            BlobTool.Run(false, false, L_toolList[i].toolName);

                            if (BlobTool.toolRunStatu != (ToolRunStatu.成功))
                            {
                                //Hzy20220509
                                if (!isLocal)
                                {
                                    if ((tool as AcqImageTool).QeHImages.Count > 0)
                                    {
                                        BlobTool.toolPar.InputPar.图像 = (tool as AcqImageTool).ReadOrWriteImage(false, false);
                                        BlobTool.toolPar.RunPar.图像 = BlobTool.toolPar.InputPar.图像;
                                    }
                                }

                                if (!Configuration.SpeedMode)
                                {
                                    Win_Main.Instance.OutputMsg(string.Format("流程 [{0}] 运行失败，原因：工具 [{1}] {2}", jobName, L_toolList[i].toolName, L_toolList[i].tool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = string.Format("状态：{0}\r\n耗时：{1}ms\r\n说明：{2}", BlobTool.toolRunStatu.ToString(), 0, L_toolList[i].toolTipInfo);
                                    }
                                }
                                if (JobIsVisable)
                                {
                                    treeNode.ForeColor = Color.Red;
                                }
                                jobRunStatu = JobRunStatu.Fail;
                                strErrToolName = L_toolList[i].toolName;
                                //20220501，斑点工具失败，跳出循环
                                break;
                            }
                            else
                            {
                                if (JobIsVisable)
                                {
                                    try
                                    {
                                        treeNode.ForeColor = Color.LightGreen;
                                    }
                                    catch { }
                                }
                            }


                            for (int j = 0; j < this.outputItemNum; j++)
                            {
                                string outputItem = L_toolList[i].output[j].IOName;
                                object value = null;
                                if (L_toolList[i].output[j].ioType == DataType.Region)
                                {
                                    L_toolList[i].GetOutput(outputItem).value = ((BlobTool)(L_toolList[i].tool)).toolPar.ResultsPar.resultRegion;
                                    value = ((BlobTool)(L_toolList[i].tool)).toolPar.ResultsPar.resultRegion;
                                }
                                else if (L_toolList[i].output[j].ioType == DataType.Pose)
                                {
                                    L_toolList[i].GetOutput(outputItem).value = ((BlobTool)(L_toolList[i].tool)).toolPar.ResultsPar.位置;
                                    value = ((BlobTool)(L_toolList[i].tool)).toolPar.ResultsPar.位置;
                                }
                                else
                                {
                                    L_toolList[i].GetOutput(outputItem).value = ((BlobTool)(L_toolList[i].tool)).toolPar.ResultsPar.L_resultBlob;
                                    value = ((BlobTool)(L_toolList[i].tool)).toolPar.ResultsPar.L_resultBlob;
                                }

                                if (JobIsVisable)
                                {
                                    try
                                    {
                                        GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, outputItem, false).ToolTipText = FormatShowTip(value);
                                    }
                                    catch { }
                                }
                            }
                        }
                        #endregion
                        #region FindLine
                        else if (L_toolList[i].toolType == ToolType.FindLine)
                        {
                            FindLineTool FindLineTool = (FindLineTool)L_toolList[i].tool;
                            if (!L_toolList[i].enable)
                            {
                                if (!isAcqExists)
                                {
                                    if (i == L_toolList.Count - 1)
                                    {
                                        (tool as AcqImageTool).ReadOrWriteImage(false, false);//删除图片
                                    }
                                }
                                FindLineTool.toolRunStatu = (ToolRunStatu.未启用);
                                if (JobIsVisable)
                                {
                                    treeNode.ToolTipText = FindLineTool.toolRunStatu.ToString();
                                    treeNode.ForeColor = Color.DarkGray;
                                }
                                continue;
                            }

                            //Hzy20220509//////////////////
                            bool isLocal = false;
                            ///////////////////////////////

                            for (int j = 0; j < inputItemNum; j++)
                            {
                                string inputItemName = L_toolList[i].input[j].IOName;
                                string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                if (sourceFrom == string.Empty && inputItemName == "图像")
                                {
                                    FindLineTool.toolRunStatu = (ToolRunStatu.未指定输入图像);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = FindLineTool.toolRunStatu.ToString();
                                        treeNode.ForeColor = Color.Red;
                                    }
                                    Win_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, FindLineTool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    return L_result;
                                }

                                string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                string toolItem = sourceFrom.Split('.')[1];
                                if (inputItemName == "图像" || inputItemName == "InputImage")
                                {
                                    //Hzy20220507
                                    FindLineTool.getOriginalJobAcquisition(sourceFrom, sourceToolName, this, out isLocal, out tool);

                                    //非队列模式
                                    HImage hImage;
                                    FindLineTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, true, i, this, out hImage);
                                    FindLineTool.toolPar.InputPar.InPuImage = hImage;

                                    loadImage = FindLineTool.toolPar.InputPar.InPuImage;
                                    FindLineTool.DisplayImage(i, FindLineTool.toolPar.InputPar.InPuImage);

                                    if (JobIsVisable)
                                    {
                                        GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, inputItemName + sourceFrom).ToolTipText = FormatShowTip(FindLineTool.toolPar.InputPar.InPuImage);
                                       
                                    }
                                    if (FindLineTool.toolPar.InputPar.InPuImage == null)
                                    {
                                        FindLineTool.toolRunStatu = ToolRunStatu.未指定输入图像;
                                        if (JobIsVisable)
                                        {
                                            treeNode.ToolTipText = FindLineTool.toolRunStatu.ToString();
                                            treeNode.ForeColor = Color.Red;
                                        }
                                        sourceValueIsEmpty = true;
                                        break;
                                    }
                                }
                                else if (inputItemName == "跟随位置")
                                {
                                    FindLineTool.toolPar.InputPar.Pose = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as List<XYU>;
                                    if (JobIsVisable)
                                    {
                                        GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, inputItemName + sourceFrom).ToolTipText = FormatShowTip(FindLineTool.toolPar.InputPar.Pose);
                                      
                                    }
                                }
                            }
                            if (sourceValueIsEmpty)
                                break;

                            FindLineTool.Run(false, false, L_toolList[i].toolName);


                            if (FindLineTool.toolRunStatu != (ToolRunStatu.成功))
                            {
                                //Hzy20220509
                                if (!isLocal)
                                {
                                    if ((tool as AcqImageTool).QeHImages.Count > 0)
                                    {
                                        FindLineTool.toolPar.InputPar.InPuImage = (tool as AcqImageTool).ReadOrWriteImage(false, false);
                                    }
                                }

                                if (!Configuration.SpeedMode)
                                {
                                    Win_Main.Instance.OutputMsg(string.Format("流程 [{0}] 运行失败，原因：工具 [{1}] {2}", jobName, L_toolList[i].toolName, L_toolList[i].tool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = string.Format("状态：{0}\r\n耗时：{1}ms\r\n说明：{2}", FindLineTool.toolRunStatu.ToString(), 0, L_toolList[i].toolTipInfo);
                                    }
                                }
                                if (JobIsVisable)
                                {
                                    treeNode.ForeColor = Color.Red;
                                }
                                jobRunStatu = JobRunStatu.Fail;
                                strErrToolName = L_toolList[i].toolName;
                                //20220501，工具失败，跳出循环
                                break;
                            }
                            else
                            {
                                if (JobIsVisable)
                                {
                                    try
                                    {
                                        treeNode.ForeColor = Color.LightGreen;
                                    }
                                    catch { }
                                }
                            }

                            for (int j = 0; j < this.outputItemNum; j++)
                            {
                                string outputItem = L_toolList[i].output[j].IOName;
                                object value = null;
                                if (L_toolList[i].output[j].ioType == DataType.Line)
                                {
                                    L_toolList[i].GetOutput(outputItem).value = ((FindLineTool)(L_toolList[i].tool)).toolPar.Results.Line;
                                    value = ((FindLineTool)(L_toolList[i].tool)).toolPar.Results.Line;
                                }

                                if (JobIsVisable)
                                {
                                    try
                                    {
                                        GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, outputItem, false).ToolTipText = FormatShowTip(value);
                                    }
                                    catch { }
                                }
                            }
                        }
                        #endregion
                        #region FindCircle
                        else if (L_toolList[i].toolType == ToolType.FindCircle)
                        {
                            FindCircleTool FindCircleTool = (FindCircleTool)L_toolList[i].tool;
                            if (!L_toolList[i].enable)
                            {
                                if (!isAcqExists)
                                {
                                    if (i == L_toolList.Count - 1)
                                    {
                                        (tool as AcqImageTool).ReadOrWriteImage(false, false);//删除图片
                                    }
                                }
                                FindCircleTool.toolRunStatu = (ToolRunStatu.未启用);
                                if (JobIsVisable)
                                {
                                    treeNode.ToolTipText = FindCircleTool.toolRunStatu.ToString();
                                    treeNode.ForeColor = Color.DarkGray;
                                }
                                continue;
                            }

                            //Hzy20220509//////////////////
                            bool isLocal = false;
                            ///////////////////////////////

                            for (int j = 0; j < inputItemNum; j++)
                            {
                                string inputItemName = L_toolList[i].input[j].IOName;
                                string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                if (sourceFrom == string.Empty && inputItemName == "图像")
                                {
                                    FindCircleTool.toolRunStatu = (ToolRunStatu.未指定输入图像);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = FindCircleTool.toolRunStatu.ToString();
                                        treeNode.ForeColor = Color.Red;
                                    }
                                    Win_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, FindCircleTool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    return L_result;
                                }

                                string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                string toolItem = sourceFrom.Split('.')[1];
                                if (inputItemName == "图像" || inputItemName == "InputImage")
                                {
                                    //Hzy20220507
                                    FindCircleTool.getOriginalJobAcquisition(sourceFrom, sourceToolName, this, out isLocal, out tool);

                                    //非队列模式
                                    HImage hImage;
                                    FindCircleTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, true, i, this, out hImage);
                                    FindCircleTool.toolPar.InputPar.InPuImage = hImage;

                                    loadImage = FindCircleTool.toolPar.InputPar.InPuImage;
                                    FindCircleTool.DisplayImage(i, FindCircleTool.toolPar.InputPar.InPuImage);

                                    if (JobIsVisable)
                                    {
                                        try 
                                        { 
                                            GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, inputItemName + sourceFrom).ToolTipText = FormatShowTip(FindCircleTool.toolPar.InputPar.InPuImage);
                                        }
                                        catch { }
                                    }
                                    if (FindCircleTool.toolPar.InputPar.InPuImage == null)
                                    {
                                        FindCircleTool.toolRunStatu = ToolRunStatu.未指定输入图像;
                                        if (JobIsVisable)
                                        {
                                            treeNode.ToolTipText = FindCircleTool.toolRunStatu.ToString();
                                            treeNode.ForeColor = Color.Red;
                                        }
                                        sourceValueIsEmpty = true;
                                        break;
                                    }
                                }
                                else if (inputItemName == "跟随位置")
                                {
                                    FindCircleTool.toolPar.InputPar.Pose = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as List<XYU>;
                                    if (JobIsVisable)
                                    {
                                        GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, inputItemName + sourceFrom).ToolTipText = FormatShowTip(FindCircleTool.toolPar.InputPar.Pose);
                                      
                                    }
                                }
                            }
                            if (sourceValueIsEmpty)
                                break;

                            FindCircleTool.Run(false, false, L_toolList[i].toolName);

                            if (FindCircleTool.toolRunStatu != (ToolRunStatu.成功))
                            {
                                //20220509
                                if (!isLocal)
                                {
                                    if ((tool as AcqImageTool).QeHImages.Count > 0)
                                    {
                                        FindCircleTool.toolPar.InputPar.InPuImage = (tool as AcqImageTool).ReadOrWriteImage(false, false);
                                    }
                                }

                                if (!Configuration.SpeedMode)
                                {
                                    Win_Main.Instance.OutputMsg(string.Format("流程 [{0}] 运行失败，原因：工具 [{1}] {2}", jobName, L_toolList[i].toolName, L_toolList[i].tool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = string.Format("状态：{0}\r\n耗时：{1}ms\r\n说明：{2}", FindCircleTool.toolRunStatu.ToString(), 0, L_toolList[i].toolTipInfo);
                                    }
                                }
                                if (JobIsVisable)
                                {
                                    treeNode.ForeColor = Color.Red;
                                }
                                jobRunStatu = JobRunStatu.Fail;
                                strErrToolName = L_toolList[i].toolName;
                                //20220501，工具失败，跳出循环
                                break;
                            }
                            else
                            {
                                if (JobIsVisable)
                                {
                                    try
                                    {
                                        treeNode.ForeColor = Color.LightGreen;
                                    }
                                    catch { }
                                }
                            }


                            for (int j = 0; j < this.outputItemNum; j++)
                            {
                                string outputItem = L_toolList[i].output[j].IOName;
                                object value = null;
                                if (L_toolList[i].output[j].ioType == DataType.Circle)
                                {
                                    L_toolList[i].GetOutput(outputItem).value = ((FindCircleTool)(L_toolList[i].tool)).toolPar.Results.Circle;
                                    value = ((FindCircleTool)(L_toolList[i].tool)).toolPar.Results.Circle;
                                }
                                else if (outputItem == "圆心")
                                {
                                    L_toolList[i].GetOutput(outputItem).value = ((FindCircleTool)(L_toolList[i].tool)).toolPar.Results.Circle.圆心;
                                    value = ((FindCircleTool)(L_toolList[i].tool)).toolPar.Results.Circle.圆心;
                                }
                                else if (outputItem == "半径")
                                {
                                    L_toolList[i].GetOutput(outputItem).value = ((FindCircleTool)(L_toolList[i].tool)).toolPar.Results.Circle.半径;
                                    value = ((FindCircleTool)(L_toolList[i].tool)).toolPar.Results.Circle.半径;
                                }
                                if (JobIsVisable)
                                {
                                    GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, outputItem, false).ToolTipText = FormatShowTip(value);
               
                                }
                            }
                        }
                        #endregion
                        #region LLIntersection
                        else if (L_toolList[i].toolType == ToolType.LLIntersect)
                        {
                            LLIntersectTool LLIntersectTool = (LLIntersectTool)L_toolList[i].tool;
                            if (!L_toolList[i].enable)
                            {
                                if (!isAcqExists)
                                {
                                    if (i == L_toolList.Count - 1)
                                    {
                                        (tool as AcqImageTool).ReadOrWriteImage(false, false);//删除图片
                                    }
                                }
                                LLIntersectTool.toolRunStatu = (ToolRunStatu.未启用);
                                if (JobIsVisable)
                                {
                                    treeNode.ToolTipText = LLIntersectTool.toolRunStatu.ToString();
                                    treeNode.ForeColor = Color.DarkGray;
                                }
                                continue;
                            }

                            //Hzy20220509//////////////////
                            bool isLocal = false;
                            ///////////////////////////////

                            for (int j = 0; j < inputItemNum; j++)
                            {
                                string inputItemName = L_toolList[i].input[j].IOName;
                                string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                if (sourceFrom == string.Empty)
                                {
                                    LLIntersectTool.toolRunStatu = (ToolRunStatu.未指定输入图像);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = LLIntersectTool.toolRunStatu.ToString();
                                        treeNode.ForeColor = Color.Red;
                                    }
                                    Win_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, LLIntersectTool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    return L_result;
                                }

                                string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                string toolItem = sourceFrom.Split('.')[1];
                                if (inputItemName == "图像" || inputItemName == "InputImage")
                                {
                                    //Hzy20220507
                                    LLIntersectTool.getOriginalJobAcquisition(sourceFrom, sourceToolName, this, out isLocal, out tool);

                                    //非队列模式
                                    HImage hImage;
                                    LLIntersectTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, true, i, this, out hImage);
                                    LLIntersectTool.toolPar.InputPar.图像 = hImage;

                                    loadImage = LLIntersectTool.toolPar.InputPar.图像;
                                    LLIntersectTool.DisplayImage(i, LLIntersectTool.toolPar.InputPar.图像);

                                    if (JobIsVisable)
                                    {
                                        GetToolNodeByNodeText(inputItemName + sourceFrom).ToolTipText = FormatShowTip(LLIntersectTool.toolPar.InputPar.图像);
                                        if (LLIntersectTool.toolPar.InputPar.图像 == null)
                                        {
                                            LLIntersectTool.toolRunStatu = ToolRunStatu.未指定输入图像;
                                            treeNode.ToolTipText = LLIntersectTool.toolRunStatu.ToString();
                                            treeNode.ForeColor = Color.Red;
                                            sourceValueIsEmpty = true;
                                            break;
                                        }
                                    }
                                }
                                else if (inputItemName == "Line1" && sourceFrom != "")
                                {
                                    LLIntersectTool.toolPar.InputPar.Line1 = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as Line;
                                }
                                else if (inputItemName == "Line2" && sourceFrom != "")
                                {
                                    LLIntersectTool.toolPar.InputPar.Line2 = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as Line;
                                }
                            }

                            LLIntersectTool.Run(true, false, L_toolList[i].toolName);

                            if (LLIntersectTool.toolRunStatu != (ToolRunStatu.成功))
                            {
                                //20220509
                                if (!isLocal)
                                {
                                    if ((tool as AcqImageTool).QeHImages.Count > 0)
                                    {
                                        LLIntersectTool.toolPar.InputPar.图像 = (tool as AcqImageTool).ReadOrWriteImage(false, false);
                                    }
                                }

                                if (!Configuration.SpeedMode)
                                {
                                    Win_Main.Instance.OutputMsg(string.Format("流程 [{0}] 运行失败，原因：工具 [{1}] {2}", jobName, L_toolList[i].toolName, L_toolList[i].tool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = string.Format("状态：{0}\r\n耗时：{1}ms\r\n说明：{2}", LLIntersectTool.toolRunStatu.ToString(), 0, L_toolList[i].toolTipInfo);
                                    }
                                }
                                if (JobIsVisable)
                                {
                                    treeNode.ForeColor = Color.Red;
                                }
                                jobRunStatu = JobRunStatu.Fail;
                                strErrToolName = L_toolList[i].toolName;
                                break;
                            }
                            else
                            {
                                if (JobIsVisable)
                                {
                                    try
                                    {
                                        treeNode.ForeColor = Color.LightGreen;
                                    }
                                    catch { }
                                }
                            }
                            for (int j = 0; j < this.outputItemNum; j++)
                            {
                                string outputItem = L_toolList[i].output[j].IOName;
                                object value = null;
                                if (outputItem == "交点")
                                {
                                    value = ((LLIntersectTool)(L_toolList[i].tool)).toolPar.ResultPar.IntersectionP;
                                }
                                else if (outputItem == "是否相交")
                                {
                                    value = ((LLIntersectTool)(L_toolList[i].tool)).toolPar.ResultPar.IsFind;
                                }
                             
                                L_toolList[i].GetOutput(outputItem).value = value;
                                if (JobIsVisable)
                                {
                                    GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, outputItem, false).ToolTipText = FormatShowTip(value);
                        
                                }
                            }
                        }
                        #endregion
                        #region LLAngle
                        else if (L_toolList[i].toolType == ToolType.LLAngle)
                        {
                            LLAngleTool LLAngleTool = (LLAngleTool)L_toolList[i].tool;
                            if (!L_toolList[i].enable)
                            {
                                if (!isAcqExists)
                                {
                                    if (i == L_toolList.Count - 1)
                                    {
                                        (tool as AcqImageTool).ReadOrWriteImage(false, false);//删除图片
                                    }
                                }
                                LLAngleTool.toolRunStatu = (ToolRunStatu.未启用);
                                if (JobIsVisable)
                                {
                                    treeNode.ToolTipText = LLAngleTool.toolRunStatu.ToString();
                                    treeNode.ForeColor = Color.DarkGray;
                                }
                                continue;
                            }

                            //Hzy20220509//////////////////
                            bool isLocal = false;

                            for (int j = 0; j < inputItemNum; j++)
                            {
                                string inputItemName = L_toolList[i].input[j].IOName;
                                string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                if (sourceFrom == string.Empty)
                                {
                                    LLAngleTool.toolRunStatu = (ToolRunStatu.未指定输入图像);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = LLAngleTool.toolRunStatu.ToString();
                                        treeNode.ForeColor = Color.Red;
                                    }
                                    Win_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, LLAngleTool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    return L_result;
                                }

                                string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                string toolItem = sourceFrom.Split('.')[1];
                                if (inputItemName == "图像" || inputItemName == "InputImage")
                                {
                                    //Hzy20220507
                                    LLAngleTool.getOriginalJobAcquisition(sourceFrom, sourceToolName, this, out isLocal, out tool);

                                    //非队列模式
                                    HImage hImage;
                                    LLAngleTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, true, i, this, out hImage);
                                    LLAngleTool.toolPar.InputPar.图像 = hImage;

                                    loadImage = LLAngleTool.toolPar.InputPar.图像;
                                    LLAngleTool.DisplayImage(i, LLAngleTool.toolPar.InputPar.图像);

                                    if (JobIsVisable)
                                    {
                                        GetToolNodeByNodeText(inputItemName + sourceFrom).ToolTipText = FormatShowTip(LLAngleTool.toolPar.InputPar.图像);
                                        if (LLAngleTool.toolPar.InputPar.图像 == null)
                                        {
                                            LLAngleTool.toolRunStatu = ToolRunStatu.未指定输入图像;
                                            treeNode.ToolTipText = LLAngleTool.toolRunStatu.ToString();
                                            treeNode.ForeColor = Color.Red;
                                            sourceValueIsEmpty = true;
                                            break;
                                        }
                                    }
                                }
                                else if (inputItemName == "Line1" && sourceFrom != "")
                                {
                                    LLAngleTool.toolPar.InputPar.Line1 = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as Line;
                                }
                                else if (inputItemName == "Line2" && sourceFrom != "")
                                {
                                    LLAngleTool.toolPar.InputPar.Line2 = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as Line;
                                }
                            }

                            LLAngleTool.Run(true, false, L_toolList[i].toolName);

                            if (LLAngleTool.toolRunStatu != (ToolRunStatu.成功))
                            {
                                //20220509
                                if (!isLocal)
                                {
                                    if ((tool as AcqImageTool).QeHImages.Count > 0)
                                    {
                                        LLAngleTool.toolPar.InputPar.图像 = (tool as AcqImageTool).ReadOrWriteImage(false, false);
                                    }
                                }

                                if (!Configuration.SpeedMode)
                                {
                                    Win_Main.Instance.OutputMsg(string.Format("流程 [{0}] 运行失败，原因：工具 [{1}] {2}", jobName, L_toolList[i].toolName, L_toolList[i].tool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = string.Format("状态：{0}\r\n耗时：{1}ms\r\n说明：{2}", LLAngleTool.toolRunStatu.ToString(), 0, L_toolList[i].toolTipInfo);
                                    }
                                }
                                if (JobIsVisable)
                                {
                                    treeNode.ForeColor = Color.Red;
                                }
                                jobRunStatu = JobRunStatu.Fail;
                                strErrToolName = L_toolList[i].toolName;
                                break;
                            }
                            else
                            {
                                if (JobIsVisable)
                                {
                                    try
                                    {
                                        treeNode.ForeColor = Color.LightGreen;
                                    }
                                    catch { }
                                }

                            }
                            for (int j = 0; j < this.outputItemNum; j++)
                            {
                                string outputItem = L_toolList[i].output[j].IOName;
                                object value = null;
                                if (outputItem == "角度")
                                {
                                    value = ((LLAngleTool)(L_toolList[i].tool)).toolPar.ResultPar.Angle;
                                }
                                else if (outputItem == "判定结果")
                                {
                                    value = ((LLAngleTool)(L_toolList[i].tool)).toolPar.ResultPar.Consequence;
                                }
                                L_toolList[i].GetOutput(outputItem).value = value;
                                if (JobIsVisable)
                                {
                                    GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, outputItem, false).ToolTipText = FormatShowTip(value);
                                
                                }
                            }
                        }
                        #endregion
                        #region PPDistance
                        else if (L_toolList[i].toolType == ToolType.P2PDistance)
                        {
                            PPDistanceTool PPDistanceTool = (PPDistanceTool)L_toolList[i].tool;
                            if (!L_toolList[i].enable)
                            {
                                if (!isAcqExists)
                                {
                                    if (i == L_toolList.Count - 1)
                                    {
                                        (tool as AcqImageTool).ReadOrWriteImage(false, false);//删除图片
                                    }
                                }
                                PPDistanceTool.toolRunStatu = (ToolRunStatu.未启用);
                                if (JobIsVisable)
                                {
                                    treeNode.ToolTipText = PPDistanceTool.toolRunStatu.ToString();
                                    treeNode.ForeColor = Color.DarkGray;
                                }
                                continue;
                            }

                            //Hzy20220509//////////////////
                            bool isLocal = false;
                            ///////////////////////////////

                            for (int j = 0; j < inputItemNum; j++)
                            {
                                string inputItemName = L_toolList[i].input[j].IOName;
                                string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                if (sourceFrom == string.Empty)
                                {
                                    PPDistanceTool.toolRunStatu = (ToolRunStatu.未指定输入图像);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = PPDistanceTool.toolRunStatu.ToString();
                                        treeNode.ForeColor = Color.Red;
                                    }
                                    Win_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, PPDistanceTool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    return L_result;
                                }

                                string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                string toolItem = sourceFrom.Split('.')[1];

                                if (inputItemName == "图像" || inputItemName == "InputImage")
                                {
                                    //Hzy20220507
                                    PPDistanceTool.getOriginalJobAcquisition(sourceFrom, sourceToolName, this, out isLocal, out tool);

                                    //非队列模式
                                    HImage hImage;
                                    PPDistanceTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, true, i, this, out hImage);
                                    PPDistanceTool.toolPar.InputPar.图像 = hImage;

                                    loadImage = PPDistanceTool.toolPar.InputPar.图像;
                                    PPDistanceTool.DisplayImage(i, PPDistanceTool.toolPar.InputPar.图像);

                                    if (JobIsVisable)
                                    {
                                        GetToolNodeByNodeText(inputItemName + sourceFrom).ToolTipText = FormatShowTip(PPDistanceTool.toolPar.InputPar.图像);
                                        if (PPDistanceTool.toolPar.InputPar.图像 == null)
                                        {
                                            PPDistanceTool.toolRunStatu = ToolRunStatu.未指定输入图像;
                                            treeNode.ToolTipText = PPDistanceTool.toolRunStatu.ToString();
                                            treeNode.ForeColor = Color.Red;
                                            sourceValueIsEmpty = true;
                                            break;
                                        }
                                    }
                                }
                                else if (inputItemName == "Point1" && sourceFrom != "")
                                {
                                    PPDistanceTool.toolPar.InputPar.Point1 = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as XY;
                                }
                                else if (inputItemName == "Point2" && sourceFrom != "")
                                {
                                    PPDistanceTool.toolPar.InputPar.Point2 = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as XY;
                                }
                            }

                            PPDistanceTool.Run(true, false, L_toolList[i].toolName);

                            if (PPDistanceTool.toolRunStatu != (ToolRunStatu.成功))
                            {
                                if (!isLocal)
                                {
                                    if ((tool as AcqImageTool).QeHImages.Count > 0)
                                    {
                                        PPDistanceTool.toolPar.InputPar.图像 = (tool as AcqImageTool).ReadOrWriteImage(false, false);
                                    }
                                }

                                if (!Configuration.SpeedMode)
                                {
                                    Win_Main.Instance.OutputMsg(string.Format("流程 [{0}] 运行失败，原因：工具 [{1}] {2}", jobName, L_toolList[i].toolName, L_toolList[i].tool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = string.Format("状态：{0}\r\n耗时：{1}ms\r\n说明：{2}", PPDistanceTool.toolRunStatu.ToString(), 0, L_toolList[i].toolTipInfo);
                                    }
                                }
                                if (JobIsVisable)
                                {
                                    treeNode.ForeColor = Color.Red;
                                }
                                jobRunStatu = JobRunStatu.Fail;
                                strErrToolName = L_toolList[i].toolName;
                                break;
                            }
                            else
                            {
                                if (JobIsVisable)
                                {
                                    try
                                    {
                                        treeNode.ForeColor = Color.LightGreen;
                                    }
                                    catch { }
                                }

                            }
                            for (int j = 0; j < this.outputItemNum; j++)
                            {
                                string outputItem = L_toolList[i].output[j].IOName;
                                object value = null;
                                if (outputItem == "距离")
                                {
                                    value = ((PPDistanceTool)(L_toolList[i].tool)).toolPar.ResultPar.Distance;
                                }
                                else if (outputItem == "判定结果")
                                {
                                    value = ((PPDistanceTool)(L_toolList[i].tool)).toolPar.ResultPar.Consequence;
                                }

                                L_toolList[i].GetOutput(outputItem).value = value;
                                if (JobIsVisable)
                                {
                                    try
                                    {
                                        GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, outputItem, false).ToolTipText = FormatShowTip(value);
                                    }
                                    catch { }
                                }
                            }


                        }
                        #endregion
                        #region PLDistance
                        else if (L_toolList[i].toolType == ToolType.P2LDistance)
                        {
                            PLDistanceTool PLDistanceTool = (PLDistanceTool)L_toolList[i].tool;
                            if (!L_toolList[i].enable)
                            {
                                if (!isAcqExists)
                                {
                                    if (i == L_toolList.Count - 1)
                                    {
                                        (tool as AcqImageTool).ReadOrWriteImage(false, false);//删除图片
                                    }
                                }
                                PLDistanceTool.toolRunStatu = (ToolRunStatu.未启用);
                                if (JobIsVisable)
                                {
                                    treeNode.ToolTipText = PLDistanceTool.toolRunStatu.ToString();
                                    treeNode.ForeColor = Color.DarkGray;
                                }
                                continue;
                            }

                            //Hzy20220509//////////////////
                            bool isLocal = false;

                            for (int j = 0; j < inputItemNum; j++)
                            {
                                string inputItemName = L_toolList[i].input[j].IOName;
                                string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                if (sourceFrom == string.Empty)
                                {
                                    PLDistanceTool.toolRunStatu = (ToolRunStatu.未指定输入图像);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = PLDistanceTool.toolRunStatu.ToString();
                                        treeNode.ForeColor = Color.Red;
                                    }
                                    Win_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, PLDistanceTool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    return L_result;
                                }

                                string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                string toolItem = sourceFrom.Split('.')[1];
                                if (inputItemName == "图像" || inputItemName == "InputImage")
                                {
                                    //Hzy20220507
                                    PLDistanceTool.getOriginalJobAcquisition(sourceFrom, sourceToolName, this, out isLocal, out tool);

                                    //非队列模式
                                    HImage hImage;
                                    PLDistanceTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, true, i, this, out hImage);
                                    PLDistanceTool.toolPar.InputPar.图像 = hImage;

                                    loadImage = PLDistanceTool.toolPar.InputPar.图像;
                                    PLDistanceTool.DisplayImage(i, PLDistanceTool.toolPar.InputPar.图像);

                                    if (JobIsVisable)
                                    {
                                        GetToolNodeByNodeText(inputItemName + sourceFrom).ToolTipText = FormatShowTip(PLDistanceTool.toolPar.InputPar.图像);
                                        if (PLDistanceTool.toolPar.InputPar.图像 == null)
                                        {
                                            PLDistanceTool.toolRunStatu = ToolRunStatu.未指定输入图像;
                                            treeNode.ToolTipText = PLDistanceTool.toolRunStatu.ToString();
                                            treeNode.ForeColor = Color.Red;
                                            sourceValueIsEmpty = true;
                                            break;
                                        }
                                    }
                                }
                                else if (inputItemName == "Point1" && sourceFrom != "")
                                {
                                    PLDistanceTool.toolPar.InputPar.Point1 = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as XY;
                                }
                                else if (inputItemName == "Line1" && sourceFrom != "")
                                {
                                    PLDistanceTool.toolPar.InputPar.Line1 = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as Line;
                                }
                            }

                            PLDistanceTool.Run(true, false, L_toolList[i].toolName);

                            if (PLDistanceTool.toolRunStatu != (ToolRunStatu.成功))
                            {
                                //Hzy20220509
                                if (!isLocal)
                                {
                                    if ((tool as AcqImageTool).QeHImages.Count > 0)
                                    {
                                        PLDistanceTool.toolPar.InputPar.图像 = (tool as AcqImageTool).ReadOrWriteImage(false, false);
                                    }
                                }

                                if (!Configuration.SpeedMode)
                                {
                                    Win_Main.Instance.OutputMsg(string.Format("流程 [{0}] 运行失败，原因：工具 [{1}] {2}", jobName, L_toolList[i].toolName, L_toolList[i].tool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = string.Format("状态：{0}\r\n耗时：{1}ms\r\n说明：{2}", PLDistanceTool.toolRunStatu.ToString(), 0, L_toolList[i].toolTipInfo);
                                    }
                                }
                                if (JobIsVisable)
                                {
                                    treeNode.ForeColor = Color.Red;
                                }
                                jobRunStatu = JobRunStatu.Fail;
                                strErrToolName = L_toolList[i].toolName;
                                break;
                            }
                            else
                            {
                                if (JobIsVisable)
                                {
                                    try
                                    {
                                        treeNode.ForeColor = Color.LightGreen;
                                    }
                                    catch { }
                                }

                            }
                            for (int j = 0; j < this.outputItemNum; j++)
                            {
                                string outputItem = L_toolList[i].output[j].IOName;
                                object value = null;
                                if (outputItem == "距离")
                                {
                                    value = ((PLDistanceTool)(L_toolList[i].tool)).toolPar.ResultPar.Distance;

                                }
                                else if (outputItem == "判定结果")
                                {
                                    value = ((PLDistanceTool)(L_toolList[i].tool)).toolPar.ResultPar.Consequence;
                                }

                                L_toolList[i].GetOutput(outputItem).value = value;
                                if (JobIsVisable)
                                {
                                    try
                                    {
                                        GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, outputItem, false).ToolTipText = FormatShowTip(value);
                                    }
                                    catch { }
                                }
                            }


                        }
                        #endregion
                        #region RRegionGraySubtract
                        else if (L_toolList[i].toolType == ToolType.RRegionGraySubtract)
                        {
                            RRegionGraySubtractTool RRegionGraySubtractTool = (RRegionGraySubtractTool)L_toolList[i].tool;
                            if (!L_toolList[i].enable)
                            {
                                if (!isAcqExists)
                                {
                                    if (i == L_toolList.Count - 1)
                                    {
                                        (tool as AcqImageTool).ReadOrWriteImage(false, false);//删除图片
                                    }
                                }
                                RRegionGraySubtractTool.toolRunStatu = (ToolRunStatu.未启用);
                                if (JobIsVisable)
                                {
                                    treeNode.ToolTipText = RRegionGraySubtractTool.toolRunStatu.ToString();
                                    treeNode.ForeColor = Color.DarkGray;
                                }
                                continue;
                            }

                            //Hzy20220509//////////////////
                            bool isLocal = false;

                            for (int j = 0; j < inputItemNum; j++)
                            {
                                string inputItemName = L_toolList[i].input[j].IOName;
                                string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                if (sourceFrom == string.Empty)
                                {
                                    RRegionGraySubtractTool.toolRunStatu = (ToolRunStatu.未指定输入图像);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = RRegionGraySubtractTool.toolRunStatu.ToString();
                                        treeNode.ForeColor = Color.Red;
                                    }
                                    Win_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, RRegionGraySubtractTool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    return L_result;
                                }

                                string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                string toolItem = sourceFrom.Split('.')[1];
                                if (inputItemName == "图像" || inputItemName == "InputImage")
                                {
                                    //Hzy20220507
                                    RRegionGraySubtractTool.getOriginalJobAcquisition(sourceFrom, sourceToolName, this, out isLocal, out tool);

                                    //非队列模式
                                    HImage hImage;
                                    RRegionGraySubtractTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, true, i, this, out hImage);
                                    RRegionGraySubtractTool.toolPar.InputPar.图像 = hImage;
                                    RRegionGraySubtractTool.toolPar.RunPar.图像 = RRegionGraySubtractTool.toolPar.InputPar.图像;

                                    loadImage = RRegionGraySubtractTool.toolPar.InputPar.图像;
                                    RRegionGraySubtractTool.DisplayImage(i, RRegionGraySubtractTool.toolPar.InputPar.图像);

                                    if (JobIsVisable)
                                    {
                                        GetToolNodeByNodeText(inputItemName + sourceFrom).ToolTipText = FormatShowTip(RRegionGraySubtractTool.toolPar.InputPar.图像);
                                        if (RRegionGraySubtractTool.toolPar.InputPar.图像 == null)
                                        {
                                            RRegionGraySubtractTool.toolRunStatu = ToolRunStatu.未指定输入图像;
                                            treeNode.ToolTipText = RRegionGraySubtractTool.toolRunStatu.ToString();
                                            treeNode.ForeColor = Color.Red;
                                            sourceValueIsEmpty = true;
                                            break;
                                        }
                                    }
                                }
                                else if (inputItemName == "跟随位置" && sourceFrom != "")
                                {
                                    RRegionGraySubtractTool.toolPar.InputPar.Pose = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as List<XYU>;
                                }
                            }

                            RRegionGraySubtractTool.Run(true, false, L_toolList[i].toolName);

                            if (RRegionGraySubtractTool.toolRunStatu != (ToolRunStatu.成功))
                            {
                                //20220509
                                if (!isLocal)
                                {
                                    if ((tool as AcqImageTool).QeHImages.Count > 0)
                                    {
                                        RRegionGraySubtractTool.toolPar.InputPar.图像 = (tool as AcqImageTool).ReadOrWriteImage(false, false);
                                        RRegionGraySubtractTool.toolPar.RunPar.图像 = RRegionGraySubtractTool.toolPar.InputPar.图像;
                                    }
                                }

                                if (!Configuration.SpeedMode)
                                {
                                    Win_Main.Instance.OutputMsg(string.Format("流程 [{0}] 运行失败，原因：工具 [{1}] {2}", jobName, L_toolList[i].toolName, L_toolList[i].tool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = string.Format("状态：{0}\r\n耗时：{1}ms\r\n说明：{2}", RRegionGraySubtractTool.toolRunStatu.ToString(), 0, L_toolList[i].toolTipInfo);
                                    }
                                }
                                if (JobIsVisable)
                                {
                                    treeNode.ForeColor = Color.Red;
                                }
                                jobRunStatu = JobRunStatu.Fail;
                                strErrToolName = L_toolList[i].toolName;
                                break;
                            }
                            else
                            {
                                if (JobIsVisable)
                                {
                                    try
                                    {
                                        treeNode.ForeColor = Color.LightGreen;
                                    }
                                    catch { }
                                }

                            }
                            for (int j = 0; j < this.outputItemNum; j++)
                            {
                                string outputItem = L_toolList[i].output[j].IOName;
                                object value = null;
                                if (outputItem == "灰度差值")
                                {
                                    value = ((RRegionGraySubtractTool)(L_toolList[i].tool)).toolPar.ResultPar.GrayDiff;
                                }
                                else if (outputItem == "判定结果")
                                {
                                    value = ((RRegionGraySubtractTool)(L_toolList[i].tool)).toolPar.ResultPar.Consequence;
                                }
                                L_toolList[i].GetOutput(outputItem).value = value;
                                if (JobIsVisable)
                                {
                                    GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, outputItem, false).ToolTipText = FormatShowTip(value);

                                }
                            }
                        }
                        #endregion
                        #region ResultsVerdict 
                        else if (L_toolList[i].toolType == ToolType.VerdictMeasure)
                        {
                            VerdictTool verdictTool = (VerdictTool)L_toolList[i].tool;

                            if (!L_toolList[i].enable)
                            {
                                if (!isAcqExists)
                                {
                                    if (i == L_toolList.Count - 1)
                                    {
                                        (tool as AcqImageTool).ReadOrWriteImage(false, false);//删除图片
                                    }
                                }
                                verdictTool.toolRunStatu = (ToolRunStatu.未启用);
                                if (JobIsVisable)
                                {
                                    treeNode.ToolTipText = verdictTool.toolRunStatu.ToString();
                                    treeNode.ForeColor = Color.DarkGray;
                                }
                                continue;
                            }

                            //Hzy20220509//////////////////
                            bool isLocal = false;

                            for (int j = 0; j < inputItemNum; j++)
                            {
                                string inputItemName = L_toolList[i].input[j].IOName;
                                string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                if (sourceFrom == string.Empty)
                                {
                                    verdictTool.toolRunStatu = (ToolRunStatu.未指定输入图像);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = verdictTool.toolRunStatu.ToString();
                                        treeNode.ForeColor = Color.Red;
                                    }
                                    Win_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, verdictTool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    return L_result;
                                }

                                string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                string toolItem = sourceFrom.Split('.')[1];
                                if (inputItemName == "图像" || inputItemName == "InputImage")
                                {
                                    //Hzy20220507
                                    verdictTool.getOriginalJobAcquisition(sourceFrom, sourceToolName, this, out isLocal, out tool);

                                    //非队列模式
                                    HImage hImage;
                                    verdictTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, true, i, this, out hImage);
                                    verdictTool.toolPar.InputPar.图像 = hImage;

                                    loadImage = verdictTool.toolPar.InputPar.图像;
                                    verdictTool.DisplayImage(i, verdictTool.toolPar.InputPar.图像);

                                    if (JobIsVisable)
                                    {
                                        GetToolNodeByNodeText(inputItemName + sourceFrom).ToolTipText = FormatShowTip(verdictTool.toolPar.InputPar.图像);
                                        if (verdictTool.toolPar.InputPar.图像 == null)
                                        {
                                            verdictTool.toolRunStatu = ToolRunStatu.未指定输入图像;
                                            treeNode.ToolTipText = verdictTool.toolRunStatu.ToString();
                                            treeNode.ForeColor = Color.Red;
                                            sourceValueIsEmpty = true;
                                            break;
                                        }
                                    }
                                }
                                else if (inputItemName == "量测值" && sourceFrom != "")
                                {
                                    verdictTool.toolPar.InputPar.DValue = Convert.ToDouble(FindToolInfoByName(sourceToolName).GetOutput(toolItem).value);
                                }
                            }

                            verdictTool.Run(true, false, L_toolList[i].toolName);

                            if (verdictTool.toolRunStatu != (ToolRunStatu.成功))
                            {
                                //20220509
                                if (!isLocal)
                                {
                                    if ((tool as AcqImageTool).QeHImages.Count > 0)
                                    {
                                        verdictTool.toolPar.InputPar.图像 = (tool as AcqImageTool).ReadOrWriteImage(false, false);
                                    }
                                }

                                if (!Configuration.SpeedMode)
                                {
                                    Win_Main.Instance.OutputMsg(string.Format("流程 [{0}] 运行失败，原因：工具 [{1}] {2}", jobName, L_toolList[i].toolName, L_toolList[i].tool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = string.Format("状态：{0}\r\n耗时：{1}ms\r\n说明：{2}", verdictTool.toolRunStatu.ToString(), 0, L_toolList[i].toolTipInfo);
                                    }
                                }
                                if (JobIsVisable)
                                {
                                    treeNode.ForeColor = Color.Red;
                                }
                                jobRunStatu = JobRunStatu.Fail;
                                strErrToolName = L_toolList[i].toolName;
                                break;
                            }
                            else
                            {
                                if (JobIsVisable)
                                {
                                    try
                                    {
                                        treeNode.ForeColor = Color.LightGreen;
                                    }
                                    catch { }
                                }
                            }
                            for (int j = 0; j < this.outputItemNum; j++)
                            {
                                string outputItem = L_toolList[i].output[j].IOName;
                                object value = null;
                                if (outputItem == "量测值")
                                {
                                    value = ((VerdictTool)(L_toolList[i].tool)).toolPar.ResultPar.DValue;
                                }
                                if (outputItem == "判定结果")
                                {
                                    value = ((VerdictTool)(L_toolList[i].tool)).toolPar.ResultPar.Consequence;
                                }
                                L_toolList[i].GetOutput(outputItem).value = value;
                                if (JobIsVisable)
                                {
                                    GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, outputItem, false).ToolTipText = FormatShowTip(value);

                                }
                            }

                           
                        }
                        #endregion
                        #region DefectDetection
                        else if (L_toolList[i].toolType == ToolType.DefectDetection)
                        {
                            DefectDetectionTool DefectDetectionTool = (DefectDetectionTool)L_toolList[i].tool;
                            if (!L_toolList[i].enable)
                            {
                                if (!isAcqExists)
                                {
                                    if (i == L_toolList.Count - 1)
                                    {
                                        (tool as AcqImageTool).ReadOrWriteImage(false, false);//删除图片
                                    }
                                }
                                DefectDetectionTool.toolRunStatu = (ToolRunStatu.未启用);
                                if (JobIsVisable)
                                {
                                    treeNode.ToolTipText = DefectDetectionTool.toolRunStatu.ToString();
                                    treeNode.ForeColor = Color.DarkGray;
                                }
                                continue;
                            }

                            //Hzy20220509//////////////////
                            bool isLocal = false;
                            ///////////////////////////////

                            for (int j = 0; j < inputItemNum; j++)
                            {
                                string inputItemName = L_toolList[i].input[j].IOName;
                                string sourceFrom = L_toolList[i].GetInput(inputItemName).value.ToString();
                                if (sourceFrom == string.Empty && inputItemName == "图像")
                                {
                                    DefectDetectionTool.toolRunStatu = (ToolRunStatu.未指定输入图像);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = DefectDetectionTool.toolRunStatu.ToString();
                                        treeNode.ForeColor = Color.Red;
                                    }
                                    Win_Main.Instance.OutputMsg(string.Format("工具 [{0}] 运行失败，原因： {1}", L_toolList[i].toolName, DefectDetectionTool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    return L_result;
                                }

                                string sourceToolName = sourceFrom.Split('.')[0].Substring(2);
                                string toolItem = sourceFrom.Split('.')[1];
                                if (inputItemName == "图像" || inputItemName == "InputImage")
                                {
                                    //Hzy20220507
                                    DefectDetectionTool.getOriginalJobAcquisition(sourceFrom, sourceToolName, this, out isLocal, out tool);

                                    //非队列模式
                                    HImage hImage;
                                    DefectDetectionTool.getOriginalJob(sourceFrom, sourceToolName, toolItem, true, i, this, out hImage);
                                    DefectDetectionTool.toolPar.InputPar.图像 = hImage;
                                    DefectDetectionTool.toolPar.RunPar.图像 = DefectDetectionTool.toolPar.InputPar.图像;

                                    loadImage = DefectDetectionTool.toolPar.InputPar.图像;
                                    DefectDetectionTool.DisplayImage(i, DefectDetectionTool.toolPar.InputPar.图像);

                                    if (JobIsVisable)
                                    {
                                        try
                                        {
                                            GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, inputItemName + sourceFrom).ToolTipText = FormatShowTip(DefectDetectionTool.toolPar.InputPar.图像);
                                        }
                                        catch { }
                                    }
                                    if (DefectDetectionTool.toolPar.InputPar.图像 == null)
                                    {
                                        DefectDetectionTool.toolRunStatu = ToolRunStatu.未指定输入图像;
                                        if (JobIsVisable)
                                        {
                                            treeNode.ToolTipText = DefectDetectionTool.toolRunStatu.ToString();
                                            treeNode.ForeColor = Color.Red;
                                        }
                                        sourceValueIsEmpty = true;
                                        break;
                                    }
                                }
                                else if (inputItemName == "跟随位置")
                                {
                                    DefectDetectionTool.toolPar.InputPar.Pose = FindToolInfoByName(sourceToolName).GetOutput(toolItem).value as List<XYU>;
                                    if (JobIsVisable)
                                    {
                                        GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, inputItemName + sourceFrom).ToolTipText = FormatShowTip(DefectDetectionTool.toolPar.InputPar.Pose);

                                    }
                                }
                            }
                            if (sourceValueIsEmpty)
                                break;

                            DefectDetectionTool.Run(false, false, L_toolList[i].toolName);

                            if (DefectDetectionTool.toolRunStatu != (ToolRunStatu.成功))
                            {
                                //20220509
                                if (!isLocal)
                                {
                                    if ((tool as AcqImageTool).QeHImages.Count > 0)
                                    {
                                        DefectDetectionTool.toolPar.InputPar.图像 = (tool as AcqImageTool).ReadOrWriteImage(false, false);
                                        DefectDetectionTool.toolPar.RunPar.图像 = DefectDetectionTool.toolPar.InputPar.图像;
                                    }
                                }

                                if (!Configuration.SpeedMode)
                                {
                                    Win_Main.Instance.OutputMsg(string.Format("流程 [{0}] 运行失败，原因：工具 [{1}] {2}", jobName, L_toolList[i].toolName, L_toolList[i].tool.toolRunStatu.ToString()), Win_Log.InfoType.error);
                                    if (JobIsVisable)
                                    {
                                        treeNode.ToolTipText = string.Format("状态：{0}\r\n耗时：{1}ms\r\n说明：{2}", DefectDetectionTool.toolRunStatu.ToString(), 0, L_toolList[i].toolTipInfo);
                                    }
                                }
                                if (JobIsVisable)
                                {
                                    treeNode.ForeColor = Color.Red;
                                }
                                jobRunStatu = JobRunStatu.Fail;
                                strErrToolName = L_toolList[i].toolName;
                                //20220501，工具失败，跳出循环
                                break;
                            }
                            else
                            {
                                if (JobIsVisable)
                                {
                                    try
                                    {
                                        treeNode.ForeColor = Color.LightGreen;
                                    }
                                    catch { }
                                }
                            }


                            for (int j = 0; j < this.outputItemNum; j++)
                            {
                                string outputItem = L_toolList[i].output[j].IOName;
                                object value = null;
                                if (L_toolList[i].output[j].ioType == DataType.Region)
                                {
                                    L_toolList[i].GetOutput(outputItem).value = ((DefectDetectionTool)(L_toolList[i].tool)).toolPar.ResultsPar.resultRegion;
                                    value = ((DefectDetectionTool)(L_toolList[i].tool)).toolPar.ResultsPar.resultRegion;
                                }
                                else if (L_toolList[i].output[j].ioType == DataType.Int)
                                {
                                    L_toolList[i].GetOutput(outputItem).value = ((DefectDetectionTool)(L_toolList[i].tool)).toolPar.ResultsPar.resultCount;
                                    value = ((DefectDetectionTool)(L_toolList[i].tool)).toolPar.ResultsPar.resultCount;
                                }
                                if (JobIsVisable)
                                {
                                    GetToolIONodeByNodeText(jobname, L_toolList[i].toolName, outputItem, false).ToolTipText = FormatShowTip(value);
                                }
                            }
                        }
                        #endregion
                        if (!Configuration.SpeedMode && JobIsVisable)
                        {
                            double elapseTime = jobElapsedTime.ElapsedMilliseconds - recordElapseTime;
                            recordElapseTime = jobElapsedTime.ElapsedMilliseconds;
                            treeNode.ToolTipText = string.Format("状态：{0}\r\n耗时：{1}ms\r\n说明：{2}", ((ToolBase)L_toolList[i].tool).toolRunStatu.ToString(), elapseTime, L_toolList[i].toolTipInfo);
                        }
                        Application.DoEvents();
                    }

                    //hzy20220505,优化工具运行失败时，报错异常


                    //GetJobTree().SelectedNode = null;//NG报错后，耗时120ms左右，连续OK，耗时0-1ms

                    jobElapsedTime.Stop();
                    double time = jobElapsedTime.ElapsedMilliseconds;
                    GetJobTree().Refresh();

                    if (jobRunStatu == JobRunStatu.Succeed)
                    {
                        bJobRunStatus = true;
                        //保存OK图片到固定文件夹，用来软件加载时运行
                        if (!Directory.Exists(Application.StartupPath + "\\ProjectImage"))
                        {
                            Directory.CreateDirectory(Application.StartupPath + "\\ProjectImage");
                        }
                        Task.Factory.StartNew(() =>
                        {
                            if (loadImage != null)
                            {
                                //判断加载文件夹内是否存在指定文件
                                for (int m = 0; m < Win_Job.Instance.listView1.Nodes.Count; m++)
                                {
                                    string strSchemeName = Win_Job.Instance.ToolBoxEdit_ProjectName.Text.Split('-').ToArray()[0].Split('：').ToArray()[1];
                                    string strJobName = Win_Job.Instance.listView1.Nodes[m].Text;
                                    if (!File.Exists(string.Format(Application.StartupPath + "\\ProjectImage\\{0}.bmp", strJobName)))
                                    {
                                        loadImage.WriteImage("bmp", 0, string.Format(Application.StartupPath + "\\ProjectImage\\{0}.bmp", strJobName));
                                    }
                                }
                            }
                        });
                    }
                    else
                    {
                        bJobRunStatus = false;
                        Win_Main.Instance.OutputMsg("流程" + " [" + jobName + "] " + "，工具：" + strErrToolName + "运行错误，请检查", Win_Log.InfoType.error);
                    }

                    //自动运行状态下结果不显示
                    if (!initRun && debugImageWindow != "不绑定")
                    {
                        if (jobRunStatu == JobRunStatu.Succeed)
                        {
                            //GetImageWindowControl().SetResult(true);
                            if (!Configuration.SpeedMode)
                                Win_Main.Instance.OutputMsg("流程" + " [" + jobName + "] " + "运行成功，耗时：" + time + "ms", Win_Log.InfoType.tip);
                        }
                        else
                        {
                            //GetImageWindowControl().SetResult(false);
                        }
                        if (Machine.curFormMode == FormMode.VisionForm && Win_Job.Instance.Visible)
                        {
                            foreach (TreeNode item in Win_Job.Instance.listView1.Nodes)
                            {
                                if (item.Text == Win_Job.Instance.JobEdit_JobName.Text.Substring(5))
                                {
                                    item.ToolTipText = string.Format("{0}  {1}ms", jobRunStatu == JobRunStatu.Succeed ? "OK" : "NG", time);
                                    break;
                                }
                            }
                            Win_Job.Instance.listView1.Refresh();
                        }

                        elapsedTime = time;
                        //更新输出
                        UpdateJobOutputList();

                        Project.Instance.curEngine.globelVariable.SetGlobalVariableValue(string.Format("流程[{0}].运行状态", jobName), jobRunStatu == JobRunStatu.Succeed ? "运行成功" : "运行失败");
                        Project.Instance.curEngine.globelVariable.SetGlobalVariableValue(string.Format("流程[{0}].运行时间", jobName), time);
                    }
                    GC.Collect();
                    Application.DoEvents();
                    Start.RunOnceStopMark = false;

                    Start.OnJobRunDone(new JobRunDoneEventArgs(jobName, Job.FindJobByName(jobName).elapsedTime, loadImage, Job.FindJobByName(jobName).OutPutList, bJobRunStatus, bJobProRunStatus, Project.Instance.curEngine.L_jobList.Count));
                   
                    return L_result;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
                return null;
            }
            
        }

        public void UpdateJobOutputList()
        {
            foreach (OutT item in OutPutList)
            {
                item.MValue = FindToolInfoByName(item.ToolName).GetOutput(item.OutItem).value;
                item.MToolType = FindToolInfoByName(item.ToolName).toolType;
            }
        }

        internal static bool isCameraHardTrigger()
        {
            bool bRet = false;
            //for (int i = 0; i < Job.FindJobByName(jobName).L_toolList.Count; i++)
            //{
            //    if (Job.FindJobByName(jobName).L_toolList[i].toolType == ToolType.ImageAcq)
            //    {
            //        bRet = (Job.FindJobByName(jobName).FindToolByName(L_toolList[i].toolName) as AcqImageTool).hardTrigger;
            //        if (bRet)
            //            break;
            //    }
            //}
            return bRet;
        }
    }
}
