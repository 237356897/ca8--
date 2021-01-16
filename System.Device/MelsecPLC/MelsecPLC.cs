using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Device
{
    public abstract class MelsecPLC
    {
        public Common.ExecuteDelegate ConnectStatus;
        public abstract void WriteM(int address, bool value);
        public abstract bool WriteMRetries(int address, bool value, int tryCount = 3);
        public abstract bool WriteMVerify(int writeAddress, int verifyAddress, bool value, int tryCount = 3);
        public abstract bool ReadM(int address);
        public abstract int ReadDeviceBlock(string address, int size, out int value);
        public abstract int WriteDeviceBlock(string address, int size, ref int value);
        public abstract void WriteX(int address, bool value);
        public abstract bool ReadX(int address);
        protected abstract int connectResult { get; set; }
        public abstract int ConnectResult { get; set; }
        public abstract void Open(string address);
        public abstract void Close();
        public abstract void Dispose();
        protected abstract int readWord(string startAddressName);
        protected abstract bool readBit(int address, string type);      
        protected abstract void writeBit(int address, bool value, string type);       
        protected abstract void writeInt16(string startAddressName, int value);
        protected abstract void writeInt32(string startAddressName, int value);             
    }
}
