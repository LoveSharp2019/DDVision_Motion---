using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinFormsUI.Docking;
using LightController;

namespace VSE
{
    public partial class Win_LightController : Form
    {
        public Win_LightController()
        {
            InitializeComponent();
            ckb_ch1.CheckChanged  += ckb_ch1_stateChanged;
            ckb_ch2.CheckChanged += ckb_ch2_stateChanged;
            ckb_ch3.CheckChanged += ckb_ch3_stateChanged;
            ckb_ch4.CheckChanged += ckb_ch4_stateChanged;
            ckb_ch5.CheckChanged += ckb_ch5_stateChanged;
            ckb_ch6.CheckChanged += ckb_ch6_stateChanged;
            ckb_ch7.CheckChanged += ckb_ch7_stateChanged;
            ckb_ch8.CheckChanged += ckb_ch8_stateChanged;

            num_ch1.ValueChanged += num_ch1_valueChanged;
            num_ch2.ValueChanged += num_ch2_valueChanged;
            num_ch3.ValueChanged += num_ch3_valueChanged;
            num_ch4.ValueChanged += num_ch4_valueChanged;
            num_ch5.ValueChanged += num_ch5_valueChanged;
            num_ch6.ValueChanged += num_ch6_valueChanged;
            num_ch7.ValueChanged += num_ch7_valueChanged;
            num_ch8.ValueChanged += num_ch8_valueChanged;
            ckb_openAllChAfterStart.CheckChanged += ckb_openAllChAfterStart_stateChanged;
            ckb_closeAllChBeforeClose.CheckChanged += ckb_closeAllChBeforeClose_stateChanged;
            cbx_IP.SelectedIndexChanged += cbx_IP_SelectedIndexChanged;
        }


