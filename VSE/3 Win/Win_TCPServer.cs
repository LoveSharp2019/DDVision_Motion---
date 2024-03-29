using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Windows.Forms;
using VControls;
using VSE.Core;

namespace VSE
{
    internal partial class Win_TCPServer : Form
    {
        public Win_TCPServer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 服务器对象
        /// </summary>
        private static TCPSever tcpSever;
        /// <summary>
        /// 窗体实例对象
        /// </summary>
        private static Win_TCPServer _instance;
        public static Win_TCPServer Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_TCPServer();
                return _instance;
            }
        }


        /// <summary>
        /// 加载参数到界面
        /// </summary>
        /// <param name="tcpClient_1"></param>
        internal void LoadPar(TCPSever tcpSever_1)
        {
            try
            {
                tcpSever = tcpSever_1;
                tbx_severName.Text = tcpSever.Name;
                tbx_severIP.Text = tcpSever.SeverIP;
                tbx_severPort.Text = tcpSever.SeverPort.ToString();
                ckb_autoConnectAfterStart.Checked = tcpSever.AutoListenAfterStart;
                ckb_autoDisconnectBeforeClose.Checked = tcpSever.AutoDisconnectBeforeClose;

                if (tcpSever.listened)
                    btn_listen.Text = "停止监听";
                else
                    btn_listen.Text = "开始监听";
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }


        private void tbx_severName_TextStrChanged(string textStr)
        {
            tcpSever.Name = tbx_severName.Text.Trim();
        }
        private void tbx_severIP_TextStrChanged(string textStr)
        {
            tcpSever.SeverIP = tbx_severIP.Text.Trim();
        }
        private void tbx_severPort_TextStrChanged(string textStr)
        {
            tcpSever.SeverPort = Convert.ToInt16(tbx_severPort.Text.Trim());
        }
        private void btn_listen_Clicked()
        {
            tcpSever.Listen();
        }
        private void ckb_autoConnectAfterStart_CheckChanged(bool Checked)
        {
            tcpSever.AutoListenAfterStart = Checked;
        }
        private void ckb_autoDisconnectBeforeClose_CheckChanged(bool Checked)
        {
            tcpSever.AutoDisconnectBeforeClose = Checked;
        }
        private void 断开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < TCPSever.L_STCPSever.Count; i++)
                {
                    if (TCPSever.L_STCPSever[i].severName == tcpSever.Name)
                    {
                        foreach (KeyValuePair<string, Socket> item in TCPSever.L_STCPSever[i].L_Client)
                        {
                            if (item.Key == lbx_connectedList.SelectedItem.ToString())
                            {
                                item.Value.Disconnect(false);
                                item.Value.Close();
                                TCPSever.L_STCPSever[i].L_Client.Remove(item.Key);

                                lbx_connectedList.Items.RemoveAt(lbx_connectedList.SelectedIndex);
                                //   cbx_connectedList .Items .remove      //移除，待完善
                                if (cbx_connectedList.Items.Count > 0)
                                    cbx_connectedList.SelectedIndex = 0;
                                break;
                            }
                        }
                    }
                }
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
        private void btn_send_Clicked()
        {
            try
            {
                if (tbx_sendMessage.Text.Trim() == string.Empty)
                {
                    Win_MessageBox.Instance.MessageBoxShow("\r\n不可发送空字符串，发送失败 ");
                    return;
                }
                tcpSever.Send(cbx_connectedList.Text, tbx_sendMessage.Text.Trim());
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private void Win_TCPServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void Win_TCPServer_Load(object sender, EventArgs e)
        {

        }
    }
}
