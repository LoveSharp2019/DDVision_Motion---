using HalconDotNet;
using Lxc.VisionPlus.ImageView.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VSE.Core;

namespace VSE
{
    [Serializable]
    internal class RRegionGraySubtractTool : ToolBase
    {
        //工具栏
        public ToolPar toolPar = new ToolPar();
   
        /// <summary>
        /// 运行工具
        /// </summary>
        /// <param name="updateImage">刷新图像</param>
        /// <param name="runTool">运行工具</param>
        /// <param name="toolName">工具名称</param>
        public override void Run(bool updateImage, bool runTool, string toolName)
        {
            try
            {
                lock (obj)
                {
                    toolRunStatu = ToolRunStatu.未知原因;
                    if (toolPar.InputPar.图像 == null)
                    {
                        toolRunStatu = ToolRunStatu.未指定输入图像;
                        return;
                    }
                    if (toolPar.InputPar.Pose == null)
                    {
                        toolRunStatu = ToolRunStatu.未指定输入坐标点;
                        return;
                    }
                    //更新图片
                    if (updateImage)
                    {
                        //Win_RRegionGraySubtractTool.Instance.PImageWin.Image = toolPar.InputPar.图像;
                    }

                    HImage image = toolPar.InputPar.图像.CopyImage();
                    XYU PoseTemp = toolPar.RunPar.StandardPose;
                    ROI ROIAffine1 = new ROI();
                    ROI ROIAffine2 = new ROI();
                    HRegion hRegionAffine1 = new HRegion();
                    HRegion hRegionAffine2 = new HRegion();

                    //Rectangle11
                    double r11 = 0, c11 = 0, r21 = 0, c21 = 0;
                    //Rectangle21,Circle1,ROIEllipse1
                    double CR1 = 0;
                    double CC1 = 0;
                    double Phi1 = 0;
                    double L11 = 0, L21 = 0;

                    //Rectangle12
                    double r12 = 0, c12 = 0, r22 = 0, c22 = 0;
                    //Rectangle22,Circle2,ROIEllipse2
                    double CR2 = 0;
                    double CC2 = 0;
                    double Phi2 = 0;
                    double L12 = 0, L22 = 0;

                    switch (toolPar.RunPar.基准区域1.Type)
                    {
                        case ROI.ROIType.ROIRectangle1:
                            r11 = (toolPar.RunPar.基准区域1 as ROIRectangle1).Row1;
                            r21 = (toolPar.RunPar.基准区域1 as ROIRectangle1).Row2;
                            c11 = (toolPar.RunPar.基准区域1 as ROIRectangle1).Column1;
                            c21 = (toolPar.RunPar.基准区域1 as ROIRectangle1).Column2;
                            break;
                        case ROI.ROIType.ROIRectangle2:
                            L11 = (toolPar.RunPar.基准区域1 as ROIRectangle2).Lenth1;
                            L21 = (toolPar.RunPar.基准区域1 as ROIRectangle2).Lenth2;
                            Phi1 = (toolPar.RunPar.基准区域1 as ROIRectangle2).Phi;
                            CR1 = (toolPar.RunPar.基准区域1 as ROIRectangle2).Row;
                            CC1 = (toolPar.RunPar.基准区域1 as ROIRectangle2).Column;
                            break;
                        case ROI.ROIType.ROICircle:
                            CR1 = (toolPar.RunPar.基准区域1 as ROICircle).Row;
                            CC1 = (toolPar.RunPar.基准区域1 as ROICircle).Column;
                            L11 = (toolPar.RunPar.基准区域1 as ROICircle).Radius;
                            break;
                        case ROI.ROIType.ROIEllipse:
                            CR1 = (toolPar.RunPar.基准区域1 as ROIEllipse).Row;
                            CC1 = (toolPar.RunPar.基准区域1 as ROIEllipse).Column;
                            Phi1 = (toolPar.RunPar.基准区域1 as ROIEllipse).Phi;
                            L11 = (toolPar.RunPar.基准区域1 as ROIEllipse).Radius1;
                            L21 = (toolPar.RunPar.基准区域1 as ROIEllipse).Radius2;
                            break;
                        default:
                            break;
                    }

                    switch (toolPar.RunPar.基准区域2.Type)
                    {
                        case ROI.ROIType.ROIRectangle1:
                            r12 = (toolPar.RunPar.基准区域2 as ROIRectangle1).Row1;
                            r22 = (toolPar.RunPar.基准区域2 as ROIRectangle1).Row2;
                            c12 = (toolPar.RunPar.基准区域2 as ROIRectangle1).Column1;
                            c22 = (toolPar.RunPar.基准区域2 as ROIRectangle1).Column2;
                            break;
                        case ROI.ROIType.ROIRectangle2:
                            L12 = (toolPar.RunPar.基准区域2 as ROIRectangle2).Lenth1;
                            L22 = (toolPar.RunPar.基准区域2 as ROIRectangle2).Lenth2;
                            Phi2 = (toolPar.RunPar.基准区域2 as ROIRectangle2).Phi;
                            CR2 = (toolPar.RunPar.基准区域2 as ROIRectangle2).Row;
                            CC2 = (toolPar.RunPar.基准区域2 as ROIRectangle2).Column;
                            break;
                        case ROI.ROIType.ROICircle:
                            CR2 = (toolPar.RunPar.基准区域2 as ROICircle).Row;
                            CC2 = (toolPar.RunPar.基准区域2 as ROICircle).Column;
                            L12 = (toolPar.RunPar.基准区域2 as ROICircle).Radius;
                            break;
                        case ROI.ROIType.ROIEllipse:
                            CR2 = (toolPar.RunPar.基准区域2 as ROIEllipse).Row;
                            CC2 = (toolPar.RunPar.基准区域2 as ROIEllipse).Column;
                            Phi2 = (toolPar.RunPar.基准区域2 as ROIEllipse).Phi;
                            L12 = (toolPar.RunPar.基准区域2 as ROIEllipse).Radius1;
                            L22 = (toolPar.RunPar.基准区域2 as ROIEllipse).Radius2;
                            break;
                        default:
                            break;
                    }

                    if (toolPar.InputPar.Pose != null && toolPar.InputPar.Pose.Count > 0)
                    {
                        if (toolPar.RunPar.StandardPose == null)
                        {
                            toolPar.RunPar.StandardPose = new XYU();
                        }

                        PoseTemp = toolPar.InputPar.Pose[0];
                        HHomMat2D homMat2D = new HHomMat2D();
                        homMat2D.HomMat2dIdentity();
                        homMat2D.VectorAngleToRigid(toolPar.RunPar.StandardPose.Point.Y, toolPar.RunPar.StandardPose.Point.X, toolPar.RunPar.StandardPose.U, PoseTemp.Point.Y, PoseTemp.Point.X, PoseTemp.U);
                        homMat2D.AffineTransPixel(toolPar.RunPar.基准区域1.Row, toolPar.RunPar.基准区域1.Column, out CR1, out CC1);
                        homMat2D.AffineTransPixel(toolPar.RunPar.基准区域2.Row, toolPar.RunPar.基准区域2.Column, out CR2, out CC2);

                        switch (toolPar.RunPar.基准区域1.Type)
                        {
                            case ROI.ROIType.ROIRectangle1:
                                homMat2D.AffineTransPixel((toolPar.RunPar.基准区域1 as ROIRectangle1).Row1, (toolPar.RunPar.基准区域1 as ROIRectangle1).Column1, out r11, out c11);
                                homMat2D.AffineTransPixel((toolPar.RunPar.基准区域1 as ROIRectangle1).Row2, (toolPar.RunPar.基准区域1 as ROIRectangle1).Column2, out r21, out c21);
                                break;
                            case ROI.ROIType.ROICircle:
                                break;
                            case ROI.ROIType.ROIRectangle2:
                                Phi1 = (toolPar.RunPar.基准区域1 as ROIRectangle2).Phi - toolPar.RunPar.StandardPose.U + PoseTemp.U;
                                break;
                            case ROI.ROIType.ROIEllipse:
                                Phi1 = (toolPar.RunPar.基准区域1 as ROIEllipse).Phi - toolPar.RunPar.StandardPose.U + PoseTemp.U;
                                break;
                            default:
                                break;
                        }

                        switch (toolPar.RunPar.基准区域2.Type)
                        {
                            case ROI.ROIType.ROIRectangle1:
                                homMat2D.AffineTransPixel((toolPar.RunPar.基准区域2 as ROIRectangle1).Row1, (toolPar.RunPar.基准区域2 as ROIRectangle1).Column1, out r12, out c12);
                                homMat2D.AffineTransPixel((toolPar.RunPar.基准区域2 as ROIRectangle1).Row2, (toolPar.RunPar.基准区域2 as ROIRectangle1).Column2, out r22, out c22);
                                break;
                            case ROI.ROIType.ROICircle:
                                break;
                            case ROI.ROIType.ROIRectangle2:
                                Phi2 = (toolPar.RunPar.基准区域2 as ROIRectangle2).Phi - toolPar.RunPar.StandardPose.U + PoseTemp.U;
                                break;
                            case ROI.ROIType.ROIEllipse:
                                Phi2 = (toolPar.RunPar.基准区域2 as ROIEllipse).Phi - toolPar.RunPar.StandardPose.U + PoseTemp.U;
                                break;
                            default:
                                break;
                        }

                        switch (toolPar.RunPar.基准区域1.Type)
                        {
                            case ROI.ROIType.ROIRectangle1:
                                ROIAffine1 = new ROIRectangle1(r11, c11, r21, c21);
                                break;
                            case ROI.ROIType.ROIRectangle2:
                                ROIAffine1 = new ROIRectangle2(CR1, CC1, Phi1, L11, L21);
                                break;
                            case ROI.ROIType.ROICircle:
                                ROIAffine1 = new ROICircle(CR1, CC1, L11);
                                break;
                            case ROI.ROIType.ROIEllipse:
                                ROIAffine1 = new ROIEllipse(CR1, CC1, L11, L21, Phi1);
                                break;
                            default:
                                break;
                        }

                        switch (toolPar.RunPar.基准区域2.Type)
                        {
                            case ROI.ROIType.ROIRectangle1:
                                ROIAffine2 = new ROIRectangle1(r12, c12, r22, c22);
                                break;
                            case ROI.ROIType.ROIRectangle2:
                                ROIAffine2 = new ROIRectangle2(CR2, CC2, Phi2, L12, L22);
                                break;
                            case ROI.ROIType.ROICircle:
                                ROIAffine2 = new ROICircle(CR2, CC2, L12);
                                break;
                            case ROI.ROIType.ROIEllipse:
                                ROIAffine2 = new ROIEllipse(CR2, CC2, L12, L22, Phi2);
                                break;
                            default:
                                break;
                        }

                        hRegionAffine1 = toolPar.RunPar.基准区域1.getRegion().AffineTransRegion(homMat2D, "nearest_neighbor");
                        toolPar.ResultPar.搜索区域1 = ROIAffine1;
                        hRegionAffine2 = toolPar.RunPar.基准区域2.getRegion().AffineTransRegion(homMat2D, "nearest_neighbor");
                        toolPar.ResultPar.搜索区域2 = ROIAffine2;
                    }
                    else
                    {
                        toolPar.ResultPar.搜索区域1 = toolPar.RunPar.基准区域1;
                        toolPar.ResultPar.搜索区域2 = toolPar.RunPar.基准区域2;
                    }

                    double deviation1 = 0;
                    double deviation2 = 0;
                    double averageGray1 = image.Intensity(toolPar.ResultPar.搜索区域1.getRegion(), out deviation1);
                    double averageGray2 = image.Intensity(toolPar.ResultPar.搜索区域2.getRegion(), out deviation2);
                    double diffGray12 = Math.Abs(Convert.ToDouble(averageGray1) - Convert.ToDouble(averageGray2));
                    toolPar.ResultPar.GrayVal1 = averageGray1;
                    toolPar.ResultPar.GrayVal2 = averageGray2;
                    toolPar.ResultPar.GrayDiff = Convert.ToDouble(diffGray12.ToString("0.000"));

                    if (toolPar.RunPar.Gray1大于Cray2)
                    {
                        if ((toolPar.ResultPar.GrayVal1 >= toolPar.ResultPar.GrayVal2) && (toolPar.ResultPar.GrayDiff >= toolPar.RunPar.RegionGrayLimit))
                        {
                            toolPar.ResultPar.Consequence = true;
                        }
                        else
                        {
                            toolPar.ResultPar.Consequence = false;
                        }
                    }
                    else if (toolPar.RunPar.Gray1小于Cray2)
                    {
                        if ((toolPar.ResultPar.GrayVal1 <= toolPar.ResultPar.GrayVal2)  && (toolPar.ResultPar.GrayDiff >= toolPar.RunPar.RegionGrayLimit))
                        {
                            toolPar.ResultPar.Consequence = true;
                        }
                        else
                        {
                            toolPar.ResultPar.Consequence = false;
                        }
                    }
                    else
                    {
                        if (toolPar.ResultPar.GrayDiff >= toolPar.RunPar.RegionGrayLimit)
                        {
                            toolPar.ResultPar.Consequence = true;
                        }
                        else
                        {
                            toolPar.ResultPar.Consequence = false;
                        }
                    }

                    if (Win_RRegionGraySubtractTool.Instance.Visible)
                    {
                        Win_RRegionGraySubtractTool.Instance.PImageWin.Image = toolPar.InputPar.图像;
                        if (toolPar.RunPar.displayGoldenROIRegion)
                        {
                            Win_RRegionGraySubtractTool.Instance.PImageWin.displayHRegion(toolPar.RunPar.基准区域1.getRegion(), "orange", "margin");
                            Win_RRegionGraySubtractTool.Instance.PImageWin.displayHRegion(toolPar.RunPar.基准区域2.getRegion(), "yellow", "margin");
                        }
                        if (toolPar.RunPar.displayROIRegion)
                        {
                            Win_RRegionGraySubtractTool.Instance.PImageWin.displayHRegion(toolPar.ResultPar.搜索区域1.getRegion(), "blue", "margin");
                            Win_RRegionGraySubtractTool.Instance.PImageWin.displayHRegion(toolPar.ResultPar.搜索区域2.getRegion(), "green", "margin");
                        }
                        if (toolPar.ResultPar.Consequence)
                        {
                            Win_RRegionGraySubtractTool.Instance.vTextBox4.TextStr = "OK";
                        }
                        else
                        {
                            Win_RRegionGraySubtractTool.Instance.vTextBox4.TextStr = "NG";
                        }
                        Win_RRegionGraySubtractTool.Instance.vTextBox_GrayDiffVal.TextStr = toolPar.ResultPar.GrayDiff.ToString("f3");
                        Win_RRegionGraySubtractTool.Instance.vTextBox_GrayVal1.TextStr = toolPar.ResultPar.GrayVal1.ToString("f3");
                        Win_RRegionGraySubtractTool.Instance.vTextBox_GrayVal2.TextStr = toolPar.ResultPar.GrayVal2.ToString("f3");
                    }
                    if ((toolPar.RunPar.基准区域1 != null && toolPar.RunPar.displayGoldenROIRegion))
                    {
                        if (runTool)
                        {
                            Win_DefectDetectionTool.Instance.PImageWin.displayHRegion(toolPar.RunPar.基准区域1.getRegion(), "orange", "margin");
                        }
                        else
                        {
                            GetImageWindowControl().BeginInvoke(new MethodInvoker(() =>
                            {
                                GetImageWindowControl().displayHRegion(toolPar.RunPar.基准区域1.getRegion(), "orange", "margin");
                            }));
                        }
                    }
                    if ((toolPar.RunPar.基准区域2 != null && toolPar.RunPar.displayGoldenROIRegion))
                    {
                        if (runTool)
                        {
                            Win_DefectDetectionTool.Instance.PImageWin.displayHRegion(toolPar.RunPar.基准区域2.getRegion(), "yellow", "margin");
                        }
                        else
                        {
                            GetImageWindowControl().BeginInvoke(new MethodInvoker(() =>
                            {
                                GetImageWindowControl().displayHRegion(toolPar.RunPar.基准区域2.getRegion(), "yellow", "margin");
                            }));
                        }
                    }
                    if ((toolPar.ResultPar.搜索区域1 != null && toolPar.RunPar.displayROIRegion))
                    {
                        if (runTool)
                        {
                            Win_DefectDetectionTool.Instance.PImageWin.displayHRegion(toolPar.ResultPar.搜索区域1.getRegion(), "blue", "margin");
                        }
                        else
                        {
                            GetImageWindowControl().BeginInvoke(new MethodInvoker(() =>
                            {
                                GetImageWindowControl().displayHRegion(toolPar.ResultPar.搜索区域1.getRegion(), "blue", "margin");
                            }));
                        }
                    }
                    if ((toolPar.ResultPar.搜索区域2 != null && toolPar.RunPar.displayROIRegion))
                    {
                        if (runTool)
                        {
                            Win_DefectDetectionTool.Instance.PImageWin.displayHRegion(toolPar.ResultPar.搜索区域2.getRegion(), "green", "margin");
                        }
                        else
                        {
                            GetImageWindowControl().BeginInvoke(new MethodInvoker(() =>
                            {
                                GetImageWindowControl().displayHRegion(toolPar.ResultPar.搜索区域2.getRegion(), "green", "margin");
                            }));
                        }
                    }
                    if (toolPar.RunPar.displayNGRegion)
                    {
                        if (!toolPar.ResultPar.Consequence)
                        {
                            if (runTool)
                            {
                                Win_DefectDetectionTool.Instance.PImageWin.displayHRegion(toolPar.ResultPar.搜索区域1.getRegion(), "red", toolPar.RunPar.regionDrawMode == FillMode.Margin ? "margin" : "fill");
                                Win_DefectDetectionTool.Instance.PImageWin.displayHRegion(toolPar.ResultPar.搜索区域2.getRegion(), "red", toolPar.RunPar.regionDrawMode == FillMode.Margin ? "margin" : "fill");
                            }
                            else
                            {
                                GetImageWindowControl().BeginInvoke(new MethodInvoker(() =>
                                {
                                    GetImageWindowControl().displayHRegion(toolPar.ResultPar.搜索区域1.getRegion(), "red", toolPar.RunPar.regionDrawMode == FillMode.Margin ? "margin" : "fill");
                                    GetImageWindowControl().displayHRegion(toolPar.ResultPar.搜索区域2.getRegion(), "red", toolPar.RunPar.regionDrawMode == FillMode.Margin ? "margin" : "fill");
                                }));
                            }
                        }
                    }

                    toolRunStatu = ToolRunStatu.成功;
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

            private List<XYU> pose;
            public List<XYU> Pose
            {
                get { return pose; }
                set { pose = value; }
            }
        }

        [Serializable]
        public class RunPar
        {
            private HImage _图像;
            public HImage 图像
            {
                get { return _图像; }
                set { _图像 = value; }
            }

            private XYU standardPose;
            public XYU StandardPose
            {
                get { return standardPose; }
                set { standardPose = value; }
            }

            private ROI _基准区域1;
            public ROI 基准区域1
            {
                get
                {
                    if (_基准区域1 == null)
                    {
                        HTuple w, h;
                        图像.GetImageSize(out w, out h);
                        _基准区域1 = new ROIRectangle1();
                        double d = (h > w ? w / 4 : h / 4);
                        double x1 = w / 2 - d;
                        double y1 = h / 2 - d;
                        double x2 = w / 2 + d;
                        double y2 = h / 2 + d;
                        ((ROIRectangle1)_基准区域1).UpdateROI(y1, x1, y2, x2);
                    }
                    return _基准区域1;
                }
                set { _基准区域1 = value; }
            }

            private ROI _基准区域2;
            public ROI 基准区域2
            {
                get
                {
                    if (_基准区域2 == null)
                    {
                        HTuple w, h;
                        图像.GetImageSize(out w, out h);
                        _基准区域2 = new ROIRectangle1();
                        double d = (h > w ? w / 4 : h / 4);
                        double x1 = w / 2 - d;
                        double y1 = h / 2 - d;
                        double x2 = w / 2 + d;
                        double y2 = h / 2 + d;
                        ((ROIRectangle1)_基准区域2).UpdateROI(y1, x1, y2, x2);
                    }
                    return _基准区域2;
                }
                set { _基准区域2 = value; }
            }

            internal double RegionGrayLimit = 5;

            internal bool Gray1大于Cray2 = false;

            internal bool Gray1小于Cray2 = false;

            internal bool displayGoldenROIRegion = false;

            internal bool displayROIRegion = true;

            internal bool displayNGRegion = true;

            internal FillMode regionDrawMode = FillMode.Margin;
        }

        [Serializable]
        internal class ResultPar
        {
            private ROI _搜索区域1;
            public ROI 搜索区域1
            {
                get { return _搜索区域1; }
                set { _搜索区域1 = value; }
            }

            private ROI _搜索区域2;
            public ROI 搜索区域2
            {
                get { return _搜索区域2; }
                set { _搜索区域2 = value; }
            }

            double grayVal1 = 999.999;
            public double GrayVal1
            {
                get { return grayVal1; }
                set { grayVal1 = value; }
            }

            double grayVal2 = 999.999;
            public double GrayVal2
            {
                get { return grayVal2; }
                set { grayVal2 = value; }
            }

            double grayDiff = 999.999;
            public double GrayDiff
            {
                get { return grayDiff; }
                set { grayDiff = value; }
            }

            bool consequence = false;
            public bool Consequence
            {
                get { return consequence; }
                set { consequence = value; }
            }            
        }
    }
}
