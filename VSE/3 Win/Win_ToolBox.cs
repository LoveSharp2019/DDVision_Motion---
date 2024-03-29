using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using VControls;
using VSE.Core;
using WinFormsUI.Docking;

namespace VSE
{
    internal partial class Win_ToolBox : DockContent
    {
        internal Win_ToolBox()
        {
            InitializeComponent();
            
            this.BackColor = VControls.VUI.WinBackColor;
            tvw_tools.BackColor = VControls.VUI.WinBackColor;
            lbl_toolInfo.BackColor = VControls.VUI.WinBackColor;
        }

       

        /// <summary>
        /// 用于关闭工具箱窗体后，再次打开时还能出现在原来消失时的位置，而不是每次只能出现在固定的位置
        /// </summary>
        internal static DockState lastDockState = DockState.Float;
        /// <summary>
        /// 正在拖拽的节点
        /// </summary>
        internal static TreeNode DragNode = null;
        /// <summary>
        /// 节点来源
        /// </summary>
        internal static TreeView NodeSource = null;
        /// <summary>
        /// 树形节点移动方向
        /// </summary>
        internal static MoveTreeView MoveTo = MoveTreeView.NoMove;
        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Win_ToolBox _instance;
        internal static Win_ToolBox Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_ToolBox();
                return _instance;
            }
        }

        /// <summary>
        /// 自动连接源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoConnectSource(string jobName, TreeNode inputNode)
        {
            try
            {
                string nodeText = string.Empty;
                if (inputNode.Level == 1)
                {
                    nodeText = inputNode.Parent.Text;
                }
                else
                {
                    nodeText = inputNode.Text;
                }

                //获取最近的相同类型的输出作为源
                TreeNode sourceNode = null;
                Job job = Job.FindJobByName(jobName);
                for (int i = job.L_toolList.Count - 2; i >= 0; i--)
                {
                    for (int j = job.L_toolList[i].output.Count - 1; j >= 0; j--)
                    {
                        if (job.L_toolList[i].output[j].ioType == (DataType)inputNode.Tag)
                        {
                            sourceNode = job.GetToolIONodeByNodeText(jobName, job.L_toolList[i].toolName, job.L_toolList[i].output[j].IOName, false);



                            if (sourceNode == null)
                                return;        //无可源项，返回

                            string input = inputNode.Text;
                            inputNode.Text = input + "<-"+ sourceNode.Parent.Text +"."+ sourceNode.Text;
                            job.FindToolInfoByName(nodeText).GetInput(input).value = "<-" + sourceNode.Parent.Text + "." + sourceNode.Text;

                          //  job.DrawLine();

                            return;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        /// <summary>
        /// 添加工具，当toolInfo1参数为null而toolType不为空时表示添加工具，当toolType为空而toolInfo1不为空时表示粘贴工具
        /// </summary>
        /// <param name="tool">工具类型</param>
        /// <param name="toolInfo1">被复制的工具</param>
        /// <param name="isInsert">插入位置，当为-1时，表示在末尾插入，当不为-1时，表示被插入的工具索引</param>
        internal void AddTool(string toolType, ToolInfo toolInfo1, int insertIdx = -1)
        {
            try
            {
                int imageIndex = 4;
                string jobName = Win_Job.Instance.JobEdit_JobName.Text.Substring(5);
                Job.GetJobTreeStatic().isDrawing = true;
                
                string toolName = string.Empty;
                if (Job.FindJobByName(jobName).isRunLoop)
                {
                    Win_Log.Instance.OutputMsg("当前流程正在运行，不可添加工具",Win_Log.InfoType.error);
                    return;
                }

                ToolInfo toolInfo = toolInfo1;
                TreeNode toolNode = new TreeNode();
                switch (toolType)
                {
                    #region 1图像工具
                    #region 1图像采集
                    case "图像采集":
                    case "ImageAcq":
                        toolName = Job.FindJobByName(jobName).GetNewToolName("图像采集");
                        if (toolName == "TooMuch")       //此工具添加个数已达到上限100各，不让继续添加
                            return;

                        if (toolInfo1 == null)
                        {
                            AcqImageTool imageAcqTool = new AcqImageTool();
                            toolInfo = new ToolInfo(ToolType.ImageAcq, imageAcqTool, jobName, toolName);
                        }
                        else
                        {
                            toolInfo1.toolName = toolName;
                            for (int i = 0; i < toolInfo1.input.Count; i++)
                            {
                                toolInfo1.input[i].value = string.Empty;
                            }
                        }

                        if (insertIdx == -1)
                        {
                            Job.FindJobByName(jobName).L_toolList.Add(toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Add("", toolInfo.toolName, imageIndex, imageIndex);
                        }
                        else
                        {
                            Job.FindJobByName(jobName).L_toolList.Insert(insertIdx, toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Insert(insertIdx, "", toolName, imageIndex, imageIndex);
                        }

                        //添加常用项
                        TreeNode itemNode = toolNode.Nodes.Add("", "图像", 3, 3);
                        itemNode.ForeColor =  Color.LightGreen;
                        itemNode.Tag = DataType.Image;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO( "图像", "", DataType.Image));
                        }
                        break;
                    #endregion
                    #region 2存储图像
                    case "存储图像":
                    case "FindLin信息ex":
                        toolName = Job.FindJobByName(jobName).GetNewToolName("存储图像");
                        if (toolName == "TooMuch")       //此工具添加个数已达到上限100各，不让继续添加
                            return;

                        if (toolInfo1 == null)
                        {
                            SaveImageTool saveImageTool = new SaveImageTool();
                            toolInfo = new ToolInfo(ToolType.SaveImage, saveImageTool, jobName, toolName);
                        }
                        else
                        {
                            toolInfo1.toolName = toolName;
                            for (int i = 0; i < toolInfo1.input.Count; i++)
                            {
                                toolInfo1.input[i].value = string.Empty;
                            }
                        }

                        if (insertIdx == -1)
                        {
                            Job.FindJobByName(jobName).L_toolList.Add(toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Add("", toolInfo.toolName, imageIndex+1, imageIndex+1);
                        }
                        else
                        {
                            Job.FindJobByName(jobName).L_toolList.Insert(insertIdx, toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Insert(insertIdx, "", toolName, imageIndex + 1, imageIndex + 1);
                        }

                        //添加常用项
                        itemNode = toolNode.Nodes.Add("","图像", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Image;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO( "图像", "", DataType.Image));
                        }
                        //自动链接输入项
                        AutoConnectSource(jobName, itemNode);
                        break;
                    #endregion
                    #region 3二值化
                    case "二值化":
                    case "Binarization":
                        toolName = Job.FindJobByName(jobName).GetNewToolName("二值化");
                        if (toolName == "TooMuch")       //此工具添加个数已达到上限100各，不让继续添加
                            return;

                        if (toolInfo1 == null)
                        {
                            BinarizationTool binarizationTool = new BinarizationTool();
                            toolInfo = new ToolInfo(ToolType.Binarization, binarizationTool, jobName, toolName);
                        }
                        else
                        {
                            toolInfo1.toolName = toolName;
                            for (int i = 0; i < toolInfo1.input.Count; i++)
                            {
                                toolInfo1.input[i].value = string.Empty;
                            }
                        }

                        if (insertIdx == -1)
                        {
                            Job.FindJobByName(jobName).L_toolList.Add(toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Add("", toolInfo.toolName, imageIndex + 2, imageIndex + 2);
                        }
                        else
                        {
                            Job.FindJobByName(jobName).L_toolList.Insert(insertIdx, toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Insert(insertIdx, "", toolName, imageIndex + 2, imageIndex + 2);
                        }
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("","图像", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Image;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO( "图像", "", DataType.Image));
                        }
                        //自动链接输入项
                        AutoConnectSource(jobName, itemNode);
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("",  "图像", 3, 3);
                        itemNode.ForeColor = Color.LightGreen;
                        itemNode.Tag = DataType.Image;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO( "图像", "", DataType.Image));
                        }
                       
                        break;
                    #endregion
                    #region 4形态学
                    case "开闭运算":
                    case "Morphology":
                        toolName = Job.FindJobByName(jobName).GetNewToolName("开闭运算");
                        if (toolName == "TooMuch")       //此工具添加个数已达到上限100各，不让继续添加
                            return;

                        if (toolInfo1 == null)
                        {
                            MorphologyTool morphologyTool = new MorphologyTool();
                            toolInfo = new ToolInfo(ToolType.Morphology, morphologyTool, jobName, toolName);
                        }
                        else
                        {
                            toolInfo1.toolName = toolName;
                            for (int i = 0; i < toolInfo1.input.Count; i++)
                            {
                                toolInfo1.input[i].value = string.Empty;
                            }
                        }

                        if (insertIdx == -1)
                        {
                            Job.FindJobByName(jobName).L_toolList.Add(toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Add("", toolInfo.toolName, imageIndex + 3, imageIndex + 3);
                        }
                        else
                        {
                            Job.FindJobByName(jobName).L_toolList.Insert(insertIdx, toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Insert(insertIdx, "", toolName, imageIndex + 3, imageIndex + 3);
                        }
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("","图像", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Image;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO( "图像", "", DataType.Image));
                        }
                        
                        //自动链接输入项
                        AutoConnectSource(jobName, itemNode);
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "ROI", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Region;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO("ROI", "", DataType.Region));
                        }

                        //自动链接输入项
                        AutoConnectSource(jobName, itemNode);
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "ROI", 3, 3);
                        itemNode.ForeColor = Color.LightGreen;
                        itemNode.Tag = DataType.Region;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO("ROI", "", DataType.Region));
                        }

                        break;
                    #endregion
                    #region 5图像滤波
                    #endregion
                    #region 6图像分解
                    case "彩图转RGB":
                    case "FindLinex":
                        toolName = Job.FindJobByName(jobName).GetNewToolName("彩图转RGB");
                        if (toolName == "TooMuch")       //此工具添加个数已达到上限100各，不让继续添加
                            return;

                        if (toolInfo1 == null)
                        {
                            ColorToRGBTool colorToRGBTool = new ColorToRGBTool();
                            toolInfo = new ToolInfo(ToolType.ImageDecomposition, colorToRGBTool, jobName, toolName);
                        }
                        else
                        {
                            toolInfo1.toolName = toolName;
                            for (int i = 0; i < toolInfo1.input.Count; i++)
                            {
                                toolInfo1.input[i].value = string.Empty;
                            }
                        }

                        if (insertIdx == -1)
                        {
                            Job.FindJobByName(jobName).L_toolList.Add(toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Add("", toolInfo.toolName, imageIndex + 5, imageIndex + 5);
                        }
                        else
                        {
                            Job.FindJobByName(jobName).L_toolList.Insert(insertIdx, toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Insert(insertIdx, "", toolName, imageIndex +5, imageIndex +5);
                        }

                        //添加常用项
                        itemNode = toolNode.Nodes.Add("","<--输入图像", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Image;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO("输入图像", "", DataType.Image));
                        }
                        break;
                    #endregion
                    #region 7查找区域1
                    case "查找区域1":
                    case "FindRegion1":
                        toolName = Job.FindJobByName(jobName).GetNewToolName("查找区域1");
                        if (toolName == "TooMuch")       //此工具添加个数已达到上限100各，不让继续添加
                            return;

                        if (toolInfo1 == null)
                        {
                            FindROI FindROI = new FindROI();
                            toolInfo = new ToolInfo(ToolType.FindROI, FindROI, jobName, toolName);
                        }
                        else
                        {
                            toolInfo1.toolName = toolName;
                            for (int i = 0; i < toolInfo1.input.Count; i++)
                            {
                                toolInfo1.input[i].value = string.Empty;
                            }
                        }

                        if (insertIdx == -1)
                        {
                            Job.FindJobByName(jobName).L_toolList.Add(toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Add("", toolInfo.toolName, imageIndex + 6, imageIndex + 6);
                        }
                        else
                        {
                            Job.FindJobByName(jobName).L_toolList.Insert(insertIdx, toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Insert(insertIdx, "", toolName, imageIndex + 6, imageIndex + 6);
                        }
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("","图像", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Image;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO( "图像", "", DataType.Image));
                        }
                        //自动链接输入项
                        AutoConnectSource(jobName, itemNode);
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("","区域", 3, 3);
                        itemNode.ForeColor = Color.LightGreen;
                        itemNode.Tag = DataType.Region;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO("区域", "", DataType.Region));
                        }

                        break;
                    #endregion
                    #region 8查找区域2
                    case "查找区域2":
                    case "FindRegion2":
                        toolName = Job.FindJobByName(jobName).GetNewToolName("查找区域2");
                        if (toolName == "TooMuch")       //此工具添加个数已达到上限100各，不让继续添加
                            return;

                        if (toolInfo1 == null)
                        {
                            FindRegionTool FindRegionTool = new FindRegionTool();
                            toolInfo = new ToolInfo(ToolType.FindRegion, FindRegionTool, jobName, toolName);
                        }
                        else
                        {
                            toolInfo1.toolName = toolName;
                            for (int i = 0; i < toolInfo1.input.Count; i++)
                            {
                                toolInfo1.input[i].value = string.Empty;
                            }
                        }

                        if (insertIdx == -1)
                        {
                            Job.FindJobByName(jobName).L_toolList.Add(toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Add("", toolInfo.toolName, imageIndex + 7, imageIndex + 7);
                        }
                        else
                        {
                            Job.FindJobByName(jobName).L_toolList.Insert(insertIdx, toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Insert(insertIdx, "", toolName, imageIndex + 7, imageIndex + 7);
                        }
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "图像", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Image;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO("图像", "", DataType.Image));
                        }
                        //自动链接输入项
                        AutoConnectSource(jobName, itemNode);
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "跟随位置", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Pose;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO("跟随位置", "", DataType.Pose));
                        }
                        //自动链接输入项
                        AutoConnectSource(jobName, itemNode);
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "区域", 3, 3);
                        itemNode.ForeColor = Color.LightGreen;
                        itemNode.Tag = DataType.Region;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO("区域", "", DataType.Region));
                        }
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "判定结果", 3, 3);
                        itemNode.ForeColor = Color.LightGreen;
                        itemNode.Tag = DataType.Bool;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO("判定结果", "", DataType.Bool));
                        }
                        break;
                    #endregion
                    #region 9缺陷检测
                    case "缺陷检测":
                    case "DefectDetection":
                        toolName = Job.FindJobByName(jobName).GetNewToolName("缺陷检测工具");
                        if (toolName == "TooMuch")       //此工具添加个数已达到上限100各，不让继续添加
                            return;

                        if (toolInfo1 == null)
                        {
                            DefectDetectionTool DefectDetectionTool = new DefectDetectionTool();
                            toolInfo = new ToolInfo(ToolType.DefectDetection, DefectDetectionTool, jobName, toolName);
                        }
                        else
                        {
                            toolInfo1.toolName = toolName;
                            for (int i = 0; i < toolInfo1.input.Count; i++)
                            {
                                toolInfo1.input[i].value = string.Empty;
                            }
                        }

                        if (insertIdx == -1)
                        {
                            Job.FindJobByName(jobName).L_toolList.Add(toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Add("", toolInfo.toolName, imageIndex + 8, imageIndex + 8);
                        }
                        else
                        {
                            Job.FindJobByName(jobName).L_toolList.Insert(insertIdx, toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Insert(insertIdx, "", toolName, imageIndex + 8, imageIndex + 8);
                        }

                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "图像", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Image;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO("图像", "", DataType.Image));
                        }
                        //自动链接输入项
                        AutoConnectSource(jobName, itemNode);
                        itemNode = toolNode.Nodes.Add("", "跟随位置", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Pose;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO("跟随位置", "", DataType.Pose));
                        }
                        //自动链接输入项
                        AutoConnectSource(jobName, itemNode);
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "区域", 3, 3);
                        itemNode.ForeColor = Color.LightGreen;
                        itemNode.Tag = DataType.Region;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO("区域", "", DataType.Region));
                        }
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "区域个数", 3, 3);
                        itemNode.ForeColor = Color.LightGreen;
                        itemNode.Tag = DataType.Int;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO("区域个数", "", DataType.Int));
                        }
                        break;
                    #endregion
                    #endregion

                    #region 2坐标变换
                    #region 1标定板标定
                    #endregion
                    #region 2N点标定
                    #endregion
                    #region 3空间转换
                    #endregion

                    #endregion

                    #region 3定位工具
                    #region 1模板匹配
                    case "2维匹配":
                    case "Match":
                        toolName = Job.FindJobByName(jobName).GetNewToolName("2维匹配");
                        if (toolName == "TooMuch")       //此工具添加个数已达到上限100各，不让继续添加
                            return;

                        if (toolInfo1 == null)
                        {
                            MatchTool MatchTool = new MatchTool();
                            toolInfo = new ToolInfo(ToolType.Match, MatchTool, jobName, toolName);
                        }
                        else
                        {
                            toolInfo1.toolName = toolName;
                            for (int i = 0; i < toolInfo1.input.Count; i++)
                            {
                                toolInfo1.input[i].value = string.Empty;
                            }
                        }

                        if (insertIdx == -1)
                        {
                            Job.FindJobByName(jobName).L_toolList.Add(toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Add("", toolInfo.toolName, imageIndex + 12, imageIndex + 12);
                        }
                        else
                        {
                            Job.FindJobByName(jobName).L_toolList.Insert(insertIdx, toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Insert(insertIdx, "", toolName, imageIndex + 12, imageIndex + 12);
                        }

                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "图像", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Image;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO( "图像", "", DataType.Image));
                        }
                        //自动链接输入项
                        AutoConnectSource(jobName, itemNode);
                        itemNode = toolNode.Nodes.Add("",  "位置", 3, 3);
                        itemNode.ForeColor = Color.LightGreen;
                        itemNode.Tag = DataType.Pose;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO( "位置", "", DataType.Pose));
                        }
                        break;
                    #endregion
                    #region 2斑点分析
                    case "斑点工具":
                    case "BlobTool":
                        toolName = Job.FindJobByName(jobName).GetNewToolName("斑点工具");
                        if (toolName == "TooMuch")       //此工具添加个数已达到上限100各，不让继续添加
                            return;

                        if (toolInfo1 == null)
                        {
                            BlobTool blobAnalyseTool = new BlobTool();
                            toolInfo = new ToolInfo(ToolType.BlobAnalyse, blobAnalyseTool, jobName, toolName);
                        }
                        else
                        {
                            toolInfo1.toolName = toolName;
                            for (int i = 0; i < toolInfo1.input.Count; i++)
                            {
                                toolInfo1.input[i].value = string.Empty;
                            }
                        }

                        if (insertIdx == -1)
                        {
                            Job.FindJobByName(jobName).L_toolList.Add(toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Add("", toolInfo.toolName, imageIndex + 13, imageIndex + 13);
                        }
                        else
                        {
                            Job.FindJobByName(jobName).L_toolList.Insert(insertIdx, toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Insert(insertIdx, "", toolName, imageIndex + 13, imageIndex + 13);
                        }

                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "图像", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Image;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO( "图像", "", DataType.Image));
                        }
                        //自动链接输入项
                        AutoConnectSource(jobName, itemNode);

                        itemNode = toolNode.Nodes.Add("", "跟随位置", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Pose;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO("跟随位置", "", DataType.Pose));
                        }
                        //自动链接输入项
                        AutoConnectSource(jobName, itemNode);
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "区域位置", 3, 3);
                        itemNode.ForeColor = Color.LightGreen;
                        itemNode.Tag = DataType.Pose;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO("区域位置", "", DataType.Pose));
                        }
                        itemNode = toolNode.Nodes.Add("", "区域", 3, 3);
                        itemNode.ForeColor = Color.LightGreen;
                        itemNode.Tag = DataType.Region;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO("区域", "", DataType.Region));
                        }
                        break;
                    #endregion
                    #region 3找线
                    case "找边":
                    case "FindLine":
                        toolName = Job.FindJobByName(jobName).GetNewToolName("抓边工具");
                        if (toolName == "TooMuch")       //此工具添加个数已达到上限100各，不让继续添加
                            return;

                        if (toolInfo1 == null)
                        {
                            FindLineTool FindLineTool = new FindLineTool();
                            toolInfo = new ToolInfo(ToolType.FindLine, FindLineTool, jobName, toolName);
                        }
                        else
                        {
                            toolInfo1.toolName = toolName;
                            for (int i = 0; i < toolInfo1.input.Count; i++)
                            {
                                toolInfo1.input[i].value = string.Empty;
                            }
                        }

                        if (insertIdx == -1)
                        {
                            Job.FindJobByName(jobName).L_toolList.Add(toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Add("", toolInfo.toolName, imageIndex + 14, imageIndex + 14);
                        }
                        else
                        {
                            Job.FindJobByName(jobName).L_toolList.Insert(insertIdx, toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Insert(insertIdx, "", toolName, imageIndex + 14, imageIndex + 14);
                        }

                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "图像", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Image;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO("图像", "", DataType.Image));
                        }
                        //自动链接输入项
                        AutoConnectSource(jobName, itemNode);
                        itemNode = toolNode.Nodes.Add("", "跟随位置", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Pose;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO("跟随位置", "", DataType.Pose));
                        }
                        //自动链接输入项
                        AutoConnectSource(jobName, itemNode);
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "Line", 3, 3);
                        itemNode.ForeColor = Color.LightGreen;
                        itemNode.Tag = DataType.Line;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO("Line", "", DataType.Line));
                        }
                        
                        break;
                    #endregion
                    #region 4找圆
                    case "找圆":
                    case "FindCircle":
                        toolName = Job.FindJobByName(jobName).GetNewToolName("抓圆工具");
                        if (toolName == "TooMuch")       //此工具添加个数已达到上限100各，不让继续添加
                            return;

                        if (toolInfo1 == null)
                        {
                            FindCircleTool FindCircleTool = new FindCircleTool();
                            toolInfo = new ToolInfo(ToolType.FindCircle, FindCircleTool, jobName, toolName);
                        }
                        else
                        {
                            toolInfo1.toolName = toolName;
                            for (int i = 0; i < toolInfo1.input.Count; i++)
                            {
                                toolInfo1.input[i].value = string.Empty;
                            }
                        }

                        if (insertIdx == -1)
                        {
                            Job.FindJobByName(jobName).L_toolList.Add(toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Add("", toolInfo.toolName, imageIndex + 15, imageIndex + 15);
                        }
                        else
                        {
                            Job.FindJobByName(jobName).L_toolList.Insert(insertIdx, toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Insert(insertIdx, "", toolName, imageIndex + 15, imageIndex + 15);
                        }

                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "图像", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Image;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO("图像", "", DataType.Image));
                        }
                        //自动链接输入项
                        AutoConnectSource(jobName, itemNode);
                        itemNode = toolNode.Nodes.Add("", "跟随位置", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Pose;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO("跟随位置", "", DataType.Pose));
                        }
                        //自动链接输入项
                        AutoConnectSource(jobName, itemNode);
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "Circle", 3, 3);
                        itemNode.ForeColor = Color.LightGreen;
                        itemNode.Tag = DataType.Circle;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO("Circle", "", DataType.Circle));
                        }
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "圆心", 3, 3);
                        itemNode.ForeColor = Color.LightGreen;
                        itemNode.Tag = DataType.XY;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO("圆心", "", DataType.XY));
                        }
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "半径", 3, 3);
                        itemNode.ForeColor = Color.LightGreen;
                        itemNode.Tag = DataType.Double;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO("半径", "", DataType.Double));
                        }
                        break;
                    #endregion
                    #region 5找拐角
                    #endregion

                    #endregion

                    #region 4量测工具
                    #region 1点到点距离
                    case "点到点距离":
                    case "PPDistance":
                        toolName = Job.FindJobByName(jobName).GetNewToolName("点到点距离");
                        if (toolName == "TooMuch")       //此工具添加个数已达到上限100各，不让继续添加
                            return;

                        if (toolInfo1 == null)
                        {
                            PPDistanceTool binarizationTool = new PPDistanceTool();
                            toolInfo = new ToolInfo(ToolType.P2PDistance, binarizationTool, jobName, toolName);
                        }
                        else
                        {
                            toolInfo1.toolName = toolName;
                            for (int i = 0; i < toolInfo1.input.Count; i++)
                            {
                                toolInfo1.input[i].value = string.Empty;
                            }
                        }

                        if (insertIdx == -1)
                        {
                            Job.FindJobByName(jobName).L_toolList.Add(toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Add("", toolInfo.toolName, imageIndex + 17, imageIndex + 17);
                        }
                        else
                        {
                            Job.FindJobByName(jobName).L_toolList.Insert(insertIdx, toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Insert(insertIdx, "", toolName, imageIndex + 17, imageIndex + 17);
                        }
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "图像", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Image;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO("图像", "", DataType.Image));
                        }
                        //自动链接输入项
                        AutoConnectSource(jobName, itemNode);
                        //添加常用项 -点1
                        itemNode = toolNode.Nodes.Add("", "Point1", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.XY;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO("Point1", "", DataType.XY));
                        }
                        //自动链接输入项 线2
                        AutoConnectSource(jobName, itemNode);
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "Point2", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.XY;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO("Point2", "", DataType.XY));
                        }
                        //自动链接输入项
                        AutoConnectSource(jobName, itemNode);
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "距离", 3, 3);
                        itemNode.ForeColor = Color.LightGreen;
                        itemNode.Tag = DataType.Double;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO("距离", "", DataType.Double));
                        }
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "判定结果", 3, 3);
                        itemNode.ForeColor = Color.LightGreen;
                        itemNode.Tag = DataType.Bool;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO("判定结果", "", DataType.Bool));
                        }

                        break;
                    #endregion
                    #region 2点到圆距离

                    #endregion
                    #region 3点到线距离
                    case "点到线距离":
                    case "PLDistance":
                        toolName = Job.FindJobByName(jobName).GetNewToolName("点到线距离");
                        if (toolName == "TooMuch")       //此工具添加个数已达到上限100各，不让继续添加
                            return;

                        if (toolInfo1 == null)
                        {
                            PLDistanceTool binarizationTool = new PLDistanceTool();
                            toolInfo = new ToolInfo(ToolType.P2LDistance, binarizationTool, jobName, toolName);
                        }
                        else
                        {
                            toolInfo1.toolName = toolName;
                            for (int i = 0; i < toolInfo1.input.Count; i++)
                            {
                                toolInfo1.input[i].value = string.Empty;
                            }
                        }

                        if (insertIdx == -1)
                        {
                            Job.FindJobByName(jobName).L_toolList.Add(toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Add("", toolInfo.toolName, imageIndex + 19, imageIndex + 19);
                        }
                        else
                        {
                            Job.FindJobByName(jobName).L_toolList.Insert(insertIdx, toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Insert(insertIdx, "", toolName, imageIndex + 19, imageIndex + 19);
                        }
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "图像", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Image;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO("图像", "", DataType.Image));
                        }
                        //自动链接输入项
                        AutoConnectSource(jobName, itemNode);
                        //添加常用项 -点1
                        itemNode = toolNode.Nodes.Add("", "Point1", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.XY;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO("Point1", "", DataType.XY));
                        }
                        //自动链接输入项 线2
                        AutoConnectSource(jobName, itemNode);
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "Line1", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Line;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO("Line1", "", DataType.Line));
                        }
                        //自动链接输入项
                        AutoConnectSource(jobName, itemNode);
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "距离", 3, 3);
                        itemNode.ForeColor = Color.LightGreen;
                        itemNode.Tag = DataType.Double;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO("距离", "", DataType.Double));
                        }
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "判定结果", 3, 3);
                        itemNode.ForeColor = Color.LightGreen;
                        itemNode.Tag = DataType.Bool;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO("判定结果", "", DataType.Bool));
                        }
                        break;
                    #endregion
                    #region 4线与线夹角
                    case "线与线夹角":
                    case "LLAngle":
                        toolName = Job.FindJobByName(jobName).GetNewToolName("线与线夹角");
                        if (toolName == "TooMuch")       //此工具添加个数已达到上限100各，不让继续添加
                            return;

                        if (toolInfo1 == null)
                        {
                            LLAngleTool binarizationTool = new LLAngleTool();
                            toolInfo = new ToolInfo(ToolType.LLAngle, binarizationTool, jobName, toolName);
                        }
                        else
                        {
                            toolInfo1.toolName = toolName;
                            for (int i = 0; i < toolInfo1.input.Count; i++)
                            {
                                toolInfo1.input[i].value = string.Empty;
                            }
                        }

                        if (insertIdx == -1)
                        {
                            Job.FindJobByName(jobName).L_toolList.Add(toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Add("", toolInfo.toolName, imageIndex + 20, imageIndex + 20);
                        }
                        else
                        {
                            Job.FindJobByName(jobName).L_toolList.Insert(insertIdx, toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Insert(insertIdx, "", toolName, imageIndex + 20, imageIndex + 20);
                        }
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "图像", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Image;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO("图像", "", DataType.Image));
                        }
                        //自动链接输入项
                        AutoConnectSource(jobName, itemNode);
                        //添加常用项 -线1
                        itemNode = toolNode.Nodes.Add("", "Line1", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Line;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO("Line1", "", DataType.Line));
                        }
                        //自动链接输入项 线2
                        AutoConnectSource(jobName, itemNode);
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "Line2", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Line;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO("Line2", "", DataType.Line));
                        }
                        //自动链接输入项
                        AutoConnectSource(jobName, itemNode);
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "角度", 3, 3);
                        itemNode.ForeColor = Color.LightGreen;
                        itemNode.Tag = DataType.Double;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO("角度", "", DataType.Double));
                        }
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "判定结果", 3, 3);
                        itemNode.ForeColor = Color.LightGreen;
                        itemNode.Tag = DataType.Bool;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO("判定结果", "", DataType.Bool));
                        }
                        break;
                    #endregion
                    #region 5线与线相交
                    case "线与线相交":
                    case "LLIntersect":
                        toolName = Job.FindJobByName(jobName).GetNewToolName("线与线相交");
                        if (toolName == "TooMuch")       //此工具添加个数已达到上限100各，不让继续添加
                            return;
                        if (toolInfo1 == null)
                        {
                            LLIntersectTool binarizationTool = new LLIntersectTool();
                            toolInfo = new ToolInfo(ToolType.LLIntersect, binarizationTool, jobName, toolName);
                        }
                        else
                        {
                            toolInfo1.toolName = toolName;
                            for (int i = 0; i < toolInfo1.input.Count; i++)
                            {
                                toolInfo1.input[i].value = string.Empty;
                            }
                        }

                        if (insertIdx == -1)
                        {
                            Job.FindJobByName(jobName).L_toolList.Add(toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Add("", toolInfo.toolName, imageIndex + 21, imageIndex + 21);
                        }
                        else
                        {
                            Job.FindJobByName(jobName).L_toolList.Insert(insertIdx, toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Insert(insertIdx, "", toolName, imageIndex + 21, imageIndex + 21);
                        }
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "图像", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Image;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO("图像", "", DataType.Image));
                        }
                        //自动链接输入项
                        AutoConnectSource(jobName, itemNode);
                        //添加常用项 -线1
                        itemNode = toolNode.Nodes.Add("", "Line1", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Line;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO("Line1", "", DataType.Line));
                        }
                        //自动链接输入项 线2
                        AutoConnectSource(jobName, itemNode);
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "Line2", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Line;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO("Line2", "", DataType.Line));
                        }
                        //自动链接输入项
                        AutoConnectSource(jobName, itemNode);
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "交点", 3, 3);
                        itemNode.ForeColor = Color.LightGreen;
                        itemNode.Tag = DataType.XY;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO("交点", "", DataType.XY));
                        }
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "是否相交", 3, 3);
                        itemNode.ForeColor = Color.LightGreen;
                        itemNode.Tag = DataType.Bool;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO("是否相交", "", DataType.Bool));
                        }
                        break;
                    #endregion
                    #region 6线与圆相交
                    #endregion
                    #region 7圆与圆相交
                    #endregion
                    #region 8灰度比较工具
                    case "灰度比较工具":
                    case "RRegionGraySubtract":
                        toolName = Job.FindJobByName(jobName).GetNewToolName("灰度比较工具");
                        if (toolName == "TooMuch")       //此工具添加个数已达到上限100各，不让继续添加
                            return;
                        if (toolInfo1 == null)
                        {
                            RRegionGraySubtractTool rRegionGraySubtractTool = new RRegionGraySubtractTool();
                            toolInfo = new ToolInfo(ToolType.RRegionGraySubtract, rRegionGraySubtractTool, jobName, toolName);
                        }
                        else
                        {
                            toolInfo1.toolName = toolName;
                            for (int i = 0; i < toolInfo1.input.Count; i++)
                            {
                                toolInfo1.input[i].value = string.Empty;
                            }
                        }

                        if (insertIdx == -1)
                        {
                            Job.FindJobByName(jobName).L_toolList.Add(toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Add("", toolInfo.toolName, imageIndex + 24, imageIndex + 24);
                        }
                        else
                        {
                            Job.FindJobByName(jobName).L_toolList.Insert(insertIdx, toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Insert(insertIdx, "", toolName, imageIndex + 24, imageIndex + 24);
                        }
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "图像", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Image;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO("图像", "", DataType.Image));
                        }
                        //自动链接输入项
                        AutoConnectSource(jobName, itemNode);

                        itemNode = toolNode.Nodes.Add("", "跟随位置", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Pose;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO("跟随位置", "", DataType.Pose));
                        }
                        //自动链接输入项
                        AutoConnectSource(jobName, itemNode);

                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "灰度差值", 3, 3);
                        itemNode.ForeColor = Color.LightGreen;
                        itemNode.Tag = DataType.Double;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO("灰度差值", "", DataType.Double));
                        }
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "判定结果", 3, 3);
                        itemNode.ForeColor = Color.LightGreen;
                        itemNode.Tag = DataType.Bool;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO("判定结果", "", DataType.Bool));
                        }

                        break;
                    #endregion'
                    #endregion

                    #region 5OCR读码
                    #region OCR

                    #endregion
                    #region 读码

                    #endregion
                    #endregion

                    #region 6逻辑工具
                    case "数据判定":         ////0505添加
                    case "VerdictMeasure":
                        toolName = Job.FindJobByName(jobName).GetNewToolName("数据判定");
                        if (toolName == "TooMuch")       //此工具添加个数已达到上限100各，不让继续添加
                            return;

                        if (toolInfo1 == null)
                        {
                            VerdictTool verdictTool = new VerdictTool();
                            toolInfo = new ToolInfo(ToolType.VerdictMeasure, verdictTool, jobName, toolName);
                        }
                        else
                        {
                            toolInfo1.toolName = toolName;
                            for (int i = 0; i < toolInfo1.input.Count; i++)
                            {
                                toolInfo1.input[i].value = string.Empty;
                            }
                        }

                        if (insertIdx == -1)
                        {
                            Job.FindJobByName(jobName).L_toolList.Add(toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Add("", toolInfo.toolName, imageIndex + 27, imageIndex + 27);
                        }
                        else
                        {
                            Job.FindJobByName(jobName).L_toolList.Insert(insertIdx, toolInfo);
                            toolNode = Job.GetJobTreeStatic().Nodes.Insert(insertIdx, "", toolName, imageIndex + 27, imageIndex + 27);
                        }

                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "图像", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Image;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO("图像", "", DataType.Image));
                        }
                        //自动链接输入项
                        AutoConnectSource(jobName, itemNode);

                        //添加常用项 -量测值
                        itemNode = toolNode.Nodes.Add("", "量测值", 2, 2);
                        itemNode.ForeColor = Color.LightPink;
                        itemNode.Tag = DataType.Double;
                        if (toolInfo1 == null)
                        {
                            toolInfo.input.Add(new ToolIO("量测值", "", DataType.Double));
                        }
                        //自动链接输入项 
                        AutoConnectSource(jobName, itemNode);

                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "量测值", 3, 3);
                        itemNode.ForeColor = Color.LightGreen;
                        itemNode.Tag = DataType.Double;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO("量测值", "", DataType.Double));
                        }
                        //添加常用项
                        itemNode = toolNode.Nodes.Add("", "判定结果", 3, 3);
                        itemNode.ForeColor = Color.LightGreen;
                        itemNode.Tag = DataType.Bool;
                        if (toolInfo1 == null)
                        {
                            toolInfo.output.Add(new ToolIO("判定结果", "", DataType.Bool));
                        }
                        break;
                    #endregion

                    #region 7创建对象

                    #endregion

                    #region 8通讯工具

                    #endregion

                    default:
                        Win_MessageBox.Instance.MessageBoxShow("\r\n此工具开发中...");
                        return;
                }
                tvw_tools.SelectedNode = toolNode;
                toolNode.Expand();
                toolNode.EnsureVisible();
                toolNode.ToolTipText ="未运行";
                Job.GetJobTreeStatic().isDrawing = false;
                //Job.FindJobByName(jobName).DrawLine();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        //工具根节点
        string[] FunctionName = new string[] {"图像工具", "坐标变换", "引导定位","量测工具","OCR读码","逻辑工具","创建对象","通讯工具","第三方工具" };
        bool[] IsOK = new bool[9] { true, false, true, true, false, true, false, false, true };
       // bool[] IsOK = new bool[9] { true, true, true, true, true, true, true, true, true };
       //工具子节点
        string[][] ToolName = new string[][] 
        { 
            new string[] { "图像采集", "存储图像", "二值化" , "开闭运算" , "图像滤波", "图像分解", "查找区域1", "查找区域2", "缺陷检测"},
            new string[] { "标定板", "N点标定", "空间转换"},
            new string[] { "2维匹配", "斑点工具", "找边" , "找圆" , "找角点" },
            new string[] { "点到点距离", "点到圆距离", "点到线距离" , "线与线夹角", "线与线相交", "线与圆相交", "圆与圆相交", "灰度比较工具"},
            new string[] { "字符识别", "读码工具"},
            new string[] { "数据判定"},
            new string[] { ""},
            new string[] { ""},
            new string[] { ""},
            //new string[] { "VisionPro","HalconScript"},
        };
       
        private void Win_Tools_Load(object sender, EventArgs e)
        {
            try
            {
                int imageIndex = 4;
                for (int i = 0; i < FunctionName.Length; i++)
                {
                    //添加主节点
                    tvw_tools.Nodes.Add("", FunctionName[i], 0, 0);
                    //添加子节点
                    for (int j = 0; j < ToolName[i].Length; j++)
                    {
                        //hzy修改图标
                        tvw_tools.Nodes[i].Nodes.Add("", ToolName[i][j], imageIndex, imageIndex);
                        imageIndex++;
                    }
                   
                }

                //默认展开第一个图像相关节点
                this.tvw_tools.Nodes[0].Expand();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private void tvw_job_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                switch (tvw_tools.SelectedNode.Text)
                {
                    case "图像相关":
                    case "AcqDevice":
                        lbl_toolInfo.Text = ("说明：此类工具用于图像的获取和相关处理");
                        break;

                    case "图像采集":
                    case "SDK_HIKVision":
                        lbl_toolInfo.Text = ("说明：此工具用于对各品牌相机连接和采图");
                        break;

                    case "SDK_Halcon":
                    case "HalconAcqInterface":
                        lbl_toolInfo.Text = ("说明：此工具基于Halcon采集接口连接相机并采图");
                        break;

                    case "预处理":
                    case "SDK_HIKVisixxon":
                        lbl_toolInfo.Text = ("说明：此工具用于图像的预处理，可改善图像质量");
                        break;

                    case "彩图转RGB":
                    case "ChannelConvert":
                        lbl_toolInfo.Text = ("说明：此工具用于将彩色图像分解成RGB单通道图像");
                        break;

                    case "存储图像":
                    case "SDK_HIKxxxVisixxon":
                        lbl_toolInfo.Text = ("说明：此工具用于将图像存储到本地磁盘");
                        break;

                    case "检测识别":
                    case "Match":
                        lbl_toolInfo.Text = ("说明：此类工具用于对图像中的特征进行检测和识别");
                        break;
                    case "缺陷检测":
                    case "DefectDetection":
                        lbl_toolInfo.Text = ("说明：此工具通过轮廓识别查找缺陷");
                        break;
                    case "模板匹配":
                    case "ShapeMatch":
                        lbl_toolInfo.Text = ("说明：此工具为模板匹配工具");
                        break;

                    case "斑点分析":
                    case "BlobAnalyse":
                        lbl_toolInfo.Text = ("说明：此工具用于对图像中的斑点进行分析");
                        break;

                    case "图像相减":
                    case "SubImage":
                        lbl_toolInfo.Text = ("说明：此工具使用两图像相减的方式进行特征检测或识别");
                        break;

                    case "区域特征":
                    case "Iden显现显现tity":
                        lbl_toolInfo.Text = ("说明：此工具用于获取区域的相关特征，如圆度、面积等");
                        break;

                    case "条码识别":
                    case "Barcode":
                        lbl_toolInfo.Text = ("说明：此工具用于条码识别");
                        break;

                    case "二维码识别":
                    case "2DUncode":
                        lbl_toolInfo.Text = ("说明：此工具用于二维码识别");
                        break;

                    case "OCR":
                    case "OCRTool":
                        lbl_toolInfo.Text = ("说明：此工具用于字符的识别");
                        break;

                    case "OCV":
                    case "OCVTool":
                        lbl_toolInfo.Text = ("说明：此工具用于OCV验证");
                        break;

                    case "标定变换":
                    case "CoorTrans":
                        lbl_toolInfo.Text = ("说明：此类工具用于标定获取两个坐标系之间的关系及进行关系转化");
                        break;

                    case "手眼标定":
                    case "EyeHandCalibration":
                        lbl_toolInfo.Text = ("说明：此工具用于手动的标定图像坐标系和机械坐标系之间的关系");
                        break;

                    case "引用标定":
                    case "EyeHandCalibra香香tion":
                        lbl_toolInfo.Text = ("说明：此工具用于引用其它标定工具中的标定关系");
                        break;

                    case "一键手眼标定":
                    case "EyeHandCalibra信息tion":
                        lbl_toolInfo.Text = ("说明：此工具用于一键自动的完成图像坐标系和机械坐标系的标定");
                        break;

                    case "尺寸标定":
                    case "Calibration":
                        lbl_toolInfo.Text = ("说明：此工具用于尺寸标定，可通过标准原点或正方形等方式完成标定");
                        break;

                    case "一维标定":
                    case "Calibrati嘻嘻嘻on":
                        lbl_toolInfo.Text = ("说明：此工具用于标定出两条一维坐标系之间的平移和缩放关系");
                        break;

                    case "定位引导":
                    case "RobotAlign":
                        lbl_toolInfo.Text = ("说明：此类工具用于机械手的定位引导");
                        break;

                    case "上相机定位":
                    case "RobotUpCameraAlign":
                        lbl_toolInfo.Text = ("说明：此工具用于相机固定于产品上方，拍照定位引导机械手抓取的场景");
                        break;

                    case "下相机定位":
                    case "RobotDownCamAlign":
                        lbl_toolInfo.Text = ("说明：此工具用于相机固定于底部，机械手抓取产品后经此相机拍照定位的场景");
                        break;

                    case "点位引导":
                    case "RobotDoDw信谢谢息nCamAlign":
                        lbl_toolInfo.Text = ("说明：此工具应用于相机固定于机械手臂上，拍照定位引导机械手运动到指定点的场景");
                        break;

                    case "对位组装":
                    case "RobotDoDw信谢谢息nC谢谢amAlign":
                        lbl_toolInfo.Text = ("说明：此工具用于上下相机组合使用的对位组装或贴合类场景");
                        break;

                    case "旋转平台":
                    case "RobotDow信息nCamAlign":
                        lbl_toolInfo.Text = ("说明：此工具应用于相机固定于待定位产品上方，产品放置于旋转平台上，相机定位后，通过旋转平台和XY模组完成取料的场景");
                        break;

                    case "XY平台":
                    case "RobotDoDw信息nCamAlign":
                        lbl_toolInfo.Text = ( "说明：此工具应用于相机固定于待定位产品上方，产品放置于旋转平台上，相机定位后，通过旋转平台和XY模组完成取料的场景");
                        break;

                    case "逻辑控制":
                    case "ThresholdSegmenta嘻嘻嘻tion":
                        lbl_toolInfo.Text = ("说明：此类工具用于流程中的逻辑控制，如循环、分支等");
                        break;

                    case "循环":
                    case "ThresholdSeg信息menta嘻嘻嘻tion":
                        lbl_toolInfo.Text = ("说明：此工具用于循环执行");
                        break;

                    case "分支":
                    case "ThresholdS信息eg信息menta嘻嘻嘻tion":
                        lbl_toolInfo.Text = ("说明：此工具用于分支选择执行");
                        break;

                    case "查找拟合":
                    case "FindAndFit":
                        lbl_toolInfo.Text = ( "说明：此类工具用于查找和拟合");
                        break;

                    case "查找边":
                    case "FindLineTool":
                        lbl_toolInfo.Text = ("说明：此工具用于查找边");
                        break;

                    case "查找圆":
                    case "FindCircle":
                        lbl_toolInfo.Text = ("说明：此工具用于查找圆");
                        break;

                    case "拟合线":
                    case "FitLine":
                        lbl_toolInfo.Text = ("说明：此工具用于拟合直线");
                        break;

                    case "拟合圆":
                    case "FitCircle":
                        lbl_toolInfo.Text = ("说明：此工具用于拟合圆");
                        break;

                    case "创建组合":
                    case "FitCirc谢谢le":
                        lbl_toolInfo.Text = ("说明：此类工具用于创建或组合一些类型");
                        break;

                    case "创建ROI":
                    case "ImageConveXXXrt":
                        lbl_toolInfo.Text = ("说明：此工具用于创建一个较复杂的ROI区域");
                        break;

                    case "阵列区域":
                    case "ImageConve信息信息信息XXXrt":
                        lbl_toolInfo.Text = ("说明：此工具用于创建阵列网格式区域");
                        break;

                    case "标记点":
                    case "ImageConve信信息息信息信息XXXrt":
                        lbl_toolInfo.Text = ("说明：此工具用于在图像上指定位置显示一个标记点");
                        break;

                    case "组合位置":
                    case "ImageConve信息XXXrt":
                        lbl_toolInfo.Text = ("说明：此工具可由一个点和一个方向组合出一个XYU位置类型");
                        break;

                    case "组合线段":
                    case "ImageConve信息信息XXXrt":
                        lbl_toolInfo.Text = ("说明：此工具用于通过两个点组合出一条线段");
                        break;

                    case "转文本":
                    case "ImageConve信息xx XXXrt":
                        lbl_toolInfo.Text = ("说明：此工具用于将点或位置类型转化成文本类型");
                        break;

                    case "数据显示":
                    case "Label":
                        lbl_toolInfo.Text = ("说明：此工具用于在图像窗口上显示文本信息");
                        break;

                    case "几何相关":
                    case "Segm显现ent":
                        lbl_toolInfo.Text = ("说明：此类工具为几何类工具");
                        break;

                    case "点点距离":
                    case "DistancePP":
                        lbl_toolInfo.Text = ("说明：此工具用于计算点与点之间的距离");
                        break;

                    case "点线距离":
                    case "DistancePL":
                        lbl_toolInfo.Text = ("说明：此工具用于计算点与线段之间的距离");
                        break;

                    case "线线角度":
                    case "AngleLL":
                        lbl_toolInfo.Text = ("说明：此工具用于计算线段与线段之间的角度");
                        break;

                    case "线线距离":
                    case "DistanceLL":
                        lbl_toolInfo.Text = ("说明：此工具用于计算线段与线段之间的距离");
                        break;

                    case "线线交点":
                    case "Segment":
                        lbl_toolInfo.Text = ( "说明：此工具用于计算线段与线段的交点");
                        break;

                    case "两点中点":
                    case "Segm嘻嘻嘻ent":
                        lbl_toolInfo.Text = ( "说明：此工具用于计算两点的中点");
                        break;

                    case "运算":
                    case "Operation":
                        lbl_toolInfo.Text = ("说明：此类工具用于运算处理");
                        break;

                    case "算术":
                    case "Arithemtic":
                        lbl_toolInfo.Text = ("说明：此工具用于数学算术运算");
                        break;

                    case "脚本编辑":
                    case "CodeEdit":
                        lbl_toolInfo.Text = ("说明：此工具用于编辑CSharp脚本");
                        break;

                    case "仪器仪表":
                    case "Iden谢谢显现tity":
                        lbl_toolInfo.Text = ("说明：此类工具用于控制市场上常用的第三方仪表，如扫码枪、光源控制器等");
                        break;

                    case "光源_奥普特":
                    case "Light_OPT":
                        lbl_toolInfo.Text = ("说明：此工具用于奥普特光源控制器");
                        break;

                    case "光源_康视达":
                    case "Light_LOTS":
                        lbl_toolInfo.Text = ("说明：此工具用于康视达光源控制器");
                        break;

                    case "光源_乐视":
                    case "Light_CST":
                        lbl_toolInfo.Text = ("说明：此工具用于乐视光源控制器");
                        break;

                    case "光源控制":
                    case "Ligh显现t_LOTS":
                        lbl_toolInfo.Text = ("说明：此工具可以对光源进行控制，如控制光源的开关、亮度等");
                        break;

                    case "扫码器_基恩士":
                    case "Ligh显现得到显现t_LOTS":
                        lbl_toolInfo.Text = ("说明：此工具用于对基恩士扫码器进行控制，如SR700、SR1000等");
                        break;

                    case "通讯相关":
                    case "Ligh显现得到显现t_LO显现TS":
                        lbl_toolInfo.Text = ("说明：此类工具用于本软件和外部设备之间的通讯");
                        break;

                    case "PLC通讯":
                    case "Ligh显现得到显信息现t_LO显现TS":
                        lbl_toolInfo.Text = ("说明：此工具用于和各品牌PLC的通讯，如寄存器读写、Socket收发等");
                        break;

                    case "3D  检测":
                        lbl_toolInfo.Text = ("说明：此类工具用于3D相关检测或测量");
                        break;

                    case "其它相关":
                    case "Ligh显现得到显显现现t_LO显现TS":
                        lbl_toolInfo.Text = ("说明：其它相关工具");
                        break;

                    case "输出项":
                    case "Output":
                        lbl_toolInfo.Text = ("说明：此工具用于将相关项添加到输出集合，用于外部调用或获取");
                        break;

                    default:
                        lbl_toolInfo.Text = ("说明：未知");
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private void tvw_job_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Developer))
                    return;
                if (tvw_tools.SelectedNode.SelectedImageIndex <2)         //如果双击的是文件夹节点，返回
                    return;
                if (Project.Instance.curEngine.L_jobList.Count > 0)        //如果流程存在
                    AddTool(tvw_tools.SelectedNode.Text, null);
                else           //如果当前不存在可用流程，先创建流程，再添加工具
                    Job.CreateJob();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private void 折叠所有ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tvw_tools.CollapseAll();
        }
        private void 展开所有ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
               // tvw_tools.ExpandAll();
              
                for (int i = 0; i < IsOK.Length; i++)
                {
                    if (IsOK[i])
                    {
                        tvw_tools.Nodes[i].Expand();
                    }
                   
                }
                tvw_tools.SelectedNode = tvw_tools.Nodes[0].Nodes[0];
                tvw_tools.AutoScrollOffset = new System.Drawing.Point(0, 0);
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private void tvw_tools_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                TreeNode tn = tvw_tools.GetNodeAt(e.X, e.Y);
                if (tn != null)
                    tvw_tools.SelectedNode = tn;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private void Win_Tools_DockStateChanged(object sender, EventArgs e)
        {
            if (Win_ToolBox.Instance.DockState != DockState.Unknown)
                lastDockState = Win_ToolBox.Instance.DockState;
        }
        private void Win_Tools_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        private void tvw_tools_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (sender != null && sender is TreeView)
                {
                    TreeView trv = sender as TreeView;
                    if (trv.Tag != null)
                    {
                        MoveTreeView move = (MoveTreeView)Convert.ToInt32(trv.Tag);
                        if (move == MoveTo) { DragNode = null; NodeSource = null; }
                        else
                        {
                            System.Drawing.Point point = trv.PointToClient(new System.Drawing.Point(e.X, e.Y));
                            TreeNode node = trv.GetNodeAt(point);
                            node.Nodes.Add(DragNode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private void tvw_tools_ItemDrag(object sender, ItemDragEventArgs e)
        {
            try
            {
                if (tvw_tools.SelectedNode.Level == 0)          //工具箱层级不应该被拖动，直接返回
                    return;

                if (e.Item is TreeNode && e.Button == System.Windows.Forms.MouseButtons.Left &&
                  e.Item != null && sender is TreeView)
                {
                    TreeView trv = sender as TreeView;
                    TreeNode node = e.Item as TreeNode;
                    int value = Convert.ToInt32(trv.Tag);
                    MoveTo = (MoveTreeView)value;
                    DragNode = node;
                    NodeSource = trv;
                    trv.DoDragDrop(node, DragDropEffects.Move);
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private void tvw_tools_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

  
        private void tvw_tools_AfterExpand(object sender, TreeViewEventArgs e)
        {
            e.Node.ImageIndex = 1;
            e.Node.SelectedImageIndex = 1;
            
        }

        private void tvw_tools_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            e.Node.ImageIndex = 0;
            e.Node.SelectedImageIndex = 0;
            
        }
        private void tvw_tools_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level==0)
            {
                if (IsOK[e.Node.Index])
                {

                    if (e.Node.IsExpanded)
                    {
                        e.Node.Collapse();
                    }
                    else
                    {
                        e.Node.Expand();
                    }
                }
                else
                {
                    Win_MessageBox.Instance.MessageBoxShow("当前工具集未开放！");
                }

            }
        }

        private void tvw_tools_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (IsOK[e.Node.Index])
            {

                if (e.Node.IsExpanded)
                {
                    e.Node.Collapse();
                }
                else
                {
                    e.Node.Expand();
                }
            }
            else
            {
                Win_MessageBox.Instance.MessageBoxShow("当前工具集未开放！");
            }

        }
    }
    /// <summary>
    /// 属性节点移动方向
    /// </summary>
    public enum MoveTreeView
    {
        /// <summary>
        /// 未移动
        /// </summary>
        NoMove = -1,
        /// <summary>
        /// 上传（客户端拖拽到服务器端）
        /// </summary>
        ClientToServer = 0,
        /// <summary>
        /// 下载（服务器端拖拽到客户端）
        /// </summary>
        ServerToClient = 1
    }
}
