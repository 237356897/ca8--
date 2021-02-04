using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Toolkit;
using System.Toolkit.Helpers;
using System.Device;
using log4net;
using System.Threading;
using License;
using System.Windows.Forms.DataVisualization.Charting;
using Motion.AdlinkAps;
using Motion.Enginee;
using Motion.Interfaces;
using System.Diagnostics;
using CVMes;
using System.IO;

namespace Desay
{
    public partial class FrmMain : Form
    {
        private static ILog log = LogManager.GetLogger(typeof(FrmMain));
        /// <summary>
        /// 初始化提示信息
        /// </summary>
        private event Action<string> LoadingMessage;
        private AlarmType MachineIsAlarm, BackflowAlarm, RobotAlarm;
        //外部条件
        private External m_External = new External();
        private MachineOperate MachineOperation;
        private Backflow m_Backflow;
        private Robot m_Robot;
        private FrmManualPLC FrmPlc;
        private DM100Q QRCodeCom;
        private DM100Q TrayCodeCom;
        private AsynTcpClient AsynTcpRobot;

        private LightButton StartButton, StopButton, PauseButton, ResetButton;
        private EventButton EstopButton;
        private LayerLight layerLight;
        private bool ManualAutoMode;
        private Thread threadMachineRun = null;
        private Thread threadAlarmCheck = null;
        private Thread threadStatusCheck = null;
        /// <summary>
        /// 是否NG标志(空跑模式)
        /// </summary>
        public static int prepos = 0;


        /// <summary>
        /// 设备运行
        /// </summary>
        public bool isRun = false;
        /// <summary>
        /// 设备复位
        /// </summary>
        public bool isReset = false;
        /// <summary>
        /// 炉开启时间
        /// </summary>
        public Stopwatch StoveStartTime = new Stopwatch();

        /// <summary>
        /// 加密狗
        /// </summary>
        private Thread threadLicenseCheck = null;
        Encryption hasp;
        Registered HaspRegistered;
        /// <summary>
        /// 加密狗屏蔽 true - 屏蔽，false - 不屏蔽
        /// </summary>
        private bool LicenseSheild = true;

        public FrmMain()
        {
            InitializeComponent();

            hasp = new Encryption();
            hasp.ShowWindows += ShowWindows;
            hasp.MachineName = "0F69";
            HaspRegistered = new Registered(hasp);
        }

        #region 用户权限

