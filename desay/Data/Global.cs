using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Toolkit;
using System.Toolkit.Helpers;

namespace Desay
{
    public class Global
    {
        #region 单例

        public static Global Instance = new Global();

        private Global() { }

        #endregion

        /// <summary>
        /// 自动运行状态(0=运行 1=停止 2=完成)
        /// </summary>
        public int Stove1RunState = 0;
        /// <summary>
        /// 自动运行状态(0=运行 1=停止 2=完成)
        /// </summary>
        public int Stove2RunState = 0;
        /// <summary>
        /// 自动运行状态(0=运行 1=停止 2=完成)
        /// </summary>
        public int Stove3RunState = 0;
        /// <summary>
        /// 自动运行状态(0=运行 1=停止 2=完成)
        /// </summary>
        public int Stove4RunState = 0;
        /// <summary>
        /// 自动运行状态(0=运行 1=停止 2=完成)
        /// </summary>
        public int Stove5RunState = 0;
        /// <summary>
        /// 自动运行状态(0=运行 1=停止 2=完成)
        /// </summary>
        public int Stove6RunState = 0;
        /// <summary>
        /// 炉1有料感应
        /// </summary>
        public bool Stove1AnyMaterial = false;
        /// <summary>
        /// 炉2有料感应
        /// </summary>
        public bool Stove2AnyMaterial = false;
        /// <summary>
        /// 炉3有料感应
        /// </summary>
        public bool Stove3AnyMaterial = false;
        /// <summary>
        /// 炉4有料感应
        /// </summary>
        public bool Stove4AnyMaterial = false;
        /// <summary>
        /// 炉5有料感应
        /// </summary>
        public bool Stove5AnyMaterial = false;
        /// <summary>
        /// 炉6有料感应
        /// </summary>
        public bool Stove6AnyMaterial = false;


        //用户相关信息
        public string AdminPassword = SecurityHelper.TextToMd5("321");
        public string OperatePassword = SecurityHelper.TextToMd5("123");

        //通信相关(扫码器)
        //ca6
        //public string QRCodeComParam= "COM5,115200,None,8,One,1500,1500";

        /// <summary>
        /// 产品扫描器参数 ca7 ca8
        /// </summary>
        public string QRCodeComParam = "COM6,115200,None,8,One,1500,1500";
        /// <summary>
        /// 料盘扫码器参数
        /// </summary>
        public string TrayCodeComParam = "COM7,115200,None,8,One,1500,1500";
        /// <summary>
        /// 三菱PLC MX端口
        /// </summary>
        public int PLCMX = 0;
        /// <summary>
        /// 机器人IP
        /// </summary>
        public string RobotIP = "192.168.8.115";
        /// <summary>
        /// 机器人端口
        /// </summary>
        public int RobotPort = 2000;
        /// <summary>
        /// Mes参数
        /// </summary>
        public string MesConnectParam = "127.0.0.1,5672,guest,guest,amq.topic,test.1";
        /// <summary>
        /// 屏蔽安全门
        /// </summary>
        public bool SafeDoorSheild;

        public string productName;
        public string OperaNameNum;

    }
}
