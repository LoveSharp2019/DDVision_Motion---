using HalconDotNet;
using Lxc.VisionPlus.ImageView.Model;
using System;
using System.Collections.Generic;
using VSE.Core;

namespace VSE
{
    [Serializable]
    internal class BinarizationTool: ToolBase
    {
        public ToolPar toolPar = new ToolPar();
        /// <summary>
        /// 搜索区域
        /// </summary>
        internal List<ROI> L_regions = new List<ROI>();


        /// <summary>
        /// 运行工具
        /// </summary>
        /// <param name="updateImage">是否刷新图像</param>
        public override void Run(bool updateImage, bool b, string toolName)
        {
            try
            {
                if (toolPar.InputPar.图像==null)
                {
                    return;
                }
                lock (obj)
                {
                    toolRunStatu = ToolRunStatu.未知原因;

                    HImage image = new HImage();
                    HRegion region = new HRegion();
                    image = toolPar.InputPar.图像.CopyImage();
                    region =toolPar.InputPar.图像.Threshold(0,toolPar.RunPar.threshold);
                    image.OverpaintRegion(region, 0.0, "fill");
                    region = toolPar.InputPar.图像.Threshold(toolPar.RunPar.threshold,255);
                    image.OverpaintRegion(region, 255.0, "fill");
                    toolPar.ResultPar.图像 = image;
                    //if (updateImage)
                    //    GetImageWindowControl().Image = image;

                    toolRunStatu =ToolRunStatu.成功;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
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
        public class InputPar
        {
            private HImage _图像;

            public HImage 图像
            {
                get { return _图像; }
                set { _图像 = value; }
            }

            private HRegion _搜索区域;

            public HRegion 搜索区域
            {
                get { return _搜索区域; }
                set { _搜索区域 = value; }
            }
        }
        [Serializable]
        public class RunPar
        {
            public double threshold = 128;
        }
        [Serializable]
        internal class ResultPar
        {
            private HImage _图像;
            public HImage 图像
            {
                get { return _图像; }
                set { _图像 = value; }
            }
        }
    }
}
