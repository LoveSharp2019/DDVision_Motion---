using HalconDotNet;
using Lxc.VisionPlus.ImageView.Model;
using System;
using System.Collections.Generic;
using VSE.Core;

namespace VSE
{
    [Serializable]
    internal class FindCircleTool : ToolBase
    {
        public enum EdgePolarity
        {
            all,
            positive,
            negative
        }
        public FindCircleTool()
        {

        }
        #region 参数定义
        public ToolPar toolPar = new ToolPar();

        #endregion

        /// <summary>
        /// 用于外界获取区域变量
        /// </summary>
        //public HObject ToolUnionRegions;

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
                    #region 计算抓圆结果
                    bool iSok = MeasureCircle(toolPar.InputPar.InPuImage, new ROICalipersCircle(toolPar.InputPar.ROIFindCircle),out Results CircleResult, out HXLDCont m_MeasureXLD);
                    
                    if (!iSok)
                    {
                        toolRunStatu = ToolRunStatu.抓圆工具特征点数量不足;
                        return;
                    }

                    toolPar.Results = CircleResult;

                    #endregion

                    #region 设定UI界面显示
                    //重新显示图像

                    if (Win_FindCircleTool.Instance.Visible)
                    {
                        Win_FindCircleTool.Instance.PImageWin.Image = toolPar.InputPar.InPuImage;
                        Win_FindCircleTool.Instance.dgv_Result.Rows.Clear();
                        Win_FindCircleTool.Instance.PImageWin.viewWindow._hWndControl.clearHRegionList();
                    }

                    if (CircleResult.row.Length == 0)
                    {
                        toolRunStatu = ToolRunStatu.抓圆工具特征点数量不足;
                        return;

                    }

                    toolPar.Results.MeasureXLD = m_MeasureXLD;
                    Circle _outCircleInfoshow = new Circle();
                    _outCircleInfoshow = CircleResult.Circle;

                    if (toolPar.Results.ResultXLD == null)
                    {
                        toolPar.Results.ResultXLD = new HXLDCont();
                    }

                    //System.Diagnostics.Debug.WriteLine(_outCircleInfoshow.StartRad * 180 / Math.PI);
                    //System.Diagnostics.Debug.WriteLine(_outCircleInfoshow.EndRad * 180 / Math.PI);
                    toolPar.Results.ResultXLD.GenCircleContourXld(_outCircleInfoshow.圆心.Y, _outCircleInfoshow.圆心.X, _outCircleInfoshow.半径, _outCircleInfoshow.StartRad, _outCircleInfoshow.EndRad, "positive", 1);
                    HXLDCont L = new HXLDCont();
                  
                    if (toolPar.RunPar.ShowCircleCenter)
                    {
                        L.GenCrossContourXld(_outCircleInfoshow.圆心.Y, _outCircleInfoshow.圆心.X,30,0);
                    }

                    if (Win_FindCircleTool.Instance.Visible)
                    {
                        if (toolPar.RunPar.ShowCircle)
                        {
                            Win_FindCircleTool.Instance.PImageWin.displayHRegion(toolPar.Results.ResultXLD);
                        }

                        if (toolPar.RunPar.ShowCircleCenter)
                        {
                            Win_FindCircleTool.Instance.PImageWin.displayHRegion(L, "blue", "margin", 2);
                        }
                        
                        if (toolPar.RunPar.ShowCalipers)
                        {
                            Win_FindCircleTool.Instance.PImageWin.displayHRegion(toolPar.Results.MeasureXLD, "green");
                        }

                        if (toolPar.RunPar.ShowPoint)
                        {
                            toolPar.Results.MeasureCross.GenCrossContourXld(CircleResult.row, CircleResult.col, (HTuple)50, new HTuple(0.785398));
                            Win_FindCircleTool.Instance.PImageWin.displayHRegion(toolPar.Results.MeasureCross, "green");
                        }
                    }

