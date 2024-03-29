using HalconDotNet;
using System;
using System.Xml.Serialization;

namespace Lxc.VisionPlus.ImageView.Model
{
    [Serializable]
    public class ROICalipersLine : ROI
    {
        [XmlElement(ElementName = "RowBegin")]
        public double RowBegin
        {
            get { return this.row1; }
            set { this.row1 = value; }
        }

        [XmlElement(ElementName = "ColumnBegin")]
        public double ColumnBegin
        {
            get { return this.col1; }
            set { this.col1 = value; }
        }
        [XmlElement(ElementName = "RowEnd")]
        public double RowEnd
        {
            get { return this.row2; }
            set { this.row2 = value; }
        }

        [XmlElement(ElementName = "ColumnEnd")]
        public double ColumnEnd
        {
            get { return this.col2; }
            set { this.col2 = value; }
        }
        public int CaliperCount
        {
            get { return this.NumHandles; }
            set { this.NumHandles = value; }
        }
        public double SearchLength
        {
            get { return this.searchLength; }
            set { this.searchLength = value; }
        }
        public double ProjectionLength
        {
            get { return this.projectionLength; }
            set { this.projectionLength = value; }
        }
        private double row1, col1;   // first end point of line
        private double row2, col2;   // second end point of line
        private double searchLength = 30;
        private double projectionLength = 10;
        private HObject arrowHandleXLD;
        Core.LxcType.PointD[] CalipersCenter;

        public ROICalipersLine(double row1, double col1, double row2, double col2)
        {
            this.row1 = row1;
            this.row2 = row2;
            this.col1 = col1;
            this.col2 = col2;
            NumHandles = 6;        // two end points of line
            activeHandleIdx = 2;
            arrowHandleXLD = new HXLDCont();
            arrowHandleXLD.GenEmptyObj();
            Type = ROIType.ROICalipersLine;
        }
        public ROICalipersLine(ROICalipersLine clone)
        {
            this.RowBegin = clone.RowBegin;
            this.RowEnd = clone.RowEnd;
            this.ColumnBegin = clone.ColumnBegin;
            this.ColumnEnd = clone.ColumnEnd;
            this.CaliperCount = clone.CaliperCount;
            this.SearchLength = clone.SearchLength;
            this.ProjectionLength = clone.ProjectionLength;
            this.CalipersCenter = clone.CalipersCenter;
            this.arrowHandleXLD = new HObject(clone.arrowHandleXLD);
                ;
        }
        /// <summary>Creates a new ROI instance at the mouse position.</summary>
        public override void createROI(double Column, double Row)
        {
            this.Row = Row;
            this.Column = Column;

            row1 = Row;
            col1 = Column - 50;
            row2 = Row;
            col2 = Column + 50;

            updateArrowHandle();
        }
        /// <summary>Paints the ROI into the supplied window.</summary>
        public override void draw(HalconDotNet.HWindow window, int imageWidth, int imageHeight)
        {
            window.SetLineWidth(2);
            //通过起始点和结束点坐标计算其他所有卡尺点中点坐标
            CalipersCenter = new Core.LxcType.PointD[NumHandles];
            double rad = -Math.Atan((row2 - row1) / (col2 - col1));
            int count = NumHandles - 1;
            int k = -1;
            if (col2 >= col1)
            {
                k = 1;
            }
            double x1 = col1 + k * Math.Cos(rad) * ProjectionLength / 2;
            double y1 = row1 - k * Math.Sin(rad) * ProjectionLength / 2;
            double x2 = col2 - k * Math.Cos(rad) * ProjectionLength / 2;
            double y2 = row2 + k * Math.Sin(rad) * ProjectionLength / 2;



            for (int i = 0; i <= count; i++)
            {
                CalipersCenter[i].X = ((count - i) * x1 + i * x2) / count;
                CalipersCenter[i].Y = ((count - i) * y1 + i * y2) / count;
            }
            //绘制所有卡尺矩形

            window.SetDraw("margin");
            window.SetColor("magenta");
            window.SetDraw("fill");
            for (int i = 0; i < NumHandles; i++)
            {
                HXLDCont RectangleXld = new HXLDCont();
                RectangleXld.GenRectangle2ContourXld(CalipersCenter[i].Y, CalipersCenter[i].X, rad, ProjectionLength / 2, SearchLength / 2);
                window.DispXld(RectangleXld);
            }
            //绘制线
            HXLDCont LineXld = new HXLDCont();
            HTuple Linecol = new HTuple(col1, col2);
            HTuple Linerow = new HTuple(row1, row2);
            LineXld.GenContourPolygonXld(Linerow, Linecol);
            window.DispXld(LineXld);
            window.SetColor("blue");
                window.DispRectangle2(row1, col1, rad, 23, 23);
                window.DispRectangle2(row2, col2, rad, 23, 23);
            int MidPoint;
            if (NumHandles % 2 == 0)
            {
                MidPoint = NumHandles / 2;
            }
            else
            {
                MidPoint = (NumHandles - 1) / 2;
            }
            if (col2 < col1)
            {
                rad = rad + Math.PI;
            }
            double x0 = CalipersCenter[MidPoint].X - Math.Cos(rad) * (ProjectionLength / 2) - Math.Sin(rad) * (SearchLength / 2);
            double x01 = CalipersCenter[MidPoint].X + Math.Cos(rad) * (ProjectionLength / 2) - Math.Sin(rad) * (SearchLength / 2);
            double y0 = CalipersCenter[MidPoint].Y - Math.Cos(rad) * (SearchLength / 2) + Math.Sin(rad) * (ProjectionLength / 2);
            double y01 = CalipersCenter[MidPoint].Y - Math.Cos(rad) * (SearchLength / 2) - Math.Sin(rad) * (ProjectionLength / 2);
            HTuple col = new HTuple(CalipersCenter[MidPoint].X, x0, x01, CalipersCenter[MidPoint].X);
            HTuple row = new HTuple(CalipersCenter[MidPoint].Y, y0, y01, CalipersCenter[MidPoint].Y);
            window.DispPolygon(row, col);
        }

