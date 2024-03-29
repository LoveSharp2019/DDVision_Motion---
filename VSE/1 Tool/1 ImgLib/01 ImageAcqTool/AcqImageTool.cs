using HalconDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using Basler.Pylon;
using VSE.Properties;
//using Ookii.Dialogs.WinForms;
using System.Text.RegularExpressions;
using Lxc.VisionPlus.ImageView.Model;
using Lxc.VisionPlus.ImageView;
using VSE.Core;
using VControls;
using System.Collections;
using System.Collections.Concurrent;

namespace VSE
{
    [Serializable]
    class AcqImageTool : ToolBase
    {
        ~AcqImageTool()
        {
            displayImageMode = false;
        }

        /// <summary>
        /// 相机集合
        /// </summary>
        internal static List<SDK_Base> L_devices = new List<SDK_Base>();
        /// <summary>
        /// 相机集合对应曝光参数值
        /// </summary>
        //internal Dictionary<string, double> L_devicesExposure = new Dictionary<string, double>();
        /// <summary>
        /// 工具参数
        /// </summary>
        internal ToolPar toolPar = new ToolPar();
        /// <summary>
        /// 绝对路径
        /// </summary>
        internal bool absPath = true;
        /// <summary>
        /// 硬触发模式
        /// </summary>
        internal bool hardTrigger = false;
        internal DeviceTriggerModel triggerModel = DeviceTriggerModel.software;
        /// <summary>
        /// 曝光时间
        /// </summary>
        internal double exposure = 20;
        /// <summary>
        /// 图像源
        /// </summary>
        internal ImageSourceMode imageSourceMode = ImageSourceMode.FromDirectory;
        /// <summary>
        /// 是否处于实时采集模式
        /// </summary>
        internal bool displayImageMode = false;
        /// <summary>
        /// 相机对象
        /// </summary>
        internal SDK_Base SDK_Camera;
        /// <summary>
        /// 相机实时线程
        /// </summary>
        internal static Thread th_displayImage;
        /// <summary>
        /// 图像旋转角度
        /// </summary>
        internal int rotateAngle = 0;
        /// <summary>
        /// 读取文件夹图像模式时每次运行是否自动切换图像
        /// </summary>
        internal bool autoSwitch = true;
        /// <summary>
        /// 是否旋转图像
        /// </summary>
        internal bool rotateImage = false;
        /// <summary>
        /// 是否将彩色图像转化成灰度图像
        /// </summary>
        internal bool RGBToGray = true;
        /// <summary>
        /// 是否显示图像的全部区域
        /// </summary>
        internal bool displayAllImageRegion = true;
        /// <summary>
        /// ROI
        /// </summary>
        internal List<ROI> L_regions = new List<ROI>();
        /// <summary>
        /// 工作模式为读取文件夹图像时，当前图像的名称
        /// </summary>
        internal string currentImageName = string.Empty;
        /// <summary>
        /// 工作模式为读取文件夹图像时，当前图片的索引
        /// </summary>
        internal int currentImageIndex = 0;
        /// <summary>
        /// 文件夹中的图像文件集合
        /// </summary>
        internal List<string> L_images = new List<string>();
        /// <summary>
        /// 单张图像文件路径
        /// </summary>
        internal string imagePath = string.Empty;
        /// <summary>
        /// 图像文件夹路径
        /// </summary>
        internal string imageDirectoryPath = string.Empty;
        /// <summary>
        /// 队列模式
        /// </summary>
        //public bool QeModel = false;
        /// <summary>
        /// 软件是否处于加载中
        /// </summary>
        public bool isLoad = false;
        /// <summary>
        /// 主站地址
        /// </summary>
        internal int MainIDAddress = 1;
        /// <summary>
        /// 图像队列
        /// </summary>
        [NonSerialized]
        private Queue<HImage> _QeHImages;

        public Queue<HImage> QeHImages
        {
            get
            {
                if (_QeHImages == null)
                    _QeHImages = new Queue<HImage>();
                return _QeHImages;
            }
        }

