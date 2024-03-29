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
    public partial class DisplayTable : UserControl
    {
        public DisplayTable()
        {
            InitializeComponent();
        }
        int rowcount;
        Color lineColor = Color.Green;
        int rowHeight = 35;
        List<DisplayTableRow> items=new List<DisplayTableRow>();
        public int RowCount
        { 
            get { return rowcount; }
            set
            { 
                rowcount = value; 
                this.Controls.Clear();
                items.Clear();  
                for (int i = 0; i < rowcount; i++)
                {
                    DisplayTableRow item = new DisplayTableRow();
                    item.Dock = DockStyle.Top;
                    item.CellText = (i+1).ToString();
                    item.Size = new Size(item.Width, rowHeight);
                   
                    item.LineColor = lineColor;
                    this.Controls.Add(item);
                    item.BringToFront();
                    items.Add(item);
                }
            }
        }
        public List<DisplayTableRow> Items
        {
            get
            {
               return items;
            }
            
        }
        public Color LineColor
        {
            get { return lineColor; }
            set
            {
                lineColor = value;
                for (int i = 0; i < RowCount; i++)
                {
                   (( DisplayTableRow) Controls[i]).LineColor = lineColor;
                }
            }
        }
        public int RowHeight
        {
            get { return rowHeight; }
            set
            {
                rowHeight = value;
                for (int i = 0; i < RowCount; i++)
                {
                    Controls[i].Size = new Size(Controls[i].Width, rowHeight);
                }
            }
        }
        
    }
}
