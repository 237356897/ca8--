using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Toolkit;

namespace System.Device
{
   public class MelsecPLCOperator
    {
        public MelsecPLC Melsec_PLC;
        public MelsecPLCTcpUtl melsecPLCTcpUtl;
        public bool[] Monitor_MInput;
        public bool[] Monitor_MOutput;

        private Thread runThread;
        private string inputStartAddressName;
        private string outputStartAddressName;       

        #region 构造函数
        /// <summary>
        /// 实例化读取三菱PLC数据的对象（TCP网络协议）
        /// </summary>
        /// <param name="plc"></param>
        /// <param name="inputStartAddressName"></param>
        /// <param name="inputCount"></param>
        /// <param name="outputStartAddressName"></param>
        /// <param name="outputCount"></param>
        public MelsecPLCOperator(MelsecPLCTcp plc,string inputStartAddressName,int inputCount,string outputStartAddressName,int outputCount)
        {
            this.inputStartAddressName = inputStartAddressName;
            Monitor_MInput = new bool[(((inputCount - 1) / 16) + 1) * 16];
            this.outputStartAddressName = outputStartAddressName;
            Monitor_MOutput = new bool[(((outputCount - 1) / 16) + 1) * 16];

            Melsec_PLC = plc;
            if (plc.ConnectResult == 0)
            {
                runThread = new Thread(new ThreadStart(run));
                runThread.IsBackground = true;
                runThread.Start();
            }
        }
        /// <summary>
        /// 实例化读取三菱PLC数据的对象（RS232串口协议）
        /// </summary>
        /// <param name="plc"></param>
        /// <param name="inputStartAddressName"></param>
        /// <param name="inputCount"></param>
        /// <param name="outputStartAddressName"></param>
        /// <param name="outputCount"></param>
        public MelsecPLCOperator(MelsecPLCRs232 plc, string inputStartAddressName, int inputCount, string outputStartAddressName, int outputCount)
        {
            this.inputStartAddressName = inputStartAddressName;
            Monitor_MInput = new bool[(((inputCount - 1) / 16) + 1) * 16];
            this.outputStartAddressName = outputStartAddressName;
            Monitor_MOutput = new bool[(((outputCount - 1) / 16) + 1) * 16];

            Melsec_PLC = plc;
            if (plc.ConnectResult == 0)
            {
                runThread = new Thread(new ThreadStart(run));
                runThread.IsBackground = true;
                runThread.Start();
            }
        }
        /// <summary>
        /// 实例化读取三菱PLC数据的对象（Utl网络协议）
        /// </summary>
        /// <param name="plc"></param>
        /// <param name="inputStartAddressName"></param>
        /// <param name="inputCount"></param>
        /// <param name="outputStartAddressName"></param>
        /// <param name="outputCount"></param>
        public MelsecPLCOperator(MelsecPLCTcpUtl plc, string inputStartAddressName, int inputCount, string outputStartAddressName, int outputCount)
        {
            this.inputStartAddressName = inputStartAddressName;
            Monitor_MInput = new bool[(((inputCount - 1) / 16) + 1) * 16];
            this.outputStartAddressName = outputStartAddressName;
            Monitor_MOutput = new bool[(((outputCount - 1) / 16) + 1) * 16];

            Melsec_PLC = plc;
            melsecPLCTcpUtl = plc;

            if (plc.ConnectResult == 0)
            {
                //runThread = new Thread(new ThreadStart(run));
                //runThread.IsBackground = true;
                //runThread.Start();
            }
        }
        #endregion
               

        #region 主运行线程
        private void run()
        {
            //while (true)
            //{
            //    readData(inputStartAddressName, Monitor_MInput);
            //    readData(outputStartAddressName, Monitor_MOutput);

            //    Thread.Sleep(20);
            //}
        }
        #endregion

        #region 单个读写 D M
        public bool ReadM(int address)
        {
            return Melsec_PLC.ReadM(address);
        }
        public void WriteM(int address,bool value)
        {
            Melsec_PLC.WriteM(address, value);
        }
        public int ReadD(int address,int size)
        {
            int value;
            Melsec_PLC.ReadDeviceBlock("D" + address, size, out value);
            return value;
        }
        public void WriteD(int address,int size,int value)
        {
            Melsec_PLC.WriteDeviceBlock("D" + address, size, ref value);
        }

        /// <summary>
        /// 断开三菱PLC链接
        /// </summary>
        public void Close()
        {
            //runThread.Abort();
            Melsec_PLC.Close();
        }
        #endregion


        #region 批量读写 D M
        private void readData(string startAddressName, bool[] data)
        {
            int count = ((data.Length - 1) / 16) + 1;
            int startAddress = Convert.ToInt32(startAddressName.Substring(1));
            for (int n = 0; n < count; ++n)
            {
                int outValue = 0;
                string strBits;
                Melsec_PLC.ReadDeviceBlock("M" + startAddress + (n * 16), 1, out outValue);               
                byte[] byteDatas = BitConverter.GetBytes(outValue);
                for (int i = 0; i < byteDatas.Length - 2; ++i)
                {
                    strBits = Convert.ToString(byteDatas[i], 2);
                    strBits = strBits.PadLeft(8, '0');
                    for (int j = 0; j < 8; ++j)
                    {
                        data[(n * 16) + (i * 8) + j] = strBits[7 - j] == '1' ? true : false;
                    }
                }
            }
        }

        private void ReadData_M(string startAddressName, bool[] data)
        {
            int count = ((data.Length - 1) / 16) + 1;
            int startAddress = Convert.ToInt32(startAddressName.Substring(1));
            for (int n = 0; n < count; ++n)
            {
                int outValue = 0;
                string strBits;
                Melsec_PLC.ReadDeviceBlock("M" + (startAddress + (n * 16)), 1, out outValue);
                byte[] byteDatas = BitConverter.GetBytes(outValue);
                for (int i = 0; i < byteDatas.Length - 2; ++i)
                {
                    strBits = Convert.ToString(byteDatas[i], 2);
                    strBits = strBits.PadLeft(8, '0');
                    for (int j = 0; j < 8; ++j)
                    {
                        data[(n * 16) + (i * 8) + j] = strBits[7 - j] == '1' ? true : false;
                    }
                }
            }
        }
        #endregion
    }
}
