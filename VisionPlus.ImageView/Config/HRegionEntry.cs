using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewROI.Config
{
	/// <summary>
	/// 显示xld和region 带有颜色
	/// </summary>
	public class HRegionEntry
	{
		public HRegionEntry(HObject _hbj)
		{
			this.color = "green";
			this.drawMode = "margin";
			this.lineWidth = 1;
			this.type = "";

			this.hobject_0 = _hbj;
		}

		public HRegionEntry(HObject _hbj, string _color, string _drawMode, string _type = "")
		{
			this.color = "green";
			this.drawMode = "margin";
			this.lineWidth = 1;
			this.type = "";
			this.hobject_0 = _hbj;
			this.color = _color;
			this.drawMode = _drawMode;
			this.type = _type;
		}

		public HRegionEntry(HObject _hbj, string _color, string _drawMode, int _lineWidth, string _type = "")
		{
			this.color = "green";
			this.drawMode = "margin";
			this.lineWidth = 1;
			this.type = "";
			this.hobject_0 = _hbj;
			this.color = _color;
			this.drawMode = _drawMode;
			this.lineWidth = _lineWidth;
			this.type = _type;
		}

		public void clear()
		{
			if (this.hobject_0 != null && this.hobject_0.IsInitialized())
			{
				this.hobject_0.Dispose();
			}
		}

		public HObject HObject
		{
			get
			{
				return this.hobject_0;
			}
			set
			{
				this.hobject_0 = value;
			}
		}

		public string Color
		{
			get
			{
				return this.color;
			}
			set
			{
				this.color = value;
			}
		}

		public string DrawMode
		{
			get
			{
				return this.drawMode;
			}
			set
			{
				this.drawMode = value;
			}
		}

		public int LineWidth
		{
			get
			{
				return this.lineWidth;
			}
			set
			{
				this.lineWidth = value;
			}
		}

		public string Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		private HObject hobject_0;

		private string color;

		private string drawMode;

		private int lineWidth;

		private string type;
	}
}
