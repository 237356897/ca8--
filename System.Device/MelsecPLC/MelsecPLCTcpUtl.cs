using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Windows.Forms;
using System.Toolkit;
using System.Diagnostics;
using System.Threading;

namespace System.Device
{
    public class MelsecPLCTcpUtl:MelsecPLC, IDisposable
    {
        private ILog log = LogManager.GetLogger(typeof(MelsecPLCTcpUtl));

        protected override int connectResult { get; set; }
        public override int ConnectResult { get { return connectResult; } set { connectResult = value; ConnectStatus?.Invoke(); } }

        private ActUtlTypeLib.ActUtlTypeClass plc;


        ~MelsecPLCTcpUtl()
        {
            try
            {
                if (ConnectResult == 0)
                { Close(); }
            }
            catch (Exception e)
            { log.ErrorFormat("{0}", e); }
        }
        public MelsecPLCTcpUtl(string stationNumber)
        {
            Open(stationNumber);
            if (ConnectResult != 0)
            {
                //MessageBox.Show(string.Format("三菱PLC站号：{0}通讯异常--Error：{1}", stationNumber, ConnectResult.ToString()));
                log.ErrorFormat("三菱PLC站号：{0}通讯异常--Error：{1}", stationNumber, ConnectResult.ToString());
            }
        }

        #region public Method
        public override void WriteM(int address, bool value)
        {
            writeBit(address, value, "M");
        }
        public override bool ReadM(int address)
        {
            return readBit(address, "M");
        }
        public override void WriteX(int address, bool value)
        {
            writeBit(address, value, "X");
        }
        public override bool ReadX(int address)
        {
            return readBit(address, "X");
        }
        public override bool WriteMRetries(int address, bool value, int tryCount = 3)
        {
            int count = 0;
            bool isOK = false;
            while (count < tryCount)
            {
                WriteM(address, value);
                if (ReadM((address)) == value)
                {
                    isOK = true;
                    break;
                }
                else
                { count++; }
            }

            if (!isOK)
            { log.ErrorFormat("Write M{0}:{1} FAIL", address, value); }
            return isOK;
        }

        public override bool WriteMVerify(int writeAddress, int verifyAddress, bool value, int tryCount = 3)
        {
            int count = 0;
            bool isOK = false;
            while (count < tryCount)
            {
                WriteM(writeAddress, value);
                if (ReadM((verifyAddress)) == value)
                {
                    isOK = true;
                    break;
                }
                else
                { count++; }
            }

            if (!isOK)
            { log.ErrorFormat("Write M{0}:{1} --Read M{2}=={1} FAIL", writeAddress, value, verifyAddress); }
            return isOK;
        }

        public override int ReadDeviceBlock(string address, int size, out int value)
        {
            return plc.ReadDeviceBlock(address, size, out value);            
        }

        public override int WriteDeviceBlock(string address, int size, ref int value)
        {
            return plc.WriteDeviceBlock(address, size, ref value);
        }
        public override void Open(string address)
        {
            try
            {
                plc = new ActUtlTypeLib.ActUtlTypeClass();
                plc.ActLogicalStationNumber = int.Parse(address);

                Stopwatch PLCTime = new Stopwatch();
                PLCTime.Restart();
                do
                {
                    ConnectResult = plc.Open();
                    Thread.Sleep(50);
                }
                while (ConnectResult != 0 && PLCTime.ElapsedMilliseconds < 5000);
            }
            catch (Exception ex)
            {
                ConnectResult = -1;
            }
        }
        public override void Close()
        {
            plc.Close();
            ConnectResult = -1;
        }
        public override void Dispose()
        {
            plc.Close();
            ConnectResult = -1;
        }
        #endregion

        #region protected Method
        protected override void writeBit(int address, bool value, string type)
        {
            int index = address % 16;
            int start = address - index;

            string name = type + start.ToString();
            int val = readWord(name);

            int newVal = val;
            int mask = 0;
            if (value)
            {
                mask = 1 << index;
                newVal = newVal | mask;
            }
            else
            {
                mask = 0xffff ^ 1 << index;
                newVal = newVal & mask;
            }
            writeInt16(name, newVal);
        }
       

        protected override int readWord(string startAddressName)
        {
            int val = 0;
            int res = -1;

            try
            { res = plc.ReadDeviceBlock(startAddressName, 1, out val); }
            catch (Exception e)
            { log.ErrorFormat("{0}", e); }
            return val;
        }

        /// <summary>
        /// 读取多个寄存器
        /// </summary>
        /// <param name="startAddressName">起始地址</param>
        /// <param name="length">个数</param>
        /// <returns></returns>
        public int readsomeWord(string startAddressName, int length)
        {
            int val = 0;
            int res = -1;
            try
            {
                res = plc.ReadDeviceBlock(startAddressName, length, out val);
            }
            catch (Exception e)
            { log.ErrorFormat("{0}", e); }
            return val;
        }

        protected override bool readBit(int address, string type)
        {
            int index = address % 16;
            int start = address - index;

            string name = type + start.ToString();
            int val = readWord(name);
            int mask = 1 << index;
            return (val & mask) > 0;
        }

        /// <summary>
        /// 读取16个bit
        /// </summary>
        /// <param name="address">起始地址(必须是16的倍数)</param>
        /// <param name="type">读取类型(X,Y,M)</param>
        /// <returns></returns>
        public bool[] read16Bit(int address, string type)
        {
            bool[] Bits= new bool[16];
            if (address % 16 == 0)
            {
                string name = type + address.ToString();
                int val = readWord(name);
                for (int i = 0; i < Bits.Length; i++)
                {
                    int mask = 1 << i;
                    Bits[i] = (val & mask) > 0;
                }
            }
            return Bits;
        }

        protected override void writeInt16(string startAddressName, int value)
        {
            int res = plc.WriteDeviceBlock(startAddressName, 1, ref value);
        }

        protected override void writeInt32(string startAddressName, int value)
        {
            int[] DestPos = new int[2];
            var b = BitConverter.GetBytes(value);
            DestPos[0] = b[0] + b[1] * 256; ;
            DestPos[1] = b[2] + b[3] * 256;

            int res = plc.WriteDeviceBlock(startAddressName, 2, ref DestPos[0]);
        }
        #endregion
    }
}
