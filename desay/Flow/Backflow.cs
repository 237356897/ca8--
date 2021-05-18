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
using Motion.AdlinkAps;

namespace Desay
{
    //输送线程
    public class Backflow : StationPart
    {
        public Backflow(External ExternalSign, StationInitialize stationIni, StationOperate stationOpe)
                : base(ExternalSign, stationIni, stationOpe, typeof(Backflow)) { }

        private static ILog logger = LogManager.GetLogger(typeof(Backflow));

        private BackflowAlarm m_Alarm;

        Stopwatch strtime = new Stopwatch();
        /// <summary>
        /// 读码器
        /// </summary>
        public ISerialPortTriggerModel TrayCodeReader { get; set; }
        /// <summary>
        /// PLC
        /// </summary>
        public FrmManualPLC frmPlc { set; get; }
        /// <summary>
        /// 输送轴
        /// </summary>
        public StepAxis CarryAxis { set; get; }
        /// <summary>
        /// 三色灯
        /// </summary>
        public LayerLight Light { set; get; }
        /// <summary>
        /// 工作盘数(除NG盘外)
        /// </summary>
        int WorkTary = 0;

        /// <summary>
        /// 三位之一无盘，去取空盘
        /// </summary>
        /// <returns></returns>
        public int calTrayNum()
        {
            if (IoPoints.I2DI16.Value)  //OK位有料盘
            {
                WorkTary++;
            }
            if (IoPoints.I2DI18.Value)  //待料位1有料盘
            {
                WorkTary++;
            }
            if (IoPoints.I2DI21.Value)  //待料位2有料盘
            {
                WorkTary++;
            }
            if (Global.Instance.Stove1AnyMaterial) //固化炉1有料盘
            {
                WorkTary++;
            }
            if (Global.Instance.Stove2AnyMaterial) //固化炉2有料盘
            {
                WorkTary++;
            }
            if (Global.Instance.Stove3AnyMaterial) //固化炉3有料盘
            {
                WorkTary++;
            }
            if (Global.Instance.Stove4AnyMaterial) //固化炉4有料盘
            {
                WorkTary++;
            }
            if (Global.Instance.Stove5AnyMaterial) //固化炉5有料盘
            {
                WorkTary++;
            }
            if (Global.Instance.Stove6AnyMaterial) //固化炉6有料盘
            {
                WorkTary++;
            }
            if (WorkTary >= RunPara.Instance.RotatingDisk)
            {
                WorkTary = 0;
                return 0;
            }
            else if ((!IoPoints.I2DI16.Value || !IoPoints.I2DI18.Value || !IoPoints.I2DI21.Value) && !RunPara.Instance.TraySolidify) //三位(OK/待料位1/待料位2)之一无料盘
            {
                if (Marking.RobotStatus == Output.s20)
                {
                    Marking.RobotStatus = Output.s70;
                }
            }
            else if(!IoPoints.I2DI16.Value && RunPara.Instance.TraySolidify && Marking.ImmediatelyUpStove)
            {
                if (Marking.RobotStatus == Output.s20)
                {
                    Marking.RobotStatus = Output.s70;
                }
            }
            return 0;
        }

        /// <summary>
        /// 降温时间
        /// </summary>
        Stopwatch StopCoolingTime = new Stopwatch();
        /// <summary>
        /// 料盘扫描时间
        /// </summary>
        Stopwatch TrayCodeTime = new Stopwatch();
        public void cool()
        {
            IoPoints.I2DO18.Value = true; //等离子风扇开
            StopCoolingTime.Restart();
            do
            {
                Thread.Sleep(1000);
            } while (!IoPoints.I2DI24.Value && StopCoolingTime.ElapsedMilliseconds < RunPara.Instance.CoolingTime);
            IoPoints.I2DO18.Value = false; //等离子风扇关
        }

