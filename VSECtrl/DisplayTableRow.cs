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
    public partial class DisplayTableRow : UserControl
    {
        public DisplayTableRow()
        {
            InitializeComponent();
        }
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                this.Invalidate();//使控件的特定区域无效并向控件发送绘制消息。还可以使分配给该控件的子控件无效。
            }
        }
        public  string CellText
        { 
            get
            {
                return RowText.Text;
            }
            set
            {
                this.RowText.Text = value;
               // this.Invalidate();
            }
        }
        public Color LineColor
        {
            get { return lineBottom.BackColor; }
            set { lineBottom.BackColor = value; }
        }
    }
}
