using VSE.Properties;
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
    internal partial class Win_AcqImageTool : ToolWinBase
    {
        internal Win_AcqImageTool()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 工具对象
        /// </summary>
        internal AcqImageTool acqImageTool = new AcqImageTool();
        /// <summary>
        /// ROI区域
        /// </summary>
        internal List<ROI> L_regions = new List<ROI>();
        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Win_AcqImageTool _instance;
        public static Win_AcqImageTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_AcqImageTool();
                return _instance;
            }
        }
        //private void tsb_SDKInfo_Click(object sender, EventArgs e)
        //{
        //    Win_SDKInfo.Instance.ShowDialog();
        //}
      
        private void btn_close_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();

                //退出窗体时自动停止相机实时
                if (acqImageTool.displayImageMode)
                    acqImageTool.PlayImage(false, PImageWin);

                acqImageTool.UpdateOutput(toolName);
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }

        }
        private void rdo_fromDevice_CheckedChanged(object sender, EventArgs e)
        {
            if (Job.loadForm)
                return;

            if (rdo_fromDevice.Checked)
                acqImageTool.SwitchImageSource(ImageSourceMode.FromDevice);

        }
        private void radio_FromLocalFile_CheckedChanged(object sender, EventArgs e)
        {
            if (Job.loadForm)
                return;

            if (radio_FromLocalFile.Checked)
                acqImageTool.SwitchImageSource(ImageSourceMode.FromFile);
        }
        private void rdo_fromLocalDirectory_CheckedChanged(object sender, EventArgs e)
        {
            if (Job.loadForm)
                return;

            if (rdo_fromLocalDirectory.Checked)
                acqImageTool.SwitchImageSource(ImageSourceMode.FromDirectory);
        }
        private void pic_fromDevice_Click(object sender, EventArgs e)
        {
            acqImageTool.SwitchImageSource(ImageSourceMode.FromDevice);
        }
        private void pic_fromLocalFile_Click(object sender, EventArgs e)
        {
            acqImageTool.SwitchImageSource(ImageSourceMode.FromFile);
        }
        private void pic_fromLocalDirectory_Click(object sender, EventArgs e)
        {
            acqImageTool.SwitchImageSource(ImageSourceMode.FromDirectory);
        }
     
        private void tsb_runTool_Click(object sender, EventArgs e)
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                Start.ImaEnqueueTool = true;
                acqImageTool.Run(true, true, toolName);
                long time = sw.ElapsedMilliseconds;

                if (acqImageTool.toolRunStatu != (  ToolRunStatu.成功))
                {
                    Plbl_toolTip.ForeColor = Color.Red;
                    Plbl_runTime.Text = string.Format("耗时：0ms");
                    Plbl_toolTip.Text = "状态：" + acqImageTool.toolRunStatu.ToString();
                }
                else
                {
                    Plbl_toolTip.ForeColor = Color.White;
                    Plbl_runTime.Text = string.Format("耗时：{0}ms", time.ToString());
                    if (acqImageTool.imageSourceMode == ImageSourceMode.FromDirectory)
                        Plbl_toolTip.Text = string.Format("状态：当前图像：{0} ({1})", acqImageTool.currentImageName, acqImageTool.currentImageIndex + 1 + "/" + acqImageTool.L_images.Count);
                    else
                        Plbl_toolTip.Text = "状态：" + acqImageTool.toolRunStatu.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        private void ckb_autoSwitch_CheckChanged(bool Checked)
        {
            acqImageTool.autoSwitch = Checked;
        }
        private void ckb_absPath_CheckChanged(bool Checked)
        {
            acqImageTool.absPath = Checked;
        }
        private void ckb_RGBToGray_CheckChanged(bool Checked)
        {
            acqImageTool.RGBToGray = Checked;
        }
        private void ckb_displayAllImageRegion_CheckChanged(bool Checked)
        {
            try
            {
                if (Job.loadForm)
                    return;

                if (Checked)
                {
                    acqImageTool.displayAllImageRegion = true;
                    PImageWin.HobjectToHimage(acqImageTool.toolPar.ResultPar.图像);
                }
                else
                {
                    acqImageTool.displayAllImageRegion = false;
                    if (acqImageTool.L_regions.Count == 0)
                        PImageWin.viewWindow.genRect1(200, 200, 400, 400, ref acqImageTool.L_regions);
                    else
                        Win_AcqImageTool.Instance.PImageWin.viewWindow.displayROI(acqImageTool.L_regions);
                    this.L_regions = acqImageTool.L_regions;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private void ckb_rotateImage_CheckChanged(bool Checked)
        {
            try
            {
                acqImageTool.rotateImage = Checked;
                nud_rotateAngle.Visible = Checked;
                lbl_deg.Visible = Checked;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
      
        private void ckb_hardware_CheckChanged(bool Checked)
        {
            if (Checked)
            {
                acqImageTool.hardTrigger = true;//硬触发  
                acqImageTool.triggerModel = DeviceTriggerModel.hardware;
                acqImageTool.SetCamTriggerModel(DeviceTriggerModel.hardware);
            }
            else
            {
                acqImageTool.hardTrigger = false;//软触发   
                acqImageTool.triggerModel = DeviceTriggerModel.software;
                acqImageTool.SetCamTriggerModel(DeviceTriggerModel.software);
            }
        }

        private void Win_AcqImageTool_VisibleChanged(object sender, EventArgs e)
        {
         
        }

        private void Pbtn_help_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Application.StartupPath + "\\HelpDoc\\取像工具.pdf");
            }
            catch
            {
                Win_MessageBox.Instance.MessageBoxShow("\r\n工具暂无工具使用说明文档！", TipType.Warn);
            }
        }
    }
}
