using System;
using System.Collections.Generic;
using System.Drawing;
using VControls;
using VSE.Core;
using HalconDotNet;
using Lxc.VisionPlus.ImageView.Model;
using System.Diagnostics;

namespace VSE
{
    public partial class Win_LLAngleTool : ToolWinBase
    {
        public Win_LLAngleTool()
        {
            InitializeComponent();
            vLinkImage1.CallBackNodes += VLinkImage1_CallBackNodes;
            vLinkImage1.ImageChange += VLinkImage1_ImageChange;
        }

        private void VLinkImage1_ImageChange(object sender, EventArgs e)
        {
            LLAngleTool.toolPar.InputPar.图像 = Job.FindJobByName(jobName).FindToolInfoByName(vLinkImage1.ImageText.Split('.')[0]).GetOutput(vLinkImage1.ImageText.Split('.')[1]).value as HImage;
            PImageWin.Image = LLAngleTool.toolPar.InputPar.图像;

            Job.FindJobByName(jobName).ConnectSource(toolName, vLinkImage1.ImageText, Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("图像").value.ToString());
        }

        private void VLinkImage1_CallBackNodes(object sender, EventArgs e)
        {
            vLinkImage1.Nodes = Job.GetJobTreeStatic().Nodes;
            vLinkImage1.NoUseImage = toolName;

        }

        private static Win_LLAngleTool _instance;
        public static Win_LLAngleTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_LLAngleTool();
                return _instance;
            }
        }

        /// <summary>
        /// 当前工具所对应的工具对象
        /// </summary>
        internal static LLAngleTool LLAngleTool = new LLAngleTool();
        internal List<ROI> regions = new List<ROI>();

        private void Pbtn_runTool_Click(object sender, EventArgs e)
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                LLAngleTool.Run(true, true, toolName);
                long time = sw.ElapsedMilliseconds;

                if (LLAngleTool.toolRunStatu != (ToolRunStatu.成功))
                {
                    Plbl_toolTip.ForeColor = Color.Red;
                    Plbl_runTime.Text = string.Format("耗时：0ms");
                    Plbl_toolTip.Text = "状态：" + LLAngleTool.toolRunStatu.ToString();
                }
                else
                {
                    Plbl_toolTip.ForeColor = Color.White;
                    Plbl_runTime.Text = string.Format("耗时：{0}ms", time.ToString());

                    Plbl_toolTip.Text = "状态：" + LLAngleTool.toolRunStatu.ToString();
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

        private void Win_LLAngleTool_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                HRegion L1 = new HRegion();
                HRegion L2 = new HRegion();
                L1.GenRegionLine(LLAngleTool.toolPar.InputPar.Line1.起点.Y, LLAngleTool.toolPar.InputPar.Line1.起点.X, LLAngleTool.toolPar.InputPar.Line1.终点.Y, LLAngleTool.toolPar.InputPar.Line1.终点.X);
                L2.GenRegionLine(LLAngleTool.toolPar.InputPar.Line2.起点.Y, LLAngleTool.toolPar.InputPar.Line2.起点.X, LLAngleTool.toolPar.InputPar.Line2.终点.Y, LLAngleTool.toolPar.InputPar.Line2.终点.X);
                PImageWin.displayHRegion(L1, "red");
                PImageWin.displayHRegion(L2, "green");
                vTextBox1.TextStr = LLAngleTool.toolPar.ResultPar.Angle.ToString("f3");
                lxcNumEdit_MAX.Value = Convert.ToDecimal( LLAngleTool.toolPar.InputPar.Tolerance_max);
                lxcNumEdit_MIN.Value = Convert.ToDecimal(LLAngleTool.toolPar.InputPar.Tolerance_min);
                if (LLAngleTool.toolPar.ResultPar.Consequence)
                {
                    vTextBox4.TextStr = "OK";
                }
                else
                {
                    vTextBox4.TextStr = "NG";
                }
            }
        }

        private void vLinkImage1_CallBackNodes_1(object sender, EventArgs e)
        {
            //
        }

        private void vLinkImage1_ImageChange_1(object sender, EventArgs e)
        {
            //
        }

        private void vLinkLine1_CallBackNodes(object sender, EventArgs e)
        {
            vLinkLine1.Nodes = Job.GetJobTreeStatic().Nodes;
            vLinkLine1.NoUseLine = toolName;
        }

        private void vLinkLine1_LineChange(object sender, EventArgs e)
        {
            LLAngleTool.toolPar.InputPar.Line1 = Job.FindJobByName(jobName).FindToolInfoByName(vLinkLine1.LineText.Split('.')[0]).GetOutput(vLinkLine1.LineText.Split('.')[1]).value as Line;
            Job.FindJobByName(jobName).ConnectSource(toolName, vLinkLine1.LineText, Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("Line1").value.ToString(), "Line1");

        }

        private void vLinkLine2_CallBackNodes(object sender, EventArgs e)
        {
            vLinkLine2.Nodes = Job.GetJobTreeStatic().Nodes;
            vLinkLine2.NoUseLine = toolName;
        }

        private void vLinkLine2_LineChange(object sender, EventArgs e)
        {
            LLAngleTool.toolPar.InputPar.Line2 = Job.FindJobByName(jobName).FindToolInfoByName(vLinkLine2.LineText.Split('.')[0]).GetOutput(vLinkLine2.LineText.Split('.')[1]).value as Line;
            Job.FindJobByName(jobName).ConnectSource(toolName, vLinkLine2.LineText, Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("Line2").value.ToString(), "Line2");

        }

        private void lxcNumEdit_MAX_OnValueChanged(object sender, EventArgs e)
        {
            LLAngleTool.toolPar.InputPar.Tolerance_max = (double)lxcNumEdit_MAX.Value;
        }

        private void lxcNumEdit_MIN_OnValueChanged(object sender, EventArgs e)
        {
            LLAngleTool.toolPar.InputPar.Tolerance_min = (double)lxcNumEdit_MIN.Value;
        }
    }
}
