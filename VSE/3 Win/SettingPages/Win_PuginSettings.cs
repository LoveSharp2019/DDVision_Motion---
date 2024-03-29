using System.Windows.Forms;
using VControls;

namespace VSE
{
    internal partial class Win_PuginSettings : Form
    {
        internal Win_PuginSettings()
        {
            InitializeComponent();
            this.BackColor = VUI.WinBackColor;
        }

        /// <summary>
        /// 窗体实例对象
        /// </summary>
        private static Win_PuginSettings _instance;
        public static Win_PuginSettings Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_PuginSettings();
                return _instance;
            }
        }

    }
}
