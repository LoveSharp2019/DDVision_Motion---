using System;
using HalconDotNet;
using System.Xml.Serialization;

namespace Lxc.VisionPlus.ImageView.Model
{
 
    [Serializable]
    public class ROIEllipse : ROI
    {

        [XmlElement(ElementName = "phi")]
        public double Phi
        {
            get { return this.phi; }
            set { this.phi = value; }
        }
        [XmlElement(ElementName = "Radius1")]
        public double Radius1
        {
            get { return this.radius1; }
            set { this.radius1 = value; }
        }
        [XmlElement(ElementName = "Radius2")]
        public double Radius2
        {
            get { return this.radius2; }
            set { this.radius2 = value; }
        }

        private double radius1, radius2;
        private double phi;
        private double[] rows, cols;  // first handle
        public ROIEllipse()
        {
            NumHandles = 5; // one at corner of circle + midpoint
            activeHandleIdx = 1;
            rows = new double[NumHandles - 1];
            cols = new double[NumHandles - 1];
            Type = ROIType.ROIEllipse;
        }
        public ROIEllipse(double x, double y, double radius1, double radius2, double phi)
        {
            NumHandles = 5; // one at corner of circle + midpoint
            activeHandleIdx = 1;
            rows = new double[NumHandles - 1];
            cols = new double[NumHandles - 1];
            Type = ROIType.ROIEllipse;

            Column = x;
            Row = y;
            this.radius1 = radius1;
            this.radius2 = radius2;
            this.phi = phi;
            rows[0] = Row + radius1 * Math.Sin(this.phi);
            cols[0] = Column + radius1 * Math.Cos(this.phi);
            rows[1] = Row + radius2 * Math.Sin(this.phi + Math.PI / 2);
            cols[1] = Column + radius2 * Math.Cos(this.phi + Math.PI / 2);
            rows[2] = Row - radius1 * Math.Sin(this.phi);
            cols[2] = Column - radius1 * Math.Cos(this.phi);
            rows[3] = Row - radius2 * Math.Sin(this.phi + Math.PI / 2);
            cols[3] = Column - radius2 * Math.Cos(this.phi + Math.PI / 2);
        }
        public void UpdateROI(double x, double y, double radius1, double radius2, double phi)
        {
            Column = x;
            Row = y;
            this.radius1 = radius1;
            this.radius2 = radius2;
            this.phi = phi * Math.PI / 180.0;
            rows[0] = Row + radius1 * Math.Sin(this.phi);
            cols[0] = Column + radius1 * Math.Cos(this.phi);
            rows[1] = Row + radius2 * Math.Sin(this.phi + Math.PI / 2);
            cols[1] = Column + radius2 * Math.Cos(this.phi + Math.PI / 2);
            rows[2] = Row - radius1 * Math.Sin(this.phi);
            cols[2] = Column - radius1 * Math.Cos(this.phi);
            rows[3] = Row - radius2 * Math.Sin(this.phi + Math.PI / 2);
            cols[3] = Column - radius2 * Math.Cos(this.phi + Math.PI / 2);

        }
        //public override void createEllipse(double row, double col,double phi, double radius1, double radius2)
        //{
        //    base.createEllipse(row, col,phi, radius1, radius2);
        //    Row = row;
        //    Column = col;
        //    this.phi = phi;
        //    this.radius1 = radius1;
        //    this.radius2 = radius2;
        //    rows[0] = Row + radius1 * Math.Sin(phi);
        //    cols[0] = Column + radius1 * Math.Cos(phi);
        //    rows[1] = Row + radius2 * Math.Sin(phi + Math.PI / 2);
        //    cols[1] = Column + radius2 * Math.Cos(phi + Math.PI / 2);
        //    rows[2] = Row - radius1 * Math.Sin(phi);
        //    cols[2] = Column - radius1 * Math.Cos(phi);
        //    rows[3] = Row - radius2 * Math.Sin(phi + Math.PI / 2);
        //    cols[3] = Column - radius2 * Math.Cos(phi + Math.PI / 2);

        //    //row1 = Row;
        //    //col1 = Column + radius1;
        //}

