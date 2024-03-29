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
    public partial class Win_BinarizationTool : ToolWinBase
    {
        public Win_BinarizationTool()
        {
            InitializeComponent();
            vLinkImage1.CallBackNodes += VLinkImage1_CallBackNodes;
            vLinkImage1.ImageChange += VLinkImage1_ImageChange;
        }

        private void VLinkImage1_ImageChange(object sender, EventArgs e)
        {
            BinarizationTool.toolPar.InputPar.图像= Job.FindJobByName(jobName).FindToolInfoByName(vLinkImage1.ImageText.Split('.')[0]).GetOutput(vLinkImage1.ImageText.Split('.')[1]).value as HImage;
            PImageWin.Image = BinarizationTool.toolPar.InputPar.图像;

            Job.FindJobByName(jobName).ConnectSource(toolName,vLinkImage1.ImageText,Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("图像").value.ToString());
        }

        private void VLinkImage1_CallBackNodes(object sender, EventArgs e)
        {
            vLinkImage1.Nodes = Job.GetJobTreeStatic().Nodes;
            vLinkImage1.NoUseImage = toolName;

        }

        private static Win_BinarizationTool _instance;
        public static Win_BinarizationTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_BinarizationTool();
                return _instance;
            }
        }
        /// <summary>
        /// 当前工具所对应的工具对象
        /// </summary>
        internal static BinarizationTool BinarizationTool = new BinarizationTool();
        internal List<ROI> regions = new List<ROI>();

        private void Pbtn_runTool_Click(object sender, System.EventArgs e)
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                BinarizationTool.toolPar.RunPar.threshold = GrayV.Value;

                BinarizationTool.Run(true, true, toolName);
                long time = sw.ElapsedMilliseconds;

                if (BinarizationTool.toolRunStatu != (  ToolRunStatu.成功))
                {
                    Plbl_toolTip.ForeColor = Color.Red;
                    Plbl_runTime.Text = string.Format("耗时：0ms");
                    Plbl_toolTip.Text = "状态：" + BinarizationTool.toolRunStatu.ToString();
                }
                else
                {
                    Plbl_toolTip.ForeColor = Color.White;
                    Plbl_runTime.Text = string.Format("耗时：{0}ms", time.ToString());
                    PImageWin.Image = BinarizationTool.toolPar.ResultPar.图像;
                        Plbl_toolTip.Text = "状态：" + BinarizationTool.toolRunStatu.ToString();
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

        private void GrayV_ValueChanged(double value)
        {
            BinarizationTool.toolPar.RunPar.threshold = GrayV.Value;
            Pbtn_runTool_Click(null,null);
        }

        private void Win_BinarizationTool_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                GrayV.Value = BinarizationTool.toolPar.RunPar.threshold;
            }
        }

        private void btn_OrgImg_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            PImageWin.ClearWindow();
            PImageWin.Image = BinarizationTool.toolPar.InputPar.图像;
        }

        private void btn_OrgImg_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            PImageWin.ClearWindow();
            PImageWin.Image = BinarizationTool.toolPar.ResultPar.图像;
        }
    }
}
