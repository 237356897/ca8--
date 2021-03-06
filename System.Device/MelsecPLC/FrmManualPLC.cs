﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Toolkit;
using System.Drawing.Printing;

namespace System.Device
{
    public partial class FrmManualPLC : Form
    {
        public static Common.void_StringDelegate AppendLog;
        public Common.ExecuteDelegate ConnectStatus;
        /// <summary>
        /// 主界面TabControl
        /// </summary>
        public TabControl FrmTab;
        private MelsecPLCOperator plc1;
        public string plcStationNumber;

        public FrmManualPLC(string plcStationNumber,string inputStartAddressName, int inputCount, string outputStartAddressName, int outputCount,TabControl Tabc)
        {
            InitializeComponent();
            this.plcStationNumber = plcStationNumber;
            toolStripTxtStationNumber.Text = this.plcStationNumber;
            plc1 = new MelsecPLCOperator(new MelsecPLCTcpUtl(toolStripTxtStationNumber.Text), inputStartAddressName, inputCount, outputStartAddressName, outputCount);
            ConnectStatus += plc1.Melsec_PLC.ConnectStatus;
            FrmTab = Tabc;
        }

        private void FrmManualPLC_Load(object sender, EventArgs e)
        {
            onOFFControlsEnabled  = IsConnect;
        }


        #region Public Method        
        public bool IsConnect { get { return plc1.Melsec_PLC.ConnectResult == 0 ? true : false; } }

        public void Connect()
        {
            if (!IsConnect)
            {
                plc1.Melsec_PLC.Open(toolStripTxtStationNumber.Text);
            }
            onOFFControlsEnabled = IsConnect;
        }
        public void Disconnect()
        {
            if (IsConnect)
            {
                plc1.Melsec_PLC.Close();
            }
            onOFFControlsEnabled = IsConnect;
        }

        private object obj = new object();
        /// <summary>
        /// 写入M区中的位状态
        /// </summary>
        /// <param name="addr">M地址</param>
        /// <param name="status">写入状态</param>
        public void WriteM(int addr, bool status)
        {
            
            if (IsConnect)
            {
                lock (obj)
                {
                    plc1.Melsec_PLC.WriteM(addr, status);
                }
                
            }
        }

