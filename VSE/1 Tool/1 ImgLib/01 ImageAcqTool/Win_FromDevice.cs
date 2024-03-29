using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using VSE.Core;
using VSE.Properties;

namespace VSE
{
    internal partial class Win_FromDevice : Form
    {
        internal Win_FromDevice()
        {
            InitializeComponent();
            this.BackColor = VControls.VUI.WinBackColor;
            cbx_deviceList.BackColorNormal = VControls.VUI.WinBackColor;
            btn_displayImage.Clicked += btn_realTime_Clicked;
            btn_saveImage.Clicked += btn_saveImage_Clicked;
        }

        /// <summary>
        /// 锁
        /// </summary>
        private object obj = new object();
        /// <summary>
        /// 当前工具所属的流程
        /// </summary>
        internal string jobName = string.Empty;
        /// <summary>
        /// 当前工具名
        /// </summary>
        internal string toolName = string.Empty;
        /// <summary>
        /// 工具对象
        /// </summary>
        internal static AcqImageTool imageAcqTool = new AcqImageTool();
        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Win_FromDevice _instance;
        internal static Win_FromDevice Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_FromDevice();
                return _instance;
            }
        }


        private void cbx_deviceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            imageAcqTool.SwitchDevice(cbx_deviceList.Text);
        }
        private void tbx_exposure_ValueChanged(double value)
        {
            try
            {
                lock (obj)
                {
                    //tkb_exposure.Value = (int)value;
                    imageAcqTool.exposure = value;
                    imageAcqTool.SetCamExposure();
                    //hzy20220530,取消设置曝光时并进行取像
                    //imageAcqTool.Run(true, true, toolName);
                    tbx_exposure.Focus();
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private void tkb_exposure_Scroll(object sender, EventArgs e)
        {
            //tbx_exposure.Value = tkb_exposure.Value;
        }
        void btn_realTime_Clicked()
        {
            imageAcqTool.PlayImage(true, Win_AcqImageTool.Instance.PImageWin);
        }
        void btn_saveImage_Clicked()
        {
            imageAcqTool.SaveImage();
        }

    }
}
