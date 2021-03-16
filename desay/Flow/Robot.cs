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
using System.IO;

namespace Desay
{
    //机器人线程
    public class Robot : StationPart
    {
        public Robot(External ExternalSign, StationInitialize stationIni, StationOperate stationOpe)
                : base(ExternalSign, stationIni, stationOpe, typeof(Robot)) { }

        private static ILog logger = LogManager.GetLogger(typeof(Robot));

        public event RefreshDataCompleteEventHandler RefreshDataDataGridView;

        private RobotAlarm m_Alarm;

        /// <summary>
        /// PLC
        /// </summary>
        public FrmManualPLC frmPlc { set; get; }

        /// <summary>
        /// 机器人
        /// </summary>
        public AsynTcpClient mAsynTcpRobot { set; get; }

        /// <summary>
        /// 读码器
        /// </summary>
        public ISerialPortTriggerModel QRCodeReader { get; set; }

        /// <summary>
        /// 数据上传MES
        /// </summary>
        public void UpMes()
        {
            //mes  1007
            try
            {
                //   TimeSpan stovetimeall = RunPara.Instance.OutTary.finishtime - RunPara.Instance.OutTary.starttime;
                //if (RunPara.Instance.OutTary.timeStove < 2820)
                //{
                //    RunPara.Instance.OutTary.timeStove = RunPara.Instance.StoveCuringTime[1]

                //  //   MessageBox.Show("炉号："+RunPara.Instance.OutTary.QRCode[0].MesStoveNoX.data.ToString()+"固化时间异常，请停机处理。");
                //}

                for (int i = 0; i < RunPara.Instance.OutTary.QRCode.Length; i++)
                {
                    //RunPara.Instance.OutTary.QRCode[i].sign = true;
                    if (RunPara.Instance.OutTary.QRCode[i].sign)
                    {
                        // RunPara.Instance.OutTary.QRCode[i].SN = "12345678";
                        if (RunPara.Instance.OutTary.QRCode[i].SN != string.Empty)
                        {
                            RunPara.Instance.OutTary.QRCode[i].endtime = DateTime.Now;
                            TimeSpan SP = RunPara.Instance.OutTary.QRCode[i].endtime - RunPara.Instance.OutTary.QRCode[i].scantime;
                            if (RunPara.Instance.OutTary.QRCode[i].A2C.Count() < 4)
                            {
                                RunPara.Instance.OutTary.QRCode[i].A2C = RunPara.Instance.A2C;
                            }
                            //  if (RunPara.Instance.OutTary.QRCode[i].MesStoveTimeX.data<2820)
                            //      RunPara.Instance.OutTary.QRCode[i].MesStoveTimeX.data = RunPara.Instance.OutTary.timeStove;

                            if (SP.TotalSeconds < 20000)
                            {
                                MesDataSave(RunPara.Instance.OutTary.QRCode[i].SN, RunPara.Instance.OutTary.QRCode[i].A2C, RunPara.Instance.OutTary.QRCode[i].MesStoveTimeX, ((int)SP.TotalSeconds).ToString(),
                                                                                      RunPara.Instance.OutTary.QRCode[i].MesStoveTemperatureX, RunPara.Instance.OutTary.QRCode[i].MesStoveNoX);
                            }
                            else
                            {
                                MesDataSave(RunPara.Instance.OutTary.QRCode[i].SN, RunPara.Instance.OutTary.QRCode[i].A2C, RunPara.Instance.OutTary.QRCode[i].MesStoveTimeX, (3210).ToString(),
                                                                                      RunPara.Instance.OutTary.QRCode[i].MesStoveTemperatureX, RunPara.Instance.OutTary.QRCode[i].MesStoveNoX);
                            }
                        }
                        else
                        {
                            MessageBox.Show("上传MES：SN为空");
                        }
                    }
                    else
                    {

                    }
                    RunPara.Instance.OutTary.QRCode[i].SN = string.Empty;
                    RunPara.Instance.OutTary.QRCode[i].sign = false;
                    RunPara.Instance.OutTary.QRCode[i].dgvRows = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("存MES错误：" + ex.ToString());
            }
        }

        /// <summary>
        /// 空跑模式
        /// </summary>
        public static bool ifemptyrun = false;
        public static int Step = 0;
        public static int WhileTimes = 0;

        public override void Running(RunningModes runningMode)
        {
            string[] tcpResults = null;                    //机器人通信结果
            string TcpRobotCmd = string.Empty;             //PC给机器人指令
            bool WishoutDiskWait = true;                  //无盘等待
            bool FirstIn = true;

            Stopwatch QRCodeTime = new Stopwatch();        //扫码计时
            Stopwatch AwaitProductTime = new Stopwatch();  //等待产品时间
            Stopwatch WishoutDiskTime = new Stopwatch();   //无盘时间
            Stopwatch _watch = new Stopwatch();
            QRCodeTime.Start();
            AwaitProductTime.Start();
            WishoutDiskTime.Start();
            _watch.Start();
            WhileTimes = 0;

            while (true)
            {
                Thread.Sleep(100);
                try
                {
                    #region 自动流程

                    if (stationOperate.Running && Marking.AlarmStopThread)
                    {
                        if (FirstIn)
                        {
                            frmPlc.WriteM(frmPlc.StoveLockControl, true);
                            FirstIn = false;
                        }
                        if (WhileTimes % 4 == 0)
                        {
                            PLCSignal();
                            WhileTimes = 1;
                        }
                        else
                        {
                            WhileTimes++;
                        }

                        switch (Step)
                        {
                            #region 空盘调度(节点0~100)
                            case 0://检查工位盘状态
                                bool ifull = false;
                                if (Marking.RobotStatus == Output.s80)
                                {
                                    if (IoPoints.I2DI16.Value && IoPoints.I2DI18.Value && IoPoints.I2DI21.Value)  //OK,待料1和待料2都有料盘
                                    {
                                        ifull = true;
                                        Marking.RobotStatus = Output.s20;
                                    }

                                    //m_Alarm = RobotAlarm.连续3个NG报警;
                                    //MessageBox.Show("请取走多余盘");
                                }

                                if (Marking.RobotStatus == Output.s80 && !ifull)
                                {
                                    if (IoPoints.I2DI19.Value)//通知机械手——输送线取盘
                                    {
                                        AppendText("机械手——输送轴取盘");
                                        mAsynTcpRobot.AsynSend("2linpk");
                                        Step = 10;
                                    }
                                    else
                                    {
                                        Step = 50;
                                    }
                                }
                                else
                                {
                                    Step = 50;
                                }
                                break;

                            case 10://检查待料位
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("2linpk"))
                                {
                                    if (!IoPoints.I2DI16.Value)    //OK位无料盘，通知机械手——OK位放盘
                                    {
                                        AppendText("机械手——OK位放盘");
                                        mAsynTcpRobot.AsynSend("1plapt");
                                        Step = 20;
                                    }
                                    else if (!IoPoints.I2DI18.Value) //待料位1无料盘，通知机械手——待料位1放盘
                                    {
                                        AppendText("机械手——待料位1放盘");
                                        mAsynTcpRobot.AsynSend("4plapt");
                                        Step = 30;
                                    }
                                    else if (!IoPoints.I2DI21.Value) //待料位2无料盘，通知机械手——待料位2放盘
                                    {
                                        AppendText("机械手——待料位2放盘");
                                        mAsynTcpRobot.AsynSend("3plapt");
                                        Step = 40;
                                    }
                                    else
                                    {
                                        m_Alarm = RobotAlarm.待料位满盘报警;
                                    }
                                }
                                break;
                            case 20://OK位放盘完成
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("1plapt"))
                                {
                                    Marking.RobotStatus = Output.s20;
                                    if (RunPara.Instance.OrgTrayCode != string.Empty)
                                    {
                                        RunPara.Instance.OKTary.TrayCode = RunPara.Instance.OrgTrayCode;
                                        RunPara.Instance.OrgTrayCode = string.Empty;
                                        Step = 50;
                                    }
                                    else
                                    {
                                        m_Alarm = RobotAlarm.料盘条码丢失;
                                    }
                                    // Marking.BackflowInform = false;                                   
                                }
                                break;
                            case 30://待料位1放盘完成
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("4plapt"))
                                {
                                    Marking.RobotStatus = Output.s20;
                                    if (RunPara.Instance.OrgTrayCode != string.Empty)
                                    {
                                        RunPara.Instance.WaitTary1.TrayCode = RunPara.Instance.OrgTrayCode;
                                        RunPara.Instance.OrgTrayCode = string.Empty;
                                        Step = 50;
                                    }
                                    else
                                    {
                                        m_Alarm = RobotAlarm.料盘条码丢失;
                                    }
                                    // Marking.BackflowInform = false;                                    
                                }
                                break;
                            case 40://待料位2放盘完成
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("3plapt"))
                                {
                                    Marking.RobotStatus = Output.s20;
                                    if (RunPara.Instance.OrgTrayCode != string.Empty)
                                    {
                                        RunPara.Instance.WaitTary2.TrayCode = RunPara.Instance.OrgTrayCode;
                                        RunPara.Instance.OrgTrayCode = string.Empty;
                                        Step = 50;
                                    }
                                    else
                                    {
                                        m_Alarm = RobotAlarm.料盘条码丢失;
                                    }
                                    //  Marking.BackflowInform = false;
                                }
                                break;
                            case 50:
                                if (Marking.DownStoveRefreshState[3] && (Marking.RobotStatus == Output.s20) && !RunPara.Instance.Stove4Shield)
                                {
                                    if (!Marking.RobotInformOutputting)
                                    {
                                        Marking.RobotInformOutputting = true;
                                        //  Marking.RobotInform = true;
                                        Marking.RobotPutAway = true;
                                    }
                                    Step = 400;
                                }
                                else if (Marking.DownStoveRefreshState[0] && (Marking.RobotStatus == Output.s20) && !RunPara.Instance.Stove1Shield)
                                {
                                    if (!Marking.RobotInformOutputting)
                                    {
                                        Marking.RobotInformOutputting = true;
                                        //  Marking.RobotInform = true;
                                        Marking.RobotPutAway = true;
                                    }
                                    Step = 100;
                                }
                                else if (Marking.DownStoveRefreshState[1] && (Marking.RobotStatus == Output.s20) && !RunPara.Instance.Stove2Shield)
                                {
                                    if (!Marking.RobotInformOutputting)
                                    {
                                        Marking.RobotInformOutputting = true;
                                        //  Marking.RobotInform = true;
                                        Marking.RobotPutAway = true;
                                    }
                                    Step = 200;
                                }
                                else if (Marking.DownStoveRefreshState[2] && (Marking.RobotStatus == Output.s20) && !RunPara.Instance.Stove3Shield)
                                {
                                    if (!Marking.RobotInformOutputting)
                                    {
                                        Marking.RobotInformOutputting = true;
                                        //   Marking.RobotInform = true;
                                        Marking.RobotPutAway = true;
                                    }
                                    Step = 300;
                                }
                                else if (Marking.DownStoveRefreshState[4] && (Marking.RobotStatus == Output.s20) && !RunPara.Instance.Stove5Shield)
                                {
                                    if (!Marking.RobotInformOutputting)
                                    {
                                        Marking.RobotInformOutputting = true;
                                        //   Marking.RobotInform = true;
                                        Marking.RobotPutAway = true;
                                    }
                                    Step = 500;
                                }
                                else if (Marking.DownStoveRefreshState[5] && (Marking.RobotStatus == Output.s20) && !RunPara.Instance.Stove6Shield)
                                {
                                    if (!Marking.RobotInformOutputting)
                                    {
                                        Marking.RobotInformOutputting = true;
                                        //  Marking.RobotInform = true;
                                        Marking.RobotPutAway = true;
                                    }
                                    Step = 600;
                                }
                                else
                                {
                                    Step = 700;
                                }
                                break;
                            #endregion
                            #region 下炉1(节点100~200)
                            case 100:
                                if (Marking.RobotStatus == Output.s20)
                                {
                                    AppendText("机械手——移至炉1取盘位");
                                    mAsynTcpRobot.AsynSend("1boxpw");   //通知机械手——移至炉1上方
                                    Marking.RobotInformOutputting = false;
                                    Marking.RobotStatus = Output.s50;
                                    Step = 110;
                                }
                                else
                                {
                                    Step = 700;
                                }
                                break;
                            case 110:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("1boxpw"))
                                {
                                    AppendText("固化炉——炉1开门");
                                    frmPlc.WriteM(frmPlc.Stove1DoorTrip, true); //炉1开门
                                    //Thread.Sleep(4000);
                                    try
                                    {
                                        //清除料盘信息
                                        for (int i = 0; i < RunPara.Instance.Stove[0].Tray.QRCode.Length; i++)
                                        {
                                            if (RunPara.Instance.Stove[0].Tray.QRCode[i].sign)
                                            {
                                                ////存Mes
                                                RunPara.Instance.MesStoveTemperature.data = RunPara.Instance.AverageTemperature[0];
                                                RunPara.Instance.MesStoveNo.data = RunPara.Instance.Stove[0].StoveNo;
                                                RunPara.Instance.OutTary.TrayCode = RunPara.Instance.Stove[0].Tray.TrayCode;
                                                //***

                                                RunPara.Instance.OutTary.QRCode[i].SN = RunPara.Instance.Stove[0].Tray.QRCode[i].SN;
                                                RunPara.Instance.OutTary.QRCode[i].sign = RunPara.Instance.Stove[0].Tray.QRCode[i].sign;
                                                RunPara.Instance.OutTary.QRCode[i].dgvRows = RunPara.Instance.Stove[0].Tray.QRCode[i].dgvRows;
                                                RunPara.Instance.OutTary.QRCode[i].scantime = RunPara.Instance.Stove[0].Tray.QRCode[i].scantime;

                                                RunPara.Instance.OutTary.QRCode[i].endtime = new DateTime();//1007
                                                RunPara.Instance.OutTary.QRCode[i].timespanScan = 0;//1007
                                                RunPara.Instance.OutTary.QRCode[i].A2C = RunPara.Instance.Stove[0].Tray.QRCode[i].A2C;//1007
                                                RunPara.Instance.OutTary.productInfo = RunPara.Instance.Stove[0].Tray.productInfo;//1007

                                                //RunPara.Instance.OutTary.QRCode[i].timespanPLC = RunPara.Instance.StoveCuringTime[0];

                                                RunPara.Instance.OutTary.QRCode[i].MesStoveTimeX = RunPara.Instance.MesStoveTime;
                                                RunPara.Instance.OutTary.QRCode[i].MesStoveTimeX.data = frmPlc.ReadInt16Data(frmPlc.Stove1CuringTime);//1017
                                                RunPara.Instance.OutTary.QRCode[i].MesStoveNoX = RunPara.Instance.MesStoveNo;
                                                RunPara.Instance.OutTary.QRCode[i].MesStoveTemperatureX = RunPara.Instance.MesStoveTemperature;

                                                //***
                                            }

                                            RunPara.Instance.Stove[0].Tray.QRCode[i].SN = string.Empty;
                                            RunPara.Instance.Stove[0].Tray.QRCode[i].sign = false;
                                            RunPara.Instance.Stove[0].Tray.QRCode[i].dgvRows = -1;
                                        }

                                        //  RunPara.Instance.OutTary.starttime = RunPara.Instance.Stove[0].Tray.starttime;
                                        //  RunPara.Instance.OutTary.finishtime = DateTime.Now;

                                        RunPara.Instance.Stove[0].ifcaltime = false;
                                        RunPara.Instance.Stove[0].StoveNo = 0;
                                        RunPara.Instance.Stove[0].AnyMaterialTary = false;
                                        RunPara.Instance.Stove[0].Tray.ProductPos = 0;
                                        RunPara.Instance.OutTary.timeStove = RunPara.Instance.Stove[0].timeStove;
                                        RunPara.Instance.OutTary.timeStove = RunPara.Instance.Stove[0].Tray.timeStove;

                                        Marking.StartTemperatureSign[0] = false;
                                        Marking.StartTemperatureSign[0] = true;
                                        Marking.StopCuringTimeSign[0] = true;
                                        RefreshDataDataGridView?.Invoke(this, RefreshDataGridView.DownStove);

                                        Marking.RobotPutAway = false;
                                        Marking.DownStoveRefreshState[0] = false;
                                        Marking.RobotStatus = Output.s30;

                                        UpMes();
                                    }
                                    catch
                                    {
                                    }

                                    Step = 120;
                                }
                                break;
                            case 120:

                                if (frmPlc.ReadM(frmPlc.Stove1DoorFront))//判断门已打开
                                {
                                    AppendText("机械手——炉1取盘");
                                    mAsynTcpRobot.AsynSend("1boxpk"); //通知机械手——炉1取盘
                                    Step = 130;
                                }
                                //else
                                //{
                                //    frmPlc.WriteM(frmPlc.Stove1DoorTrip, true); //炉1开门
                                //    Thread.Sleep(4000);
                                //}

                                break;
                            case 130:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("1boxpk"))
                                {
                                    AppendText("固化炉——炉1关门");
                                    frmPlc.WriteM(frmPlc.Stove1DoorTrip, false); //炉1关门

                                    //Thread.Sleep(4000);
                                    Step = 140;
                                }

