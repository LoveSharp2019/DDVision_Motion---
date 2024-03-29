using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VControls
{
    public partial class VROISel : UserControl
    {
        public VROISel()
        {
            InitializeComponent();

            CB.Items.AddRange(rn);
            CB.SelectedIndex = 0;


        }
        public delegate void LxcROIChangedEventHandler(object sender, LxcROIChangedEventArgs e);
        public class LxcROIChangedEventArgs : EventArgs
        {
            private string mROI;

            public string ROI
            {
                get
                {
                    return this.mROI;
                }
            }

            public LxcROIChangedEventArgs(string roi)
            {
                mROI = roi;
            }

        }
        public event LxcROIChangedEventHandler ROIChangedEvent;
        



        public string SelectedValue
        {
            get { return CB.Text; }
        }
        public int SelectedIndex
        {
            get { return CB.SelectedIndex; }
            set { CB.SelectedIndex = value; }
        }
        string[] rn = new string[5] { "整幅图像", "圆形", "椭圆", "矩形", "带角度矩形" };

        private void CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ROIChangedEvent != null)
            {
                ROIChangedEvent(this, new LxcROIChangedEventArgs(SelectedValue));
            }
        }
    }
}
