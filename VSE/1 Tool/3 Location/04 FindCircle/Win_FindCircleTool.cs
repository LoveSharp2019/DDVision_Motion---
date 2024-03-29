using HalconDotNet;
using Lxc.VisionPlus.ImageView.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using VControls;
using VSE.Core;

namespace VSE
{
    public partial class Win_FindCircleTool : ToolWinBase
    {
        public Win_FindCircleTool()
        {
            InitializeComponent();
   
            vLinkImage1.CallBackNodes += VLinkImage1_CallBackNodes;
            vLinkImage1.ImageChange += VLinkImage1_ImageChange;

            vLinkPose1.CallBackNodes += VLinkPose1_CallBackNodes;
            vLinkPose1.PoseChange += VLinkPose1_PoseChange;

            PImageWin.viewWindow._hWndControl.ROIChangedEvent += _hWndControl_ROIChangedEvent;

            vCaliperSet1.CaliperCountChange += VCaliperSet1_CaliperCountChange;
            vCaliperSet1.CaliperLengthChange += VCaliperSet1_CaliperLengthChange;
            vCaliperSet1.CaliperWidthChange += VCaliperSet1_CaliperWidthChange;
            vCaliperSet1.CaliperThresholdChange += VCaliperSet1_CaliperThresholdChange;
        }

