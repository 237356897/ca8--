using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;

namespace System.Toolkit
{
    public class AsynTcpClient
    {

        Socket tcpClient;

        public AsynTcpClient(string ip, int port)
        {
            IP = ip;
            Port = port;
            tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpClient.SendTimeout = 3000;
            tcpClient.ReceiveTimeout = 6000;
        }

        public string IP { get; set; }

        public int Port { get; set; }

        /// <summary>
        /// 是否已连接到服务器
        /// </summary>
        public bool IsConnected { get { return tcpClient.Connected; } }

        /// <summary>
        /// 客户端接收到数据内容
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 客户端接收到数据标志
        /// </summary>
        public bool IsDataReceiveed { get; set; }

        #region 连接

        public void SynConnect()
        {
            try
            {
                tcpClient.Close();
                tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                tcpClient.Connect(IPAddress.Parse(IP), Port);
                AliveCheck();
            }
            catch (Exception ex)
            {
                LogHelper.Info(ex.ToString());
            }
        }

        public void AsynConnect()
        {
            try
            {
                tcpClient.Close();
                tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                tcpClient.BeginConnect(IPAddress.Parse(IP), Port, asyncResult =>
                {
                    try
                    {
                        tcpClient.EndConnect(asyncResult);
                        AliveCheck();
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Info(ex.ToString());
                    }
                }, null);

            }
            catch (Exception ex)
            {
                LogHelper.Info(ex.ToString());
            }
        }

        #endregion

        #region 发送消息

        public string SynSend(string message)
        {
            try
            {
                Result = "";
                byte[] data = Encoding.UTF8.GetBytes(message);
                LogHelper.Info("client-->-->server:" + message);
                tcpClient.Send(data, 0, data.Length, SocketFlags.None);

                tcpClient.Receive(data, SocketFlags.None);
                Result = Encoding.UTF8.GetString(data).Trim();
                LogHelper.Info("client<--<--server:" + Result);

            }
            catch (Exception ex)
            {
                Result = "Error";
                LogHelper.Info(ex.ToString());
            }

            return Result;
        }

        bool ifget = false;



        int checknum = 0;
        public void AsynSend(string message)
        {
           
            try
            {
                Result = "";
                IsDataReceiveed = false;

                byte[] senddata = Encoding.UTF8.GetBytes(message);
                LogHelper.Info("client-->-->server:" + message);
                tcpClient.Send(senddata, 0, senddata.Length, SocketFlags.None);
                ifget = false;
                byte[] receivedata = new byte[256];
                tcpClient.BeginReceive(receivedata, 0, receivedata.Length, SocketFlags.None, asyncResult =>
                {
                    try
                    {
                        int length = tcpClient.EndReceive(asyncResult);
                        Result = Encoding.UTF8.GetString(receivedata).Trim();
                        LogHelper.Info("client<--<--server:" + Result);
                        IsDataReceiveed = true;
                        ifget = true;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Info(ex.ToString());
                    }
                }, null);

                checknum = 0;
                while (checknum<80)
                {
                    checknum++;

                    Thread.Sleep(200);
                    if (ifget)
                        break;
                }
                if(checknum>=80)
                {
                  //  Marking.AlarmStopThread = false;

                    MessageBox.Show("机器人无信号");

                }




                


            }
            catch (Exception ex)
            {
                LogHelper.Info(ex.ToString());
            }

        }

        #endregion

        public void AliveCheck()
        {
            bool blockingState = tcpClient.Blocking;
            try
            {
                byte[] tmp = new byte[1];

                tcpClient.Blocking = false;
                tcpClient.Send(tmp, 0, SocketFlags.None);
            }
            catch (SocketException e)
            {
                // 10035 == WSAEWOULDBLOCK
                if (e.NativeErrorCode.Equals(10035))
                {
                    LogHelper.Info("Still Connected, but the Send would block");
                }
                else
                {
                    LogHelper.Info(string.Format("Disconnected: error code {0}!", e.NativeErrorCode));
                }
            }
            finally
            {
                tcpClient.Blocking = blockingState;
            }
        }
    }
}
