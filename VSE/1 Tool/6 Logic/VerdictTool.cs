using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSE.Core;

namespace VSE
{
    [Serializable]
    internal class VerdictTool : ToolBase
    {
        internal ToolPar toolPar = new ToolPar();

        public override void Run(bool updateImage, bool runTool, string toolName)
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
                    double value = toolPar.InputPar.DValue;
                    if (toolPar.RunPar.IsCheck)
                    {
                        value = value + toolPar.RunPar.Offset;
                        if (value >= toolPar.RunPar.Tolerance_min && value <= toolPar.RunPar.Tolerance_max)
                        {
                            toolPar.ResultPar.Consequence = true;
                        }
                        else
                        {
                            toolPar.ResultPar.Consequence = false;
                        }
                        toolPar.ResultPar.DValue = value;
                    }
                    else
                    {
                        toolPar.ResultPar.DValue = 999.999;
                        toolPar.ResultPar.Consequence = true;
                    }

                    if (Win_VerdictTool.Instance.Visible)
                    {
                        Win_VerdictTool.Instance.label_Val.Text = toolPar.ResultPar.DValue.ToString();
                        if (toolPar.ResultPar.Consequence)
                        {
                            Win_VerdictTool.Instance.label_Result.Text = "OK";
                        }
                        else
                        {
                            Win_VerdictTool.Instance.label_Result.Text = "NG";
                        }
                    }
                    toolRunStatu = ToolRunStatu.成功;
                }
            }
            catch (Exception ex)
            {
                toolRunStatu = ToolRunStatu.未知原因;
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
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

            private Double dValue;
            public Double DValue
            {
                get { return dValue; }
                set { dValue = value; }
            }

        }

        [Serializable]
        internal class RunPar
        {
            /// <summary>
            /// 是否启用
            /// </summary>
            private bool isCheck = false;
            public bool IsCheck
            {
                get { return isCheck; }
                set { isCheck = value; }
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

            /// <summary>
            /// 补偿值
            /// </summary>
            private double offset = 0;
            public double Offset
            {
                get { return offset; }
                set { offset = value; }
            }

        }

        [Serializable]
        internal class ResultPar
        {
            double dValue = 999.999;
            public double DValue
            {
                get { return dValue; }
                set { dValue = value; }
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
