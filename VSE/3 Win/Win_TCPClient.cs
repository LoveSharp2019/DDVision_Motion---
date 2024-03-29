using System;
using System.Windows.Forms;
using VControls;
using VSE.Core;

namespace VSE
{
    internal partial class Win_TCPClient : Form
    {
        internal Win_TCPClient()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 客户端对象
        /// </summary>
        private static TCPClient tcpClient;
        /// <summary>
        /// 窗体实例对象
        /// </summary>
        private static Win_TCPClient _instance;
        internal static Win_TCPClient Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_TCPClient();
                return _instance;
            }
        }


        /// <summary>
        /// 加载参数到界面
        /// </summary>
        /// <param name="tcpClient_1"></param>
        internal void LoadPar(TCPClient tcpClient_1)
        {
            try
            {
                tcpClient = tcpClient_1;
                tbx_clientName.Text = tcpClient.Name;
                tbx_severIP.Text = tcpClient.severIP;
                tbx_severPort.Text = tcpClient.severPort.ToString();
                ckb_autoConnectAfterStart.Checked = tcpClient.AutoConnectAfterStart;
                ckb_autoDisconnectBeforeClose.Checked = tcpClient.AutoDisconnectBeforeClose;

                if (tcpClient.FindSocketByName ().Connected )
                    btn_connect.Text = "断开";
                else
                    btn_connect.Text = "连接";
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }


        private void tbx_clientName_TextStrChanged(string textStr)
        {
            tcpClient.Name = tbx_clientName.Text.Trim();
        }
        private void tbx_severIP_TextStrChanged(string textStr)
        {
            tcpClient.severIP = tbx_severIP.Text.Trim();
        }
        private void tbx_port_TextStrChanged(string textStr)
        {
            tcpClient.severPort = Convert.ToInt32(tbx_severPort.Text.Trim());
        }
        private void btn_connect_Clicked()
        {
            btn_connect.Text = "连接中...";
            Application.DoEvents();
            tcpClient.Connect();

            if (tcpClient.FindSocketByName().Connected)
                btn_connect.Text = "断开";
            else
                btn_connect.Text = "连接";
        }
        private void btn_send_Clicked()
        {
            try
            {
                if (!tcpClient.FindSocketByName().Connected)
                {
                    Win_MessageBox.Instance.MessageBoxShow("\r\n未连接到服务端，发送失败");
                    return;
                }
                if (tbx_sendMessage.Text.Trim() == string.Empty)
                {
                    Win_MessageBox.Instance.MessageBoxShow("\r\n不可发送空字符串，发送失败 ");
                    return;
                }
                tcpClient.Send(tbx_sendMessage.Text.Trim());
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private void lnk_clearLog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tbx_log.Clear();
        }
        private void Win_TCPClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

    }
}

