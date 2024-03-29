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
    public partial class VCaliperSet : UserControl
    {
        public VCaliperSet()
        {
            InitializeComponent();
        }
        public event DValueChanged CaliperCountChange;
        public event DValueChanged CaliperLengthChange;
        public event DValueChanged CaliperWidthChange;
        public event DValueChanged CaliperThresholdChange;
        /// <summary>
        /// 卡尺数量
        /// </summary>
        public int CaliperCount  
        {
            get { return (int)vNumericUpDown1.Value; }
            set {

                this.vNumericUpDown1.ValueChanged -= this.vNumericUpDown1_ValueChanged;
                vNumericUpDown1.Value = value;
                this.vNumericUpDown1.ValueChanged += this.vNumericUpDown1_ValueChanged;
            }
        }
        /// <summary>
        /// 卡尺长度
        /// </summary>
        public int CaliperLength
        {
            get { return (int)vNumericUpDown1.Value; }
            set
            {

                this.vNumericUpDown2.ValueChanged -= this.vNumericUpDown2_ValueChanged;
                vNumericUpDown2.Value = value;
                this.vNumericUpDown2.ValueChanged += this.vNumericUpDown2_ValueChanged;
            }
        }
        /// <summary>
        /// 卡尺宽度
        /// </summary>
        public int CaliperWidth
        {
            get { return (int)vNumericUpDown3.Value; }
            set
            {

                this.vNumericUpDown3.ValueChanged -= this.vNumericUpDown3_ValueChanged;
                vNumericUpDown3.Value = value;
                this.vNumericUpDown3.ValueChanged += this.vNumericUpDown3_ValueChanged;
            }
        }
        /// <summary>
        /// 边缘阈值
        /// </summary>
        public int CaliperThreshold
        {
            get { return (int)vNumericUpDown4.Value; }
            set
            {

                this.vNumericUpDown4.ValueChanged -= this.vNumericUpDown4_ValueChanged;
                vNumericUpDown4.Value = value;
                this.vNumericUpDown4.ValueChanged += this.vNumericUpDown4_ValueChanged;
            }
        }
        private void vNumericUpDown1_ValueChanged(double value)
        {
            if (CaliperCountChange!=null)
            {
                CaliperCountChange(value);
            }
        }
        private void vNumericUpDown2_ValueChanged(double value)
        {
            if (CaliperLengthChange != null)
            {
                CaliperLengthChange(value);
            }
        }
        private void vNumericUpDown3_ValueChanged(double value)
        {
            if (CaliperWidthChange != null)
            {
                CaliperWidthChange(value);
            }
        }
        private void vNumericUpDown4_ValueChanged(double value)
        {
            if (CaliperThresholdChange != null)
            {
                CaliperThresholdChange(value);
            }

        }

     
    }
}
