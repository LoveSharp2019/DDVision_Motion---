using System;
using HalconDotNet;
using System.Xml.Serialization;

namespace Lxc.VisionPlus.ImageView.Model
{
	/// <summary>
	/// This class demonstrates one of the possible implementations for a 
	/// circular ROI. ROICircle inherits from the base class ROI and 
	/// implements (besides other auxiliary methods) all virtual methods 
	/// defined in ROI.cs.
	/// </summary>
    [Serializable]
	public class ROICircle : ROI
	{

        //[XmlElement(ElementName = "Row")]
        //public double Row
        //{
        //    get { return this.Row; }
        //    set { this.Row = value; }
        //}

        //[XmlElement(ElementName = "Column")]
        //public double Column
        //{
        //    get { return this.Column; }
        //    set { this.Column = value; }
        //}
        [XmlElement(ElementName = "Radius")]
        public double Radius
        {
            get { return this.radius; }
            set { this.radius = value; }
        }


		private double radius;
		private double row1, col1;  // first handle


		public ROICircle()
		{
			NumHandles = 2; // one at corner of circle + midpoint
			activeHandleIdx = 1;
			Type = ROIType.ROICircle;
		}

        public ROICircle(double row, double col, double radius)
        {
			NumHandles = 2; // one at corner of circle + midpoint
			activeHandleIdx = 1;
			createCircle(row, col, radius);
			Type = ROIType.ROICircle;
		}
        public void UpdateROI(double x, double y, double radius)
        {
            Column = x;
            Row = y;
            this.radius = radius;
            row1 = Row;
            col1 = Column + radius;

        }
        public override void createCircle(double row, double col, double radius)
        {
            base.createCircle(row, col, radius);
            Row = row;
            Column = col;

            this.radius = radius;

            row1 = Row;
            col1 = Column + radius;
        }

		/// <summary>Creates a new ROI instance at the mouse position</summary>
		public override void createROI(double midX, double midY)
		{
			Row = midY;
			Column = midX;

			radius = 100;

			row1 = Row;
			col1 = Column + radius;
		}

		/// <summary>Paints the ROI into the supplied window</summary>
		/// <param name="window">HALCON window</param>
        public override void draw(HalconDotNet.HWindow window, int imageWidth, int imageHeight)
		{
			//window.DispCircle(Row, Column, radius);
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
			HXLDCont circlexld=new HXLDCont();
			
			circlexld.GenCircleContourXld(Row, Column, radius, 0, 6.28318, "positive", 0.1);
			window.DispXld(circlexld);
			window.SetDraw("fill");
			window.SetColor("blue");
			
			window.DispLine(Row, Column, row1, col1);
			window.DispRectangle2(row1, col1, 0, littleRecSize+5, littleRecSize+5);
			window.DispRectangle2(Row, Column, 0, littleRecSize+5, littleRecSize+5);

		}

		/// <summary> 
		/// Returns the distance of the ROI handle being
		/// closest to the image point(x,y)
		/// </summary>
		public override double distToClosestHandle(double x, double y)
		{
			double max = 10000;
			double [] val = new double[NumHandles];

			val[0] = HMisc.DistancePp(y, x, row1, col1); // border handle 
			val[1] = HMisc.DistancePp(y, x, Row, Column); // midpoint 

			for (int i=0; i < NumHandles; i++)
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


			window.SetLineWidth(2);
			window.SetColor("magenta");
			window.SetDraw("fill");
			switch (activeHandleIdx)
			{
				case 0:
					window.DispRectangle2(row1, col1, 0, littleRecSize+5, littleRecSize + 5);
					window.DispLine(Row, Column, row1, col1);
					break;
				case 1:
					
					window.DispRectangle2(Row, Column, 0, littleRecSize + 5, littleRecSize + 5);

					break;
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
			HTuple distance;
			double shiftX,shiftY;

			switch (activeHandleIdx)
			{
				case 0: // handle at circle border

					row1 = newY;
					col1 = newX;
					HOperatorSet.DistancePp(new HTuple(row1), new HTuple(col1),
											new HTuple(Row), new HTuple(Column),
											out distance);

					radius = distance[0].D;
					break;
				case 1: // midpoint 

					shiftY = Row - newY;
					shiftX = Column - newX;

					Row = newY;
					Column = newX;

					row1 -= shiftY;
					col1 -= shiftX;
					break;
			}
		}
	}//end of class
}//end of namespace
