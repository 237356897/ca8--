using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Motion.AdlinkDash;
using Motion.Interfaces;
using Motion.AdlinkAps;

namespace Desay
{
    /// <summary>
    ///     设备 IO 项
    /// </summary>
    public class IoPoints
    {
        private const string ApsControllerName = "ApsController";
        private const string daskControllerName = "daskController";
        internal static readonly int Card204C = 0;
        internal static readonly byte PCI7432 = 0;
        public static ApsController m_ApsController = new ApsController(Card204C) { Name = ApsControllerName };
        public static DaskController m_DaskController = new DaskController(PCI7432) { Name = daskControllerName };

        #region Card204C IO list

        /// <summary>
        ///  备用
        /// </summary>
        public static IoPoint T1DI00 = new IoPoint(m_ApsController, Card204C, 8, IoModes.Senser)
        {
            Name = "T1DI00",
            Description = "备用"
        };
        /// <summary>
        ///   备用 
        /// </summary>
        public static IoPoint T1DI01 = new IoPoint(m_ApsController, Card204C, 9, IoModes.Senser)
        {
            Name = "T1DI01",
            Description = "备用"
        };

        /// <summary>
        ///   接驳台定位气缸原点
        /// </summary>
        public static IoPoint T1DI02 = new IoPoint(m_ApsController, Card204C, 10, IoModes.Senser)
        {
            Name = "T1DI02",
            Description = "接驳台定位气缸原点"
        };

        /// <summary>
        ///   接驳台定位气缸动点
        /// </summary>
        public static IoPoint T1DI03 = new IoPoint(m_ApsController, Card204C, 11, IoModes.Senser)
        {
            Name = "T1DI03",
            Description = "接驳台定位气缸动点"
        };

        /// <summary>
        ///   接驳台开夹气缸原点
        /// </summary>
        public static IoPoint T1DI04 = new IoPoint(m_ApsController, Card204C, 12, IoModes.Senser)
        {
            Name = "T1DI04",
            Description = "接驳台开夹气缸原点"
        };

        /// <summary>
        ///   接驳台开夹气缸动点
        /// </summary>
        public static IoPoint T1DI05 = new IoPoint(m_ApsController, Card204C, 13, IoModes.Senser)
        {
            Name = "T1DI05",
            Description = "接驳台开夹气缸动点"
        };

        /// <summary>
        ///   AA有料信号
        /// </summary>
        public static IoPoint T1DI06 = new IoPoint(m_ApsController, Card204C, 14, IoModes.Senser)
        {
            Name = "T1DI06",
            Description = "AA有料信号"
        };

        /// <summary>
        ///   AA Ready信号
        /// </summary>
        public static IoPoint T1DI07 = new IoPoint(m_ApsController, Card204C, 15, IoModes.Senser)
        {
            Name = "T1DI07",
            Description = "AA Ready信号"
        };

        /// <summary>
        ///   AA OK信号
        /// </summary>
        public static IoPoint T1DI08 = new IoPoint(m_ApsController, Card204C, 16, IoModes.Senser)
        {
            Name = "T1DI08",
            Description = "AA OK信号"
        };
        /// <summary>
        ///   AA NG信号
        /// </summary>
        public static IoPoint T1DI09 = new IoPoint(m_ApsController, Card204C, 17, IoModes.Senser)
        {
            Name = "T1DI09",
            Description = "AA NG信号"
        };
        /// <summary>
        ///   备用
        /// </summary>
        public static IoPoint T1DI10 = new IoPoint(m_ApsController, Card204C, 18, IoModes.Senser)
        {
            Name = "T1DI10",
            Description = "备用"
        };
        /// <summary>
        ///   备用
        /// </summary>
        public static IoPoint T1DI11 = new IoPoint(m_ApsController, Card204C, 19, IoModes.Senser)
        {
            Name = "T1DI11",
            Description = "备用"
        };
        /// <summary>
        ///   备用
        /// </summary>
        public static IoPoint T1DI12 = new IoPoint(m_ApsController, Card204C, 20, IoModes.Senser)
        {
            Name = "TDI12",
            Description = "备用"
        };
        /// <summary>
        ///   备用
        /// </summary>
        public static IoPoint T1DI13 = new IoPoint(m_ApsController, Card204C, 21, IoModes.Senser)
        {
            Name = "T1DI13",
            Description = "备用"
        };