        /// <summary>
        /// 料盘已取走过
        /// </summary>
        bool ifgetout = false;
        /// <summary>
        /// 光栅感应轴停止
        /// </summary>
        bool isGratingStop = false;
        /// <summary>
        ///轴回原到降温位
        /// </summary>
        bool isHomeToCoolPos = false;
        /// <summary>
        /// 步近电机目标位置
        /// </summary>
        double StepAxisTargetPos = 0;
        public override void Running(RunningModes runningMode)
        {
            while (true)
            {
                Thread.Sleep(100);
                try
                {
                    #region 光栅
                    if (!IoPoints.I2DI07.Value && !CarryAxis.IsDone && !isHomeToCoolPos && !isGratingStop)
                    {
                        Light.SpeakImmediately = true;
                        StepAxisTargetPos = CarryAxis.TargetPos;
                        CarryAxis.Stop();
                        isGratingStop = true;
                    }
                    else if (stationOperate.Running && IoPoints.I2DI07.Value && isGratingStop)
                    {
                        Light.SpeakImmediately = false;
                        Thread.Sleep(1000);
                        CarryAxis.MoveTo(StepAxisTargetPos, AxisParameter.Instance.CarryvelocityCurve);
                        isGratingStop = false;
                    }
                    else if (stationInitialize.Running && IoPoints.I2DI07.Value && isGratingStop)
                    {
                        Light.SpeakImmediately = false;
                        Thread.Sleep(1000);
                        IoPoints.m_ApsController.SetAxisHomeConfig(CarryAxis.NoId, AxisParameter.Instance.CarryHomeParams);
                        IoPoints.m_ApsController.BackHome(CarryAxis.NoId);
                        isGratingStop = false;
                    }
                    #endregion

                    #region 自动化流程
                    if (stationOperate.Running && !isGratingStop)
                    {
                        calTrayNum(); //OK/待料位1/待料位2之一无料盘，则去取空盘

                        switch (Marking.RobotStatus)
                        {
                            case Output.s10://初始状态
                                if (CarryAxis.IsInPosition(RunPara.Instance.CarryAxisOrgPos))
                                {
                                    if (IoPoints.I2DI19.Value)
                                    {
                                        Marking.RobotStatus = Output.s30; //输送线上料位有料盘
                                    }
                                    else
                                    {
                                        Marking.RobotStatus = Output.s20; //输送线上料位无料盘
                                    }
                                }
                                else
                                {
                                    m_Alarm = BackflowAlarm.料盘位置异常;
                                    Marking.RobotStatus = Output.s10;
                                }
                                Marking.preRobotStatus = Output.s10;
                                coutnumb1 = 0;
                                break;
                            case Output.s20: //等待固化炉下盘
                                if (IoPoints.I2DI19.Value && Marking.RobotStatus == Output.s20)
                                {
                                    Thread.Sleep(100);
                                }
                                ifgetout = false;
                                Marking.preRobotStatus = Output.s20;
                                coutnumb1 = 0;
                                break;
                            case Output.s80: //上料位有料盘，等待机械手取走空盘
                                if (!IoPoints.I2DI19.Value && Marking.RobotStatus == Output.s80)
                                {
                                    if (IoPoints.I2DI16.Value && IoPoints.I2DI18.Value && IoPoints.I2DI21.Value)
                                    {
                                        Thread.Sleep(1000);
                                        Marking.RobotStatus = Output.s20;
                                    }
                                }
                                //robot取盘
                                Marking.preRobotStatus = Output.s80;
                                coutnumb1 = 0;
                                break;
                            case Output.s60://输送线返回原点位
                                if (Marking.preRobotStatus != Output.s60)
                                {
                                    CarryAxis.MoveTo(RunPara.Instance.CarryAxisOrgPos, AxisParameter.Instance.CarryvelocityCurve);
                                }
                                Marking.preRobotStatus = Output.s60;
                                if (CarryAxis.IsInPosition(RunPara.Instance.CarryAxisOrgPos))
                                {
                                    if (IoPoints.I2DI19.Value)
                                    {
                                        if (RunPara.Instance.cbAuto || RunPara.Instance.ShieldTrayCode || RunPara.Instance.TraySolidify)
                                        {
                                            RunPara.Instance.OrgTrayCode = "TrayNum" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString();
                                            Marking.RobotStatus = Output.s80;
                                        }
                                        else
                                        {
                                            Marking.RobotStatus = Output.s40; //扫料盘码                                            
                                        }
                                    }
                                    else
                                    {
                                        Marking.RobotStatus = Output.s20;
                                    }
                                }
                                Marking.preRobotStatus = Output.s60;
                                coutnumb1 = 0;
                                break;
                            case Output.s70: //回到放盘位通知放盘
                                if (Marking.preRobotStatus != Output.s70)
                                {
                                    CarryAxis.MoveTo(RunPara.Instance.CarryAxisMovePos, AxisParameter.Instance.CarryvelocityCurve);
                                }
                                if (CarryAxis.IsInPosition(RunPara.Instance.CarryAxisMovePos))
                                {
                                    if (IoPoints.I2DI20.Value) //输送线放好盘，停止报警
                                    {
                                        //停止蜂鸣
                                        Light.SpeakImmediately = false;
                                        //IoPoints.I2DO02.Value = true;
                                        //IoPoints.I2DO03.Value = false;
                                        if (IoPoints.I2DI25.Value)
                                        {
                                            Marking.RobotStatus = Output.s60;
                                        }
                                    }
                                    else                       //输送线无盘 开始报警
                                    {
                                        //蜂鸣
                                        Light.SpeakImmediately = true;
                                        //IoPoints.I2DO02.Value = false;
                                        //IoPoints.I2DO03.Value = true;
                                    }
                                }
                                Marking.preRobotStatus = Output.s70;
                                coutnumb1 = 0;
                                break;
                            case Output.s30: //移到 降温位，下料位
                                if (IoPoints.I2DI19.Value)  //机械手放好料盘
                                {
                                    Thread.Sleep(2000);
                                    if (Marking.preRobotStatus != Output.s30)
                                    {
                                        isHomeToCoolPos = true;
                                        Marking.preRobotStatus = Output.s30;
                                        CarryAxis.SetAxisHomeParam(AxisParameter.Instance.CarryHomeParams);
                                        CarryAxis.BackHome();
                                        //CarryAxis.MoveTo(RunPara.Instance.CarryAxisCoolingPos, AxisParameter.Instance.CarryvelocityCurve);
                                        cool();
                                        isHomeToCoolPos = false;
                                        CarryAxis.MoveTo(RunPara.Instance.CarryAxisMovePos, AxisParameter.Instance.CarryvelocityCurve);
                                    }
                                }
                                if (CarryAxis.IsInPosition(RunPara.Instance.CarryAxisMovePos))
                                {
                                    bool IsFullTray = IoPoints.I2DI16.Value && IoPoints.I2DI18.Value && IoPoints.I2DI21.Value;
                                    if ((!IoPoints.I2DI20.Value) || (RunPara.Instance.cbAuto && !IsFullTray))
                                    {
                                        ifgetout = true;         // 盘已取走 或 空跑时料盘不满
                                    }
                                    if (IoPoints.I2DI20.Value && !ifgetout)         //有盘且未取过
                                    {
                                        //蜂鸣
                                        Light.SpeakImmediately = true;
                                        //IoPoints.I2DO02.Value = false;
                                        //IoPoints.I2DO03.Value = true; 
                                    }
                                    else if (IoPoints.I2DI20.Value && IsFullTray)    //有盘且满料盘
                                    {
                                        //蜂鸣
                                        Light.SpeakImmediately = true;
                                        //IoPoints.I2DO02.Value = false;
                                        //IoPoints.I2DO03.Value = true; 
                                    }
                                    else if (!IoPoints.I2DI20.Value && !IsFullTray && !RunPara.Instance.TraySolidify)  //无盘且不满料盘
                                    {
                                        //蜂鸣
                                        Light.SpeakImmediately = true;
                                        //IoPoints.I2DO02.Value = false;
                                        //IoPoints.I2DO03.Value = true; 
                                    }
                                    else
                                    {
                                        //停止蜂鸣
                                        Light.SpeakImmediately = false;
                                        //IoPoints.I2DO02.Value = true;
                                        //IoPoints.I2DO03.Value = false; 
                                    }
                                    if ((IoPoints.I2DI25.Value && ifgetout && !Light.SpeakImmediately) || (RunPara.Instance.cbAuto && !IsFullTray))
                                    {
                                        Marking.RobotStatus = Output.s60;
                                    }
                                }
                                else if (CarryAxis.IsInPosition(RunPara.Instance.CarryAxisOrgPos) && IoPoints.I2DI19.Value)
                                {
                                    coutnumb1++;
                                    Thread.Sleep(300);

                                    if (coutnumb1 > 80)
                                    {
                                        coutnumb1 = 0;
                                        Marking.preRobotStatus = Output.s10;
                                    }
                                }
                                break;
                            case Output.s40: //触发扫描料盘码
                                if (IoPoints.I2DI19.Value && CarryAxis.IsInPosition(RunPara.Instance.CarryAxisOrgPos))
                                {
                                    if (Marking.preRobotStatus != Output.s40)
                                    {
                                        Marking.preRobotStatus = Output.s40;
                                        TrayCodeReader.receiveFinish = false;
                                        TrayCodeReader.Trigger(new TriggerArgs()
                                        {
                                            sender = this,
                                            tryTimes = 1,
                                            message = "FN\r\n"
                                        });
                                        TrayCodeTime.Restart();
                                    }
                                    if (TrayCodeReader.receiveFinish && RunPara.Instance.OrgTrayCode != string.Empty) //此处增加条码判断
                                    {
                                        Marking.RobotStatus = Output.s80;
                                    }
                                    else if (TrayCodeReader.receiveFinish)
                                    {
                                        if (TrayCodeTime.ElapsedMilliseconds < RunPara.Instance.TrayCodeDelayTime)
                                        {
                                            Marking.preRobotStatus = Output.s60; //重新扫描
                                        }
                                        else
                                        {
                                            m_Alarm = BackflowAlarm.料盘扫码NG;   //超时报警
                                        }
                                    }
                                    else
                                    {
                                        if (TrayCodeTime.ElapsedMilliseconds > RunPara.Instance.TrayCodeDelayTime)
                                        {
                                            m_Alarm = BackflowAlarm.料盘扫码NG;   //超时报警
                                        }
                                    }
                                }
                                break;
                            case Output.s50: //等待机械手炉内取料
                                if (!IoPoints.I2DI19.Value && Marking.RobotStatus == Output.s50)
                                {
                                    Thread.Sleep(500);
                                    if (alarmch > 300)
                                    {
                                        alarmch = 0;
                                        Marking.RobotStatus = Output.s20;
                                    }
                                }
                                Marking.preRobotStatus = Output.s50;
                                break;
                            default:
                                //  MessageBox.Show("异常位置0001");
                                break;
                        }
                    }

                    #endregion

                    #region 初始化流程

                    if (stationInitialize.Running && !isGratingStop)
                    {
                        switch (stationInitialize.Flow)
                        {
                            case 0:
                                AppendText("输送轴初始化中...");
                                stationInitialize.InitializeDone = false;
                                stationOperate.RunningSign = false;
                                //Marking.BackflowInform = false;
                                CarryAxis.Stop();
                                stationInitialize.Flow = 10;
                                break;
                            case 10://输送线无报警励磁
                                if (!CarryAxis.IsAlarmed)
                                {
                                    CarryAxis.IsServon = true;
                                    stationInitialize.Flow = 20;
                                }
                                break;
                            case 20://输送线设置回原参数并回原
                                IoPoints.m_ApsController.SetAxisHomeConfig(CarryAxis.NoId, AxisParameter.Instance.CarryHomeParams);
                                IoPoints.m_ApsController.BackHome(CarryAxis.NoId);
                                strtime.Restart();
                                stationInitialize.Flow = 30;
                                break;
                            case 30://输送线移到原点位
                                if (IoPoints.m_ApsController.CheckHomeDone(CarryAxis.NoId) == 0)
                                {
                                    CarryAxis.MoveTo(RunPara.Instance.CarryAxisOrgPos, AxisParameter.Instance.CarryvelocityCurve);
                                    stationInitialize.Flow = 40;
                                }
                                else if (strtime.ElapsedMilliseconds / 1000.0 > 120.0)
                                {
                                    APS168.APS_emg_stop(CarryAxis.NoId);
                                    m_Alarm = BackflowAlarm.输送轴复位超时;
                                }
                                break;
                            case 40://初始化完成
                                if (CarryAxis.IsInPosition(RunPara.Instance.CarryAxisOrgPos))
                                {
                                    stationInitialize.InitializeDone = true;
                                    stationInitialize.Flow = 50;
                                    AppendText("输送轴初始化完成！");
                                    Marking.RobotStatus = Output.s10;
                                    Marking.preRobotStatus = Output.s10;
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
                        m_Alarm = BackflowAlarm.无消息;
                        if (Marking.RobotStatus == Output.s40)
                        {
                            Marking.preRobotStatus = Output.s60;
                            TrayCodeReader.receiveFinish = false;
                            TrayCodeTime.Restart();
                        }
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        int alarmch = 0;
        int coutnumb1 = 0;
        public void MesDataSave(string strSN, string A2C, StoveData stoveTime, string scantimespan, StoveData stoveTemperature, StoveData stoveNo)
        {
            try
            {
                bool isOk = true;
                string SN = $"{A2C}{0}{strSN.Substring(strSN.Length - 6)}";

                if (stoveTime.min > stoveTime.data || stoveTime.data > stoveTime.max)
                {
                    isOk = isOk && false;
                }
                if (stoveTemperature.min > stoveTemperature.data || stoveTemperature.data > stoveTemperature.max)
                {
                    isOk = isOk && false;
                }
                if (isOk)
                {
                    RunPara.Instance.OkNumber++;
                }
                else
                {
                    RunPara.Instance.NgNumber++;
                }

                Common.Test_WriteMesTxtAndCsvFile(SN, "", scantimespan, isOk, stoveTime, stoveTemperature, stoveNo);
            }
            catch
            {
                LogHelper.Error("Test_WriteMesTxtAndCsvFile Error");
            }
        }

        /// <summary>
        /// 流程报警集合
        /// </summary>
        protected override IList<Alarm> alarms()
        {
            var list = new List<Alarm>();

            list.AddRange(CarryAxis.Alarms);

            list.Add(new Alarm(() => m_Alarm == BackflowAlarm.初始化故障)
            {
                AlarmLevel = AlarmLevels.Error,
                Name = BackflowAlarm.初始化故障.ToString()
            });
            list.Add(new Alarm(() => m_Alarm == BackflowAlarm.料盘扫码NG)
            {
                AlarmLevel = AlarmLevels.Error,
                Name = BackflowAlarm.料盘扫码NG.ToString()
            });
            list.Add(new Alarm(() => m_Alarm == BackflowAlarm.料盘位置异常)
            {
                AlarmLevel = AlarmLevels.Error,
                Name = BackflowAlarm.料盘位置异常.ToString()
            });
            list.Add(new Alarm(() => m_Alarm == BackflowAlarm.输送轴复位超时)
            {
                AlarmLevel = AlarmLevels.Error,
                Name = BackflowAlarm.输送轴复位超时.ToString()
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

            return list;
        }


        public enum BackflowAlarm : int
        {
            无消息,
            初始化故障,
            料盘扫码NG,
            料盘位置异常,
            输送轴复位超时
        }
    }

    public enum Output
    {
        s10,
        /// <summary>
        /// 原点位等待放盘
        /// </summary>
        s20,
        /// <summary>
        /// 输送线送料盘
        /// </summary>
        s30,
        /// <summary>
        /// 等待料盘扫码
        /// </summary>
        s40,
        /// <summary>
        /// 等待
        /// </summary>
        s50,
        /// <summary>
        /// 返回原点位
        /// </summary>
        s60,
        /// <summary>
        /// 输送线取空盘
        /// </summary>
        s70,
        /// <summary>
        /// 原点位等待取盘
        /// </summary>
        s80

    }
}

