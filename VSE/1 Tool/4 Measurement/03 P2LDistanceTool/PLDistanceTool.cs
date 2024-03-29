using HalconDotNet;
using Lxc.VisionPlus.ImageView;
using System;
using VSE.Core;

namespace VSE
{
    [Serializable]
    internal class PLDistanceTool : ToolBase
    {
        public ToolPar toolPar = new ToolPar();

        /// <summary>
        /// 用于外界获取区域变量
        /// </summary>
        //public HObject ToolUnionRegions;

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
                    HXLDCont P1 = new HXLDCont();
                    HRegion L1 = new HRegion();
                   
                    double Y1 = toolPar.InputPar.Point1.Y;
                    double X1= toolPar.InputPar.Point1.X;
                    double L1Y1 = toolPar.InputPar.Line1.起点.Y;
                    double L1X1 = toolPar.InputPar.Line1.起点.X;
                    double L1Y2 = toolPar.InputPar.Line1.终点.Y;
                    double L1X2 = toolPar.InputPar.Line1.终点.X;
                   
                    HTuple D;
                    P1.GenCrossContourXld(Y1,X1, 80, 0);
                    L1.GenRegionLine(L1Y1, L1X1, L1Y2, L1X2);
                
                    if (Win_PLDistanceTool.Instance.Visible)
                    {
                        Win_PLDistanceTool.Instance.PImageWin.displayHRegion(P1, "red");
                        Win_PLDistanceTool.Instance.PImageWin.displayHRegion(L1, "green");
                    }

                    HOperatorSet.DistancePl(X1, Y1, L1X1, L1Y1, L1X2, L1Y2, out D);

                    //toolPar.ResultPar.Distance = D;
                    double distancePL = D * toolPar.InputPar.VisionAccuracy;
                    toolPar.ResultPar.Distance = Convert.ToDouble(distancePL.ToString("0.000"));//point to line distance

                    HTuple ProX1;
                    HTuple ProY1;
                    HOperatorSet.ProjectionPl(X1, Y1, L1X1, L1Y1, L1X2, L1Y2, out ProX1, out ProY1);

                    HRegion RLine = new HRegion();
                    RLine.GenRegionLine(Y1, X1, ProY1, ProX1);

                    if (Win_PLDistanceTool.Instance.Visible)
                    {
                        ViewWindow.DisMsg(Win_PLDistanceTool.Instance.PImageWin.hv_window, D.D.ToString("f3"), ImgView.CoordSystem.image, Y1, X1, "coral");
                        Win_PLDistanceTool.Instance.vTextBox1.TextStr = toolPar.ResultPar.Distance.ToString("f3");
                        Win_PLDistanceTool.Instance.PImageWin.displayHRegion(RLine, "coral");
                        if ((toolPar.ResultPar.Distance) <= toolPar.InputPar.Tolerance_max && (toolPar.ResultPar.Distance)  >= toolPar.InputPar.Tolerance_min)
                        {                            
                            toolPar.ResultPar.Consequence = true;
                            Win_PLDistanceTool.Instance.vTextBox4.TextStr = "OK";
                        }
                        else
                        {                            
                            toolPar.ResultPar.Consequence = false;
                            Win_PLDistanceTool.Instance.vTextBox4.TextStr = "NG";
                        }
                    }

                    GetImageWindowControl().BeginInvoke(new System.Windows.Forms.MethodInvoker(() =>
                    {
                        GetImageWindowControl().displayHRegion(P1, "red");
                        GetImageWindowControl().displayHRegion(L1, "green");
                        GetImageWindowControl().displayHRegion(RLine, "coral");
                    }));

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

            private XY point1;
            public XY Point1
            {
                get { return point1; }
                set { point1 = value; }
            }

            private Line line1;
            public Line Line1
            {
                get { return line1; }
                set { line1 = value; }
            }

            /// <summary>
            /// 视觉换算精度
            /// </summary>
            private double visionAccuracy = 0.003;
            public double VisionAccuracy
            {
                get { return visionAccuracy; }
                set { visionAccuracy = value; }
            }

            /// <summary>
            /// 判定公差上限
            /// </summary>
            private double tolerance_max = 0;
            public double Tolerance_max
            {
                get { return tolerance_max; }
                set { tolerance_max = value; }
            }
            /// <summary>
            /// 判定公差下限
            /// </summary>
            private double tolerance_min = 0;
            public double Tolerance_min
            {
                get { return tolerance_min; }
                set { tolerance_min = value; }
            }
        }
      
        [Serializable]
        internal class ResultPar
        {
            double distance = 0;
            bool  consequence = false ;

            public double Distance
            {
                get
                {
                    return distance;
                }
                set
                {
                    distance = value;
                }
            }
            public bool Consequence
            {
                get
                {
                    return consequence;
                }
                set
                {
                    consequence = value;
                }
            }
        }
    }
}