        /// <summary>
        ///   备用
        /// </summary>
        public static IoPoint T1DI14 = new IoPoint(m_ApsController, Card204C, 22, IoModes.Senser)
        {
            Name = "T1DI14",
            Description = "备用"
        };
        /// <summary>
        ///   备用
        /// </summary>
        public static IoPoint T1DI15 = new IoPoint(m_ApsController, Card204C, 23, IoModes.Senser)
        {
            Name = "T1DI15",
            Description = "备用"
        };

        /// <summary>
        ///   接驳台Ready（ToAA）
        /// </summary>
        public static IoPoint T1DO00 = new IoPoint(m_ApsController, Card204C, 8, IoModes.Signal)
        {
            Name = "T1DO00",
            Description = "接驳台Ready（ToAA）"
        };

        /// <summary>
        /// 接驳台有料（ToAA）
        /// </summary>
        public static IoPoint T1DO01 = new IoPoint(m_ApsController, Card204C, 9, IoModes.Signal)
        {
            Name = "T1DO01",
            Description = "接驳台有料（ToAA）"
        };

        /// <summary>
        ///   备用
        /// </summary>
        public static IoPoint T1DO02 = new IoPoint(m_ApsController, Card204C, 10, IoModes.Signal)
        {
            Name = "T1DO02",
            Description = "备用"
        };

        /// <summary>
        ///   备用
        /// </summary>
        public static IoPoint T1DO03 = new IoPoint(m_ApsController, Card204C, 11, IoModes.Signal)
        {
            Name = "T1DO03",
            Description = "备用"
        };

        /// <summary>
        ///   备用
        /// </summary>
        public static IoPoint T1DO04 = new IoPoint(m_ApsController, Card204C, 12, IoModes.Signal)
        {
            Name = "T1DO04",
            Description = "备用"
        };

        /// <summary>
        ///   备用
        /// </summary>
        public static IoPoint T1DO05 = new IoPoint(m_ApsController, Card204C, 13, IoModes.Signal)
        {
            Name = "T1DO05",
            Description = "备用"
        };

        /// <summary>
        ///  备用
        /// </summary>
        public static IoPoint T1DO06 = new IoPoint(m_ApsController, Card204C, 14, IoModes.Signal)
        {
            Name = "T1DO06",
            Description = "备用"
        };

        /// <summary>
        ///  备用
        /// </summary>
        public static IoPoint T1DO07 = new IoPoint(m_ApsController, Card204C, 15, IoModes.Signal)
        {
            Name = "T1DO07",
            Description = "备用"
        };

        /// <summary>
        ///   备用
        /// </summary>
        public static IoPoint T1DO08 = new IoPoint(m_ApsController, Card204C, 16, IoModes.Signal)
        {
            Name = "T1DO08",
            Description = "备用"
        };

        /// <summary>
        ///  备用
        /// </summary>
        public static IoPoint T1DO09 = new IoPoint(m_ApsController, Card204C, 17, IoModes.Signal)
        {
            Name = "T1DO09",
            Description = "备用"
        };

        /// <summary>
        ///  备用
        /// </summary>
        public static IoPoint T1DO10 = new IoPoint(m_ApsController, Card204C, 18, IoModes.Signal)
        {
            Name = "T1DO10",
            Description = "备用"
        };

        /// <summary>
        ///  备用
        /// </summary>
        public static IoPoint T1DO11 = new IoPoint(m_ApsController, Card204C, 19, IoModes.Signal)
        {
            Name = "T1DO11",
            Description = "备用"
        };

        /// <summary>
        ///  备用
        /// </summary>
        public static IoPoint T1DO12 = new IoPoint(m_ApsController, Card204C, 20, IoModes.Signal)
        {
            Name = "T1DO12",
            Description = "备用"
        };

        /// <summary>
        ///   备用
        /// </summary>
        public static IoPoint T1DO13 = new IoPoint(m_ApsController, Card204C, 21, IoModes.Signal)
        {
            Name = "T1DO13",
            Description = "备用"
        };

        /// <summary>
        ///  备用
        /// </summary>
        public static IoPoint T1DO14 = new IoPoint(m_ApsController, Card204C, 22, IoModes.Signal)
        {
            Name = "T1DO14",
            Description = "备用"
        };

