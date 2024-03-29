using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxc.VisionPlus.ImageView
{
	public class HWndMessage
	{
		public HWndMessage(string message, int row, int colunm, int size, string color, ImgView.CoordSystem coord)
		{

			this.message = message;
			this.size = size;
			this.row = row;
			this.colunm = colunm;
			this.color = color;
			this.showSize = (double)size;
			this.coordSystem = coord;
		}

		public HWndMessage(string message, int row, int colunm)
		{
			this.size = 16;
			this.color = "green";
			this.showSize = 16.0;
			this.coordSystem = ImgView.CoordSystem.image;
			this.message = message;
			this.row = row;
			this.colunm = colunm;
		}

		public double changeDisplayFontSize(HWindow Window, double zoom, double sizeOld)
		{
			double num = (double)this.size * zoom;
			if (num != sizeOld)
			{
				ViewWindow.SetDisplayFont(Window, num, "true", "false");
			}
			this.showSize = num;
			return num;
		}

		public void DispMessage(HWindow Window, ImgView.CoordSystem coordSystem)
		{
			string[] array = this.message.Split(new char[]
			{
				'#'
			});
			for (int i = 0; i < array.Length; i++)
			{
				ViewWindow.DisMsg(Window, array[i], coordSystem, (double)this.row + (double)i * (this.showSize + 2.0), this.colunm, this.color, "false");
			}
		}

		public void DispMessage(HWindow Window, ImgView.CoordSystem coordSystem, double zoom)
		{
			string[] array = this.message.Split(new char[]
			{
				'#'
			});
			for (int i = 0; i < array.Length; i++)
			{
				ViewWindow.DisMsg(Window, array[i], coordSystem, (double)this.row + (double)i * (this.showSize + 2.0), this.colunm, this.color, "false");
			}
		}

		public string message;

		public int size;

		public int row;

		public int colunm;

		public string color;

		public double showSize;

		public ImgView.CoordSystem coordSystem;
	}
}
