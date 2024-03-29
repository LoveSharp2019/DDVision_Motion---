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
    public partial class Win_PLDistanceTool : ToolWinBase
    {
        public Win_PLDistanceTool()
        {
            InitializeComponent();
            vLinkImage1.CallBackNodes += VLinkImage1_CallBackNodes;
            vLinkImage1.ImageChange += VLinkImage1_ImageChange;
        }

        private void VLinkImage1_ImageChange(object sender, EventArgs e)
        {
            PLineDistanceTool.toolPar.InputPar.图像 = Job.FindJobByName(jobName).FindToolInfoByName(vLinkImage1.ImageText.Split('.')[0]).GetOutput(vLinkImage1.ImageText.Split('.')[1]).value as HImage;
            PImageWin.Image = PLineDistanceTool.toolPar.InputPar.图像;

            Job.FindJobByName(jobName).ConnectSource(toolName, vLinkImage1.ImageText, Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("图像").value.ToString());
        }

        private void VLinkImage1_CallBackNodes(object sender, EventArgs e)
        {
            vLinkImage1.Nodes = Job.GetJobTreeStatic().Nodes;
            vLinkImage1.NoUseImage = toolName;

        }

        private static Win_PLDistanceTool _instance;
        public static Win_PLDistanceTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_PLDistanceTool();
                return _instance;
            }
        }
       
        /// <summary>
        /// 当前工具所对应的工具对象
        /// </summary>
        internal static PLDistanceTool PLineDistanceTool = new PLDistanceTool();
        internal List<ROI> regions = new List<ROI>();

        private void Pbtn_runTool_Click(object sender, EventArgs e)
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                PLineDistanceTool.Run(true, true, toolName);
                long time = sw.ElapsedMilliseconds;

                if (PLineDistanceTool.toolRunStatu != (ToolRunStatu.成功))
                {
                    Plbl_toolTip.ForeColor = Color.Red;
                    Plbl_runTime.Text = string.Format("耗时：0ms");
                    Plbl_toolTip.Text = "状态：" + PLineDistanceTool.toolRunStatu.ToString();
                }
                else
                {
                    Plbl_toolTip.ForeColor = Color.White;
                    Plbl_runTime.Text = string.Format("耗时：{0}ms", time.ToString());

                    Plbl_toolTip.Text = "状态：" + PLineDistanceTool.toolRunStatu.ToString();
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

        private void Win_PLDistanceTool_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                HXLDCont P1 = new HXLDCont();
                HRegion L1 = new HRegion();
                P1.GenCrossContourXld(PLineDistanceTool.toolPar.InputPar.Point1.Y, PLineDistanceTool.toolPar.InputPar.Point1.X, 80, 0);
                L1.GenRegionLine(PLineDistanceTool.toolPar.InputPar.Line1.起点.Y, PLineDistanceTool.toolPar.InputPar.Line1.起点.X, PLineDistanceTool.toolPar.InputPar.Line1.终点.Y, PLineDistanceTool.toolPar.InputPar.Line1.终点.X);
                PImageWin.displayHRegion(P1, "red");
                PImageWin.displayHRegion(L1, "green");

                lxcNumEdit_Accuracy.Value = Convert.ToDecimal(PLineDistanceTool.toolPar.InputPar.VisionAccuracy);
                lxcNumEdit_MAX.Value = Convert.ToDecimal(PLineDistanceTool.toolPar.InputPar.Tolerance_max);
                lxcNumEdit_MIN.Value = Convert.ToDecimal(PLineDistanceTool.toolPar.InputPar.Tolerance_min);
                vTextBox1.TextStr = PLineDistanceTool.toolPar.ResultPar.Distance.ToString("f3");
                if (PLineDistanceTool.toolPar.ResultPar.Consequence)
                {
                    vTextBox4.TextStr = "OK";
                }
                else
                {
                    vTextBox4.TextStr = "NG";
                }
            }
        }
       
        /// <summary>
        /// 判定标准设定
        /// </summary>
        /// <param name="value"></param>
      
        private void vLinkImage1_CallBackNodes_1(object sender, EventArgs e)
        {
            //
        }
        private void vLinkImage1_ImageChange_1(object sender, EventArgs e)
        {
            //
        }

        private void vLinkPoint1_CallBackNodes(object sender, EventArgs e)
        {
            vLinkPoint1.Nodes = Job.GetJobTreeStatic().Nodes;
            vLinkPoint1.NoUsePoint = toolName;
        }

        private void vLinkPoint1_PointChange(object sender, EventArgs e)
        {
            PLineDistanceTool.toolPar.InputPar.Point1 = Job.FindJobByName(jobName).FindToolInfoByName(vLinkPoint1.PointText.Split('.')[0]).GetOutput(vLinkPoint1.PointText.Split('.')[1]).value as XY;
            Job.FindJobByName(jobName).ConnectSource(toolName, vLinkPoint1.PointText, Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("Point1").value.ToString(), "Point1");

        }

        private void vLinkLine1_CallBackNodes(object sender, EventArgs e)
        {
            vLinkLine1.Nodes = Job.GetJobTreeStatic().Nodes;
            vLinkLine1.NoUseLine = toolName;
        }

        private void vLinkLine1_LineChange(object sender, EventArgs e)
        {
            PLineDistanceTool.toolPar.InputPar.Line1 = Job.FindJobByName(jobName).FindToolInfoByName(vLinkLine1.LineText.Split('.')[0]).GetOutput(vLinkLine1.LineText.Split('.')[1]).value as Line;
            Job.FindJobByName(jobName).ConnectSource(toolName, vLinkLine1.LineText, Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("Line1").value.ToString(), "Line1");

        }

        private void lxcNumEdit_MAX_OnValueChanged(object sender, EventArgs e)
        {
            PLineDistanceTool.toolPar.InputPar.Tolerance_max = (double)lxcNumEdit_MAX.Value;
        }

        private void lxcNumEdit_MIN_OnValueChanged(object sender, EventArgs e)
        {
            PLineDistanceTool.toolPar.InputPar.Tolerance_min = (double)lxcNumEdit_MIN.Value;
        }

        private void lxcNumEdit_Accuracy_OnValueChanged(object sender, EventArgs e)
        {
            PLineDistanceTool.toolPar.InputPar.VisionAccuracy= (double)lxcNumEdit_Accuracy.Value;   
        }
    }
}
