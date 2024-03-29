using System;
using System.Collections.Generic;
using System.Drawing;
using VControls;
using VSE.Core;
using HalconDotNet;
using Lxc.VisionPlus.ImageView.Model;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace VSE
{
    public partial class Win_RRegionGraySubtractTool : ToolWinBase
    {
        public Win_RRegionGraySubtractTool()
        {
            InitializeComponent();

            vLinkImage1.CallBackNodes += vLinkImage1_CallBackNodes;
            vLinkImage1.ImageChange += vLinkImage1_ImageChange;

            vLinkPose1.CallBackNodes += vLinkPose1_CallBackNodes;
            vLinkPose1.PoseChange += vLinkPose1_PoseChange;

            vroiSet1.H = PImageWin;
            vroiSet1.ROIChangedEvent += LxcROISet1_ROISetChangedEvent;

            vroiSet2.H = PImageWin;
            vroiSet2.ROIChangedEvent += LxcROISet2_ROISetChangedEvent;

            PImageWin.viewWindow._hWndControl.ROIChangedEvent += _hWndControl_ROIChangedEvent;
        }

        internal static RRegionGraySubtractTool RRegionGraySubtractTool = new RRegionGraySubtractTool();
        internal List<ROI> regions = new List<ROI>();

        private static Win_RRegionGraySubtractTool _instance;
        public static Win_RRegionGraySubtractTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_RRegionGraySubtractTool();
                return _instance;
            }
        }

        private void vLinkImage1_CallBackNodes(object sender, EventArgs e)
        {
            vLinkImage1.Nodes = Job.GetJobTreeStatic().Nodes;
            vLinkImage1.NoUseImage = toolName;
        }

        private void vLinkImage1_ImageChange(object sender, EventArgs e)
        {
            RRegionGraySubtractTool.toolPar.InputPar.图像 = Job.FindJobByName(jobName).FindToolInfoByName(vLinkImage1.ImageText.Split('.')[0]).GetOutput(vLinkImage1.ImageText.Split('.')[1]).value as HImage;
            RRegionGraySubtractTool.toolPar.RunPar.图像 = RRegionGraySubtractTool.toolPar.InputPar.图像;
            PImageWin.Image = RRegionGraySubtractTool.toolPar.InputPar.图像;
            Job.FindJobByName(jobName).ConnectSource(toolName, vLinkImage1.ImageText, Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("图像").value.ToString());
        }

        private void vLinkPose1_CallBackNodes(object sender, EventArgs e)
        {
            vLinkPose1.Nodes = Job.GetJobTreeStatic().Nodes;
            vLinkPose1.NoUseROI = toolName;
        }

        private void vLinkPose1_PoseChange(object sender, EventArgs e)
        {
            if (vLinkPose1.ROIText != "无")
            {
                RRegionGraySubtractTool.toolPar.InputPar.Pose = Job.FindJobByName(jobName).FindToolInfoByName(vLinkPose1.ROIText.Split('.')[0]).GetOutput(vLinkPose1.ROIText.Split('.')[1]).value as List<XYU>;
                if (RRegionGraySubtractTool.toolPar.InputPar.Pose != null && RRegionGraySubtractTool.toolPar.InputPar.Pose.Count > 0)
                {
                    RRegionGraySubtractTool.toolPar.RunPar.StandardPose = RRegionGraySubtractTool.toolPar.InputPar.Pose[0];
                    RRegionGraySubtractTool.toolPar.RunPar.基准区域1 = RRegionGraySubtractTool.toolPar.ResultPar.搜索区域1;
                    RRegionGraySubtractTool.toolPar.RunPar.基准区域2 = RRegionGraySubtractTool.toolPar.ResultPar.搜索区域2;
                }
                else
                {
                    RRegionGraySubtractTool.toolPar.RunPar.StandardPose = new XYU();
                    vroiSet1.ROIFitImage();
                    vroiSet2.ROIFitImage();
                }
                Job.FindJobByName(jobName).ConnectSource(toolName, vLinkPose1.ROIText, Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("跟随位置").value.ToString(), "跟随位置");
            }
            else
            {
                Job.FindJobByName(jobName).DisSource(toolName, Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("跟随位置").value.ToString(), "跟随位置");
            }
        }

        private void LxcROISet1_ROISetChangedEvent(object sender, VControls.VROISet.LxcROISetChangedEventArgs e)
        {
            RRegionGraySubtractTool.toolPar.RunPar.基准区域1 = e.ROI;
        }

        private void LxcROISet2_ROISetChangedEvent(object sender, VControls.VROISet.LxcROISetChangedEventArgs e)
        {
            RRegionGraySubtractTool.toolPar.RunPar.基准区域2 = e.ROI;
        }

        private void _hWndControl_ROIChangedEvent(object sender, EventArgs e)
        {
            PImageWin.viewWindow.ClearWindow();
            PImageWin.Image = RRegionGraySubtractTool.toolPar.InputPar.图像;  
            if (RRegionGraySubtractTool.toolPar.InputPar.Pose != null && RRegionGraySubtractTool.toolPar.InputPar.Pose.Count > 0)
            {
                RRegionGraySubtractTool.toolPar.RunPar.StandardPose = RRegionGraySubtractTool.toolPar.InputPar.Pose[0];
            }
            else
            {
                RRegionGraySubtractTool.toolPar.RunPar.StandardPose = new XYU();
            }

            if (RRegionGraySubtractTool.toolPar.RunPar.基准区域1 != null && RRegionGraySubtractTool.toolPar.RunPar.基准区域2 == null)
            {
                if (PImageWin.viewWindow.RoiController.ROIList.Count == 0)
                {
                    PImageWin.viewWindow.RoiController.ROIList.Add(RRegionGraySubtractTool.toolPar.RunPar.基准区域1);
                }
                else if (PImageWin.viewWindow.RoiController.ROIList.Count == 1)
                {
                    PImageWin.viewWindow.RoiController.ROIList[0] = RRegionGraySubtractTool.toolPar.RunPar.基准区域1;
                }
            }

            if (RRegionGraySubtractTool.toolPar.RunPar.基准区域1 == null && RRegionGraySubtractTool.toolPar.RunPar.基准区域2 != null)
            {
                if (PImageWin.viewWindow.RoiController.ROIList.Count == 0)
                {
                    PImageWin.viewWindow.RoiController.ROIList.Add(RRegionGraySubtractTool.toolPar.RunPar.基准区域2);
                }
                else if (PImageWin.viewWindow.RoiController.ROIList.Count == 1)
                {
                    PImageWin.viewWindow.RoiController.ROIList[0] = RRegionGraySubtractTool.toolPar.RunPar.基准区域2;
                }
            }

            if (RRegionGraySubtractTool.toolPar.RunPar.基准区域1 != null && RRegionGraySubtractTool.toolPar.RunPar.基准区域2 != null)
            {
                if (PImageWin.viewWindow.RoiController.ROIList.Count != 2)
                {
                    PImageWin.viewWindow.RoiController.ROIList.Clear();
                    PImageWin.viewWindow.RoiController.ROIList.Add(RRegionGraySubtractTool.toolPar.RunPar.基准区域1);
                    PImageWin.viewWindow.RoiController.ROIList.Add(RRegionGraySubtractTool.toolPar.RunPar.基准区域2);
                }
                else if (PImageWin.viewWindow.RoiController.ROIList.Count == 2)
                {
                    PImageWin.viewWindow.RoiController.ROIList[0] = RRegionGraySubtractTool.toolPar.RunPar.基准区域1;
                    PImageWin.viewWindow.RoiController.ROIList[1] = RRegionGraySubtractTool.toolPar.RunPar.基准区域2;
                }
            }

            if (RRegionGraySubtractTool.toolPar.RunPar.基准区域1 != null || RRegionGraySubtractTool.toolPar.RunPar.基准区域2 != null)
            {
                PImageWin.viewWindow._hWndControl.repaint();
            }
        }

        private void Win_RRegionGraySubtractTool_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                lxcCheckBox_1大于2.Checked = RRegionGraySubtractTool.toolPar.RunPar.Gray1大于Cray2;
                lxcCheckBox_1小于2.Checked = RRegionGraySubtractTool.toolPar.RunPar.Gray1小于Cray2;
                lxcCheckBox_displayNGRegion.Checked = RRegionGraySubtractTool.toolPar.RunPar.displayNGRegion;
                lxcCheckBox_DisplaySearchRegion.Checked = RRegionGraySubtractTool.toolPar.RunPar.displayROIRegion;
                lxcCheckBox_displayGoldenRegion.Checked = RRegionGraySubtractTool.toolPar.RunPar.displayGoldenROIRegion;
                lxcCheckBox_regionDrawMode.Checked = RRegionGraySubtractTool.toolPar.RunPar.regionDrawMode == VSE.Core.FillMode.Margin ? false : true;
                vroiSet1.mROI = RRegionGraySubtractTool.toolPar.ResultPar.搜索区域1;
                vroiSet2.mROI = RRegionGraySubtractTool.toolPar.ResultPar.搜索区域2;
                lxcNumEdit_GrayDiffLimit.Value = Convert.ToDecimal(RRegionGraySubtractTool.toolPar.RunPar.RegionGrayLimit);
                vTextBox_GrayVal1.TextStr = RRegionGraySubtractTool.toolPar.ResultPar.GrayVal1.ToString("f3");
                vTextBox_GrayVal2.TextStr = RRegionGraySubtractTool.toolPar.ResultPar.GrayVal2.ToString("f3");
                vTextBox_GrayDiffVal.TextStr = RRegionGraySubtractTool.toolPar.ResultPar.GrayDiff.ToString("f3");
                if (RRegionGraySubtractTool.toolPar.ResultPar.Consequence)
                {
                    vTextBox4.TextStr = "OK";
                }
                else
                {
                    vTextBox4.TextStr = "NG";
                }

                if (RRegionGraySubtractTool.toolPar.InputPar.Pose != null && RRegionGraySubtractTool.toolPar.InputPar.Pose.Count > 0)
                {
                    RRegionGraySubtractTool.toolPar.RunPar.StandardPose = RRegionGraySubtractTool.toolPar.InputPar.Pose[0];
                    RRegionGraySubtractTool.toolPar.RunPar.基准区域1 = RRegionGraySubtractTool.toolPar.ResultPar.搜索区域1;
                    RRegionGraySubtractTool.toolPar.RunPar.基准区域2 = RRegionGraySubtractTool.toolPar.ResultPar.搜索区域2;
                }
                else
                {
                    RRegionGraySubtractTool.toolPar.RunPar.StandardPose = new XYU();
                    vroiSet1.ROIFitImage();
                    vroiSet2.ROIFitImage();
                }
                _hWndControl_ROIChangedEvent(null, null);
            }
        }

        private void Pbtn_runTool_Click(object sender, EventArgs e)
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                RRegionGraySubtractTool.Run(true, true, toolName);
                long time = sw.ElapsedMilliseconds;
                if (RRegionGraySubtractTool.toolRunStatu != (ToolRunStatu.成功))
                {
                    Plbl_toolTip.ForeColor = Color.Red;
                    Plbl_runTime.Text = string.Format("耗时：0ms");
                    Plbl_toolTip.Text = "状态：" + RRegionGraySubtractTool.toolRunStatu.ToString();
                }
                else
                {
                    Plbl_toolTip.ForeColor = Color.White;
                    Plbl_runTime.Text = string.Format("耗时：{0}ms", time.ToString());

                    Plbl_toolTip.Text = "状态：" + RRegionGraySubtractTool.toolRunStatu.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        private void Pbtn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Pbtn_help_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Application.StartupPath + "\\HelpDoc\\区域灰度比较工具.pdf");
            }
            catch
            {
                Win_MessageBox.Instance.MessageBoxShow("\r\n工具暂无工具使用说明文档！", TipType.Warn);
            }
        }

        private void lxcNumEdit_MAX_OnValueChanged(object sender, EventArgs e)
        {
            RRegionGraySubtractTool.toolPar.RunPar.RegionGrayLimit = (double)lxcNumEdit_GrayDiffLimit.Value;
        }

        private void lxcCheckBox_1大于2_Click(object sender, EventArgs e)
        {
            RRegionGraySubtractTool.toolPar.RunPar.Gray1大于Cray2 = lxcCheckBox_1大于2.Checked;
            lxcCheckBox_1小于2.Checked = false;
            RRegionGraySubtractTool.toolPar.RunPar.Gray1小于Cray2 = false;
        }

        private void lxcCheckBox_1小于2_Click(object sender, EventArgs e)
        {
            RRegionGraySubtractTool.toolPar.RunPar.Gray1小于Cray2 = lxcCheckBox_1小于2.Checked;
            lxcCheckBox_1大于2.Checked = false;
            RRegionGraySubtractTool.toolPar.RunPar.Gray1大于Cray2 = false;
        }
        private void lxcCheckBox_displayGoldenRegion_Click(object sender, EventArgs e)
        {
            RRegionGraySubtractTool.toolPar.RunPar.displayGoldenROIRegion= lxcCheckBox_displayGoldenRegion.Checked;    
        }

        private void lxcCheckBox_DisplaySearchRegion_Click(object sender, EventArgs e)
        {
            RRegionGraySubtractTool.toolPar.RunPar.displayROIRegion = lxcCheckBox_DisplaySearchRegion.Checked;
        }

        private void lxcCheckBox_displayNGRegion_Click(object sender, EventArgs e)
        {
            RRegionGraySubtractTool.toolPar.RunPar.displayNGRegion = lxcCheckBox_displayNGRegion.Checked;
        }

        private void lxcCheckBox_regionDrawMode_Click(object sender, EventArgs e)
        {
            if (!lxcCheckBox_regionDrawMode.Checked)
            {
                RRegionGraySubtractTool.toolPar.RunPar.regionDrawMode = FillMode.Margin;
            }
            else
            {
                RRegionGraySubtractTool.toolPar.RunPar.regionDrawMode = FillMode.Fill;
            }
        }

    }
}
