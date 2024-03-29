using HalconDotNet;
using Lxc.VisionPlus.ImageView.Model;
using System;
using System.Collections.Generic;
using VSE.Core;

namespace VSE
{
    [Serializable]
    internal class LLAngleTool : ToolBase
    {
        public ToolPar toolPar = new ToolPar();

        /// <summary>
        /// 运行工具
        /// </summary>
        /// <param name="updateImage">是否刷新图像</param>
        public override void Run(bool updateImage, bool b, string toolName)
        {
            try
            {
                if (toolPar.InputPar.图像 == null)
                {
                    return;
                }
                lock (obj)
                {
                    toolRunStatu = ToolRunStatu.未知原因;

                    HImage image = new HImage();
                    HRegion L1 = new HRegion();
                    HRegion L2 = new HRegion();
                    double L1Y1 = toolPar.InputPar.Line1.起点.Y;
                    double L1X1= toolPar.InputPar.Line1.起点.X;
                    double L1Y2 = toolPar.InputPar.Line1.终点.Y;
                    double L1X2 = toolPar.InputPar.Line1.终点.X;
                    double L2Y1 = toolPar.InputPar.Line2.起点.Y;
                    double L2X1 = toolPar.InputPar.Line2.起点.X;
                    double L2Y2 = toolPar.InputPar.Line2.终点.Y;
                    double L2X2 = toolPar.InputPar.Line2.终点.X;
                    L1.GenRegionLine(L1Y1, L1X1, L1Y2, L1X2);
                    L2.GenRegionLine(L2Y1, L2X1, L2Y2, L2X2);
                    if (Win_LLAngleTool.Instance.Visible)
                    {
                        Win_LLAngleTool.Instance.PImageWin.displayHRegion(L1, "red");
                        Win_LLAngleTool.Instance.PImageWin.displayHRegion(L2, "green");
                     
                    }

                    HTuple angle;
                    HOperatorSet.AngleLl(L1Y1, L1X1, L1Y2, L1X2, L2Y1, L2X1, L2Y2, L2X2, out angle);
                    double dAngle = angle * 180 / Math.PI;
                    if (Math.Abs(dAngle) > 90 && Math.Abs(dAngle) < 180)
                    {
                        dAngle = 180 - Math.Abs(dAngle);
                    }
                    toolPar.ResultPar.Angle = Math.Abs(Convert.ToDouble(dAngle.ToString("0.000")));
                    if (toolPar.ResultPar.Angle <= (toolPar.InputPar.Tolerance_max) && toolPar.ResultPar.Angle >= (toolPar.InputPar.Tolerance_min))
                    {
                        toolPar.ResultPar.Consequence = true;
                        Win_LLAngleTool.Instance.vTextBox4.TextStr = "OK";
                        
                    }
                    else
                    {
                        toolPar.ResultPar.Consequence = false;
                        Win_LLAngleTool.Instance.vTextBox4.TextStr = "NG";
                       
                    }
                    if (Win_LLAngleTool.Instance.Visible)
                    {
                        Win_LLAngleTool.Instance.vTextBox1.TextStr = toolPar.ResultPar.Angle.ToString("f3");
                    }

                    GetImageWindowControl().BeginInvoke(new System.Windows.Forms.MethodInvoker(() =>
                    {
                        GetImageWindowControl().displayHRegion(L1, "red");
                        GetImageWindowControl().displayHRegion(L2, "green");
                    }));

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

            private Line line1;
            public Line Line1
            {
                get { return line1; }
                set { line1 = value; }
            }

            private Line line2;
            public Line Line2
            {
                get { return line2; }
                set { line2 = value; }
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
            double _angle = 999.999;

            public double Angle
            {
                get
                {
                    return _angle;
                }
                set 
                {
                    _angle = value;
                }
            }
            bool consequence = false;
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
