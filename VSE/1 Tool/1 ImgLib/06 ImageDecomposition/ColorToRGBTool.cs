using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VSE.Core;

namespace VSE
{
    [Serializable]
    internal class ColorToRGBTool : ToolBase
    {

        /// <summary>
        /// 输入图像
        /// </summary>
        internal HObject inputImage;
        /// <summary>
        /// 流程名
        /// </summary>
        internal new string jobName = string.Empty;
        /// <summary>
        /// 转化后的红色通道图像
        /// </summary>
        internal HObject outputRed;
        /// <summary>
        /// 转化后的绿色通道图像
        /// </summary>
        internal HObject outputGreen;
        /// <summary>
        /// 转化后的蓝色通道图像
        /// </summary>
        internal HObject outputBlue;


        /// <summary>
        /// 工具恢复到初始状态
        /// </summary>
        internal void ResetTool()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        /// <summary>
        /// 清空上次运行的所有输入
        /// </summary>
        internal void ClearLastInput()
        {
            try
            {
                inputImage = null;
                outputRed = null;
                outputGreen = null;
                outputBlue = null;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private new object obj = new object();
        /// <summary>
        /// 运行工具
        /// </summary>
        /// <param name="updateImage">是否刷新图像</param>
        /// <param name="jobName">流程名</param>
        public override void Run(bool updateImage, bool b, string toolName)
        {
            try
            {
                lock (obj)
                {
                    toolRunStatu =ToolRunStatu.未知原因;
                    if (inputImage == null)
                    {
                        toolRunStatu = (  ToolRunStatu.未指定输入图像);
                        return;
                    }
                    HTuple channelNum;
                    HOperatorSet.CountChannels(inputImage, out channelNum);
                    if (channelNum != 3)
                    {
                        toolRunStatu = (ToolRunStatu.输入图像不能被转化);
                        return;
                    }
                    HOperatorSet.Decompose3(inputImage, out outputRed, out  outputGreen, out  outputBlue);
                    toolRunStatu =ToolRunStatu.成功;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

    }
}
