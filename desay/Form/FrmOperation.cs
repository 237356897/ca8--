using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Motion.Enginee;
using System.Threading;
using System.Diagnostics;
using Motion.Interfaces;
using System.Toolkit;
using System.Device;
using System.IO;

namespace Desay
{
    public partial class FrmOperationReversal : Form
    {
        private Backflow m_Backflow;
        private Robot m_Robot;

        public FrmOperationReversal(Backflow backflow, Robot robot)
        {
            InitializeComponent();
            m_Backflow = backflow;
            m_Robot = robot;
        }

        private void FrmOperation_Load(object sender, EventArgs e)
        {          
            m_Backflow.CarryAxis.Speed = (RunPara.Instance.CarryvelocityMax * RunPara.Instance.CarryvelocityRate) / 100;
            tbrJogSpeed.Value = RunPara.Instance.CarryvelocityRate;
            lblJogSpeed.Text = ((RunPara.Instance.CarryvelocityMax * tbrJogSpeed.Value) / 100).ToString("00.00") + "mm/s";

            labOrgPos.Text = RunPara.Instance.CarryAxisOrgPos.ToString("0.000");
            labCoolingPos.Text = RunPara.Instance.CarryAxisCoolingPos.ToString("0.000");
            labMovePos.Text = RunPara.Instance.CarryAxisMovePos.ToString("0.000");

            refreshTimer.Enabled = true;
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            refreshTimer.Enabled = false;

            #region 轴状态

            lblCurrentPositionCarry.Text = m_Backflow.CarryAxis.CurrentPos.ToString("0.000");

            lblCurrentSpeedCarry.Text = m_Backflow.CarryAxis.CurrentSpeed.ToString("0.000");

            chkCarryAxisIsServoOn.Checked = m_Backflow.CarryAxis.IsServon;

            #endregion

            #region 气缸状态

            #endregion

            refreshTimer.Enabled = true;
        }

        private void chkCarryAxisIsServoOn_CheckedChanged(object sender, EventArgs e)
        {
            if (m_Backflow.stationInitialize.Running || m_Backflow.stationOperate.Running) return;
            m_Backflow.CarryAxis.IsServon = chkCarryAxisIsServoOn.Checked;
        }

        private void tbrJogSpeed_Scroll(object sender, EventArgs e)
        {
            lblJogSpeed.Text = (RunPara.Instance.CarryvelocityMax * tbrJogSpeed.Value / 100).ToString("0.00") + "mm/s";
            m_Backflow.CarryAxis.Speed = (int)(RunPara.Instance.CarryvelocityMax * tbrJogSpeed.Value / 100);

            AxisParameter.Instance.CarryvelocityRate = tbrJogSpeed.Value;
        }

        private void btnLeftMove_Click(object sender, EventArgs e)
        {
            if (!rbnLocationMoveSelect.Checked) return;
            if (!m_Backflow.CarryAxis.IsDone) return;
            var Value = 0.0;
            if (rbnPos10um.Checked)
                Value = 0.01;
            if (rbnPos100um.Checked)
                Value = 0.10;
            if (rbnPos1000um.Checked)
                Value = 1.00;
            if (rbnPosOtherum.Checked)
                Value = (double)ndnPosOther.Value;
            Value *= -1;
            var velocityCurve = new VelocityCurve { Maxvel = m_Backflow.CarryAxis.Speed ?? 0 };
            m_Backflow.CarryAxis.MoveDelta(Value, velocityCurve);
        }

        private void btnRightMove_Click(object sender, EventArgs e)
        {
            if (!rbnLocationMoveSelect.Checked) return;
            if (!m_Backflow.CarryAxis.IsDone) return;
            var Value = 0.0;
            if (rbnPos10um.Checked)
                Value = 0.01;
            if (rbnPos100um.Checked)
                Value = 0.10;
            if (rbnPos1000um.Checked)
                Value = 1.00;
            if (rbnPosOtherum.Checked)
                Value = (double)ndnPosOther.Value;
            Value *= 1;
            var velocityCurve = new VelocityCurve { Maxvel = m_Backflow.CarryAxis.Speed ?? 0 };
            m_Backflow.CarryAxis.MoveDelta(Value, velocityCurve);
        }

        private void btnLeftMove_MouseDown(object sender, MouseEventArgs e)
        {
            if (!rbnContinueMoveSelect.Checked) return;
            m_Backflow.CarryAxis.Negative();
        }


        private void btnLeftMove_MouseUp(object sender, MouseEventArgs e)
        {
            if (rbnLocationMoveSelect.Checked) return;
            m_Backflow.CarryAxis.Stop();
        }

