using System;
using System.Drawing;
using System.Windows.Forms;
using VControls.Properties;

namespace VControls
{
    public partial class UserFormBase : Form
    {
        public UserFormBase()
        {
            InitializeComponent();
            this.BackColor = VControls.VUI.WinBackColor;
            this.TopMost = false;

        }


        private void Frm_ToolBase_FormClosing(object sender, FormClosingEventArgs e)
        { 
          
           
        }


    }
}