        /// <summary>Creates a new ROI instance at the mouse position</summary>
        public override void createROI(double Column, double Row)
        {
            this.Row = Row;
            this.Column = Column;

            radius1 = 100;
            radius2 = 200;
            rows[0] = Row + radius1 * Math.Sin(phi);
            cols[0] = Column + radius1 * Math.Cos(phi);
            rows[1] = Row + radius2 * Math.Sin(phi + Math.PI / 2);
            cols[1] = Column + radius2 * Math.Cos(phi + Math.PI / 2);
            rows[2] = Row - radius1 * Math.Sin(phi);
            cols[2] = Column - radius1 * Math.Cos(phi);
            rows[3] = Row - radius2 * Math.Sin(phi + Math.PI / 2);
            cols[3] = Column - radius2 * Math.Cos(phi + Math.PI / 2);
        }

        /// <summary>Paints the ROI into the supplied window</summary>
        /// <param name="window">HALCON window</param>
        public override void draw(HalconDotNet.HWindow window, int imageWidth, int imageHeight)
        {
            window.SetLineWidth(2);//
            window.SetColor("spring green");
            double littleRecSize = 0;
            if (imageHeight < 300) littleRecSize = 1;
            else if (imageHeight < 600) littleRecSize = 2;
            else if (imageHeight < 900) littleRecSize = 3;
            else if (imageHeight < 1200) littleRecSize = 4;
            else if (imageHeight < 1500) littleRecSize = 5;
            else if (imageHeight < 1800) littleRecSize = 6;
            else if (imageHeight < 2100) littleRecSize = 7;
            else if (imageHeight < 2400) littleRecSize = 8;
            else if (imageHeight < 2700) littleRecSize = 9;
            else if (imageHeight < 3000) littleRecSize = 10;
            else if (imageHeight < 3300) littleRecSize = 11;
            else if (imageHeight < 3600) littleRecSize = 12;
            else if (imageHeight < 3900) littleRecSize = 13;
            else if (imageHeight < 4200) littleRecSize = 14;
            else if (imageHeight < 4500) littleRecSize = 15;
            else if (imageHeight < 4800) littleRecSize = 16;
            else if (imageHeight < 5100) littleRecSize = 17;
            else littleRecSize = 18;
            //绘制椭圆
            HXLDCont Ellipsexld = new HXLDCont();
            Ellipsexld.GenEllipseContourXld(Row, Column, -phi, radius1, radius2,0,6.28318, "positive", 0.1);
            window.DispXld(Ellipsexld);
            HXLDCont arrowxld = new HXLDCont();
            arrowxld = GenArrowContourXld(Row, Column, Row + (Math.Sin(phi) * radius1 * 1.2),
                Column + (Math.Cos(phi) * radius1 * 1.2), littleRecSize * 10, littleRecSize * 10);
            window.SetColor("blue");
            window.DispXld(arrowxld);
            window.SetDraw("fill");
            
            for (int i = 1; i < NumHandles - 1; i++)
            {
                window.DispRectangle2(rows[i], cols[i], 0, littleRecSize + 5, littleRecSize + 5);
            }

            window.DispCircle(Row, Column, littleRecSize + 5);
    
  

            window.DispCircle(rows[0], cols[0], littleRecSize + 5);
        }