        void UserLevelChange(UserLevel level)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<UserLevel>(UserLevelChange), level);
            }
            else
            {
                switch (level)
                {
                    case UserLevel.操作员:
                        btnMain.Enabled = true;
                        btnIOMonitor.Enabled = true;
                        btnPLCMonitor.Enabled = false;
                        btnRecipe.Enabled = false;
                        btnOperation.Enabled = false;
                        btnQRCodeSet.Enabled = false;
                        btnTrayCodeSet.Enabled = false;
                        btnMes.Enabled = false;
                        break;
                    case UserLevel.工程师:
                        btnMain.Enabled = true;
                        btnIOMonitor.Enabled = true;
                        btnPLCMonitor.Enabled = true;
                        btnRecipe.Enabled = true;
                        btnOperation.Enabled = true;
                        btnQRCodeSet.Enabled = true;
                        btnTrayCodeSet.Enabled = true;
                        btnMes.Enabled = true;
                        break;
                    default:
                        btnMain.Enabled = true;
                        btnIOMonitor.Enabled = true;
                        btnPLCMonitor.Enabled = false;
                        btnRecipe.Enabled = false;
                        btnOperation.Enabled = false;
                        btnQRCodeSet.Enabled = false;
                        btnTrayCodeSet.Enabled = false;
                        btnMes.Enabled = false;
                        break;
                }
            }
        }

        #endregion

        #region 初始化


        List<bool> firsttemp = new List<bool>();
        private void FrmMain_Load(object sender, EventArgs e)
        {
            firsttemp.Add(true);
            firsttemp.Add(true);
            firsttemp.Add(true);
            firsttemp.Add(true);
            firsttemp.Add(true);
            firsttemp.Add(true);


            #region 初始化进度界面

            new Thread(new ThreadStart(() =>
            {
                frmStarting loading = new frmStarting(12); //11
                LoadingMessage += new Action<string>(loading.ShowMessage);
                loading.ShowDialog();
            })).Start();
            Thread.Sleep(500);
            RunPara.Instance.cbAuto=false;
            //UserLevelChange(Marking.userLevel);
            UserLevelChange(UserLevel.工程师);            

            #endregion

            #region 通信初始化

            LoadingMessage("初始化PLC通信");
            try
            {
                FrmManualPLC.AppendLog += AppendText;
                FrmPlc = new FrmManualPLC(Global.Instance.PLCMX.ToString(), "M114", 16, "M316", 16, tbcMain);
            }
            catch (Exception ex)
            {
                appendText($"PLC连接失败：{ex.Message}");
            }
            if (!FrmPlc.IsConnect) appendText("PLC连接失败");

            LoadingMessage("初始化产品扫码器");
            QRCodeCom = new DM100Q()
            {
                Name = "产品扫码器通信"
            };
            try
            {
                QRCodeCom.SetConnectionParam(Global.Instance.QRCodeComParam);
                QRCodeCom.DeviceDataReceiveCompelete += new DataReceiveCompleteEventHandler(DealWithQRCodeReceiveData);
                QRCodeCom.DeviceOpen();
            }
            catch (Exception ex)
            {
                appendText(string.Format("{0}连接失败：{1}", QRCodeCom.Name, ex.Message));
            }

            LoadingMessage("初始化料盘扫码器");
            TrayCodeCom = new DM100Q()
            {
                Name = "料盘扫码器通信"
            };
            try
            {
                TrayCodeCom.SetConnectionParam(Global.Instance.TrayCodeComParam);
                TrayCodeCom.DeviceDataReceiveCompelete += new DataReceiveCompleteEventHandler(DealWithTrayCodeReceiveData);
                TrayCodeCom.DeviceOpen();
            }
            catch (Exception ex)
            {
                appendText(string.Format("{0}连接失败：{1}", TrayCodeCom.Name, ex.Message));
            }

            LoadingMessage("连接机器人");
            try
            {
                Stopwatch RobotTime = new Stopwatch();
                RobotTime.Restart();
                AsynTcpRobot = new AsynTcpClient(Global.Instance.RobotIP, Global.Instance.RobotPort);
                do
                {
                    AsynTcpRobot.AsynConnect();
                    Thread.Sleep(10);
                }
                while (!AsynTcpRobot.IsConnected && RobotTime.ElapsedMilliseconds < 3000);

                if (!AsynTcpRobot.IsConnected) appendText("机器人连接失败");
            }
            catch (Exception ex)
            {
                appendText($"机器人连接失败：{ex.Message}");
            }

            #endregion

            #region  加载板卡

            LoadingMessage("加载板卡信息");

            try
            {
                IoPoints.m_ApsController.Initialize();
                IoPoints.m_ApsController.LoadParamFromFile(AppConfig.CardParamFilePath);
                IoPoints.m_DaskController.Initialize();
            }
            catch (Exception ex)
            {
                log.Fatal("板卡初始化失败：" + ex.Message);
                AppendText("板卡初始化失败！请检查硬件！");
            }

            #endregion

            #region 初始化轴

            LoadingMessage("初始化轴");

            var carryAxis = new StepAxis(IoPoints.m_ApsController)
            {
                NoId = 0,
                Name = "输送轴",
                Transmission = AxisParameter.Instance.CarrytransParams,
            };
            carryAxis.SetAxisHomeParam(new HomeParams(0, 1, 0, 40000, 4000, 0));

            Thread.Sleep(100);

            #endregion

            #region 初始化气缸

            LoadingMessage("初始化气缸");

            #endregion

            #region 加载模组操作资源

            LoadingMessage("加载模组操作资源");
            layerLight = new LayerLight(IoPoints.I2DO02, IoPoints.I2DO01, IoPoints.I2DO00, IoPoints.I2DO03);

            var BackflowInitialize = new StationInitialize(
                () => { return !ManualAutoMode/* && (hasp.LicenseIsOK && !LicenseSheild)*/; },
                () => { return BackflowAlarm.IsAlarm; });
            var BackflowOperate = new StationOperate(
                () => { return BackflowInitialize.InitializeDone/* && (hasp.LicenseIsOK && !LicenseSheild)*/; },
                () => { return BackflowAlarm.IsAlarm; });

            var RobotInitialize = new StationInitialize(
                () => { return !ManualAutoMode/* && (hasp.LicenseIsOK && !LicenseSheild)*/; },
                () => { return RobotAlarm.IsAlarm; });
            var RobotOperate = new StationOperate(
                () => { return RobotInitialize.InitializeDone/* && (hasp.LicenseIsOK && !LicenseSheild)*/; },
                () => { return RobotAlarm.IsAlarm; });

            m_Backflow = new Backflow(m_External, BackflowInitialize, BackflowOperate)
            {
                CarryAxis = carryAxis,
                frmPlc = FrmPlc,
                TrayCodeReader = TrayCodeCom,
                Light = layerLight
            };
            m_Backflow.AddPart();
            m_Backflow.Run(RunningModes.Online);
            m_Backflow.StationAppendTextReceive += new System.Toolkit.Interfaces.DataReceiveCompleteEventHandler(DealWithReceiveData);

            m_Robot = new Robot(m_External, RobotInitialize, RobotOperate)
            {
                frmPlc = FrmPlc,
                mAsynTcpRobot = AsynTcpRobot,
                QRCodeReader = QRCodeCom
            };
            m_Robot.AddPart();
            m_Robot.Run(RunningModes.Online);
            m_Robot.RefreshDataDataGridView += new RefreshDataCompleteEventHandler(DealWithDGVReceiveData);
            m_Robot.StationAppendTextReceive += new System.Toolkit.Interfaces.DataReceiveCompleteEventHandler(DealWithReceiveData);
            //m_StoveRefresh.RefreshDataDataGridView += new RefreshDataCompleteEventHandler(DealWithDGVReceiveData);

            MachineOperation = new MachineOperate(() =>
            {
                return BackflowInitialize.InitializeDone && RobotInitialize.InitializeDone /*&& (hasp.LicenseIsOK && !LicenseSheild)*/;
            },
           () =>
            {
                return MachineIsAlarm.IsAlarm | BackflowAlarm.IsAlarm | RobotAlarm.IsAlarm;
            });

            Thread.Sleep(100);

            #endregion            

            #region 加载信号灯资源

            LoadingMessage("加载信号灯资源");
            StartButton = new LightButton(IoPoints.I2DI00, IoPoints.I2DO04);
            ResetButton = new LightButton(IoPoints.I2DI02, IoPoints.I2DO06);
            PauseButton = new LightButton(IoPoints.I2DI01, IoPoints.I2DO05);
            StopButton = new LightButton(IoPoints.I2DI14, IoPoints.I2DO14);
            EstopButton = new EventButton(IoPoints.I2DI03);
            

            StartButton.Pressed += btnStart_MouseDown;
            StartButton.Released += btnStart_MouseUp;
            PauseButton.Pressed += btnPause_MouseDown;
            PauseButton.Released += btnPause_MouseUp;
            ResetButton.Pressed += btnReset_MouseDown;
            ResetButton.Released += btnReset_MouseUp;
            StopButton.Pressed += btnStop_MouseDown;
            StopButton.Released += btnStop_MouseUp;

            MachineOperation.StartButton = StartButton;
            MachineOperation.PauseButton = PauseButton;
            MachineOperation.StopButton = StopButton;
            MachineOperation.ResetButton = ResetButton;
            MachineOperation.EstopButton = EstopButton;
            #endregion

            #region 初始化窗体

            LoadingMessage("初始化窗体");

            AddSubForm();

            #endregion

            #region 初始化线程

            LoadingMessage("初始化线程");

            threadMachineRun = new Thread(MachineRun);
            threadMachineRun.IsBackground = true;
            threadMachineRun.Start();
            if (threadMachineRun.IsAlive) AppendText("设备操作线程运行中...");

            threadAlarmCheck = new Thread(AlarmCheck);
            threadAlarmCheck.IsBackground = true;
            threadAlarmCheck.Start();
            if (threadAlarmCheck.IsAlive) AppendText("故障检查线程运行中...");

            threadStatusCheck = new Thread(StatusCheck);
            threadStatusCheck.IsBackground = true;
            threadStatusCheck.Start();
            if (threadStatusCheck.IsAlive) AppendText("状态检查线程运行中...");

            threadLicenseCheck = new Thread(LicenseCheck);
            threadLicenseCheck.IsBackground = true;
            threadLicenseCheck.Start();
            //if (threadStatusCheck.IsAlive) AppendText("加密检查线程运行中...");

            #endregion

            #region 刷新状态

            LoadingMessage("刷新状态");
            //dgvStoveResult dgvTrayResult表格刷新
            DGVLoad();
            rbHand.Checked = true;
            rbAuto.Checked = false;
            ManualAutoMode = false;
            this.refreshStateTimer.Enabled = true;
            this.refreshDatatimer.Enabled = true;

            #endregion

            #region SV MES
            //SV mes
            Common.ReadCommonIniFile();

            #endregion

            int n1 = 0;
            if (RunPara.Instance.OKTary.ProductPos == RunPara.Instance.TrayPoint + 1)
            {
                foreach (var si in RunPara.Instance.OKTary.QRCode)
                {
                    if (si.sign)
                    {
                        n1++;
                    }
                }
                if (n1 == RunPara.Instance.TrayPoint)
                {
                    RunPara.Instance.OKTary.ProductPos = RunPara.Instance.TrayPoint;
                }
                else
                {
                    MessageBox.Show("OK盘重新计数，请取走所有料后开始");

                    RunPara.Instance.OKTary.ProductPos = 0;
                }
            }

            try
            {
                if (RunPara.Instance.ShieldEntraceGuard)
                {
                    IoPoints.I2DO29.Value = false;
                }
                else
                {
                    IoPoints.I2DO29.Value = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region 线程响应

        #region MachineRun
        private void MachineRun()
        {
            while (true)
            {
                Thread.Sleep(100);
                m_External.AirSignal = !IoPoints.I2DI06.Value;
                m_External.ManualAutoMode = ManualAutoMode;

                MachineOperation.ManualAutoModel = ManualAutoMode;
                MachineOperation.CleanProductDone = Marking.CleanProductDone;
                MachineOperation.Run();

                layerLight.Status = MachineOperation.Status;
                layerLight.Refreshing();

                m_Backflow.stationOperate.ManualAutoMode = ManualAutoMode;
                m_Backflow.stationOperate.AutoRun = MachineOperation.Running;
                m_Backflow.stationInitialize.Run();
                m_Backflow.stationOperate.Run();

                m_Robot.stationOperate.ManualAutoMode = ManualAutoMode;
                m_Robot.stationOperate.AutoRun = MachineOperation.Running;
                m_Robot.stationInitialize.Run();
                m_Robot.stationOperate.Run();

                #region 按钮灯响应
                IoPoints.I2DO04.Value = IoPoints.I2DI00.Value;
                //IoPoints.I2DO05.Value = IoPoints.I2DI01.Value;
                //IoPoints.I2DO06.Value = IoPoints.I2DI02.Value;
                IoPoints.I2DO07.Value = IoPoints.I2DI25.Value;
                #endregion

                #region 设备运行中
                if (MachineOperation.Running)
                {
                    IoPoints.I2DO04.Value = true;
                    IoPoints.I2DO05.Value = false;
                }
                #endregion

                #region 设备暂停中
                if (MachineOperation.Pausing)
                {
                    IoPoints.I2DO04.Value = false;
                    IoPoints.I2DO05.Value = true;
                }
                #endregion

                #region 设备复位中

                if (MachineOperation.Resetting)
                {
                    switch (MachineOperation.Flow)
                    {
                        case 0:
                            m_External.InitializingDone = false;
                            MachineOperation.IniliazieDone = false;
                            m_Backflow.stationInitialize.InitializeDone = false;
                            m_Robot.stationInitialize.InitializeDone = false;
                            m_Backflow.stationInitialize.Start = false;
                            m_Robot.stationInitialize.Start = false;
                            IoPoints.I2DO04.Value = false;
                            IoPoints.I2DO05.Value = false;
                            IoPoints.I2DO06.Value = true;
                            MachineOperation.Flow = 10;
                            break;
                        case 10:
                            m_Robot.stationInitialize.Start = true;
                            if (m_Robot.stationInitialize.Running)
                            {
                                MachineOperation.Flow = 20;
                            }
                            break;
                        case 20:
                            if (m_Robot.stationInitialize.Flow == -1)
                            {
                                MachineOperation.IniliazieDone = false;
                                MachineOperation.Flow = -1;
                            }
                            else if (m_Robot.stationInitialize.InitializeDone)
                            {

                                m_Backflow.stationInitialize.Start = true;
                                if (m_Backflow.stationInitialize.Running)
                                {
                                    MachineOperation.Flow = 30;
                                    _watch.Restart();
                                }
                            }
                            break;
                        case 30:
                            if (m_Backflow.stationInitialize.Flow == -1)
                            {
                                MachineOperation.IniliazieDone = false;
                                MachineOperation.Flow = -1;
                            }
                            else if (m_Backflow.stationInitialize.InitializeDone)
                            {
                                MachineOperation.IniliazieDone = true;
                                MachineOperation.Flow = 40;
                                IoPoints.I2DO06.Value = false;
                            }

                            if (_watch.ElapsedMilliseconds > 60000)
                            {
                                string res = "";
                                if (m_Backflow.stationInitialize.InitializeDone)
                                {
                                    res += res + "输出轴复位异常";
                                }
                                AppendText(res);
                                // MessageBox.Show(res);
                            }
                            break;
                        default:
                            m_Backflow.stationInitialize.Start = false;
                            m_Robot.stationInitialize.Start = false;

                            break;
                    }
                }

                #endregion

                #region 设备停止中
                if (MachineOperation.Stopping)
                {
                    m_Backflow.CarryAxis.Stop();
                    m_Backflow.stationInitialize.Estop = true;
                    m_Robot.stationInitialize.Estop = true;
                    IoPoints.I2DO04.Value = false;
                    IoPoints.I2DO05.Value = false;
                    IoPoints.I2DO06.Value = false;

                    if (!m_Backflow.stationInitialize.Running  && !m_Robot.stationInitialize.Running)
                    {
                        MachineOperation.IniliazieDone = false;
                        MachineOperation.Stopping = false;
                        m_Backflow.stationInitialize.Estop = false;
                        m_Robot.stationInitialize.Estop = false;
                    }
                }
                #endregion

                #region 设备急停中
                if (!EstopButton.PressedIO.Value)
                {
                    m_Backflow.CarryAxis.Stop();
                    m_Backflow.stationInitialize.InitializeDone = false;
                    m_Robot.stationInitialize.InitializeDone = false;
                    MachineOperation.IniliazieDone = false;
                    IoPoints.I2DO04.Value = false;
                    IoPoints.I2DO05.Value = false;
                    IoPoints.I2DO06.Value = false;


                    //ABB停止
                    IoPoints.I2DO12.Value = true;
                    Thread.Sleep(500);
                    IoPoints.I2DO12.Value = false;
                }
                #endregion
            }
        }
        #endregion

        #region AlarmCheck
        Stopwatch _watch = new Stopwatch();
        private void AlarmCheck()
        {

            while (true)
            {
                Thread.Sleep(1000);

                var list = new List<Alarm>();

                list.Add(new Alarm(() => !IoPoints.I2DI03.Value)
                {
                    AlarmLevel = AlarmLevels.Error,
                    Name = "急停按钮已按下，注意安全！"
                });
                list.Add(new Alarm(() => IoPoints.I2DI07.Value)
                {
                    AlarmLevel = AlarmLevels.Error,
                    Name = "机器人报警"
                });
                list.Add(new Alarm(() => !IoPoints.I2DI04.Value && !RunPara.Instance.ShieldEntraceGuard)
                {
                    AlarmLevel = AlarmLevels.Error,
                    Name = "门禁报警"
                });
                list.Add(new Alarm(() => !IoPoints.I2DI17.Value)
                {
                    AlarmLevel = AlarmLevels.Error,
                    Name = "NG位无料盘报警"
                });
                if (!LicenseSheild)
                {
                    list.Add(new Alarm(() => !hasp.LicenseIsOK && hasp.Duetime <= 0)
                    {
                        AlarmLevel = AlarmLevels.Error,
                        Name = "该软件为试用软件，现已到期，或加密狗已拔出，请尽快联系厂商！"
                    });
                    list.Add(new Alarm(() => !hasp.LicenseIsOK && hasp.Duetime > 0)
                    {
                        AlarmLevel = AlarmLevels.Error,
                        Name = "加密狗无法授权，请检查加密狗或联系厂商！"
                    });
                }

                BackflowAlarm = AlarmCheck(m_Backflow.Alarms);
                RobotAlarm = AlarmCheck(m_Robot.Alarms);
                MachineIsAlarm = AlarmCheck(list);
            }
        }
        #endregion

        #region StatusCheck
        private void StatusCheck()
        {
            var list = new List<ICylinderStatusJugger>();
            m_Backflow.stationInitialize.Estop = false;
            m_Robot.stationInitialize.Estop = false;
            list.AddRange(m_Backflow.CylinderStatus);
            list.AddRange(m_Robot.CylinderStatus);
            while (true)
            {
                Thread.Sleep(500);
                foreach (var lst in list)
                    lst.StatusJugger();
            }
        }
        #endregion

        #region LicenseCheck
        private void LicenseCheck()
        {
            while (true)
            {
                try
                {
                    if (!LicenseSheild) hasp.UnauthorizedDetection();
                    Thread.Sleep(60000);
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            }
        }

        private void ShowWindows(object sender, EventArgs e)
        {
            if (!HaspRegistered.Created)
            {
                HaspRegistered.ShowDialog();
            }
        }
        #endregion

        #region 刷新状态——refreshStateTimer
        //刷新状态
        private void refreshStateTimer_Tick(object sender, EventArgs e)
        {
            refreshStateTimer.Enabled = false;

            IabPlcCom.BackColor = FrmPlc.IsConnect ? Color.Green : Color.Red;
            labQRCodeCom.BackColor = QRCodeCom.IsOpen ? Color.Green : Color.Red;
            labRobot.BackColor = AsynTcpRobot.IsConnected ? Color.Green : Color.Red;
            labTrayCodeCom.BackColor = TrayCodeCom.IsOpen ? Color.Green : Color.Red;
            lblProductType.Text = Product.Instance.CurrentProductType;

            //if (RunPara2.data.Count() > 0 && RunPara2.data[0].selindex >= 0)
            //    lblProductType.Text = RunPara2.data[RunPara2.data[0].selindex].ProductName;

            cb_down.Text = cb_down.Checked ? "下炉" : "上炉";

            if (!FrmPlc.IsConnect) return;

            ManualAutoMode = rbAuto.Checked ? true : false;
            button5.Enabled = rbAuto.Checked ? false : !RunPara.Instance.TraySolidify;
            button5.Text = RunPara.Instance.TraySolidify ? "固化模式" : (RunPara.Instance.cbAuto ? "空跑模式" : "生产模式");

            lblMachineStatus.Text = MachineOperation.Status.ToString();
            lblMachineStatus.ForeColor = MachineStatusColor(MachineOperation.Status);
            rbHand.Enabled = !MachineOperation.Running;
           
            if (cb_down.Checked)
            {
                labOven1Start.BackColor = (Marking.DownStoveRefreshState[0]) ? Color.Green : Color.Red;
                labOven2Start.BackColor = (Marking.DownStoveRefreshState[1]) ? Color.Green : Color.Red;
                labOven3Start.BackColor = (Marking.DownStoveRefreshState[2]) ? Color.Green : Color.Red;
                labOven4Start.BackColor = (Marking.DownStoveRefreshState[3]) ? Color.Green : Color.Red;
                labOven5Start.BackColor = (Marking.DownStoveRefreshState[4]) ? Color.Green : Color.Red;
                labOven6Start.BackColor = (Marking.DownStoveRefreshState[5]) ? Color.Green : Color.Red;
            }
            else
            {
                labOven1Start.BackColor = (Marking.UpStoveRefreshState[0]) ? Color.Green : Color.Red;
                labOven2Start.BackColor = (Marking.UpStoveRefreshState[1]) ? Color.Green : Color.Red;
                labOven3Start.BackColor = (Marking.UpStoveRefreshState[2]) ? Color.Green : Color.Red;
                labOven4Start.BackColor = (Marking.UpStoveRefreshState[3]) ? Color.Green : Color.Red;
                labOven5Start.BackColor = (Marking.UpStoveRefreshState[4]) ? Color.Green : Color.Red;
                labOven6Start.BackColor = (Marking.UpStoveRefreshState[5]) ? Color.Green : Color.Red;
            }

            //lblCycleTime.Text = FrmPlc.ReadInt16Data(FrmPlc.Stove1CuringTime).ToString();
            //labSetTemperature.Text = FrmPlc.ReadInt16Data(FrmPlc.SetStove1Temperature).ToString();

            if (((0 == Global.Instance.Stove1RunState) || (0 == Global.Instance.Stove2RunState) ||
                (0 == Global.Instance.Stove3RunState) || (0 == Global.Instance.Stove4RunState) ||
                (0 == Global.Instance.Stove5RunState) || (0 == Global.Instance.Stove6RunState)) &&
                 !Marking.plcStoveTotalStart)
            {
                Marking.plcStoveTotalStart = true;
                StoveStartTime.Restart();
            }
            else if ((((((1 == Global.Instance.Stove1RunState) || (2 == Global.Instance.Stove1RunState) &&
                (1 == Global.Instance.Stove2RunState) || (2 == Global.Instance.Stove2RunState) &&
                (1 == Global.Instance.Stove3RunState) || (2 == Global.Instance.Stove3RunState) &&
                (1 == Global.Instance.Stove4RunState)) || (2 == Global.Instance.Stove4RunState) &&
                (1 == Global.Instance.Stove5RunState) || (2 == Global.Instance.Stove5RunState) &&
                (1 == Global.Instance.Stove6RunState) || (2 == Global.Instance.Stove6RunState)) &&
                Marking.plcStoveTotalStart)) || (StoveStartTime.ElapsedMilliseconds / 1000) > 18000)
            {
                Marking.plcStoveTotalStart = false;
                StoveStartTime.Stop();
                temperatureChart.DataBind();
            }

            int n1 = 0;
            if (RunPara.Instance.OKTary.ProductPos == RunPara.Instance.TrayPoint + 1)
            {

                checktime++;
                Thread.Sleep(1000);

                if (checktime > 30)
                {
                    if (RunPara.Instance.OKTary.ProductPos == RunPara.Instance.TrayPoint + 1)
                    {
                        foreach (var si in RunPara.Instance.OKTary.QRCode)
                        {

                            if (si.sign)
                            {
                                n1++;
                            }
                        }
                        if (n1 == RunPara.Instance.TrayPoint)

                        {

                            RunPara.Instance.OKTary.ProductPos = RunPara.Instance.TrayPoint;
                        }
                        else
                        {
                            RunPara.Instance.OKTary.ProductPos = 0;


                            //     layerLight.VoiceClosed =true;
                        }
                    }
                }
            }
            else
            {
                checktime = 0;
            }

            refreshStateTimer.Enabled = true;
        }
        #endregion

        #region 刷新数据——refreshDatatimer
        int xaxis = 0;
        int checktime = 0;
        //刷新数据
        private void refreshDatatimer_Tick(object sender, EventArgs e)
        {
            /// refreshDatatimer.Enabled = false;

            for (int i = 0; i < 6; i++)
            {
                if (RunPara.Instance.Stove[i].ifcaltime)
                {
                    if (RunPara.Instance.MesStoveTemperature.min <= RunPara.Instance.Stove[i].Temperature)
                    {

                        if (firsttemp[i])
                        {
                            RunPara.Instance.Stove[i].timeStove = 0;
                            firsttemp[i] = false;
                        }
                        RunPara.Instance.Stove[i].timeStove += 2;
                    }
                }
                else
                {
                    firsttemp[i] = true;
                }
            }

            tbOven1Temperature.Text = RunPara.Instance.Stove[0].Temperature.ToString();
            tbOven2Temperature.Text = RunPara.Instance.Stove[1].Temperature.ToString();
            tbOven3Temperature.Text = RunPara.Instance.Stove[2].Temperature.ToString();
            tbOven4Temperature.Text = RunPara.Instance.Stove[3].Temperature.ToString();
            tbOven5Temperature.Text = RunPara.Instance.Stove[4].Temperature.ToString();
            tbOven6Temperature.Text = RunPara.Instance.Stove[5].Temperature.ToString();

            try
            {
                for (int i = 0; i < RunPara.Instance.Stove.Length; i++)
                {
                    for (int j = 0; j < RunPara.Instance.Stove[i].Tray.QRCode.Length; j++)
                    {
                        if (RunPara.Instance.Stove[i].Tray.QRCode[j].dgvRows >= 0)
                        {
                            showCell(dgvStoveResult.Rows[RunPara.Instance.Stove[i].Tray.QRCode[j].dgvRows].Cells[3], RunPara.Instance.Stove[i].Temperature.ToString());
                        }
                    }
                }
            }
            catch
            {

            }
            if (xaxis % 200 == 0)
            {
                refreshTemperatureChart(1, xaxis, RunPara.Instance.Stove[0].Temperature);
                refreshTemperatureChart(2, xaxis, RunPara.Instance.Stove[1].Temperature);
                refreshTemperatureChart(3, xaxis, RunPara.Instance.Stove[2].Temperature);
                refreshTemperatureChart(4, xaxis, RunPara.Instance.Stove[3].Temperature);
                refreshTemperatureChart(5, xaxis, RunPara.Instance.Stove[4].Temperature);
                refreshTemperatureChart(6, xaxis, RunPara.Instance.Stove[5].Temperature);
            }
            xaxis += 10;
            if (xaxis > 17000)
            {
                xaxis = 0;
            }
            //listView1.Items.Clear();
            foreach (var str in RunPara.Instance.Stove)
            {
                //listView1.Items.Add(str.timeStove.ToString());
            }
            foreach (var str in Marking.UpStoveRefreshState)
            {

                ///listView1.Items.Add(str.ToString());
            }


            tstrobotstatus.Text = Marking.RobotStatus.ToString();
            //textBox1.Text = Marking.RobotStatus.ToString();
            //textBox1.Text = Connection.Step.ToString();

            //textBox2.Text = Marking.preRobotStatus.ToString();

            tstrobotstep.Text = Robot.Step.ToString();
            //textBox4.Text = RunPara.Instance.OKTary.ProductPos.ToString();
            //textBox4.Text = Robot.Step.ToString();

            //textBox5.Text = Marking.ConnectionInform.ToString();

            //listView1.Items.Add(Marking.AAOK.ToString());
            //listView1.Items.Add(Marking.AANG.ToString());

            //refreshDatatimer.Enabled = true;
        }
        #endregion

        #endregion

        #region 通信响应事件

        /// <summary>
        /// 产品扫码器数据接收
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="result">返回结果</param>
        private void DealWithQRCodeReceiveData(object sender, string result)
        {
            try
            {
                //Marking.QRCodeSign = false;
                if (Robot.ifemptyrun || RunPara.Instance.ShieldProductCode) //空跑模式 屏蔽产品扫码
                {
                    result = "CV1V3CV4" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString();
                    TaryQRCode(result);
                    appendText(result);
                }
                else
                {
                    if (result.Contains(UniversalFlags.errorStr))
                    {
                        throw new Exception(result);
                    }
                    if (result != string.Empty)
                    {
                        Thread.Sleep(200);
                        if (m_Robot.ScanSN(result)) //检查条码
                        {
                            TaryQRCode(result);
                            appendText(result);
                        }
                        else
                        {
                            appendText("SV_Interlocking_Main: false");
                            LogHelper.Debug("SV_Interlocking_Main: false");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                appendText("扫码失败：" + ex.Message);
            }
            finally
            {
                QRCodeCom.receiveFinish = true;
            }
        }

        /// <summary>
        /// 料盘扫码器数据接收
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="result">返回结果</param>
        private void DealWithTrayCodeReceiveData(object sender, string result)
        {
            try
            {
                if (Robot.ifemptyrun || RunPara.Instance.ShieldTrayCode)  //空跑模式 屏蔽料盘扫码
                {
                    result = "TrayNum" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString();
                    RunPara.Instance.OrgTrayCode = result;
                    appendText(result);
                }
                else
                {
                    if (result.Contains(UniversalFlags.errorStr))
                    {
                        throw new Exception(result);
                    }
                    if (result != string.Empty)
                    {
                        Thread.Sleep(200);
                        RunPara.Instance.OrgTrayCode = result;
                        appendText(result);
                    }
                }                
            }
            catch (Exception ex)
            {
                appendText("扫码失败：" + ex.Message);
            }
            finally
            {
                TrayCodeCom.receiveFinish = true;
            }
        }

        /// <summary>
        /// 刷新表格
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="result">刷新对象</param>
        private void DealWithDGVReceiveData(object sender, RefreshDataGridView dgv)
        {
            //  refreshDatatimer.Enabled = false;
            try
            {
                if (dgv == RefreshDataGridView.UpStove)
                {
                    for (int i = 0; i < RunPara.Instance.Stove.Length; i++)
                    {
                        for (int j = 0; j < RunPara.Instance.Stove[i].Tray.QRCode.Length; j++)
                        {
                            RunPara.Instance.Stove[i].Tray.QRCode[j].dgvRows = -1;
                        }
                    }
                    TaryDGVClear();
                    StoveDGVClear();
                    for (int i = 0; i < RunPara.Instance.Stove.Length; i++)
                    {
                        for (int j = 0; j < RunPara.Instance.Stove[i].Tray.QRCode.Length; j++)
                        {
                            if (RunPara.Instance.Stove[i].Tray.QRCode[j].sign)
                            {
                                StoveDGVShow(i, j);
                            }
                        }
                    }
                }
                else if (dgv == RefreshDataGridView.DownStove)
                {
                    StoveDGVClear();
                    for (int i = 0; i < RunPara.Instance.Stove.Length; i++)
                    {
                        for (int j = 0; j < RunPara.Instance.Stove[i].Tray.QRCode.Length; j++)
                        {
                            if (RunPara.Instance.Stove[i].Tray.QRCode[j].sign)
                            {
                                StoveDGVShow(i, j);
                            }
                        }
                    }

                    ShowStatistics();
                }
                else if (dgv == RefreshDataGridView.ManualTakeDish)
                {
                    TaryDGVClear();
                }
            }
            catch (Exception ex)
            {
                appendText("刷新表格数据报错!");
            }
            refreshDatatimer.Enabled = true;
        }

        #endregion

        #region UI操作

        /// <summary>
        /// 窗体加到选项卡中
        /// </summary>
        private void AddSubForm()
        {
            GenerateForm(FrmPlc, tbgPLCMonitor);
            GenerateForm(new FrmIOmonitor(), tbgIOMonitor);
            GenerateForm(new frmRecipe(Product.Instance.ProductDataFilePath, Product.Instance.CurrentProductType,
                () =>
                {
                    try
                    {
                        Product.Instance.CurrentProductType = frmRecipe.CurrentProductType;
                        RunPara.Instance = SerializerManager<RunPara>.Instance.Load(Product.Instance.ProductDataFile);
                    }
                    catch (Exception ex)
                    {
                        AppendText($"加载数据失败！");
                    }
                },
                () =>
                {
                    try
                    {
                        Product.Instance.CurrentProductType = frmRecipe.CurrentProductType;
                        SerializerManager<RunPara>.Instance.Save(Product.Instance.ProductDataFile, RunPara.Instance);
                        SerializerManager<Product>.Instance.Save(AppConfig.RecipeFilePath, Product.Instance);
                    }
                    catch (Exception ex)
                    {
                        AppendText($"保存数据失败！");
                    }

                }), tbgRecipe);
            GenerateForm(new FrmOperationReversal(m_Backflow, m_Robot), tbgOperation);
        }

        /// <summary>
        /// 设置窗体没有边框 加入到选项卡中
        /// </summary>
        /// <param name="Fm">窗体</param>
        /// <param name="sender">选项卡</param>
        private void GenerateForm(Form Fm, TabPage sender)
        {
            try
            {
                Fm.FormBorderStyle = FormBorderStyle.None;
                Fm.TopLevel = false;
                Fm.Parent = sender;
                Fm.ControlBox = false;
                Fm.Dock = DockStyle.Fill;
                Fm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            //主界面
            tbcMain.SelectedTab = tbgMain;
        }

        private void btnPLCMonitor_Click(object sender, EventArgs e)
        {
            //PLC监控
            tbcMain.SelectedTab = tbgPLCMonitor;
        }

        private void btnIOMonitor_Click(object sender, EventArgs e)
        {
            //IO监控
            tbcMain.SelectedTab = tbgIOMonitor;
        }

        private void btnRecipe_Click(object sender, EventArgs e)
        {
            //型号选择
            tbcMain.SelectedTab = tbgRecipe;
        }

        private void btnOperation_Click(object sender, EventArgs e)
        {
            //设备操作
            tbcMain.SelectedTab = tbgOperation;
        }

        private void btnQRCodeCom_Click(object sender, EventArgs e)
        {
            //扫码设置
            new FrmSerialPort(QRCodeCom, typeof(DM100Q)).ShowDialog();

        }

        private void btnTrayCodeSet_Click(object sender, EventArgs e)
        {
            //扫码设置
            new FrmSerialPort(TrayCodeCom, typeof(DM100Q)).ShowDialog();
        }

        private void btnMes_Click(object sender, EventArgs e)
        {
            //Mes
            new FrmMesSet(AppConfig.MESConfigFileName).ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //登录
            new FrmLogin().ShowDialog();
            UserLevelChange(Marking.userLevel);
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //退出
            DialogResult result = MessageBox.Show("退出程序？", "退出", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                IoPoints.I2DO20.Value = false;
                IoPoints.I2DO21.Value = false;

                Thread.Sleep(600);

                threadMachineRun.Abort();
                threadAlarmCheck.Abort();
                threadStatusCheck.Abort();

                FrmPlc.Disconnect();

                SerializerManager<Delay>.Instance.Save(AppConfig.DelayFilePath, Delay.Instance);
                SerializerManager<Product>.Instance.Save(AppConfig.RecipeFilePath, Product.Instance);
                SerializerManager<Global>.Instance.Save(AppConfig.GlobalFilePath, Global.Instance);
                SerializerManager<RunPara>.Instance.Save(Product.Instance.ProductDataFile, RunPara.Instance);

                base.OnClosing(e);
            }
            else
            {
                e.Cancel = true;
            }
           
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClearCount_Click(object sender, EventArgs e)
        {
            //统计清零
            RunPara.Instance.OkNumber = 0;
            RunPara.Instance.NgNumber = 0;
            RunPara.Instance.TotalNumber = 0;

            ShowStatistics();
        }

        private void btnIllumination_Click(object sender, EventArgs e)
        {
            //照明开关
            if (IoPoints.I2DO17.Value)
            {
                IoPoints.I2DO17.Value = false;
                btnIllumination.Text = "照明灯开";
                btnIllumination.BackColor = System.Drawing.SystemColors.Control;
            }
            else
            {
                IoPoints.I2DO17.Value = true;
                btnIllumination.Text = "照明灯关";
                btnIllumination.BackColor = Color.Green;
            }
        }

        private void btnTricolorLamp_Click(object sender, EventArgs e)
        {
            //蜂鸣
            layerLight.VoiceClosed = !layerLight.VoiceClosed;
            btnTricolorLamp.Text = layerLight.VoiceClosed ? "蜂鸣静止" : "蜂鸣";
            btnTricolorLamp.BackColor = layerLight.VoiceClosed ? Color.Red : System.Drawing.SystemColors.Control;
        }

        private void btnRunSetting_Click(object sender, EventArgs e)
        {
            //设置
            new FrmRunSet().ShowDialog();

            #region 炉产品清空
            if (Marking.StoveProductClear[0])
            {
                for (int i = 0; i < RunPara.Instance.Stove[0].Tray.QRCode.Length; i++)
                {
                    if (RunPara.Instance.Stove[0].Tray.QRCode[i].sign)
                    {
                        RunPara.Instance.Stove[0].Tray.QRCode[i].SN = string.Empty;
                        RunPara.Instance.Stove[0].Tray.QRCode[i].sign = false;
                        RunPara.Instance.Stove[0].Tray.QRCode[i].dgvRows = -1;
                    }
                }
            }
            if (Marking.StoveProductClear[1])
            {
                for (int i = 0; i < RunPara.Instance.Stove[1].Tray.QRCode.Length; i++)
                {
                    if (RunPara.Instance.Stove[1].Tray.QRCode[i].sign)
                    {
                        RunPara.Instance.Stove[1].Tray.QRCode[i].SN = string.Empty;
                        RunPara.Instance.Stove[1].Tray.QRCode[i].sign = false;
                        RunPara.Instance.Stove[1].Tray.QRCode[i].dgvRows = -1;
                    }
                }
            }
            if (Marking.StoveProductClear[2])
            {
                for (int i = 0; i < RunPara.Instance.Stove[2].Tray.QRCode.Length; i++)
                {
                    if (RunPara.Instance.Stove[2].Tray.QRCode[i].sign)
                    {
                        RunPara.Instance.Stove[2].Tray.QRCode[i].SN = string.Empty;
                        RunPara.Instance.Stove[2].Tray.QRCode[i].sign = false;
                        RunPara.Instance.Stove[2].Tray.QRCode[i].dgvRows = -1;
                    }
                }
            }
            if (Marking.StoveProductClear[3])
            {
                for (int i = 0; i < RunPara.Instance.Stove[3].Tray.QRCode.Length; i++)
                {
                    if (RunPara.Instance.Stove[3].Tray.QRCode[i].sign)
                    {
                        RunPara.Instance.Stove[3].Tray.QRCode[i].SN = string.Empty;
                        RunPara.Instance.Stove[3].Tray.QRCode[i].sign = false;
                        RunPara.Instance.Stove[3].Tray.QRCode[i].dgvRows = -1;
                    }
                }
            }
            if (Marking.StoveProductClear[4])
            {
                for (int i = 0; i < RunPara.Instance.Stove[4].Tray.QRCode.Length; i++)
                {
                    if (RunPara.Instance.Stove[4].Tray.QRCode[i].sign)
                    {
                        RunPara.Instance.Stove[4].Tray.QRCode[i].SN = string.Empty;
                        RunPara.Instance.Stove[4].Tray.QRCode[i].sign = false;
                        RunPara.Instance.Stove[4].Tray.QRCode[i].dgvRows = -1;
                    }
                }
            }
            if (Marking.StoveProductClear[5])
            {
                for (int i = 0; i < RunPara.Instance.Stove[5].Tray.QRCode.Length; i++)
                {
                    if (RunPara.Instance.Stove[5].Tray.QRCode[i].sign)
                    {
                        RunPara.Instance.Stove[5].Tray.QRCode[i].SN = string.Empty;
                        RunPara.Instance.Stove[5].Tray.QRCode[i].sign = false;
                        RunPara.Instance.Stove[5].Tray.QRCode[i].dgvRows = -1;
                    }
                }
            }

            //refreshDatatimer.Enabled = false;
            StoveDGVClear();
            for (int i = 0; i < RunPara.Instance.Stove.Length; i++)
            {
                for (int j = 0; j < RunPara.Instance.Stove[i].Tray.QRCode.Length; j++)
                {
                    if (RunPara.Instance.Stove[i].Tray.QRCode[j].sign)
                    {
                        StoveDGVShow(i, j);
                    }
                }
            }
            refreshDatatimer.Enabled = true;

            Marking.StoveProductClear[0] = false;
            Marking.StoveProductClear[1] = false;
            Marking.StoveProductClear[2] = false;
            Marking.StoveProductClear[3] = false;
            Marking.StoveProductClear[4] = false;
            Marking.StoveProductClear[5] = false;
            #endregion

            #region 料盘产品清空
            if (Marking.TaryProductClear)
            {
                for (int n = 0; n < RunPara.Instance.OKTary.QRCode.Length; n++)
                {
                    RunPara.Instance.OKTary.QRCode[n].SN = string.Empty;
                    RunPara.Instance.OKTary.QRCode[n].sign = false;
                    RunPara.Instance.OKTary.QRCode[n].dgvRows = -1;
                }
                RunPara.Instance.OKTary.ProductPos = 0;
                TaryDGVClear();
            }
            Marking.TaryProductClear = false;
            #endregion
        }

        private void clearall()
        {
            #region 炉产品清空

            for (int i = 0; i < RunPara.Instance.Stove[0].Tray.QRCode.Length; i++)
            {
                if (RunPara.Instance.Stove[0].Tray.QRCode[i].sign)
                {
                    RunPara.Instance.Stove[0].Tray.QRCode[i].SN = string.Empty;
                    RunPara.Instance.Stove[0].Tray.QRCode[i].sign = false;
                    RunPara.Instance.Stove[0].Tray.QRCode[i].dgvRows = -1;
                }
            }


            for (int i = 0; i < RunPara.Instance.Stove[1].Tray.QRCode.Length; i++)
            {
                if (RunPara.Instance.Stove[1].Tray.QRCode[i].sign)
                {
                    RunPara.Instance.Stove[1].Tray.QRCode[i].SN = string.Empty;
                    RunPara.Instance.Stove[1].Tray.QRCode[i].sign = false;
                    RunPara.Instance.Stove[1].Tray.QRCode[i].dgvRows = -1;
                }
            }


            for (int i = 0; i < RunPara.Instance.Stove[2].Tray.QRCode.Length; i++)
            {
                if (RunPara.Instance.Stove[2].Tray.QRCode[i].sign)
                {
                    RunPara.Instance.Stove[2].Tray.QRCode[i].SN = string.Empty;
                    RunPara.Instance.Stove[2].Tray.QRCode[i].sign = false;
                    RunPara.Instance.Stove[2].Tray.QRCode[i].dgvRows = -1;
                }
            }


            for (int i = 0; i < RunPara.Instance.Stove[3].Tray.QRCode.Length; i++)
            {
                if (RunPara.Instance.Stove[3].Tray.QRCode[i].sign)
                {
                    RunPara.Instance.Stove[3].Tray.QRCode[i].SN = string.Empty;
                    RunPara.Instance.Stove[3].Tray.QRCode[i].sign = false;
                    RunPara.Instance.Stove[3].Tray.QRCode[i].dgvRows = -1;
                }
            }


            for (int i = 0; i < RunPara.Instance.Stove[4].Tray.QRCode.Length; i++)
            {
                if (RunPara.Instance.Stove[4].Tray.QRCode[i].sign)
                {
                    RunPara.Instance.Stove[4].Tray.QRCode[i].SN = string.Empty;
                    RunPara.Instance.Stove[4].Tray.QRCode[i].sign = false;
                    RunPara.Instance.Stove[4].Tray.QRCode[i].dgvRows = -1;
                }
            }


            for (int i = 0; i < RunPara.Instance.Stove[5].Tray.QRCode.Length; i++)
            {
                if (RunPara.Instance.Stove[5].Tray.QRCode[i].sign)
                {
                    RunPara.Instance.Stove[5].Tray.QRCode[i].SN = string.Empty;
                    RunPara.Instance.Stove[5].Tray.QRCode[i].sign = false;
                    RunPara.Instance.Stove[5].Tray.QRCode[i].dgvRows = -1;
                }
            }


            //refreshDatatimer.Enabled = false;
            StoveDGVClear();
            for (int i = 0; i < RunPara.Instance.Stove.Length; i++)
            {
                for (int j = 0; j < RunPara.Instance.Stove[i].Tray.QRCode.Length; j++)
                {
                    if (RunPara.Instance.Stove[i].Tray.QRCode[j].sign)
                    {
                        StoveDGVShow(i, j);
                    }
                }
            }
            refreshDatatimer.Enabled = true;

            Marking.StoveProductClear[0] = false;
            Marking.StoveProductClear[1] = false;
            Marking.StoveProductClear[2] = false;
            Marking.StoveProductClear[3] = false;
            Marking.StoveProductClear[4] = false;
            Marking.StoveProductClear[5] = false;
            #endregion

            #region 料盘产品清空

            for (int n = 0; n < RunPara.Instance.OKTary.QRCode.Length; n++)
            {
                RunPara.Instance.OKTary.QRCode[n].SN = string.Empty;
                RunPara.Instance.OKTary.QRCode[n].sign = false;
                RunPara.Instance.OKTary.QRCode[n].dgvRows = -1;
            }
            RunPara.Instance.OKTary.ProductPos = 0;
            TaryDGVClear();

            Marking.TaryProductClear = false;
            #endregion
        }

        #endregion

        #region 消息显示

        public void appendText(string str)
        {
            AppendText(string.Format("<<主界面>>:: {0}", str));
        }

        private void DealWithReceiveData(object sender, string result) => AppendText(result);

        /// <summary>
        /// 使用委托方式更新AppendText显示
        /// </summary>
        /// <param name="txt">消息</param>
        private void AppendText(string txt)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendText), txt);
            }
            else
            {
                if (lstInfo.Items.Count > 500) lstInfo.Items.Clear();
                txtMessage.AppendText(string.Format("{0}-{1}" + Environment.NewLine, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), txt));
                log.Info(txt);
            }
        }

        public AlarmType AlarmCheck(IList<Alarm> Alarms)
        {
            var Alarm = new AlarmType();
            foreach (Alarm alarm in Alarms)
            {
                var btemp = alarm.IsAlarm;
                if (alarm.AlarmLevel == AlarmLevels.Error)
                {
                    Alarm.IsAlarm |= btemp;
                    Invoke(new Action(() =>
                    {
                        Msg(string.Format("{0},{1}", alarm.AlarmLevel.ToString(), alarm.Name), btemp);
                    }));
                }
                else if (alarm.AlarmLevel == AlarmLevels.None)
                {
                    Alarm.IsPrompt |= btemp;
                    this.Invoke(new Action(() =>
                    {
                        Msg(string.Format("{0},{1}", alarm.AlarmLevel.ToString(), alarm.Name), btemp);
                    }));
                }
                else
                {
                    Alarm.IsWarning |= btemp;
                    this.Invoke(new Action(() =>
                    {
                        Msg(string.Format("{0},{1}", alarm.AlarmLevel.ToString(), alarm.Name), btemp);
                    }));
                }
            }
            return Alarm;
        }

        private void Msg(string str, bool value)
        {
            string tempstr = null;
            bool sign = false;

            var arrRight = new List<object>();
            foreach (var tmpist in lstInfo.Items) arrRight.Add(tmpist);
            if (value)
            {
                foreach (string tmplist in arrRight)
                {
                    if (tmplist.IndexOf("-") > -1)
                    {
                        tempstr = tmplist.Substring(tmplist.IndexOf("-") + 1, tmplist.Length - tmplist.IndexOf("-") - 1);
                    }
                    if (tempstr == (str + "\r\n"))
                    {
                        sign = true;
                        break;
                    }
                }
                if (!sign)
                {
                    lstInfo.Items.Insert(0, (string.Format("{0}-{1}" + Environment.NewLine, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), str)));
                    log.Debug(str);
                }
            }
            else
            {
                foreach (string tmplist in arrRight)
                {
                    if (tmplist.IndexOf("-") > -1)
                    {
                        tempstr = tmplist.Substring(tmplist.IndexOf("-") + 1, tmplist.Length - tmplist.IndexOf("-") - 1);
                        if (tempstr == (str + "\r\n")) lstInfo.Items.Remove(tmplist);
                    }
                }
            }
        }

        /// <summary>
        /// 检查产品码，填入数据
        /// </summary>
        /// <param name="QRCode"></param>
        public void TaryQRCode(string QRCode)
        {
            //检查固化炉中产品数据
            for (int i = 0; i < RunPara.Instance.Stove.Length; i++)
            {
                for (int j = 0; j < RunPara.Instance.Stove[i].Tray.QRCode.Length; j++)
                {
                    if (QRCode == RunPara.Instance.Stove[i].Tray.QRCode[j].SN)
                    {
                        Marking.QRCodeSign = false;
                    }
                }
            }
            //检查OK盘中产品数据
            if (Marking.QRCodeSign)
            {
                //   Marking.QRCodeSign = true;
                for (int i = 0; i < RunPara.Instance.OKTary.QRCode.Length; i++)
                {
                    if (QRCode == RunPara.Instance.OKTary.QRCode[i].SN)
                    {
                        Marking.QRCodeSign = false;
                    }
                }
            }
            if (Marking.QRCodeSign) //固化炉 或 ok盘中有过数据就不扫入
            {
                RunPara.Instance.OKTary.QRCode[RunPara.Instance.OKTary.ProductPos].SN = QRCode;
                RunPara.Instance.OKTary.QRCode[RunPara.Instance.OKTary.ProductPos].sign = true;
                RunPara.Instance.OKTary.QRCode[RunPara.Instance.OKTary.ProductPos].scantime = DateTime.Now;

                TaryDGVShow(RunPara.Instance.OKTary.ProductPos);
            }
            Marking.QRCodeSign = true;
        }        

        public void DGVLoad()
        {
            for (int i = 0; i < RunPara.Instance.Stove.Length; i++)
            {
                for (int j = 0; j < RunPara.Instance.Stove[i].Tray.QRCode.Length; j++)
                {
                    if (RunPara.Instance.Stove[i].Tray.QRCode[j].dgvRows >= 0)
                    {
                        StoveDGVShow(i, j);
                    }
                }
            }

            for (int n = 0; n < RunPara.Instance.OKTary.QRCode.Length; n++)
            {
                if (RunPara.Instance.OKTary.QRCode[n].dgvRows >= 0)
                {
                    TaryDGVShow(n);
                }
            }
        }

        public void TaryDGVShow(int productNo)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<int>(TaryDGVShow), productNo);
            }
            else
            {
                RunPara.Instance.OKTary.QRCode[productNo].dgvRows = dgvTaryResult.Rows.Add();
                showCell(dgvTaryResult.Rows[RunPara.Instance.OKTary.QRCode[productNo].dgvRows].Cells[0], (productNo + 1).ToString());
                showCell(dgvTaryResult.Rows[RunPara.Instance.OKTary.QRCode[productNo].dgvRows].Cells[1], RunPara.Instance.OKTary.QRCode[productNo].SN.ToString());
            }
        }

        public void TaryDGVClear()
        {
            this.Invoke(new Action(() =>
            {
                dgvTaryResult.Rows.Clear();
            }));
        }

        public void StoveDGVShow(int StoveNo, int productNo)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<int, int>(StoveDGVShow), StoveNo, productNo);
            }
            else
            {
                RunPara.Instance.Stove[StoveNo].Tray.QRCode[productNo].dgvRows = dgvStoveResult.Rows.Add();
                showCell(dgvStoveResult.Rows[RunPara.Instance.Stove[StoveNo].Tray.QRCode[productNo].dgvRows].Cells[0], (StoveNo + 1).ToString());
                showCell(dgvStoveResult.Rows[RunPara.Instance.Stove[StoveNo].Tray.QRCode[productNo].dgvRows].Cells[1], (productNo + 1).ToString());
                showCell(dgvStoveResult.Rows[RunPara.Instance.Stove[StoveNo].Tray.QRCode[productNo].dgvRows].Cells[2], RunPara.Instance.Stove[StoveNo].Tray.QRCode[productNo].SN);
                showCell(dgvStoveResult.Rows[RunPara.Instance.Stove[StoveNo].Tray.QRCode[productNo].dgvRows].Cells[3], RunPara.Instance.Stove[StoveNo].Temperature.ToString());
            }
        }

        public void StoveDGVClear()
        {
            this.Invoke(new Action(() =>
            {
                dgvStoveResult.Rows.Clear();
            }));
        }

        private void showCell(DataGridViewCell cell, string str)
        {
            Invoke(new Action(() => { cell.Value = str; }));
        }

        private void refreshTemperatureChart(int stove, double xValue, double yValue)
        {
            try
            {


                if (xValue == 0)
                {
                    this.temperatureChart.Series[0].Points.Clear();

                }


                Invoke(new Action(() =>
                {
                    switch (stove)
                    {
                        case 1:

                            this.temperatureChart.Series[0].Points.AddXY(xValue, yValue);
                            break;
                        case 2:
                            this.temperatureChart.Series[1].Points.AddXY(xValue, yValue);
                            break;
                        case 3:
                            this.temperatureChart.Series[2].Points.AddXY(xValue, yValue);
                            break;
                        case 4:
                            this.temperatureChart.Series[3].Points.AddXY(xValue, yValue);
                            break;
                        case 5:
                            this.temperatureChart.Series[4].Points.AddXY(xValue, yValue);
                            break;
                        case 6:
                            this.temperatureChart.Series[5].Points.AddXY(xValue, yValue);
                            break;
                        default:
                            break;
                    }
                }));
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 刷新统计信息
        /// </summary>
        public void ShowStatistics()
        {
            this.Invoke(new Action(() =>
            {
                lblOK.Text = RunPara.Instance.OkNumber.ToString();
                lblNG.Text = RunPara.Instance.NgNumber.ToString();
                RunPara.Instance.TotalNumber = RunPara.Instance.OkNumber + RunPara.Instance.NgNumber;
                lblSum.Text = RunPara.Instance.TotalNumber.ToString();
            }));
        }

        #endregion

        #region 设备操作按钮

        private void btnStart_MouseDown(object sender, EventArgs e)
        {
            if (!ManualAutoMode)
            {
                AppendText("设备无法启动，必须在自动模式才能操作！");
                return;
            }
            Marking.AlarmStopThread = true;
            MachineOperation.Start = true;
            MachineOperation.Stop = false;
            MachineOperation.Reset = false;
            MachineOperation.Pause = false;
        }

        private void btnStart_MouseUp(object sender, EventArgs e)
        {
            MachineOperation.Start = false;
        }

        private void btnPause_MouseDown(object sender, EventArgs e)
        {
            MachineOperation.Pause = true;
            MachineOperation.Stop = false;
            MachineOperation.Reset = false;
            MachineOperation.Start = false;
        }

        private void btnPause_MouseUp(object sender, EventArgs e)
        {
            MachineOperation.Pause = false;
        }

        private void btnAlarmClean_MouseDown_1(object sender, MouseEventArgs e)
        {
            m_External.AlarmReset = false;
        }

        private void btnAlarmClean_MouseUp_1(object sender, MouseEventArgs e)
        {
            m_External.AlarmReset = true;
            if (m_Backflow.CarryAxis.IsAlarmed)
            {
                m_Backflow.CarryAxis.Clean();
            }
            AppendText("清除报警！");
        }

        private void btnStop_MouseDown(object sender, EventArgs e)
        {
            MachineOperation.Stop = true;
            MachineOperation.Reset = false;
            MachineOperation.Pause = false;
            MachineOperation.Start = false;
        }

        private void btnStop_MouseUp(object sender, EventArgs e)
        {
            MachineOperation.Stop = false;
        }

        private void btnReset_MouseDown(object sender, EventArgs e)
        {
            if (ManualAutoMode)
            {
                if (!MachineIsAlarm.IsAlarm && !BackflowAlarm.IsAlarm && !RobotAlarm.IsAlarm)
                    AppendText("设备手动状态时，才能复位。自动状态只能清除报警！");
                m_External.AlarmReset = true;
                if (m_Backflow.CarryAxis.IsAlarmed) m_Backflow.CarryAxis.Clean();
            }
            else
            {
                if (MachineOperation != null)
                {
                    MachineOperation.IniliazieDone = false;
                    MachineOperation.Flow = 0;
                    MachineOperation.Reset = true;
                    MachineOperation.Stop = false;
                    MachineOperation.Pause = false;
                    MachineOperation.Start = false;
                }
            }
        }

        private void btnReset_MouseUp(object sender, EventArgs e)
        {
            MachineOperation.Reset = false;
            m_External.AlarmReset = false;
        }

        private void btnImmediatelyUp_Click(object sender, EventArgs e)
        {
            Marking.ImmediatelyUpStove = true;
        }

        private void btnAlarmClean_MouseDown(object sender, MouseEventArgs e)
        {
            m_External.AlarmReset = true;

        }

        private void btnAlarmClean_MouseUp(object sender, MouseEventArgs e)
        {
            m_External.AlarmReset = false;
        }

        private Color MachineStatusColor(MachineStatus status)
        {
            switch (status)
            {
                case MachineStatus.设备未准备好:
                    return Color.Orange;
                case MachineStatus.设备准备好:
                    return Color.Blue;
                case MachineStatus.设备运行中:
                    return Color.Green;
                case MachineStatus.设备停止中:
                    return Color.Red;
                case MachineStatus.设备暂停中:
                    return Color.Purple;
                case MachineStatus.设备复位中:
                    return Color.OrangeRed;
                case MachineStatus.设备报警中:
                    return Color.Red;
                case MachineStatus.设备急停已按下:
                    return Color.Red;
                default:
                    return Color.Red;
            }
        }

        #endregion

        #region 调试代码
        /// <summary>
        /// 空跑模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            RunPara.Instance.cbAuto = !RunPara.Instance.cbAuto;
            if (RunPara.Instance.cbAuto)
            {
                button5.Text = "空跑模式";
                Robot.ifemptyrun = true;
                timer1.Enabled = true;
            }
            else
            {
                button5.Text = "生产模式";
                Robot.ifemptyrun = false;
                timer1.Enabled = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Marking.ImmediatelyDownStove)
            {
                Marking.AAOK = false;
                Marking.AANG = false;
            }
            else if (RunPara.Instance.OKTary.ProductPos == RunPara.Instance.TrayPoint - 1 && prepos == 0)
            {
                Marking.AAOK = false;
                Marking.AANG = true;
            }
            else
            {
                Marking.AAOK = true;
                Marking.AANG = false;
            }
            if (RunPara.Instance.OKTary.ProductPos == 2)
            {
                RunPara.Instance.OKTary.ProductPos = RunPara.Instance.TrayPoint - 2;
            }
            //Marking.ConnectionInform = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Robot.Step = 440;
        }

        #endregion

    }


}
