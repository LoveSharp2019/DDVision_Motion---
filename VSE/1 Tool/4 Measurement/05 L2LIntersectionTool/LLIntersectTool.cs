using HalconDotNet;
using Lxc.VisionPlus.ImageView.Model;
using System;
using System.Collections.Generic;
using VSE.Core;

namespace VSE
{
    [Serializable]
    internal class LLIntersectTool : ToolBase
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
                    HTuple Y, X;
                    HTuple isInterSection;
                    L1.GenRegionLine(L1Y1, L1X1, L1Y2, L1X2);
                    L2.GenRegionLine(L2Y1, L2X1, L2Y2, L2X2);
                    if (Win_LLIntersectTool.Instance.Visible)
                    {
                        Win_LLIntersectTool.Instance.PImageWin.displayHRegion(L1, "red");
                        Win_LLIntersectTool.Instance.PImageWin.displayHRegion(L2, "green");
                     
                    }
                    HOperatorSet.IntersectionLines(L1Y1, L1X1, L1Y2, L1X2, L2Y1, L2X1, L2Y2, L2X2,out Y,out X,out isInterSection);

                    if (isInterSection==0&& Y.Length!=0&&!Y.D.Equals(double.NaN))
                    {
                        toolPar.ResultPar.IntersectionP = new XY(X,Y);
                        //toolPar.ResultPar.IntersectionP.Y = Y.D;
                        //toolPar.ResultPar.IntersectionP.X= X.D;
                        HXLDCont c = new HXLDCont();
                        c.GenCrossContourXld(Y,X,80,0);
                        if (Win_LLIntersectTool.Instance.Visible)
                        {
                            Win_LLIntersectTool.Instance.vTextBox1.TextStr = toolPar.ResultPar.IntersectionP.X.ToString("f3");
                            Win_LLIntersectTool.Instance.vTextBox2.TextStr = toolPar.ResultPar.IntersectionP.Y.ToString("f3");
                            Win_LLIntersectTool.Instance.PImageWin.displayHRegion(c, "coral");
                        }

                        GetImageWindowControl().BeginInvoke(new System.Windows.Forms.MethodInvoker(() =>
                        {
                            GetImageWindowControl().displayHRegion(L1, "red");
                            GetImageWindowControl().displayHRegion(L2, "green");
                            GetImageWindowControl().displayHRegion(c, "coral");
                        }));

                        toolPar.ResultPar.IsFind = true;
                    }
                    else
                    {
                        toolPar.ResultPar.IsFind = false;
                    }

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
          
        }

        [Serializable]
        internal class ResultPar
        {
            XY intersectionP;
            public XY IntersectionP
            {
                get
                {
                    if (intersectionP == null)
                    {
                        intersectionP = new XY();
                    }
                    return intersectionP;
                }
                set {  intersectionP = value; }
            }

            public bool IsFind=false;
         
        }
    }
}