        public int BufferCount
        {
            get
            {
                try
                {
                    if (SDK_Camera != null)
                    {
                        if ((SDK_Camera as SDK_Halcon) != null)
                        {
                            return 0;
                        }
                        else
                        {
                            {
                                if ((SDK_Camera as SDK_HIKVision).QhImages != null)
                                {
                                    return (SDK_Camera as SDK_HIKVision).QhImages.Count;
                                }
                                else
                                {
                                    return 0;
                                }
                            }
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch
                {
                    return 0;
                }
            }

        }

        public void ClearCallBackBuffer()
        {
            try
            {
                if (SDK_Camera != null)
                {
                    if ((SDK_Camera as SDK_HIKVision).QhImages != null)
                    {
                        (SDK_Camera as SDK_HIKVision).QhImages.Clear();
                    }
                }
            }
            catch
            { }
        }

        /// <summary>
        /// 图像源模式切换
        /// </summary>
        internal void SwitchImageSource(ImageSourceMode mode)
        {
            try
            {
                imageSourceMode = mode;
                switch (mode)
                {
                    case ImageSourceMode.FromDevice:
                        Win_AcqImageTool.Instance.rdo_fromDevice.Checked = true;
                        Win_AcqImageTool.Instance.pic_fromDevice.Image = Resources.勾选;
                        Win_AcqImageTool.Instance.pic_fromLocalFile.Image = Resources.去勾选;
                        Win_AcqImageTool.Instance.pic_fromLocalDirectory.Image = Resources.去勾选;
                        Win_AcqImageTool.Instance.rdo_fromDevice.ForeColor = Color.White;
                        Win_AcqImageTool.Instance.radio_FromLocalFile.ForeColor = Color.Gray;
                        Win_AcqImageTool.Instance.rdo_fromLocalDirectory.ForeColor = Color.Gray;
                        Win_AcqImageTool.Instance.rdo_fromDevice.Font = new Font(Win_AcqImageTool.Instance.rdo_fromDevice.Font.Name, Win_AcqImageTool.Instance.rdo_fromDevice.Font.Size, FontStyle.Bold);
                        Win_AcqImageTool.Instance.radio_FromLocalFile.Font = new Font(Win_AcqImageTool.Instance.radio_FromLocalFile.Font.Name, Win_AcqImageTool.Instance.radio_FromLocalFile.Font.Size, FontStyle.Regular);
                        Win_AcqImageTool.Instance.rdo_fromLocalDirectory.Font = new Font(Win_AcqImageTool.Instance.rdo_fromLocalDirectory.Font.Name, Win_AcqImageTool.Instance.rdo_fromLocalDirectory.Font.Size, FontStyle.Regular);

                        Win_AcqImageTool.Instance.pnl_formPanel.Controls.Clear();
                        Win_FromDevice.Instance.TopLevel = false;
                        Win_FromDevice.Instance.Parent = Win_AcqImageTool.Instance.pnl_formPanel;
                        Win_FromDevice.Instance.Dock = DockStyle.Top;
                        Win_FromDevice.Instance.Show();

                        Win_AcqImageTool.Instance.ckb_hardware.Visible = true;
                        //Win_AcqImageTool.Instance.cCheckBox1.Location = new System.Drawing.Point(Win_AcqImageTool.Instance.cCheckBox1.Location.X, 366);
                        Win_AcqImageTool.Instance.ckb_autoSwitch.Visible = false;
                        Win_AcqImageTool.Instance.ckb_absPath.Visible = false;
                        //Win_AcqImageTool.Instance.vcb_ReadQe.Visible = true;
                        //Win_AcqImageTool.Instance.vcb_WriteQe.Visible = true;

                        if (Win_FromDevice.Instance.cbx_deviceList.Text != "")
                        {
                            this.SwitchDevice(Win_FromDevice.Instance.cbx_deviceList.Text);
                        }
                        break;

                    case ImageSourceMode.FromFile:
                        Win_AcqImageTool.Instance.radio_FromLocalFile.Checked = true;
                        Win_AcqImageTool.Instance.pic_fromDevice.Image = Resources.去勾选;
                        Win_AcqImageTool.Instance.pic_fromLocalFile.Image = Resources.勾选;
                        Win_AcqImageTool.Instance.pic_fromLocalDirectory.Image = Resources.去勾选;
                        Win_AcqImageTool.Instance.rdo_fromDevice.ForeColor = Color.Gray;
                        Win_AcqImageTool.Instance.radio_FromLocalFile.ForeColor = Color.White;
                        Win_AcqImageTool.Instance.rdo_fromLocalDirectory.ForeColor = Color.Gray;
                        Win_AcqImageTool.Instance.rdo_fromDevice.Font = new Font(Win_AcqImageTool.Instance.rdo_fromDevice.Font.Name, Win_AcqImageTool.Instance.rdo_fromDevice.Font.Size, FontStyle.Regular);
                        Win_AcqImageTool.Instance.radio_FromLocalFile.Font = new Font(Win_AcqImageTool.Instance.radio_FromLocalFile.Font.Name, Win_AcqImageTool.Instance.radio_FromLocalFile.Font.Size, FontStyle.Bold);
                        Win_AcqImageTool.Instance.rdo_fromLocalDirectory.Font = new Font(Win_AcqImageTool.Instance.rdo_fromLocalDirectory.Font.Name, Win_AcqImageTool.Instance.rdo_fromLocalDirectory.Font.Size, FontStyle.Regular);

                        Win_AcqImageTool.Instance.pnl_formPanel.Controls.Clear();
                        Win_FromLocal.Instance.TopLevel = false;
                        Win_FromLocal.Instance.Parent = Win_AcqImageTool.Instance.pnl_formPanel;
                        Win_FromLocal.Instance.Dock = DockStyle.Top;
                        Win_FromLocal.Instance.Show();

                        Win_AcqImageTool.Instance.ckb_hardware.Visible = false;
                        //Win_AcqImageTool.Instance.cCheckBox1.Location = new System.Drawing.Point(Win_AcqImageTool.Instance.cCheckBox1.Location.X, 316);
                        Win_AcqImageTool.Instance.ckb_autoSwitch.Visible = false;
                        Win_FromLocal.Instance.pnl_multImage.Visible = false;
                        Win_FromLocal.Instance.btn_browseImage.Visible = false;
                        //Win_AcqImageTool.Instance.vcb_ReadQe.Visible = true;
                        //Win_AcqImageTool.Instance.vcb_WriteQe.Visible = true;
                        Win_FromLocal.Instance.pnl_multImage.Focus();
                        break;

                    case ImageSourceMode.FromDirectory:
                        Win_AcqImageTool.Instance.rdo_fromLocalDirectory.Checked = true;
                        Win_AcqImageTool.Instance.pic_fromDevice.Image = Resources.去勾选;
                        Win_AcqImageTool.Instance.pic_fromLocalFile.Image = Resources.去勾选;
                        Win_AcqImageTool.Instance.pic_fromLocalDirectory.Image = Resources.勾选;
                        Win_AcqImageTool.Instance.rdo_fromDevice.ForeColor = Color.Gray;
                        Win_AcqImageTool.Instance.radio_FromLocalFile.ForeColor = Color.Gray;
                        Win_AcqImageTool.Instance.rdo_fromLocalDirectory.ForeColor = Color.White;
                        Win_AcqImageTool.Instance.rdo_fromDevice.Font = new Font(Win_AcqImageTool.Instance.rdo_fromDevice.Font.Name, Win_AcqImageTool.Instance.rdo_fromDevice.Font.Size, FontStyle.Regular);
                        Win_AcqImageTool.Instance.radio_FromLocalFile.Font = new Font(Win_AcqImageTool.Instance.radio_FromLocalFile.Font.Name, Win_AcqImageTool.Instance.radio_FromLocalFile.Font.Size, FontStyle.Regular);
                        Win_AcqImageTool.Instance.rdo_fromLocalDirectory.Font = new Font(Win_AcqImageTool.Instance.rdo_fromLocalDirectory.Font.Name, Win_AcqImageTool.Instance.rdo_fromLocalDirectory.Font.Size, FontStyle.Bold);

                        Win_AcqImageTool.Instance.pnl_formPanel.Controls.Clear();
                        Win_FromLocal.Instance.TopLevel = false;
                        Win_FromLocal.Instance.Parent = Win_AcqImageTool.Instance.pnl_formPanel;
                        Win_FromLocal.Instance.Dock = DockStyle.Top;
                        Win_FromLocal.Instance.Show();

                        Win_AcqImageTool.Instance.ckb_hardware.Visible = false;
                        //Win_AcqImageTool.Instance.cCheckBox1.Location = new System.Drawing.Point(Win_AcqImageTool.Instance.cCheckBox1.Location.X, 316);
                        Win_AcqImageTool.Instance.ckb_autoSwitch.Visible = true;
                        Win_AcqImageTool.Instance.ckb_absPath.Visible = true;
                        Win_FromLocal.Instance.pnl_multImage.Visible = true;
                        Win_FromLocal.Instance.btn_browseImage.Visible = true;
                        //Win_AcqImageTool.Instance.vcb_ReadQe.Visible = true;
                        //Win_AcqImageTool.Instance.vcb_WriteQe.Visible = true;
                        Win_FromLocal.Instance.pnl_multImage.Focus();
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        /// <summary>
        /// 切换相机
        /// </summary>
        /// <param name="deviceDescriptionStr">设备信息字符串</param>
        internal void SwitchDevice(string cameraInfoStr)
        {
            try
            {
                if (Win_FromDevice.Instance.cbx_deviceList.Text == "")
                {
                    this.SDK_Camera = null;
                }
                else
                {
                    for (int i = 0; i < L_devices.Count; i++)
                    {
                        if (L_devices[i].CameraInfoStr == cameraInfoStr)
                        {
                            Win_AcqImageTool.Instance.Plbl_toolTip.ForeColor = Color.White;
                            Win_AcqImageTool.Instance.Plbl_toolTip.Text = "当前相机：" + cameraInfoStr;
                            this.SDK_Camera = L_devices[i];
                            if (this.hardTrigger)
                            {
                                this.triggerModel = DeviceTriggerModel.hardware;
                                this.SDK_Camera.SetCamTriggerModel(DeviceTriggerModel.hardware);
                            }
                            else
                            {
                                this.triggerModel = DeviceTriggerModel.software;
                                this.SDK_Camera.SetCamTriggerModel(DeviceTriggerModel.software);
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        /// <summary>
        /// 复位工具
        /// </summary>
        internal void ResetTool()
        {
            try
            {
                imagePath = string.Empty;
                imageDirectoryPath = string.Empty;
                if (imageSourceMode != ImageSourceMode.FromDevice)            //避免切换闪烁
                    SwitchImageSource(ImageSourceMode.FromDevice);

                Win_FromDevice.Instance.cbx_deviceList.Text = string.Empty;
                Win_FromDevice.Instance.tbx_exposure.Text = "0";
                Win_FromLocal.Instance.tbx_imagePath.Text = string.Empty;
                Win_FromLocal.Instance.tbx_imageDirectoryPath.Text = string.Empty;

                Win_AcqImageTool.Instance.PImageWin.ClearWindow();
                Win_AcqImageTool.Instance.Plbl_toolTip.ForeColor = Color.White;
                Win_AcqImageTool.Instance.Plbl_toolTip.Text = "状态：无";
                Win_AcqImageTool.Instance.Plbl_runTime.Text = "耗时：0ms";
                Win_AcqImageTool.Instance.ckb_autoSwitch.Checked = true;
                Win_AcqImageTool.Instance.ckb_RGBToGray.Checked = true;
                Win_AcqImageTool.Instance.ckb_displayAllImageRegion.Checked = true;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        /// <summary>
        /// 选择图像文件路径
        /// </summary>
        internal void SelectImagePath()
        {
            try
            {
                System.Windows.Forms.OpenFileDialog dig_openFileDialog = new System.Windows.Forms.OpenFileDialog();
                dig_openFileDialog.Title = ( "请选择图像文件路径");
                if (imagePath == string.Empty)
                {
                    if (absPath)
                        dig_openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    else
                        dig_openFileDialog.InitialDirectory = Application.StartupPath;
                }
                else
                {
                    if (absPath)
                        dig_openFileDialog.InitialDirectory = Path.GetDirectoryName(imagePath);
                    else
                        dig_openFileDialog.InitialDirectory = Application.StartupPath + imagePath;
                }
                dig_openFileDialog.Filter = ("图像文件(*.*)|*.*|图像文件(*.tif)|*.tif|图像文件(*.png)|*.png|图像文件(*.jpg)|*.jpg|图像文件(*.bmp)|*.bmp");
                if (dig_openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (!absPath)         //相对路径模式下，只允许选择程序输出目录下的路径
                    {
                        if (!dig_openFileDialog.FileName.StartsWith(Application.StartupPath))
                        {
                            Win_MessageBox.Instance.MessageBoxShow("\r\n相对路径模式下只能指定程序输出目录下的路径，路径指定失败");
                            return;
                        }
                        else
                        {
                            imagePath = dig_openFileDialog.FileName.Substring(Application.StartupPath.Length, dig_openFileDialog.FileName.Length - Application.StartupPath.Length);
                        }
                    }
                    else
                    {
                        imagePath = dig_openFileDialog.FileName;
                    }
                    Win_FromLocal.Instance.tbx_imagePath.Text = imagePath;

                    HImage image=new HImage();
                    try
                    {
                        image.ReadImage(dig_openFileDialog.FileName);
                        if (RGBToGray)
                        {
                            HTuple channel;
                            HOperatorSet.CountChannels(image, out channel);
                            if (channel == 3)
                                image= image.Rgb1ToGray();
                        }
                    }
                    catch
                    {
                        Win_AcqImageTool.Instance.Plbl_toolTip.ForeColor = Color.Red;
                        Win_AcqImageTool.Instance.Plbl_toolTip.Text ="图像文件异常或路径不合法（错误代码：0102）";
                        return;
                    }
                    toolPar.ResultPar.图像 = image;
                    Win_AcqImageTool.Instance.PImageWin.HobjectToHimage(image);
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        /// <summary>
        /// 选择图像文件夹路径
        /// </summary>
        internal void SelectImageDirectoryPath()
        {
            try
            {
                VControls.FolderBrowserDialog _sampleVistaFolderBrowserDialog = new VControls.FolderBrowserDialog();
                if (imageDirectoryPath == string.Empty)
                {
                    if (absPath)
                        _sampleVistaFolderBrowserDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    else
                        _sampleVistaFolderBrowserDialog.SelectedPath = Application.StartupPath;
                }
                else
                {
                    if (absPath)
                        _sampleVistaFolderBrowserDialog.SelectedPath = imageDirectoryPath;
                    else
                        _sampleVistaFolderBrowserDialog.SelectedPath = Application.StartupPath + imageDirectoryPath;
                }
                _sampleVistaFolderBrowserDialog.Description = ("请选择图像文件夹路径");
                if (_sampleVistaFolderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (!absPath)
                    {
                        if (!_sampleVistaFolderBrowserDialog.SelectedPath.StartsWith(Application.StartupPath))
                        {
                            Win_MessageBox.Instance.MessageBoxShow("\r\n相对路径模式下只能指定程序输出目录下的路径，路径指定失败");
                            return;
                        }
                        else
                        {
                            imageDirectoryPath = _sampleVistaFolderBrowserDialog.SelectedPath.Substring(Application.StartupPath.Length, _sampleVistaFolderBrowserDialog.SelectedPath.Length - Application.StartupPath.Length);
                        }
                    }
                    else
                    {
                        imageDirectoryPath = _sampleVistaFolderBrowserDialog.SelectedPath;
                    }
                    Win_FromLocal.Instance.tbx_imageDirectoryPath.Text = imageDirectoryPath;

                    L_images.Clear();
                    string[] files = Directory.GetFiles(_sampleVistaFolderBrowserDialog.SelectedPath);
                    for (int i = 0; i < files.Length; i++)
                    {
                        FileInfo fileInfo = new FileInfo(files[i]);
                        if (fileInfo.Extension == ".jpg" || fileInfo.Extension == ".bmp" || fileInfo.Extension == ".png" || fileInfo.Extension == ".tif")
                            L_images.Add(files[i]);
                    }
                    if (L_images.Count > 0)
                    {
                        HImage image = new HImage();
                        try
                        {
                            image.ReadImage(L_images[0]);
                            if (RGBToGray)
                            {
                                HTuple channel;
                                HOperatorSet.CountChannels(image, out channel);
                                if (channel == 3)
                                    image = image.Rgb1ToGray();
                            }
                        }
                        catch
                        {
                            Win_AcqImageTool.Instance.Plbl_toolTip.ForeColor = Color.Red;
                            Win_AcqImageTool.Instance.Plbl_toolTip.Text = ("图像文件异常或路径不合法（错误代码：0102）");
                            return;
                        }
                        currentImageIndex = 0;
                        currentImageName = Path.GetFileName(L_images[0]);
                        toolPar.ResultPar.图像 = image;
                        Win_AcqImageTool.Instance.PImageWin.HobjectToHimage(image);
                        Win_AcqImageTool.Instance.Plbl_toolTip.ForeColor = Color.White;
                        Win_AcqImageTool.Instance.Plbl_toolTip.Text = string.Format("状态：当前图像：{0} ({1})", currentImageName, currentImageIndex + 1 + "/" + L_images.Count);
                    }
                    else
                    {
                        Win_AcqImageTool.Instance.Plbl_toolTip.ForeColor = Color.Red;
                        Win_AcqImageTool.Instance.Plbl_toolTip.Text = "状态：文件夹中无图像";
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        /// <summary>
        /// 读取文件夹中上一张图像
        /// </summary>
        internal void ReadLastImage()
        {
            try
            {
                HImage image=new HImage();
                currentImageIndex = currentImageIndex - 1;
                if (currentImageIndex < 0)
                    currentImageIndex = L_images.Count - 1;
                try
                {
                    image.ReadImage(L_images[currentImageIndex]);
                }
                catch
                {
                    Win_AcqImageTool.Instance.Plbl_toolTip.ForeColor = Color.Red;
                    Win_AcqImageTool.Instance.Plbl_toolTip.Text = ("图像文件异常或路径不合法（错误代码：0102）");
                    return;
                }
                currentImageName = Path.GetFileName(L_images[currentImageIndex]);
                Win_FromLocal.Instance.pnl_multImage.Focus();
                Win_AcqImageTool.Instance.PImageWin.HobjectToHimage(image);
                Win_AcqImageTool.Instance.Plbl_toolTip.Text = string.Format("状态：成功，当前图像：{0} ({1})", currentImageName, currentImageIndex + 1 + "/" + L_images.Count);
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        /// <summary>
        /// 读取文件夹中下一张图像
        /// </summary>
        internal void ReadNextImage()
        {
            try
            {
                HImage image=new HImage();
                currentImageIndex = currentImageIndex + 1;
                if (currentImageIndex > L_images.Count - 1)
                    currentImageIndex = 0;
                try
                {
                    image.ReadImage(L_images[currentImageIndex]);
                }
                catch
                {
                    Win_AcqImageTool.Instance.Plbl_toolTip.ForeColor = Color.Red;
                    Win_AcqImageTool.Instance.Plbl_toolTip.Text = ("图像文件异常或路径不合法（错误代码：0102）");
                    return;
                }
                currentImageName = Path.GetFileName(L_images[currentImageIndex]);
                Win_FromLocal.Instance.pnl_multImage.Focus();
                Win_AcqImageTool.Instance.PImageWin.HobjectToHimage(image);
                Win_AcqImageTool.Instance.Plbl_toolTip.Text = string.Format("状态：成功，当前图像：{0} ({1})", currentImageName, currentImageIndex + 1 + "/" + L_images.Count);
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        
        /// <summary>
        /// 相机设置曝光时间
        /// </summary>
        internal void SetCamExposure()
        {
            if (imageSourceMode == ImageSourceMode.FromDevice)
            {
                foreach (SDK_Base item in L_devices)
                {
                    if (item.CameraInfoStr == SDK_Camera.CameraInfoStr)
                    {
                        SDK_Camera = item;
                        break;
                    }
                }
                SDK_Camera.SetExposure(exposure);
            }
        }

        /// <summary>
        /// 相机设置触发模式
        /// </summary>
        internal void SetCamTriggerModel(DeviceTriggerModel triggerModel)
        {
            try
            {
                if (imageSourceMode == ImageSourceMode.FromDevice)
                {
                    foreach (SDK_Base item in L_devices)
                    {
                        if (item.CameraInfoStr == this.SDK_Camera.CameraInfoStr)
                        {
                            SDK_Camera = item;
                            break;
                        }
                    }
                    SDK_Camera.SetCamTriggerModel(triggerModel);
                }
            }
            catch
            { }
        }

        /// <summary>
        /// 相机实时
        /// </summary>
        internal void PlayImage(bool runTool, ImgView window)
        {
            try
            {
                if (SDK_Camera == null)
                {
                    Win_AcqImageTool.Instance.Plbl_toolTip.ForeColor = Color.Red;
                    Win_AcqImageTool.Instance.Plbl_toolTip.Text = "状态：未指定采集设备";
                    if (!runTool)
                        Win_Log.Instance.OutputMsg(string.Format("流程 [{0}] 相机实时失败，原因：未指定采集设备", jobName),Win_Log.InfoType.error);
                    return;
                }

                if (!displayImageMode)
                {
                    displayImageMode = true;
                    Win_FromDevice.Instance.btn_displayImage.Text = "停止实时";

                    Win_AcqImageTool.Instance.Pbtn_runTool.Enabled = false;
                    Win_FromDevice.Instance.btn_saveImage.Enabled = false;
                    Win_FromDevice.Instance.cbx_deviceList.Enabled = false;
                    Win_FromDevice.Instance.tbx_exposure.Enabled = false;

                    th_displayImage = new Thread(() =>
                    {
                        #region 相机实时
                        try
                        {
                            while (displayImageMode)
                            {
                                if (runTool)
                                {
                                    HTuple row1, col1, row2, col2;
                                    HOperatorSet.GetPart(window.hv_window, out row1, out col1, out row2, out col2);
                                    ViewWindow.DisMsg(window.hv_window, "实时中...",  ImgView.CoordSystem.window, row1 + (row2 - row1) / 30, col1 + (col2 - col1) / 30, "blue", "false");                
                                }
                                else
                                {
                                    ViewWindow.DisMsg(window.hv_window, "实时中...");
                                }

                                toolPar.ResultPar.图像 = SDK_Camera.GrabOneImage();

                                if (rotateImage)
                                {
                                    HImage image=new HImage();
                                    image= toolPar.ResultPar.图像.RotateImage((double)rotateAngle, "constant");
                                    toolPar.ResultPar.图像 = image;
                                }

                                //彩色图像转灰度图像
                                if (RGBToGray)
                                {
                                    HTuple channel;
                                    HOperatorSet.CountChannels(toolPar.ResultPar.图像, out channel);
                                    if (channel == 3)
                                    {
                                        HImage image=new HImage();
                                        image= toolPar.ResultPar.图像.Rgb1ToGray();
                                        toolPar.ResultPar.图像 = image;
                                    }
                                }

                                if (runTool)
                                {
                                    window.HobjectToHimage(toolPar.ResultPar.图像);
                                }
                                else
                                {
                                    window.hv_window.DispObj(toolPar.ResultPar.图像);
                                }

                                Thread.Sleep(10);
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.SaveError(Project.Instance.configuration.dataPath,ex);
                        }
                        #endregion
                    });
                    th_displayImage.IsBackground = true;
                    th_displayImage.Start();
                }
                else
                {
                    displayImageMode = false;
                    Win_FromDevice.Instance.btn_displayImage.Text = "实时采集";

                    Win_AcqImageTool.Instance.Pbtn_runTool.Enabled = true;
                    Win_FromDevice.Instance.btn_saveImage.Enabled = true;
                    Win_FromDevice.Instance.cbx_deviceList.Enabled = true;
                    Win_FromDevice.Instance.tbx_exposure.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        /// <summary>
        /// 图像另存
        /// </summary>
        internal void SaveImage()
        {
            try
            {
                System.Windows.Forms.SaveFileDialog dig_saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                dig_saveFileDialog.FileName = DateTime.Now.ToString("yyyy_MM_dd");
                dig_saveFileDialog.Title = ("请选择图像保存路径");
                dig_saveFileDialog.Filter = ("图像文件(*.tif)|*.tif|图像文件(*.png)|*.png|图像文件(*.jpg)|*.jpg|图像文件(*.*)|*.*");
                dig_saveFileDialog.InitialDirectory = path;
                if (dig_saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        HOperatorSet.WriteImage(toolPar.ResultPar.图像, "tiff", 0, dig_saveFileDialog.FileName);
                    }
                    catch
                    {
                        Win_Main.Instance.OutputMsg( "图像文件异常或路径不合法（错误代码：0102）",Win_Log.InfoType.error);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        /// <summary>
        /// 刷新输出
        /// </summary>
        /// <param name="toolName">工具名称</param>
        internal void UpdateOutput(string toolName)
        {
            try
            {
                List<ToolIO> L_toolIO = Job.FindJobByName(jobName).FindToolInfoByName(toolName).output;
                for (int i = 0; i < L_toolIO.Count; i++)
                {
                    string outputItem = L_toolIO[i].IOName;
                    string[] items = Regex.Split(outputItem, " . ");
                    object value = toolPar;
                    value = GetValue(value, "ResultPar");
                    for (int j = 0; j < items.Length; j++)
                    {
                        value = GetValue(value, items[j]);
                    }
                    Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetOutput(outputItem).value = value;
                    Job.FindJobByName(jobName).GetToolIONodeByNodeText(jobName, toolName, outputItem, false).ToolTipText = Method.FormatShowTip(value);
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        /// <summary>
        /// 运行工具
        /// </summary>
        /// <param name="updateImage">是否更新图像</param>
        /// <param name="debugTool">调试工具模式</param>
        public override void Run(bool updateImage, bool runTool, string toolName)
        {
            try
            {
                lock (obj)
                {
                    toolRunStatu = (ToolRunStatu.未知原因);

                    // if (runTool)
                    //    Win_AcqImageTool.Instance.PImageWin.ClearWindow();

                    HImage image = new HImage();
                    switch (imageSourceMode)
                    {
                        case ImageSourceMode.FromDevice:
                            #region 从设备采集
                            if (displayImageMode)
                            {
                                toolRunStatu = ToolRunStatu.相机实时状态下不可采集图像;
                                Win_MessageBox.Instance.MessageBoxShow("\r\n相机实时状态下不可采集图像，请停止实时后重试", TipType.Error);
                                 GetImageWindowControl().ClearWindow();
                                return;
                            }

                            if (!isLoad)
                            {
                                //设备取像，软件处于非加载状态时
                                if (SDK_Camera == null)
                                {
                                    toolRunStatu = (ToolRunStatu.未指定采集设备);
                                    GetImageWindowControl().ClearWindow();
                                    if (Win_AcqImageTool.Instance.Visible)
                                    {
                                        Win_AcqImageTool.Instance.PImageWin.ClearWindow();
                                        Win_AcqImageTool.Instance.Plbl_toolTip.ForeColor = Color.Red;
                                        Win_AcqImageTool.Instance.Plbl_toolTip.Text = "状态：未指定采集设备";
                                    }
                                    return;
                                }
                                if (!SDK_Camera.CheckCamExist())
                                {
                                    toolRunStatu = (ToolRunStatu.相机未连接);
                                    GetImageWindowControl().ClearWindow();
                                    return;
                                }

                                if (triggerModel == DeviceTriggerModel.hardware)
                                {
                                    foreach (SDK_Base item in L_devices)
                                    {
                                        if (item.CameraInfoStr == SDK_Camera.CameraInfoStr)
                                        {
                                            SDK_Camera = item;
                                            break;
                                        }
                                    }

                                    if (Start.JobSwitchMark_Hard)//如果硬触发切换方案时，选择从文件夹加载读取
                                    {
                                        Start.JobSwitchMark_Hard = false;
                                        HImage loadImage = new HImage();
                                        try
                                        {
                                            loadImage.ReadImage(string.Format(Application.StartupPath + "\\ProjectImage\\{0}.bmp", jobName));
                                        }
                                        catch
                                        {
                                            //创建一个空图片
                                            loadImage.GenImageConst("byte", 4096, 3000);
                                        }
                                        (SDK_Camera as SDK_HIKVision).QhImages.Enqueue(loadImage);
                                    }

                                    Start.JobRunMarkM = false;
                                    while (true)
                                    {
                                        if ((SDK_Camera as SDK_HIKVision).QhImages.Count > 0 || Start.JobRunMarkM)
                                        {
                                            Start.ImaEnqueueTool = false;
                                            break;
                                        }
                                        System.Threading.Thread.Sleep(10);
                                    }
                                    //hzy20220622
                                    if (Start.JobRunMarkM)
                                    {
                                        Start.isCamBreakWhile = true;
                                        toolRunStatu = ToolRunStatu.采集图像时出错;
                                        //GetImageWindowControl().ClearWindow();
                                        return;
                                    }

                                    toolPar.ResultPar.图像 = (SDK_Camera as SDK_HIKVision).QhImages.Dequeue();
                                }
                                else if (triggerModel == DeviceTriggerModel.software)
                                {
                                   
                                    //海康相机，实测发现设置完曝光后200毫秒以后才能生效，这会影响CT，此处禁用
                                    //SDK_Camera.SetExposure(exposure);

                                    //从设备获取图像

                                    foreach (SDK_Base item in L_devices)
                                    {
                                        if (item.CameraInfoStr == SDK_Camera.CameraInfoStr)
                                        {
                                            SDK_Camera = item;
                                            break;
                                        }
                                    }
                                    toolPar.ResultPar.图像 = SDK_Camera.GrabOneImage();

                                    Start.JobSwitchMark_Soft = false;//如果软触发切换方案时，选择从设备获取
                                    //toolPar.ResultPar.图像 = L_devices[1].GrabOneImage();

                                    if (toolPar.ResultPar.图像 == null)
                                    {
                                        toolRunStatu = ToolRunStatu.采集图像时出错;
                                        GetImageWindowControl().ClearWindow();
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                //设备取像，软件处于加载状态时，读取固定图像，用来打开软件
                                HImage loadImage = new HImage();
                                try
                                {
                                    loadImage.ReadImage(string.Format(Application.StartupPath + "\\ProjectImage\\{0}.bmp", jobName));
                                    toolPar.ResultPar.图像 = loadImage;
                                }
                                catch 
                                {
                                    //创建一个空图片
                                    loadImage.GenImageConst("byte", 4096, 3000);
                                    toolPar.ResultPar.图像 = loadImage;
                                }
                            }

                            break;
                            #endregion

                        case ImageSourceMode.FromFile:
                            #region 从文件读取
                            if (imagePath == string.Empty)
                            {
                                toolRunStatu = (ToolRunStatu.未指定图像路径);
                                GetImageWindowControl().ClearWindow();
                                if (Win_AcqImageTool.Instance.Visible)
                                {
                                    Win_AcqImageTool.Instance.PImageWin.ClearWindow();
                                    Win_AcqImageTool.Instance.Plbl_toolTip.ForeColor = Color.Red;
                                    Win_AcqImageTool.Instance.Plbl_toolTip.Text = "状态：未指定图像路径";
                                }

                                //return;
                                //hzy20220523,解决路径为空，软件加载图片假死异常
                                image.GenImageConst("byte", 4096, 3000);
                                toolPar.ResultPar.图像 = image;
                            }
                            else
                            {
                                try
                                {
                                    if (absPath)
                                    {
                                        image.ReadImage(imagePath);
                                    }
                                    else
                                    {
                                       
                                        image.ReadImage(Application.StartupPath + imagePath);
                                    }

                                    toolPar.ResultPar.图像 = image;
                                }
                                catch
                                {
                                    //hzy20220523,解决路径错误，软件加载图片假死异常
                                    image.GenImageConst("byte", 4096, 3000);
                                    toolPar.ResultPar.图像 = image;
                                }
                            }

                            break;
                            #endregion

                        case ImageSourceMode.FromDirectory:
                            #region 从文件夹读取
                            if (imageDirectoryPath == string.Empty)
                            {
                                toolRunStatu = (ToolRunStatu.未指定图像路径);
                                if (GetImageWindowControl().Image != null)
                                {
                                    GetImageWindowControl().ClearWindow();
                                    if (Win_AcqImageTool.Instance.Visible)
                                    {
                                        Win_AcqImageTool.Instance.PImageWin.ClearWindow();
                                        Win_AcqImageTool.Instance.Plbl_toolTip.ForeColor = Color.Red;
                                        Win_AcqImageTool.Instance.Plbl_toolTip.Text = "状态：未指定图像路径";
                                    }
                                }
                                //return;

                                //hzy20220523,解决路径为空，软件加载图片假死异常
                                image.GenImageConst("byte", 4096, 3000);
                            }
                            else
                            {

                                //更新一下文件夹下面的图像
                                string[] files = new string[] { };
                                try
                                {
                                    if (absPath)
                                    {
                                        files = Directory.GetFiles(imageDirectoryPath);
                                    }
                                    else
                                    {
                                        files = Directory.GetFiles(Application.StartupPath + imageDirectoryPath);
                                    }


                                    L_images.Clear();

                                    for (int i = 0; i < files.Length; i++)
                                    {
                                        FileInfo fileInfo = new FileInfo(files[i]);
                                        if (fileInfo.Extension == ".jpg" || fileInfo.Extension == ".bmp" || fileInfo.Extension == ".png" || fileInfo.Extension == ".tif")
                                            L_images.Add(files[i]);
                                    }

                                    if (autoSwitch)
                                    {
                                        currentImageIndex++;
                                    }

                                    if (currentImageIndex > L_images.Count - 1)
                                    {
                                        currentImageIndex = 0;
                                    }

                                    if (L_images.Count == 0)
                                    {
                                        Win_Main.Instance.OutputMsg("图像路径下无有效图像文件", Win_Log.InfoType.tip);
                                        toolRunStatu = ToolRunStatu.文件夹内无图像;
                                        GetImageWindowControl().ClearWindow();
                                        //return;

                                        //hzy20220523,解决图像文件夹为空，软件加载图片假死异常
                                        image.GenImageConst("byte", 4096, 3000);
                                    }
                                    else
                                    {
                                        image.ReadImage(L_images[currentImageIndex]);
                                        currentImageName = Path.GetFileName(L_images[currentImageIndex]);
                                    }
                                }
                                catch
                                {
                                    Win_Main.Instance.OutputMsg("路径不合法（错误代码：10106）", Win_Log.InfoType.error);
                                    GetImageWindowControl().ClearWindow();
                                    //return;

                                    //hzy20220523,解决路径错误，软件加载图片假死异常
                                    image.GenImageConst("byte", 4096, 3000);
                                }
                            }

                            toolPar.ResultPar.图像 = image;

                            break;
                            #endregion
                    }

                    //彩色图像转灰度图像
                    if (RGBToGray)
                    {
                        HTuple channel;
                        HOperatorSet.CountChannels(toolPar.ResultPar.图像, out channel);
                        if (channel == 3)
                        {
                            HImage image1=new HImage();
                            image1= toolPar.ResultPar.图像.Rgb1ToGray();
                            toolPar.ResultPar.图像 = image1;
                        }
                    }

                    //旋转图像
                    if (rotateImage)
                    {
                        HImage image2 = new HImage();
                        image2= toolPar.ResultPar.图像.RotateImage((double)rotateAngle, "constant");
                        toolPar.ResultPar.图像 = image2;
                    }

                    if (runTool)
                    {
                        if (Win_AcqImageTool.Instance.Visible)
                        {
                            Win_AcqImageTool.Instance.PImageWin.HobjectToHimage(toolPar.ResultPar.图像);
                            if (!displayAllImageRegion)
                            {
                                Win_AcqImageTool.Instance.PImageWin.viewWindow.displayROI(L_regions);
                                Win_AcqImageTool.Instance.L_regions = this.L_regions;
                            }
                        }
                    }
                    else
                    {
                        //自动运行时显示局部图像
                        if (!displayAllImageRegion && Machine.machineRunStatu == MachineRunStatu.Running)
                        {
                            HOperatorSet.SetPart( GetImageWindowControl().hv_window, L_regions[0].getModelData()[0], L_regions[0].getModelData()[1], L_regions[0].getModelData()[2], L_regions[0].getModelData()[3]);
                        }

                        //GetImageWindowControl().ClearWindow();
                        GetImageWindowControl().BeginInvoke(new Action(() =>
                        {
                            GetImageWindowControl().Image = (toolPar.ResultPar.图像);
                        }));

                        if (Win_AcqImageTool.Instance.Visible && Win_AcqImageTool.Instance.jobName == jobName && Win_AcqImageTool.Instance.toolName == toolName)
                            Win_AcqImageTool.Instance.PImageWin.HobjectToHimage(toolPar.ResultPar.图像);

                        //显示文件夹中图片信息
                        if (imageSourceMode != ImageSourceMode.FromDevice)
                        {
                            if (imageSourceMode != ImageSourceMode.FromFile)
                            {
                                if (Win_AcqImageTool.Instance.Visible)
                                {
                                    Win_AcqImageTool.Instance.Plbl_toolTip.ForeColor = Color.White;
                                    Win_AcqImageTool.Instance.Plbl_toolTip.Text = string.Format("状态：成功，当前图像：{0} ({1})", currentImageName, currentImageIndex + 1 + "/" + L_images.Count);
                                }
                            }
                        }
                    }

                    toolRunStatu = (  ToolRunStatu.成功);
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }


        private object objLock = new object();
        /// <summary>
        /// 读写图片
        /// </summary>
        /// <param name="bReadOrWrite">读写模式：true,写；false,读；</param>
        /// <param name="bGetModel">读取模式：true，取但不删除；false,取但删除</param>
        internal HImage ReadOrWriteImage(bool bReadOrWrite, bool bGetModel = false, HImage himage = null)
        {
            if (objLock == null)
            {
                objLock = new object();
            }
            lock (objLock)
            {
                if (bReadOrWrite)
                {
                    //写图片
                    QeHImages.Enqueue(himage);
                    return null;
                }
                else
                {
                    HImage image = null;    
                    //读图片
                    if (bGetModel)
                    {
                        //取图片，但不删除
                        image = QeHImages.Peek();
                    }
                    else
                    {
                        //读图片，但删除
                        image = QeHImages.Dequeue();
                    }
                    return image;
                }
            }
        }


        #region 参数
        [Serializable]
        public class ToolPar : ToolParBase
        {
            private InputPar _inputPar = new InputPar();
            public InputPar InputPar
            {
                get { return _inputPar; }
                set { _inputPar = value; }
            }

            private RunPar _runPar = new RunPar();
            public RunPar RunPar
            {
                get { return _runPar; }
                set { _runPar = value; }
            }

            private ResultPar _resultPar = new ResultPar();
            public ResultPar ResultPar
            {
                get { return _resultPar; }
                set { _resultPar = value; }
            }
        }

        [Serializable]
        public class InputPar { }

        [Serializable]
        public class RunPar { }

        [Serializable]
        internal class ResultPar
        {
            private HImage _图像;
            public HImage 图像
            {
                get 
                {
                    return _图像;
                }
                set 
                { 
                    _图像 = value;
                }
            }

        }
        #endregion

    }
    /// <summary>
    /// 图像源模式：从设备采集 | 读取图像文件 | 读取文件夹图像
    /// </summary>
    internal enum ImageSourceMode
    {
        FromDevice,
        FromFile,
        FromDirectory,
    }

}
