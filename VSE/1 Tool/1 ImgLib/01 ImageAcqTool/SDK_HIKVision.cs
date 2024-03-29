using HalconDotNet;
using MvCamCtrl.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using VControls;
using VSE.Core;

namespace VSE
{
    /// <summary>
    /// 海康威视相机
    /// </summary>
    [Serializable]
    internal class SDK_HIKVision : SDK_Base
    {
        internal SDK_HIKVision(string cameraInfoStr)
        {
            this.CameraInfoStr = cameraInfoStr;
        }

        /// <summary>
        /// 锁
        /// </summary>
        private object obj = new object();

        internal static List<DeviceInfo> deviceList = new List<DeviceInfo>();

        /// <summary>
        /// 相机集合   键：信息字符串  值：相机对象
        /// </summary>
        internal static Dictionary<string, MyCamera> D_cameras = new Dictionary<string, MyCamera>();

        MyCamera.cbOutputExdelegate cbImage;

        [NonSerialized]
        public Queue<HImage> QhImages = new Queue<HImage>();

        //[NonSerialized]
        //public int QhImagesCount = 0;

        /// <summary>
        /// 枚举相机
        /// </summary>
        /// <returns>是否成功</returns>
        internal static bool EnumCamrea()
        {
            try
            {
                Machine.UpdateStep(25, "正在枚举海康威视相机", true);
                deviceList.Clear();

                //cbImage = new MyCamera.cbOutputExdelegate(ImageCallBack);//实例化并注册回调函数
                //GC.KeepAlive(cbImage);

                int nRet = MyCamera.MV_OK;
                MyCamera myCamera = new MyCamera();
                MyCamera.MV_CC_DEVICE_INFO_LIST stDevList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
                nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref stDevList);
                
                if (MyCamera.MV_OK == nRet)
                {
                    MyCamera.MV_CC_DEVICE_INFO stDevInfo;
                    int camIndex = 0;
                    for (Int32 i = 0; i < stDevList.nDeviceNum; i++)
                    {
                        camIndex++;
                        DeviceInfo deviceinfo = new DeviceInfo();
                        stDevInfo = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(stDevList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                        deviceinfo.device = stDevInfo;
                        if (MyCamera.MV_GIGE_DEVICE == stDevInfo.nTLayerType)
                        {
                            MyCamera.MV_GIGE_DEVICE_INFO stGigEDeviceInfo = (MyCamera.MV_GIGE_DEVICE_INFO)MyCamera.ByteToStruct(stDevInfo.SpecialInfo.stGigEInfo, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                            uint nIp1 = ((stGigEDeviceInfo.nCurrentIp & 0xff000000) >> 24);
                            uint nIp2 = ((stGigEDeviceInfo.nCurrentIp & 0x00ff0000) >> 16);
                            uint nIp3 = ((stGigEDeviceInfo.nCurrentIp & 0x0000ff00) >> 8);
                            uint nIp4 = (stGigEDeviceInfo.nCurrentIp & 0x000000ff);
                            deviceinfo.serialNumber = stGigEDeviceInfo.chSerialNumber;
                            deviceinfo.ipAddress = string.Format("{0}.{1}.{2}.{3}", nIp1, nIp2, nIp3, nIp4);
                            deviceinfo.macAddress = stGigEDeviceInfo.nCurrentSubNetMask.ToString();
                            deviceinfo.vendorName = stGigEDeviceInfo.chModelName;
                            deviceinfo.friendlyName = stGigEDeviceInfo.chManufacturerName;

                            //stDevInfo = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(stDevList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                            //nRet = myCamera.MV_CC_CreateDevice_NET(ref stDevInfo);
                            //nRet = myCamera.MV_CC_OpenDevice_NET();
                            //hzy20220515,Modify
                            //开启相机触发模式
                            //nRet = myCamera.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON);
                            //设置相机触发模式为软触发
                            //nRet = myCamera.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE);
                            //设置相机触发模式为硬触发
                            //nRet = myCamera.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE0);
                            //nRet = myCamera.MV_CC_RegisterImageCallBackEx_NET(cbImage, (IntPtr)i);
                            //GC.KeepAlive(cbImage);

                            //nRet = myCamera.MV_CC_StartGrabbing_NET();
                            //MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
                            //nRet = myCamera.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);
                            //UInt32 nPayloadSize = stParam.nCurValue;

                            deviceinfo.cameraInfoStr = string.Format("{0} | {1} | {2} | {3} | {4}", deviceinfo.serialNumber, deviceinfo.ipAddress, deviceinfo.macAddress, deviceinfo.vendorName, deviceinfo.friendlyName);
                            Machine.UpdateStep(25 + 10 * camIndex, string.Format("初始化海康威视相机{0}[{1}]", camIndex, deviceinfo.cameraInfoStr), true);

                            //SDK_HIKVision sdk_HIKVision = new SDK_HIKVision(cameraInfoStr);
                            //myCamera.MV_CC_RegisterImageCallBackEx_NET(cbImage, (IntPtr)i);
                            //D_cameras.Add(cameraInfoStr, myCamera);
                            //AcqImageTool.L_devices.Add(sdk_HIKVision);

                            Win_FromDevice.Instance.cbx_deviceList.Items.Add(deviceinfo.cameraInfoStr);
                        }
                        else if (MyCamera.MV_USB_DEVICE == stDevInfo.nTLayerType)
                        {
                            MyCamera.MV_USB3_DEVICE_INFO stUsb3DeviceInfo = (MyCamera.MV_USB3_DEVICE_INFO)MyCamera.ByteToStruct(stDevInfo.SpecialInfo.stUsb3VInfo, typeof(MyCamera.MV_USB3_DEVICE_INFO));                           
                            deviceinfo.serialNumber = stUsb3DeviceInfo.chSerialNumber;
                            deviceinfo.ipAddress = "169.252.169.254";
                            deviceinfo.macAddress = "169.254.169.254";
                            deviceinfo.vendorName = stUsb3DeviceInfo.chModelName;
                            deviceinfo.friendlyName = stUsb3DeviceInfo.chManufacturerName;

                            //stDevInfo = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(stDevList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                            //nRet = myCamera.MV_CC_CreateDevice_NET(ref stDevInfo);
                            //nRet = myCamera.MV_CC_OpenDevice_NET();
                            //注册回调函数，软件异常，闪退
                            //nRet = myCamera.MV_CC_RegisterImageCallBackEx_NET(cbImage, (IntPtr)i);
                            //Hzy20220512,Add,确保对对象的引用存在
                            //GC.KeepAlive(cbImage);

                            //nRet = myCamera.MV_CC_StartGrabbing_NET();
                            //MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
                            //nRet = myCamera.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);
                            //UInt32 nPayloadSize = stParam.nCurValue;

                            deviceinfo.cameraInfoStr = string.Format("{0} | {1} | {2} | {3} | {4}", deviceinfo.serialNumber, deviceinfo.ipAddress, deviceinfo.macAddress, deviceinfo.vendorName, deviceinfo.friendlyName);
                            Machine.UpdateStep(25 + 10 * camIndex, string.Format("初始化海康威视相机{0}[{1}]", camIndex, deviceinfo.cameraInfoStr), true);
                            
                            //SDK_HIKVision sdk_HIKVision = new SDK_HIKVision(cameraInfoStr);
                            //注册回调函数，失败,软件能打开
                            //nRet = myCamera.MV_CC_RegisterImageCallBackEx_NET(cbImage, (IntPtr)i);
                            //D_cameras.Add(cameraInfoStr, myCamera);
                            //AcqImageTool.L_devices.Add(sdk_HIKVision);

                            Win_FromDevice.Instance.cbx_deviceList.Items.Add(deviceinfo.cameraInfoStr);
                        }

                        deviceList.Add(deviceinfo);
                    }
                }
                else
                {
                    Win_MessageBox.Instance.MessageBoxShow("枚举相机失败", TipType.Error);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
                return false;
            }
        }


        internal bool OpenCamera(DeviceInfo deviceinfo,out MyCamera myCamera)
        {
            myCamera = new MyCamera();
            int nRet = MyCamera.MV_OK;
            // ch:创建设备 | en:Create device
            nRet = myCamera.MV_CC_CreateDevice_NET(ref deviceinfo.device);
            if (MyCamera.MV_OK != nRet)
            {
                return false;
            }
            // ch:打开设备 | en:Open device
            nRet = myCamera.MV_CC_OpenDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                return false;
            }
            // ch:探测网络最佳包大小(只对GigE相机有效) | en:Detection network optimal package size(It only works for the GigE camera)
            if (deviceinfo.device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
            {
                int nPacketSize = myCamera.MV_CC_GetOptimalPacketSize_NET();
                if (nPacketSize > 0)
                {
                    nRet = myCamera.MV_CC_SetIntValue_NET("GevSCPSPacketSize", (uint)nPacketSize);
                    if (nRet != MyCamera.MV_OK)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            // ch: 设置触发模式为off || en:set trigger mode as off
            nRet = myCamera.MV_CC_SetEnumValue_NET("TriggerMode", 1);
            if (MyCamera.MV_OK != nRet)
            {
                return false;
            }
            // ch:注册回调函数 | en:Register image callback
            cbImage = new MyCamera.cbOutputExdelegate(ImageCallBack);
            nRet = myCamera.MV_CC_RegisterImageCallBackEx_NET(cbImage, IntPtr.Zero);
            if (MyCamera.MV_OK != nRet)
            {
                return false;
            }

            //设置缓存节点
            nRet = myCamera.MV_CC_SetImageNodeNum_NET(5);
            if (MyCamera.MV_OK != nRet)
            {
                return false;
            }

            // ch:开启抓图 || en: start grab image
            nRet = myCamera.MV_CC_StartGrabbing_NET();
            if (MyCamera.MV_OK != nRet)
            {
                return false;
            }

            nRet = myCamera.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE);
            if (MyCamera.MV_OK != nRet)
            {
                return false;
            }


            QhImages.Clear();

            return true;
        }

        //internal SDK_HIKVision getCurCam()
        //{ 
        
        //}


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
                    //相机软触发
                    int nRet = D_cameras[this.CameraInfoStr].MV_CC_SetCommandValue_NET("TriggerSoftware");
                    while (true)
                    {
                        if (QhImages.Count > 0)
                        {
                            break;
                        }
                        Thread.Sleep(10);
                    }

                    return QhImages.Dequeue();
                    #region 取像OK，但使用回调函数功能，此处注释不使用
                    //foreach (KeyValuePair<string, MyCamera> item in D_cameras)
                    //{
                    //    if (item.Key == CameraInfoStr)
                    //    {
                    //        MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
                    //        int nRet = item.Value.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);
                    //        if (MyCamera.MV_OK != nRet)
                    //        {
                    //            Win_MessageBox.Instance.MessageBoxShow("相机采图异常\r\n可能原因：未正常关闭\r\n解决方案：重启电脑或5分钟后重启程序", TipType.Error);
                    //            break;
                    //        }
                    //        UInt32 nPayloadSize = stParam.nCurValue;
                    //        IntPtr pBufForDriver = Marshal.AllocHGlobal((int)nPayloadSize);
                    //        IntPtr pBufForSaveImage = IntPtr.Zero;

                    //        MyCamera.MV_FRAME_OUT_INFO_EX FrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();
                    //        nRet = item.Value.MV_CC_GetOneFrameTimeout_NET(pBufForDriver, nPayloadSize, ref FrameInfo, 1000);
                    //        if (MyCamera.MV_OK == nRet)
                    //        {
                    //            if (pBufForSaveImage == IntPtr.Zero)
                    //            {
                    //                pBufForSaveImage = Marshal.AllocHGlobal((int)(FrameInfo.nHeight * FrameInfo.nWidth * 3 + 2048));
                    //            }

                    //            MyCamera.MV_SAVE_IMAGE_PARAM_EX stSaveParam = new MyCamera.MV_SAVE_IMAGE_PARAM_EX();
                    //            stSaveParam.enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Bmp;
                    //            stSaveParam.enPixelType = FrameInfo.enPixelType;
                    //            stSaveParam.pData = pBufForDriver;
                    //            stSaveParam.nDataLen = FrameInfo.nFrameLen;
                    //            stSaveParam.nHeight = FrameInfo.nHeight;
                    //            stSaveParam.nWidth = FrameInfo.nWidth;
                    //            stSaveParam.pImageBuffer = pBufForSaveImage;
                    //            stSaveParam.nBufferSize = (uint)(FrameInfo.nHeight * FrameInfo.nWidth * 3 + 2048);
                    //            stSaveParam.nJpgQuality = 80;
                    //            nRet = item.Value.MV_CC_SaveImageEx_NET(ref stSaveParam);
                    //            if (MyCamera.MV_OK != nRet)
                    //            {
                    //                Win_MessageBox.Instance.MessageBoxShow("采图异常", TipType.Error);
                    //            }
                    //            byte[] data = new byte[stSaveParam.nImageLen];
                    //            Marshal.Copy(pBufForSaveImage, data, 0, (int)stSaveParam.nImageLen);

                    //            //转化成Halcon对象
                    //            GCHandle hand = GCHandle.Alloc(data, GCHandleType.Pinned);
                    //            IntPtr pr = hand.AddrOfPinnedObject();
                    //            image.GenImage1(new HTuple("byte"), stSaveParam.nWidth, stSaveParam.nHeight, pBufForDriver);
                    //            if (hand.IsAllocated)
                    //                hand.Free();
                    //            Marshal.FreeHGlobal(pBufForDriver);
                    //            Marshal.FreeHGlobal(pBufForSaveImage);
                    //            break;
                    //        }
                    //        else
                    //        {
                    //            Win_MessageBox.Instance.MessageBoxShow("采图异常", TipType.Error);
                    //        }
                    //    }
                    //}
                    #endregion


                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
                return null;
            }
        }

        //System.DateTime d1 = DateTime.Now;
        //public static string ExecDateDiff(System.DateTime d1, System.DateTime d2)
        //{
        //    TimeSpan ts1 = new TimeSpan(d1.Ticks);
        //    TimeSpan ts2 = new TimeSpan(d2.Ticks);
        //    TimeSpan ts3 = ts1.Subtract(ts2).Duration();
        //    return ts3.TotalMilliseconds.ToString();
        //}

        /// <summary>
        /// 采图回调函数
        /// </summary>
        /// <param name="pData"></param>
        /// <param name="pFrameInfo"></param>
        /// <param name="pUser"></param>
        private void ImageCallBack(IntPtr pData, ref MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser)
        {
            try
            {
                //System.DateTime d2 = DateTime.Now;
                //System.Diagnostics.Debug.WriteLine(ExecDateDiff(d2, d1));
                //d1 = d2;

                HImage image = new HImage();
                image.GenImage1("byte", pFrameInfo.nWidth, pFrameInfo.nHeight, pData);
                if (Start.ImgEnqueueFlag || Start.ImgEnqueueFlagOp || Start.ImaEnqueueTool || Start.JobSwitchMark_Soft)
                {
                    QhImages.Enqueue(image.CopyImage());
                }
                else
                {
                    QhImages.Clear();
                }

               


                //int nIndex = (int)pUser;

                //// ch:抓取的帧数 | en:Aquired Frame Number
                ////////++m_nFrames[nIndex];

                ////ch:判断是否需要保存图片 | en:Determine whether to save image
                ////////if (m_bSaveImg[nIndex])
                ////////{
                ////////    SaveImage(pData, pFrameInfo, nIndex);
                ////////    m_bSaveImg[nIndex] = false;
                ////////}

                //IntPtr pBufForSaveImage = IntPtr.Zero;

                ////MyCamera.MV_SAVE_IMAGE_PARAM_EX stDisplayInfo = new MyCamera.MV_SAVE_IMAGE_PARAM_EX();
                //////stDisplayInfo.hWnd = m_hDisplayHandle[nIndex];
                ////stDisplayInfo.pData = pData;
                ////stDisplayInfo.nDataLen = pFrameInfo.nFrameLen;
                ////stDisplayInfo.nWidth = pFrameInfo.nWidth;
                ////stDisplayInfo.nHeight = pFrameInfo.nHeight;
                ////stDisplayInfo.enPixelType = pFrameInfo.enPixelType;

                ////////item .Value .MV_CC_DisplayOneFrame_NET(ref stDisplayInfo);

                //if (pBufForSaveImage == IntPtr.Zero)
                //{
                //    pBufForSaveImage = Marshal.AllocHGlobal((int)(pFrameInfo.nHeight * pFrameInfo.nWidth * 3 + 2048));
                //}

                //MyCamera.MV_SAVE_IMAGE_PARAM_EX stSaveParam = new MyCamera.MV_SAVE_IMAGE_PARAM_EX();
                //stSaveParam.enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Bmp;
                //stSaveParam.enPixelType = pFrameInfo.enPixelType;
                //stSaveParam.pData = pData;
                //stSaveParam.nDataLen = pFrameInfo.nFrameLen;
                //stSaveParam.nHeight = pFrameInfo.nHeight;
                //stSaveParam.nWidth = pFrameInfo.nWidth;
                //stSaveParam.pImageBuffer = pBufForSaveImage;
                //stSaveParam.nBufferSize = (uint)(pFrameInfo.nHeight * pFrameInfo.nWidth);
                //stSaveParam.nJpgQuality = 80;

                //foreach (KeyValuePair<string, MyCamera> item in D_cameras)
                //{
                //    if (item.Key == CameraInfoStr)
                //    {
                //        int nRet = item.Value.MV_CC_SaveImageEx_NET(ref stSaveParam);
                //        if (MyCamera.MV_OK != nRet)
                //        {
                //            Win_MessageBox.Instance.MessageBoxShow("采图异常", TipType.Error);
                //        }
                //    }
                //}
                //byte[] data = new byte[stSaveParam.nImageLen];
                //Marshal.Copy(pBufForSaveImage, data, 0, (int)stSaveParam.nImageLen);

                ////转化成Halcon对象
                //GCHandle hand = GCHandle.Alloc(data, GCHandleType.Pinned);
                //IntPtr pr = hand.AddrOfPinnedObject();
                //HOperatorSet.GenImage1(out image, new HTuple("byte"), stSaveParam.nWidth, stSaveParam.nHeight, pData);
                
                //if (hand.IsAllocated)
                //    hand.Free();

                //Marshal.FreeHGlobal(pData);//释放非托管内存;
                //Marshal.FreeHGlobal(pBufForSaveImage);

                //waitingHardTriggerImage = false;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
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
                foreach (KeyValuePair<string, MyCamera> item in D_cameras)
                {
                    if (item.Key == CameraInfoStr)
                    {
                        item.Value.MV_CC_SetFloatValue_NET("ExposureTime", (float)exposure * 10);
                        Thread.Sleep(200);      //海康威视相机，实测发现设置完曝光后200毫秒以后才能生效，所以如此，当然这会影响CT
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        internal override void SetCamTriggerModel(DeviceTriggerModel triggerModel)
        {
            try
            {
                foreach (KeyValuePair<string, MyCamera> item in D_cameras)
                {
                    if (item.Key == CameraInfoStr)
                    {
                        if (triggerModel == DeviceTriggerModel.software)
                        {
                            int nRet = item.Value.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE);
                        }
                        else if (triggerModel == DeviceTriggerModel.hardware)
                        {
                            int nRet = item.Value.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE0);
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }


        internal override bool CheckCamExist()
        {
            foreach (KeyValuePair<string, MyCamera> item in D_cameras)
            {
                if (item.Key == CameraInfoStr)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 关闭所有相机
        /// </summary>
        internal static void CloseAllCamera()
        {
            try
            {
                foreach (KeyValuePair<string, MyCamera> item in D_cameras)
                {
                    item.Value.MV_CC_StopGrabbing_NET();
                    item.Value.MV_CC_CloseDevice_NET();
                    item.Value.MV_CC_DestroyDevice_NET();
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

    }

    /// <summary>
    /// 设备基础信息
    /// </summary>
    [Serializable]
    internal struct DeviceInfo
    {
        [NonSerialized]
        public MyCamera.MV_CC_DEVICE_INFO device;

        public string userName;
        public string vendorName;
        public string friendlyName;
        public string serialNumber;
        public string ipAddress;
        public string macAddress;
        public string cameraInfoStr;
    }

    /// <summary>
    /// 设备触发模式
    /// </summary>
    internal enum DeviceTriggerModel
    {
        software,
        hardware
    }


}
