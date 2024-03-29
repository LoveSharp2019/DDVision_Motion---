using HalconDotNet;
using Lxc.VisionPlus.ImageView.Model;
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
    public partial class Win_FindROITool : ToolWinBase
    {
        public Win_FindROITool()
        {
            InitializeComponent();
            vLinkImage1.CallBackNodes += VLinkImage1_CallBackNodes;
            vLinkImage1.ImageChange += VLinkImage1_ImageChange;
            vroiSet2.H = PImageWin;

            vroiSet2.ROIChangedEvent += LxcROISet1_ROISetChangedEvent;
        }
        private void LxcROISet1_ROISetChangedEvent(object sender, VControls.VROISet.LxcROISetChangedEventArgs e)
        {
            
                FindROI.toolPar.InputPar.搜索区域=(e.ROI);
        }
        private void VLinkImage1_ImageChange(object sender, EventArgs e)
        {
            FindROI.toolPar.InputPar.图像 = Job.FindJobByName(jobName).FindToolInfoByName(vLinkImage1.ImageText.Split('.')[0]).GetOutput(vLinkImage1.ImageText.Split('.')[1]).value as HImage;
            PImageWin.Image = FindROI.toolPar.InputPar.图像; 
            Job.FindJobByName(jobName).ConnectSource(toolName, vLinkImage1.ImageText, Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("图像").value.ToString());
        }

        private void VLinkImage1_CallBackNodes(object sender, EventArgs e)
        {
            vLinkImage1.Nodes = Job.GetJobTreeStatic().Nodes;
            vLinkImage1.NoUseImage = toolName;

        }
        private static Win_FindROITool _instance;
        public static Win_FindROITool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_FindROITool();
                return _instance;
            }
        }
        internal static FindROI FindROI = new FindROI();
        internal List<ROI> regions = new List<ROI>();

        private void numericUpDown3_OnValueChanged(object sender, EventArgs e)
        {
            FindROI.toolPar.RunPar.MinThreshold = (double)numericUpDown3.Value;
        }

        private void numericUpDown6_OnValueChanged(object sender, EventArgs e)
        {
            FindROI.toolPar.RunPar.MaxThreshold = (double)numericUpDown6.Value;
        }

        private void Pbtn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Pbtn_runTool_Click(object sender, EventArgs e)
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
          
                FindROI.Run(true, true, toolName);
                long time = sw.ElapsedMilliseconds;

                if (FindROI.toolRunStatu != (  ToolRunStatu.成功))
                {
                    Plbl_toolTip.ForeColor = Color.Red;
                    Plbl_runTime.Text = string.Format("耗时：0ms");
                    Plbl_toolTip.Text = "状态：" + FindROI.toolRunStatu.ToString();
                }
                else
                {
                    Plbl_toolTip.ForeColor = Color.White;
                    Plbl_runTime.Text = string.Format("耗时：{0}ms", time.ToString());
                    PImageWin.viewWindow._hWndControl.clearHRegionList();
                    PImageWin.viewWindow._hWndControl.repaint();
                    PImageWin.displayHRegion(FindROI.toolPar.ResultPar.region);
                    Plbl_toolTip.Text = "状态：" + FindROI.toolRunStatu.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        private void Win_FindROITool_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                tabControl1.SelectedTab = tabPage1;
                numericUpDown3.Value = (decimal)Win_FindROITool.FindROI.toolPar.RunPar.MinThreshold;
                numericUpDown6.Value = (decimal)Win_FindROITool.FindROI.toolPar.RunPar.MaxThreshold;
                vroiSet2.mROI = FindROI.toolPar.InputPar.搜索区域;
                lxcCheckBox2.Checked = !FindROI.FindROIShow;
            }
        }

        private void lxcCheckBox2_Click(object sender, EventArgs e)
        {
            FindROI.FindROIShow = !lxcCheckBox2.Checked;
        }
    }
}
