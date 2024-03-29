using HalconDotNet;
using Lxc.VisionPlus.ImageView.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using VControls;
using VSE.Core;

namespace VSE
{
    public partial class Win_LLIntersectTool : ToolWinBase
    {
        public Win_LLIntersectTool()
        {
            InitializeComponent();
            vLinkImage1.CallBackNodes += VLinkImage1_CallBackNodes;
            vLinkImage1.ImageChange += VLinkImage1_ImageChange;
        }

        private void VLinkImage1_ImageChange(object sender, EventArgs e)
        {
            LLIntersectTool.toolPar.InputPar.图像= Job.FindJobByName(jobName).FindToolInfoByName(vLinkImage1.ImageText.Split('.')[0]).GetOutput(vLinkImage1.ImageText.Split('.')[1]).value as HImage;
            PImageWin.Image = LLIntersectTool.toolPar.InputPar.图像;

            Job.FindJobByName(jobName).ConnectSource(toolName,vLinkImage1.ImageText,Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("图像").value.ToString());
        }

        private void VLinkImage1_CallBackNodes(object sender, EventArgs e)
        {
            vLinkImage1.Nodes = Job.GetJobTreeStatic().Nodes;
            vLinkImage1.NoUseImage = toolName;

        }

        private static Win_LLIntersectTool _instance;
        public static Win_LLIntersectTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_LLIntersectTool();
                return _instance;
            }
        }
        /// <summary>
        /// 当前工具所对应的工具对象
        /// </summary>
        internal static LLIntersectTool LLIntersectTool = new LLIntersectTool();
        internal List<ROI> regions = new List<ROI>();

        private void Pbtn_runTool_Click(object sender, System.EventArgs e)
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                LLIntersectTool.Run(true, true, toolName);
                long time = sw.ElapsedMilliseconds;

                if (LLIntersectTool.toolRunStatu != (  ToolRunStatu.成功))
                {
                    Plbl_toolTip.ForeColor = Color.Red;
                    Plbl_runTime.Text = string.Format("耗时：0ms");
                    Plbl_toolTip.Text = "状态：" + LLIntersectTool.toolRunStatu.ToString();
                }
                else
                {
                    Plbl_toolTip.ForeColor = Color.White;
                    Plbl_runTime.Text = string.Format("耗时：{0}ms", time.ToString());
               
                        Plbl_toolTip.Text = "状态：" + LLIntersectTool.toolRunStatu.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        private void Pbtn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Win_LLIntersectTool_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                HRegion L1 = new HRegion();
                HRegion L2 = new HRegion();
                L1.GenRegionLine(LLIntersectTool.toolPar.InputPar.Line1.起点.Y, LLIntersectTool.toolPar.InputPar.Line1.起点.X, LLIntersectTool.toolPar.InputPar.Line1.终点.Y, LLIntersectTool.toolPar.InputPar.Line1.终点.X);
                L2.GenRegionLine(LLIntersectTool.toolPar.InputPar.Line2.起点.Y, LLIntersectTool.toolPar.InputPar.Line2.起点.X, LLIntersectTool.toolPar.InputPar.Line2.终点.Y, LLIntersectTool.toolPar.InputPar.Line2.终点.X);
                PImageWin.displayHRegion(L1,"red");
                PImageWin.displayHRegion(L2, "green");
                vTextBox1.TextStr = LLIntersectTool.toolPar.ResultPar.IntersectionP.X.ToString("f3");
                vTextBox2.TextStr = LLIntersectTool.toolPar.ResultPar.IntersectionP.Y.ToString("f3");
            }
        }

      

        private void vLinkLine1_CallBackNodes(object sender, EventArgs e)
        {
            vLinkLine1.Nodes = Job.GetJobTreeStatic().Nodes;
            vLinkLine1.NoUseLine = toolName;
        }

        private void vLinkLine1_LineChange(object sender, EventArgs e)
        {
            LLIntersectTool.toolPar.InputPar.Line1 = Job.FindJobByName(jobName).FindToolInfoByName(vLinkLine1.LineText.Split('.')[0]).GetOutput(vLinkLine1.LineText.Split('.')[1]).value as Line;
            Job.FindJobByName(jobName).ConnectSource(toolName, vLinkLine1.LineText, Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("Line1").value.ToString(),"Line1");
        }
        private void vLinkLine2_LineChange(object sender, EventArgs e)
        {
            LLIntersectTool.toolPar.InputPar.Line2 = Job.FindJobByName(jobName).FindToolInfoByName(vLinkLine2.LineText.Split('.')[0]).GetOutput(vLinkLine2.LineText.Split('.')[1]).value as Line;
            Job.FindJobByName(jobName).ConnectSource(toolName, vLinkLine2.LineText, Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("Line2").value.ToString(),"Line2");
        }
        private void vLinkLine2_CallBackNodes(object sender, EventArgs e)
        {
            vLinkLine2.Nodes = Job.GetJobTreeStatic().Nodes;
            vLinkLine2.NoUseLine = toolName;
        }

       
    }
}
