using HalconDotNet;
using Lxc.VisionPlus.ImageView.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VSE.Core;

namespace VSE
{
    [Serializable]
    internal class DefectDetectionTool : ToolBase
    {
        internal DefectDetectionTool()
        {
            IsUseBlobFilter = new List<SelShapePar>();
            IsUseBlobFilter.Add(new SelShapePar { features = ResultPar.featuresArray[0], operation = "and", min = 10, max = 999.0, enable = false });
            IsUseBlobFilter.Add(new SelShapePar { features = ResultPar.featuresArray[1], operation = "and", min = 10, max = 999.0, enable = false });
            IsUseBlobFilter.Add(new SelShapePar { features = ResultPar.featuresArray[2], operation = "and", min = 10, max = 999.0, enable = false });
            BlobFilter = new SelShapePar[37];
            LoadBlobFilter();
        }

        public ToolPar toolPar = new ToolPar();

        internal SortMode sortMode = SortMode.从上至下且从左至右;

        internal SelShapePar[] BlobFilter;

        internal List<SelShapePar> IsUseBlobFilter = new List<SelShapePar>();
 
        internal int spanPixelNum = 100;

        public void LoadBlobFilter()
        {
            for (int i = 0; i < BlobFilter.Length; i++)
            {
                if (i < 3)
                {
                    BlobFilter[i] = new SelShapePar { features = ResultPar.featuresArray[i], operation = "and", min = 10, max = 999, enable = false };
                }
                else
                {
                    BlobFilter[i] = new SelShapePar { features = ResultPar.featuresArray[i], operation = "and", min = 10, max = 9999.0, enable = false };
                }
            }
        }