        /// <summary> 
        /// Returns the distance of the ROI handle being
        /// closest to the image point(x,y).
        /// </summary>
        public override double distToClosestHandle(double x, double y)
        {

            double max = 10000;
            double[] val = new double[NumHandles];
            for (int i = 0; i < NumHandles; i++)
            {
                if (i == 0)
                {
                    val[i] = HMisc.DistancePp(y, x, row1, col1); // upper left 
                }
                else if (i == NumHandles - 1)
                {
                    val[i] = HMisc.DistancePp(y, x, row2, col2); // upper right 
                }
                else
                {
                    val[i] = HMisc.DistancePp(y, x, CalipersCenter[i].Y, CalipersCenter[i].X); // upper left 
                }

            }

            for (int i = 0; i < NumHandles; i++)
            {
                if (val[i] < max)
                {
                    max = val[i];
                    activeHandleIdx = i;
                }
            }

            return val[activeHandleIdx];
        }

        /// <summary> 
        /// Paints the active handle of the ROI object into the supplied window. 
        /// </summary>
        public override void displayActive(HalconDotNet.HWindow window, int imageWidth, int imageHeight)
        {
            //window.SetLineWidth(2);
            window.SetColor("magenta");
            double rad = -Math.Atan((row2 - row1) / (col2 - col1));
            if (activeHandleIdx == 0)
            {
                window.SetDraw("fill");
                window.DispRectangle2(row1, col1, rad, 23, 23);
            }
            else if (activeHandleIdx == NumHandles - 1)
            {
                window.SetDraw("fill");
                window.DispRectangle2(row2, col2, rad, 23, 23);
            }
            //else
            //{
            //    window.SetDraw("margin");

            //    window.DispRectangle2(CalipersCenter[activeHandleIdx].Y, CalipersCenter[activeHandleIdx].X, rad, ProjectionLength / 2, SearchLength / 2);
            //}

        }

        /// <summary>Gets the HALCON region described by the ROI.</summary>
        public override HRegion getRegion()
        {
            HRegion region = new HRegion();
            region.GenRegionLine(row1, col1, row2, col2);
            return region;
        }

        public override double getDistanceFromStartPoint(double row, double col)
        {
            double distance = HMisc.DistancePp(row, col, row1, col1);
            return distance;
        }
        /// <summary>
        /// Gets the model information described by 
        /// the ROI.
        /// </summary> 
        public override HTuple getModelData()
        {
            return new HTuple(new double[] { row1, col1, row2, col2 });
        }

        /// <summary> 
        /// Recalculates the shape of the ROI. Translation is 
        /// performed at the active handle of the ROI object 
        /// for the image coordinate (x,y).
        /// </summary>
        public override void moveByHandle(double newX, double newY)
        {
            if (activeHandleIdx == 0)
            {
                row1 = newY;
                col1 = newX;
            }
            else if (activeHandleIdx == NumHandles - 1)
            {
                row2 = newY;
                col2 = newX;
            }

            updateArrowHandle();
        }


        /// <summary> Auxiliary method </summary>
        private void updateArrowHandle()
        {
            double length, dr, dc, halfHW;
            double rrow1, ccol1, rowP1, colP1, rowP2, colP2;

            double headLength = 35;
            double headWidth = 35;


            arrowHandleXLD.Dispose();
            arrowHandleXLD.GenEmptyObj();

            rrow1 = row1 + (row2 - row1) * 0.9;
            ccol1 = col1 + (col2 - col1) * 0.9;

            length = HMisc.DistancePp(rrow1, ccol1, row2, col2);
            if (length == 0)
                length = -1;

            dr = (row2 - rrow1) / length;
            dc = (col2 - ccol1) / length;

            halfHW = headWidth / 2.0;
            rowP1 = rrow1 + (length - headLength) * dr + halfHW * dc;
            rowP2 = rrow1 + (length - headLength) * dr - halfHW * dc;
            colP1 = ccol1 + (length - headLength) * dc - halfHW * dr;
            colP2 = ccol1 + (length - headLength) * dc + halfHW * dr;

            if (length == -1)
                HOperatorSet.GenContourPolygonXld(out arrowHandleXLD, rrow1, ccol1);
            else
                HOperatorSet.GenContourPolygonXld(out arrowHandleXLD, new HTuple(new double[] { rrow1, row2, rowP1, row2, rowP2, row2 }),
                                                    new HTuple(new double[] { ccol1, col2, colP1, col2, colP2, col2 }));
        }
    }
}
