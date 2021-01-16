using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Toolkit;
using System.Diagnostics;

namespace Desay
{
    public class Marking
    {
        /// <summary>
        ///  扫码标识
        /// </summary>
        public static bool QRCodeSign = true;

        /// <summary>
        ///  PLC烤箱总开启
        /// </summary>
        public static bool plcStoveTotalStart = false;

        public static bool CleanProductDone;

        /// <summary>
        /// 蜂鸣器关闭
        /// </summary>
        public static bool VoiceClosed;

        public static string userName = "Local";
        public static UserLevel userLevel = UserLevel.None;

        /// <summary>
        /// 通知机器人取料
        /// </summary>
        public static bool ConnectionInform = false;

        /// <summary>
        /// 通知机器人取盘
        /// </summary>
    //    public static bool BackflowInform = false;
        /// <summary>
        /// 原点待料盘标记
        /// </summary>
        public static bool BackflowStayPlate = false;

        /// <summary>
        /// 机器人通知输出盘
        /// </summary>
      //public static bool RobotInform = false;

        /// <summary>
        /// 输送线状态
        /// </summary>
        public static Output RobotStatus = Output.s10;
        /// <summary>
        /// 输送线前状态
        /// </summary>
        public static Output preRobotStatus = Output.s10;
        /// <summary>
        /// 通知输出中
        /// </summary>
        public static bool RobotInformOutputting = false;
        /// <summary>
        /// 机器人盘放好
        /// </summary>
        public static bool RobotPutAway = false;

        /// <summary>
        /// 立即上炉
        /// </summary>
        public static bool ImmediatelyUpStove = false;

        /// <summary>
        /// 开始计度
        /// </summary>
        public static bool[] StartTemperatureSign = new bool[6] { false, false, false, false, false, false };
        /// <summary>
        /// 首次计度
        /// </summary>
        public static bool[] FirstTemperatureSign = new bool[6] { true, true, true, true, true, true };
        /// <summary>
        /// 固化时间
        /// </summary>
        public static Stopwatch[] CuringTime = new Stopwatch[6] { new Stopwatch(), new Stopwatch(), new Stopwatch(), new Stopwatch(), new Stopwatch(), new Stopwatch() };
        /// <summary>
        /// 固化停止时间标记
        /// </summary>
        public static bool[] StopCuringTimeSign = new bool[6] { true, true, true, true, true, true };

        /// <summary>
        ///上炉刷新状态
        /// </summary>
        public static bool[] UpStoveRefreshState = new bool[6] { false, false, false, false, false, false };
        /// <summary>
        ///下炉刷新状态
        /// </summary>
        public static bool[] DownStoveRefreshState = new bool[6] { false, false, false, false, false, false };

        /// <summary>
        ///炉产品清空
        /// </summary>
        public static bool[] StoveProductClear = new bool[6] { false, false, false, false, false, false };
        /// <summary>
        ///料盘产品清空
        /// </summary> 
        public static bool TaryProductClear = false;


        /// <summary>
        ///AAOK
        /// </summary> 
        public static bool AAOK = false;
        /// <summary>
        ///AANG
        /// </summary> 
        public static bool AANG = false;

        /// <summary>
        ///报警立即停止运行线程
        /// </summary> 
        public static bool AlarmStopThread = true;
    }
}