        internal static FindCircleTool FindCircleTool = new FindCircleTool();
        ROICalipersCircle roiTemp = new ROICalipersCircle(0, 0);
        private static Win_FindCircleTool _instance;
        internal static Win_FindCircleTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_FindCircleTool();
                return _instance;
            }
        }

        private void VLinkImage1_CallBackNodes(object sender, EventArgs e)
        {
            vLinkImage1.Nodes = Job.GetJobTreeStatic().Nodes;
            vLinkImage1.NoUseImage = toolName;
        }
        private void VLinkImage1_ImageChange(object sender, EventArgs e)
        {
            FindCircleTool.toolPar.InputPar.InPuImage = Job.FindJobByName(jobName).FindToolInfoByName(vLinkImage1.ImageText.Split('.')[0]).GetOutput(vLinkImage1.ImageText.Split('.')[1]).value as HImage;
            PImageWin.Image = FindCircleTool.toolPar.InputPar.InPuImage;
            Job.FindJobByName(jobName).ConnectSource(toolName, vLinkImage1.ImageText, Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("图像").value.ToString());
        }

        private void VLinkPose1_CallBackNodes(object sender, EventArgs e)
        {
            vLinkPose1.Nodes = Job.GetJobTreeStatic().Nodes;
            vLinkPose1.NoUseROI = toolName;
        }

        private void VLinkPose1_PoseChange(object sender, EventArgs e)
        {
            if (vLinkPose1.ROIText != "无")
            {
                FindCircleTool.toolPar.InputPar.Pose = Job.FindJobByName(jobName).FindToolInfoByName(vLinkPose1.ROIText.Split('.')[0]).GetOutput(vLinkPose1.ROIText.Split('.')[1]).value as List<XYU>;
                Job.FindJobByName(jobName).ConnectSource(toolName, vLinkPose1.ROIText, Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("跟随位置").value.ToString(), "跟随位置");
            }
            else
            {
                Job.FindJobByName(jobName).DisSource(toolName, Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("跟随位置").value.ToString(), "跟随位置");
            }
        }

        private void _hWndControl_ROIChangedEvent(object sender, EventArgs e)
        {
            if (FindCircleTool.toolPar.InputPar.ROIFindCircle != null)
            {
                if (PImageWin.viewWindow.RoiController.ROIList.Count==0)
                {
                    PImageWin.viewWindow.RoiController.ROIList.Add(FindCircleTool.toolPar.InputPar.ROIFindCircle);
                }
                else
                {
                    PImageWin.viewWindow.RoiController.ROIList[0] = FindCircleTool.toolPar.InputPar.ROIFindCircle;
                }
               
                if (FindCircleTool.toolPar.InputPar.Pose != null && FindCircleTool.toolPar.InputPar.Pose.Count > 0)
                {
                    FindCircleTool.toolPar.InputPar.StandardPose = FindCircleTool.toolPar.InputPar.Pose[0];
                }
                else
                {
                    FindCircleTool.toolPar.InputPar.StandardPose = new XYU();
                }
                vNumericUpDown4.Value = FindCircleTool.toolPar.InputPar.ROIFindCircle.StartRad * 180 / Math.PI;
                vNumericUpDown1.Value = FindCircleTool.toolPar.InputPar.ROIFindCircle.RangeRad * 180 / Math.PI;

                PImageWin.viewWindow._hWndControl.repaint();
            }
        }

        private void Win_FindCircleTool_VisibleChanged(object sender, System.EventArgs e)
        {
            if (this.Visible)
            {
                tabControl1.SelectedTab = tabPage1;
                vCaliperSet1.CaliperCount = FindCircleTool.toolPar.InputPar.CaliperCount;
                vCaliperSet1.CaliperLength = FindCircleTool.toolPar.InputPar.CaliperLength;
                vCaliperSet1.CaliperWidth = FindCircleTool.toolPar.InputPar.CaliperWidth;
                vCaliperSet1.CaliperThreshold = FindCircleTool.toolPar.InputPar.CaliperThreshold;
                if (FindCircleTool.toolPar.InputPar.InPuImage!=null)
                {
                    if (PImageWin.viewWindow.RoiController.ROIList.Count == 0)
                    {
                        HTuple w, h;
                        FindCircleTool.toolPar.InputPar.InPuImage.GetImageSize(out w, out h);
                        PImageWin.viewWindow.RoiController.ROIList.Add(new ROICalipersCircle(h / 2, w / 2));
                      
                    }
                    FindCircleTool.UpDateROI();
                    PImageWin.viewWindow.RoiController.ROIList[0]= FindCircleTool.toolPar.InputPar.ROIFindCircle;
                    PImageWin.viewWindow._hWndControl.repaint();

                }
                checkBox_displayPoints.Checked= FindCircleTool.toolPar.RunPar.ShowPoint;
                checkBox_displayCalipers.Checked = FindCircleTool.toolPar.RunPar.ShowCalipers;
                checkBox_displayCircleCenter.Checked = FindCircleTool.toolPar.RunPar.ShowCircleCenter;
                checkBox_displayCircle.Checked = FindCircleTool.toolPar.RunPar.ShowCircleCenter;
                if (FindCircleTool.toolPar.InputPar.SerachDir == FindCircleTool.EdgePolarity.positive)
                {
                    lxcRadioButton_BlackToWhite.Checked=true;
                }
                else if (FindCircleTool.toolPar.InputPar.SerachDir == FindCircleTool.EdgePolarity.negative)
                {
                    lxcRadioButton_WhiteToBlack.Checked = true;
                }
                else
                {
                    lxcRadioButton_All.Checked = true;
                }
               
            }
        }

        #region 卡尺设定事件
        private void VCaliperSet1_CaliperCountChange(double value)
        {
            if (value < 3)
            {
                value = 3;
                vCaliperSet1.CaliperCount = 3;
            }
            FindCircleTool.toolPar.InputPar.CaliperCount = (int)value;
            FindCircleTool.toolPar.InputPar.ROIFindCircle.CaliperCount = FindCircleTool.toolPar.InputPar.CaliperCount;
            if (PImageWin.viewWindow.RoiController.ROIList.Count == 0)
            {
                PImageWin.viewWindow.RoiController.ROIList.Add(FindCircleTool.toolPar.InputPar.ROIFindCircle);
            }
            else
            {
                PImageWin.viewWindow.RoiController.ROIList[0] = FindCircleTool.toolPar.InputPar.ROIFindCircle;
            }

            PImageWin.viewWindow._hWndControl.repaint();
        }

        private void VCaliperSet1_CaliperLengthChange(double value)
        {
            FindCircleTool.toolPar.InputPar.CaliperLength = (int)value;
            FindCircleTool.toolPar.InputPar.ROIFindCircle.SearchLength = FindCircleTool.toolPar.InputPar.CaliperLength;
            if (PImageWin.viewWindow.RoiController.ROIList.Count == 0)
            {
                PImageWin.viewWindow.RoiController.ROIList.Add(FindCircleTool.toolPar.InputPar.ROIFindCircle);
            }
            else
            {
                PImageWin.viewWindow.RoiController.ROIList[0] = FindCircleTool.toolPar.InputPar.ROIFindCircle;
            }
            PImageWin.viewWindow._hWndControl.repaint();
        }

        private void VCaliperSet1_CaliperWidthChange(double value)
        {
            FindCircleTool.toolPar.InputPar.CaliperWidth = (int)value;
            FindCircleTool.toolPar.InputPar.ROIFindCircle.ProjectionLength = FindCircleTool.toolPar.InputPar.CaliperWidth;
            if (PImageWin.viewWindow.RoiController.ROIList.Count == 0)
            {
                PImageWin.viewWindow.RoiController.ROIList.Add(FindCircleTool.toolPar.InputPar.ROIFindCircle);
            }
            else
            {
                PImageWin.viewWindow.RoiController.ROIList[0] = FindCircleTool.toolPar.InputPar.ROIFindCircle;
            }
            PImageWin.viewWindow._hWndControl.repaint();
        }

        private void VCaliperSet1_CaliperThresholdChange(double value)
        {
            FindCircleTool.toolPar.InputPar.CaliperThreshold = (int)value;
        }

        #endregion

        private void Pbtn_runTool_Click(object sender, System.EventArgs e)
        {
            Pbtn_runTool.Enabled = false;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            FindCircleTool.Run(true, true, toolName);
            long elapsedTime = sw.ElapsedMilliseconds;
            dgv_Result.Rows.Clear();
            if (FindCircleTool.toolRunStatu != (ToolRunStatu.成功))
            {
                vTextBox1.TextStr = "NaN";
                vTextBox2.TextStr = "NaN";
                vTextBox3.TextStr = "NaN";
                Plbl_runTime.ForeColor = Color.Red;
                Plbl_runTime.Text = string.Format("耗时：0ms");
            }
            else
            {
                int c = FindCircleTool.toolPar.Results.PointCounts;
               // CCount.Text = c.ToString() + " 结果";
                for (int i = 0; i < c; i++)
                {
                    dgv_Result.Rows.Add();
                    dgv_Result.Rows[i].Cells[0].Value = i+1;
                    dgv_Result.Rows[i].Cells[1].Value = FindCircleTool.toolPar.Results.row.DArr[i].ToString("f3");
                    dgv_Result.Rows[i].Cells[2].Value = FindCircleTool.toolPar.Results.col.DArr[i].ToString("f3");

                }

                vTextBox1.TextStr = FindCircleTool.toolPar.Results.Circle.圆心.X.ToString("f3");
                vTextBox2.TextStr = FindCircleTool.toolPar.Results.Circle.圆心.Y.ToString("f3");
                vTextBox3.TextStr = FindCircleTool.toolPar.Results.Circle.半径.ToString("f3");
                
                Plbl_runTime.ForeColor = Color.FromArgb(244, 244, 244);
                Plbl_runTime.Text = string.Format("耗时：{0}ms", elapsedTime.ToString());
            }
            Plbl_toolTip.Text = "状态：" + FindCircleTool.toolRunStatu.ToString();

            Pbtn_runTool.Enabled = true;
        }

        private void Pbtn_close_Click(object sender, System.EventArgs e)
        {
            this.Close();   
        }

        private void Pbtn_help_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Application.StartupPath + "\\HelpDoc\\抓圆工具.pdf");
            }
            catch
            {
                Win_MessageBox.Instance.MessageBoxShow("\r\n工具暂无工具使用说明文档！", TipType.Warn);
            }
        }

        private void checkBox_displayPoints_Click(object sender, System.EventArgs e)
        {
            FindCircleTool.toolPar.RunPar.ShowPoint = checkBox_displayPoints.Checked;
        }

        private void checkBox_displayCalipers_Click(object sender, System.EventArgs e)
        {
            FindCircleTool.toolPar.RunPar.ShowCalipers = checkBox_displayCalipers.Checked;
        }

        private void checkBox_displayCircleCenter_Click(object sender, System.EventArgs e)
        {
            FindCircleTool.toolPar.RunPar.ShowCircleCenter = checkBox_displayCircleCenter.Checked;
        }

        private void checkBox_displayCircle_Click(object sender, EventArgs e)
        {
            FindCircleTool.toolPar.RunPar.ShowCircle = checkBox_displayCircle.Checked;    
        }

        private void lxcRadioButton4_Click(object sender, System.EventArgs e)
        {
            if (lxcRadioButton_BlackToWhite.Checked)
            {
                FindCircleTool.toolPar.InputPar.SerachDir = FindCircleTool.EdgePolarity.positive;
            }
            else if (lxcRadioButton_WhiteToBlack.Checked)
            {
                FindCircleTool.toolPar.InputPar.SerachDir = FindCircleTool.EdgePolarity.negative;
            }
            else
            {
                FindCircleTool.toolPar.InputPar.SerachDir = FindCircleTool.EdgePolarity.all;
            }
        }

        private void vNumericUpDown4_ValueChanged(double value)
        {
            FindCircleTool.toolPar.InputPar.ROIFindCircle.StartRad = value * Math.PI / 180;
            if (PImageWin.viewWindow.RoiController.ROIList.Count == 0)
            {
                PImageWin.viewWindow.RoiController.ROIList.Add(FindCircleTool.toolPar.InputPar.ROIFindCircle);
            }
            else
            {
                PImageWin.viewWindow.RoiController.ROIList[0] = FindCircleTool.toolPar.InputPar.ROIFindCircle;
            }
            PImageWin.viewWindow._hWndControl.repaint();

        }

        private void vNumericUpDown1_ValueChanged(double value)
        {
            FindCircleTool.toolPar.InputPar.ROIFindCircle.RangeRad = value * Math.PI / 180;
            if (PImageWin.viewWindow.RoiController.ROIList.Count == 0)
            {
                PImageWin.viewWindow.RoiController.ROIList.Add(FindCircleTool.toolPar.InputPar.ROIFindCircle);
            }
            else
            {
                PImageWin.viewWindow.RoiController.ROIList[0] = FindCircleTool.toolPar.InputPar.ROIFindCircle;
            }
            PImageWin.viewWindow._hWndControl.repaint();
        }

    }
}