        /// <summary> 
        /// Returns the distance of the ROI handle being
        /// closest to the image point(x,y)
        /// </summary>
        public override double distToClosestHandle(double x, double y)
        {
            double max = 10000;
            double[] val = new double[NumHandles];


            val[0] = HMisc.DistancePp(y, x, Row, Column); // midpoint 
            for (int i = 1; i < NumHandles; i++)
            {
                val[i] = HMisc.DistancePp(y, x, rows[i - 1], cols[i - 1]); // border handle 
            }
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
        /// Paints the active handle of the ROI object into the supplied window 
        /// </summary>
        public override void displayActive(HalconDotNet.HWindow window, int imageWidth, int imageHeight)
        {
            window.SetLineWidth(2);
            window.SetColor("magenta");
            double littleRecSize = 0;
            if (imageHeight < 300) littleRecSize = 1;
            else if (imageHeight < 600) littleRecSize = 2;
            else if (imageHeight < 900) littleRecSize = 3;
            else if (imageHeight < 1200) littleRecSize = 4;
            else if (imageHeight < 1500) littleRecSize = 5;
            else if (imageHeight < 1800) littleRecSize = 6;
            else if (imageHeight < 2100) littleRecSize = 7;
            else if (imageHeight < 2400) littleRecSize = 8;
            else if (imageHeight < 2700) littleRecSize = 9;
            else if (imageHeight < 3000) littleRecSize = 10;
            else if (imageHeight < 3300) littleRecSize = 11;
            else if (imageHeight < 3600) littleRecSize = 12;
            else if (imageHeight < 3900) littleRecSize = 13;
            else if (imageHeight < 4200) littleRecSize = 14;
            else if (imageHeight < 4500) littleRecSize = 15;
            else if (imageHeight < 4800) littleRecSize = 16;
            else if (imageHeight < 5100) littleRecSize = 17;
            else littleRecSize = 18;
            switch (activeHandleIdx)
            {
                case 0:
                    window.DispRectangle2(Row, Column, 0, littleRecSize + 5, littleRecSize + 5);
                    
                    break;
                case 1:
                    window.DispRectangle2(rows[activeHandleIdx - 1], cols[activeHandleIdx - 1], 0, littleRecSize + 5, littleRecSize + 5);
                    HXLDCont arrowxld = new HXLDCont();
                    arrowxld = GenArrowContourXld(Row, Column, Row + (Math.Sin(phi) * radius1 * 1.2),
                        Column + (Math.Cos(phi) * radius1 * 1.2), littleRecSize * 10, littleRecSize * 10);
                    window.DispXld(arrowxld);

                    break;
                default:
                    window.DispRectangle2(rows[activeHandleIdx - 1], cols[activeHandleIdx - 1], 0, littleRecSize + 5, littleRecSize + 5);
                    
                    break;
            }
        }

        /// <summary>Gets the HALCON region described by the ROI</summary>
        public override HRegion getRegion()
        {
            HRegion region = new HRegion();
            region.GenEllipse(Row, Column, -phi, radius1, radius2);
            return region;
        }

        public override double getDistanceFromStartPoint(double row, double col)
        {
            double sRow = Row; // assumption: we have an angle starting at 0.0
            double sCol = Column + 1 * radius1;

            double angle = HMisc.AngleLl(Row, Column, sRow, sCol, Row, Column, row, col);

            if (angle < 0)
                angle += 2 * Math.PI;

            return (radius1 * angle);
        }

        /// <summary>
        /// Gets the model information described by 
        /// the  ROI
        /// </summary> 
        public override HTuple getModelData()
        {
            return new HTuple(new double[] { Row, Column, radius1 });
        }

        /// <summary> 
        /// Recalculates the shape of the ROI. Translation is 
        /// performed at the active handle of the ROI object 
        /// for the image coordinate (x,y)
        /// </summary>
        public override void moveByHandle(double newX, double newY)
        {
            HTuple distance;
            double shiftX, shiftY;
            double vX, vY;
            switch (activeHandleIdx)
            {
                case 0: // handle at circle border
                    shiftY = Row - newY;
                    shiftX = Column - newX;

                    Row = newY;
                    Column = newX;

                    for (int i = 0; i < NumHandles - 1; i++)
                    {
                        rows[i] -= shiftY;
                        cols[i] -= shiftX;
                    }
                    break;

                default: // midpoint 
                    if (activeHandleIdx == 1)
                    {
                        vY = newY - Row;
                        vX = newX - Column;
                        phi = Math.Atan2(vY, vX);
                        //phi = Math.Atan2(vY, vX) + Math.PI / 2;
                        //if (phi > Math.PI)
                        //{
                        //    phi -= Math.PI * 2;
                        //}
                        //System.Diagnostics.Debug.WriteLine(((phi) * 180 / Math.PI).ToString());
                    }
                    else
                    {
                        HOperatorSet.DistancePp(new HTuple(newY), new HTuple(newX),
                                           new HTuple(Row), new HTuple(Column),
                                           out distance);
                        if (activeHandleIdx % 2 == 1)
                        {
                            radius1 = distance[0].D;
                        }
                        else
                        {
                            radius2 = distance[0].D;
                        }
                    }


                    rows[0] = Row + radius1 * Math.Sin(phi);
                    cols[0] = Column + radius1 * Math.Cos(phi);
                    rows[1] = Row + radius2 * Math.Sin(phi + Math.PI / 2);
                    cols[1] = Column + radius2 * Math.Cos(phi + Math.PI / 2);
                    rows[2] = Row - radius1 * Math.Sin(phi);
                    cols[2] = Column - radius1 * Math.Cos(phi);
                    rows[3] = Row - radius2 * Math.Sin(phi + Math.PI / 2);
                    cols[3] = Column - radius2 * Math.Cos(phi + Math.PI / 2);

                    break;
            }
        }
    }//end of class
}//end of namespace
