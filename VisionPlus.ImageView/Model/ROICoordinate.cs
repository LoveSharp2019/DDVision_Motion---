using HalconDotNet;
using System;
using System.Xml.Serialization;

namespace Lxc.VisionPlus.ImageView.Model
{
    [Serializable]
    public class ROICoordinate : ROI
    {
        [XmlElement(ElementName = "Row")]
        public new double Row
        {
            get { return this.midy; }
            set { this.midy = value; }
        }
        [XmlElement(ElementName = "Column")]
        public new double Column
        {
            get { return this.midx; }
            set { this.midx = value; }
        }
        private double angle;   // 当前角度
        private double midy = 0, midx = 0;   // midPoint of line
        private const double length = 50;

        double row1, col1, row2, col2;

        public ROICoordinate()
        {
            NumHandles = 1;        // two end points of line
            activeHandleIdx = 0;
        }


        /// <summary>Creates a new ROI instance at the mouse position.</summary>
        public override void createROI(double midX, double midY)
        {
            midy = midY;
            midx = midX;
            angle = 0;
        }

        /// <summary>Paints the ROI into the supplied window.</summary>
        public override void draw(HalconDotNet.HWindow window,int imageWidth, int imageHeight)
        {
            if (midx == 0)
            {
                return;
            }
            window.SetLineWidth(1);
            row1 = Math.Cos(angle) * length + midy;
            col1 = Math.Sin(angle) * length + midx;
            row2 = Math.Cos(angle + Math.PI / 2) * length + midy;
            col2 = Math.Sin(angle + Math.PI / 2) * length + midx;

            window.DispLine(midy, midx, row1, col1);
            window.DispLine(midy, midx, row2, col2);
            window.DispLine(row2 + 10, col2 - 10, row2, col2);
            window.DispLine(row2 - 10, col2 - 10, row2, col2);
            window.DispLine(row1 - 10, col1 + 10, row1, col1);
            window.DispLine(row1 - 10, col1 -10, row1, col1);
            window.DispRectangle2(midy, midx, 0, 5, 5);
            // window.DispCross(midy, midx, 10, Math.PI / 4);
        }

        /// <summary> 
        /// Returns the distance of the ROI handle being
        /// closest to the image point(x,y).
        /// </summary>
        public override double distToClosestHandle(double x, double y)
        {

            double max = 10000;
            double[] val = new double[NumHandles];

            val[0] = HMisc.DistancePp(y, x, midy, midx); // midpoint 

            for (int i = 0; i < NumHandles; i++)
            {
                if (val[i] < max)
                {
                    max = val[i];
                    activeHandleIdx = i;
                }
            }// end of for 

            return val[activeHandleIdx];
        }

        /// <summary> 
        /// Paints the active handle of the ROI object into the supplied window. 
        /// </summary>
        public override void displayActive(HalconDotNet.HWindow window, int imageWidth, int imageHeight)
        {

            switch (activeHandleIdx)
            {
                case 0:
                    window.DispRectangle2(midy, midx, 0, 5, 5);
                    break;
            }
        }

        /// <summary>
        /// Gets the model information described by 
        /// the ROI.
        /// </summary> 
        public override HTuple getModelData()
        {
            return new HTuple(new double[] { midy, midx, angle });
        }

        /// <summary> 
        /// Recalculates the shape of the ROI. Translation is 
        /// performed at the active handle of the ROI object 
        /// for the image coordinate (x,y).
        /// </summary>
        public override void moveByHandle(double newX, double newY)
        {
            switch (activeHandleIdx)
            {
                case 0: // midpoint 
                    midy = newY;
                    midx = newX;
                    break;
            }
        }
        public void UpdateROI(double x, double y)
        {
            midx = x;
            midy = y;
            this.angle = 0;
        }
    }
}
