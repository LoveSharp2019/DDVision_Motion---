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
    public partial class ControlBox : UserControl
    {
        public ControlBox()
        {
            InitializeComponent();
        }
        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            { base.BackColor = value; 
                ButtonLock.BackColor = value;
                ButtonMin.BackColor = value;
                ButtonMax.BackColor = value;
                ButtonClose.BackColor = value;
                this.Invalidate();
            }

        }
        bool LockFlag = true;
        bool MaxFlag = false;
        public Color MouseHoverColor
        {
            get { return ButtonLock.FlatAppearance.MouseOverBackColor; }
            set
            {
                ButtonLock.FlatAppearance.MouseOverBackColor = value;
                ButtonMin.FlatAppearance.MouseOverBackColor = value;
                ButtonMax.FlatAppearance.MouseOverBackColor = value;
                ButtonClose.FlatAppearance.MouseOverBackColor = value;
            }
        }
        public bool LockEnable 
        {
            get { return ButtonLock.Visible; }
            set { ButtonLock.Visible = value; }
        }
        public bool MinEnable
        {
            get { return ButtonMin.Visible; }
            set { ButtonMin.Visible = value; }
        }
        public bool MaxEnable
        {
            get { return ButtonMax.Visible; }
            set { ButtonMax.Visible = value; }
        }
        //private void LayoutPos()
        //{
        //    if (ButtonMax.Visible)
        //    {
        //        ButtonMax.Location = new Point(64,0);
        //        if (ButtonMin.Visible)
        //        {
        //            ButtonMin.Location = new Point(34, 0);
        //            if (ButtonLock.Visible)
        //            {
        //                ButtonLock.Location = new Point(4, 0);
        //            }

        //        }
        //        else
        //        {
        //            if (ButtonLock.Visible)
        //            {
        //                ButtonLock.Location = new Point(34, 0);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (ButtonMin.Visible)
        //        {
        //            ButtonMin.Location = new Point(64, 0);
        //            if (ButtonLock.Visible)
        //            {
        //                ButtonLock.Location = new Point(34, 0);
        //            }

        //        }
        //        else
        //        {
        //            if (ButtonLock.Visible)
        //            {
        //                ButtonLock.Location = new Point(64, 0);
        //            }
        //        }
        //    }
        
        //}
        private void ButtonLock_Click(object sender, EventArgs e)
        {
            if (!LockFlag)
            {
                ButtonLock.BackgroundImage = imageList1.Images[0];
                   LockFlag = true;
            }
            else
            {
                ButtonLock.BackgroundImage = imageList1.Images[1];
                LockFlag = false;
            }
            if (ButtonLockClick!=null)
            {
                ButtonLockClick(sender,e);
            }
        }

        private void ButtonMax_Click(object sender, EventArgs e)
        {
            if (!MaxFlag)
            {
                ButtonMax.BackgroundImage = imageList1.Images[3];
                MaxFlag = true;
            }
            else
            {
                ButtonMax.BackgroundImage = imageList1.Images[4];
                MaxFlag = false;
            }
            if (ButtonMaxClick != null)
            {
                ButtonMaxClick(sender, e);
            }
        }
        public event EventHandler ButtonLockClick;
        public event EventHandler ButtonMaxClick;
        public event EventHandler ButtonMinClick;
        public event EventHandler ButtonCloseClick;

        private void ButtonMin_Click(object sender, EventArgs e)
        {
            if (ButtonMinClick != null)
            {
                ButtonMinClick(sender, e);
            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            if (ButtonCloseClick != null)
            {
                ButtonCloseClick(sender, e);
            }
        }
    }
}
