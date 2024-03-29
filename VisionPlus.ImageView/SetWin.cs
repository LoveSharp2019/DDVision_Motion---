using HalconDotNet;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lxc.VisionPlus.ImageView
{
    public partial class SetWin : UserControl
    {
       public bool[] check;
        public Color[] c;
        public int[] V;
        public SetWin(bool[] check,Color[] c,int[] V)
        {
            this.InitializeComponent();
            this.check = check;
            this.c = c;
            checkBox2.Checked = check[0];
            checkBox1.Checked = check[1];
            checkBox6.Checked = check[2];
            checkBox3.Checked = check[3];
            checkBox4.Checked = check[4];
            checkBox5.Checked = check[5];
            radioButton7.Checked = check[6];
            radioButton9.Checked = check[7];
            checkBox7.Checked = check[8];
            panel4.Visible = !check[8];
            numericUpDown1.Value = V[0];
            numericUpDown2.Value = V[1];
            numericUpDown3.Value = V[2];
            if (c[0] ==Color.Green)
            {
                radioButton1.Checked = true;
            }
            else if (c[0] == Color.Orange)
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton3.Checked = true;
            }
            if (c[1] == Color.Green)
            {
                radioButton4.Checked = true;
            }
            else if (c[1] == Color.Orange)
            {
                radioButton6.Checked = true;
            }
            else
            {
                radioButton5.Checked = true;
            }
            this.V = V;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            this.numericUpDown3.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
          check[0]= checkBox2.Checked ;
          check[1]= checkBox1.Checked ;
          check[2]= checkBox6.Checked ;
          check[3]= checkBox3.Checked ;
          check[4]= checkBox4.Checked ;
          check[5] = checkBox5.Checked ;
         check[6] = radioButton7.Checked;
         check[7] = radioButton9.Checked;
         check[8] = checkBox7.Checked;
            panel4.Visible = !check[8];
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {


            if (radioButton1.Checked)
            {
                c[0] = Color.Green;
            }
           
            else if (radioButton2.Checked)
            {
                c[0] = Color.Orange;
            }
            else
            {
                c[0] = Color.Blue;
            }

            if (radioButton4.Checked)
            {
                c[1] = Color.Green;
            }

            else if (radioButton6.Checked)
            {
                c[1] = Color.Orange;
            }
            else
            {
                c[1] = Color.Blue;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            V[0]=(int)numericUpDown1.Value;
            V[1] = (int)numericUpDown2.Value;
            V[2] = (int)numericUpDown3.Value;
        }
    }
}
