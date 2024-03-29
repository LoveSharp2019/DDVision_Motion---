using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using VSE.Core;

namespace VSE
{
    /// <summary>
    /// Halcon相机接口
    /// </summary>
     [Serializable]
    internal class SDK_Halcon : SDK_Base
    {
        internal SDK_Halcon(string cameraInfoStr)
        {
            this.CameraInfoStr = cameraInfoStr;
        }
        /// <summary>
        /// 锁
        /// </summary>
        private object obj = new object();
        /// <summary>
        /// 相机集合   键：信息字符串  值：相机对象
        /// </summary>
        private static Dictionary<string, HFramegrabber> D_cameras = new Dictionary<string, HFramegrabber>();


        /// <summary>
        /// 通过Halcon图像采集接口枚举网络中所有的相机
        /// </summary>
        internal static void EnumCamera()
        {
            try
            {
                string[] interfaceTypeArray = new string[] { "DirectShow", "GigEVision2" };
                //遍历所有接口
                for (int i = 0; i < interfaceTypeArray.Length; i++)
                {
                    List<string> L_deviceStr = new List<string>();
                    HTuple info, infoValue = new HTuple();

                    {
                        try
                        {
                            HOperatorSet.InfoFramegrabber(new HTuple(interfaceTypeArray[i]), new HTuple("device"), out info, out infoValue);
                        }
                        catch
                        {
                            continue;
                        }
                        //////if (infoValue.ToString() == "\"device:default\"")       //表示没有搜索到采集设备
                        //////    continue;
                        for (int j = 0; j < infoValue.Length; j++)
                        {

                            string temp = string.Empty;
                            if (i == 0)
                            {
                                temp = infoValue;
                            }
                            else
                            {
                                temp = infoValue.TupleSelect(j).ToString();
                                temp = Regex.Split(temp, "device:")[1];
                                temp = Regex.Split(temp, "unique_name")[0];
                                temp = temp.Substring(0, temp.Length - 3);
                            }


                            //初始化相机
                            HFramegrabber handle=new HFramegrabber();
                            try
                            {
                                handle.OpenFramegrabber(new HTuple(interfaceTypeArray[i]),
                                                              new HTuple(0),
                                                              new HTuple(0),
                                                              new HTuple(0),
                                                              new HTuple(0),
                                                              new HTuple(0),
                                                              new HTuple(0),
                                                              new HTuple("progressive"),
                                                              new HTuple(-1),
                                                              new HTuple("default"),
                                                              new HTuple(-1),
                                                              new HTuple("false"),
                                                              new HTuple("default"),
                                                              new HTuple(temp),
                                                              new HTuple(0),
                                                              new HTuple(-1)
                                                              );
                            }
                            catch
                            {
                                continue;
                            }
                            HTuple exposure = 100;
                            // HOperatorSet.GetFramegrabberParam(handle, new HTuple("exposure"), out exposure);
                            string cameraInfoStr = interfaceTypeArray[i] + " | " + temp;
                            SDK_Halcon sdk_Halcon = new SDK_Halcon(cameraInfoStr);

                           
                            D_cameras.Add(cameraInfoStr, handle);
                            AcqImageTool.L_devices.Add(sdk_Halcon);
                            Win_FromDevice.Instance.cbx_deviceList.Items.Add(cameraInfoStr);

   
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        internal override bool CheckCamExist()
        {
            foreach (KeyValuePair<string, HFramegrabber> item in D_cameras)
            {
                if (item.Key == CameraInfoStr)
                    return true;
            }
            return false;
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
                    foreach (KeyValuePair<string, HFramegrabber> item in D_cameras)
                    {
                        if (item.Key == CameraInfoStr)
                        {
                            image.GrabImage(item.Value);
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
        /// 关闭所有相机
        /// </summary>
        internal static void CloseAllCamera()
        {
            try
            {
                foreach (KeyValuePair<string, HFramegrabber> item in D_cameras)
                {
                    item.Value.CloseFramegrabber();
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

    }
}
