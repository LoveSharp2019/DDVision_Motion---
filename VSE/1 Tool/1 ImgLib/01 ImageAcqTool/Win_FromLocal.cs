using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VSE.Core;
using VSE.Properties;

namespace VSE
{
    internal partial class Win_FromLocal : Form
    {
        internal Win_FromLocal()
        {
            InitializeComponent();
            this.BackColor = VControls.VUI.WinBackColor;
            //btn_browseImage.FlatAppearance.BorderColor = VUI.ButtonBorderColor;
            //Pbtn_close.FlatAppearance.BorderColor = VUI.ButtonBorderColor;
            btn_selectImagePath.Clicked += btn_selectImagePath_Clicked;
            btn_browseImage.Clicked += btn_browseImage_Clicked;
            btn_selectImageDirectoryPath.Clicked += btn_selectImageDirectoryPath_Clicked;


            ToolTip toolTip = new ToolTip();
            toolTip.ShowAlways = true;
            toolTip.SetToolTip(this.btn_lastImage, "上一张图像");
            toolTip.SetToolTip(this.btn_nextImage, "下一张图像");
        }

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
        private static Win_FromLocal _instance;
        public static Win_FromLocal Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_FromLocal();
                return _instance;
            }
        }


        private void tbx_imageDirectoryPath_TextChanged(object sender, EventArgs e)
        {
            imageAcqTool.imageDirectoryPath = tbx_imageDirectoryPath.Text.Trim();
        }
        private void btn_lastImage_Click(object sender, EventArgs e)
        {
            imageAcqTool.ReadLastImage();
        }
        private void btn_nextImage_Click(object sender, EventArgs e)
        {
            imageAcqTool.ReadNextImage();
        }
        void btn_browseImage_Clicked()
        {
            try
            {
                if (Directory.Exists(imageAcqTool.imageDirectoryPath))
                {
                     Win_AcqImageTool.Instance.TopMost = false;
                    
                    Process.Start(imageAcqTool.imageDirectoryPath);
                    this.pnl_multImage.Focus();
                }
                else
                {
                     Win_AcqImageTool.Instance.Plbl_toolTip.Text = "状态：请先指定图像目录路径";
                     Win_AcqImageTool.Instance.Plbl_toolTip.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        void btn_selectImageDirectoryPath_Clicked()
        {
            imageAcqTool.SelectImageDirectoryPath();
        }
        private void tbx_imagePath_TextChanged(object sender, EventArgs e)
        {
            imageAcqTool.imagePath = this.tbx_imagePath.Text.Trim();
        }
        void btn_selectImagePath_Clicked()
        {
            imageAcqTool.SelectImagePath();
        }

        private void btn_selectImageDirectoryPath_Load(object sender, EventArgs e)
        {

        }
    }
}
