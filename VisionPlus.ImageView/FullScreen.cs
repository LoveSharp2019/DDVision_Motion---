using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lxc.VisionPlus.ImageView
{
    public partial class FullScreen : Form
    {
        public FullScreen()
        {
            InitializeComponent();
            this.TopMost = true;
        
       this.WindowState = FormWindowState.Maximized;
          
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x112)
            {
                switch ((int)m.WParam)
                {
                    //禁止双击标题栏关闭窗体
                    case 0xF063:
                    case 0xF093:
                        m.WParam = IntPtr.Zero;
                        break;
                    //禁止拖拽标题栏还原窗体
                    case 0xF012:
                    case 0xF010:
                        m.WParam = IntPtr.Zero;
                        break;
                    //禁止双击标题栏
                    case 0xf122:
                        m.WParam = IntPtr.Zero;
                        break;
                   
                }
            }
            base.WndProc(ref m);
        }
        private void FullScreen_Load(object sender, EventArgs e)
        {
          
        }
    }
}
