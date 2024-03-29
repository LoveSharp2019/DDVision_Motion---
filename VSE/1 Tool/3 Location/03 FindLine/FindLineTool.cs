using HalconDotNet;
using Lxc.VisionPlus.ImageView.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VSE.Core;

namespace VSE
{
    [Serializable]
    internal class FindLineTool : ToolBase
    {
        public FindLineTool()
        {

        }

        public enum EdgePolarity
        {
            all,
            positive,
            negative
        }

        public ToolPar toolPar = new ToolPar();

        public override void Run(bool updateImage, bool runTool, string toolName)
        {
            try
            {
                lock (obj)
                {
                    #region 检查输入图像
                    toolRunStatu = ToolRunStatu.未知原因;
                    if (toolPar.InputPar.InPuImage == null || !toolPar.InputPar.InPuImage.IsInitialized())
                    {
                        toolRunStatu = (ToolRunStatu.未指定输入图像);
                        return;
                    }
                    //是否有位置输入
                    if (toolPar.InputPar.Pose == null)
                    {
                        toolRunStatu = ToolRunStatu.未指定输入坐标点;
                        return;
                    }
                    //更新图片
                    if (updateImage)
                    {
                        //Win_FindLineTool.Instance.PImageWin.Image = toolPar.InputPar.图像;
                    }
                    #endregion

                    #region 计算抓边结果
                    bool iSok = MeasureLine(toolPar.InputPar.InPuImage, toolPar.InputPar.SerachDir, toolPar.InputPar.CaliperCount, toolPar.InputPar.CaliperLength, toolPar.InputPar.CaliperWidth, toolPar.InputPar.CaliperThreshold,new ROICalipersLine(toolPar.InputPar.ROIFindLine), out Results LineResult, out HXLDCont m_MeasureXLD);
                    toolPar.Results.MeasureXLD = m_MeasureXLD;
                    if (!iSok)
                    {
                        toolPar.Results.Line=null;
                        
                        toolRunStatu = ToolRunStatu.抓边工具特征点数量不足;
                        return;
                    }
                    toolPar.Results = LineResult;
                    #endregion

                    #region 设定UI界面显示
                    //重新显示图像

                    if (Win_FindLineTool.Instance.Visible)
                    {
                        Win_FindLineTool.Instance.PImageWin.Image = toolPar.InputPar.InPuImage;
                        Win_FindLineTool.Instance.dgv_Result.Rows.Clear();
                        Win_FindLineTool.Instance.PImageWin.viewWindow._hWndControl.clearHRegionList();
                    }
                    if (LineResult.row.Length == 0)
                    {
                        toolRunStatu = ToolRunStatu.抓边工具特征点数量不足;
                        return;
                    }

                    Line _outLineInfoshow = new Line();
                    _outLineInfoshow = LineResult.Line;
                    if (toolPar.Results.ResultXLD == null)
                    {
                        toolPar.Results.ResultXLD = new HXLDCont();
                    }
                    toolPar.Results.ResultXLD.GenContourPolygonXld(new HTuple(_outLineInfoshow.起点.Y, _outLineInfoshow.终点.Y), new HTuple(_outLineInfoshow.起点.X, _outLineInfoshow.终点.X));
                    HRegion L = new HRegion();

                    if (toolPar.RunPar.ShowLine)
                    {
                        HTuple RowCenter;
                        HTuple ColCenter;
                        HTuple lineLength;
                        HTuple Phi;

                        L.GenRegionLine(_outLineInfoshow.起点.Y, _outLineInfoshow.起点.X, _outLineInfoshow.终点.Y, _outLineInfoshow.终点.X);
                        HOperatorSet.LinePosition(_outLineInfoshow.起点.Y, _outLineInfoshow.起点.X, _outLineInfoshow.终点.Y, _outLineInfoshow.终点.X, out RowCenter, out ColCenter, out lineLength, out Phi);

                        HTuple Row1, Row2, Col1, Col2;
                        lineLength = lineLength * 10;

                        Row1 = RowCenter - (Phi - 1.5708).TupleCos() * lineLength;
                        Col1 = ColCenter - (Phi - 1.5708).TupleSin() * lineLength;
                        Row2 = RowCenter + (Phi - 1.5708).TupleCos() * lineLength;
                        Col2 = ColCenter + (Phi - 1.5708).TupleSin() * lineLength;
                        L.GenRegionLine(Row1, Col1, Row2, Col2);
                    }

                    if (Win_FindLineTool.Instance.Visible)
                    {
                        if (toolPar.RunPar.ShowSegment)
                        {
                            Win_FindLineTool.Instance.PImageWin.displayHRegion(toolPar.Results.ResultXLD, "blue", "fill", 2);
                        }
                        if(toolPar.RunPar.ShowLine)
                        {
                            Win_FindLineTool.Instance.PImageWin.displayHRegion(L, "blue", "fill", 2);
                        }
                        if (toolPar.RunPar.ShowCalipers)
                        {
                            Win_FindLineTool.Instance.PImageWin.displayHRegion(toolPar.Results.MeasureXLD, "green");
                        }
                        if (toolPar.RunPar.ShowPoint)
                        {
                            toolPar.Results.MeasureCross.GenCrossContourXld(LineResult.row, LineResult.col, (HTuple)50, new HTuple(_outLineInfoshow.GetAngle() + 0.785398));
                            Win_FindLineTool.Instance.PImageWin.displayHRegion(toolPar.Results.MeasureCross, "green");
                        }
                    }

                    if (toolPar.RunPar.ShowSegment)
                    {
                        GetImageWindowControl().BeginInvoke(new MethodInvoker(() =>
                        {
                            GetImageWindowControl().displayHRegion(toolPar.Results.ResultXLD, "blue", "fill", 2);
                        }));
                    }
                   if(toolPar.RunPar.ShowLine)
                    {
                        GetImageWindowControl().BeginInvoke(new MethodInvoker(() =>
                        {
                            GetImageWindowControl().displayHRegion(L, "blue", "fill", 2);
                        }));
                    }
                    if (toolPar.RunPar.ShowCalipers)
                    {
                        GetImageWindowControl().BeginInvoke(new MethodInvoker(() =>
                        {
                            GetImageWindowControl().displayHRegion(m_MeasureXLD, "green");
                        }));
                    }
                    if (toolPar.RunPar.ShowPoint)
                    {
                        toolPar.Results.MeasureCross.GenCrossContourXld(LineResult.row, LineResult.col, (HTuple)50, new HTuple(_outLineInfoshow.GetAngle() + 0.785398));
                        GetImageWindowControl().BeginInvoke(new MethodInvoker(() =>
                        {
                            GetImageWindowControl().displayHRegion(toolPar.Results.MeasureCross, "green");
                        }));
                    }

                    #endregion

                    toolRunStatu = ToolRunStatu.成功;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        public void UpDateROI()
        {
            //HTuple SPR = toolPar.InputPar.ROIFindLine.RowBegin;
            //HTuple SPC = toolPar.InputPar.ROIFindLine.ColumnBegin;
            //HTuple EPR = toolPar.InputPar.ROIFindLine.RowEnd;
            //HTuple EPC = toolPar.InputPar.ROIFindLine.ColumnEnd;
            //if (toolPar.InputPar.Pose != null && toolPar.InputPar.Pose.Count > 0)
            //{
            //    XYU PoseTemp = toolPar.InputPar.Pose[0];
            //    if (toolPar.InputPar.StandardPose==null)
            //    {
            //        toolPar.InputPar.StandardPose = PoseTemp;

            //    }
            //    //toolPar.InputPar.StandardPose = PoseTemp;
            //    HTuple homMat2D1;
            //    HOperatorSet.HomMat2dIdentity(out homMat2D1);//
            //    HOperatorSet.VectorAngleToRigid(toolPar.InputPar.StandardPose.Point.Y, toolPar.InputPar.StandardPose.Point.X, toolPar.InputPar.StandardPose.U, PoseTemp.Point.Y, PoseTemp.Point.X, PoseTemp.U, out homMat2D1);
            //    HOperatorSet.AffineTransPoint2d(homMat2D1, toolPar.InputPar.ROIFindLine.RowBegin, toolPar.InputPar.ROIFindLine.ColumnBegin, out SPR, out SPC);
            //    HOperatorSet.AffineTransPoint2d(homMat2D1, toolPar.InputPar.ROIFindLine.RowEnd, toolPar.InputPar.ROIFindLine.ColumnEnd, out EPR, out EPC);
               
               
           // }
        }
            
        /// <summary>
        /// 检测直线
        /// </summary>
        /// <param name="inImage">检测图像</param>
        /// <param name="inLine">输入检测直线区域</param>
        /// <param name="inMetrology">形态参数</param>
        /// <param name="outLine">输出直线</param>
        /// <param name="outR">输出行点</param>
        /// <param name="outC">输出列点</param>
        /// <param name="outMeasureXLD">输出检测轮廓</param>
        public bool MeasureLine(HImage inImage, EdgePolarity SearchDir, int CalipersCount, double SearchLength, double CaliperWidth, double CaliperThreshold, ROICalipersLine roi, out Results outLine, out HXLDCont outMeasureXLD)
        {
            HMetrologyModel hMetrologyModel = new HMetrologyModel();
            HTuple outR;
            HTuple outC;
            try
            {
                outLine = new Results();
                HTuple lineResult = new HTuple();
                HTuple lineInfo = new HTuple();
                HTuple ParamName = new HTuple();
                HTuple ParamValue = new HTuple();
                ParamName.Append("measure_transition");//边缘极性
                ParamName.Append("num_measures");
                ParamValue.Append(SearchDir.ToString());
                ParamValue.Append(CalipersCount);
                // 计算跟随
                HTuple SPR = roi.RowBegin;
                HTuple SPC = roi.ColumnBegin;
                HTuple EPR = roi.RowEnd;
                HTuple EPC = roi.ColumnEnd;
                XYU PoseTemp = toolPar.InputPar.StandardPose;
                if (toolPar.InputPar.Pose!=null&& toolPar.InputPar.Pose.Count>0)
                {
                    if (toolPar.InputPar.StandardPose==null)
                    {
                        toolPar.InputPar.StandardPose = new XYU();
                        //toolPar.InputPar.StandardPose.Point.X = 0;
                        //toolPar.InputPar.StandardPose.Point.Y = 0;
                        //toolPar.InputPar.StandardPose.U = 0;// = new XY(0,0);
                    }
                    PoseTemp = toolPar.InputPar.Pose[0];
                    HTuple homMat2D1;
                    HOperatorSet.HomMat2dIdentity(out homMat2D1);//
                    HOperatorSet.VectorAngleToRigid(toolPar.InputPar.StandardPose.Point.Y, toolPar.InputPar.StandardPose.Point.X, toolPar.InputPar.StandardPose.U, PoseTemp.Point.Y, PoseTemp.Point.X, PoseTemp.U, out homMat2D1);
                    HOperatorSet.AffineTransPoint2d(homMat2D1, roi.RowBegin, roi.ColumnBegin, out SPR, out SPC);
                    HOperatorSet.AffineTransPoint2d(homMat2D1, roi.RowEnd, roi.ColumnEnd, out EPR, out EPC);
                    roi.RowBegin = SPR;
                    roi.RowEnd = EPR;
                    roi.ColumnBegin = SPC;
                    roi.ColumnEnd = EPC;

                    //System.Diagnostics.Debug.WriteLine("转换后的卡尺"+EPR.ToString());
                    //System.Diagnostics.Debug.WriteLine("转换前的" + toolPar.InputPar.ROIFindLine.RowEnd.ToString());
                }
                lineInfo.Append(new HTuple(new double[] { SPR, SPC, EPR, EPC }));
                hMetrologyModel.AddMetrologyObjectGeneric(new HTuple("line"), lineInfo, new HTuple(SearchLength / 2),
                    new HTuple(CaliperWidth / 2), new HTuple(1), new HTuple(CaliperThreshold)
                    , ParamName, ParamValue);
                
                hMetrologyModel.ApplyMetrologyModel(inImage);

                outMeasureXLD = hMetrologyModel.GetMetrologyObjectMeasures("all", "all", out outR, out outC);
                lineResult = hMetrologyModel.GetMetrologyObjectResult(new HTuple("all"), new HTuple("all"), new HTuple("result_type"), new HTuple("all_param"));
                if (lineResult.TupleLength() >= 4)
                {
                    outLine.Line = new Line(lineResult[1].D, lineResult[0].D, lineResult[3].D, lineResult[2].D);

                }
                else
                {
                    if (outR.TupleLength()<2)
                    {
                        outLine = new Results();

                        outMeasureXLD = new HXLDCont();
                        hMetrologyModel.Dispose();
                        return false;
                    }
                    HXLDCont temp = new HXLDCont();
                    temp.GenContourPolygonXld(outR, outC);
                    temp.FitLineContourXld("tukey", -1, 0, 5, 2, out double a, out double b, out double c, out double d, out _, out _, out _);
                    outLine.Line = new Line(b,a, d, c);
                }
                outLine.row = outR;
                outLine.col = outC;
                hMetrologyModel.Dispose();
                toolPar.InputPar.StandardPose = PoseTemp;
                toolPar.InputPar.ROIFindLine = roi;
                return true;
            }
            catch (Exception )
            {

                outLine = new Results();

                outMeasureXLD = new HXLDCont();
                hMetrologyModel.Dispose();
                return false;
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

            private Results _resultPar = new Results();
            public Results Results
            {
                get { return _resultPar; }
                set { _resultPar = value; }
            }
        }
        [Serializable]
        public class InputPar
        {
            private HImage inPutImage;

            public HImage InPuImage
            {
                get { return inPutImage; }
                set { inPutImage = value; }
            }
            private List<XYU> pose;
            public List<XYU> Pose
            {
                get { return pose; }
                set { pose = value; }
            }
            private XYU standardPose;
            private ROICalipersLine roiFindLine;

            public ROICalipersLine ROIFindLine
            {
                get
                {
                    if (roiFindLine == null)
                    {
                        HTuple w, h;
                        inPutImage.GetImageSize(out w, out h);
                        roiFindLine = new ROICalipersLine(h / 2 - w / 6, w / 2 - w / 6, h / 2 + w / 6, w / 2 + w / 6);
                        roiFindLine.CaliperCount = CaliperCount;
                        roiFindLine.ProjectionLength = CaliperWidth;
                        roiFindLine.SearchLength = caliperLength;

                    }

                    return roiFindLine;
                }
                set { roiFindLine = value; }
            }
            int caliperCount;
            int caliperLength;
            int caliperWidth;
            int caliperThreshold;
            EdgePolarity serachDir;
            public int CaliperCount
            {
                get
                {
                    if (caliperCount == 0)
                    { caliperCount = 6; }
                    return caliperCount;
                }
                set
                {
                    caliperCount = value;
                }
            }
            public int CaliperLength
            {
                get
                {
                    if (caliperLength == 0)
                    { caliperLength = 100; }
                    return caliperLength;
                }
                set
                {
                    caliperLength = value;
                }
            }
            public int CaliperWidth
            {
                get
                {
                    if (caliperWidth == 0)
                    { caliperWidth = 50; }
                    return caliperWidth;
                }
                set
                {
                    caliperWidth = value;
                }
            }
            public int CaliperThreshold
            {
                get
                {
                    if (caliperThreshold == 0)
                    { caliperThreshold = 30; }
                    return caliperThreshold;
                }
                set
                {
                    caliperThreshold = value;
                }
            }
            public EdgePolarity SerachDir
            {
                get
                {

                    return serachDir;
                }
                set
                {
                    serachDir = value;
                }
            }

            public XYU StandardPose
            {
                get { return standardPose; }
                set { standardPose = value; }
            }
        }
        [Serializable]
        public class RunPar
        {
            public bool ShowCalipers = false;
            public bool ShowSegment = true;
            public bool ShowPoint = false;
            public bool ShowLine = false;
        }
        [Serializable]
        public class Results
        {

            //private HXLDCont m_ResultXLD;    ///检测结果轮廓

            //private HXLDCont m_MeasureXLD;   ///检测形态轮廓

            //private HXLDCont m_MeasureCross; ///检测点十字
            private Line line;
            public Line Line
            {
                get
                {
                    if (line == null)
                    {
                        line = new Line();
                    }
                    return line;
                }
                set { line = value; }
            }

            /// <summary>
            ///输出检测点row
            /// </summary>
            private HTuple m_row = new HTuple();
            /// <summary>
            ///   //输出检测点col
            /// </summary>
            private HTuple m_col = new HTuple();
            public int PointCounts
            {
                get
                {
                    return this.m_row.Length;
                }
            }
            public HTuple row
            {
                get
                {
                    return m_row;
                }

                set
                {
                    m_row = value;
                }
            }
            public HTuple col
            {
                get
                {
                    return m_col;
                }

                set
                {
                    m_col = value;
                }
            }
            [NonSerialized]
            public HXLDCont ResultXLD = new HXLDCont();
            [NonSerialized]
            public HXLDCont MeasureXLD = new HXLDCont();
            [NonSerialized]
            public HXLDCont MeasureCross = new HXLDCont();
        }
        #endregion
    }


}
