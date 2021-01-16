using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using Motion.Enginee;
using Motion.Interfaces;
using System.Toolkit;
using log4net;
using System.Device;

namespace Desay
{
    //接驳台线程
    public class Connection : StationPart
    {
        public Connection(External ExternalSign, StationInitialize stationIni, StationOperate stationOpe)
                : base(ExternalSign, stationIni, stationOpe, typeof(Connection)) { }

        private static ILog logger = LogManager.GetLogger(typeof(Connection));

        private ConnectionAlarm m_Alarm;

        /// <summary>
        /// 接驳台定位气缸
        /// </summary>
        public SingleCylinder PositioningCylinder { set; get; }

        /// <summary>
        /// 接驳台开夹气缸
        /// </summary>
        public SingleCylinder OpenClampCylinder { set; get; }

        /// <summary>
        /// 接驳台平移气缸
        /// </summary>
        public DoubleCylinder TranslationCylinder { set; get; }

        public static int Step = 0;             //步骤

        public override void Running(RunningModes runningMode)
        {

            //xmz 增加 标志
            bool HaveBackAA = false;
            while (true)
            {
                Thread.Sleep(100);

                try
                {
                    #region 自动流程
                    if (HaveBackAA && !IoPoints.T1DI07.Value)
                    {
                        HaveBackAA = false;
                    }
                    if (stationOperate.Running)
                    {
                        switch (Step)
                        {
                            case 0: //接驳台有治具回流,平移气缸复位
                                if (IoPoints.I2DI27.Value)
                                {
                                    Marking.AAOK = IoPoints.T1DI08.Value;
                                    Marking.AANG = IoPoints.T1DI09.Value;
                                    Thread.Sleep(1300);
                                    //通知AA有料
                                    IoPoints.T1DO00.Value = false;
                                    Step = 60;
                                }
                                else
                                {
                                    TranslationCylinder.Reset();
                                    Step = 10;
                                    _watch.Restart();
                                }
                                break;
                            case 10://接驳台电机正转，通知AA无料
                                if (TranslationCylinder.OutOriginStatus)
                                {
                                    IoPoints.I2DO20.Value = true;
                                    IoPoints.I2DO21.Value = false;
                                    //通知AA无料
                                    IoPoints.T1DO00.Value = true;
                                    Step = 30;
                                }
                                if (_watch.ElapsedMilliseconds > 12000 && Marking.AlarmStopThread)
                                {
                                    _watch.Restart();
                                    Marking.AlarmStopThread = false;
                                    MessageBox.Show("接驳台原点信号异常,请按暂停再启动");
                                    AppendText("接驳台原点信号异常");
                                }
                                break;
                            case 30://治具到位感应,定位气缸顶起
                                if (IoPoints.I2DI27.Value)
                                {
                                    IoPoints.I2DO20.Value = false;
                                    IoPoints.I2DO21.Value = false;
                                    Marking.AAOK = IoPoints.T1DI08.Value;
                                    Marking.AANG = IoPoints.T1DI09.Value;
                                    Thread.Sleep(1300);
                                    IoPoints.T1DO00.Value = false;//通知AA有料
                                    PositioningCylinder.Set();
                                    _watch.Restart();
                                    Step = 40;
                                }
                                break;
                            case 40://开夹气缸打开
                                if (PositioningCylinder.OutMoveStatus)
                                {
                                    OpenClampCylinder.Set();
                                    Step = 50;
                                }
                                if (_watch.ElapsedMilliseconds > 10000 && Marking.AlarmStopThread)
                                {
                                    Marking.AlarmStopThread = false;
                                    MessageBox.Show("定位气缸动点异常,请按暂停再启动");
                                    _watch.Restart();
                                    AppendText("开夹定位气缸动点异常");
                                }
                                break;
                            case 50://通知跑取料流程
                                if (OpenClampCylinder.OutMoveStatus)
                                {
                                    IoPoints.I2DO20.Value = false;
                                    IoPoints.I2DO21.Value = false;
                                    Marking.ConnectionInform = true;//通知机械手取料
                                    Step = 60;
                                }
                                if (_watch.ElapsedMilliseconds > 12000 && Marking.AlarmStopThread)
                                {
                                    Marking.AlarmStopThread = false;
                                    MessageBox.Show("开夹气缸动点异常,请按暂停再启动");
                                    _watch.Restart();
                                    AppendText("开夹气缸动点异常");
                                }
                                break;
                            case 60://取料完成，平移气缸到动点
                                if (!Marking.ConnectionInform)
                                {
                                    IoPoints.T1DO00.Value = false;
                                    TranslationCylinder.Set();
                                    _watch.Restart();
                                    Step = 70;
                                }
                                break;
                            case 70://收到AA Ready信号，开夹气缸复位
                                if (TranslationCylinder.OutMoveStatus && (IoPoints.T1DI07.Value || RunPara.Instance.ShieldAAReady))
                                {
                                    OpenClampCylinder.Reset();
                                    Step = 80;
                                    _watch.Restart();
                                }
                                if (_watch.ElapsedMilliseconds > 12000 && Marking.AlarmStopThread)
                                {

                                    if (!TranslationCylinder.OutMoveStatus)
                                    {
                                        _watch.Restart();
                                        Marking.AlarmStopThread = false;
                                        MessageBox.Show("接驳台平移气缸动点异常,请按暂停 再启动");
                                        AppendText("接驳台平移气缸动点异常");
                                    }
                                    //  m_Alarm = ConnectionAlarm.接驳台平移气缸动点异常;
                                }

                                //if (!IoPoints.T1DI07.Value && _watch.ElapsedMilliseconds > 35000 && Marking.AlarmStopThread)
                                //{
                                //    _watch.Restart();
                                //    Marking.AlarmStopThread = false;
                                //    MessageBox.Show("接受不到AAready,请确认前面设备正常，可关闭软件再启动");                           
                                //}

                                break;
                            case 80://定位气缸复位
                                if (OpenClampCylinder.OutOriginStatus)
                                {
                                    PositioningCylinder.Reset();
                                    Step = 90;
                                    _watch.Restart();
                                }
                                if (_watch.ElapsedMilliseconds > 12000 && Marking.AlarmStopThread)
                                {
                                    Marking.AlarmStopThread = false;
                                    MessageBox.Show("开夹气缸原点异常,请按暂停再启动");
                                    AppendText("接驳台开夹气缸原点异常");
                                }
                                break;
                            case 90://接驳台电机反转
                                if (PositioningCylinder.OutOriginStatus)
                                {
                                    IoPoints.I2DO20.Value = false;
                                    IoPoints.I2DO21.Value = true;
                                    HaveBackAA = true;
                                    Step = 100;
                                }
                                if (_watch.ElapsedMilliseconds > 12000 && Marking.AlarmStopThread)
                                {
                                    Marking.AlarmStopThread = false;
                                    MessageBox.Show("定位气缸原点异常,请按暂停再启动");
                                    _watch.Restart();
                                    Marking.AlarmStopThread = false;
                                    AppendText("接驳台定位气缸原点异常");
                                }
                                break;
                            case 100://xmz  增加保持变量标志
                                if (!HaveBackAA || !IoPoints.T1DI07.Value || RunPara.Instance.ShieldAAReady)
                                {
                                    IoPoints.I2DO20.Value = false;
                                    IoPoints.I2DO21.Value = false;
                                    Step = 0;
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    #endregion

                    #region 初始化流程

                    if (stationInitialize.Running)
                    {
                        switch (stationInitialize.Flow)
                        {
                            case 0:
                                stationInitialize.InitializeDone = false;
                                stationOperate.RunningSign = false;
                                Step = 0;
                                HaveBackAA = false;
                                Marking.ConnectionInform = false;
                                IoPoints.I2DO20.Value = false;
                                IoPoints.I2DO21.Value = false;
                                PositioningCylinder.InitExecute();
                                PositioningCylinder.Reset();
                                OpenClampCylinder.InitExecute();
                                OpenClampCylinder.Reset();
                                TranslationCylinder.InitExecute();
                                TranslationCylinder.Reset();
                                stationInitialize.Flow = 10;
                                break;
                            case 10:
                                if (PositioningCylinder.OutOriginStatus && OpenClampCylinder.OutOriginStatus && TranslationCylinder.OutOriginStatus)
                                {
                                    stationInitialize.InitializeDone = true;
                                    stationInitialize.Flow = 20;
                                }
                                if (_watch.ElapsedMilliseconds > 20000)
                                {
                                    if (!PositioningCylinder.OutOriginStatus)
                                    {
                                        MessageBox.Show("接驳台定位气缸不在原点");
                                    }
                                    if (!OpenClampCylinder.OutOriginStatus)
                                    {
                                        MessageBox.Show("接驳台开夹气缸不在原点");
                                    }
                                    if (!TranslationCylinder.OutOriginStatus)
                                    {
                                        MessageBox.Show("接驳台平移气缸不在原点");
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    #endregion

                    #region 报警复位

                    if (AlarmReset.AlarmReset)
                    {
                        m_Alarm = ConnectionAlarm.无消息;
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        Stopwatch _watch = new Stopwatch();
        /// <summary>
        /// 流程报警集合
        /// </summary>
        protected override IList<Alarm> alarms()
        {
            var list = new List<Alarm>();

            list.AddRange(PositioningCylinder.Alarms);
            list.AddRange(OpenClampCylinder.Alarms);
            list.AddRange(TranslationCylinder.Alarms);

            list.Add(new Alarm(() => m_Alarm == ConnectionAlarm.初始化故障)
            {
                AlarmLevel = AlarmLevels.Error,
                Name = ConnectionAlarm.初始化故障.ToString()
            });

            return list;
        }

        /// <summary>
        /// 气缸状态集合
        /// </summary>
        protected override IList<ICylinderStatusJugger> cylinderStatus()
        {
            var list = new List<ICylinderStatusJugger>();

            //要添加气缸
            list.Add(PositioningCylinder);
            list.Add(OpenClampCylinder);
            list.Add(TranslationCylinder);

            return list;
        }


        public enum ConnectionAlarm : int
        {
            无消息,
            初始化故障,
            接驳台开夹气缸原点异常,
            接驳台开夹气缸动点异常,
            接驳台定位气缸原点异常,
            接驳台定位气缸动点异常,
            接驳台平移气缸动点异常,
            接驳台平移气缸原点异常
        }
    }
}
