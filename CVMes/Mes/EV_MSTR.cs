using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace CVMes
{
    public class EV_MSTR
    {
        public string UUT_Order = "1";
        public string UUT_SOURCE = "EV";
        public string DeviceA2C = "907025501471";
        public string ScanLength = "20";
        public string ProgramNumber = "01";
        public string EquipmentFunction = "EOL";
        public string SerialNumber = "9070255014711234567";//实时 
        public string StationName = "VXI75030";
        public string ProductTime = "160228142021";//实时
        public string TestStandName = "EOL";
        public string LoginName = "";
        public string ExecutionTime = "";//实时
        public string TestSocket = "-1";
        public string BatchSerialNumber = "";//实时
        public string TestStatus = "OK";

        //数据库参数
        public string DB_Password = "";
        public string DB_User = "";
        public string DatabaseName = "";
        public string ServerName = "";
        //public string SerialNumber = "";
        public string StationID = "";
        public string LineGroup = "";
        public string SW_User = "";
        public string Debug = "";
        public string ShowWindow = "";
        public string PassForNoDB = "";
        public string Function = "";

        [DllImport("sv_Interlocking_Main.dll", EntryPoint = "Sv_Interlocking_Main")]
        public static extern long Sv_Interlocking_Main(string info);

    }
}