        public double[][] Rotate(double[][] array)
        {
            try
            {
                int x = array.GetUpperBound(0) + 1; //一维 
                int y = array[0].Length; //二维 
                double[][] newArray = new double[y][]; //构造转置二维数组
                for (int i = 0; i < y; i++)
                {
                    double[] r = new double[x];
                    for (int j = 0; j < x; j++)
                    {
                        r[j] = array[j][i];
                    }
                    newArray[i] = r;
                }
                return newArray;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private List<ItemType> l_item = new List<ItemType>();

        public List<ItemType> L_item
        {
            get
            {
                if (l_item == null)
                {
                    l_item = new List<ItemType>();
                }
                return l_item;
            }
        }

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
                    if (updateImage)
                    {
                        //Win_DefectDetectionTool.Instance.PImageWin.Image = toolPar.InputPar.图像;
                    }

                    HImage image = toolPar.InputPar.图像.CopyImage();
                    XYU PoseTemp = toolPar.RunPar.StandardPose;
                    HHomMat2D homMat2D = new HHomMat2D();
                    ROI ROIAffine1 = new ROI();//
                    HRegion hRegionAffine1 = new HRegion();//
                     
                    //Rectangle1
                    double r1 = 0, c1 = 0, r2 = 0, c2 = 0;
                    //Rectangle2
                    double CR = 0;
                    double CC = 0;
                    double Phi = 0;
                    double L1 = 0, L2 = 0;

                    switch (toolPar.RunPar.基准区域.Type)
                    {
                        case ROI.ROIType.ROIRectangle1:
                            r1 = (toolPar.RunPar.基准区域 as ROIRectangle1).Row1;
                            r2 = (toolPar.RunPar.基准区域 as ROIRectangle1).Row2;
                            c1 = (toolPar.RunPar.基准区域 as ROIRectangle1).Column1;
                            c2 = (toolPar.RunPar.基准区域 as ROIRectangle1).Column2;
                            break;
                        case ROI.ROIType.ROIRectangle2:
                            L1 = (toolPar.RunPar.基准区域 as ROIRectangle2).Lenth1;
                            L2 = (toolPar.RunPar.基准区域 as ROIRectangle2).Lenth2;
                            Phi = (toolPar.RunPar.基准区域 as ROIRectangle2).Phi;
                            CR = (toolPar.RunPar.基准区域 as ROIRectangle2).Row;
                            CC = (toolPar.RunPar.基准区域 as ROIRectangle2).Column;
                            break;
                        case ROI.ROIType.ROICircle:
                            CR = (toolPar.RunPar.基准区域 as ROICircle).Row;
                            CC = (toolPar.RunPar.基准区域 as ROICircle).Column;
                            L1 = (toolPar.RunPar.基准区域 as ROICircle).Radius;
                            break;
                        case ROI.ROIType.ROIEllipse:
                            CR = (toolPar.RunPar.基准区域 as ROIEllipse).Row;
                            CC = (toolPar.RunPar.基准区域 as ROIEllipse).Column;
                            Phi = (toolPar.RunPar.基准区域 as ROIEllipse).Phi;
                            L1 = (toolPar.RunPar.基准区域 as ROIEllipse).Radius1;
                            L2 = (toolPar.RunPar.基准区域 as ROIEllipse).Radius2;
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
                        homMat2D.HomMat2dIdentity();
                        homMat2D.VectorAngleToRigid(toolPar.RunPar.StandardPose.Point.Y, toolPar.RunPar.StandardPose.Point.X, toolPar.RunPar.StandardPose.U, PoseTemp.Point.Y, PoseTemp.Point.X, PoseTemp.U);
                        homMat2D.AffineTransPixel(toolPar.RunPar.基准区域.Row, toolPar.RunPar.基准区域.Column, out CR, out CC);

                        switch (toolPar.RunPar.基准区域.Type)
                        {
                            case ROI.ROIType.ROIRectangle1:
                                homMat2D.AffineTransPixel((toolPar.RunPar.基准区域 as ROIRectangle1).Row1, (toolPar.RunPar.基准区域 as ROIRectangle1).Column1, out r1, out c1);
                                homMat2D.AffineTransPixel((toolPar.RunPar.基准区域 as ROIRectangle1).Row2, (toolPar.RunPar.基准区域 as ROIRectangle1).Column2, out r2, out c2);
                                break;
                            case ROI.ROIType.ROICircle:
                                break;
                            case ROI.ROIType.ROIRectangle2:
                                Phi = (toolPar.RunPar.基准区域 as ROIRectangle2).Phi - toolPar.RunPar.StandardPose.U + PoseTemp.U;
                                break;
                            case ROI.ROIType.ROIEllipse:
                                Phi = (toolPar.RunPar.基准区域 as ROIEllipse).Phi - toolPar.RunPar.StandardPose.U + PoseTemp.U;
                                break;
                            default:
                                break;
                        }

                        switch (toolPar.RunPar.基准区域.Type)
                        {
                            case ROI.ROIType.ROIRectangle1:
                                ROIAffine1 = new ROIRectangle1(r1, c1, r2, c2);
                                break;
                            case ROI.ROIType.ROIRectangle2:
                                ROIAffine1 = new ROIRectangle2(CR, CC, Phi, L1, L2);
                                break;
                            case ROI.ROIType.ROICircle:
                                ROIAffine1 = new ROICircle(CR, CC, L1);
                                break;
                            case ROI.ROIType.ROIEllipse:
                                ROIAffine1 = new ROIEllipse(CR, CC, L1, L2, Phi);
                                break;
                            default:
                                break;
                        }

                        hRegionAffine1 = toolPar.RunPar.基准区域.getRegion().AffineTransRegion(homMat2D, "nearest_neighbor");
                        toolPar.ResultsPar.搜索区域 = ROIAffine1;
                    }
                    else
                    {
                        toolPar.ResultsPar.搜索区域 = toolPar.RunPar.基准区域; 
                    }

                    HRegion BaseXLDRegionAffine1 = new HRegion();
                    HRegion RealXLDROIAffine1 = new HRegion();
                    BaseXLDRegionAffine1 = toolPar.RunPar.BaseXLDRegion.AffineTransRegion(homMat2D, "nearest_neighbor");
                    RealXLDROIAffine1 = toolPar.RunPar.RealXLDROI.AffineTransRegion(homMat2D, "nearest_neighbor");
                    toolPar.ResultsPar.BaseXLDRegionAffine = BaseXLDRegionAffine1;
                    toolPar.ResultsPar.RealXLDROIAffine = RealXLDROIAffine1;   

                    HImage reduceImage = new HImage();
                    HRegion hregionBaseXLDRegionAffine1 = ROIAffine1.getRegion().Difference(BaseXLDRegionAffine1);
                    reduceImage = image.ReduceDomain(hregionBaseXLDRegionAffine1);

                    HImage ImgTemp = new HImage();
                    reduceImage.EdgesImage(out ImgTemp, "canny", toolPar.RunPar.NewAlpha, "nms", toolPar.RunPar.NewMinThreshold, toolPar.RunPar.NewMaxThreshold);
                    toolPar.ResultsPar.resultRegion = ImgTemp.Threshold(toolPar.RunPar.MinThreshold, 255.0).Connection();

                    HRegion resultRegion = toolPar.ResultsPar.resultRegion;
                    HRegion emptyRegion = new HRegion();
                    emptyRegion.GenEmptyRegion();
                    if (resultRegion != null)
                    {
                        for (int i = 0; i < L_item.Count; i++)
                        {
                            if (L_item[i].enable)
                            {
                                switch (L_item[i].type)
                                {
                                    case "OpenCircle":
                                        OpenCircle OpenCircle = (OpenCircle)L_item[i].item;
                                        resultRegion = resultRegion.OpeningCircle(OpenCircle.r);
                                        break;
                                    case "CloseCircle":
                                        CloseCircle CloseCircle = (CloseCircle)L_item[i].item;
                                        resultRegion = resultRegion.ClosingCircle(CloseCircle.r);
                                        break;
                                    case "DilationCircle":
                                        DilationCircle DilationCircle = (DilationCircle)L_item[i].item;
                                        resultRegion = resultRegion.DilationCircle(DilationCircle.r);
                                        break;
                                    case "ErosionCircle":
                                        ErosionCircle ErosionCircle = (ErosionCircle)L_item[i].item;
                                        resultRegion = resultRegion.ErosionCircle(ErosionCircle.r);
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        resultRegion = emptyRegion;
                    }

                    HTuple count = 0;
                    resultRegion = resultRegion.Connection();
                    count = resultRegion.CountObj();
                    if (count > 10000)
                    {
                        Win_Log.Instance.OutputMsg("斑点分析工具分割出的斑点数大于10000，可能会造成电脑卡顿，已放弃执行", Win_Log.InfoType.alarm);
                        toolRunStatu = ToolRunStatu.未知原因;
                        return;
                    }

                    if (IsUseBlobFilter == null)
                    {
                        IsUseBlobFilter = new List<SelShapePar>();
                        IsUseBlobFilter.Add(new SelShapePar { features = ResultPar.featuresArray[0], operation = "and", min = 10, max = 999.0, enable = false });
                        IsUseBlobFilter.Add(new SelShapePar { features = ResultPar.featuresArray[1], operation = "and", min = 10, max = 999.0, enable = false });
                        IsUseBlobFilter.Add(new SelShapePar { features = ResultPar.featuresArray[2], operation = "and", min = 10, max = 999.0, enable = false });
                        BlobFilter = new SelShapePar[37];
                        LoadBlobFilter();
                    }

                    for (int i = 0; i < IsUseBlobFilter.Count; i++)
                    {
                        if (IsUseBlobFilter[i].enable)
                        {
                            resultRegion = resultRegion.SelectShape(IsUseBlobFilter[i].features, IsUseBlobFilter[i].operation, IsUseBlobFilter[i].min, IsUseBlobFilter[i].max);
                        }
                    }
                    resultRegion = resultRegion.SelectShape("area", "and", 0.000000001, 9999999999999);
                    toolPar.ResultsPar.L_resultRegion.Clear();

                    if (resultRegion.Row.Length != 0)
                    {
                        double[][] r = new double[IsUseBlobFilter.Count][];
                        for (int i = 0; i < IsUseBlobFilter.Count; i++)
                        {
                            double[] k = resultRegion.RegionFeatures(new HTuple(IsUseBlobFilter[i].features)).DArr;
                            r[i] = k;
                        }
                        double[][] s = Rotate(r);
                        for (int i = 0; i < s.GetUpperBound(0) + 1; i++)
                        {
                            ResultPar br = new ResultPar(i + 1, true, s[i], resultRegion.SelectObj(new HTuple(i + 1)));
                            toolPar.ResultsPar.L_resultRegion.Add(br);
                        }
                    }

                    toolPar.ResultsPar.resultRegion = resultRegion;
                    toolPar.ResultsPar.resultCount = toolPar.ResultsPar.resultRegion.CountObj();


                    HTuple resultCount = 0;
                    resultCount = toolPar.ResultsPar.resultRegion.CountObj();

                    if (Win_DefectDetectionTool.Instance.Visible || runTool)
                    {
                        if (Win_DefectDetectionTool.Instance.Visible)
                        {
                            Win_DefectDetectionTool.Instance.LoadResult();
                            Win_DefectDetectionTool.Instance.PImageWin.Image = toolPar.InputPar.图像;
                        }
                        if (toolPar.RunPar.displayGoldenSearchRegion)
                        {
                            Win_DefectDetectionTool.Instance.PImageWin.displayHRegion(toolPar.RunPar.基准区域.getRegion(), "orange", "margin");
                        }
                        if (toolPar.RunPar.displayGoldenBaseXLDRegion)
                        {
                            Win_DefectDetectionTool.Instance.PImageWin.displayHRegion(toolPar.RunPar.BaseXLDRegion, "orange", "margin");
                        }
                        if (toolPar.RunPar.displaySearchRegion)
                        {
                            Win_DefectDetectionTool.Instance.PImageWin.displayHRegion(toolPar.ResultsPar.搜索区域.getRegion(), "blue", "margin");
                        }
                        if (toolPar.RunPar.displayBaseXLDRegion)
                        {
                            Win_DefectDetectionTool.Instance.PImageWin.displayHRegion(toolPar.ResultsPar.BaseXLDRegionAffine, "green", "margin");
                        }
                        if (toolPar.RunPar.displayResultRegion && toolPar.ResultsPar.resultRegion != null)
                        {
                            Win_DefectDetectionTool.Instance.PImageWin.displayHRegion(toolPar.ResultsPar.resultRegion.Union1(), "red", toolPar.RunPar.regionDrawMode == FillMode.Margin ? "margin" : "fill");
                        }
                    }
                    else
                    {
                        if (toolPar.RunPar.displayGoldenSearchRegion)
                        {
                            GetImageWindowControl().BeginInvoke(new MethodInvoker(() =>
                            {
                                GetImageWindowControl().displayHRegion(toolPar.RunPar.基准区域.getRegion(), "orange", "margin");
                            }));
                        }
                        if (toolPar.RunPar.displayGoldenBaseXLDRegion)
                        {
                            GetImageWindowControl().BeginInvoke(new MethodInvoker(() =>
                            {
                                GetImageWindowControl().displayHRegion(toolPar.RunPar.BaseXLDRegion, "orange", "margin");
                            }));
                        }
                        if (toolPar.RunPar.displaySearchRegion)
                        {
                            GetImageWindowControl().BeginInvoke(new MethodInvoker(() =>
                            {
                                GetImageWindowControl().displayHRegion(toolPar.ResultsPar.搜索区域.getRegion(), "blue", "margin");
                            }));
                        }
                        if (toolPar.RunPar.displayBaseXLDRegion)
                        {
                            GetImageWindowControl().BeginInvoke(new MethodInvoker(() =>
                            {
                                GetImageWindowControl().displayHRegion(toolPar.ResultsPar.BaseXLDRegionAffine, "green", "margin");
                                //GetImageWindowControl().displayHRegion(toolPar.ResultPar.RealXLDROIAffine, "green", "margin");
                            }));
                        }
                        if (toolPar.RunPar.displayResultRegion && toolPar.ResultsPar.resultRegion != null)
                        {
                            GetImageWindowControl().BeginInvoke(new MethodInvoker(() =>
                            {
                                GetImageWindowControl().displayHRegion(toolPar.ResultsPar.resultRegion.Union1(), "red", toolPar.RunPar.regionDrawMode == FillMode.Margin ? "margin" : "fill");
                            }));
                        }
                    }

                    if ((toolPar.RunPar.displayOutCircle || toolPar.RunPar.displayCross) && count < 1000)
                    {
                        for (int i = 0; i < resultCount; i++)
                        {
                            HRegion region = new HRegion();
                            region = toolPar.ResultsPar.resultRegion.SelectObj(new HTuple(i + 1));

                            if (toolPar.RunPar.displayOutCircle)
                            {
                                HTuple row2, col2, radius2;
                                region.SmallestCircle(out row2, out col2, out radius2);
                                HRegion circle = new HRegion();
                                circle.GenCircle(row2, col2, radius2);

                                if (Win_DefectDetectionTool.Instance.Visible)
                                {
                                    Win_DefectDetectionTool.Instance.PImageWin.displayHRegion(circle, "green", toolPar.RunPar.outCircleDrawMode == FillMode.Margin ? "margin" : "fill");
                                }
                                else
                                {
                                    if (runTool)
                                    {
                                        Win_DefectDetectionTool.Instance.PImageWin.displayHRegion(circle, "green", toolPar.RunPar.outCircleDrawMode == FillMode.Margin ? "margin" : "fill");
                                    }
                                    else
                                    {
                                        GetImageWindowControl().BeginInvoke(new MethodInvoker(() =>
                                        {
                                            GetImageWindowControl().displayHRegion(circle, "green", toolPar.RunPar.outCircleDrawMode == FillMode.Margin ? "margin" : "fill");
                                        }));
                                    }
                                }
                            }

                            if (toolPar.RunPar.displayCross)
                            {
                                HTuple area3, row3, col3;
                                area3 = region.AreaCenter(out row3, out col3);
                                HXLDCont cross = new HXLDCont();
                                cross.GenCrossContourXld(row3, col3, 20, 0);
                                if (Win_DefectDetectionTool.Instance.Visible)
                                {
                                    Win_DefectDetectionTool.Instance.PImageWin.displayHRegion(cross, "blue");
                                }
                                else
                                {
                                    if (runTool)
                                    {
                                        Win_DefectDetectionTool.Instance.PImageWin.displayHRegion(cross, "blue");
                                    }
                                    else
                                    {
                                        GetImageWindowControl().BeginInvoke(new MethodInvoker(() =>
                                        {
                                            GetImageWindowControl().displayHRegion(cross, "blue");
                                        }));
                                    }
                                }
                            }
                        }
                    }

                    toolPar.ResultsPar.位置.Clear();
                    for (int i = 0; i < toolPar.ResultsPar.L_resultRegion.Count; i++)
                    {
                        XYU xyu = new XYU();
                        xyu.Point.X = toolPar.ResultsPar.L_resultRegion[i].CenterOfMassX;
                        xyu.Point.Y = toolPar.ResultsPar.L_resultRegion[i].CenterOfMassY;
                        xyu.U = 0;
                        toolPar.ResultsPar.位置.Add(xyu);
                    }

                    toolRunStatu = ToolRunStatu.成功;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        public void FindBaseXLD()
        {
            HImage hImage1 = new HImage();
            HImage hImageReduced1 = new HImage();
            hImage1 = toolPar.InputPar.图像.CopyImage();
            hImageReduced1 = hImage1.ReduceDomain(toolPar.RunPar.基准区域.getRegion());
            HXLDCont xld = hImageReduced1.EdgesSubPix("canny", toolPar.RunPar.BaseAlpha, toolPar.RunPar.BaseMinThreshold, toolPar.RunPar.BaseMaxThreshold);
            HXLDCont xld1 = xld.SelectShapeXld("area", "and", toolPar.RunPar.BaseFilterArea, 99999999999);
            HXLDCont xld2 = xld1.SortContoursXld("upper_left", "true", "column");
            int count = xld2.CountObj();
            HRegion Line = new HRegion();
            Line.GenEmptyRegion();
            for (int i = 1; i <= count; i++)
            {
                HTuple row, col;
                HRegion region = new HRegion();
                HXLDCont xld3 = xld2.SelectObj(i);
                xld3.GetContourXld(out row, out col);
                region.GenRegionPolygon(row, col);
                region = region.DilationRectangle1(31, 31);
                Line = Line.ConcatObj(region);
            }
            toolPar.RunPar.BaseXLDRegion = Line.Union1();
            toolPar.RunPar.RealXLDROI = toolPar.RunPar.BaseXLDRegion.FillUp();
            if (Win_DefectDetectionTool.Instance.Visible)
            {
                Win_DefectDetectionTool.Instance.PImageWin.Image = toolPar.InputPar.图像;
                Win_DefectDetectionTool.Instance.PImageWin.displayHRegion(toolPar.RunPar.基准区域.getRegion(), "orange", "margin");
                Win_DefectDetectionTool.Instance.PImageWin.displayHRegion(toolPar.RunPar.BaseXLDRegion, "orange", "margin");
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

            private ResultsPar _resultsPar = new ResultsPar();
            public ResultsPar ResultsPar
            {
                get { return _resultsPar; }
                set { _resultsPar = value; }
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

            private ROI _基准区域;
            public ROI 基准区域
            {
                get
                {
                    if (_基准区域 == null)
                    {
                        HTuple w, h;
                        图像.GetImageSize(out w, out h);
                        _基准区域 = new ROIRectangle1();
                        double d = (h > w ? w / 4 : h / 4);
                        double x1 = w / 2 - d;
                        double y1 = h / 2 - d;
                        double x2 = w / 2 + d;
                        double y2 = h / 2 + d;
                        ((ROIRectangle1)_基准区域).UpdateROI(y1, x1, y2, x2);
                    }
                    return _基准区域;
                }
                set { _基准区域 = value; }
            }

            private  HRegion _BaseXLDRegion;
            public HRegion BaseXLDRegion
            {
                get 
                {
                    if (_BaseXLDRegion == null)
                        _BaseXLDRegion = new HRegion();
                    return _BaseXLDRegion; 
                }
                set { _BaseXLDRegion = value; }
            }

            public HRegion _RealXLDROI;
            public HRegion RealXLDROI
            {
                get
                {
                    if (_RealXLDROI == null)
                        _RealXLDROI = new HRegion();
                    return _RealXLDROI;
                }
                set { _RealXLDROI = value; }
            }

            public double BaseAlpha = 1;

            public double BaseMinThreshold = 10;

            public double BaseMaxThreshold = 20;

            public double BaseFilterArea = 10;

            public double NewAlpha = 1;

            public double NewMinThreshold = 10;

            public double NewMaxThreshold = 20;

            public double FilterArea = 10;

            private int minThreshold = 5;
            public int MinThreshold
            {
                get { return minThreshold; }
                set { minThreshold = value; }
            }

            internal bool displayGoldenSearchRegion = false;

            internal bool displaySearchRegion = true;

            internal bool displayResultRegion = true;

            internal bool displayCross = false;

            internal bool displayOutCircle = false;

            internal bool displayGoldenBaseXLDRegion = false;

            internal bool displayBaseXLDRegion = false;

            internal FillMode regionDrawMode = FillMode.Fill;

            internal FillMode outCircleDrawMode = FillMode.Margin;
        }

        [Serializable]
        internal class ResultsPar
        {
            private ROI _搜索区域;
            public ROI 搜索区域
            {
                get { return _搜索区域; }
                set { _搜索区域 = value; }
            }

            private HRegion _BaseXLDRegionAffine;
            public HRegion BaseXLDRegionAffine
            {
                get { return _BaseXLDRegionAffine; }
                set { _BaseXLDRegionAffine = value; }
            }

            public HRegion _RealXLDROIAffine;
            public HRegion RealXLDROIAffine
            {
                get { return _RealXLDROIAffine; }
                set { _RealXLDROIAffine = value; }
            }

            private List<XYU> _位置 = new List<XYU>();
            public List<XYU> 位置
            {
                get { return _位置; }
                set { _位置 = value; }
            }

            private HRegion _resultRegion;
            public HRegion resultRegion
            {
                get
                {
                    if (_resultRegion == null)
                    {
                        _resultRegion = new HRegion();
                    }
                    return _resultRegion;
                }
                set { _resultRegion = value; }
            }

            private int _resultCount;
            public int resultCount
            {
                get { return _resultCount; }
                set { _resultCount = value; }
            }

            internal List<ResultPar> L_resultRegion = new List<ResultPar>();
        }

        [Serializable]
        public class ResultPar
        {
            [NonSerialized]
            private bool mIsDisposed = false;
            private int mBlobID;
            private bool mFilteredOut;
            private double[] mFilteredResultOut;
            #region 特征数组定义
            public static string[] featuresArray = new string[37] { "area","row","column", "width", "height",
            "row1","column1","row2", "column2", "circularity",
            "compactness","contlength","convexity", "rectangularity", "ra",
            "rb","phi","anisometry", "bulkiness", "struct_factor",

             "outer_radius","inner_radius","inner_width", "inner_height", "dist_mean",
            "dist_deviation","roundness","num_sides", "connect_num", "holes_num",
             "area_holes","max_diameter","orientation", "euler_number", "rect2_phi",
            "rect2_len1","rect2_len2"
        };
            public static string[] featuresArrayT = new string[38] { "对象的面积","中心点的行坐标","中心点的列坐标", "区域的宽度", "区域的高度",
            "左上角行坐标","左上角列坐标","右下角行坐标", "右下角列坐标", "圆度",
            "紧密度","轮廓线总长","凸性", "矩形度", "等效椭圆长轴半径长度",
            "等效椭圆短轴半径长度","等效椭圆方向","椭圆参数 Ra/Rb长轴与短轴的比值", "椭圆参数，蓬松度π*Ra*Rb/A", "椭圆参数 Anisometry*Bulkiness-1",

             "最小外接圆半径","最大内接圆半径","最大内接矩形宽度", "最大内接矩形高度", "区域边界到中心的平均距离",
            "区域边界到中心距离的偏差","圆度 与circularity计算方法不同","多边形边数", "连通数", "区域内洞数",
             "所有洞的面积","最大直径","区域方向", "欧拉数 即连通数和洞数的差", "最小外接矩形的方向",
            "最小外接矩形长度的一半","最小外接矩形宽度的一半","添加所有"
        };
            #endregion

            public static string Findfeature(string f)
            {
                int i = 0;
                foreach (var item in featuresArray)
                {
                    if (item == f)
                    {
                        break;
                    }
                    i++;
                }
                return featuresArrayT[i];

            }

            public int ID
            {
                get
                {
                    if (this.mIsDisposed)
                    {
                        throw new ObjectDisposedException("ID");
                    }
                    return this.mBlobID;
                }
            }

            public bool FilteredOut
            {
                get
                {
                    if (this.mIsDisposed)
                    {
                        throw new ObjectDisposedException("FilteredOut");
                    }
                    return this.mFilteredOut;
                }
            }

            public double[] FilteredResultOut
            {
                get { return mFilteredResultOut; }
            }

            HRegion mRegion;
            public HRegion MRegion
            {
                get { return mRegion; }
            }

            public double CenterOfMassY
            {
                get
                {
                    if (this.mIsDisposed)
                    {
                        throw new ObjectDisposedException("CenterOfMassY");
                    }

                    return this.mFilteredResultOut[2];
                }
            }

            public double CenterOfMassX
            {
                get
                {
                    if (this.mIsDisposed)
                    {
                        throw new ObjectDisposedException("CenterOfMassX");
                    }
                    return this.mFilteredResultOut[1];
                }
            }

            public double Area
            {
                get
                {
                    if (this.mIsDisposed)
                    {
                        throw new ObjectDisposedException("Area");
                    }
                    return this.mFilteredResultOut[0];
                }
            }

            internal ResultPar(int blobID, bool filteredOut, double[] FilteredResultOut, HRegion region)
            {
                this.mBlobID = blobID;
                this.mFilteredOut = filteredOut;
                this.mFilteredResultOut = FilteredResultOut;
                this.mRegion = region;
            }

            private string ValueToString(double val)
            {
                if (val >= 1000.0 && val < 1000000.0)
                {
                    return string.Format("{0:f0}", val);
                }
                return string.Format("{0:g3}", val);
            }

            private string ValuePairToStringComma(double val1, double val2)
            {
                if (val1 >= 1000.0 && val1 < 1000000.0 && val2 >= 1000.0 && val2 <= 1000000.0)
                {
                    return string.Format("({0:f0},{1:f0})", val1, val2);
                }
                return string.Format("({0:g3},{1:g3})", val1, val2);
            }

            private string ValuePairToStringX(double val1, double val2)
            {
                if (val1 >= 1000.0 && val1 < 1000000.0 && val2 >= 1000.0 && val2 <= 1000000.0)
                {
                    return string.Format("({0:f0}x{1:f0})", val1, val2);
                }
                return string.Format("({0:g3}x{1:g3})", val1, val2);
            }

        }

        [Serializable]
        internal class ItemType
        {
            internal ItemType(string type, string itemName, itemBase item)
            {
                this.type = type;
                this.itemName = itemName;
                this.item = item;
            }
            internal string type;
            internal string itemName;
            internal itemBase item;
            internal bool enable = true;
        }

        [Serializable]
        internal class itemBase
        {

        }

        [Serializable]
        public class OpenCircle : itemBase
        {
            internal double r = 3;
        }

        [Serializable]
        public class CloseCircle : itemBase
        {
            internal double r = 3;
        }

        [Serializable]
        public class DilationCircle : itemBase
        {
            internal double r = 3;
        }

        [Serializable]
        public class ErosionCircle : itemBase
        {
            internal double r = 3;
        }

    }
}