                    if (toolPar.RunPar.ShowCircle)
                    {
                        GetImageWindowControl().BeginInvoke(new System.Windows.Forms.MethodInvoker(() =>
                        {
                            GetImageWindowControl().displayHRegion(toolPar.Results.ResultXLD, "blue");
                        }));
                    }

                    if (toolPar.RunPar.ShowCircleCenter)
                    {
                        GetImageWindowControl().BeginInvoke(new System.Windows.Forms.MethodInvoker(() =>
                        {
                            GetImageWindowControl().displayHRegion(L, "blue", "margin", 2);
                        }));
                    }

                    if (toolPar.RunPar.ShowCalipers)
                    {
                        GetImageWindowControl().BeginInvoke(new System.Windows.Forms.MethodInvoker(() =>
                        {
                            GetImageWindowControl().displayHRegion(m_MeasureXLD, "green");
                        }));
                    }

                    if (toolPar.RunPar.ShowPoint)
                    {
                        toolPar.Results.MeasureCross.GenCrossContourXld(CircleResult.row, CircleResult.col, (HTuple)50, new HTuple(0.785398));
                        GetImageWindowControl().BeginInvoke(new System.Windows.Forms.MethodInvoker(() =>
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
            //HTuple CPR;
            //HTuple CPC;
            //HTuple StartRad;
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
            //    HOperatorSet.AffineTransPoint2d(homMat2D1, toolPar.InputPar.ROIFindCircle.CenterY, toolPar.InputPar.ROIFindCircle.CenterX, out CPR, out CPC);
            //    StartRad = toolPar.InputPar.ROIFindCircle.StartRad + PoseTemp.U - toolPar.InputPar.StandardPose.U;
            //}
        }
        public bool MeasureCircle(HImage inImage,ROICalipersCircle roi, out Results outCircle, out HXLDCont outMeasureXLD)
        {
            HMetrologyModel hMetrologyModel = new HMetrologyModel();
            HTuple outR;
            HTuple outC;
            try
            {
                outCircle = new Results();
                HTuple CircleResult = new HTuple();
                HTuple CircleInfo = new HTuple();
                HTuple ParamName = new HTuple();
                HTuple ParamValue = new HTuple();
                HTuple CPR = roi.CenterY;
                HTuple CPC = roi.CenterX;
                HTuple StartRad = roi.StartRad;
                XYU PoseTemp = toolPar.InputPar.StandardPose;
                if (toolPar.InputPar.Pose != null && toolPar.InputPar.Pose.Count > 0)
                { if (toolPar.InputPar.StandardPose == null)
                    {
                        toolPar.InputPar.StandardPose = new XYU();
                    }
                PoseTemp = toolPar.InputPar.Pose[0];
                    HTuple homMat2D1;
                    HOperatorSet.HomMat2dIdentity(out homMat2D1);//
                    HOperatorSet.VectorAngleToRigid(toolPar.InputPar.StandardPose.Point.Y, toolPar.InputPar.StandardPose.Point.X, toolPar.InputPar.StandardPose.U, PoseTemp.Point.Y, PoseTemp.Point.X, PoseTemp.U, out homMat2D1);
                    HOperatorSet.AffineTransPoint2d(homMat2D1, roi.CenterY, roi.CenterX, out CPR, out CPC);
                    StartRad = roi.StartRad + PoseTemp.U - toolPar.InputPar.StandardPose.U;
                 
                    roi.CenterY = CPR;
                    roi.CenterX = CPC;
                    roi.StartRad = StartRad;
                }
                ParamName.Append("measure_transition");//边缘极性
                ParamName.Append("num_measures");
                ParamName.Append("start_phi");
                ParamName.Append("end_phi");
                ParamValue.Append(toolPar.InputPar.SerachDir.ToString());
                ParamValue.Append(toolPar.InputPar.CaliperCount);

                ParamValue.Append(roi.StartRad);
                ParamValue.Append(roi.StartRad+ roi.RangeRad);

                CircleInfo.Append(new HTuple(new double[] { roi.CenterY, roi.CenterX, roi.Radius }));


                hMetrologyModel.AddMetrologyObjectGeneric(new HTuple("circle"), CircleInfo, new HTuple(toolPar.InputPar.CaliperLength / 2),
                    new HTuple(toolPar.InputPar.CaliperWidth/ 2), new HTuple(1), new HTuple(toolPar.InputPar.CaliperThreshold)
                    , ParamName, ParamValue);

                hMetrologyModel.ApplyMetrologyModel(inImage);



                outMeasureXLD = hMetrologyModel.GetMetrologyObjectMeasures("all", "all", out outR, out outC);
                //hxldCont = hMetrologyModel.GetMetrologyObjectResultContour(new HTuple("all"), new HTuple("all"), 0.5);

                HTuple a, b, c, d, e, f;
                //CircleResult = hMetrologyModel.GetMetrologyObjectResult(new HTuple("all"), new HTuple("all"), new HTuple("result_type"), new HTuple("all_param"));
                //if (CircleResult.TupleLength() >= 3)
                //{
                //    outCircle.Circle.圆心.Y = CircleResult[0].D;
                //    outCircle.Circle.圆心.X = CircleResult[1].D;
                //    outCircle.Circle.半径 = CircleResult[2].D;
                //    outCircle.Circle.StartRad = CircleResult[3].D;
                //    outCircle.Circle.EndRad = CircleResult[4].D;
                //}
                //else
                //{
                    HXLDCont temp = new HXLDCont();
                    temp.GenContourPolygonXld(outR, outC);
                 
                    temp.FitCircleContourXld("geotukey", -1, 0, 0, 3, 2, out a, out b, out c, out d, out e, out f);
                    outCircle.Circle = new Circle(b,a,c);
                    outCircle.Circle.StartRad = d;
                    outCircle.Circle.EndRad = d+roi.RangeRad; //new UserDefine.Circle_INFO(a.D, b.D, c.D, d.D, e.D, f.S);
               // }
                outCircle.row = outR;
                outCircle.col = outC;
                hMetrologyModel.Dispose();
                toolPar.InputPar.StandardPose = PoseTemp;
                toolPar.InputPar.ROIFindCircle = roi;
                return true;
            }
            catch (Exception )
            {

                outCircle = new Results();
                outMeasureXLD = new HXLDCont();
                hMetrologyModel.Dispose();
                //throw new Exception(ex.Message);
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
            private ROICalipersCircle roiFindCircle;

            public ROICalipersCircle ROIFindCircle
            {
                get
                {
                    if (roiFindCircle == null)
                    {
                        HTuple w, h;
                        inPutImage.GetImageSize(out w, out h);
                        roiFindCircle = new ROICalipersCircle(h / 2 , w / 2 );
                        roiFindCircle.CaliperCount = CaliperCount;
                        roiFindCircle.ProjectionLength = CaliperWidth;
                        roiFindCircle.SearchLength = caliperLength;

                    }

                    return roiFindCircle;
                }
                set { roiFindCircle = value; }
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

            public XYU StandardPose { get { return standardPose; } set { standardPose = value; } }
        }
        [Serializable]
        public class RunPar
        {
            public bool ShowCalipers = false;
            public bool ShowCircleCenter = true;
            public bool ShowPoint = false;
            public bool ShowCircle= true;
        }
        [Serializable]
        public class Results
        {

            //private HXLDCont m_ResultXLD;    ///检测结果轮廓

            //private HXLDCont m_MeasureXLD;   ///检测形态轮廓

            //private HXLDCont m_MeasureCross; ///检测点十字
            private Circle circle;
            public Circle Circle
            {
                get
                {
                    if (circle == null)
                    {
                        circle = new Circle();
                    }
                    return circle;
                }
                set { circle = value; }
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
