using LXCSystem.Control.CommonCtrl;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VControls
{
    public partial class Layout : UserControl
    {
      public  enum CCDWindDispalyMode
        {
            单画布,
            双画布,
            三画布,
            四画布,
        }
        //int VDistance = 160;
        int HDistance = 160;
        int StartPosX = 4;
        int StartPosY = 4;
        public Layout()
        {
            InitializeComponent();
        }
        public int GetSelectIndex()
        {
            int index = 0;
            foreach (LayoutButton item in this.Controls)
            {
                if (item.Checked)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }
        public void SetSelectIndex(int index)
        {
            if (index< this.Controls.Count)
            {
                (this.Controls[index] as LayoutButton).Checked = true;
            }
            
        }
        public void SetMode(CCDWindDispalyMode CCDWindDispalyMode)
        {
            this.Controls.Clear();
            switch (CCDWindDispalyMode)
            {
                case CCDWindDispalyMode.单画布:
                    this.Controls.Add(new LayoutButton(11, new Point(StartPosX, StartPosY)));
                    break;
                case CCDWindDispalyMode.双画布:
                    for (int i = 0; i < 2; i++)
                    {
                        this.Controls.Add(new LayoutButton(21 + i, new Point(StartPosX + i * 190, StartPosY)));
                    }
                    break;
                case CCDWindDispalyMode.三画布:
                    

                        for (int j = 0; j < 4; j++)
                        {
                            if( (j)<5)
                            {
                                this.Controls.Add(new LayoutButton(31 + j, new Point(StartPosX + j * HDistance, StartPosY)));
                            }
                            else
                            {
                                return;
                            }
                        }

                    break;
                case CCDWindDispalyMode.四画布:
                    for (int j = 0; j < 4; j++)
                    {
                        if ((j) < 5)
                        {
                            this.Controls.Add(new LayoutButton(41 + j, new Point(StartPosX + j * HDistance, StartPosY)));
                        }
                        else
                        {
                            return;
                        }
                    }
                    //for (int i = 0; i < 2; i++)
                    //{
                    //    for (int j = 0; j < 5; j++)
                    //    {
                    //        if ((i * 5+ j) < 7)
                    //        {
                    //            this.Controls.Add(new LayoutButton(41 + (i * 5 + j), new Point(StartPosX + j * HDistance, StartPosY + i * VDistance)));
                    //        }
                    //        else
                    //        {
                    //            return;
                    //        }
                    //    }
                    //}
                    break;
                default:
                    break;
            }
        }
        internal class LayoutButton: LxcRadioButton
        {
            public LayoutButton(int picIndex,Point Loc)
            {
                this.Checked = false;
                this.BackgroundImage = GetPic(picIndex);// 
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                this.CheckBoxBackColor = System.Drawing.Color.White;
                this.BorderColor = System.Drawing.Color.Black;
                this.CheckColor = System.Drawing.Color.Black;
                this.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                this.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
                this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                this.Location = Loc;
                this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
                this.OuterCircleRadius = 8;
                this.MinimumSize = new System.Drawing.Size(22, 22);
                this.InnerCircleRadius = 4;
                this.Name = "lxcRadioButton1";
                this.Size = new System.Drawing.Size(144, 100);
                this.TabIndex = 4;
                this.TabStop = true;
                this.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
                this.UseVisualStyleBackColor = true;
            }
            private Image GetPic(int index)
            {
                Image img = global::VControls.Properties.Resources.Layout11;
                switch (index)
                {
                    case 11:
                       img = global::VControls.Properties.Resources.Layout11;
                        this.Checked = true;
                        break;
                    case 21:
                        img = global::VControls.Properties.Resources.Layout21;
                        this.Checked = true;
                        break;
                    case 22:
                        img = global::VControls.Properties.Resources.Layout22;
                        break;
                    case 31:
                        img = global::VControls.Properties.Resources.Layout31;
                        this.Checked = true;
                        break;
                    case 32:
                        img = global::VControls.Properties.Resources.Layout32;
                        break;
                    case 33:
                        img = global::VControls.Properties.Resources.Layout33;
                        break;
                    case 34:
                        img = global::VControls.Properties.Resources.Layout34;
                        break;
                  
                    case 41:
                        img = global::VControls.Properties.Resources.Layout41;
                        this.Checked = true;
                        break;
                    case 42:
                        img = global::VControls.Properties.Resources.Layout42;
                        break;
                    case 43:
                        img = global::VControls.Properties.Resources.Layout43;
                        break;
                    case 44:
                        img = global::VControls.Properties.Resources.Layout44;
                        break;
                  
                 
                }
               
                return img;
            }
        }
    }
}