        /// <summary>
        /// 控制器对象
        /// </summary>
        private static LightController_Base lightController;
        /// <summary>
        /// 窗体实例对象
        /// </summary>
        private static Win_LightController _instance;
        public static Win_LightController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_LightController();
                return _instance;
            }
        }


        /// <summary>
        /// 加载参数到界面
        /// </summary>
        /// <param name="lightController_1"></param>
        public void LoadPar(LightController_Base lightController_1)
        {
            lightController = lightController_1;
            cbx_controllerName.Text = lightController.Name;
            cbx_IP.Text = lightController.IP;
            ckb_openAllChAfterStart.Checked = lightController.OpenAllChAfterStart;
            ckb_closeAllChBeforeClose.Checked = lightController.CloseAllChBeforeClose;
            tck_ch1.Value = lightController.Brightness[0];
            tck_ch2.Value = lightController.Brightness[1];
            tck_ch3.Value = lightController.Brightness[2];
            tck_ch4.Value = lightController.Brightness[3];
            tck_ch5.Value = lightController.Brightness[4];
            tck_ch6.Value = lightController.Brightness[5];
            tck_ch7.Value = lightController.Brightness[6];
            tck_ch8.Value = lightController.Brightness[7];
        }


        void cbx_IP_SelectedIndexChanged(object sender, EventArgs e)
        {
            lightController.IP = cbx_IP.Text.Trim();
        }
        private void btn_openController_Click(object sender, EventArgs e)
        {
            lightController.OpenController();
            if (lightController.InitSucceed)
            {
                Win_DeviceManager.Instance.lbl_tip.Text = "打开成功";
                Win_DeviceManager.Instance.lbl_tip.ForeColor = Color.Green;
                btn_openController.Enabled = false;
            }
            else
            {
                Win_DeviceManager.Instance.lbl_tip.Text = "打开失败";
                Win_DeviceManager.Instance.lbl_tip.ForeColor = Color.Red;
            }
        }
        private void btn_closeController_Click(object sender, EventArgs e)
        {
            lightController.CloseController();
        }

        void ckb_closeAllChBeforeClose_stateChanged(bool state)
        {
            lightController.OpenAllChAfterStart = state;
        }
        void ckb_openAllChAfterStart_stateChanged(bool state)
        {
            lightController.CloseAllChBeforeClose = state;
        }

        void ckb_ch1_stateChanged(bool state)
        {
            if (ckb_ch1.Checked)
                lightController.OpenChannel(1);
            else
                lightController.CloseChannel(1);
        }
        void ckb_ch2_stateChanged(bool state)
        {
            if (ckb_ch1.Checked)
                lightController.OpenChannel(2);
            else
                lightController.CloseChannel(2);
        }
        void ckb_ch3_stateChanged(bool state)
        {
            if (ckb_ch1.Checked)
                lightController.OpenChannel(3);
            else
                lightController.CloseChannel(3);
        }
        void ckb_ch4_stateChanged(bool state)
        {
            if (ckb_ch1.Checked)
                lightController.OpenChannel(4);
            else
                lightController.CloseChannel(4);
        }
        void ckb_ch5_stateChanged(bool state)
        {
            if (ckb_ch1.Checked)
                lightController.OpenChannel(5);
            else
                lightController.CloseChannel(5);
        }
        void ckb_ch6_stateChanged(bool state)
        {
            if (ckb_ch1.Checked)
                lightController.OpenChannel(6);
            else
                lightController.CloseChannel(6);
        }
        void ckb_ch7_stateChanged(bool state)
        {
            if (ckb_ch1.Checked)
                lightController.OpenChannel(7);
            else
                lightController.CloseChannel(7);
        }
        void ckb_ch8_stateChanged(bool state)
        {
            if (ckb_ch1.Checked)
                lightController.OpenChannel(8);
            else
                lightController.CloseChannel(8);
        }

        private void tck_ch1_ValueChanged(object sender, EventArgs e)
        {
            num_ch1.Value = tck_ch1.Value;
        }
        private void tck_ch2_ValueChanged(object sender, EventArgs e)
        {
            num_ch2.Value = tck_ch2.Value;
        }
        private void tck_ch3_ValueChanged(object sender, EventArgs e)
        {
            num_ch3.Value = tck_ch3.Value;
        }
        private void tck_ch4_ValueChanged(object sender, EventArgs e)
        {
            num_ch4.Value = tck_ch4.Value;
        }
        private void tck_ch5_ValueChanged(object sender, EventArgs e)
        {
            num_ch5.Value = tck_ch5.Value;
        }
        private void tck_ch6_ValueChanged(object sender, EventArgs e)
        {
            num_ch6.Value = tck_ch6.Value;
        }
        private void tck_ch7_ValueChanged(object sender, EventArgs e)
        {
            num_ch7.Value = tck_ch7.Value;
        }
        private void tck_ch8_ValueChanged(object sender, EventArgs e)
        {
            num_ch8.Value = tck_ch8.Value;
        }

        void num_ch1_valueChanged(double value)
        {
            int temp = (int)value;
            tck_ch1.Value = temp;
            lightController.SetValue(1, temp);
        }
        void num_ch2_valueChanged(double value)
        {
            int temp = (int)value;
            tck_ch2.Value = temp;
            lightController.SetValue(2, temp);
        }
        void num_ch3_valueChanged(double value)
        {
            int temp = (int)value;
            tck_ch3.Value = temp;
            lightController.SetValue(3, temp);
        }
        void num_ch4_valueChanged(double value)
        {
            int temp = (int)value;
            tck_ch4.Value = temp;
            lightController.SetValue(4, temp);
        }
        void num_ch5_valueChanged(double value)
        {
            int temp = (int)value;
            tck_ch5.Value = temp;
            lightController.SetValue(5, temp);
        }
        void num_ch6_valueChanged(double value)
        {
            int temp = (int)value;
            tck_ch6.Value = temp;
            lightController.SetValue(6, temp);
        }
        void num_ch7_valueChanged(double value)
        {
            int temp = (int)value;
            tck_ch7.Value = temp;
            lightController.SetValue(7, temp);
        }
        void num_ch8_valueChanged(double value)
        {
            int temp = (int)value;
            tck_ch8.Value = temp;
            lightController.SetValue(8, temp);
        }

    }
}