                                break;
                            case 140:
                                if (frmPlc.ReadM(frmPlc.Stove1DoorQueen)) //判断门已关闭
                                {
                                    AppendText("机械手——输送轴放盘");
                                    mAsynTcpRobot.AsynSend("2linpt"); //通知机械手——放盘到输送线                         
                                    Step = 150;
                                }
                                //else
                                //{
                                //    frmPlc.WriteM(frmPlc.Stove1DoorTrip, true); //炉1开门
                                //    Thread.Sleep(4000);
                                //}
                                break;
                            case 150:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("2linpt"))
                                {
                                    Step = 700;
                                }
                                break;
                            #endregion
                            #region 下炉2(节点200~300)
                            case 200:
                                if (Marking.RobotStatus == Output.s20)
                                {
                                    AppendText("机械手——移至炉2取盘位");
                                    mAsynTcpRobot.AsynSend("2boxpw");   //机器人移至炉2上方
                                    Marking.RobotInformOutputting = false;
                                    Marking.RobotStatus = Output.s50;
                                    Step = 210;
                                }
                                else
                                {
                                    Step = 0;
                                }
                                break;
                            case 210:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("2boxpw"))
                                {

                                    AppendText("固化炉——炉2开门");
                                    frmPlc.WriteM(frmPlc.Stove2DoorTrip, true); //炉2开门
                                    try
                                    {
                                        //清除料盘信息
                                        for (int i = 0; i < RunPara.Instance.Stove[1].Tray.QRCode.Length; i++)
                                        {
                                            if (RunPara.Instance.Stove[1].Tray.QRCode[i].sign)
                                            {
                                                RunPara.Instance.MesStoveTemperature.data = RunPara.Instance.AverageTemperature[1];
                                                RunPara.Instance.MesStoveNo.data = RunPara.Instance.Stove[1].StoveNo;
                                                RunPara.Instance.OutTary.TrayCode = RunPara.Instance.Stove[1].Tray.TrayCode;
                                                //***
                                                RunPara.Instance.OutTary.QRCode[i].SN = RunPara.Instance.Stove[1].Tray.QRCode[i].SN;
                                                RunPara.Instance.OutTary.QRCode[i].sign = RunPara.Instance.Stove[1].Tray.QRCode[i].sign;
                                                RunPara.Instance.OutTary.QRCode[i].dgvRows = RunPara.Instance.Stove[1].Tray.QRCode[i].dgvRows;
                                                RunPara.Instance.OutTary.QRCode[i].scantime = RunPara.Instance.Stove[1].Tray.QRCode[i].scantime;

                                                RunPara.Instance.OutTary.QRCode[i].endtime = new DateTime();//1007
                                                RunPara.Instance.OutTary.QRCode[i].timespanScan = 0;//1007
                                                RunPara.Instance.OutTary.QRCode[i].A2C = RunPara.Instance.Stove[1].Tray.QRCode[i].A2C;//1007
                                                RunPara.Instance.OutTary.productInfo = RunPara.Instance.Stove[1].Tray.productInfo;//1007

                                                // RunPara.Instance.OutTary.QRCode[i].timespanPLC = RunPara.Instance.StoveCuringTime[1];

                                                RunPara.Instance.OutTary.QRCode[i].MesStoveTimeX = RunPara.Instance.MesStoveTime;
                                                RunPara.Instance.OutTary.QRCode[i].MesStoveTimeX.data = frmPlc.ReadInt16Data(frmPlc.Stove2CuringTime);//1017
                                                RunPara.Instance.OutTary.QRCode[i].MesStoveNoX = RunPara.Instance.MesStoveNo;
                                                RunPara.Instance.OutTary.QRCode[i].MesStoveTemperatureX = RunPara.Instance.MesStoveTemperature;
                                                //***
                                            }
                                            else
                                            {
                                            }
                                            RunPara.Instance.Stove[1].Tray.QRCode[i].SN = string.Empty;
                                            RunPara.Instance.Stove[1].Tray.QRCode[i].sign = false;
                                            RunPara.Instance.Stove[1].Tray.QRCode[i].dgvRows = -1;
                                        }

                                        RunPara.Instance.Stove[1].ifcaltime = false;

                                        //RunPara.Instance.OutTary.starttime = RunPara.Instance.Stove[1].Tray.starttime;
                                        //RunPara.Instance.OutTary.finishtime = DateTime.Now;
                                        //RunPara.Instance.OutTary.timeStove = RunPara.Instance.Stove[1].Tray.timeStove;

                                        RunPara.Instance.OutTary.timeStove = RunPara.Instance.Stove[1].timeStove;
                                        RunPara.Instance.Stove[1].StoveNo = 0;
                                        RunPara.Instance.Stove[1].AnyMaterialTary = false;
                                        RunPara.Instance.Stove[1].Tray.ProductPos = 0;

                                        Marking.StartTemperatureSign[1] = false;
                                        Marking.StartTemperatureSign[1] = true;
                                        Marking.StopCuringTimeSign[1] = true;
                                        RefreshDataDataGridView?.Invoke(this, RefreshDataGridView.DownStove);

                                        Marking.RobotPutAway = false;
                                        Marking.DownStoveRefreshState[1] = false;
                                        Marking.RobotStatus = Output.s30;
                                        UpMes();

                                    }
                                    catch
                                    {

                                    }
                                    // Thread.Sleep(4000);
                                    Step = 220;
                                }
                                break;
                            case 220:
                                if (frmPlc.ReadM(frmPlc.Stove2DoorFront))//判断门已打开
                                {
                                    AppendText("机械手——炉2取盘");
                                    mAsynTcpRobot.AsynSend("2boxpk"); //炉2取盘
                                    Step = 230;
                                }
                                break;
                            case 230:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("2boxpk"))
                                {
                                    AppendText("固化炉——炉2关门");
                                    frmPlc.WriteM(frmPlc.Stove2DoorTrip, false);
                                    Step = 240;
                                }
                                break;
                            case 240:
                                if (frmPlc.ReadM(frmPlc.Stove2DoorQueen)) //判断门已关闭
                                {
                                    AppendText("机械手——输送轴放盘");
                                    mAsynTcpRobot.AsynSend("2linpt"); //放盘到输送线                         
                                    Step = 250;
                                }
                                break;
                            case 250:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("2linpt"))
                                {
                                    Step = 700;
                                }
                                break;
                            #endregion
                            #region 下炉3(节点300~400)
                            case 300:
                                if (Marking.RobotStatus == Output.s20)
                                {
                                    AppendText("机械手移至炉3取盘位");
                                    mAsynTcpRobot.AsynSend("3boxpw");   //机器人移至炉3上方
                                    Marking.RobotInformOutputting = false;
                                    Marking.RobotStatus = Output.s50;
                                    Step = 310;
                                }
                                else
                                {
                                    Step = 0;
                                }
                                break;
                            case 310:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("3boxpw"))
                                {
                                    AppendText("固化炉——炉3开门");
                                    frmPlc.WriteM(frmPlc.Stove3DoorTrip, true); //炉3开门
                                    //Thread.Sleep(4000);
                                    try
                                    {
                                        //清除料盘信息
                                        for (int i = 0; i < RunPara.Instance.Stove[2].Tray.QRCode.Length; i++)
                                        {
                                            if (RunPara.Instance.Stove[2].Tray.QRCode[i].sign)
                                            {
                                                //  RunPara.Instance.MesStoveTime.data = RunPara.Instance.StoveCuringTime[2];
                                                RunPara.Instance.MesStoveTemperature.data = RunPara.Instance.AverageTemperature[2];
                                                RunPara.Instance.MesStoveNo.data = RunPara.Instance.Stove[2].StoveNo;
                                                RunPara.Instance.OutTary.TrayCode = RunPara.Instance.Stove[2].Tray.TrayCode;
                                                //***
                                                //MesDataSave(RunPara.Instance.Stove[2].Tray.QRCode[i].SN, RunPara.Instance.MesStoveTime,
                                                //RunPara.Instance.MesStoveTemperature, RunPara.Instance.MesStoveNo);
                                                //RunPara.Instance.Stove[2].Tray.QRCode[i].endtime = DateTime.Now;
                                                //TimeSpan SP = RunPara.Instance.Stove[2].Tray.QRCode[i].endtime - RunPara.Instance.Stove[2].Tray.QRCode[i].scantime;
                                                //MesDataSave(RunPara.Instance.Stove[2].Tray.QRCode[i].SN, RunPara.Instance.A2C, RunPara.Instance.MesStoveTime, SP.TotalSeconds.ToString(),
                                                //RunPara.Instance.MesStoveTemperature, RunPara.Instance.MesStoveNo);

                                                RunPara.Instance.OutTary.QRCode[i].scantime = RunPara.Instance.Stove[2].Tray.QRCode[i].scantime;
                                                RunPara.Instance.OutTary.QRCode[i].SN = RunPara.Instance.Stove[2].Tray.QRCode[i].SN;
                                                RunPara.Instance.OutTary.QRCode[i].sign = RunPara.Instance.Stove[2].Tray.QRCode[i].sign;
                                                RunPara.Instance.OutTary.QRCode[i].dgvRows = RunPara.Instance.Stove[2].Tray.QRCode[i].dgvRows;

                                                RunPara.Instance.OutTary.QRCode[i].endtime = new DateTime();//1007
                                                RunPara.Instance.OutTary.QRCode[i].timespanScan = 0;//1007
                                                RunPara.Instance.OutTary.QRCode[i].A2C = RunPara.Instance.Stove[2].Tray.QRCode[i].A2C;//1007
                                                RunPara.Instance.OutTary.productInfo = RunPara.Instance.Stove[2].Tray.productInfo;//1007


                                                //  RunPara.Instance.OutTary.QRCode[i].timespanPLC = RunPara.Instance.StoveCuringTime[2];
                                                RunPara.Instance.OutTary.QRCode[i].MesStoveTimeX = RunPara.Instance.MesStoveTime;
                                                RunPara.Instance.OutTary.QRCode[i].MesStoveTimeX.data = frmPlc.ReadInt16Data(frmPlc.Stove3CuringTime);//1017
                                                RunPara.Instance.OutTary.QRCode[i].MesStoveNoX = RunPara.Instance.MesStoveNo;
                                                RunPara.Instance.OutTary.QRCode[i].MesStoveTemperatureX = RunPara.Instance.MesStoveTemperature;

                                            }

                                            RunPara.Instance.Stove[2].Tray.QRCode[i].SN = string.Empty;
                                            RunPara.Instance.Stove[2].Tray.QRCode[i].sign = false;
                                            RunPara.Instance.Stove[2].Tray.QRCode[i].dgvRows = -1;
                                        }


                                        RunPara.Instance.Stove[2].ifcaltime = false;

                                        //  RunPara.Instance.OutTary.starttime = RunPara.Instance.Stove[2].Tray.starttime;
                                        //  RunPara.Instance.OutTary.finishtime = DateTime.Now;
                                        //  RunPara.Instance.OutTary.timeStove = RunPara.Instance.Stove[2].Tray.timeStove;
                                        RunPara.Instance.OutTary.timeStove = RunPara.Instance.Stove[2].timeStove;
                                        RunPara.Instance.Stove[2].StoveNo = 0;
                                        RunPara.Instance.Stove[2].AnyMaterialTary = false;
                                        RunPara.Instance.Stove[2].Tray.ProductPos = 0;

                                        Marking.StartTemperatureSign[2] = false;
                                        Marking.StartTemperatureSign[2] = true;
                                        Marking.StopCuringTimeSign[2] = true;
                                        RefreshDataDataGridView?.Invoke(this, RefreshDataGridView.DownStove);

                                        Marking.RobotPutAway = false;
                                        Marking.DownStoveRefreshState[2] = false;
                                        Marking.RobotStatus = Output.s30;

                                        UpMes();
                                    }
                                    catch
                                    {
                                    }
                                    Step = 320;
                                }
                                break;
                            case 320:
                                if (frmPlc.ReadM(frmPlc.Stove3DoorFront))//判断门已打开
                                {
                                    AppendText("机械手——炉3取盘");
                                    mAsynTcpRobot.AsynSend("3boxpk"); //炉3取盘
                                    Step = 330;
                                }
                                break;
                            case 330:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("3boxpk"))
                                {
                                    AppendText("固化炉——炉3关门");
                                    frmPlc.WriteM(frmPlc.Stove3DoorTrip, false); //炉3关门
                                    //Thread.Sleep(4000);
                                    Step = 340;
                                }
                                break;
                            case 340:
                                if (frmPlc.ReadM(frmPlc.Stove3DoorQueen)) //判断门已关闭
                                {
                                    AppendText("机械手——输送轴放盘");
                                    mAsynTcpRobot.AsynSend("2linpt"); //放盘到输送线                         
                                    Step = 350;
                                }
                                break;
                            case 350:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("2linpt"))
                                {
                                    Step = 700;
                                }
                                break;
                            #endregion
                            #region 下炉4(节点400~500)
                            case 400:
                                if (Marking.RobotStatus == Output.s20)
                                {
                                    AppendText("机械手移至炉4取盘位");
                                    mAsynTcpRobot.AsynSend("4boxpw");   //机器人移至炉4上方
                                    Marking.RobotInformOutputting = false;
                                    Marking.RobotStatus = Output.s50;
                                    Step = 410;
                                }
                                else
                                {
                                    Step = 0;
                                }
                                break;
                            case 410:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("4boxpw"))
                                {
                                    AppendText("固化炉——炉4开门");
                                    frmPlc.WriteM(frmPlc.Stove4DoorTrip, true); //炉4开门
                                    try
                                    {
                                        //清除料盘信息
                                        for (int i = 0; i < RunPara.Instance.Stove[3].Tray.QRCode.Length; i++)
                                        {
                                            if (RunPara.Instance.Stove[3].Tray.QRCode[i].sign)
                                            {
                                                //RunPara.Instance.MesStoveTime.data = RunPara.Instance.StoveCuringTime[3];
                                                RunPara.Instance.MesStoveTemperature.data = RunPara.Instance.AverageTemperature[3];
                                                RunPara.Instance.MesStoveNo.data = RunPara.Instance.Stove[3].StoveNo;
                                                RunPara.Instance.OutTary.TrayCode = RunPara.Instance.Stove[3].Tray.TrayCode;
                                                //***
                                                //RunPara.Instance.Stove[3].Tray.QRCode[i].endtime = DateTime.Now;
                                                //TimeSpan SP = RunPara.Instance.Stove[3].Tray.QRCode[i].endtime - RunPara.Instance.Stove[3].Tray.QRCode[i].scantime;
                                                //MesDataSave(RunPara.Instance.Stove[3].Tray.QRCode[i].SN, RunPara.Instance.A2C, RunPara.Instance.MesStoveTime, SP.TotalSeconds.ToString(),
                                                //RunPara.Instance.MesStoveTemperature, RunPara.Instance.MesStoveNo);

                                                //MesDataSave(RunPara.Instance.Stove[3].Tray.QRCode[i].SN, RunPara.Instance.MesStoveTime,
                                                //RunPara.Instance.MesStoveTemperature, RunPara.Instance.MesStoveNo);


                                                RunPara.Instance.OutTary.QRCode[i].SN = RunPara.Instance.Stove[3].Tray.QRCode[i].SN;
                                                RunPara.Instance.OutTary.QRCode[i].sign = RunPara.Instance.Stove[3].Tray.QRCode[i].sign;
                                                RunPara.Instance.OutTary.QRCode[i].dgvRows = RunPara.Instance.Stove[3].Tray.QRCode[i].dgvRows;
                                                RunPara.Instance.OutTary.QRCode[i].scantime = RunPara.Instance.Stove[3].Tray.QRCode[i].scantime;

                                                RunPara.Instance.OutTary.QRCode[i].endtime = new DateTime();//1007
                                                RunPara.Instance.OutTary.QRCode[i].timespanScan = 0;//1007
                                                RunPara.Instance.OutTary.QRCode[i].A2C = RunPara.Instance.Stove[3].Tray.QRCode[i].A2C;//1007
                                                RunPara.Instance.OutTary.productInfo = RunPara.Instance.Stove[3].Tray.productInfo;//1007

                                                RunPara.Instance.OutTary.QRCode[i].MesStoveTimeX = RunPara.Instance.MesStoveTime;
                                                RunPara.Instance.OutTary.QRCode[i].MesStoveTimeX.data = frmPlc.ReadInt16Data(frmPlc.Stove4CuringTime);//1017
                                                RunPara.Instance.OutTary.QRCode[i].MesStoveNoX = RunPara.Instance.MesStoveNo;
                                                RunPara.Instance.OutTary.QRCode[i].MesStoveTemperatureX = RunPara.Instance.MesStoveTemperature;
                                                //***
                                            }

                                            RunPara.Instance.Stove[3].Tray.QRCode[i].SN = string.Empty;
                                            RunPara.Instance.Stove[3].Tray.QRCode[i].sign = false;
                                            RunPara.Instance.Stove[3].Tray.QRCode[i].dgvRows = -1;
                                        }

                                        RunPara.Instance.Stove[3].ifcaltime = false;

                                        // RunPara.Instance.OutTary.timeStove = RunPara.Instance.Stove[3].Tray.timeStove;
                                        RunPara.Instance.OutTary.timeStove = RunPara.Instance.Stove[3].timeStove;
                                        RunPara.Instance.Stove[3].StoveNo = 0;
                                        RunPara.Instance.Stove[3].AnyMaterialTary = false;
                                        RunPara.Instance.Stove[3].Tray.ProductPos = 0;

                                        Marking.StartTemperatureSign[3] = false;
                                        Marking.StartTemperatureSign[3] = true;
                                        Marking.StopCuringTimeSign[3] = true;
                                        RefreshDataDataGridView?.Invoke(this, RefreshDataGridView.DownStove);

                                        Marking.RobotPutAway = false;
                                        Marking.DownStoveRefreshState[3] = false;
                                        Marking.RobotStatus = Output.s30;
                                        UpMes();
                                    }
                                    catch
                                    {

                                    }
                                    //   Thread.Sleep(4000);
                                    Step = 420;
                                }
                                break;
                            case 420:
                                if (frmPlc.ReadM(frmPlc.Stove4DoorFront))//判断门已打开
                                {
                                    AppendText("机械手——炉4取盘");
                                    mAsynTcpRobot.AsynSend("4boxpk"); //炉4取盘
                                    Step = 430;
                                }
                                break;
                            case 430:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("4boxpk"))
                                {
                                    AppendText("固化炉——炉4关门");
                                    frmPlc.WriteM(frmPlc.Stove4DoorTrip, false); //炉4关门
                                    // Thread.Sleep(4000)
                                    Step = 440;
                                }
                                break;
                            case 440:
                                if (frmPlc.ReadM(frmPlc.Stove4DoorQueen)) //判断门已关闭
                                {
                                    AppendText("机械手——输送轴放盘");
                                    mAsynTcpRobot.AsynSend("2linpt"); //放盘到输送线                         
                                    Step = 450;
                                }
                                break;
                            case 450:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("2linpt"))
                                {
                                    Step = 700;
                                }
                                break;
                            #endregion
                            #region 下炉5(节点500~600)
                            case 500:
                                if (Marking.RobotStatus == Output.s20)
                                {
                                    AppendText("机械手移至炉5取盘位");
                                    mAsynTcpRobot.AsynSend("5boxpw");   //机器人移至炉5上方
                                    Marking.RobotInformOutputting = false;
                                    Marking.RobotStatus = Output.s50;
                                    Step = 510;
                                }
                                else
                                {
                                    Step = 0;
                                }
                                break;
                            case 510:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("5boxpw"))
                                {
                                    AppendText("固化炉——炉5开门");
                                    frmPlc.WriteM(frmPlc.Stove5DoorTrip, true); //炉5开门
                                    //  Thread.Sleep(4000);
                                    try
                                    {
                                        //清除料盘信息
                                        for (int i = 0; i < RunPara.Instance.Stove[4].Tray.QRCode.Length; i++)
                                        {
                                            if (RunPara.Instance.Stove[4].Tray.QRCode[i].sign)
                                            {
                                                //      RunPara.Instance.MesStoveTime.data = RunPara.Instance.StoveCuringTime[4];
                                                RunPara.Instance.MesStoveTemperature.data = RunPara.Instance.AverageTemperature[4];
                                                RunPara.Instance.MesStoveNo.data = RunPara.Instance.Stove[4].StoveNo;
                                                RunPara.Instance.OutTary.TrayCode = RunPara.Instance.Stove[4].Tray.TrayCode;
                                                //***
                                                // MesDataSave(RunPara.Instance.Stove[4].Tray.QRCode[i].SN, RunPara.Instance.MesStoveTime,
                                                // RunPara.Instance.MesStoveTemperature, RunPara.Instance.MesStoveNo);
                                                // RunPara.Instance.Stove[4].Tray.QRCode[i].endtime = DateTime.Now;
                                                // TimeSpan SP = RunPara.Instance.Stove[4].Tray.QRCode[i].endtime - RunPara.Instance.Stove[4].Tray.QRCode[i].scantime;
                                                // MesDataSave(RunPara.Instance.Stove[4].Tray.QRCode[i].SN, RunPara.Instance.A2C, RunPara.Instance.MesStoveTime, SP.TotalSeconds.ToString(),
                                                // RunPara.Instance.MesStoveTemperature, RunPara.Instance.MesStoveNo);


                                                RunPara.Instance.OutTary.QRCode[i].scantime = RunPara.Instance.Stove[4].Tray.QRCode[i].scantime;
                                                RunPara.Instance.OutTary.QRCode[i].SN = RunPara.Instance.Stove[4].Tray.QRCode[i].SN;
                                                RunPara.Instance.OutTary.QRCode[i].sign = RunPara.Instance.Stove[4].Tray.QRCode[i].sign;
                                                RunPara.Instance.OutTary.QRCode[i].dgvRows = RunPara.Instance.Stove[4].Tray.QRCode[i].dgvRows;

                                                RunPara.Instance.OutTary.QRCode[i].endtime = new DateTime();//1007
                                                RunPara.Instance.OutTary.QRCode[i].timespanScan = 0;//1007
                                                RunPara.Instance.OutTary.QRCode[i].A2C = RunPara.Instance.Stove[4].Tray.QRCode[i].A2C;//1007
                                                RunPara.Instance.OutTary.productInfo = RunPara.Instance.Stove[4].Tray.productInfo;//1007


                                                RunPara.Instance.OutTary.QRCode[i].MesStoveTimeX = RunPara.Instance.MesStoveTime;
                                                RunPara.Instance.OutTary.QRCode[i].MesStoveTimeX.data = frmPlc.ReadInt16Data(frmPlc.Stove5CuringTime);//1017
                                                RunPara.Instance.OutTary.QRCode[i].MesStoveNoX = RunPara.Instance.MesStoveNo;
                                                RunPara.Instance.OutTary.QRCode[i].MesStoveTemperatureX = RunPara.Instance.MesStoveTemperature;
                                                //***
                                            }

                                            RunPara.Instance.Stove[4].Tray.QRCode[i].SN = string.Empty;
                                            RunPara.Instance.Stove[4].Tray.QRCode[i].sign = false;
                                            RunPara.Instance.Stove[4].Tray.QRCode[i].dgvRows = -1;
                                        }

                                        RunPara.Instance.Stove[4].ifcaltime = false;

                                        // RunPara.Instance.OutTary.timeStove = RunPara.Instance.Stove[4].Tray.timeStove;
                                        RunPara.Instance.OutTary.timeStove = RunPara.Instance.Stove[4].timeStove;
                                        RunPara.Instance.Stove[4].StoveNo = 0;
                                        RunPara.Instance.Stove[4].AnyMaterialTary = false;
                                        RunPara.Instance.Stove[4].Tray.ProductPos = 0;

                                        Marking.StartTemperatureSign[4] = false;
                                        Marking.StartTemperatureSign[4] = true;
                                        Marking.StopCuringTimeSign[4] = true;
                                        RefreshDataDataGridView?.Invoke(this, RefreshDataGridView.DownStove);

                                        Marking.RobotPutAway = false;
                                        Marking.DownStoveRefreshState[4] = false;
                                        Marking.RobotStatus = Output.s30;

                                        UpMes();
                                    }
                                    catch
                                    {
                                    }
                                    Step = 520;
                                }
                                break;
                            case 520:
                                if (frmPlc.ReadM(frmPlc.Stove5DoorFront))//判断门已打开
                                {
                                    AppendText("机械手——炉5取盘");
                                    mAsynTcpRobot.AsynSend("5boxpk"); //炉5取盘
                                    Step = 530;
                                }
                                break;
                            case 530:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("5boxpk"))
                                {
                                    AppendText("固化炉——炉5关门");
                                    frmPlc.WriteM(frmPlc.Stove5DoorTrip, false); //炉5关门
                                    //Thread.Sleep(4000);
                                    Step = 540;
                                }
                                break;
                            case 540:
                                if (frmPlc.ReadM(frmPlc.Stove5DoorQueen)) //判断门已关闭
                                {
                                    AppendText("机械手——输送轴放盘");
                                    mAsynTcpRobot.AsynSend("2linpt"); //放盘到输送线                         
                                    Step = 550;
                                }
                                break;
                            case 550:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("2linpt"))
                                {
                                    Step = 700;
                                }
                                break;
                            #endregion
                            #region 下炉6(节点600~700)
                            case 600:
                                if (Marking.RobotStatus == Output.s20)
                                {
                                    AppendText("机械手移至炉6取盘位");
                                    mAsynTcpRobot.AsynSend("6boxpw");   //机器人移至炉6上方
                                    Marking.RobotInformOutputting = false;
                                    Marking.RobotStatus = Output.s50;
                                    Step = 610;
                                }
                                else
                                {
                                    Step = 0;
                                }
                                break;
                            case 610:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("6boxpw"))
                                {
                                    AppendText("固化炉——炉6开门");
                                    frmPlc.WriteM(frmPlc.Stove6DoorTrip, true); //炉6开门
                                    // Thread.Sleep(4000);
                                    try
                                    {
                                        //清除料盘信息
                                        for (int i = 0; i < RunPara.Instance.Stove[5].Tray.QRCode.Length; i++)
                                        {
                                            if (RunPara.Instance.Stove[5].Tray.QRCode[i].sign)
                                            {
                                                //  RunPara.Instance.MesStoveTime.data = RunPara.Instance.StoveCuringTime[5];
                                                RunPara.Instance.MesStoveTemperature.data = RunPara.Instance.AverageTemperature[5];
                                                RunPara.Instance.MesStoveNo.data = RunPara.Instance.Stove[5].StoveNo;
                                                RunPara.Instance.OutTary.TrayCode = RunPara.Instance.Stove[5].Tray.TrayCode;
                                                //***
                                                //RunPara.Instance.Stove[5].Tray.QRCode[i].endtime = DateTime.Now;
                                                //TimeSpan SP = RunPara.Instance.Stove[5].Tray.QRCode[i].endtime - RunPara.Instance.Stove[5].Tray.QRCode[i].scantime;
                                                //MesDataSave(RunPara.Instance.Stove[5].Tray.QRCode[i].SN, RunPara.Instance.A2C, RunPara.Instance.MesStoveTime, SP.TotalSeconds.ToString(),
                                                //RunPara.Instance.MesStoveTemperature, RunPara.Instance.MesStoveNo);


                                                RunPara.Instance.OutTary.QRCode[i].scantime = RunPara.Instance.Stove[5].Tray.QRCode[i].scantime;
                                                RunPara.Instance.OutTary.QRCode[i].SN = RunPara.Instance.Stove[5].Tray.QRCode[i].SN;
                                                RunPara.Instance.OutTary.QRCode[i].sign = RunPara.Instance.Stove[5].Tray.QRCode[i].sign;
                                                RunPara.Instance.OutTary.QRCode[i].dgvRows = RunPara.Instance.Stove[5].Tray.QRCode[i].dgvRows;

                                                RunPara.Instance.OutTary.QRCode[i].endtime = new DateTime();//1007
                                                RunPara.Instance.OutTary.QRCode[i].timespanScan = 0;//1007
                                                RunPara.Instance.OutTary.QRCode[i].A2C = RunPara.Instance.Stove[5].Tray.QRCode[i].A2C;//1007
                                                RunPara.Instance.OutTary.productInfo = RunPara.Instance.Stove[5].Tray.productInfo;//1007

                                                RunPara.Instance.OutTary.QRCode[i].MesStoveTimeX = RunPara.Instance.MesStoveTime;
                                                RunPara.Instance.OutTary.QRCode[i].MesStoveTimeX.data = frmPlc.ReadInt16Data(frmPlc.Stove6CuringTime);//1017
                                                RunPara.Instance.OutTary.QRCode[i].MesStoveNoX = RunPara.Instance.MesStoveNo;
                                                RunPara.Instance.OutTary.QRCode[i].MesStoveTemperatureX = RunPara.Instance.MesStoveTemperature;
                                                //***
                                            }

                                            RunPara.Instance.Stove[5].Tray.QRCode[i].SN = string.Empty;
                                            RunPara.Instance.Stove[5].Tray.QRCode[i].sign = false;
                                            RunPara.Instance.Stove[5].Tray.QRCode[i].dgvRows = -1;
                                        }


                                        RunPara.Instance.Stove[5].ifcaltime = false;
                                        //   RunPara.Instance.OutTary.timeStove = RunPara.Instance.Stove[5].Tray.timeStove;
                                        RunPara.Instance.OutTary.timeStove = RunPara.Instance.Stove[5].timeStove;
                                        RunPara.Instance.Stove[5].StoveNo = 0;
                                        RunPara.Instance.Stove[5].AnyMaterialTary = false;
                                        RunPara.Instance.Stove[5].Tray.ProductPos = 0;

                                        Marking.StartTemperatureSign[5] = false;
                                        Marking.StartTemperatureSign[5] = true;
                                        Marking.StopCuringTimeSign[5] = true;
                                        RefreshDataDataGridView?.Invoke(this, RefreshDataGridView.DownStove);

                                        Marking.RobotPutAway = false;
                                        Marking.DownStoveRefreshState[5] = false;
                                        Marking.RobotStatus = Output.s30;

                                        UpMes();
                                    }
                                    catch
                                    {
                                    }
                                    Step = 620;
                                }
                                break;
                            case 620:
                                if (frmPlc.ReadM(frmPlc.Stove6DoorFront))//判断门已打开
                                {
                                    AppendText("机械手——炉6取盘");
                                    mAsynTcpRobot.AsynSend("6boxpk"); //炉6取盘
                                    Step = 630;
                                }
                                break;
                            case 630:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("6boxpk"))
                                {
                                    AppendText("固化炉——炉6关门");
                                    frmPlc.WriteM(frmPlc.Stove6DoorTrip, false); //炉6关门
                                    //  Thread.Sleep(4000);
                                    Step = 640;
                                }
                                break;
                            case 640:
                                if (frmPlc.ReadM(frmPlc.Stove6DoorQueen)) //判断门已关闭
                                {
                                    AppendText("机械手——输送轴放盘");
                                    mAsynTcpRobot.AsynSend("2linpt"); //放盘到输送线                         
                                    Step = 650;
                                }
                                break;
                            case 650:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("2linpt"))
                                {
                                    Step = 700;
                                }
                                break;
                            #endregion
                            #region 产品调度(节点700~900)
                            case 700:
                                if ((RunPara.Instance.OKTary.ProductPos == RunPara.Instance.TrayPoint || Marking.ImmediatelyUpStove) && IoPoints.I2DI16.Value) //OK满盘上炉 或 立即上炉
                                {
                                    RunPara.Instance.OKTary.CurProductPos = RunPara.Instance.TrayPoint;
                                    RunPara.Instance.OKTary.ProductPos = RunPara.Instance.TrayPoint + 1;
                                    Marking.ImmediatelyUpStove = false;
                                    FrmMain.prepos = 0; //清除已NG标志

                                    if (IoPoints.I2DI16.Value && RunPara.Instance.OKTary.ProductPos > 0) //工作位有盘 && 盘里有料
                                    {
                                        if (Marking.UpStoveRefreshState[3] && !RunPara.Instance.Stove4Shield)
                                        {
                                            Step = 1200;
                                        }
                                        else if (Marking.UpStoveRefreshState[0] && !RunPara.Instance.Stove1Shield)
                                        {
                                            Step = 900;
                                        }
                                        else if (Marking.UpStoveRefreshState[1] && !RunPara.Instance.Stove2Shield)
                                        {
                                            Step = 1000;
                                        }
                                        else if (Marking.UpStoveRefreshState[2] && !RunPara.Instance.Stove3Shield)
                                        {
                                            Step = 1100;
                                        }
                                        else if (Marking.UpStoveRefreshState[4] && !RunPara.Instance.Stove5Shield)
                                        {
                                            Step = 1300;
                                        }
                                        else if (Marking.UpStoveRefreshState[5] && !RunPara.Instance.Stove6Shield)
                                        {
                                            Step = 1400;
                                        }
                                        else
                                        {
                                            RefreshDataDataGridView?.Invoke(this, RefreshDataGridView.ManualTakeDish);
                                            if (ifwait01)
                                            {
                                                if (RunPara.Instance.OKTary.ProductPos == RunPara.Instance.TrayPoint + 1)
                                                    RunPara.Instance.OKTary.ProductPos = RunPara.Instance.TrayPoint;


                                                //m_Alarm = RobotAlarm.无法上炉请等待;
                                                //DialogResult result = MessageBox.Show("等待下炉30s. 请检查下炉位置", "", MessageBoxButtons.YesNoCancel);
                                                ifwait01 = false;
                                                Step = 50;
                                            }
                                            else
                                            {
                                                ifwait01 = true;
                                                RunPara.Instance.OKTary.ProductPos = RunPara.Instance.TrayPoint;
                                                Step = 50;
                                            }
                                        }
                                        ///10014
                                    }
                                }
                                else if (((IoPoints.T1DI00.Value || IoPoints.T1DI01.Value) && (IoPoints.T1DI00.Value != IoPoints.T1DI01.Value)) || ifemptyrun)//通知机械手——取料
                                {
                                    if (!ifemptyrun)
                                    {
                                        Marking.AAOK = IoPoints.T1DI00.Value;
                                        Marking.AANG = IoPoints.T1DI01.Value;
                                        IoPoints.T1DO00.Value = false;
                                    }
                                    AppendText("AA——接驳台到位");
                                    AwaitProductTime.Restart();//等待产品时间
                                    Step = 710;
                                }
                                else
                                {
                                    Step = 0;
                                }
                                break;
                            case 710:
                                if ((Marking.AAOK || RunPara.Instance.ShieldAAOK) && !RunPara.Instance.ShieldAANG) //产品OK
                                {
                                    if (IoPoints.I2DI16.Value) //OK位有盘
                                    {
                                        if (RunPara.Instance.OKTary.ProductPos < RunPara.Instance.TrayPoint) //OK位未满盘
                                        {
                                            if (!ifemptyrun)
                                            {
                                                AppendText("机械手——接驳台取料");
                                                mAsynTcpRobot.AsynSend("1linpk"); //通知机械手——取料
                                            }
                                            Step = 750;
                                        }
                                        else
                                        {
                                            Step = 700; //等待上炉
                                        }
                                    }
                                    else if (IoPoints.I2DI18.Value && !RunPara.Instance.TraySolidify) //待料位1有盘
                                    {
                                        AppendText("机械手——待料位1取盘");
                                        mAsynTcpRobot.AsynSend("4plapk"); //通知机械手——从待料位1取盘至OK位
                                        Step = 720;
                                    }
                                    else if (IoPoints.I2DI21.Value && !RunPara.Instance.TraySolidify) //待料位2有盘
                                    {
                                        AppendText("机械手——待料位2取盘");
                                        mAsynTcpRobot.AsynSend("3plapk"); //通知机械手——从待料位2取盘至OK位
                                        Step = 730;
                                    }
                                    else if (WishoutDiskWait) //无盘进入等待来盘，初始化为True
                                    {
                                        WishoutDiskWait = false;
                                        WishoutDiskTime.Restart();
                                    }
                                    else if ((WishoutDiskTime.ElapsedMilliseconds > RunPara.Instance.WishoutDiskWaitTime) && !RunPara.Instance.TraySolidify)  //等待来盘超时报警
                                    {
                                        WishoutDiskWait = true;
                                        Marking.AlarmStopThread = false;
                                        m_Alarm = RobotAlarm.无盘等待超时报警;
                                    }
                                    else
                                    {
                                        Step = 0;
                                    }
                                }
                                else if ((Marking.AANG && !RunPara.Instance.ShieldNGTray)  || RunPara.Instance.ShieldAANG) // 产品NG
                                {
                                    if (RunPara.Instance.NGTary.ProductPos < RunPara.Instance.TrayPoint) //NG位未满盘
                                    {
                                        if (!ifemptyrun)
                                        {
                                            AppendText("机械手——接驳台取料");
                                            mAsynTcpRobot.AsynSend("1linpk"); //通知机械手——取料
                                        }
                                        else
                                        {
                                            FrmMain.prepos = 1; //已NG标志
                                        }

                                        Step = 800;
                                    }
                                    else
                                    {
                                        for (int i = 0; i < RunPara.Instance.NGTary.QRCode.Length; i++)
                                        {
                                            RunPara.Instance.NGTary.QRCode[i].SN = string.Empty;
                                            RunPara.Instance.NGTary.QRCode[i].sign = false;
                                            RunPara.Instance.NGTary.QRCode[i].dgvRows = -1;
                                        }
                                        RunPara.Instance.NGTary.ProductPos = 0;

                                        m_Alarm = RobotAlarm.NG料盘已满_清料;
                                        Marking.AlarmStopThread = false;
                                        Step = 0;  //1130
                                    }
                                }
                                else if (Marking.AANG && RunPara.Instance.ShieldNGTray)
                                {
                                    IoPoints.T1DO01.Value = true; //通知AA开夹
                                    AppendText("机械手——屏蔽NG料盘，NG料不取");
                                    Thread.Sleep(500);
                                    IoPoints.T1DO00.Value = true;
                                    IoPoints.T1DO01.Value = false;
                                    Thread.Sleep(500);
                                    IoPoints.T1DO00.Value = false;
                                    RunPara.Instance.Continuous3Alarm++;
                                    AwaitProductTime.Restart();
                                    Step = 820;
                                }
                                else
                                {
                                    Step = 0;
                                }
                                break;
                            case 720:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("4plapk"))
                                {
                                    AppendText("机械手——OK位放盘");
                                    mAsynTcpRobot.AsynSend("1plapt");  //OK位放盘
                                    RunPara.Instance.OKTary.TrayCode = RunPara.Instance.WaitTary1.TrayCode;
                                    Step = 740;
                                }
                                break;
                            case 730:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("3plapk"))
                                {
                                    AppendText("机械手——OK位放盘");
                                    mAsynTcpRobot.AsynSend("1plapt");  //OK位放盘
                                    RunPara.Instance.OKTary.TrayCode = RunPara.Instance.WaitTary2.TrayCode;
                                    Step = 740;
                                }
                                break;
                            case 740: //OK位放盘结束
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("1plapt"))
                                {
                                    Step = 0;
                                }
                                break;
                            case 750://OK品
                                if (!ifemptyrun)
                                {
                                    tcpResults = mAsynTcpRobot.Result.Split('\r');
                                    if (tcpResults.Length > 0 && tcpResults[0].Contains("1linpk")) //机械手已到位
                                    {
                                        IoPoints.T1DO01.Value = true; //通知AA开夹
                                        if (IoPoints.T1DI02.Value)    //接驳台已开夹
                                        {
                                            AppendText("AA——开夹气缸已打开");
                                            Thread.Sleep(500);
                                            AppendText("机械手——接驳台取料完成");
                                            mAsynTcpRobot.AsynSend("1linpw"); //通知机械手已开夹
                                            Step = 755;
                                        }
                                    }
                                }
                                else
                                {
                                    AppendText("机械手——移至产品扫码位");
                                    mAsynTcpRobot.AsynSend("codeps");
                                    Step = 760;
                                }
                                break;
                            case 755:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("1linpw"))
                                {
                                    IoPoints.T1DO00.Value = true;
                                    IoPoints.T1DO01.Value = false;
                                    if (!RunPara.Instance.ShieldProductCode)
                                    {
                                        AppendText("机械手——移至产品扫码位");
                                        mAsynTcpRobot.AsynSend("codeps"); //通知机械手扫码
                                        Step = 760;
                                    }
                                    else
                                    {
                                        TcpRobotCmd = "1pla" + (RunPara.Instance.OKTary.ProductPos + 1).ToString("D2");
                                        RunPara.Instance.OKTary.CurProductPos = RunPara.Instance.OKTary.ProductPos;
                                        RunPara.Instance.OKTary.ProductPos++;
                                        RunPara.Instance.OKTary.QRCode[RunPara.Instance.OKTary.CurProductPos].A2C = RunPara.Instance.A2C;
                                        AppendText("机械手——OK位放料");
                                        mAsynTcpRobot.AsynSend(TcpRobotCmd); //OK料放置
                                        IoPoints.T1DO00.Value = false;
                                        Step = 790;
                                    }
                                    
                                }
                                break;
                            case 760: //产品到扫码位
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("codeps"))
                                {
                                    IoPoints.T1DO00.Value = false;    //OK到达扫码位，发送取料完成信号
                                    QRCodeTime.Restart();
                                    QRCodeReader.receiveFinish = false;
                                    QRCodeReader.Trigger(new TriggerArgs()
                                    {
                                        sender = this,
                                        tryTimes = 1,
                                        message = "SN\r\n"
                                    });
                                    Step = 780;
                                }
                                break;
                            case 770: //扫码
                                QRCodeReader.receiveFinish = false;
                                QRCodeReader.Trigger(new TriggerArgs()
                                {
                                    sender = this,
                                    tryTimes = 1,
                                    message = "SN\r\n"
                                });
                                Step = 780;
                                break;
                            case 780://扫产品码
                                if (QRCodeReader.receiveFinish)
                                {
                                    if (RunPara.Instance.OKTary.QRCode[RunPara.Instance.OKTary.ProductPos].SN != string.Empty)
                                    {
                                        TcpRobotCmd = "1pla" + (RunPara.Instance.OKTary.ProductPos + 1).ToString("D2");
                                        RunPara.Instance.OKTary.CurProductPos = RunPara.Instance.OKTary.ProductPos;
                                        RunPara.Instance.OKTary.ProductPos++;
                                        RunPara.Instance.OKTary.QRCode[RunPara.Instance.OKTary.CurProductPos].A2C = RunPara.Instance.A2C;
                                        AppendText("机械手——OK位放料");
                                        mAsynTcpRobot.AsynSend(TcpRobotCmd); //OK料放置
                                        Step = 790;
                                    }
                                    else if (QRCodeTime.ElapsedMilliseconds < RunPara.Instance.QRCodeDelayTime)//扫码NG重扫
                                    {
                                        Step = 770;
                                    }
                                    else if (RunPara.Instance.NGTary.ProductPos < RunPara.Instance.TrayPoint)
                                    {
                                        TcpRobotCmd = "2pla" + (RunPara.Instance.NGTary.ProductPos + 1).ToString("D2");
                                        AppendText("机械手——NG位放料");
                                        mAsynTcpRobot.AsynSend(TcpRobotCmd); //NG料放置
                                        LogHelper.Debug("扫码NG");
                                        Step = 810;
                                    }
                                    else
                                    {
                                        //NG物料放置 满盘
                                        for (int i = 0; i < RunPara.Instance.NGTary.QRCode.Length; i++)
                                        {
                                            RunPara.Instance.NGTary.QRCode[i].SN = string.Empty;
                                            RunPara.Instance.NGTary.QRCode[i].sign = false;
                                            RunPara.Instance.NGTary.QRCode[i].dgvRows = -1;
                                        }
                                        RunPara.Instance.NGTary.ProductPos = 0;

                                        m_Alarm = RobotAlarm.NG料盘已满_清料;
                                        Marking.AlarmStopThread = false;
                                        Step = 0;  //1130
                                    }
                                }
                                break;
                            case 790://OK料放置
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains(TcpRobotCmd))
                                {
                                    RunPara.Instance.Continuous3Alarm = 0;
                                    //ok满 判断是否炉屏蔽
                                    if (RunPara.Instance.OKTary.ProductPos >= RunPara.Instance.TrayPoint && RunPara.Instance.Stove1Shield && RunPara.Instance.Stove2Shield && RunPara.Instance.Stove3Shield
                                            && RunPara.Instance.Stove4Shield && RunPara.Instance.Stove5Shield && RunPara.Instance.Stove6Shield)
                                    {
                                        RunPara.Instance.OKTary.ProductPos = 0;
                                        m_Alarm = RobotAlarm.无法上炉请等待;
                                    }
                                    AwaitProductTime.Restart();
                                    Step = 0;
                                }
                                break;
                            case 800: //AA_NG产品
                                if (!ifemptyrun)
                                {
                                    tcpResults = mAsynTcpRobot.Result.Split('\r');
                                    if (tcpResults.Length > 0 && tcpResults[0].Contains("1linpk")) //机械手已到位
                                    {
                                        IoPoints.T1DO01.Value = true; //通知AA开夹
                                        if (IoPoints.T1DI02.Value)    //接驳台已开夹
                                        {
                                            AppendText("AA——开夹气缸已打开");
                                            Thread.Sleep(500);
                                            AppendText("机械手——接驳台夹料完成");
                                            mAsynTcpRobot.AsynSend("1linpw"); //通知机械手已开夹
                                            Step = 805;
                                        }
                                    }
                                }
                                else
                                {
                                    TcpRobotCmd = "2pla" + (RunPara.Instance.NGTary.ProductPos + 1).ToString("D2");
                                    AppendText("机械手——NG位放料");
                                    mAsynTcpRobot.AsynSend(TcpRobotCmd); //NG物料放置
                                    Step = 810;
                                }
                                break;
                            case 805:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("1linpw"))
                                {
                                    IoPoints.T1DO00.Value = true;
                                    IoPoints.T1DO01.Value = false;
                                    Thread.Sleep(500);
                                    TcpRobotCmd = "2pla" + (RunPara.Instance.NGTary.ProductPos + 1).ToString("D2");
                                    AppendText("机械手——NG位放料");
                                    mAsynTcpRobot.AsynSend(TcpRobotCmd); //NG物料放置
                                    Step = 810;
                                }
                                break;
                            case 810: //NG产品
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains(TcpRobotCmd))
                                {
                                    IoPoints.T1DO00.Value = false;  //NG放置完成，发送取料完成信号
                                    RunPara.Instance.NGTary.ProductPos++;
                                    if (RunPara.Instance.NGTary.ProductPos >= RunPara.Instance.TrayPoint)
                                    {
                                        RunPara.Instance.NGTary.ProductPos = 0;
                                        m_Alarm = RobotAlarm.NG料盘已满_清料;
                                        Marking.AlarmStopThread = false;
                                    }
                                    RunPara.Instance.Continuous3Alarm++;
                                    AwaitProductTime.Restart();
                                    Step = 820;
                                }
                                break;
                            case 820:
                                if (RunPara.Instance.Continuous3Alarm >= RunPara.Instance.ContinuousAlarmLimt)
                                {
                                    RunPara.Instance.Continuous3Alarm = 0;
                                    m_Alarm = RobotAlarm.连续NG报警;
                                    Marking.AlarmStopThread = false;
                                    Step = 0;
                                }
                                else
                                {
                                    Step = 0;
                                }
                                break;
                            #endregion
                            #region 上炉1(节点900~1000)
                            case 900:
                                if (!Global.Instance.Stove1AnyMaterial)
                                {
                                    AppendText("机械手——OK位取盘");
                                    mAsynTcpRobot.AsynSend("1plapk"); //OK位取料盘
                                }
                                else
                                    MessageBox.Show("上炉1异常");
                                Step = 910;
                                break;
                            case 910:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("1plapk"))
                                {
                                    AppendText("机械手——移至炉1放盘位");
                                    mAsynTcpRobot.AsynSend("1boxpw");//机器人移至炉1上方
                                    Step = 920;
                                }
                                break;
                            case 920:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("1boxpw"))
                                {
                                    AppendText("固化炉——炉1开门");
                                    frmPlc.WriteM(frmPlc.Stove1DoorTrip, true); //炉1开门
                                    //  Thread.Sleep(4000);
                                    Step = 930;
                                }
                                break;
                            case 930:
                                if (frmPlc.ReadM(frmPlc.Stove1DoorFront))//判断门已打开
                                {
                                    AppendText("机械手——炉1放盘");
                                    mAsynTcpRobot.AsynSend("1boxpt");//炉1放料盘
                                    Step = 940;
                                }
                                break;
                            case 940:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("1boxpt"))
                                {
                                    AppendText("固化炉——炉1关门");
                                    frmPlc.WriteM(frmPlc.Stove1DoorTrip, false); //炉1关门
                                    // Thread.Sleep(4000);
                                    Step = 950;
                                }
                                break;
                            case 950:
                                if (frmPlc.ReadM(frmPlc.Stove1DoorQueen))//判断门已关闭
                                {
                                    //RunPara.Instance.OKTary.ProductPos = 0;
                                    //添加新料盘信息
                                    RunPara.Instance.Stove[0].StoveNo = 1;
                                    RunPara.Instance.Stove[0].AnyMaterialTary = true;
                                    RunPara.Instance.Stove[0].Tray.TrayCode = RunPara.Instance.OKTary.TrayCode;
                                    for (int i = 0; i < RunPara.Instance.OKTary.QRCode.Length; i++)
                                    {
                                        RunPara.Instance.Stove[0].Tray.QRCode[i].SN = RunPara.Instance.OKTary.QRCode[i].SN;
                                        RunPara.Instance.Stove[0].Tray.QRCode[i].sign = RunPara.Instance.OKTary.QRCode[i].sign;
                                        RunPara.Instance.Stove[0].Tray.QRCode[i].dgvRows = RunPara.Instance.OKTary.QRCode[i].dgvRows;
                                        RunPara.Instance.Stove[0].Tray.QRCode[i].scantime = RunPara.Instance.OKTary.QRCode[i].scantime;//1007
                                        RunPara.Instance.Stove[0].Tray.QRCode[i].endtime = new DateTime();//1007
                                        RunPara.Instance.Stove[0].Tray.QRCode[i].timespanScan = 0;//1007
                                        RunPara.Instance.Stove[0].Tray.QRCode[i].A2C = RunPara.Instance.OKTary.QRCode[i].A2C;//1007
                                        RunPara.Instance.Stove[0].Tray.productInfo = RunPara.Instance.OKTary.productInfo;//1007

                                        RunPara.Instance.OKTary.QRCode[i].SN = string.Empty;
                                        RunPara.Instance.OKTary.QRCode[i].sign = false;
                                        RunPara.Instance.OKTary.QRCode[i].dgvRows = -1;
                                        RunPara.Instance.OKTary.QRCode[i].scantime = new DateTime();//1007
                                        RunPara.Instance.OKTary.QRCode[i].A2C = "";
                                        RunPara.Instance.OKTary.productInfo = "";
                                    }
                                    //  RunPara.Instance.Stove[0].Tray.starttime = DateTime.Now;
                                    RunPara.Instance.OKTary.ProductPos = 0;
                                    RefreshDataDataGridView?.Invoke(this, RefreshDataGridView.UpStove);
                                    RunPara.Instance.Stove[0].ifcaltime = true;

                                    frmPlc.WriteM(frmPlc.Stove1AutoStart, true); //自动运行
                                    AppendText("机械手——返回待命位");
                                    mAsynTcpRobot.AsynSend("waitps");//机器人回待料位
                                    Step = 960;
                                }
                                break;
                            case 960:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("waitps"))
                                {
                                    Marking.UpStoveRefreshState[0] = false;
                                    Step = 0;
                                }
                                break;
                            #endregion
                            #region 上炉2(节点1000~1100)
                            case 1000:

                                if (!Global.Instance.Stove2AnyMaterial)
                                {
                                    AppendText("机械手——OK位取盘");
                                    mAsynTcpRobot.AsynSend("1plapk"); //OK位取料盘
                                }
                                else
                                    MessageBox.Show("上炉2异常");
                                Step = 1010;
                                break;
                            case 1010:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("1plapk"))
                                {
                                    AppendText("机械手——移至炉2放盘位");
                                    mAsynTcpRobot.AsynSend("2boxpw");//机器人移至炉2上方
                                    Step = 1020;
                                }
                                break;
                            case 1020:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("2boxpw"))
                                {
                                    AppendText("固化炉——炉2开门");
                                    frmPlc.WriteM(frmPlc.Stove2DoorTrip, true); //炉2开门
                                    // Thread.Sleep(4000);
                                    Step = 1030;
                                }
                                break;
                            case 1030:
                                if (frmPlc.ReadM(frmPlc.Stove2DoorFront))//判断门已打开
                                {
                                    AppendText("机械手——炉2放盘");
                                    mAsynTcpRobot.AsynSend("2boxpt");//炉2放料盘
                                    Step = 1040;
                                }
                                break;
                            case 1040:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("2boxpt"))
                                {
                                    AppendText("固化炉——炉2关门");
                                    frmPlc.WriteM(frmPlc.Stove2DoorTrip, false); //炉2关门
                                    //   Thread.Sleep(4000);
                                    Step = 1050;
                                }
                                break;
                            case 1050:
                                if (frmPlc.ReadM(frmPlc.Stove2DoorQueen))
                                {
                                    //添加新料盘信息
                                    RunPara.Instance.Stove[1].StoveNo = 2;
                                    RunPara.Instance.Stove[1].AnyMaterialTary = true;
                                    RunPara.Instance.Stove[1].Tray.TrayCode = RunPara.Instance.OKTary.TrayCode;
                                    for (int i = 0; i < RunPara.Instance.OKTary.QRCode.Length; i++)
                                    {
                                        //RunPara.Instance.Stove[1].Tray.QRCode[i].SN = RunPara.Instance.OKTary.QRCode[i].SN;
                                        //RunPara.Instance.Stove[1].Tray.QRCode[i].sign = RunPara.Instance.OKTary.QRCode[i].sign;
                                        //RunPara.Instance.Stove[1].Tray.QRCode[i].dgvRows = RunPara.Instance.OKTary.QRCode[i].dgvRows;

                                        //RunPara.Instance.OKTary.QRCode[i].SN = string.Empty;
                                        //RunPara.Instance.OKTary.QRCode[i].sign = false;
                                        //RunPara.Instance.OKTary.QRCode[i].dgvRows = -1;


                                        RunPara.Instance.Stove[1].Tray.QRCode[i].SN = RunPara.Instance.OKTary.QRCode[i].SN;
                                        RunPara.Instance.Stove[1].Tray.QRCode[i].sign = RunPara.Instance.OKTary.QRCode[i].sign;
                                        RunPara.Instance.Stove[1].Tray.QRCode[i].dgvRows = RunPara.Instance.OKTary.QRCode[i].dgvRows;
                                        RunPara.Instance.Stove[1].Tray.QRCode[i].scantime = RunPara.Instance.OKTary.QRCode[i].scantime;//1007
                                        RunPara.Instance.Stove[1].Tray.QRCode[i].endtime = new DateTime();//1007
                                        RunPara.Instance.Stove[1].Tray.QRCode[i].timespanScan = 0;//1007
                                        RunPara.Instance.Stove[1].Tray.QRCode[i].A2C = RunPara.Instance.OKTary.QRCode[i].A2C;//1007
                                        RunPara.Instance.Stove[1].Tray.productInfo = RunPara.Instance.OKTary.productInfo;//1007

                                        RunPara.Instance.OKTary.QRCode[i].SN = string.Empty;
                                        RunPara.Instance.OKTary.QRCode[i].sign = false;
                                        RunPara.Instance.OKTary.QRCode[i].dgvRows = -1;
                                        RunPara.Instance.OKTary.QRCode[i].scantime = new DateTime();//1007
                                        RunPara.Instance.OKTary.QRCode[i].A2C = "";
                                        RunPara.Instance.OKTary.productInfo = "";
                                    }
                                    RunPara.Instance.Stove[1].ifcaltime = true;
                                    //RunPara.Instance.Stove[1].Tray.starttime = DateTime.Now;
                                    RunPara.Instance.OKTary.ProductPos = 0;
                                    RefreshDataDataGridView?.Invoke(this, RefreshDataGridView.UpStove);

                                    frmPlc.WriteM(frmPlc.Stove2AutoStart, true); //自动运行
                                    AppendText("机械手——返回待命位");
                                    mAsynTcpRobot.AsynSend("waitps");//机器人回待料位
                                    Step = 1060;
                                }
                                break;
                            case 1060:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("waitps"))
                                {
                                    Marking.UpStoveRefreshState[1] = false;
                                    Step = 0;
                                }
                                break;
                            #endregion
                            #region 上炉3(节点1100~1200)
                            case 1100:
                                if (!Global.Instance.Stove3AnyMaterial)
                                {
                                    AppendText("机械手——OK位取盘");
                                    mAsynTcpRobot.AsynSend("1plapk"); //OK位取料盘
                                }
                                else
                                    MessageBox.Show("上炉3异常");
                                Step = 1110;
                                break;
                            case 1110:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("1plapk"))
                                {
                                    AppendText("机械手——移至炉3放盘位");
                                    mAsynTcpRobot.AsynSend("3boxpw");//机器人移至炉3上方
                                    Step = 1120;
                                }
                                break;
                            case 1120:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("3boxpw"))
                                {
                                    AppendText("固化炉——炉3开门");
                                    frmPlc.WriteM(frmPlc.Stove3DoorTrip, true); //炉3开门
                                    //       Thread.Sleep(4000);
                                    Step = 1130;
                                }
                                break;
                            case 1130:
                                if (frmPlc.ReadM(frmPlc.Stove3DoorFront))//判断门已打开
                                {
                                    AppendText("机械手——炉3放盘");
                                    mAsynTcpRobot.AsynSend("3boxpt");//炉3放料盘
                                    Step = 1140;
                                }
                                break;
                            case 1140:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("3boxpt"))
                                {
                                    AppendText("固化炉——炉3关门");
                                    frmPlc.WriteM(frmPlc.Stove3DoorTrip, false); //炉3关门
                                    //   Thread.Sleep(4000);
                                    Step = 1150;
                                }
                                break;
                            case 1150:
                                if (frmPlc.ReadM(frmPlc.Stove3DoorQueen))
                                {
                                    //添加新料盘信息
                                    RunPara.Instance.Stove[2].StoveNo = 3;
                                    RunPara.Instance.Stove[2].AnyMaterialTary = true;
                                    RunPara.Instance.Stove[2].Tray.TrayCode = RunPara.Instance.OKTary.TrayCode;
                                    for (int i = 0; i < RunPara.Instance.OKTary.QRCode.Length; i++)
                                    {
                                        //RunPara.Instance.Stove[2].Tray.QRCode[i].SN = RunPara.Instance.OKTary.QRCode[i].SN;
                                        //RunPara.Instance.Stove[2].Tray.QRCode[i].sign = RunPara.Instance.OKTary.QRCode[i].sign;
                                        //RunPara.Instance.Stove[2].Tray.QRCode[i].dgvRows = RunPara.Instance.OKTary.QRCode[i].dgvRows;
                                        //RunPara.Instance.OKTary.QRCode[i].SN = string.Empty;
                                        //RunPara.Instance.OKTary.QRCode[i].sign = false;
                                        //RunPara.Instance.OKTary.QRCode[i].dgvRows = -1;

                                        RunPara.Instance.Stove[2].Tray.QRCode[i].SN = RunPara.Instance.OKTary.QRCode[i].SN;
                                        RunPara.Instance.Stove[2].Tray.QRCode[i].sign = RunPara.Instance.OKTary.QRCode[i].sign;
                                        RunPara.Instance.Stove[2].Tray.QRCode[i].dgvRows = RunPara.Instance.OKTary.QRCode[i].dgvRows;
                                        RunPara.Instance.Stove[2].Tray.QRCode[i].scantime = RunPara.Instance.OKTary.QRCode[i].scantime;//1007
                                        RunPara.Instance.Stove[2].Tray.QRCode[i].endtime = new DateTime();//1007
                                        RunPara.Instance.Stove[2].Tray.QRCode[i].timespanScan = 0;//1007
                                        RunPara.Instance.Stove[2].Tray.QRCode[i].A2C = RunPara.Instance.OKTary.QRCode[i].A2C;//1007
                                        RunPara.Instance.Stove[2].Tray.productInfo = RunPara.Instance.OKTary.productInfo;//1007

                                        RunPara.Instance.OKTary.QRCode[i].SN = string.Empty;
                                        RunPara.Instance.OKTary.QRCode[i].sign = false;
                                        RunPara.Instance.OKTary.QRCode[i].dgvRows = -1;
                                        RunPara.Instance.OKTary.QRCode[i].scantime = new DateTime();//1007
                                        RunPara.Instance.OKTary.QRCode[i].A2C = "";
                                        RunPara.Instance.OKTary.productInfo = "";
                                    }

                                    RunPara.Instance.Stove[2].ifcaltime = true;
                                    //   RunPara.Instance.Stove[2].Tray.starttime = DateTime.Now;
                                    RunPara.Instance.OKTary.ProductPos = 0;
                                    RefreshDataDataGridView?.Invoke(this, RefreshDataGridView.UpStove);

                                    frmPlc.WriteM(frmPlc.Stove3AutoStart, true); //自动运行
                                    AppendText("机械手——返回待命位");
                                    mAsynTcpRobot.AsynSend("waitps");//机器人回待料位
                                    Step = 1160;
                                }

                                break;
                            case 1160:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("waitps"))
                                {
                                    Marking.UpStoveRefreshState[2] = false;
                                    Step = 0;
                                }
                                break;
                            #endregion
                            #region 上炉4(节点1200~1300)
                            case 1200:

                                if (!Global.Instance.Stove4AnyMaterial)
                                {
                                    AppendText("机械手——OK位取盘");
                                    mAsynTcpRobot.AsynSend("1plapk"); //OK位取料盘
                                }
                                else
                                    MessageBox.Show("上炉4异常");
                                Step = 1210;
                                break;
                            case 1210:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("1plapk"))
                                {
                                    AppendText("机械手——移至炉4放盘位");
                                    mAsynTcpRobot.AsynSend("4boxpw");//机器人移至炉4上方
                                    Step = 1220;
                                }
                                break;
                            case 1220:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("4boxpw"))
                                {
                                    AppendText("固化炉——炉4开门");
                                    frmPlc.WriteM(frmPlc.Stove4DoorTrip, true); //炉4开门
                                    //  Thread.Sleep(4000);
                                    Step = 1230;
                                }
                                break;
                            case 1230:
                                if (frmPlc.ReadM(frmPlc.Stove4DoorFront))//判断门已打开
                                {
                                    AppendText("机械手——炉4放盘");
                                    mAsynTcpRobot.AsynSend("4boxpt");//炉2放料盘
                                    Step = 1240;
                                }

                                break;
                            case 1240:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("4boxpt"))
                                {
                                    AppendText("固化炉——炉4关门");
                                    frmPlc.WriteM(frmPlc.Stove4DoorTrip, false); //炉4关门
                                                                                 // Thread.Sleep(4000);                                                                               
                                    Step = 1250;
                                }
                                break;
                            case 1250:
                                if (frmPlc.ReadM(frmPlc.Stove4DoorQueen))
                                {
                                    //添加新料盘信息
                                    RunPara.Instance.Stove[3].StoveNo = 4;
                                    RunPara.Instance.Stove[3].AnyMaterialTary = true;
                                    RunPara.Instance.Stove[3].Tray.TrayCode = RunPara.Instance.OKTary.TrayCode;
                                    for (int i = 0; i < RunPara.Instance.OKTary.QRCode.Length; i++)
                                    {
                                        //RunPara.Instance.Stove[3].Tray.QRCode[i].SN = RunPara.Instance.OKTary.QRCode[i].SN;
                                        //RunPara.Instance.Stove[3].Tray.QRCode[i].sign = RunPara.Instance.OKTary.QRCode[i].sign;
                                        //RunPara.Instance.Stove[3].Tray.QRCode[i].dgvRows = RunPara.Instance.OKTary.QRCode[i].dgvRows;
                                        //RunPara.Instance.OKTary.QRCode[i].SN = string.Empty;
                                        //RunPara.Instance.OKTary.QRCode[i].sign = false;
                                        //RunPara.Instance.OKTary.QRCode[i].dgvRows = -1;


                                        RunPara.Instance.Stove[3].Tray.QRCode[i].SN = RunPara.Instance.OKTary.QRCode[i].SN;
                                        RunPara.Instance.Stove[3].Tray.QRCode[i].sign = RunPara.Instance.OKTary.QRCode[i].sign;
                                        RunPara.Instance.Stove[3].Tray.QRCode[i].dgvRows = RunPara.Instance.OKTary.QRCode[i].dgvRows;
                                        RunPara.Instance.Stove[3].Tray.QRCode[i].scantime = RunPara.Instance.OKTary.QRCode[i].scantime;//1007
                                        RunPara.Instance.Stove[3].Tray.QRCode[i].endtime = new DateTime();//1007
                                        RunPara.Instance.Stove[3].Tray.QRCode[i].timespanScan = 0;//1007
                                        RunPara.Instance.Stove[3].Tray.QRCode[i].A2C = RunPara.Instance.OKTary.QRCode[i].A2C;//1007
                                        RunPara.Instance.Stove[3].Tray.productInfo = RunPara.Instance.OKTary.productInfo;//1007

                                        RunPara.Instance.OKTary.QRCode[i].SN = string.Empty;
                                        RunPara.Instance.OKTary.QRCode[i].sign = false;
                                        RunPara.Instance.OKTary.QRCode[i].dgvRows = -1;
                                        RunPara.Instance.OKTary.QRCode[i].scantime = new DateTime();//1007
                                        RunPara.Instance.OKTary.QRCode[i].A2C = "";
                                        RunPara.Instance.OKTary.productInfo = "";
                                    }
                                    RunPara.Instance.Stove[3].ifcaltime = true;
                                    //      RunPara.Instance.Stove[3].Tray.starttime = DateTime.Now;
                                    RunPara.Instance.OKTary.ProductPos = 0;
                                    RefreshDataDataGridView?.Invoke(this, RefreshDataGridView.UpStove);

                                    frmPlc.WriteM(frmPlc.Stove4AutoStart, true); //自动运行
                                    AppendText("机械手——返回待命位");
                                    mAsynTcpRobot.AsynSend("waitps");//机器人回待料位
                                    Step = 1260;
                                }
                                break;
                            case 1260:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("waitps"))
                                {
                                    Marking.UpStoveRefreshState[3] = false;
                                    Step = 0;
                                }
                                break;
                            #endregion
                            #region 上炉5(节点1300~1400)
                            case 1300:
                                if (!Global.Instance.Stove5AnyMaterial)
                                {
                                    AppendText("机械手——OK位取盘");
                                    mAsynTcpRobot.AsynSend("1plapk"); //OK位取料盘
                                }
                                else
                                    MessageBox.Show("上炉5异常");
                                Step = 1310;
                                break;
                            case 1310:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("1plapk"))
                                {
                                    AppendText("机械手——移至炉5放盘位");
                                    mAsynTcpRobot.AsynSend("5boxpw");//机器人移至炉5上方
                                    Step = 1320;
                                }
                                break;
                            case 1320:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("5boxpw"))
                                {
                                    AppendText("固化炉——炉5开门");
                                    frmPlc.WriteM(frmPlc.Stove5DoorTrip, true); //炉5开门
                                    //   Thread.Sleep(4000);
                                    //  frmPlc.WriteM(frmPlc.Stove5DoorTrip, true); //炉5开门
                                    Step = 1330;
                                }
                                break;
                            case 1330:
                                if (frmPlc.ReadM(frmPlc.Stove5DoorFront))//判断门已打开
                                {
                                    AppendText("机械手——炉5放盘");
                                    mAsynTcpRobot.AsynSend("5boxpt");//炉5放料盘
                                    Step = 1340;
                                }
                                break;
                            case 1340:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("5boxpt"))
                                {
                                    AppendText("固化炉——炉5关门");
                                    frmPlc.WriteM(frmPlc.Stove5DoorTrip, false); //炉5关门
                                    // Thread.Sleep(4000);
                                    // frmPlc.WriteM(frmPlc.Stove5DoorTrip, true); //炉5关门
                                    Step = 1350;
                                }
                                break;
                            case 1350:
                                if (frmPlc.ReadM(frmPlc.Stove5DoorQueen))
                                {
                                    //添加新料盘信息
                                    RunPara.Instance.Stove[4].StoveNo = 5;
                                    RunPara.Instance.Stove[4].AnyMaterialTary = true;
                                    RunPara.Instance.Stove[4].Tray.TrayCode = RunPara.Instance.OKTary.TrayCode;
                                    for (int i = 0; i < RunPara.Instance.OKTary.QRCode.Length; i++)
                                    {
                                        RunPara.Instance.Stove[4].Tray.QRCode[i].SN = RunPara.Instance.OKTary.QRCode[i].SN;
                                        RunPara.Instance.Stove[4].Tray.QRCode[i].sign = RunPara.Instance.OKTary.QRCode[i].sign;
                                        RunPara.Instance.Stove[4].Tray.QRCode[i].dgvRows = RunPara.Instance.OKTary.QRCode[i].dgvRows;
                                        RunPara.Instance.Stove[4].Tray.QRCode[i].scantime = RunPara.Instance.OKTary.QRCode[i].scantime;//1007
                                        RunPara.Instance.Stove[4].Tray.QRCode[i].endtime = new DateTime();//1007
                                        RunPara.Instance.Stove[4].Tray.QRCode[i].timespanScan = 0;//1007
                                        RunPara.Instance.Stove[4].Tray.QRCode[i].A2C = RunPara.Instance.OKTary.QRCode[i].A2C;//1007
                                        RunPara.Instance.Stove[4].Tray.productInfo = RunPara.Instance.OKTary.productInfo;//1007

                                        RunPara.Instance.OKTary.QRCode[i].SN = string.Empty;
                                        RunPara.Instance.OKTary.QRCode[i].sign = false;
                                        RunPara.Instance.OKTary.QRCode[i].dgvRows = -1;
                                        RunPara.Instance.OKTary.QRCode[i].scantime = new DateTime();//1007
                                        RunPara.Instance.OKTary.QRCode[i].A2C = "";
                                        RunPara.Instance.OKTary.productInfo = "";
                                    }
                                    //  RunPara.Instance.Stove[4].Tray.starttime = DateTime.Now;
                                    RunPara.Instance.Stove[4].ifcaltime = true;
                                    RunPara.Instance.OKTary.ProductPos = 0;
                                    RefreshDataDataGridView?.Invoke(this, RefreshDataGridView.UpStove);

                                    frmPlc.WriteM(frmPlc.Stove5AutoStart, true); //自动运行
                                    AppendText("机械手——返回待命位");
                                    mAsynTcpRobot.AsynSend("waitps");//机器人回待料位
                                    Step = 1360;
                                }
                                break;
                            case 1360:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("waitps"))
                                {
                                    Marking.UpStoveRefreshState[4] = false;
                                    Step = 0;
                                }
                                break;
                            #endregion
                            #region 上炉6(节点1400~1500)
                            case 1400:
                                if (!Global.Instance.Stove6AnyMaterial)
                                {
                                    AppendText("机械手——OK位取盘");
                                    mAsynTcpRobot.AsynSend("1plapk"); //OK位取料盘
                                }
                                else
                                    MessageBox.Show("上炉6异常");
                                //   mAsynTcpRobot.AsynSend("1plapk");//OK位取料盘
                                Step = 1410;
                                break;
                            case 1410:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("1plapk"))
                                {
                                    AppendText("机械手——移至炉6放盘位");
                                    mAsynTcpRobot.AsynSend("6boxpw");//机器人移至炉6上方
                                    Step = 1420;
                                }
                                break;
                            case 1420:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("6boxpw"))
                                {
                                    AppendText("固化炉——炉6开门");
                                    frmPlc.WriteM(frmPlc.Stove6DoorTrip, true); //炉6开门
                                    //    Thread.Sleep(4000);
                                    // frmPlc.WriteM(frmPlc.Stove6DoorTrip, true); //炉6开门
                                    Step = 1430;
                                }
                                break;
                            case 1430:
                                if (frmPlc.ReadM(frmPlc.Stove6DoorFront))//判断门已打开
                                {
                                    AppendText("机械手——炉6放盘");
                                    mAsynTcpRobot.AsynSend("6boxpt");//炉6放料盘
                                    Step = 1440;
                                }
                                break;
                            case 1440:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("6boxpt"))
                                {
                                    AppendText("固化炉——炉6关门");
                                    frmPlc.WriteM(frmPlc.Stove6DoorTrip, false); //炉6关门
                                    //Thread.Sleep(4000);
                                    //  frmPlc.WriteM(frmPlc.Stove6DoorTrip, true); //炉6关门
                                    Step = 1450;
                                }
                                break;
                            case 1450:
                                if (frmPlc.ReadM(frmPlc.Stove6DoorQueen))
                                {
                                    //添加新料盘信息
                                    RunPara.Instance.Stove[5].StoveNo = 6;
                                    RunPara.Instance.Stove[5].AnyMaterialTary = true;
                                    RunPara.Instance.Stove[5].Tray.TrayCode = RunPara.Instance.OKTary.TrayCode;
                                    for (int i = 0; i < RunPara.Instance.OKTary.QRCode.Length; i++)
                                    {
                                        RunPara.Instance.Stove[5].Tray.QRCode[i].SN = RunPara.Instance.OKTary.QRCode[i].SN;
                                        RunPara.Instance.Stove[5].Tray.QRCode[i].sign = RunPara.Instance.OKTary.QRCode[i].sign;
                                        RunPara.Instance.Stove[5].Tray.QRCode[i].dgvRows = RunPara.Instance.OKTary.QRCode[i].dgvRows;
                                        RunPara.Instance.Stove[5].Tray.QRCode[i].scantime = RunPara.Instance.OKTary.QRCode[i].scantime;//1007
                                        RunPara.Instance.Stove[5].Tray.QRCode[i].endtime = new DateTime();//1007
                                        RunPara.Instance.Stove[5].Tray.QRCode[i].timespanScan = 0;//1007
                                        RunPara.Instance.Stove[5].Tray.QRCode[i].A2C = RunPara.Instance.OKTary.QRCode[i].A2C;//1007
                                        RunPara.Instance.Stove[5].Tray.productInfo = RunPara.Instance.OKTary.productInfo;//1007

                                        RunPara.Instance.OKTary.QRCode[i].SN = string.Empty;
                                        RunPara.Instance.OKTary.QRCode[i].sign = false;
                                        RunPara.Instance.OKTary.QRCode[i].dgvRows = -1;
                                        RunPara.Instance.OKTary.QRCode[i].scantime = new DateTime();//1007
                                        RunPara.Instance.OKTary.QRCode[i].A2C = "";
                                        RunPara.Instance.OKTary.productInfo = "";
                                    }
                                    //  RunPara.Instance.Stove[5].Tray.starttime = DateTime.Now;
                                    RunPara.Instance.Stove[5].ifcaltime = true;
                                    RunPara.Instance.OKTary.ProductPos = 0;
                                    RefreshDataDataGridView?.Invoke(this, RefreshDataGridView.UpStove);

                                    frmPlc.WriteM(frmPlc.Stove6AutoStart, true); //自动运行
                                    AppendText("机械手——返回待命位");
                                    mAsynTcpRobot.AsynSend("waitps");//机器人回待料位
                                    Step = 1460;
                                }
                                break;
                            case 1460:
                                tcpResults = mAsynTcpRobot.Result.Split('\r');
                                if (tcpResults.Length > 0 && tcpResults[0].Contains("waitps"))
                                {
                                    Marking.UpStoveRefreshState[5] = false;
                                    Step = 0;
                                }
                                break;
                            #endregion
                            default:
                                Step = 0;
                                break;
                        }
                    }
                    else
                    {
                        frmPlc.WriteM(frmPlc.StoveLockControl, false);
                        FirstIn = true;
                    }
                    #endregion

                    #region 初始化流程

                    if (stationInitialize.Running)
                    {
                        switch (stationInitialize.Flow)
                        {
                            case 0:
                                //RunPara.Instance.OKTary.QRCode[RunPara.Instance.OKTary.ProductPos].SN = "50394102033496";
                                stationInitialize.InitializeDone = false;
                                stationOperate.RunningSign = false;
                                Step = 0;
                                WishoutDiskWait = true;
                                FirstIn = true;
                                WhileTimes = 1;
                                //Marking.RobotInform = false;
                                Marking.RobotInformOutputting = false;
                                QRCodeReader.StopTrigger();
                                QRCodeReader.receiveFinish = false;
                                stationInitialize.Flow = 10;
                                break; ;
                            case 10:
                                stationInitialize.InitializeDone = true;
                                stationInitialize.Flow = 20;
                                break;
                            default:
                                break;
                        }
                    }

                    #endregion

                    #region 报警复位

                    if (AlarmReset.AlarmReset)
                    {
                        m_Alarm = RobotAlarm.无消息;
                        Marking.AlarmStopThread = true;
                        WhileTimes = 0;
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show("001" + ex.ToString());
                }
            }
        }


        bool ifwait01 = true;
        /// <summary>
        /// 检查条码是否正常
        /// </summary>
        public bool ScanSN(string strSN)
        {
            bool result = true;
            try
            {
                result = Common.Test_ScanSN(RunPara.Instance.IsUseMesLock, strSN);//是否启用MES查询测试
            }
            catch
            {
                result = false;
                LogHelper.Error("SV_Interlocking_Main Error");
            }

            return result;
        }

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

        public void PLCSignal()
        {
            #region PLC
            if (frmPlc.IsConnect)
            {
                Global.Instance.Stove1RunState = frmPlc.ReadInt16Data(frmPlc.Stove1RunState);
                Global.Instance.Stove2RunState = frmPlc.ReadInt16Data(frmPlc.Stove2RunState);
                Global.Instance.Stove3RunState = frmPlc.ReadInt16Data(frmPlc.Stove3RunState);
                Global.Instance.Stove4RunState = frmPlc.ReadInt16Data(frmPlc.Stove4RunState);
                Global.Instance.Stove5RunState = frmPlc.ReadInt16Data(frmPlc.Stove5RunState);
                Global.Instance.Stove6RunState = frmPlc.ReadInt16Data(frmPlc.Stove6RunState);

                Global.Instance.Stove1AnyMaterial = frmPlc.ReadM(frmPlc.Stove1AnyMaterial);
                Global.Instance.Stove2AnyMaterial = frmPlc.ReadM(frmPlc.Stove2AnyMaterial);
                Global.Instance.Stove3AnyMaterial = frmPlc.ReadM(frmPlc.Stove3AnyMaterial);
                Global.Instance.Stove4AnyMaterial = frmPlc.ReadM(frmPlc.Stove4AnyMaterial);
                Global.Instance.Stove5AnyMaterial = frmPlc.ReadM(frmPlc.Stove5AnyMaterial);
                Global.Instance.Stove6AnyMaterial = frmPlc.ReadM(frmPlc.Stove6AnyMaterial);

                RunPara.Instance.Stove[0].Temperature = frmPlc.ReadInt16Data(frmPlc.Stove1Temperature);
                RunPara.Instance.Stove[1].Temperature = frmPlc.ReadInt16Data(frmPlc.Stove2Temperature);
                RunPara.Instance.Stove[2].Temperature = frmPlc.ReadInt16Data(frmPlc.Stove3Temperature);
                RunPara.Instance.Stove[3].Temperature = frmPlc.ReadInt16Data(frmPlc.Stove4Temperature);
                RunPara.Instance.Stove[4].Temperature = frmPlc.ReadInt16Data(frmPlc.Stove5Temperature);
                RunPara.Instance.Stove[5].Temperature = frmPlc.ReadInt16Data(frmPlc.Stove6Temperature);
            }
            #endregion

            #region 上炉刷新状态                        

            if ((0 == Global.Instance.Stove1RunState || 2 == Global.Instance.Stove1RunState) && !Marking.DownStoveRefreshState[0]
                && !Global.Instance.Stove1AnyMaterial && frmPlc.ReadM(frmPlc.Stove1AllowHouse))
            {
                Marking.UpStoveRefreshState[0] = true;
            }
            else
            {
                Marking.UpStoveRefreshState[0] = false;
            }
            if ((0 == Global.Instance.Stove2RunState || 2 == Global.Instance.Stove2RunState) && !Marking.DownStoveRefreshState[1]
                && !Global.Instance.Stove2AnyMaterial && frmPlc.ReadM(frmPlc.Stove2AllowHouse))
            {
                Marking.UpStoveRefreshState[1] = true;
            }
            else
            {
                Marking.UpStoveRefreshState[1] = false;
            }
            if ((0 == Global.Instance.Stove3RunState || 2 == Global.Instance.Stove3RunState) && !Marking.DownStoveRefreshState[2]
                && !Global.Instance.Stove3AnyMaterial && frmPlc.ReadM(frmPlc.Stove3AllowHouse))
            {
                Marking.UpStoveRefreshState[2] = true;
            }
            else
            {
                Marking.UpStoveRefreshState[2] = false;
            }
            if ((0 == Global.Instance.Stove4RunState || 2 == Global.Instance.Stove4RunState) && !Marking.DownStoveRefreshState[3]
                && !Global.Instance.Stove4AnyMaterial && frmPlc.ReadM(frmPlc.Stove4AllowHouse))
            {
                Marking.UpStoveRefreshState[3] = true;
            }
            else
            {
                Marking.UpStoveRefreshState[3] = false;
            }
            if ((0 == Global.Instance.Stove5RunState || 2 == Global.Instance.Stove5RunState) && !Marking.DownStoveRefreshState[4]
                && !Global.Instance.Stove5AnyMaterial && frmPlc.ReadM(frmPlc.Stove5AllowHouse))
            {
                Marking.UpStoveRefreshState[4] = true;
            }
            else
            {
                Marking.UpStoveRefreshState[4] = false;
            }

            if ((0 == Global.Instance.Stove6RunState || 2 == Global.Instance.Stove6RunState) && !Marking.DownStoveRefreshState[5]
                && !Global.Instance.Stove6AnyMaterial && frmPlc.ReadM(frmPlc.Stove6AllowHouse))
            {
                Marking.UpStoveRefreshState[5] = true;
            }
            else
            {
                Marking.UpStoveRefreshState[5] = false;
            }
            #endregion

            #region 下炉刷新状态
            //***
            if (2 == Global.Instance.Stove1RunState && !Marking.UpStoveRefreshState[0] && Global.Instance.Stove1AnyMaterial)
            {
                if (2 == Global.Instance.Stove1RunState)
                {
                    Marking.DownStoveRefreshState[0] = true;
                }
                else
                {
                    Marking.DownStoveRefreshState[0] = false;
                }
            }
            else
            {
                Marking.DownStoveRefreshState[0] = false;
            }
            //***
            if (2 == Global.Instance.Stove2RunState && !Marking.UpStoveRefreshState[1] && Global.Instance.Stove2AnyMaterial)
            {
                if (2 == Global.Instance.Stove2RunState)
                {
                    Marking.DownStoveRefreshState[1] = true;
                }
                else
                {
                    Marking.DownStoveRefreshState[1] = false;
                }
            }
            else
            {
                Marking.DownStoveRefreshState[1] = false;
            }
            //***
            if (2 == Global.Instance.Stove3RunState && !Marking.UpStoveRefreshState[2] && Global.Instance.Stove3AnyMaterial)
            {
                if (2 == Global.Instance.Stove3RunState)
                {
                    Marking.DownStoveRefreshState[2] = true;
                }
                else
                {
                    Marking.DownStoveRefreshState[2] = false;
                }
            }
            else
            {
                Marking.DownStoveRefreshState[2] = false;
            }
            //***
            if (2 == Global.Instance.Stove4RunState && !Marking.UpStoveRefreshState[3] && Global.Instance.Stove4AnyMaterial)
            {
                if (2 == Global.Instance.Stove4RunState)
                {
                    Marking.DownStoveRefreshState[3] = true;
                }
                else
                {
                    Marking.DownStoveRefreshState[3] = false;
                }
            }
            else
            {
                Marking.DownStoveRefreshState[3] = false;
            }
            //***
            if (2 == Global.Instance.Stove5RunState && !Marking.UpStoveRefreshState[4] && Global.Instance.Stove5AnyMaterial)
            {
                if (2 == Global.Instance.Stove5RunState)
                {
                    Marking.DownStoveRefreshState[4] = true;
                }
                else
                {
                    Marking.DownStoveRefreshState[4] = false;
                }
            }
            else
            {
                Marking.DownStoveRefreshState[4] = false;
            }
            //***
            if (2 == Global.Instance.Stove6RunState && !Marking.UpStoveRefreshState[5] && Global.Instance.Stove6AnyMaterial)
            {
                if (2 == Global.Instance.Stove6RunState)
                {
                    Marking.DownStoveRefreshState[5] = true;
                }
                else
                {
                    Marking.DownStoveRefreshState[5] = false;
                }
            }
            else
            {
                Marking.DownStoveRefreshState[5] = false;
            }

            #endregion

            #region 炉1固化数据获取

            if (1 == Global.Instance.Stove1RunState)
            {
                if (RunPara.Instance.MesStoveTemperature.min <= RunPara.Instance.Stove[0].Temperature && !Marking.StartTemperatureSign[0])
                {
                    Marking.StartTemperatureSign[0] = true;
                }
                if (Marking.StartTemperatureSign[0])
                {
                    if (Marking.FirstTemperatureSign[0])
                    {
                        Marking.FirstTemperatureSign[0] = false;
                        RunPara.Instance.AverageTemperature[0] = RunPara.Instance.Stove[0].Temperature;
                    }
                    else  //平均固化温度
                    {
                        RunPara.Instance.AverageTemperature[0] = (RunPara.Instance.AverageTemperature[0] + RunPara.Instance.Stove[0].Temperature) / 2;
                    }
                }
            }

            #endregion

            #region 炉2固化数据获取
            if (1 == Global.Instance.Stove2RunState)
            {
                if (RunPara.Instance.MesStoveTemperature.min <= RunPara.Instance.Stove[1].Temperature && !Marking.StartTemperatureSign[1])
                {
                    Marking.StartTemperatureSign[1] = true;
                }
                if (Marking.StartTemperatureSign[1])
                {
                    if (Marking.FirstTemperatureSign[1])
                    {
                        Marking.FirstTemperatureSign[1] = false;
                        RunPara.Instance.AverageTemperature[1] = RunPara.Instance.Stove[1].Temperature;
                    }
                    else  //平均固化温度
                    {
                        RunPara.Instance.AverageTemperature[1] = (RunPara.Instance.AverageTemperature[1] + RunPara.Instance.Stove[1].Temperature) / 2;
                    }
                }
            }

            #endregion

            #region 炉3固化数据获取
            if (1 == Global.Instance.Stove3RunState)
            {
                //refreshTemperatureChart(3, StoveStartTime.ElapsedMilliseconds / 1000, RunPara.Instance.Stove[2].Temperature);
                if (RunPara.Instance.MesStoveTemperature.min <= RunPara.Instance.Stove[2].Temperature && !Marking.StartTemperatureSign[2])
                {
                    Marking.StartTemperatureSign[2] = true;
                }
                if (Marking.StartTemperatureSign[2])
                {
                    if (Marking.FirstTemperatureSign[2])
                    {
                        Marking.CuringTime[2].Restart();  //开始固化计时
                        Marking.FirstTemperatureSign[2] = false;
                        RunPara.Instance.AverageTemperature[2] = RunPara.Instance.Stove[2].Temperature;
                    }
                    else  //平均固化温度
                    {
                        RunPara.Instance.AverageTemperature[2] = (RunPara.Instance.AverageTemperature[2] + RunPara.Instance.Stove[2].Temperature) / 2;
                    }
                }
            }

            #endregion

            #region 炉4固化数据获取
            if (1 == Global.Instance.Stove4RunState)
            {
                //refreshTemperatureChart(4, StoveStartTime.ElapsedMilliseconds / 1000, RunPara.Instance.Stove[3].Temperature);
                if (RunPara.Instance.MesStoveTemperature.min <= RunPara.Instance.Stove[3].Temperature && !Marking.StartTemperatureSign[3])
                {
                    Marking.StartTemperatureSign[3] = true;
                }
                if (Marking.StartTemperatureSign[3])
                {
                    if (Marking.FirstTemperatureSign[3])
                    {
                        Marking.CuringTime[3].Restart();  //开始固化计时
                        Marking.FirstTemperatureSign[3] = false;
                        RunPara.Instance.AverageTemperature[3] = RunPara.Instance.Stove[3].Temperature;
                    }
                    else  //平均固化温度
                    {
                        RunPara.Instance.AverageTemperature[3] = (RunPara.Instance.AverageTemperature[3] + RunPara.Instance.Stove[3].Temperature) / 2;
                    }
                }
            }

            #endregion

            #region 炉5固化数据获取
            if (1 == Global.Instance.Stove5RunState)
            {
                //refreshTemperatureChart(5, StoveStartTime.ElapsedMilliseconds / 1000, RunPara.Instance.Stove[4].Temperature);
                if (RunPara.Instance.MesStoveTemperature.min <= RunPara.Instance.Stove[4].Temperature && !Marking.StartTemperatureSign[4])
                {
                    Marking.StartTemperatureSign[4] = true;
                }
                if (Marking.StartTemperatureSign[4])
                {
                    if (Marking.FirstTemperatureSign[4])
                    {
                        Marking.CuringTime[4].Restart();  //开始固化计时
                        Marking.FirstTemperatureSign[4] = false;
                        RunPara.Instance.AverageTemperature[4] = RunPara.Instance.Stove[4].Temperature;
                    }
                    else  //平均固化温度
                    {
                        RunPara.Instance.AverageTemperature[4] = (RunPara.Instance.AverageTemperature[4] + RunPara.Instance.Stove[4].Temperature) / 2;
                    }
                }
            }

            #endregion

            #region 炉6固化数据获取
            if (1 == Global.Instance.Stove6RunState)
            {
                //refreshTemperatureChart(6, StoveStartTime.ElapsedMilliseconds / 1000, RunPara.Instance.Stove[5].Temperature);
                if (RunPara.Instance.MesStoveTemperature.min <= RunPara.Instance.Stove[5].Temperature && !Marking.StartTemperatureSign[5])
                {
                    Marking.StartTemperatureSign[5] = true;
                }
                if (Marking.StartTemperatureSign[5])
                {
                    if (Marking.FirstTemperatureSign[5])
                    {
                        Marking.CuringTime[5].Restart();  //开始固化计时
                        Marking.FirstTemperatureSign[5] = false;
                        RunPara.Instance.AverageTemperature[5] = RunPara.Instance.Stove[5].Temperature;
                    }
                    else  //平均固化温度
                    {
                        RunPara.Instance.AverageTemperature[5] = (RunPara.Instance.AverageTemperature[5] + RunPara.Instance.Stove[5].Temperature) / 2;
                    }
                }
            }

            #endregion

        }

        /// <summary>
        /// 流程报警集合
        /// </summary>
        protected override IList<Alarm> alarms()
        {
            var list = new List<Alarm>();

            list.Add(new Alarm(() => m_Alarm == RobotAlarm.初始化故障)
            {
                AlarmLevel = AlarmLevels.Error,
                Name = RobotAlarm.初始化故障.ToString()
            });
            list.Add(new Alarm(() => m_Alarm == RobotAlarm.NG料盘已满_清料)
            {
                AlarmLevel = AlarmLevels.Error,
                Name = RobotAlarm.NG料盘已满_清料.ToString()
            });
            list.Add(new Alarm(() => m_Alarm == RobotAlarm.待料位满盘报警)
            {
                AlarmLevel = AlarmLevels.Error,
                Name = RobotAlarm.待料位满盘报警.ToString()
            });
            list.Add(new Alarm(() => m_Alarm == RobotAlarm.无盘等待超时报警)
            {
                AlarmLevel = AlarmLevels.Error,
                Name = RobotAlarm.无盘等待超时报警.ToString()
            });
            list.Add(new Alarm(() => m_Alarm == RobotAlarm.连续NG报警)
            {
                AlarmLevel = AlarmLevels.Error,
                Name = RobotAlarm.连续NG报警.ToString()
            });
            list.Add(new Alarm(() => m_Alarm == RobotAlarm.无法上炉请等待)
            {
                AlarmLevel = AlarmLevels.Error,
                Name = RobotAlarm.无法上炉请等待.ToString()
            });
            list.Add(new Alarm(() => m_Alarm == RobotAlarm.料盘条码丢失)
            {
                AlarmLevel = AlarmLevels.Error,
                Name = RobotAlarm.料盘条码丢失.ToString()
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


        public enum RobotAlarm : int
        {
            无消息,
            初始化故障,
            NG料盘已满_清料,
            待料位满盘报警,
            无盘等待超时报警,
            连续NG报警,
            无法上炉请等待,
            料盘条码丢失,
        }
    }
}
