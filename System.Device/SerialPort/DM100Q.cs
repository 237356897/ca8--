﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace System.Device
{
    /// <summary>
    /// 得利捷读码器通信类
    /// </summary>
    public class DM100Q : SerialPort, ISerialPortTriggerModel
    {
        #region 字段

        string receiveString = "";

        #endregion

        #region 构造函数

        public DM100Q() : base() { }
        /// <summary>
        /// 使用指定的端口名称、波特率、奇偶校验位、数据位和停止位初始化 System.IO.Ports.SerialPort 类的新实例。
        /// </summary>
        /// <param name="portName">要使用的端口（例如 COM1）。</param>
        /// <param name="baudRate">波特率。</param>
        /// <param name="parity"> System.IO.Ports.SerialPort.Parity 值之一。</param>
        /// <param name="dataBits">数据位值。</param>
        /// <param name="stopBits">System.IO.Ports.SerialPort.StopBits 值之一。</param>
        public DM100Q(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits) : base(portName, baudRate, parity, dataBits, stopBits) { }

        #endregion

        #region 接口

        public event DataReceiveCompleteEventHandler DeviceDataReceiveCompelete;

        public string Name { get; set; }

        public string ConnectionParam { get; set; }

        public bool receiveFinish { get; set; }

        public void SetConnectionParam(string str)
        {
            ConnectionParam = str;
            string[] param = ConnectionParam.Split(',');
            PortName = param[0];
            BaudRate = int.Parse(param[1]);
            Parity = (Parity)Enum.Parse(typeof(Parity), param[2]);
            DataBits = int.Parse(param[3]);
            StopBits = (StopBits)Enum.Parse(typeof(StopBits), param[4]);
            ReadTimeout = int.Parse(param[5]);
            WriteTimeout = int.Parse(param[6]);
            RtsEnable = true;
            DtrEnable = true;
        }

        public void DeviceOpen()
        {
            if (IsOpen)
                throw new Exception("设备已经连接\n");
            Open();
        }

        public void DeviceClose()
        {
            if (IsOpen)
            {
                Application.DoEvents();
                Close();
            }
        }

        delegate void TriggerDelegate(TriggerArgs args);
        TriggerDelegate triggerMethod;
        public IAsyncResult TriggerResult;
        public IAsyncResult BeginTrigger(TriggerArgs args)
        {
            if (triggerMethod == null)
                triggerMethod = new TriggerDelegate(Trigger);

            if (TriggerResult != null && !TriggerResult.IsCompleted)
                return TriggerResult;

            TriggerResult = triggerMethod.BeginInvoke(args, null, null);

            return TriggerResult;
        }

        [ExecuteInfo("TRG", "读码器开始解码", "TRG")]
        public void Trigger(TriggerArgs args)
        {
            for (int i = 0; i < args.tryTimes; i++)
            {
                try
                {                   
                    StopTrigger();
                    receiveString = string.Empty;
                    DiscardInBuffer();
                    WriteLine("\u0016T\r");
                    receiveString = ReadTo("\r");
                    break;
                }
                catch (Exception ex)
                {
                    receiveString = UniversalFlags.errorStr + ex.Message;
                    receiveFinish = true;
                }
            }

            StopTrigger();

            if (args.sender.GetType() != this.GetType())
            {
                if (DeviceDataReceiveCompelete != null)
                    DeviceDataReceiveCompelete(this, receiveString);
            }    
        }

        [ExecuteInfo("RST", "读码器停止解码", "RST")]
        public string StopTrigger()
        {
            try
            {
                WriteLine("\u0016U\r");
                return UniversalFlags.successStr;
            }
            catch (Exception ex)
            {
                return UniversalFlags.errorStr + ex.Message;
            }
        }

        public string Execute(string cmd)
        {
            string result = "";
            cmd = cmd.Trim().ToUpper();
            if (Regex.IsMatch(cmd, "^HELP"))
            {
                Attribute[] attribs = this.GetType().GetMethods().Select(s =>
                    Attribute.GetCustomAttribute(s, typeof(ExecuteInfo))
                    ).Where(s => s != null).ToArray();

                result = Environment.NewLine + "-----------------------------------" + Environment.NewLine;
                result += Environment.NewLine + "该设备支持以下指令：" + Environment.NewLine;
                foreach (Attribute attrib in attribs)
                {
                    ExecuteInfo exe = (ExecuteInfo)attrib;
                    result += Environment.NewLine + exe.Command + " - " + exe.Describe + Environment.NewLine
                        + "示例：" + exe.Example + Environment.NewLine;
                }
                result += Environment.NewLine + "-----------------------------------" + Environment.NewLine;
            }
            else if (Regex.IsMatch(cmd, "^TRG"))
            {
                Trigger(new TriggerArgs()
                {
                    sender = this,
                    tryTimes = 1
                });
                result = receiveString;
            }
            else if (Regex.IsMatch(cmd, "^RST"))
            {
                result = StopTrigger();
            }
            else
            {
                result = "不支持指令" + cmd;
            }
            return result;
        }

        #endregion
    }
}
