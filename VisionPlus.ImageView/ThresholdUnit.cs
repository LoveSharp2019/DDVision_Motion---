using HalconDotNet;
using System;
using System.Windows.Forms;

namespace Lxc.VisionPlus.ImageView
{
    public partial class ThresholdUnit : UserControl
    {
        private ThresholdPlot thresholdPlot_0;
        HTuple grayValues = new HTuple();
        public ThresholdUnit()
        {
            this.InitializeComponent();
       
        }

        public void setAxisAdaption(int mode)
        {
            this.thresholdPlot_0.setAxisAdaption(mode);
        }

        public void setLabel(string x, string y)
        {
            this.thresholdPlot_0.setLabel(x, y);
            this.lblX.Text = x;
            this.lblY.Text = y;
        }

        public void computeStatistics(HTuple grayVals)
        {
            if (grayVals != null && grayVals.Length > 1)
            {
                HTuple htuple = new HTuple(grayVals);
                HTuple htuple2 = htuple.TupleMean();
                this.labelMean.Text = htuple2[0].D.ToString("f2");
                htuple2 = htuple.TupleDeviation();
                this.labelDeviation.Text = htuple2[0].D.ToString("f2");
                htuple2 = htuple.TupleSortIndex();
                this.labelPeakX.Text = string.Concat(htuple2[htuple2.Length - 1].I);
                int num = (int)htuple[htuple2[htuple2.Length - 1].I].D;
                double sub = 0;
               
                for (int i = 0; i < htuple.Length; i++)
                {
                    sub += (int)htuple[i].D;
                }
                this.labelRange.Text = ((int)sub).ToString();
                this.labelPeak.Text = string.Format("{0} [{1}%]", num, (num / sub * 100).ToString("f1"));

                this.labelRangeX.Text = "0 ... " + (htuple.Length - 1);
            }
            else
            {
                this.labelMean.Text = "0";
                this.labelDeviation.Text = "0";
                this.labelPeakX.Text = "0";
                this.labelPeak.Text = "0";
                this.labelRange.Text = "0 ... 0";
                this.labelRangeX.Text = "0 ... 0";
            }
        }

        public void computeStatistics(double[] grayVals)
        {
            if (grayVals != null && grayVals.Length > 1)
            {
                this.computeStatistics(new HTuple(grayVals));
            }
            else
            {
                this.computeStatistics(new HTuple());
            }
        }

        public void setFunctionPlotValue(double[] grayValues)
        {
            this.thresholdPlot_0.drawFunction(new HTuple(grayValues));
        }

        public void setFunctionPlotValue(HTuple grayValues)
        {
            if (this.grayValues.Length==0)
            {
                this.grayValues = grayValues;
            }
            if (this.grayValues.Length == 0)
            {
                return;
            }
            this.thresholdPlot_0.drawFunction(this.grayValues);
        }

        public void setFunctionPlotValue(float[] grayValues)
        {
            this.thresholdPlot_0.drawFunction(new HTuple(grayValues));
        }

        public void setFunctionPlotValue(int[] grayValues)
        {
            this.thresholdPlot_0.drawFunction(new HTuple(grayValues));
        }
        bool first = true;
        private void ThresholdUnit_Resize(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                this.thresholdPlot_0 = new ThresholdPlot(this.panelAxis, true);
                setAxisAdaption(4);
                setLabel("灰度值", "频率");
                setFunctionPlotValue(grayValues);
                computeStatistics(grayValues);
            }
            else
            {
                this.thresholdPlot_0.GrawAxis(this.panelAxis);
                setAxisAdaption(4);
                setLabel("灰度值", "频率");
                setFunctionPlotValue(grayValues);
               computeStatistics(grayValues);
            }
         
        }

        private void ThresholdUnit_Load(object sender, EventArgs e)
        {
            this.thresholdPlot_0.GrawAxis(this.panelAxis);
            setAxisAdaption(4);
            setLabel("灰度值", "频率");
            setFunctionPlotValue(grayValues);
            computeStatistics(grayValues);
        }
    }
}