        private void btnRightMove_MouseDown(object sender,MouseEventArgs e)
        {
            if (!rbnContinueMoveSelect.Checked) return;
            m_Backflow.CarryAxis.Postive();
        }


        private void btnRightMove_MouseUp(object sender, MouseEventArgs e)
        {
            if (rbnLocationMoveSelect.Checked) return;
            m_Backflow.CarryAxis.Stop();
        }

        private void btnSaveOrg_Click(object sender, EventArgs e)
        {
            DialogResult r1 = MessageBox.Show("是否保存位置", "", MessageBoxButtons.YesNo);

                if(r1==DialogResult.Yes)

            {
                RunPara.Instance.CarryAxisOrgPos = m_Backflow.CarryAxis.CurrentPos;
                labOrgPos.Text = RunPara.Instance.CarryAxisOrgPos.ToString("0.000");


            }
         
        }

        private void btnGotoOrg_Click(object sender, EventArgs e)
        {
            //移动到指定位置
            if (!m_Backflow.CarryAxis.IsInPosition(RunPara.Instance.CarryAxisOrgPos)) m_Backflow.CarryAxis.MoveTo(RunPara.Instance.CarryAxisOrgPos, new VelocityCurve(0, (double)m_Backflow.CarryAxis.Speed, 0));
            while (true)
            {
                Thread.Sleep(10);
                if (m_Backflow.CarryAxis.IsInPosition(RunPara.Instance.CarryAxisOrgPos)) break;
                if (m_Backflow.CarryAxis.IsAlarmed || m_Backflow.CarryAxis.IsEmg || !m_Backflow.CarryAxis.IsServon
                    || m_Backflow.CarryAxis.IsPositiveLimit || m_Backflow.CarryAxis.IsNegativeLimit)
                {
                    m_Backflow.CarryAxis.Stop();
                }
            }
        }

        private void btnSaveCooling_Click(object sender, EventArgs e)
        {
       

            DialogResult r1 = MessageBox.Show("是否保存位置", "", MessageBoxButtons.YesNo);

            if (r1 == DialogResult.Yes)

            {
                RunPara.Instance.CarryAxisCoolingPos = m_Backflow.CarryAxis.CurrentPos;
                labCoolingPos.Text = RunPara.Instance.CarryAxisCoolingPos.ToString("0.000");


            }


        }

        private void btnGotoCooling_Click(object sender, EventArgs e)
        {
            //移动到指定位置
            if (!m_Backflow.CarryAxis.IsInPosition(RunPara.Instance.CarryAxisCoolingPos)) m_Backflow.CarryAxis.MoveTo(RunPara.Instance.CarryAxisCoolingPos, new VelocityCurve(0, (double)m_Backflow.CarryAxis.Speed, 0));
            while (true)
            {
                Thread.Sleep(10);
                if (m_Backflow.CarryAxis.IsInPosition(RunPara.Instance.CarryAxisCoolingPos)) break;
                if (m_Backflow.CarryAxis.IsAlarmed || m_Backflow.CarryAxis.IsEmg || !m_Backflow.CarryAxis.IsServon
                    || m_Backflow.CarryAxis.IsPositiveLimit || m_Backflow.CarryAxis.IsNegativeLimit)
                {
                    m_Backflow.CarryAxis.Stop();
                }
            }
        }

        private void btnSaveMove_Click(object sender, EventArgs e)
        {
         

            DialogResult r1 = MessageBox.Show("是否保存位置", "", MessageBoxButtons.YesNo);

            if (r1 == DialogResult.Yes)

            {
                RunPara.Instance.CarryAxisMovePos = m_Backflow.CarryAxis.CurrentPos;
                labMovePos.Text = RunPara.Instance.CarryAxisMovePos.ToString("0.000");


            }
        }

        private void btnGotoMove_Click(object sender, EventArgs e)
        {
            //移动到指定位置
            if (!m_Backflow.CarryAxis.IsInPosition(RunPara.Instance.CarryAxisMovePos)) m_Backflow.CarryAxis.MoveTo(RunPara.Instance.CarryAxisMovePos, new VelocityCurve(0, (double)m_Backflow.CarryAxis.Speed, 0));
            while (true)
            {
                Thread.Sleep(10);
                if (m_Backflow.CarryAxis.IsInPosition(RunPara.Instance.CarryAxisMovePos)) break;
                if (m_Backflow.CarryAxis.IsAlarmed || m_Backflow.CarryAxis.IsEmg || !m_Backflow.CarryAxis.IsServon
                    || m_Backflow.CarryAxis.IsPositiveLimit || m_Backflow.CarryAxis.IsNegativeLimit)
                {
                    m_Backflow.CarryAxis.Stop();
                }
            }
        }

