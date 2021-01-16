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

        private CylinderOperate positioningCylinder, openClampCylinder, translationCylinder;

        private CylinderParameter positioningParameter, openClampParameter, translationParameter;

        private Connection m_Connection;
        private Backflow m_Backflow;
        private Robot m_Robot;

        public FrmOperationReversal(Connection connection, Backflow backflow, Robot robot)
        {
            InitializeComponent();

            m_Connection = connection;
            m_Backflow = backflow;
            m_Robot = robot;

            positioningParameter = new CylinderParameter(Delay.Instance.PositioningDelay) {  Name = "接驳台定位气缸" };
            flpCylinderParam.Controls.Add(positioningParameter);
            openClampParameter = new CylinderParameter(Delay.Instance.OpenClampDelay) { Name = "接驳台开夹气缸" };
            flpCylinderParam.Controls.Add(openClampParameter);
            translationParameter = new CylinderParameter(Delay.Instance.TranslationDelay) { Name = "接驳台平移气缸" };
            flpCylinderParam.Controls.Add(translationParameter);

        }

        private void FrmOperation_Load(object sender, EventArgs e)
        {
            positioningCylinder = new CylinderOperate(() => { m_Connection.PositioningCylinder.ManualExecute(); }) { CylinderName = "接驳台定位气缸" };
            flpCylinder.Controls.Add(positioningCylinder);
            openClampCylinder = new CylinderOperate(() => { m_Connection.OpenClampCylinder.ManualExecute(); }) { CylinderName = "接驳台开夹气缸" };
            flpCylinder.Controls.Add(openClampCylinder);
            translationCylinder = new CylinderOperate(() => { m_Connection.TranslationCylinder.ManualExecute(); }) { CylinderName = "接驳台平移气缸" };
            flpCylinder.Controls.Add(translationCylinder);
            m_Backflow.CarryAxis.Speed = 10;

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

            positioningCylinder.InOrigin = m_Connection.PositioningCylinder.OutOriginStatus;
            positioningCylinder.InMove = m_Connection.PositioningCylinder.OutMoveStatus;
            positioningCylinder.OutMove = m_Connection.PositioningCylinder.IsOutMove;

            openClampCylinder.InOrigin = m_Connection.OpenClampCylinder.OutOriginStatus;
            openClampCylinder.InMove = m_Connection.OpenClampCylinder.OutMoveStatus;
            openClampCylinder.OutMove = m_Connection.OpenClampCylinder.IsOutMove;

            translationCylinder.InOrigin = m_Connection.TranslationCylinder.OutOriginStatus;
            translationCylinder.InMove = m_Connection.TranslationCylinder.OutMoveStatus;
            translationCylinder.OutMove = m_Connection.TranslationCylinder.IsOutMove;

            #endregion

            refreshTimer.Enabled = true;
        }

        private void btnSaveCylinder_Click(object sender, EventArgs e)
        {
            Delay.Instance.PositioningDelay = positioningParameter.Save;
            Delay.Instance.OpenClampDelay = openClampParameter.Save;
            Delay.Instance.TranslationDelay = translationParameter.Save;
        }

        private void chkCarryAxisIsServoOn_CheckedChanged(object sender, EventArgs e)
        {
            if (m_Backflow.stationInitialize.Running || m_Backflow.stationOperate.Running) return;
            m_Backflow.CarryAxis.IsServon = chkCarryAxisIsServoOn.Checked;
        }

        private void tbrJogSpeed_Scroll(object sender, EventArgs e)
        {
            lblJogSpeed.Text = tbrJogSpeed.Value.ToString("0.00") + "mm/s";
            m_Backflow.CarryAxis.Speed = (int)tbrJogSpeed.Value;

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

        private void btnConnectionForeward_Click(object sender, EventArgs e)
        {
            IoPoints.I2DO20.Value = true;
            IoPoints.I2DO21.Value = false;
            btnConnectionForeward.BackColor = Color.Green;
            btnConnectionReversal.BackColor = System.Drawing.SystemColors.Control;
            btnConnectionStop.BackColor = System.Drawing.SystemColors.Control;
        }

        private void btnConnectionReversal_Click(object sender, EventArgs e)
        {
            IoPoints.I2DO20.Value = false;
            IoPoints.I2DO21.Value = true;
            btnConnectionForeward.BackColor = System.Drawing.SystemColors.Control;
            btnConnectionReversal.BackColor = Color.Green;
            btnConnectionStop.BackColor = System.Drawing.SystemColors.Control;
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


            Thread.Sleep(2000);

            do
            {

                m_Robot.mAsynTcpRobot.AsynConnect();
                Thread.Sleep(10);
                if (RobotTime.ElapsedMilliseconds / 1000 > 10) break;
            }
            while (!m_Robot.mAsynTcpRobot.IsConnected);
            if (m_Robot.mAsynTcpRobot.IsConnected)
            {
                //  btnRobotConnect.BackColor = Color.Green;
                LogHelper.Debug("手动操作：ABB连接成功");
                MessageBox.Show("ABB连接成功");

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

        private void btnConnectionStop_Click(object sender, EventArgs e)
        {
            IoPoints.I2DO20.Value = false;
            IoPoints.I2DO21.Value = false;
            btnConnectionForeward.BackColor = System.Drawing.SystemColors.Control;
            btnConnectionReversal.BackColor = System.Drawing.SystemColors.Control;
            btnConnectionStop.BackColor = System.Drawing.SystemColors.Control;
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
                btnReadyToAA.Text = "接驳台Ready开(ToAA)";
                btnReadyToAA.BackColor = System.Drawing.SystemColors.Control;
            }
            else
            {
                IoPoints.T1DO00.Value = true;
                btnReadyToAA.Text = "接驳台Ready关(ToAA)";
                btnReadyToAA.BackColor = Color.Green;
            }
        }

        private void btnProductToAA_Click(object sender, EventArgs e)
        {
            if (IoPoints.T1DO01.Value)
            {
                IoPoints.T1DO01.Value = false;
                btnProductToAA.Text = "接驳台有料开(ToAA)";
                btnProductToAA.BackColor = System.Drawing.SystemColors.Control;
            }
            else
            {
                IoPoints.T1DO01.Value = true;
                btnProductToAA.Text = "接驳台有料关(ToAA)";
                btnProductToAA.BackColor = Color.Green;
            }
        }
    }
}
