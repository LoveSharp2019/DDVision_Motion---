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
    public partial class Win_PPDistanceTool : ToolWinBase
    {
        public Win_PPDistanceTool()
        {
            InitializeComponent();
            vLinkImage1.CallBackNodes += VLinkImage1_CallBackNodes;
            vLinkImage1.ImageChange += VLinkImage1_ImageChange;
        }

        private void VLinkImage1_ImageChange(object sender, EventArgs e)
        {
            PPDistanceTool.toolPar.InputPar.图像= Job.FindJobByName(jobName).FindToolInfoByName(vLinkImage1.ImageText.Split('.')[0]).GetOutput(vLinkImage1.ImageText.Split('.')[1]).value as HImage;
            PImageWin.Image = PPDistanceTool.toolPar.InputPar.图像;

            Job.FindJobByName(jobName).ConnectSource(toolName,vLinkImage1.ImageText,Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("图像").value.ToString());
        }

        private void VLinkImage1_CallBackNodes(object sender, EventArgs e)
        {
            vLinkImage1.Nodes = Job.GetJobTreeStatic().Nodes;
            vLinkImage1.NoUseImage = toolName;

        }

        private static Win_PPDistanceTool _instance;
        public static Win_PPDistanceTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_PPDistanceTool();
                return _instance;
            }
        }
        /// <summary>
        /// 当前工具所对应的工具对象
        /// </summary>
        internal static PPDistanceTool PPDistanceTool = new PPDistanceTool();
        internal List<ROI> regions = new List<ROI>();

        private void Pbtn_runTool_Click(object sender, System.EventArgs e)
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                PPDistanceTool.Run(true, true, toolName);
                long time = sw.ElapsedMilliseconds;

                if (PPDistanceTool.toolRunStatu != (  ToolRunStatu.成功))
                {
                    Plbl_toolTip.ForeColor = Color.Red;
                    Plbl_runTime.Text = string.Format("耗时：0ms");
                    Plbl_toolTip.Text = "状态：" + PPDistanceTool.toolRunStatu.ToString();
                }
                else
                {
                    Plbl_toolTip.ForeColor = Color.White;
                    Plbl_runTime.Text = string.Format("耗时：{0}ms", time.ToString());
               
                        Plbl_toolTip.Text = "状态：" + PPDistanceTool.toolRunStatu.ToString();
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

        private void Win_P2PDistanceTool_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                HXLDCont P1 = new HXLDCont();
                HXLDCont P2 = new HXLDCont();
                P1.GenCrossContourXld(PPDistanceTool.toolPar.InputPar.Point1.Y,PPDistanceTool.toolPar.InputPar.Point1.X,80,0);
                P2.GenCrossContourXld(PPDistanceTool.toolPar.InputPar.Point2.Y, PPDistanceTool.toolPar.InputPar.Point2.X, 80, 0);
                PImageWin.displayHRegion(P1,"red");
                PImageWin.displayHRegion(P2, "green");

                lxcNumEdit_Accuracy.Value = Convert.ToDecimal(PPDistanceTool.toolPar.InputPar.VisionAccuracy);
                lxcNumEdit_MAX.Value = Convert.ToDecimal(PPDistanceTool.toolPar.InputPar.Tolerance_max);
                lxcNumEdit_MIN.Value = Convert.ToDecimal(PPDistanceTool.toolPar.InputPar.Tolerance_min);
                vTextBox1.TextStr = PPDistanceTool.toolPar.ResultPar.Distance.ToString("f3");
                if (PPDistanceTool.toolPar.ResultPar.Consequence)
                {
                    vTextBox4.TextStr = "OK";
                }
                else
                {
                    vTextBox4.TextStr = "NG";
                }

            }
        }

      

        private void vLinkPoint1_CallBackNodes(object sender, EventArgs e)
        {
            vLinkPoint1.Nodes = Job.GetJobTreeStatic().Nodes;
            vLinkPoint1.NoUsePoint = toolName;
        }

        private void vLinkPoint1_LineChange(object sender, EventArgs e)
        {
            PPDistanceTool.toolPar.InputPar.Point1 = Job.FindJobByName(jobName).FindToolInfoByName(vLinkPoint1.PointText.Split('.')[0]).GetOutput(vLinkPoint1.PointText.Split('.')[1]).value as XY;
            Job.FindJobByName(jobName).ConnectSource(toolName, vLinkPoint1.PointText, Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("Point1").value.ToString(), "Point1");
        }
        private void vLinkPoint2_LineChange(object sender, EventArgs e)
        {
            PPDistanceTool.toolPar.InputPar.Point2 = Job.FindJobByName(jobName).FindToolInfoByName(vLinkPoint2.PointText.Split('.')[0]).GetOutput(vLinkPoint2.PointText.Split('.')[1]).value as XY;
            Job.FindJobByName(jobName).ConnectSource(toolName, vLinkPoint2.PointText, Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("Point2").value.ToString(), "Point2");
        }
        private void vLinkPoint2_CallBackNodes(object sender, EventArgs e)
        {
            vLinkPoint2.Nodes = Job.GetJobTreeStatic().Nodes;
            vLinkPoint2.NoUsePoint = toolName;
        }

        private void lxcNumEdit_Accuracy_OnValueChanged(object sender, EventArgs e)
        {
            PPDistanceTool.toolPar.InputPar.VisionAccuracy =(double)lxcNumEdit_Accuracy.Value;
        }

        private void lxcNumEdit_MAX_OnValueChanged(object sender, EventArgs e)
        {
            PPDistanceTool.toolPar.InputPar.Tolerance_max = (double)lxcNumEdit_MAX.Value;
        }

        private void lxcNumEdit_MIN_OnValueChanged(object sender, EventArgs e)
        {
            PPDistanceTool.toolPar.InputPar.Tolerance_min = (double)lxcNumEdit_MIN.Value;
        }
    }
}
