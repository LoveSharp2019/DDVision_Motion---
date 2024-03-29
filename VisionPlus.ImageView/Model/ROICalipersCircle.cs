using HalconDotNet;
using Lxc.VisionPlus.Core.LxcType;
using System;
using System.Xml.Serialization;

namespace Lxc.VisionPlus.ImageView.Model
{
    [Serializable]
    public class ROICalipersCircle : ROI
    {
        private double radius = 180;
        private double centerX = 200;
        private double centerY = 200;
        private double startRad = 0;
        private double rangeRad = Math.PI*2;
        private double searchLength = 30;
        private double projectionLength = 10;
        PointD[] CalipersCenter;
        public int CaliperCount
        {
            get { return this.NumHandles; }
            set {
                if (value<3)
                {
                    return;
                }
                this.NumHandles = value;
            }
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
        [XmlElement(ElementName = "Radius")]
        public double Radius
        {
            get { return this.radius; }
            set { this.radius = value; }
        }
        [XmlElement(ElementName = "CenterX")]
        public double CenterX
        {
            get { return this.centerX; }
            set { this.centerX = value; }
        }
        [XmlElement(ElementName = "CenterY")]
        public double CenterY
        {
            get { return this.centerY; }
            set { this.centerY = value; }
        }
        [XmlElement(ElementName = "StartRad")]
        public double StartRad
        {
            get { return this.startRad; }
            set { this.startRad = value; }
        }
        [XmlElement(ElementName = "RangeRad")]
        public double RangeRad
        {
            get { return this.rangeRad; }
            set { this.rangeRad = value; }
        }
        public ROICalipersCircle(double centerX, double centerY)
        {
            NumHandles = 3;
            activeHandleIdx = 0;
            CenterX = centerX;
            CenterY = centerY;
            Type = ROIType.ROICalipersCircle;
        }
        public ROICalipersCircle(ROICalipersCircle clone)
        {
            this.Radius = clone.Radius;
            this.CenterX = clone.CenterX;
            this.CenterY = clone.CenterY;
            this.StartRad = clone.StartRad;
            this.CaliperCount = clone.CaliperCount;
            this.SearchLength = clone.SearchLength;
            this.ProjectionLength = clone.ProjectionLength;
            this.CalipersCenter = clone.CalipersCenter;
            this.RangeRad = clone.RangeRad;
            ;
        }
        /// <summary>Creates a new ROI instance at the mouse position</summary>
        public override void createROI(double midX, double midY)
        {
            centerY = midY;
            centerX = midX;

            radius = 200;
            startRad = 0;
            rangeRad = Math.PI * 2;
        }

        /// <summary>Paints the ROI into the supplied window</summary>
        /// <param name="window">HALCON window</param>
        public override void draw(HalconDotNet.HWindow window, int imageWidth, int imageHeight)
        {
          
            window.SetDraw("margin");
            window.SetColor("magenta");
            window.SetLineWidth(1);
            //计算起始点
            HXLDCont circlrXld = new HXLDCont();
            circlrXld.GenCircleContourXld(centerY, centerX, radius, startRad, startRad+ rangeRad, "positive", 1);
            window.DispXld(circlrXld);
            //通过起始点和结束点坐标计算其他所有卡尺点中点坐标
            CalipersCenter = new Core.LxcType.PointD[NumHandles];
            int count = NumHandles;
          
            for (int i = 0; i < count; i++)
            {
              CalipersCenter[i].X = centerX + Math.Cos(startRad + i * rangeRad / NumHandles + 0.5 * rangeRad / NumHandles) * radius;
              CalipersCenter[i].Y = centerY - Math.Sin(startRad + i * rangeRad / NumHandles + 0.5 * rangeRad / NumHandles) * radius;
            }
            //画卡尺
            window.SetDraw("margin");
            window.SetColor("magenta");
            for (int i = 0; i < NumHandles; i++)
            {
                HXLDCont RectangleXld = new HXLDCont();
                RectangleXld.GenRectangle2ContourXld(CalipersCenter[i].Y, CalipersCenter[i].X, -Math.PI / 2 + (startRad + rangeRad * i / (NumHandles)+0.5 * rangeRad / NumHandles), ProjectionLength / 2, SearchLength / 2);
                window.DispXld(RectangleXld);
            }
            window.SetColor("blue");
            window.SetDraw("fill");
            window.DispRectangle1(centerY - 12, centerX - 12, centerY + 12, centerX + 12);//圆心
            //画 其他小矩形
            PointD[] PCenter = new PointD[3];
            for (int i = 0; i < 3; i++)
            {
                PCenter[i].X = centerX + Math.Cos(startRad + i * rangeRad / 2) * radius;
                PCenter[i].Y = centerY - Math.Sin(startRad + i * rangeRad / 2) * radius;
                window.DispCircle(PCenter[i].Y, PCenter[i].X, 12);//圆心
            }
            PCenter[1].X = centerX + Math.Cos(startRad + 1 * rangeRad / 2) * (radius+40);
            PCenter[1].Y = centerY - Math.Sin(startRad + 1 * rangeRad / 2) * (radius + 40);
            HXLDCont arrowxld = GenArrowContourXld(centerY, centerX, PCenter[1].Y, PCenter[1].X, 20, 20);
            window.DispXld(arrowxld);
            int MidPoint;
            if (NumHandles % 2 == 0)
            {
                MidPoint = NumHandles / 2;
            }
            else
            {
                MidPoint = (NumHandles - 1) / 2;
            }
            double rad = Math.PI / 2 - Math.Atan((CalipersCenter[MidPoint].Y - centerY) / (CalipersCenter[MidPoint].X - centerX));
            if (centerX > CalipersCenter[MidPoint].X)
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
        /// closest to the image point(x,y)
        /// </summary>
        public override double distToClosestHandle(double x, double y)
        {
            double max = 100000;
            double[] val = new double[4];
            PointD[] CalipersCenter= new PointD[3];
            for (int i = 0; i < 3; i++)
            {
                CalipersCenter[i].X = centerX + Math.Cos(startRad + i * rangeRad / 2) * radius;
                CalipersCenter[i].Y = centerY - Math.Sin(startRad + i * rangeRad / 2) * radius;
            }
            
            val[0] = HMisc.DistancePp(y, x, centerY, centerX);
            val[1] = HMisc.DistancePp(y, x, CalipersCenter[0].Y, CalipersCenter[0].X);
            val[2] = HMisc.DistancePp(y, x, CalipersCenter[1].Y, CalipersCenter[1].X);
            val[3] = HMisc.DistancePp(y, x, CalipersCenter[2].Y, CalipersCenter[2].X);

            for (int i = 0; i < 4; i++)
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
        /// Paints the active handle of the ROI object into the supplied window 
        /// </summary>
        public override void displayActive(HalconDotNet.HWindow window, int imageWidth, int imageHeight)
        {

            window.SetColor("red");
            window.SetDraw("fill");
            if (activeHandleIdx == 0)
            {
               
                window.DispRectangle1(centerY - 14, centerX - 14, centerY + 14, centerX + 14);//起点

            }
            else
            {
                PointD[] CalipersCenter = new PointD[3];
                for (int i = 0; i < 3; i++)
                {
                    CalipersCenter[i].X = centerX + Math.Cos(startRad + i * rangeRad / 2) * radius;
                    CalipersCenter[i].Y = centerY - Math.Sin(startRad + i * rangeRad / 2) * radius;
                }
                window.DispCircle(CalipersCenter[activeHandleIdx - 1].Y, CalipersCenter[activeHandleIdx - 1].X, 12);//圆心
                //window.DispRectangle2(CalipersCenter[activeHandleIdx-1].Y, CalipersCenter[activeHandleIdx-1].X, -Math.PI / 2 + (startRad + rangeRad * (activeHandleIdx-1) / 2), 20, 20);
            }
        }

        /// <summary>Gets the HALCON region described by the ROI</summary>
        public override HRegion getRegion()
        {
            HRegion region = new HRegion();
            region.GenCircle(Row, Column, radius);
            return region;
        }

        public override double getDistanceFromStartPoint(double row, double col)
        {
            double sRow = Row; // assumption: we have an angle starting at 0.0
            double sCol = Column + 1 * radius;

            double angle = HMisc.AngleLl(Row, Column, sRow, sCol, Row, Column, row, col);

            if (angle < 0)
                angle += 2 * Math.PI;

            return (radius * angle);
        }

        /// <summary>
        /// Gets the model information described by 
        /// the  ROI
        /// </summary> 
        public override HTuple getModelData()
        {
            return new HTuple(new double[] { Row, Column, radius });
        }

        /// <summary> 
        /// Recalculates the shape of the ROI. Translation is 
        /// performed at the active handle of the ROI object 
        /// for the image coordinate (x,y)
        /// </summary>
        public override void moveByHandle(double newX, double newY)
        {
            switch (activeHandleIdx)
            {
                case 0: // 圆心
                    centerY = newY;
                    centerX = newX;
                    break;
                case 1: // 起始点

                    double STTemp = startRad;
                    
                    if (Math.Atan2((newY - centerY), (newX - centerX)) > -Math.PI && Math.Atan2((newY - centerY), (newX - centerX)) < 0)
                    {
                        startRad = -Math.Atan2((newY - centerY), (newX - centerX));

                    }
                    else
                    {
                        startRad = -Math.Atan2((newY - centerY), (newX - centerX)) + 2 * Math.PI;

                        //rangeRad = Math.Atan2((newX - centerX), (newY - centerY)) - startRad + Math.PI * 3 / 2;
                    }
                    if (startRad > Math.PI * 2)
                    {
                        startRad -= Math.PI * 2;
                        rangeRad -= startRad - STTemp+Math.PI*2;
                    }
                    else
                    {
                        rangeRad -= startRad - STTemp;
                    }
                    
                    System.Diagnostics.Debug.WriteLine(startRad * 180 / Math.PI + "===="+(rangeRad) * 180 / Math.PI);
                    if (rangeRad <0)
                    {
                        rangeRad += Math.PI*2;
                    }
                    if (rangeRad > Math.PI * 2)
                    {
                        rangeRad -= Math.PI * 2;
                    }


                    break;
                case 2: // 中点
                    radius=Math.Sqrt((centerX-newX) * (centerX - newX) + (centerY - newY) * (centerY - newY));

                    break;
                case 3: // 结束点
                    if (Math.Atan2((newY - centerY), (newX - centerX))> -Math.PI&& Math.Atan2((newY - centerY), (newX - centerX)) < 0)
                    {
                        rangeRad = -Math.Atan2((newY - centerY), (newX - centerX)) - startRad;// + Math.PI * 3/ 2;
                    }
                    else
                    {
                        rangeRad = -Math.Atan2((newY - centerY), (newX - centerX)) - startRad+ Math.PI * 2;
                    }
                    if (rangeRad<0)
                    {
                        rangeRad +=2 * Math.PI;
                    }
                    System.Diagnostics.Debug.WriteLine(rangeRad * 180 / Math.PI);

                    ;
                    break;
                
            }

            
            
        }
    }
}
