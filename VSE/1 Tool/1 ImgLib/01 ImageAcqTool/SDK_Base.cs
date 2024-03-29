using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSE
{
    /// <summary>
    /// 相机基类
    /// </summary>
    [Serializable]
    internal class SDK_Base
    {

        /// <summary>
        /// 相机信息字符串
        /// </summary>
        internal string CameraInfoStr = string.Empty;
        /// <summary>
        /// 等待硬触发图像到达
        /// </summary>
        internal bool waitingHardTriggerImage = false;

        /// <summary>
        /// 采图
        /// </summary>
        /// <returns>采集到的图像</returns>
        internal virtual HImage GrabOneImage() { return null; }
        /// <summary>
        /// 设置相机曝光时间
        /// </summary>
        /// <param name="exposure">曝光时间</param>
        internal   virtual void SetExposure(double exposure) { }
        /// <summary>
        /// 检查相机是否存在
        /// </summary>
        /// <returns></returns>
        internal virtual  bool CheckCamExist(){return false ;}
        /// <summary>
        /// 设置相机触发模式
        /// </summary>
        /// <param name="triggerModel"></param>
        internal virtual void SetCamTriggerModel(DeviceTriggerModel triggerModel) { }

    }
}
