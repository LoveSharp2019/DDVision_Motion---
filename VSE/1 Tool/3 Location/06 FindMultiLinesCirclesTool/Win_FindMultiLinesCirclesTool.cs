using HalconDotNet;
using Lxc.VisionPlus.ImageView.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using VControls;
using VSE.Core;

namespace VSE
{
    public partial class Win_FindMultiLinesCirclesTool : ToolWinBase
    {
        public Win_FindMultiLinesCirclesTool()
        {
            InitializeComponent();
            //vCaliperSet1.CaliperCountChange += VCaliperSet1_CaliperCountChange;
            //vCaliperSet1.CaliperLengthChange += VCaliperSet1_CaliperLengthChange;
            //vCaliperSet1.CaliperWidthChange += VCaliperSet1_CaliperWidthChange;
            //vCaliperSet1.CaliperThresholdChange += VCaliperSet1_CaliperThresholdChange;
            vLinkImage1.CallBackNodes += VLinkImage1_CallBackNodes;
            vLinkImage1.ImageChange += VLinkImage1_ImageChange;
            vLinkPose1.CallBackNodes += VLinkPose1_CallBackNodes;
            vLinkPose1.PoseChange += VLinkPose1_PoseChange;
            PImageWin.viewWindow._hWndControl.ROIChangedEvent += _hWndControl_ROIChangedEvent;
        }
        ROICalipersLine roiTemp=new ROICalipersLine(0,0,0,0);
        private void _hWndControl_ROIChangedEvent(object sender, EventArgs e)
        {
            if (FindLineTool.toolPar.InputPar.ROIFindLine != null)
            {
                if(PImageWin.viewWindow.RoiController.ROIList.Count == 0)
                {
                    PImageWin.viewWindow.RoiController.ROIList.Add(FindLineTool.toolPar.InputPar.ROIFindLine);
                }
                else
                {
                    PImageWin.viewWindow.RoiController.ROIList[0] = FindLineTool.toolPar.InputPar.ROIFindLine;
                }
               
                if (FindLineTool.toolPar.InputPar.Pose != null && FindLineTool.toolPar.InputPar.Pose.Count > 0)
                {
                    FindLineTool.toolPar.InputPar.StandardPose = FindLineTool.toolPar.InputPar.Pose[0];
                }
                else
                {
                    FindLineTool.toolPar.InputPar.StandardPose = new XYU();
                }
    
                PImageWin.viewWindow._hWndControl.repaint();
            }
        }


        private void VLinkPose1_PoseChange(object sender, EventArgs e)
        {
            if (vLinkPose1.ROIText!="无")
            {
                FindLineTool.toolPar.InputPar.Pose = Job.FindJobByName(jobName).FindToolInfoByName(vLinkImage1.ImageText.Split('.')[0]).GetOutput(vLinkPose1.ROIText.Split('.')[1]).value as List<XYU>;
                Job.FindJobByName(jobName).ConnectSource(toolName, vLinkPose1.ROIText, Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("跟随位置").value.ToString(), "跟随位置");
            }
            else
            {
                FindLineTool.toolPar.InputPar.Pose = null;
                Job.FindJobByName(jobName).DisSource(toolName, Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("跟随位置").value.ToString(), "跟随位置");
                //Job.GetJobTreeStatic().Nodes[ToolIndex].Nodes[SubNodeIndex].Text = Regex.Split(input, "<-")[0] + "<-" + NewSource.ToString();
                //FindToolInfoByName(nodeText).GetInput(Regex.Split(input, "<-")[0]).value = "<-" + NewSource.ToString();
            }
            
        }

        private void VLinkPose1_CallBackNodes(object sender, EventArgs e)
        {
            vLinkPose1.Nodes = Job.GetJobTreeStatic().Nodes;
            vLinkPose1.NoUseROI = toolName;
        }

        private void VLinkImage1_ImageChange(object sender, EventArgs e)
        {
            FindLineTool.toolPar.InputPar.InPuImage = Job.FindJobByName(jobName).FindToolInfoByName(vLinkImage1.ImageText.Split('.')[0]).GetOutput(vLinkImage1.ImageText.Split('.')[1]).value as HImage;
            PImageWin.Image = FindLineTool.toolPar.InputPar.InPuImage;
            Job.FindJobByName(jobName).ConnectSource(toolName, vLinkImage1.ImageText, Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("图像").value.ToString());
        }

