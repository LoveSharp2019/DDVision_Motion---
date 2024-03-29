
using System.Windows.Forms;

namespace VControls
{
    public partial class ToolWinVP : FormBase
    {
        public ToolWinVP()
        {
            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            Pbtn_runTool.FlatAppearance.BorderColor = VUI.ButtonBorderColor;
            Pbtn_close.FlatAppearance.BorderColor = VUI.ButtonBorderColor;
            this.LockBox = true;
            this.TopMost = true;
        }
        /// <summary>
        /// 当前工具所属的流程
        /// </summary>
        public string jobName = string.Empty;
        /// <summary>
        /// 当前工具名
        /// </summary>
        public string toolName = string.Empty;
        public virtual void EnableTool(bool IsEnable)
        { }
        private void switchButton1_Click(object sender, System.EventArgs e)
        {
            EnableTool(xxxxxxxxxxxxxxxxxxxswitchButton1.Checked);
        }

        private void ToolWinBase_Load(object sender, System.EventArgs e)
        {
        }

        private void Plbl_runTime_Click(object sender, System.EventArgs e)
        {

        }
     
    }
}
