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
    //高温固化炉线程
    public class StoveRefresh : StationPart
    {
        public StoveRefresh(External ExternalSign, StationInitialize stationIni, StationOperate stationOpe)
                : base(ExternalSign, stationIni, stationOpe, typeof(StoveRefresh)) { }

        private static ILog logger = LogManager.GetLogger(typeof(StoveRefresh));

        private StoveRefreshAlarm m_Alarm;

        /// <summary>
        /// PLC
        /// </summary>
        public FrmManualPLC frmPlc { set; get; }
        bool ifalarmone = true;
        int checktime = 0;




        public override void Running(RunningModes runningMode)
        {
            while (true)
            {
                Thread.Sleep(800);

                try
                {
                    #region 自动流程

                    if (stationOperate.Running)
                    {
                        frmPlc.WriteM(frmPlc.StoveLockControl, true);

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

                        #region 上炉刷新状态                        

                        if ((1 == Global.Instance.Stove1RunState || 2 == Global.Instance.Stove1RunState) && !Marking.DownStoveRefreshState[0]
                            && !Global.Instance.Stove1AnyMaterial && frmPlc.ReadM(frmPlc.Stove1AllowHouse))
                        {
                            Marking.UpStoveRefreshState[0] = true;
                        }
                        else
                        {
                            Marking.UpStoveRefreshState[0] = false;
                        }
                        if ((1 == Global.Instance.Stove2RunState || 2 == Global.Instance.Stove2RunState) && !Marking.DownStoveRefreshState[1]
                            && !Global.Instance.Stove2AnyMaterial && frmPlc.ReadM(frmPlc.Stove2AllowHouse))
                        {
                            Marking.UpStoveRefreshState[1] = true;
                        }
                        else
                        {
                            Marking.UpStoveRefreshState[1] = false;
                        }
                        if ((1 == Global.Instance.Stove3RunState || 2 == Global.Instance.Stove3RunState) && !Marking.DownStoveRefreshState[2]
                            && !Global.Instance.Stove3AnyMaterial && frmPlc.ReadM(frmPlc.Stove3AllowHouse))
                        {
                            Marking.UpStoveRefreshState[2] = true;
                        }
                        else
                        {
                            Marking.UpStoveRefreshState[2] = false;
                        }
                        if ((1 == Global.Instance.Stove4RunState || 2 == Global.Instance.Stove4RunState) && !Marking.DownStoveRefreshState[3]
                            && !Global.Instance.Stove4AnyMaterial && frmPlc.ReadM(frmPlc.Stove4AllowHouse))
                        {
                            Marking.UpStoveRefreshState[3] = true;
                        }
                        else
                        {
                            Marking.UpStoveRefreshState[3] = false;
                        }
                        if ((1 == Global.Instance.Stove5RunState || 2 == Global.Instance.Stove5RunState) && !Marking.DownStoveRefreshState[4]
                            && !Global.Instance.Stove5AnyMaterial && frmPlc.ReadM(frmPlc.Stove5AllowHouse))
                        {
                            Marking.UpStoveRefreshState[4] = true;
                        }
                        else
                        {
                            Marking.UpStoveRefreshState[4] = false;
                        }

                        if ((1 == Global.Instance.Stove6RunState || 2 == Global.Instance.Stove6RunState) && !Marking.DownStoveRefreshState[5]
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
                            Thread.Sleep(2000);
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
                            Thread.Sleep(2000);
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
                            Thread.Sleep(2000);
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
                            Thread.Sleep(2000);
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
                            Thread.Sleep(2000);
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

                        if (0 == Global.Instance.Stove1RunState)
                        {
                            if (RunPara.Instance.MesStoveTemperature.min <= RunPara.Instance.Stove[0].Temperature && !Marking.StartTemperatureSign[0])
                            {
                                Marking.StartTemperatureSign[0] = true;
                            }
                            if (Marking.StartTemperatureSign[0])
                            {
                                if (Marking.FirstTemperatureSign[0])
                                {
                                    Marking.CuringTime[0].Restart();  //开始固化计时 Stove1CuringTime
                                    Marking.FirstTemperatureSign[0] = false;
                                    RunPara.Instance.AverageTemperature[0] = RunPara.Instance.Stove[0].Temperature;
                                }
                                else  //平均固化温度
                                {
                                    RunPara.Instance.AverageTemperature[0] = (RunPara.Instance.AverageTemperature[0] + RunPara.Instance.Stove[0].Temperature) / 2;
                                }
                            }
                        }
                        else if (!Marking.FirstTemperatureSign[0] && Marking.StopCuringTimeSign[0] && 2 == Global.Instance.Stove1RunState)
                        {
                            Marking.StopCuringTimeSign[0] = false;
                            RunPara.Instance.StoveCuringTime[0] = frmPlc.ReadInt16Data(frmPlc.Stove1CuringTime);
                            Marking.CuringTime[0].Stop();
                            if (RunPara.Instance.StoveCuringTime[0] < 2000)
                            {
                                //  MessageBox.Show("1号炉异常");
                            }

                        }
                        else if (2 == Global.Instance.Stove1RunState && Global.Instance.Stove1AnyMaterial)
                        {
                            RunPara.Instance.StoveCuringTime[0] = frmPlc.ReadInt16Data(frmPlc.Stove1CuringTime);
                            if (RunPara.Instance.StoveCuringTime[0] < 2000)
                            {
                                //  MessageBox.Show("1号炉异常");
                            }
                        }

                        #endregion

                        #region 炉2固化数据获取
                        if (0 == Global.Instance.Stove2RunState)
                        {
                            //refreshTemperatureChart(2, StoveStartTime.ElapsedMilliseconds / 1000, RunPara.Instance.Stove[1].Temperature);
                            if (RunPara.Instance.MesStoveTemperature.min <= RunPara.Instance.Stove[1].Temperature && !Marking.StartTemperatureSign[1])
                            {
                                Marking.StartTemperatureSign[1] = true;
                            }
                            if (Marking.StartTemperatureSign[1])
                            {
                                if (Marking.FirstTemperatureSign[1])
                                {
                                    Marking.CuringTime[1].Restart();  //开始固化计时
                                    Marking.FirstTemperatureSign[1] = false;
                                    RunPara.Instance.AverageTemperature[1] = RunPara.Instance.Stove[1].Temperature;
                                }
                                else  //平均固化温度
                                {
                                    RunPara.Instance.AverageTemperature[1] = (RunPara.Instance.AverageTemperature[1] + RunPara.Instance.Stove[1].Temperature) / 2;
                                }
                            }
                        }
                        else if (!Marking.FirstTemperatureSign[1] && Marking.StopCuringTimeSign[1] && 2 == Global.Instance.Stove2RunState)
                        {
                            Marking.StopCuringTimeSign[1] = false;
                            RunPara.Instance.StoveCuringTime[1] = frmPlc.ReadInt16Data(frmPlc.Stove2CuringTime);
                            Marking.CuringTime[1].Stop();
                            if (RunPara.Instance.StoveCuringTime[1] < 2000)
                            {
                                if (ifalarmone)
                                {
                                    ifalarmone = false;
                                    //MessageBox.Show("2号炉异常"); }
                                }
                            }
                        }
                        else if (2 == Global.Instance.Stove2RunState && Global.Instance.Stove2AnyMaterial)
                        {
                            RunPara.Instance.StoveCuringTime[1] = frmPlc.ReadInt16Data(frmPlc.Stove2CuringTime);
                            if (RunPara.Instance.StoveCuringTime[1] < 2000)
                            {
                                if (ifalarmone)
                                {
                                    ifalarmone = false;
                                    //MessageBox.Show("2号炉异常");
                                }
                            }
                        }
                        #endregion

                        #region 炉3固化数据获取
                        if (0 == Global.Instance.Stove3RunState)
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
                        else if (!Marking.FirstTemperatureSign[2] && Marking.StopCuringTimeSign[2] && 2 == Global.Instance.Stove3RunState)
                        {
                            Marking.StopCuringTimeSign[2] = false;
                            RunPara.Instance.StoveCuringTime[2] = frmPlc.ReadInt16Data(frmPlc.Stove3CuringTime);
                            Marking.CuringTime[2].Stop();
                            if (RunPara.Instance.StoveCuringTime[2] < 2000)
                            {
                                if (ifalarmone)
                                {
                                    ifalarmone = false;
                                    // MessageBox.Show("3号炉异常");
                                }
                            }
                        }
                        else if (2 == Global.Instance.Stove3RunState && Global.Instance.Stove3AnyMaterial)
                        {
                            RunPara.Instance.StoveCuringTime[2] = frmPlc.ReadInt16Data(frmPlc.Stove3CuringTime);
                            if (RunPara.Instance.StoveCuringTime[2] < 2000)
                            {
                                // MessageBox.Show("3号炉异常");
                            }
                        }
                        #endregion

                        #region 炉4固化数据获取
                        if (0 == Global.Instance.Stove4RunState)
                        {
                            //refreshTemperatureChart(4, StoveStartTime.ElapsedMilliseconds / 1000, RunPara.Instance.Stove[3].Temperature);
                            RunPara.Instance.StoveCuringTime[3] = frmPlc.ReadInt16Data(frmPlc.Stove4CuringTime);
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
                        else if (!Marking.FirstTemperatureSign[3] && Marking.StopCuringTimeSign[3] && 2 == Global.Instance.Stove4RunState)
                        {
                            Marking.StopCuringTimeSign[3] = false;
                            RunPara.Instance.StoveCuringTime[3] = frmPlc.ReadInt16Data(frmPlc.Stove4CuringTime);
                            Marking.CuringTime[3].Stop();
                            //if (RunPara.Instance.StoveCuringTime[3] < 2000)
                            //{
                            //    if (ifalarmone)
                            //    {
                            //        ifalarmone = false;
                            //        MessageBox.Show("4号炉异常");
                            //    }
                            ////    MessageBox.Show("4号炉异常");
                            //}
                        }
                        else if (2 == Global.Instance.Stove4RunState && Global.Instance.Stove4AnyMaterial)
                        {
                            RunPara.Instance.StoveCuringTime[3] = frmPlc.ReadInt16Data(frmPlc.Stove4CuringTime);

                            //if (RunPara.Instance.StoveCuringTime[3] < 2000)
                            //{
                            //    if (ifalarmone)
                            //    {
                            //        ifalarmone = false;
                            //        MessageBox.Show("4号炉异常");
                            //    }
                            //  //  MessageBox.Show("4号炉异常");
                            //}
                        }
                        #endregion

                        #region 炉5固化数据获取
                        if (0 == Global.Instance.Stove5RunState)
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
                        else if (!Marking.FirstTemperatureSign[4] && Marking.StopCuringTimeSign[4] && 2 == Global.Instance.Stove5RunState)
                        {
                            Marking.StopCuringTimeSign[4] = false;
                            RunPara.Instance.StoveCuringTime[4] = frmPlc.ReadInt16Data(frmPlc.Stove5CuringTime);
                            Marking.CuringTime[4].Stop();
                            if (RunPara.Instance.StoveCuringTime[4] < 2000)
                            {
                                if (ifalarmone)
                                {
                                    ifalarmone = false;
                                    // MessageBox.Show("5号炉异常");
                                }
                            }
                        }
                        else if (2 == Global.Instance.Stove5RunState && Global.Instance.Stove5AnyMaterial)
                        {
                            RunPara.Instance.StoveCuringTime[4] = frmPlc.ReadInt16Data(frmPlc.Stove5CuringTime);
                            if (RunPara.Instance.StoveCuringTime[4] < 2000)
                            {
                                if (ifalarmone)
                                {
                                    ifalarmone = false;
                                    //  MessageBox.Show("5号炉异常");
                                }
                            }
                        }

                        #endregion

                        #region 炉6固化数据获取
                        if (0 == Global.Instance.Stove6RunState)
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
                        else if (!Marking.FirstTemperatureSign[5] && Marking.StopCuringTimeSign[5] && 2 == Global.Instance.Stove6RunState)
                        {
                            Marking.StopCuringTimeSign[5] = false;
                            RunPara.Instance.StoveCuringTime[5] = frmPlc.ReadInt16Data(frmPlc.Stove6CuringTime);
                            Marking.CuringTime[5].Stop();
                            if (RunPara.Instance.StoveCuringTime[5] < 2000)
                            {
                                if (ifalarmone)
                                {
                                    ifalarmone = false;
                                    //  MessageBox.Show("6号炉异常");
                                }
                            }
                        }
                        else if (2 == Global.Instance.Stove6RunState && Global.Instance.Stove6AnyMaterial)
                        {
                            RunPara.Instance.StoveCuringTime[5] = frmPlc.ReadInt16Data(frmPlc.Stove6CuringTime);
                            if (RunPara.Instance.StoveCuringTime[5] < 2000)
                            {
                                if (ifalarmone)
                                {
                                    ifalarmone = false;
                                    //  MessageBox.Show("6号炉异常");
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        frmPlc.WriteM(frmPlc.StoveLockControl, false);
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
                                stationInitialize.Flow = 10;
                                break;
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
                        m_Alarm = StoveRefreshAlarm.无消息;
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        //   public event RefreshDataCompleteEventHandler RefreshDataDataGridView;

        /// <summary>
        /// 流程报警集合
        /// </summary>
        protected override IList<Alarm> alarms()
        {
            var list = new List<Alarm>();

            list.Add(new Alarm(() => m_Alarm == StoveRefreshAlarm.初始化故障)
            {
                AlarmLevel = AlarmLevels.Error,
                Name = StoveRefreshAlarm.初始化故障.ToString()
            });

            return list;
        }

        /// <summary>
        /// 气缸状态集合
        /// </summary>
        protected override IList<ICylinderStatusJugger> cylinderStatus()
        {
            var list = new List<ICylinderStatusJugger>();

            return list;
        }


        public enum StoveRefreshAlarm : int
        {
            无消息,
            初始化故障,
        }
    }
}