        private void VLinkImage1_CallBackNodes(object sender, EventArgs e)
        {
            vLinkImage1.Nodes = Job.GetJobTreeStatic().Nodes;
            vLinkImage1.NoUseImage = toolName;

        }
        #region 卡尺设定事件
        //private void VCaliperSet1_CaliperCountChange(double value)
        //{
        //    FindLineTool.toolPar.InputPar.CaliperCount = (int)value;
        //    FindLineTool.toolPar.InputPar.ROIFindLine.CaliperCount = FindLineTool.toolPar.InputPar.CaliperCount;
        //    if (PImageWin.viewWindow.RoiController.ROIList.Count==0)
        //    {
        //        PImageWin.viewWindow.RoiController.ROIList.Add(FindLineTool.toolPar.InputPar.ROIFindLine);
        //    }
        //    else
        //    {
        //        PImageWin.viewWindow.RoiController.ROIList[0] = FindLineTool.toolPar.InputPar.ROIFindLine;
        //    }
            
        //    PImageWin.viewWindow._hWndControl.repaint();
        //}
        //private void VCaliperSet1_CaliperLengthChange(double value)
        //{
        //    FindLineTool.toolPar.InputPar.CaliperLength = (int)value;
        //    FindLineTool.toolPar.InputPar.ROIFindLine.SearchLength = FindLineTool.toolPar.InputPar.CaliperLength;
        //    if (PImageWin.viewWindow.RoiController.ROIList.Count == 0)
        //    {
        //        PImageWin.viewWindow.RoiController.ROIList.Add(FindLineTool.toolPar.InputPar.ROIFindLine);
        //    }
        //    else
        //    {
        //        PImageWin.viewWindow.RoiController.ROIList[0] = FindLineTool.toolPar.InputPar.ROIFindLine;
        //    }
        //    PImageWin.viewWindow._hWndControl.repaint();
        //}
        //private void VCaliperSet1_CaliperWidthChange(double value)
        //{
        //    FindLineTool.toolPar.InputPar.CaliperWidth = (int)value;
        //    FindLineTool.toolPar.InputPar.ROIFindLine.ProjectionLength = FindLineTool.toolPar.InputPar.CaliperWidth;
        //    if (PImageWin.viewWindow.RoiController.ROIList.Count == 0)
        //    {
        //        PImageWin.viewWindow.RoiController.ROIList.Add(FindLineTool.toolPar.InputPar.ROIFindLine);
        //    }
        //    else
        //    {
        //        PImageWin.viewWindow.RoiController.ROIList[0] = FindLineTool.toolPar.InputPar.ROIFindLine;
        //    }
        //    PImageWin.viewWindow._hWndControl.repaint();
        //}
        //private void VCaliperSet1_CaliperThresholdChange(double value)
        //{
        //    FindLineTool.toolPar.InputPar.CaliperThreshold = (int)value;
        //}
        #endregion
        private static Win_FindMultiLinesCirclesTool _instance;
        internal static Win_FindMultiLinesCirclesTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_FindMultiLinesCirclesTool();
                return _instance;
            }
        }
        internal static FindLineTool FindLineTool = new FindLineTool();

        private void Win_FindLineTool_VisibleChanged(object sender, System.EventArgs e)
        {
            if (this.Visible)
            {
                tabControl1.SelectedTab = tabPage1;
                //vCaliperSet1.CaliperCount = FindLineTool.toolPar.InputPar.CaliperCount;
                //vCaliperSet1.CaliperLength = FindLineTool.toolPar.InputPar.CaliperLength;
                //vCaliperSet1.CaliperWidth = FindLineTool.toolPar.InputPar.CaliperWidth;
                //vCaliperSet1.CaliperThreshold = FindLineTool.toolPar.InputPar.CaliperThreshold;
                if (FindLineTool.toolPar.InputPar.InPuImage!=null)
                {
                    if (PImageWin.viewWindow.RoiController.ROIList.Count == 0)
                    {
                        HTuple w, h;
                        FindLineTool.toolPar.InputPar.InPuImage.GetImageSize(out w, out h);
                        PImageWin.viewWindow.RoiController.ROIList.Add(new ROICalipersLine(h / 2 - w/6, w / 2 - w / 6, h / 2 + w / 6, w / 2 + w / 6));
                      
                    }
                    FindLineTool.UpDateROI();
                    PImageWin.viewWindow.RoiController.ROIList[0]= FindLineTool.toolPar.InputPar.ROIFindLine;
                    PImageWin.viewWindow._hWndControl.repaint();

                }
                lxcCheckBox3.Checked= !FindLineTool.toolPar.RunPar.ShowPoint;
                lxcCheckBox4.Checked = !FindLineTool.toolPar.RunPar.ShowCalipers;
                checkBox1.Checked = !FindLineTool.toolPar.RunPar.ShowSegment;
                if (FindLineTool.toolPar.InputPar.SerachDir == FindLineTool.EdgePolarity.positive)
                {
                   // lxcRadioButton3.Checked=true;
                }
                else if (FindLineTool.toolPar.InputPar.SerachDir == FindLineTool.EdgePolarity.negative)
                {
                    //lxcRadioButton4.Checked = true;
                }
                else
                {
                    //lxcRadioButton5.Checked = true;
                }
               
            }
        }

        private void Pbtn_runTool_Click(object sender, System.EventArgs e)
        {
            Pbtn_runTool.Enabled = false;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            FindLineTool.Run(true, true, toolName);
            long elapsedTime = sw.ElapsedMilliseconds;
            dgv_Result.Rows.Clear();
            if (FindLineTool.toolRunStatu != (ToolRunStatu.成功))
            {
                //vTextBox1.TextStr = "NaN";
                //vTextBox2.TextStr = "NaN";
                //vTextBox3.TextStr = "NaN";
                //vTextBox4.TextStr = "NaN";
                //vTextBox5.TextStr = "NaN";
                Plbl_runTime.ForeColor = Color.Red;
                Plbl_runTime.Text = string.Format("耗时：0ms");
            }
            else
            {
                int c = FindLineTool.toolPar.Results.PointCounts;
               // CCount.Text = c.ToString() + " 结果";
                for (int i = 0; i < c; i++)
                {
                    dgv_Result.Rows.Add();
                    dgv_Result.Rows[i].Cells[0].Value = i+1;
                    dgv_Result.Rows[i].Cells[1].Value = FindLineTool.toolPar.Results.row.DArr[i].ToString("f3");
                    dgv_Result.Rows[i].Cells[2].Value = FindLineTool.toolPar.Results.col.DArr[i].ToString("f3");

                }

                //vTextBox1.TextStr = FindLineTool.toolPar.Results.Line.起点.X.ToString("f3");
                //vTextBox2.TextStr = FindLineTool.toolPar.Results.Line.起点.Y.ToString("f3");
                //vTextBox3.TextStr = FindLineTool.toolPar.Results.Line.终点.X.ToString("f3");
                //vTextBox4.TextStr = FindLineTool.toolPar.Results.Line.终点.Y.ToString("f3");
                //vTextBox5.TextStr = FindLineTool.toolPar.Results.Line.方向.ToString("f3");
                
                Plbl_runTime.ForeColor = Color.FromArgb(244, 244, 244);
                Plbl_runTime.Text = string.Format("耗时：{0}ms", elapsedTime.ToString());
            }
            Plbl_toolTip.Text = "状态：" + FindLineTool.toolRunStatu.ToString();

            Pbtn_runTool.Enabled = true;
        }

        private void Pbtn_close_Click(object sender, System.EventArgs e)
        {
            this.Close();   
        }

        private void lxcCheckBox3_Click(object sender, System.EventArgs e)
        {
            FindLineTool.toolPar.RunPar.ShowPoint = !lxcCheckBox3.Checked;
        }

        private void lxcCheckBox4_Click(object sender, System.EventArgs e)
        {
            FindLineTool.toolPar.RunPar.ShowCalipers = !lxcCheckBox4.Checked;
        }

        private void checkBox1_Click(object sender, System.EventArgs e)
        {
            FindLineTool.toolPar.RunPar.ShowSegment = !checkBox1.Checked;
        }

        //private void lxcRadioButton4_Click(object sender, System.EventArgs e)
        //{
        //    if (lxcRadioButton3.Checked)
        //    {
        //        FindLineTool.toolPar.InputPar.SerachDir = FindLineTool.EdgePolarity.positive;
        //    }
        //    else if (lxcRadioButton4.Checked)
        //    {
        //        FindLineTool.toolPar.InputPar.SerachDir = FindLineTool.EdgePolarity.negative;
        //    }
        //    else
        //    {
        //        FindLineTool.toolPar.InputPar.SerachDir = FindLineTool.EdgePolarity.all;
        //    }
        //}
    }
}
