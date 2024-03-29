using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VControls.Properties;

namespace VControls
{
    public delegate void DClicked();
    public partial class VButton : UserControl
    {
        public VButton()
        {
            InitializeComponent();
            btn_button.Text = TextStr;
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        public event DClicked Clicked;
        /// <summary>
        /// 文本
        /// </summary>
        private string _textStr = "Button";
        public string TextStr
        {
            get { return _textStr; }
            set
            {
                _textStr = value;
                btn_button.Text = value;
            }
        }
        private void btn_button_Click(object sender, EventArgs e)
        {
            if (Clicked != null)
                Clicked();
        }

    }
}
