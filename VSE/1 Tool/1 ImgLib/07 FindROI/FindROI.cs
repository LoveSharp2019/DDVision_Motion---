using HalconDotNet;
using Lxc.VisionPlus.ImageView.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSE.Core;

namespace VSE
{
    [Serializable]
    internal class FindROI:ToolBase
    {
            public ToolPar toolPar = new ToolPar();
            internal List<ROI> L_regions = new List<ROI>();
            internal bool FindROIShow = true;
            /// <summary>
            /// 运行工具
            /// </summary>
            /// <param name="updateImage">是否刷新图像</param>
            public override void Run(bool updateImage, bool b, string toolName)
            {
                try
                {
                    lock (obj)
                    {
                        toolRunStatu = ToolRunStatu.未知原因;
                        HImage image = new HImage();
                        HRegion region = new HRegion();

                        image = toolPar.InputPar.图像.CopyImage();
                        region= image.Threshold(toolPar.RunPar.MinThreshold, toolPar.RunPar.MaxThreshold);
                        if (toolPar.InputPar.搜索区域!=null&& toolPar.InputPar.搜索区域.getRegion().IsInitialized())
                        {
                            region = region.Intersection(toolPar.InputPar.搜索区域.getRegion());
                        }
                    

                        if (updateImage && region != null && region.IsInitialized()&& FindROIShow)
                            GetImageWindowControl().displayHRegion(region, "green"); ;
                        toolPar.ResultPar.region = region;
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

                private ROI _搜索区域;

                public ROI 搜索区域
                {
                    get
                    {
                        if (_搜索区域 == null)
                        {
                                HTuple w, h;
                                图像.GetImageSize(out w, out h);
                                _搜索区域 = new ROIRectangle1();
                                double d = (h > w ? w / 4 : h / 4);
                                double x1 = w / 2 - d;
                                double y1 = h / 2 - d;
                                double x2 = w / 2 + d;
                                double y2 = h / 2 + d;
                                ((ROIRectangle1)_搜索区域).UpdateROI(y1, x1, y2, x2);
                        }
                        return _搜索区域;
                    }
                    set { _搜索区域 = value; }
                }
            }
            [Serializable]
            public class RunPar
            {
                public double MinThreshold = 0;
                public double MaxThreshold = 128;
            }
            [Serializable]
            internal class ResultPar
            {
                private HRegion _region;
                public HRegion region
                {
                    get
                    {
                        if (_region == null)
                        {
                            //HTuple w, h;
                            //_图像.GetImageSize(out w, out h);//0, 0, h, w
                            _region = new HRegion();// _图像.Threshold(0,255.0);
                        }
                        return _region;
                    }
                    set { _region = value; }
                }
            }
       
    }
}