        /// <summary>
        /// 读取M区中的位状态
        /// </summary>
        /// <param name="addr">M地址</param>
        /// <returns>返回状态</returns>
        public bool ReadM(int addr)
        {
            if (IsConnect)
            {
                return plc1.Melsec_PLC.ReadM(addr);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 写入X区中的位状态
        /// </summary>
        /// <param name="addr">M地址</param>
        /// <param name="status">写入状态</param>
        public void WriteX(int addr, bool status)
        {
            if (IsConnect) plc1.Melsec_PLC.WriteX(addr, status);
        }

        /// <summary>
        /// 读取X区中的位状态
        /// </summary>
        /// <param name="addr">M地址</param>
        /// <returns>返回状态</returns>
        public bool ReadX(int addr)
        {
            if (IsConnect)
            {
                return plc1.Melsec_PLC.ReadX(addr);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 写入D字数据(16位数据)
        /// </summary>
        /// <param name="address">D地址</param>
        /// <param name="data"></param>
        public void WriteInt16Data(int address, int data)
        {
            if (IsConnect) plc1.WriteD(address, 1, data);
        }

        /// <summary>
        /// 读取D字数据(16位数据)
        /// </summary>
        /// <param name="address">D地址</param>
        /// <returns>返回所读取的16位int整数数据</returns>
        public int ReadInt16Data(int address)
        {
            if (IsConnect)
            {
                return plc1.ReadD(address, 1);
            }
            else
            {
                return 0;
            }
        }

        #endregion

        #region PLC地址

        #region WriteBit

        /// <summary>
        /// 锁控件        
        /// </summary>
        public int StoveLockControl = 500;

        //一号炉
        /// <summary>
        /// 门开关
        /// </summary>
        public int Stove1DoorTrip = 114;      
        /// <summary>
        /// 自动启动
        /// </summary>
        public int Stove1AutoStart = 110;     
        /// <summary>
        /// 自动停止
        /// </summary>
        public int Stove1AutoStop = 111;     

        //二号炉
        /// <summary>
        /// 门开关
        /// </summary>
        public int Stove2DoorTrip = 124;       
        /// <summary>
        /// 自动启动
        /// </summary>
        public int Stove2AutoStart = 120;      
        /// <summary>
        /// 自动停止
        /// </summary>
        public int Stove2AutoStop = 121;       

        //三号炉
        /// <summary>
        /// 门开关
        /// </summary>
        public int Stove3DoorTrip = 134;        
        /// <summary>
        /// 自动启动
        /// </summary>
        public int Stove3AutoStart = 130;       
        /// <summary>
        /// 自动停止
        /// </summary>
        public int Stove3AutoStop = 131;       

        //四号炉
        /// <summary>
        /// 门开关
        /// </summary>
        public int Stove4DoorTrip = 144;        
        /// <summary>
        /// 自动启动
        /// </summary>
        public int Stove4AutoStart = 140;       
        /// <summary>
        /// 自动停止
        /// </summary>
        public int Stove4AutoStop = 141;        

        //五号炉
        /// <summary>
        /// 门开关
        /// </summary>
        public int Stove5DoorTrip = 154;        
        /// <summary>
        /// 自动启动
        /// </summary>
        public int Stove5AutoStart = 150;      
        /// <summary>
        /// 自动停止
        /// </summary>
        public int Stove5AutoStop = 151;        

        //六号炉
        /// <summary>
        /// 门开关
        /// </summary>
        public int Stove6DoorTrip = 164;        
        /// <summary>
        /// 自动启动
        /// </summary>
        public int Stove6AutoStart = 160;     
        /// <summary>
        /// 自动停止
        /// </summary>
        public int Stove6AutoStop = 161;

        #endregion

        #region ReadBit
        /// <summary>
        /// 炉1有料感应
        /// </summary>
        public int Stove1AnyMaterial = 301;     
        /// <summary>
        /// 炉2有料感应
        /// </summary>
        public int Stove2AnyMaterial = 302;    
        /// <summary>
        /// 炉3有料感应
        /// </summary>
        public int Stove3AnyMaterial = 303;    
        /// <summary>
        /// 炉4有料感应
        /// </summary>
        public int Stove4AnyMaterial = 304;    
        /// <summary>
        /// 炉5有料感应
        /// </summary>
        public int Stove5AnyMaterial = 305;     
        /// <summary>
        /// 炉6有料感应
        /// </summary>
        public int Stove6AnyMaterial = 306;    

        //WriteInt16

        //ReadInt16
        //一号炉
        /// <summary>
        /// 自动运行状态(0=待料 1=运行 2=完成)
        /// </summary>
        public int Stove1RunState = 71;         
        /// <summary>
        /// 设计温度
        /// </summary>
        public int SetStove1Temperature = 500;  
        /// <summary>
        /// 实时温度
        /// </summary>
        public int Stove1Temperature = 720;    
        /// <summary>
        /// 剩余时间
        /// </summary>
        public int Stove1TimeRemaining= 710;   
        /// <summary>
        /// 固化时间
        /// </summary>
        public int Stove1CuringTime = 901;      

        //二号炉
        /// <summary>
        /// 自动运行状态(0=待料 1=运行 2=完成)
        /// </summary>
        public int Stove2RunState = 72;     
        /// <summary>
        /// 设计温度
        /// </summary>
        public int SetStove2Temperature = 504;  
        /// <summary>
        /// 实时温度
        /// </summary>
        public int Stove2Temperature = 750;     
        /// <summary>
        /// 剩余时间
        /// </summary>
        public int Stove2TimeRemaining = 740;   
        /// <summary>
        /// 固化时间
        /// </summary>
        public int Stove2CuringTime = 902;      

        //三号炉
        /// <summary>
        /// 自动运行状态(0=待料 1=运行 2=完成)
        /// </summary>
        public int Stove3RunState = 73;        
        /// <summary>
        /// 设计温度
        /// </summary>
        public int SetStove3Temperature = 508;  
        /// <summary>
        /// 实时温度
        /// </summary>
        public int Stove3Temperature = 780;     
        /// <summary>
        /// 剩余时间
        /// </summary>
        public int Stove3TimeRemaining = 770;   
        /// <summary>
        /// 固化时间
        /// </summary>
        public int Stove3CuringTime = 903;      

        //四号炉
        /// <summary>
        /// 自动运行状态(0=待料 1=运行 2=完成)
        /// </summary>
        public int Stove4RunState = 74;        
        /// <summary>
        /// 设计温度
        /// </summary>
        public int SetStove4Temperature = 510;  
        /// <summary>
        /// 实时温度
        /// </summary>
        public int Stove4Temperature = 820;    
        /// <summary>
        /// 剩余时间
        /// </summary>
        public int Stove4TimeRemaining = 810;   
        /// <summary>
        /// 固化时间
        /// </summary>
        public int Stove4CuringTime = 904;     

        //五号炉
        /// <summary>
        /// 自动运行状态(0=待料 1=运行 2=完成)
        /// </summary>
        public int Stove5RunState = 75;        
        /// <summary>
        /// 设计温度
        /// </summary>
        public int SetStove5Temperature = 512; 
        /// <summary>
        /// 实时温度
        /// </summary>
        public int Stove5Temperature = 850;    
        /// <summary>
        /// 剩余时间
        /// </summary>
        public int Stove5TimeRemaining = 890;  
        /// <summary>
        /// 固化时间
        /// </summary>
        public int Stove5CuringTime = 905;     

        //六号炉
        /// <summary>
        /// 自动运行状态(0=待料 1=运行 2=完成)
        /// </summary>
        public int Stove6RunState = 76;       
        /// <summary>
        /// 设计温度
        /// </summary>
        public int SetStove6Temperature = 514;
        /// <summary>
        /// 实时温度
        /// </summary>
        public int Stove6Temperature = 880;    
        /// <summary>
        /// 剩余时间
        /// </summary>
        public int Stove6TimeRemaining = 870;  
        /// <summary>
        /// 固化时间
        /// </summary>
        public int Stove6CuringTime = 906;     


        //ReadM地址
        //一号炉
        /// <summary>
        /// 门前感应
        /// </summary>
        public int Stove1DoorFront = 192;       
        /// <summary>
        /// 门后感应
        /// </summary>
        public int Stove1DoorQueen = 198;      
        /// <summary>
        /// 起温报警
        /// </summary>
        public int Stove1STAlarm = 311;        
        /// <summary>
        /// 允许放盘
        /// </summary>
        public int Stove1AllowHouse = 513;


        //二号炉
        /// <summary>
        /// 门前感应
        /// </summary>
        public int Stove2DoorFront = 193;      
        /// <summary>
        /// 门后感应
        /// </summary>
        public int Stove2DoorQueen = 199;       
        /// <summary>
        /// 起温报警
        /// </summary>
        public int Stove2STAlarm = 312;        
        /// <summary>
        /// 允许放盘
        /// </summary>
        public int Stove2AllowHouse = 523;     


        //三号炉
        /// <summary>
        /// 门前感应
        /// </summary>
        public int Stove3DoorFront = 194;    
        /// <summary>
        /// 门后感应
        /// </summary>
        public int Stove3DoorQueen = 200;     
        /// <summary>
        /// 起温报警
        /// </summary>
        public int Stove3STAlarm = 313;       
        /// <summary>
        /// 允许放盘
        /// </summary>
        public int Stove3AllowHouse = 533;     

        //四号炉
        /// <summary>
        /// 门前感应
        /// </summary>
        public int Stove4DoorFront = 195;      
        /// <summary>
        /// 门后感应
        /// </summary>
        public int Stove4DoorQueen = 201;     
        /// <summary>
        /// 起温报警
        /// </summary>
        public int Stove4STAlarm = 314;        
        /// <summary>
        /// 允许放盘
        /// </summary>
        public int Stove4AllowHouse = 543;   


        //五号炉
        /// <summary>
        /// 门前感应
        /// </summary>
        public int Stove5DoorFront = 196;     
        /// <summary>
        /// 门后感应
        /// </summary>
        public int Stove5DoorQueen = 202;       
        /// <summary>
        /// 起温报警
        /// </summary>
        public int Stove5STAlarm = 315;        
        /// <summary>
        /// 允许放盘
        /// </summary>
        public int Stove5AllowHouse = 553;  


        //六号炉
        /// <summary>
        /// 门前感应
        /// </summary>
        public int Stove6DoorFront = 197;     
        /// <summary>
        /// 门后感应
        /// </summary>
        public int Stove6DoorQueen = 203;      
        /// <summary>
        /// 起温报警
        /// </summary>
        public int Stove6STAlarm = 316;        
        /// <summary>
        /// 允许放盘
        /// </summary>
        public int Stove6AllowHouse = 563;     

        /// <summary>
        /// 固化炉急停
        /// </summary>
        public int StoveScram = 204;

        #endregion

        #endregion

        #region Windows Operation

        private bool onOFFControlsEnabled
        {
            set
            {
                toolStripBtnDisconn.Enabled = toolStripTxtStationNumber.ReadOnly = value;
                toolStripBtnConn.Enabled = !toolStripBtnDisconn.Enabled;
            }
        }
        
        private void toolStripBtnConn_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void toolStripBtnDisconn_Click(object sender, EventArgs e)
        {
            Disconnect();
        }
        private void toolStripBtnSave_Click(object sender, EventArgs e)
        {
            this.plcStationNumber = toolStripTxtStationNumber.Text;

            MessageBox.Show("plc1参数保存成功！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            AppendLog("PLC参数保存成功！");
        }

        #region Signal Opration       
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            bool[] StoveDoors = plc1.melsecPLCTcpUtl.read16Bit(192, "M");
            bool[] StoveTAlarm = plc1.melsecPLCTcpUtl.read16Bit(304, "M");
            bool[] StoveAnyMaterial = plc1.melsecPLCTcpUtl.read16Bit(288, "M");

            btnPLCI0.Enabled = StoveDoors[0];
            btnPLCI1.Enabled = StoveDoors[6];           
            btnPLCI2.Enabled = StoveTAlarm[7];//311

            btnPLCI3.Enabled = StoveDoors[1];
            btnPLCI4.Enabled = StoveDoors[7];
            btnPLCI5.Enabled = StoveTAlarm[8];//312

            btnPLCI6.Enabled = StoveDoors[2];
            btnPLCI7.Enabled = StoveDoors[8];
            btnPLCI8.Enabled = StoveTAlarm[9];//313

            btnPLCI9.Enabled = StoveDoors[3];
            btnPLCI10.Enabled = StoveDoors[9];
            btnPLCI11.Enabled = StoveTAlarm[10];//314

            btnPLCI12.Enabled = StoveDoors[4];
            btnPLCI13.Enabled = StoveDoors[10];
            btnPLCI14.Enabled = StoveTAlarm[11];//315

            btnPLCI15.Enabled = StoveDoors[5];
            btnPLCI16.Enabled = StoveDoors[11];
            btnPLCI17.Enabled = StoveTAlarm[12];//316

            btnPLCI18.Enabled = StoveDoors[12];

            btnPLCI19.Enabled = StoveAnyMaterial[13]; //ReadM(Stove1AnyMaterial) 301
            btnPLCI20.Enabled = StoveAnyMaterial[14]; //ReadM(Stove2AnyMaterial) 302
            btnPLCI21.Enabled = StoveAnyMaterial[15]; //ReadM(Stove3AnyMaterial) 303
            btnPLCI22.Enabled = StoveTAlarm[0]; //ReadM(Stove4AnyMaterial) 
            btnPLCI23.Enabled = StoveTAlarm[1]; //ReadM(Stove5AnyMaterial) 
            btnPLCI24.Enabled = StoveTAlarm[2]; //ReadM(Stove6AnyMaterial) 

            txtChannel1Lever.Text = ReadInt16Data(Stove1RunState).ToString();
            txtChannel2Lever.Text = ReadInt16Data(Stove1Temperature).ToString();
            txtChannel3Lever.Text = ReadInt16Data(Stove1TimeRemaining).ToString();

            txtChannel4Lever.Text = ReadInt16Data(Stove2RunState).ToString();
            txtChannel5Lever.Text = ReadInt16Data(Stove2Temperature).ToString();
            txtChannel6Lever.Text = ReadInt16Data(Stove2TimeRemaining).ToString();

            txtChannel7Lever.Text = ReadInt16Data(Stove3RunState).ToString();
            txtChannel8Lever.Text = ReadInt16Data(Stove3Temperature).ToString();
            txtChannel9Lever.Text = ReadInt16Data(Stove3TimeRemaining).ToString();

            txtChannel10Lever.Text = ReadInt16Data(Stove4RunState).ToString();
            txtChannel11Lever.Text = ReadInt16Data(Stove4Temperature).ToString();
            txtChannel12Lever.Text = ReadInt16Data(Stove4TimeRemaining).ToString();

            txtChannel13Lever.Text = ReadInt16Data(Stove5RunState).ToString();
            txtChannel14Lever.Text = ReadInt16Data(Stove5Temperature).ToString();
            txtChannel15Lever.Text = ReadInt16Data(Stove5TimeRemaining).ToString();

            txtChannel16Lever.Text = ReadInt16Data(Stove6RunState).ToString();
            txtChannel17Lever.Text = ReadInt16Data(Stove6Temperature).ToString();
            txtChannel18Lever.Text = ReadInt16Data(Stove6TimeRemaining).ToString();

            if (!ReadM(StoveLockControl) && FrmTab.SelectedTab.Name == "tbgPLCMonitor")
            {
                timer1.Enabled = true;
            }
        }


        #endregion

        #endregion

        #region Key Press
        private void toolStripTxtIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            Common.IsDigitalInput(sender, e);
        }
        private void txtChannel1Lever_KeyPress(object sender, KeyPressEventArgs e)
        {
            Common.IsDigitalInput(sender, e);
        }

        private void txtChannel2Lever_KeyPress(object sender, KeyPressEventArgs e)
        {
            Common.IsDigitalInput(sender, e);
        }

        private void txtChannel3Lever_KeyPress(object sender, KeyPressEventArgs e)
        {
            Common.IsDigitalInput(sender, e);
        }

        private void txtChannel4Lever_KeyPress(object sender, KeyPressEventArgs e)
        {
            Common.IsDigitalInput(sender, e);
        }

        private void txtChannel5Lever_KeyPress(object sender, KeyPressEventArgs e)
        {
            Common.IsDigitalInput(sender, e);
        }

        private void txtChannel6Lever_KeyPress(object sender, KeyPressEventArgs e)
        {
            Common.IsDigitalInput(sender, e);
        }

        private void txtChannel7Lever_KeyPress(object sender, KeyPressEventArgs e)
        {
            Common.IsDigitalInput(sender, e);
        }

        private void txtChannel8Lever_KeyPress(object sender, KeyPressEventArgs e)
        {
            Common.IsDigitalInput(sender, e);
        }

        private void txtChannel9Lever_KeyPress(object sender, KeyPressEventArgs e)
        {
            Common.IsDigitalInput(sender, e);
        }

        private void txtChannel10Lever_KeyPress(object sender, KeyPressEventArgs e)
        {
            Common.IsDigitalInput(sender, e);
        }
        #endregion

        #region 信号输出PLC
        private void btnPLCO0_Click(object sender, EventArgs e)
        {
            if(ReadM(Stove1DoorFront))
            {
                WriteM(Stove1DoorTrip, false);
                btnPLCO0.BackColor = Color.Transparent;
            }
            if(ReadM(Stove1DoorQueen))
            {
                WriteM(Stove1DoorTrip, true);
                btnPLCO0.BackColor = Color.DarkGray;
            }
            LogHelper.Debug("手动操作：炉1开关");
        }

        private void btnPLCO1_Click(object sender, EventArgs e)
        {
            WriteM(Stove1AutoStart, true);
            LogHelper.Debug("手动操作：炉1自动运行");
        }

        private void btnPLCO2_Click(object sender, EventArgs e)
        {
            WriteM(Stove1AutoStop, true);
            LogHelper.Debug("手动操作：炉1自动停止");
        }

        private void btnPLCO3_Click(object sender, EventArgs e)
        {
            if (ReadM(Stove2DoorFront))
            {
                WriteM(Stove2DoorTrip, false);
                btnPLCO3.BackColor = Color.Transparent;
            }
            if (ReadM(Stove2DoorQueen))
            {
                WriteM(Stove2DoorTrip, true);
                btnPLCO3.BackColor = Color.DarkGray;
            }
            LogHelper.Debug("手动操作：炉2开关");
        }

        private void btnPLCO4_Click(object sender, EventArgs e)
        {
            WriteM(Stove2AutoStart, true);
            LogHelper.Debug("手动操作：炉2自动运行");
        }

        private void btnPLCO5_Click(object sender, EventArgs e)
        {
            WriteM(Stove2AutoStop, true);
            LogHelper.Debug("手动操作：炉2自动停止");
        }

        private void btnPLCO6_Click(object sender, EventArgs e)
        { 
            if (ReadM(Stove3DoorFront))
            {
                WriteM(Stove3DoorTrip, false);
                btnPLCO6.BackColor = Color.Transparent;
            }
            if (ReadM(Stove3DoorQueen))
            {
                WriteM(Stove3DoorTrip, true);
                btnPLCO6.BackColor = Color.DarkGray;
            }
            LogHelper.Debug("手动操作：炉3开关");
        }

        private void btnPLCO7_Click(object sender, EventArgs e)
        {
            WriteM(Stove3AutoStart, true);
            LogHelper.Debug("手动操作：炉3自动运行");
        }

        private void btnPLCO8_Click(object sender, EventArgs e)
        {
            WriteM(Stove3AutoStop, true);
            LogHelper.Debug("手动操作：炉3自动停止");
        }

        private void btnPLCO9_Click(object sender, EventArgs e)
        {
            if (ReadM(Stove4DoorFront))
            {
                WriteM(Stove4DoorTrip, false);
                btnPLCO9.BackColor = Color.Transparent;
            }
            if (ReadM(Stove4DoorQueen))
            {
                WriteM(Stove4DoorTrip, true);
                btnPLCO9.BackColor = Color.DarkGray;
            }
            LogHelper.Debug("手动操作：炉4开关");
        }

        private void btnPLCO10_Click(object sender, EventArgs e)
        {
            WriteM(Stove4AutoStart, true);
            LogHelper.Debug("手动操作：炉4自动运行");
        }

        private void btnPLCO11_Click(object sender, EventArgs e)
        {
            WriteM(Stove4AutoStop, true);
            LogHelper.Debug("手动操作：炉4自动停止");
        }

        private void btnPLCO12_Click(object sender, EventArgs e)
        {
            if (ReadM(Stove5DoorFront))
            {
                WriteM(Stove5DoorTrip, false);
                btnPLCO12.BackColor = Color.Transparent;
            }
            if (ReadM(Stove5DoorQueen))
            {
                WriteM(Stove5DoorTrip, true);
                btnPLCO12.BackColor = Color.DarkGray;
            }
            LogHelper.Debug("手动操作：炉5开关");
        }

        private void btnPLCO13_Click(object sender, EventArgs e)
        {
            WriteM(Stove5AutoStart, true);
            LogHelper.Debug("手动操作：炉5自动运行");
        }

        private void btnPLCO14_Click(object sender, EventArgs e)
        {
            WriteM(Stove5AutoStop, true);
            LogHelper.Debug("手动操作：炉5自动停止");
        }

        private void btnPLCO15_Click(object sender, EventArgs e)
        { 
            if (ReadM(Stove6DoorFront))
            {
                WriteM(Stove6DoorTrip, false);
                btnPLCO15.BackColor = Color.Transparent;
            }
            if (ReadM(Stove6DoorQueen))
            {
                WriteM(Stove6DoorTrip, true);
                btnPLCO15.BackColor = Color.DarkGray;
            }
            LogHelper.Debug("手动操作：炉6开关");
        }

        private void btnPLCO16_Click(object sender, EventArgs e)
        {
            WriteM(Stove6AutoStart, true);
            LogHelper.Debug("手动操作：炉6自动运行");
        }

        private void btnPLCO17_Click(object sender, EventArgs e)
        {
            WriteM(Stove6AutoStop, true);
            LogHelper.Debug("手动操作：炉6自动停止");
        }

        #endregion

        private void Refresh_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool[] res = plc1.melsecPLCTcpUtl.read16Bit(192, "M");
        }
    }
}
