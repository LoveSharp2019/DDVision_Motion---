using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VControls;

namespace VSE
{
    internal partial class Win_AdvancedSetting : Form
    {
        internal Win_AdvancedSetting()
        {
            InitializeComponent();
            this.BackColor = VUI.WinBackColor;
        }

        /// <summary>
        /// 窗体实例对象
        /// </summary>
        private static Win_AdvancedSetting _instance;
        public static Win_AdvancedSetting Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_AdvancedSetting();
                return _instance;
            }
        }


       

    }
}