        /// <summary>
        ///  备用
        /// </summary>
        public static IoPoint T1DO15 = new IoPoint(m_ApsController, Card204C, 23, IoModes.Signal)
        {
            Name = "T1DO15",
            Description = "备用"
        };

        #endregion

        #region PCI7432 IO list
        /// <summary>
        ///   启动按钮
        /// </summary>
        public static IoPoint I2DI00 = new IoPoint(m_DaskController, PCI7432, 0, IoModes.Senser)
        {
            Name = "I2DI00",
            Description = "启动按钮"
        };
        /// <summary>
        ///   暂停按钮
        /// </summary>
        public static IoPoint I2DI01 = new IoPoint(m_DaskController, PCI7432, 1, IoModes.Senser)
        {
            Name = "I2DI01",
            Description = "暂停按钮"
        };
        /// <summary>
        ///   复位按钮   
        /// </summary>
        public static IoPoint I2DI02 = new IoPoint(m_DaskController, PCI7432, 2, IoModes.Senser)
        {
            Name = "I2DI02",
            Description = "复位按钮"
        };
        /// <summary>
        ///   急停
        /// </summary>
        public static IoPoint I2DI03 = new IoPoint(m_DaskController, PCI7432, 3, IoModes.Senser)
        {
            Name = "I2DI03",
            Description = "急停"
        };
        /// <summary>
        ///   门禁
        /// </summary>
        public static IoPoint I2DI04 = new IoPoint(m_DaskController, PCI7432, 4, IoModes.Senser)
        {
            Name = "I2DI04",
            Description = "门禁"
        };
        /// <summary>
        ///   ABB在安全位
        /// </summary>
        public static IoPoint I2DI05 = new IoPoint(m_DaskController, PCI7432, 5, IoModes.Senser)
        {
            Name = "I2DI05",
            Description = "ABB在安全位"
        };
        /// <summary>
        ///   气压不足报警
        /// </summary>
        public static IoPoint I2DI06 = new IoPoint(m_DaskController, PCI7432, 6, IoModes.Senser)
        {
            Name = "I2DI06",
            Description = "气压不足报警"
        };
        /// <summary>
        ///  固化炉急停
        /// </summary>
        public static IoPoint I2DI07 = new IoPoint(m_DaskController, PCI7432, 7, IoModes.Senser)
        {
            Name = "I2DI07",
            Description = "固化炉急停"
        };
        /// <summary>
        ///  备用
        /// </summary>
        public static IoPoint I2DI08 = new IoPoint(m_DaskController, PCI7432, 8, IoModes.Senser)
        {
            Name = "I2DI08",
            Description = "备用"
        };
        /// <summary>
        ///   备用
        /// </summary>
        public static IoPoint I2DI09 = new IoPoint(m_DaskController, PCI7432, 9, IoModes.Senser)
        {
            Name = "I2DI09",
            Description = "备用"
        };
        /// <summary>
        ///  备用
        /// </summary>
        public static IoPoint I2DI10 = new IoPoint(m_DaskController, PCI7432, 10, IoModes.Senser)
        {
            Name = "I2DI10",
            Description = "备用"
        };
        /// <summary>
        ///  备用
        /// </summary>
        public static IoPoint I2DI11 = new IoPoint(m_DaskController, PCI7432, 11, IoModes.Senser)
        {
            Name = "I2DI11",
            Description = "备用"
        };
        /// <summary>
        ///  备用
        /// </summary>
        public static IoPoint I2DI12 = new IoPoint(m_DaskController, PCI7432, 12, IoModes.Senser)
        {
            Name = "I2DI12",
            Description = "备用"
        };
        /// <summary>
        ///   备用
        /// </summary>
        public static IoPoint I2DI13 = new IoPoint(m_DaskController, PCI7432, 13, IoModes.Senser)
        {
            Name = "I2DI13",
            Description = "备用"
        };
        /// <summary>
        ///   虚似端子，务接线
        /// </summary>
        public static IoPoint I2DI14 = new IoPoint(m_DaskController, PCI7432, 14, IoModes.Senser)
        {
            Name = "I2DI14",
            Description = "虚似端子，务接线"
        };
        /// <summary>
        ///   备用
        /// </summary>
        public static IoPoint I2DI15 = new IoPoint(m_DaskController, PCI7432, 15, IoModes.Senser)
        {
            Name = "I2DI15",
            Description = "备用"
        };
        /// <summary>
        ///   OK位有料盘感应
        /// </summary>
        public static IoPoint I2DI16 = new IoPoint(m_DaskController, PCI7432, 16, IoModes.Senser)
        {
            Name = "I2DI16",
            Description = "OK位有料盘感应"
        };
        /// <summary>
        ///   NG位有料盘感应
        /// </summary>
        public static IoPoint I2DI17 = new IoPoint(m_DaskController, PCI7432, 17, IoModes.Senser)
        {
            Name = "I2DI17",
            Description = "NG位有料盘感应"
        };
        /// <summary>
        ///   待料位1有料盘感应
        /// </summary>
        public static IoPoint I2DI18 = new IoPoint(m_DaskController, PCI7432, 18, IoModes.Senser)
        {
            Name = "I2DI18",
            Description = "待料位1有料盘感应"
        };
        /// <summary>
        /// 输送气缸上料有料盘感应
        /// </summary>
        public static IoPoint I2DI19 = new IoPoint(m_DaskController, PCI7432, 19, IoModes.Senser)
        {
            Name = "I2DI19",
            Description = "输送气缸上料有料盘感应"
        };
        /// <summary>
        /// 输送气缸下料有料盘感应
        /// </summary>
        public static IoPoint I2DI20 = new IoPoint(m_DaskController, PCI7432, 20, IoModes.Senser)
        {
            Name = "I2DI20",
            Description = "输送气缸下料有料盘感应"
        };
        /// <summary>
        ///  待料位2有料盘感应
        /// </summary>
        public static IoPoint I2DI21 = new IoPoint(m_DaskController, PCI7432, 21, IoModes.Senser)
        {
            Name = "I2DI21",
            Description = "待料位2有料盘感应"
        };
        /// <summary>
        ///   ABB报错
        /// </summary>
        public static IoPoint I2DI22 = new IoPoint(m_DaskController, PCI7432, 22, IoModes.Senser)
        {
            Name = "I2DI22",
            Description = "ABB报错"
        };
        /// <summary>
        ///   备用
        /// </summary>
        public static IoPoint I2DI23 = new IoPoint(m_DaskController, PCI7432, 23, IoModes.Senser)
        {
            Name = "I2DI23",
            Description = "备用"
        };
        /// <summary>
        ///  温度感应信号
        /// </summary>
        public static IoPoint I2DI24 = new IoPoint(m_DaskController, PCI7432, 24, IoModes.Senser)
        {
            Name = "I2DI24",
            Description = "温度感应信号"
        };
        /// <summary>
        ///   下料确认按钮
        /// </summary>
        public static IoPoint I2DI25 = new IoPoint(m_DaskController, PCI7432, 25, IoModes.Senser)
        {
            Name = "I2DI25",
            Description = "下料确认按钮"
        };
        /// <summary>
        ///   备用
        /// </summary>
        public static IoPoint I2DI26 = new IoPoint(m_DaskController, PCI7432, 26, IoModes.Senser)
        {
            Name = "I2DI26",
            Description = "备用"
        };
        /// <summary>
        ///   接驳台到位感应
        /// </summary>
        public static IoPoint I2DI27 = new IoPoint(m_DaskController, PCI7432, 27, IoModes.Senser)
        {
            Name = "I2DI27",
            Description = "接驳台到位感应"
        };
        /// <summary>
        ///  备用
        /// </summary>
        public static IoPoint I2DI28 = new IoPoint(m_DaskController, PCI7432, 28, IoModes.Senser)
        {
            Name = "I2DI28",
            Description = "备用"
        };
        /// <summary>
        ///   AA接驳台夹具有料感应
        /// </summary>
        public static IoPoint I2DI29 = new IoPoint(m_DaskController, PCI7432, 29, IoModes.Senser)
        {
            Name = "I2DI29",
            Description = "AA接驳台夹具有料感应"
        };
        /// <summary>
        ///   接驳台平移气缸原点
        /// </summary>
        public static IoPoint I2DI30 = new IoPoint(m_DaskController, PCI7432, 30, IoModes.Senser)
        {
            Name = "I2DI30",
            Description = "接驳台平移气缸原点"
        };
        /// <summary>
        ///   接驳台平移气缸动点
        /// </summary>
        public static IoPoint I2DI31 = new IoPoint(m_DaskController, PCI7432, 31, IoModes.Senser)
        {
            Name = "I2DI31",
            Description = "接驳台平移气缸动点"
        };
        /// <summary>
        ///   三色灯红灯
        /// </summary>
        public static IoPoint I2DO00 = new IoPoint(m_DaskController, PCI7432, 0, IoModes.Signal)
        {
            Name = "I2DO00",
            Description = "三色灯红灯"
        };
        /// <summary>
        ///    三色灯黄灯
        /// </summary>
        public static IoPoint I2DO01 = new IoPoint(m_DaskController, PCI7432, 1, IoModes.Signal)
        {
            Name = "I2DO01",
            Description = "三色灯黄灯"
        };
        /// <summary>
        ///   三色灯绿灯
        /// </summary>
        public static IoPoint I2DO02 = new IoPoint(m_DaskController, PCI7432, 2, IoModes.Signal)
        {
            Name = "I2DO02",
            Description = "三色灯绿灯"
        };
        /// <summary>
        ///   蜂鸣器
        /// </summary>
        public static IoPoint I2DO03 = new IoPoint(m_DaskController, PCI7432, 3, IoModes.Signal)
        {
            Name = "I2DO03",
            Description = " 蜂鸣器"
        };
        /// <summary>
        ///   停止按钮灯
        /// </summary>
        public static IoPoint I2DO04 = new IoPoint(m_DaskController, PCI7432, 4, IoModes.Signal)
        {
            Name = "I2DO04",
            Description = "停止按钮灯"
        };
        /// <summary>
        ///   暂停按钮灯
        /// </summary>
        public static IoPoint I2DO05 = new IoPoint(m_DaskController, PCI7432, 5, IoModes.Signal)
        {
            Name = "I2DO05",
            Description = "暂停按钮灯"
        };
        /// <summary>
        ///   复位按钮灯
        /// </summary>
        public static IoPoint I2DO06 = new IoPoint(m_DaskController, PCI7432, 6, IoModes.Signal)
        {
            Name = "I2DO06",
            Description = "复位按钮灯"
        };
        /// <summary>
        ///   下料确认按钮灯
        /// </summary>
        public static IoPoint I2DO07 = new IoPoint(m_DaskController, PCI7432, 7, IoModes.Signal)
        {
            Name = "I2DO07",
            Description = "下料确认按钮灯"
        };
        /// <summary>
        ///   启动ABB
        /// </summary>
        public static IoPoint I2DO08 = new IoPoint(m_DaskController, PCI7432, 8, IoModes.Signal)
        {
            Name = "I2DO08",
            Description = "启动ABB"
        };
        /// <summary>
        ///   ABB程序复位
        /// </summary>
        public static IoPoint I2DO09 = new IoPoint(m_DaskController, PCI7432, 9, IoModes.Signal)
        {
            Name = "I2DO09",
            Description = "ABB程序复位"
        };
        /// <summary>
        ///   ABB急停复位
        /// </summary>
        public static IoPoint I2DO10 = new IoPoint(m_DaskController, PCI7432, 10, IoModes.Signal)
        {
            Name = "I2DO10",
            Description = "ABB急停复位"
        };
        /// <summary>
        ///  ABB电机上电
        /// </summary>
        public static IoPoint I2DO11 = new IoPoint(m_DaskController, PCI7432, 11, IoModes.Signal)
        {
            Name = "I2DO11",
            Description = "ABB电机上电"
        };
        /// <summary>
        ///   ABB停止
        /// </summary>
        public static IoPoint I2DO12 = new IoPoint(m_DaskController, PCI7432, 12, IoModes.Signal)
        {
            Name = "I2DO12",
            Description = "ABB停止"
        };
        /// <summary>
        ///   备用
        /// </summary>
        public static IoPoint I2DO13 = new IoPoint(m_DaskController, PCI7432, 13, IoModes.Signal)
        {
            Name = "I2DO13",
            Description = "备用"
        };
        /// <summary>
        ///   备用
        /// </summary>
        public static IoPoint I2DO14 = new IoPoint(m_DaskController, PCI7432, 14, IoModes.Signal)
        {
            Name = "I2DO14",
            Description = "备用"
        };
        /// <summary>
        ///   备用
        /// </summary>
        public static IoPoint I2DO15 = new IoPoint(m_DaskController, PCI7432, 15, IoModes.Signal)
        {
            Name = "I2DO15",
            Description = "备用"
        };
        /// <summary>
        ///  备用
        /// </summary>
        public static IoPoint I2DO16 = new IoPoint(m_DaskController, PCI7432, 16, IoModes.Signal)
        {
            Name = "I2DO16",
            Description = "备用"
        };
        /// <summary>
        ///   照明灯
        /// </summary>
        public static IoPoint I2DO17 = new IoPoint(m_DaskController, PCI7432, 17, IoModes.Signal)
        {
            Name = "I2DO17",
            Description = "照明灯"
        };
        /// <summary>
        ///   等离子风扇
        /// </summary>
        public static IoPoint I2DO18 = new IoPoint(m_DaskController, PCI7432, 18, IoModes.Signal)
        {
            Name = "I2DO18",
            Description = "等离子风扇"
        };
        /// <summary>
        ///   接驳台平移气缸电磁阀2
        /// </summary>
        public static IoPoint I2DO19 = new IoPoint(m_DaskController, PCI7432, 19, IoModes.Signal)
        {
            Name = "I2DO19",
            Description = "接驳台平移气缸电磁阀2"
        };
        /// <summary>
        ///   接驳台正转
        /// </summary>
        public static IoPoint I2DO20 = new IoPoint(m_DaskController, PCI7432, 20, IoModes.Signal)
        {
            Name = "I2DO20",
            Description = "接驳台正转"
        };
        /// <summary>
        ///  接驳台反转
        /// </summary>
        public static IoPoint I2DO21 = new IoPoint(m_DaskController, PCI7432, 21, IoModes.Signal)
        {
            Name = "I2DO21",
            Description = "接驳台反转"
        };
        /// <summary>
        ///   备用
        /// </summary>
        public static IoPoint I2DO22 = new IoPoint(m_DaskController, PCI7432, 22, IoModes.Signal)
        {
            Name = "I2DO22",
            Description = "备用"
        };
        /// <summary>
        ///   备用
        /// </summary>
        public static IoPoint I2DO23 = new IoPoint(m_DaskController, PCI7432, 23, IoModes.Signal)
        {
            Name = "I2DO23",
            Description = "备用"
        };
        /// <summary>
        ///   备用
        /// </summary>
        public static IoPoint I2DO24 = new IoPoint(m_DaskController, PCI7432, 24, IoModes.Signal)
        {
            Name = "I2DO24",
            Description = "备用"
        };
        /// <summary>
        ///  备用
        /// </summary>
        public static IoPoint I2DO25 = new IoPoint(m_DaskController, PCI7432, 25, IoModes.Signal)
        {
            Name = "I2DO25",
            Description = "备用"
        };
        /// <summary>
        ///   备用
        /// </summary>
        public static IoPoint I2DO26 = new IoPoint(m_DaskController, PCI7432, 26, IoModes.Signal)
        {
            Name = "I2DO26",
            Description = "备用"
        };
        /// <summary>
        ///   备用
        /// </summary>
        public static IoPoint I2DO27 = new IoPoint(m_DaskController, PCI7432, 27, IoModes.Signal)
        {
            Name = "I2DO27",
            Description = "备用"
        };
        /// <summary>
        ///  接驳台平移气缸电磁阀1
        /// </summary>
        public static IoPoint I2DO28 = new IoPoint(m_DaskController, PCI7432, 28, IoModes.Signal)
        {
            Name = "I2DO28",
            Description = "接驳台平移气缸电磁阀1"
        };
        /// <summary>
        ///  门禁电磁铁
        /// </summary>
        public static IoPoint I2DO29 = new IoPoint(m_DaskController, PCI7432, 29, IoModes.Signal)
        {
            Name = "I2DO29",
            Description = "门禁电磁铁"
        };
        /// <summary>
        ///   接驳台定位气缸
        /// </summary>
        public static IoPoint I2DO30 = new IoPoint(m_DaskController, PCI7432, 30, IoModes.Signal)
        {
            Name = "I2DO30",
            Description = "接驳台定位气缸"
        };
        /// <summary>
        ///   接驳台开夹气缸
        /// </summary>
        public static IoPoint I2DO31 = new IoPoint(m_DaskController, PCI7432, 31, IoModes.Signal)
        {
            Name = "I2DO31",
            Description = "接驳台开夹气缸"
        };
        #endregion
    }
}
