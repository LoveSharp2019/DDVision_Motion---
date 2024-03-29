using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basler.Pylon;
using HalconDotNet;
using System.Runtime.InteropServices;
using System.Windows;
using System.Threading;
using System.Drawing;
using VSE.Core;
using VControls;

namespace VSE
{
    /// <summary>
    /// 巴斯勒相机
    /// </summary>
    [Serializable]
    internal class SDK_Basler : SDK_Base
    {
        internal SDK_Basler(string cameraInfoStr)
        {
            this.CameraInfoStr = cameraInfoStr;
            if (D_cameras == null)
            {
                D_cameras = new Dictionary<string, Basler.Pylon.Camera>();
                L_cameraInfo = new List<Basler.Pylon.ICameraInfo>();
            }
        }

        /// <summary>
        /// 锁
        /// </summary>
        object obj = new object();
        /// <summary>
        /// 相机集合   键：信息字符串  值：相机对象
        /// </summary>
        private static Dictionary<string, Basler.Pylon.Camera> D_cameras = new Dictionary<string, Camera>();
        /// <summary>
        /// 相机列表
        /// </summary>
        private static List<ICameraInfo> L_cameraInfo;


        /// <summary>
        /// 枚举相机
        /// </summary>
        /// <returns>是否成功</returns>
        internal static bool EnumCamrea()
        {
            try
            {
                Machine.UpdateStep(25, "正在枚举巴斯勒相机", true);
                L_cameraInfo = CameraFinder.Enumerate();
                int camIndex = 0;
                foreach (ICameraInfo item in L_cameraInfo)
                {
                    camIndex++;
                    string sn = item[CameraInfoKey.SerialNumber];
                    string IPAddress = item[CameraInfoKey.DeviceIpAddress];
                    string macAddress = item[CameraInfoKey.DeviceMacAddress];
                    string vendorName = item[CameraInfoKey.VendorName];
                    string friendlyName = item[CameraInfoKey.FriendlyName];

                    Basler.Pylon.Camera camera = new Basler.Pylon.Camera(item);
                    {
                        camera.CameraOpened += Basler.Pylon.Configuration.AcquireSingleFrame;
                        camera.ConnectionLost += OnConnectionLost;
                        try
                        {
                            camera.Open();
                        }
                        catch
                        {
                            Win_MessageBox.Instance.MessageBoxShow(string.Format("\r\n相机{0}初始化失败,相机信息：{1}\r\n可尝试重启电脑或5分钟后重启程序解决此问题", camIndex, friendlyName), TipType.Error);
                            continue;
                        }

                        camera.Parameters[PLCameraInstance.MaxNumBuffer].SetValue(5);
                        camera.Parameters[PLCamera.PixelFormat].SetValue(PLCamera.PixelFormat.Mono8);
                        string cameraInfoStr = string.Format("{0} | {1} | {2} | {3} | {4}", sn, vendorName, friendlyName, IPAddress, macAddress);
                        Machine.UpdateStep(25 + 10 * camIndex, string.Format("初始化巴斯勒相机{0}[{1}]", camIndex, cameraInfoStr), true);
                        SDK_Basler sdk_Basler = new SDK_Basler(cameraInfoStr);
                        D_cameras.Add(cameraInfoStr, camera);
                        AcqImageTool.L_devices.Add(sdk_Basler);
                        Win_FromDevice.Instance.cbx_deviceList.Items.Add(cameraInfoStr);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
                return false;
            }
        }
        /// <summary>
        /// 采图
        /// </summary>
        /// <returns>采集到的图像</returns>
        internal override HImage GrabOneImage()
        {
            try
            {
                lock (obj)
                {
                    HImage image = new HImage();
                    foreach (KeyValuePair<string, Basler.Pylon.Camera> item in D_cameras)
                    {
                        if (item.Key == CameraInfoStr)
                        {
                            IGrabResult grabResult;
                            try
                            {
                                grabResult = item.Value.StreamGrabber.GrabOne(5000, TimeoutHandling.ThrowException);
                            }
                            catch
                            {
                                Win_MessageBox.Instance.MessageBoxShow("采图异常", TipType.Error);
                                break;
                            }
                            using (grabResult)
                            {
                                if (grabResult.GrabSucceeded)
                                {
                                    byte[] buffer = grabResult.PixelData as byte[];
                                    GCHandle hand = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                                    IntPtr pr = hand.AddrOfPinnedObject();
                                    image.GenImage1( new HTuple("byte"), grabResult.Width, grabResult.Height, pr);
                                    if (hand.IsAllocated)
                                        hand.Free();
                                    break;
                                }
                            }
                        }
                    }
                    return image;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
                return null;
            }
        }
        /// <summary>
        /// 相机丢失触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnConnectionLost(Object sender, EventArgs e)
        {
            try
            {
                string sn = ((Basler.Pylon.Camera)sender).CameraInfo[CameraInfoKey.SerialNumber];
                string IPAddress = ((Basler.Pylon.Camera)sender).CameraInfo[CameraInfoKey.DeviceIpAddress];
                string macAddress = ((Basler.Pylon.Camera)sender).CameraInfo[CameraInfoKey.DeviceMacAddress];
                string vendorName = ((Basler.Pylon.Camera)sender).CameraInfo[CameraInfoKey.VendorName];
                string friendlyName = ((Basler.Pylon.Camera)sender).CameraInfo[CameraInfoKey.FriendlyName];

                string cameraInfoStr = string.Format("{0} | {1} | {2} | {3} | {4}", sn, vendorName, friendlyName, IPAddress, macAddress);
                D_cameras.Remove(cameraInfoStr);
                Win_Log.Instance.OutputMsg("相机连接中断，相机信息：" + cameraInfoStr,Win_Log.InfoType.error);
                Win_MessageBox.Instance.MessageBoxShow("\r\n相机连接中断，相机信息：" + cameraInfoStr, TipType.Error);
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        /// <summary>
        /// 设置相机曝光时间
        /// </summary>
        /// <param name="exposure">曝光时间</param>
        internal override void SetExposure(double exposure)
        {
            try
            {
                foreach (KeyValuePair<string, Basler.Pylon.Camera> item in D_cameras)
                {
                    if (item.Key == CameraInfoStr)
                    {
                        if (item.Value.Parameters.Contains(PLCamera.ExposureTimeAbs))
                            item.Value.Parameters[PLCamera.ExposureTimeAbs].TrySetValue(exposure * 1000);
                        else
                            item.Value.Parameters[PLCamera.ExposureTime].TrySetValue(exposure * 1000);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        /// <summary>
        /// 关闭所有相机
        /// </summary>
        public static void CloseAllCamera()
        {
            try
            {
                foreach (KeyValuePair<string, Basler.Pylon.Camera> item in D_cameras)
                {
                    item.Value.Close();
                    item.Value.Dispose();
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

    }
}