        private void btnRobotStop_Click(object sender, EventArgs e)
        {
            btnRobotStop.BackColor = Color.Green;
            IoPoints.I2DO12.Value = true;
            Thread.Sleep(500);
            IoPoints.I2DO12.Value = false;
            btnRobotStop.BackColor = System.Drawing.SystemColors.Control;
            LogHelper.Debug("手动操作：ABB停止");
        }

        private void btnRobotReset_Click(object sender, EventArgs e)
        {
            btnRobotReset.BackColor = Color.Green;
            IoPoints.I2DO10.Value = true;
            Thread.Sleep(500);
            IoPoints.I2DO10.Value = false;
            btnRobotReset.BackColor = System.Drawing.SystemColors.Control;
            LogHelper.Debug("手动操作：ABB急停复位");
        }

        private void btnRobotElectrify_Click(object sender, EventArgs e)
        {
            btnRobotElectrify.BackColor = Color.Green;
            IoPoints.I2DO11.Value = true;
            Thread.Sleep(500);
            IoPoints.I2DO11.Value = false;
            btnRobotElectrify.BackColor = System.Drawing.SystemColors.Control;
            LogHelper.Debug("手动操作：ABB电机上电");
        }

        private void btnRobotInit_Click(object sender, EventArgs e)
        {
            btnRobotInit.BackColor = Color.Green;
            IoPoints.I2DO09.Value = true;
            Thread.Sleep(500);
            IoPoints.I2DO09.Value = false;
            btnRobotInit.BackColor = System.Drawing.SystemColors.Control;
            LogHelper.Debug("手动操作：ABB程序复位");
        }

        private void btnRobotStart_Click(object sender, EventArgs e)
        {
            btnRobotStart.BackColor = Color.Green;
            IoPoints.I2DO08.Value = true;
            Thread.Sleep(500);
            IoPoints.I2DO08.Value = false;
            btnRobotStart.BackColor = System.Drawing.SystemColors.Control;
            LogHelper.Debug("手动操作：启动ABB");
        }

