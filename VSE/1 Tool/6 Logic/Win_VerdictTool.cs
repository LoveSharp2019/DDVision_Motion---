using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VControls;
using VSE.Core;

namespace VSE
{
    public partial class Win_VerdictTool : ToolWinBase
    {
        public Win_VerdictTool()
        {
            InitializeComponent();
            vLinkImage1.CallBackNodes += VLinkImage1_CallBackNodes;
            vLinkImage1.ImageChange += VLinkImage1_ImageChange;
        }

        private void VLinkImage1_ImageChange(object sender, EventArgs e)
        {
            verdictTool.toolPar.InputPar.图像 = Job.FindJobByName(jobName).FindToolInfoByName(vLinkImage1.ImageText.Split('.')[0]).GetOutput(vLinkImage1.ImageText.Split('.')[1]).value as HImage;
            PImageWin.Image = verdictTool.toolPar.InputPar.图像;
            Job.FindJobByName(jobName).ConnectSource(toolName, vLinkImage1.ImageText, Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("图像").value.ToString());
        }

        private void VLinkImage1_CallBackNodes(object sender, EventArgs e)
        {
            vLinkImage1.Nodes = Job.GetJobTreeStatic().Nodes;
            vLinkImage1.NoUseImage = toolName;
        }

        private static Win_VerdictTool _instance;
        internal static Win_VerdictTool Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                    _instance = new Win_VerdictTool();
                return _instance;
            }
        }

        /// <summary>
        /// 当前工具所对应的工具对象
        /// </summary>
        internal static VerdictTool verdictTool = new VerdictTool();

        private void Pbtn_runTool_Click(object sender, EventArgs e)
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                verdictTool.Run(true, true, toolName);
                long time = sw.ElapsedMilliseconds;

                if (verdictTool.toolRunStatu != (ToolRunStatu.成功))
                {
                    Plbl_toolTip.ForeColor = Color.Red;
                    Plbl_runTime.Text = string.Format("耗时：0ms");
                    Plbl_toolTip.Text = "状态：" + verdictTool.toolRunStatu.ToString();
                }
                else
                {
                    Plbl_toolTip.ForeColor = Color.White;
                    Plbl_runTime.Text = string.Format("耗时：{0}ms", time.ToString());

                    Plbl_toolTip.Text = "状态：" + verdictTool.toolRunStatu.ToString();
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

        private void Win_VerdictTool_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                label_Name.Text = Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("量测值").value.ToString();
                label_Val.Text = verdictTool.toolPar.InputPar.DValue.ToString();
                txt_Max.Text= verdictTool.toolPar.RunPar.Tolerance_max.ToString();
                txt_Min.Text= verdictTool.toolPar.RunPar.Tolerance_min.ToString();  
                txt_Offset.Text= verdictTool.toolPar.RunPar.Offset.ToString();
                label_OffsetVal.Text = verdictTool.toolPar.ResultPar.DValue.ToString();
                cbx_FAIEnable.Checked = verdictTool.toolPar.RunPar.IsCheck;
                if (verdictTool.toolPar.ResultPar.Consequence)
                {
                    label_Result.Text = "OK";
                }
                else
                {
                    label_Result.Text = "NG";
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

        private void txt_Max_TextChanged(object sender, EventArgs e)
        {
            verdictTool.toolPar.RunPar.Tolerance_max = Convert.ToDouble(txt_Max.Text);
        }

        private void txt_Min_TextChanged(object sender, EventArgs e)
        {
            verdictTool.toolPar.RunPar.Tolerance_min = Convert.ToDouble(txt_Min.Text);
        }

        private void txt_Offset_TextChanged(object sender, EventArgs e)
        {
            verdictTool.toolPar.RunPar.Offset = Convert.ToDouble(txt_Offset.Text);
        }

        private void cbx_FAIEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_FAIEnable.Checked == true)
            {
                verdictTool.toolPar.RunPar.IsCheck = true;
            }
            else if (cbx_FAIEnable.Checked == false)
            {
                verdictTool.toolPar.RunPar.IsCheck = false;
            }
        }

   
    }
}