        private void btnRobotConnect_Click(object sender, EventArgs e)
        {
            Stopwatch RobotTime = new Stopwatch();
            RobotTime.Restart();
            do
            {

                m_Robot.mAsynTcpRobot.AsynConnect();
                Thread.Sleep(10);
                if (RobotTime.ElapsedMilliseconds / 1000 > 10) break;
            }
            while (!m_Robot.mAsynTcpRobot.IsConnected);
            if (m_Robot.mAsynTcpRobot.IsConnected)
            {
                btnRobotConnect.BackColor = Color.Green;
                LogHelper.Debug("手动操作：ABB连接成功");
                MessageBox.Show("ABB连接成功");

            }
            else
            {
                btnRobotConnect.BackColor = System.Drawing.SystemColors.Control;
                LogHelper.Debug("手动操作：ABB连接失败");
                MessageBox.Show("ABB连接失败");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IoPoints.I2DO12.Value = true;
            Thread.Sleep(500);
            IoPoints.I2DO12.Value = false;
            Thread.Sleep(500);
            IoPoints.I2DO10.Value = true;
            Thread.Sleep(500);
            IoPoints.I2DO10.Value = false;
            Thread.Sleep(500);
            IoPoints.I2DO11.Value = true;
            Thread.Sleep(500);
            IoPoints.I2DO11.Value = false;
            Thread.Sleep(500);
            IoPoints.I2DO09.Value = true;
            Thread.Sleep(500);
            IoPoints.I2DO09.Value = false;
            Thread.Sleep(500);
            IoPoints.I2DO08.Value = true;
            Thread.Sleep(500);
            IoPoints.I2DO08.Value = false;
            Stopwatch RobotTime = new Stopwatch();
            RobotTime.Restart();

            do
            {
                m_Robot.mAsynTcpRobot.AsynConnect();
                Thread.Sleep(100);
                if (RobotTime.ElapsedMilliseconds / 1000 > 20) break;
            }
            while (!m_Robot.mAsynTcpRobot.IsConnected);
            if (m_Robot.mAsynTcpRobot.IsConnected)
            {
                string type = string.Empty;
                string[] result;
                bool ChangeType = false;
                Stopwatch RobotChangeTime = new Stopwatch();
                RobotChangeTime.Restart();
                switch (Product.Instance.CurrentProductType)
                {
                    case "20.5M":
                        type = "m010000";
                        break;
                    case "FreeTech":
                        type = "m020000";
                        break;
                    default:
                        break;
                }
                if (type != string.Empty)
                {
                    RobotChangeTime.Restart();
                    m_Robot.mAsynTcpRobot.AsynSend(type);
                    do
                    {
                        result = m_Robot.mAsynTcpRobot.Result.Split('\r');
                        if (result.Length > 0 && result[0].Contains(type.Substring(0, 3)))
                        {
                            ChangeType = true;
                        }
                        Thread.Sleep(100);
                    } while (!ChangeType && (RobotChangeTime.ElapsedMilliseconds / 1000 < 10));
                }
                if (ChangeType)
                {
                    MessageBox.Show("ABB连接成功!", Product.Instance.CurrentProductType.ToString());
                }
                else
                {
                    MessageBox.Show("ABB连接失败!");
                }

            }
            else
            {
                //btnRobotConnect.BackColor = System.Drawing.SystemColors.Control;
                LogHelper.Debug("手动操作：ABB连接失败");
                MessageBox.Show("ABB连接失败");
            }


        }

        private void btnscan_Click(object sender, EventArgs e)
        {

            m_Robot.QRCodeReader.receiveFinish = false;
            m_Robot.QRCodeReader.Trigger(new TriggerArgs()  //扫码
            {
                sender = this,
                tryTimes = 1,
                message = "SN\r\n"
            });
           
        }

        private void btnLonFan_Click(object sender, EventArgs e)
        {
            if (IoPoints.I2DO18.Value)
            {
                IoPoints.I2DO18.Value = false;
                btnLonFan.Text = "等离子风扇关";
                btnLonFan.BackColor = System.Drawing.SystemColors.Control;
            }
            else
            {
                IoPoints.I2DO18.Value = true;
                btnLonFan.Text = "等离子风扇开";
                btnLonFan.BackColor = Color.Green;
            }
        }

        private void btnReadyToAA_Click(object sender, EventArgs e)
        {
            if (IoPoints.T1DO00.Value)
            {
                IoPoints.T1DO00.Value = false;
                btnReadyToAA.Text = "取料完成信号-OFF";
                btnReadyToAA.BackColor = System.Drawing.SystemColors.Control;
            }
            else
            {
                IoPoints.T1DO00.Value = true;
                btnReadyToAA.Text = "取料完成信号-ON";
                btnReadyToAA.BackColor = Color.Green;
            }
        }

        private void btnProductToAA_Click(object sender, EventArgs e)
        {
            if (IoPoints.T1DO01.Value)
            {
                IoPoints.T1DO01.Value = false;
                btnProductToAA.Text = "AA开夹信号-OFF";
                btnProductToAA.BackColor = System.Drawing.SystemColors.Control;
            }
            else
            {
                IoPoints.T1DO01.Value = true;
                btnProductToAA.Text = "AA开夹信号-ON";
                btnProductToAA.BackColor = Color.Green;
            }
        }

        private void btnscan2_Click(object sender, EventArgs e)
        {
            m_Backflow.TrayCodeReader.receiveFinish = false;
            m_Backflow.TrayCodeReader.Trigger(new TriggerArgs()  //扫码
            {
                sender = this,
                tryTimes = 1,
                message = "FN\r\n"
            });
        }

        private void btnChangeType_Click(object sender, EventArgs e)
        {
            if (m_Robot.mAsynTcpRobot.IsConnected)
            {
                string type = string.Empty;
                string[] result;
                bool ChangeType = false;
                Stopwatch RobotChangeTime = new Stopwatch();
                RobotChangeTime.Restart();
                switch (Product.Instance.CurrentProductType)
                {
                    case "20.5M":
                        type = "m010000";
                        break;
                    case "FreeTech":
                        type = "m020000";
                        break;
                    default:
                       // type = "m010000";
                        break;
                }
                if (type != string.Empty)
                {
                    RobotChangeTime.Restart();
                    m_Robot.mAsynTcpRobot.AsynSend(type);
                    do
                    {
                        result = m_Robot.mAsynTcpRobot.Result.Split('\r');
                        if (result.Length > 0 && result[0].Contains(type.Substring(0, 3)))
                        {
                            ChangeType = true;
                        }
                        Thread.Sleep(100);
                    } while (!ChangeType && (RobotChangeTime.ElapsedMilliseconds / 1000 < 10));
                }
                if (ChangeType)
                {
                    MessageBox.Show("ABB换型成功");
                }
                else
                {
                    MessageBox.Show("ABB换型失败");
                }
            }
            else
            {
                MessageBox.Show("ABB未连接");
            }
        }
    }
}
