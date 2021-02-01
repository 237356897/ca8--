namespace Desay
{
    partial class FrmOperationReversal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnRobotConnect = new System.Windows.Forms.Button();
            this.btnRobotStart = new System.Windows.Forms.Button();
            this.btnRobotInit = new System.Windows.Forms.Button();
            this.btnRobotElectrify = new System.Windows.Forms.Button();
            this.btnRobotReset = new System.Windows.Forms.Button();
            this.btnRobotStop = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnChangeType = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SN = new System.Windows.Forms.TextBox();
            this.btnscan2 = new System.Windows.Forms.Button();
            this.btnscan = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSaveOrg = new System.Windows.Forms.Button();
            this.btnSaveCooling = new System.Windows.Forms.Button();
            this.btnGotoOrg = new System.Windows.Forms.Button();
            this.btnGotoCooling = new System.Windows.Forms.Button();
            this.labOrgPos = new System.Windows.Forms.Label();
            this.labCoolingPos = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnGotoMove = new System.Windows.Forms.Button();
            this.btnSaveMove = new System.Windows.Forms.Button();
            this.labMovePos = new System.Windows.Forms.Label();
            this.btnRightMove = new System.Windows.Forms.Button();
            this.btnLeftMove = new System.Windows.Forms.Button();
            this.grpLocationMoveSelect = new System.Windows.Forms.GroupBox();
            this.ndnPosOther = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.rbnPosOtherum = new System.Windows.Forms.RadioButton();
            this.rbnPos1000um = new System.Windows.Forms.RadioButton();
            this.rbnPos100um = new System.Windows.Forms.RadioButton();
            this.rbnPos10um = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbnLocationMoveSelect = new System.Windows.Forms.RadioButton();
            this.rbnContinueMoveSelect = new System.Windows.Forms.RadioButton();
            this.lblJogSpeed = new System.Windows.Forms.Label();
            this.tbrJogSpeed = new System.Windows.Forms.TrackBar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkCarryAxisIsServoOn = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCurrentPositionCarry = new System.Windows.Forms.Label();
            this.lblCurrentSpeedCarry = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnLonFan = new System.Windows.Forms.Button();
            this.btnReadyToAA = new System.Windows.Forms.Button();
            this.btnProductToAA = new System.Windows.Forms.Button();
            this.btnOpenJaw = new System.Windows.Forms.Button();
            this.btnCloseJaw = new System.Windows.Forms.Button();
            this.btnOpenCylinder = new System.Windows.Forms.Button();
            this.btnCloseCylinder = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.grpLocationMoveSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ndnPosOther)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbrJogSpeed)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRobotConnect
            // 
            this.btnRobotConnect.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRobotConnect.Location = new System.Drawing.Point(206, 244);
            this.btnRobotConnect.Name = "btnRobotConnect";
            this.btnRobotConnect.Size = new System.Drawing.Size(153, 35);
            this.btnRobotConnect.TabIndex = 6;
            this.btnRobotConnect.Text = "连接ABB";
            this.btnRobotConnect.UseVisualStyleBackColor = true;
            this.btnRobotConnect.Click += new System.EventHandler(this.btnRobotConnect_Click);
            // 
            // btnRobotStart
            // 
            this.btnRobotStart.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRobotStart.Location = new System.Drawing.Point(206, 200);
            this.btnRobotStart.Name = "btnRobotStart";
            this.btnRobotStart.Size = new System.Drawing.Size(153, 35);
            this.btnRobotStart.TabIndex = 7;
            this.btnRobotStart.Text = "启动ABB";
            this.btnRobotStart.UseVisualStyleBackColor = true;
            this.btnRobotStart.Click += new System.EventHandler(this.btnRobotStart_Click);
            // 
            // btnRobotInit
            // 
            this.btnRobotInit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRobotInit.Location = new System.Drawing.Point(206, 156);
            this.btnRobotInit.Name = "btnRobotInit";
            this.btnRobotInit.Size = new System.Drawing.Size(153, 35);
            this.btnRobotInit.TabIndex = 8;
            this.btnRobotInit.Text = "ABB程序复位";
            this.btnRobotInit.UseVisualStyleBackColor = true;
            this.btnRobotInit.Click += new System.EventHandler(this.btnRobotInit_Click);
            // 
            // btnRobotElectrify
            // 
            this.btnRobotElectrify.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRobotElectrify.Location = new System.Drawing.Point(206, 112);
            this.btnRobotElectrify.Name = "btnRobotElectrify";
            this.btnRobotElectrify.Size = new System.Drawing.Size(153, 35);
            this.btnRobotElectrify.TabIndex = 9;
            this.btnRobotElectrify.Text = "ABB电机上电";
            this.btnRobotElectrify.UseVisualStyleBackColor = true;
            this.btnRobotElectrify.Click += new System.EventHandler(this.btnRobotElectrify_Click);
            // 
            // btnRobotReset
            // 
            this.btnRobotReset.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRobotReset.Location = new System.Drawing.Point(206, 68);
            this.btnRobotReset.Name = "btnRobotReset";
            this.btnRobotReset.Size = new System.Drawing.Size(153, 35);
            this.btnRobotReset.TabIndex = 10;
            this.btnRobotReset.Text = "ABB急停复位";
            this.btnRobotReset.UseVisualStyleBackColor = true;
            this.btnRobotReset.Click += new System.EventHandler(this.btnRobotReset_Click);
            // 
            // btnRobotStop
            // 
            this.btnRobotStop.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRobotStop.Location = new System.Drawing.Point(206, 24);
            this.btnRobotStop.Name = "btnRobotStop";
            this.btnRobotStop.Size = new System.Drawing.Size(153, 35);
            this.btnRobotStop.TabIndex = 11;
            this.btnRobotStop.Text = "ABB停止";
            this.btnRobotStop.UseVisualStyleBackColor = true;
            this.btnRobotStop.Click += new System.EventHandler(this.btnRobotStop_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCloseCylinder);
            this.groupBox1.Controls.Add(this.btnOpenCylinder);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnRobotStop);
            this.groupBox1.Controls.Add(this.btnCloseJaw);
            this.groupBox1.Controls.Add(this.btnRobotInit);
            this.groupBox1.Controls.Add(this.btnOpenJaw);
            this.groupBox1.Controls.Add(this.btnRobotElectrify);
            this.groupBox1.Controls.Add(this.btnProductToAA);
            this.groupBox1.Controls.Add(this.btnChangeType);
            this.groupBox1.Controls.Add(this.btnReadyToAA);
            this.groupBox1.Controls.Add(this.btnRobotStart);
            this.groupBox1.Controls.Add(this.btnLonFan);
            this.groupBox1.Controls.Add(this.btnRobotReset);
            this.groupBox1.Controls.Add(this.btnRobotConnect);
            this.groupBox1.Location = new System.Drawing.Point(464, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(383, 514);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "机器人操作";
            // 
            // btnChangeType
            // 
            this.btnChangeType.Location = new System.Drawing.Point(206, 288);
            this.btnChangeType.Name = "btnChangeType";
            this.btnChangeType.Size = new System.Drawing.Size(153, 35);
            this.btnChangeType.TabIndex = 17;
            this.btnChangeType.Text = "ABB换型";
            this.btnChangeType.UseVisualStyleBackColor = true;
            this.btnChangeType.Click += new System.EventHandler(this.btnChangeType_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(206, 335);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(153, 35);
            this.button1.TabIndex = 16;
            this.button1.Text = "机器人一键复位";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // refreshTimer
            // 
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.SN);
            this.groupBox3.Controls.Add(this.btnscan2);
            this.groupBox3.Controls.Add(this.btnscan);
            this.groupBox3.Controls.Add(this.tableLayoutPanel2);
            this.groupBox3.Controls.Add(this.btnRightMove);
            this.groupBox3.Controls.Add(this.btnLeftMove);
            this.groupBox3.Controls.Add(this.grpLocationMoveSelect);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.lblJogSpeed);
            this.groupBox3.Controls.Add(this.tbrJogSpeed);
            this.groupBox3.Controls.Add(this.tableLayoutPanel1);
            this.groupBox3.Location = new System.Drawing.Point(12, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(434, 514);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "轴操作";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(28, 211);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "移动速度：";
            // 
            // SN
            // 
            this.SN.Location = new System.Drawing.Point(21, 462);
            this.SN.Name = "SN";
            this.SN.Size = new System.Drawing.Size(271, 21);
            this.SN.TabIndex = 14;
            // 
            // btnscan2
            // 
            this.btnscan2.Location = new System.Drawing.Point(310, 476);
            this.btnscan2.Name = "btnscan2";
            this.btnscan2.Size = new System.Drawing.Size(104, 23);
            this.btnscan2.TabIndex = 13;
            this.btnscan2.Text = "料盘扫码";
            this.btnscan2.UseVisualStyleBackColor = true;
            this.btnscan2.Click += new System.EventHandler(this.btnscan2_Click);
            // 
            // btnscan
            // 
            this.btnscan.Location = new System.Drawing.Point(310, 447);
            this.btnscan.Name = "btnscan";
            this.btnscan.Size = new System.Drawing.Size(104, 23);
            this.btnscan.TabIndex = 13;
            this.btnscan.Text = "产品扫码";
            this.btnscan.UseVisualStyleBackColor = true;
            this.btnscan.Click += new System.EventHandler(this.btnscan_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnSaveOrg, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSaveCooling, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnGotoOrg, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnGotoCooling, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.labOrgPos, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.labCoolingPos, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnGotoMove, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnSaveMove, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.labMovePos, 1, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(15, 284);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(403, 144);
            this.tableLayoutPanel2.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(4, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 46);
            this.label3.TabIndex = 0;
            this.label3.Text = "输送轴原点位";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(4, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 46);
            this.label4.TabIndex = 1;
            this.label4.Text = "降温位";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSaveOrg
            // 
            this.btnSaveOrg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSaveOrg.Location = new System.Drawing.Point(244, 4);
            this.btnSaveOrg.Name = "btnSaveOrg";
            this.btnSaveOrg.Size = new System.Drawing.Size(73, 40);
            this.btnSaveOrg.TabIndex = 2;
            this.btnSaveOrg.Text = "保存";
            this.btnSaveOrg.UseVisualStyleBackColor = true;
            this.btnSaveOrg.Click += new System.EventHandler(this.btnSaveOrg_Click);
            // 
            // btnSaveCooling
            // 
            this.btnSaveCooling.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSaveCooling.Location = new System.Drawing.Point(244, 51);
            this.btnSaveCooling.Name = "btnSaveCooling";
            this.btnSaveCooling.Size = new System.Drawing.Size(73, 40);
            this.btnSaveCooling.TabIndex = 2;
            this.btnSaveCooling.Text = "保存";
            this.btnSaveCooling.UseVisualStyleBackColor = true;
            this.btnSaveCooling.Click += new System.EventHandler(this.btnSaveCooling_Click);
            // 
            // btnGotoOrg
            // 
            this.btnGotoOrg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGotoOrg.Location = new System.Drawing.Point(324, 4);
            this.btnGotoOrg.Name = "btnGotoOrg";
            this.btnGotoOrg.Size = new System.Drawing.Size(75, 40);
            this.btnGotoOrg.TabIndex = 2;
            this.btnGotoOrg.Text = "定位";
            this.btnGotoOrg.UseVisualStyleBackColor = true;
            this.btnGotoOrg.Click += new System.EventHandler(this.btnGotoOrg_Click);
            // 
            // btnGotoCooling
            // 
            this.btnGotoCooling.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGotoCooling.Location = new System.Drawing.Point(324, 51);
            this.btnGotoCooling.Name = "btnGotoCooling";
            this.btnGotoCooling.Size = new System.Drawing.Size(75, 40);
            this.btnGotoCooling.TabIndex = 2;
            this.btnGotoCooling.Text = "定位";
            this.btnGotoCooling.UseVisualStyleBackColor = true;
            this.btnGotoCooling.Click += new System.EventHandler(this.btnGotoCooling_Click);
            // 
            // labOrgPos
            // 
            this.labOrgPos.AutoSize = true;
            this.labOrgPos.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labOrgPos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labOrgPos.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labOrgPos.ForeColor = System.Drawing.Color.SpringGreen;
            this.labOrgPos.Location = new System.Drawing.Point(124, 1);
            this.labOrgPos.Name = "labOrgPos";
            this.labOrgPos.Size = new System.Drawing.Size(113, 46);
            this.labOrgPos.TabIndex = 3;
            this.labOrgPos.Text = "0.000";
            this.labOrgPos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labCoolingPos
            // 
            this.labCoolingPos.AutoSize = true;
            this.labCoolingPos.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labCoolingPos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labCoolingPos.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labCoolingPos.ForeColor = System.Drawing.Color.SpringGreen;
            this.labCoolingPos.Location = new System.Drawing.Point(124, 48);
            this.labCoolingPos.Name = "labCoolingPos";
            this.labCoolingPos.Size = new System.Drawing.Size(113, 46);
            this.labCoolingPos.TabIndex = 4;
            this.labCoolingPos.Text = "0.000";
            this.labCoolingPos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(4, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 48);
            this.label6.TabIndex = 1;
            this.label6.Text = "输送轴动点位";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnGotoMove
            // 
            this.btnGotoMove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGotoMove.Location = new System.Drawing.Point(324, 98);
            this.btnGotoMove.Name = "btnGotoMove";
            this.btnGotoMove.Size = new System.Drawing.Size(75, 42);
            this.btnGotoMove.TabIndex = 2;
            this.btnGotoMove.Text = "定位";
            this.btnGotoMove.UseVisualStyleBackColor = true;
            this.btnGotoMove.Click += new System.EventHandler(this.btnGotoMove_Click);
            // 
            // btnSaveMove
            // 
            this.btnSaveMove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSaveMove.Location = new System.Drawing.Point(244, 98);
            this.btnSaveMove.Name = "btnSaveMove";
            this.btnSaveMove.Size = new System.Drawing.Size(73, 42);
            this.btnSaveMove.TabIndex = 2;
            this.btnSaveMove.Text = "保存";
            this.btnSaveMove.UseVisualStyleBackColor = true;
            this.btnSaveMove.Click += new System.EventHandler(this.btnSaveMove_Click);
            // 
            // labMovePos
            // 
            this.labMovePos.AutoSize = true;
            this.labMovePos.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labMovePos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labMovePos.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labMovePos.ForeColor = System.Drawing.Color.SpringGreen;
            this.labMovePos.Location = new System.Drawing.Point(124, 95);
            this.labMovePos.Name = "labMovePos";
            this.labMovePos.Size = new System.Drawing.Size(113, 48);
            this.labMovePos.TabIndex = 4;
            this.labMovePos.Text = "0.000";
            this.labMovePos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRightMove
            // 
            this.btnRightMove.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRightMove.Location = new System.Drawing.Point(310, 242);
            this.btnRightMove.Name = "btnRightMove";
            this.btnRightMove.Size = new System.Drawing.Size(108, 28);
            this.btnRightMove.TabIndex = 10;
            this.btnRightMove.Text = "左移";
            this.btnRightMove.UseVisualStyleBackColor = true;
            this.btnRightMove.Click += new System.EventHandler(this.btnRightMove_Click);
            this.btnRightMove.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnRightMove_MouseDown);
            this.btnRightMove.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnRightMove_MouseUp);
            // 
            // btnLeftMove
            // 
            this.btnLeftMove.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLeftMove.Location = new System.Drawing.Point(310, 214);
            this.btnLeftMove.Name = "btnLeftMove";
            this.btnLeftMove.Size = new System.Drawing.Size(108, 28);
            this.btnLeftMove.TabIndex = 11;
            this.btnLeftMove.Text = "右移";
            this.btnLeftMove.UseVisualStyleBackColor = true;
            this.btnLeftMove.Click += new System.EventHandler(this.btnLeftMove_Click);
            this.btnLeftMove.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLeftMove_MouseDown);
            this.btnLeftMove.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnLeftMove_MouseUp);
            // 
            // grpLocationMoveSelect
            // 
            this.grpLocationMoveSelect.Controls.Add(this.ndnPosOther);
            this.grpLocationMoveSelect.Controls.Add(this.label2);
            this.grpLocationMoveSelect.Controls.Add(this.rbnPosOtherum);
            this.grpLocationMoveSelect.Controls.Add(this.rbnPos1000um);
            this.grpLocationMoveSelect.Controls.Add(this.rbnPos100um);
            this.grpLocationMoveSelect.Controls.Add(this.rbnPos10um);
            this.grpLocationMoveSelect.Location = new System.Drawing.Point(162, 123);
            this.grpLocationMoveSelect.Name = "grpLocationMoveSelect";
            this.grpLocationMoveSelect.Size = new System.Drawing.Size(256, 69);
            this.grpLocationMoveSelect.TabIndex = 8;
            this.grpLocationMoveSelect.TabStop = false;
            this.grpLocationMoveSelect.Text = "定距选择";
            // 
            // ndnPosOther
            // 
            this.ndnPosOther.DecimalPlaces = 2;
            this.ndnPosOther.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ndnPosOther.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.ndnPosOther.Location = new System.Drawing.Point(157, 37);
            this.ndnPosOther.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.ndnPosOther.Name = "ndnPosOther";
            this.ndnPosOther.Size = new System.Drawing.Size(58, 21);
            this.ndnPosOther.TabIndex = 7;
            this.ndnPosOther.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ndnPosOther.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(176, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "mm";
            // 
            // rbnPosOtherum
            // 
            this.rbnPosOtherum.AutoSize = true;
            this.rbnPosOtherum.Location = new System.Drawing.Point(140, 41);
            this.rbnPosOtherum.Name = "rbnPosOtherum";
            this.rbnPosOtherum.Size = new System.Drawing.Size(14, 13);
            this.rbnPosOtherum.TabIndex = 0;
            this.rbnPosOtherum.UseVisualStyleBackColor = true;
            // 
            // rbnPos1000um
            // 
            this.rbnPos1000um.AutoSize = true;
            this.rbnPos1000um.Location = new System.Drawing.Point(140, 19);
            this.rbnPos1000um.Name = "rbnPos1000um";
            this.rbnPos1000um.Size = new System.Drawing.Size(59, 16);
            this.rbnPos1000um.TabIndex = 0;
            this.rbnPos1000um.Text = "1.00mm";
            this.rbnPos1000um.UseVisualStyleBackColor = true;
            // 
            // rbnPos100um
            // 
            this.rbnPos100um.AutoSize = true;
            this.rbnPos100um.Location = new System.Drawing.Point(40, 42);
            this.rbnPos100um.Name = "rbnPos100um";
            this.rbnPos100um.Size = new System.Drawing.Size(59, 16);
            this.rbnPos100um.TabIndex = 0;
            this.rbnPos100um.Text = "0.10mm";
            this.rbnPos100um.UseVisualStyleBackColor = true;
            // 
            // rbnPos10um
            // 
            this.rbnPos10um.AutoSize = true;
            this.rbnPos10um.Checked = true;
            this.rbnPos10um.Location = new System.Drawing.Point(40, 20);
            this.rbnPos10um.Name = "rbnPos10um";
            this.rbnPos10um.Size = new System.Drawing.Size(59, 16);
            this.rbnPos10um.TabIndex = 0;
            this.rbnPos10um.TabStop = true;
            this.rbnPos10um.Text = "0.01mm";
            this.rbnPos10um.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbnLocationMoveSelect);
            this.groupBox4.Controls.Add(this.rbnContinueMoveSelect);
            this.groupBox4.Location = new System.Drawing.Point(15, 123);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(141, 69);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "模式选择";
            // 
            // rbnLocationMoveSelect
            // 
            this.rbnLocationMoveSelect.AutoSize = true;
            this.rbnLocationMoveSelect.Location = new System.Drawing.Point(49, 42);
            this.rbnLocationMoveSelect.Name = "rbnLocationMoveSelect";
            this.rbnLocationMoveSelect.Size = new System.Drawing.Size(47, 16);
            this.rbnLocationMoveSelect.TabIndex = 0;
            this.rbnLocationMoveSelect.Text = "定距";
            this.rbnLocationMoveSelect.UseVisualStyleBackColor = true;
            // 
            // rbnContinueMoveSelect
            // 
            this.rbnContinueMoveSelect.AutoSize = true;
            this.rbnContinueMoveSelect.Checked = true;
            this.rbnContinueMoveSelect.Location = new System.Drawing.Point(49, 20);
            this.rbnContinueMoveSelect.Name = "rbnContinueMoveSelect";
            this.rbnContinueMoveSelect.Size = new System.Drawing.Size(47, 16);
            this.rbnContinueMoveSelect.TabIndex = 0;
            this.rbnContinueMoveSelect.TabStop = true;
            this.rbnContinueMoveSelect.Text = "连续";
            this.rbnContinueMoveSelect.UseVisualStyleBackColor = true;
            // 
            // lblJogSpeed
            // 
            this.lblJogSpeed.AutoSize = true;
            this.lblJogSpeed.Location = new System.Drawing.Point(102, 211);
            this.lblJogSpeed.Name = "lblJogSpeed";
            this.lblJogSpeed.Size = new System.Drawing.Size(41, 12);
            this.lblJogSpeed.TabIndex = 7;
            this.lblJogSpeed.Text = "10mm/s";
            // 
            // tbrJogSpeed
            // 
            this.tbrJogSpeed.AutoSize = false;
            this.tbrJogSpeed.Location = new System.Drawing.Point(22, 233);
            this.tbrJogSpeed.Maximum = 100;
            this.tbrJogSpeed.Name = "tbrJogSpeed";
            this.tbrJogSpeed.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbrJogSpeed.Size = new System.Drawing.Size(270, 23);
            this.tbrJogSpeed.TabIndex = 6;
            this.tbrJogSpeed.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbrJogSpeed.Value = 10;
            this.tbrJogSpeed.Scroll += new System.EventHandler(this.tbrJogSpeed_Scroll);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblCurrentPositionCarry, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblCurrentSpeedCarry, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label11, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label12, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label13, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(403, 90);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkCarryAxisIsServoOn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(284, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(115, 38);
            this.panel1.TabIndex = 5;
            // 
            // chkCarryAxisIsServoOn
            // 
            this.chkCarryAxisIsServoOn.AutoSize = true;
            this.chkCarryAxisIsServoOn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkCarryAxisIsServoOn.Location = new System.Drawing.Point(0, 0);
            this.chkCarryAxisIsServoOn.Name = "chkCarryAxisIsServoOn";
            this.chkCarryAxisIsServoOn.Size = new System.Drawing.Size(115, 38);
            this.chkCarryAxisIsServoOn.TabIndex = 5;
            this.chkCarryAxisIsServoOn.Text = "是否ON  ";
            this.chkCarryAxisIsServoOn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkCarryAxisIsServoOn.UseVisualStyleBackColor = true;
            this.chkCarryAxisIsServoOn.CheckedChanged += new System.EventHandler(this.chkCarryAxisIsServoOn_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(4, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 44);
            this.label1.TabIndex = 0;
            this.label1.Text = "输送轴";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentPositionCarry
            // 
            this.lblCurrentPositionCarry.AutoSize = true;
            this.lblCurrentPositionCarry.BackColor = System.Drawing.Color.Black;
            this.lblCurrentPositionCarry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentPositionCarry.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCurrentPositionCarry.ForeColor = System.Drawing.Color.SpringGreen;
            this.lblCurrentPositionCarry.Location = new System.Drawing.Point(84, 45);
            this.lblCurrentPositionCarry.Name = "lblCurrentPositionCarry";
            this.lblCurrentPositionCarry.Size = new System.Drawing.Size(93, 44);
            this.lblCurrentPositionCarry.TabIndex = 2;
            this.lblCurrentPositionCarry.Text = "000.000";
            this.lblCurrentPositionCarry.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentSpeedCarry
            // 
            this.lblCurrentSpeedCarry.AutoSize = true;
            this.lblCurrentSpeedCarry.BackColor = System.Drawing.Color.Black;
            this.lblCurrentSpeedCarry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentSpeedCarry.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCurrentSpeedCarry.ForeColor = System.Drawing.Color.SpringGreen;
            this.lblCurrentSpeedCarry.Location = new System.Drawing.Point(184, 45);
            this.lblCurrentSpeedCarry.Name = "lblCurrentSpeedCarry";
            this.lblCurrentSpeedCarry.Size = new System.Drawing.Size(93, 44);
            this.lblCurrentSpeedCarry.TabIndex = 4;
            this.lblCurrentSpeedCarry.Text = "000.000";
            this.lblCurrentSpeedCarry.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(84, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 43);
            this.label11.TabIndex = 7;
            this.label11.Text = "当前位置mm";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(184, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(93, 43);
            this.label12.TabIndex = 7;
            this.label12.Text = "当前速度mm/s";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(284, 1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(115, 43);
            this.label13.TabIndex = 7;
            this.label13.Text = "步进状态";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(4, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 43);
            this.label5.TabIndex = 8;
            this.label5.Text = "轴名";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLonFan
            // 
            this.btnLonFan.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLonFan.Location = new System.Drawing.Point(22, 200);
            this.btnLonFan.Name = "btnLonFan";
            this.btnLonFan.Size = new System.Drawing.Size(153, 35);
            this.btnLonFan.TabIndex = 12;
            this.btnLonFan.Text = "等离子风扇开";
            this.btnLonFan.UseVisualStyleBackColor = true;
            this.btnLonFan.Click += new System.EventHandler(this.btnLonFan_Click);
            // 
            // btnReadyToAA
            // 
            this.btnReadyToAA.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReadyToAA.Location = new System.Drawing.Point(22, 244);
            this.btnReadyToAA.Name = "btnReadyToAA";
            this.btnReadyToAA.Size = new System.Drawing.Size(153, 35);
            this.btnReadyToAA.TabIndex = 12;
            this.btnReadyToAA.Text = "取料完成信号-OFF";
            this.btnReadyToAA.UseVisualStyleBackColor = true;
            this.btnReadyToAA.Click += new System.EventHandler(this.btnReadyToAA_Click);
            // 
            // btnProductToAA
            // 
            this.btnProductToAA.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnProductToAA.Location = new System.Drawing.Point(22, 288);
            this.btnProductToAA.Name = "btnProductToAA";
            this.btnProductToAA.Size = new System.Drawing.Size(153, 35);
            this.btnProductToAA.TabIndex = 12;
            this.btnProductToAA.Text = "AA开夹信号-OFF";
            this.btnProductToAA.UseVisualStyleBackColor = true;
            this.btnProductToAA.Click += new System.EventHandler(this.btnProductToAA_Click);
            // 
            // btnOpenJaw
            // 
            this.btnOpenJaw.Location = new System.Drawing.Point(22, 24);
            this.btnOpenJaw.Name = "btnOpenJaw";
            this.btnOpenJaw.Size = new System.Drawing.Size(153, 35);
            this.btnOpenJaw.TabIndex = 18;
            this.btnOpenJaw.Text = "打开产品夹爪";
            this.btnOpenJaw.UseVisualStyleBackColor = true;
            this.btnOpenJaw.Click += new System.EventHandler(this.btnOpenJaw_Click);
            // 
            // btnCloseJaw
            // 
            this.btnCloseJaw.Location = new System.Drawing.Point(22, 68);
            this.btnCloseJaw.Name = "btnCloseJaw";
            this.btnCloseJaw.Size = new System.Drawing.Size(153, 35);
            this.btnCloseJaw.TabIndex = 19;
            this.btnCloseJaw.Text = "关闭产品夹爪";
            this.btnCloseJaw.UseVisualStyleBackColor = true;
            this.btnCloseJaw.Click += new System.EventHandler(this.btnCloseJaw_Click);
            // 
            // btnOpenCylinder
            // 
            this.btnOpenCylinder.Location = new System.Drawing.Point(22, 112);
            this.btnOpenCylinder.Name = "btnOpenCylinder";
            this.btnOpenCylinder.Size = new System.Drawing.Size(153, 35);
            this.btnOpenCylinder.TabIndex = 20;
            this.btnOpenCylinder.Text = "打开料盘夹爪";
            this.btnOpenCylinder.UseVisualStyleBackColor = true;
            this.btnOpenCylinder.Click += new System.EventHandler(this.btnOpenCylinder_Click);
            // 
            // btnCloseCylinder
            // 
            this.btnCloseCylinder.Location = new System.Drawing.Point(22, 156);
            this.btnCloseCylinder.Name = "btnCloseCylinder";
            this.btnCloseCylinder.Size = new System.Drawing.Size(153, 35);
            this.btnCloseCylinder.TabIndex = 21;
            this.btnCloseCylinder.Text = "关闭料盘夹爪";
            this.btnCloseCylinder.UseVisualStyleBackColor = true;
            this.btnCloseCylinder.Click += new System.EventHandler(this.btnCloseCylinder_Click);
            // 
            // FrmOperationReversal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 522);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmOperationReversal";
            this.Text = "设备操作";
            this.Load += new System.EventHandler(this.FrmOperation_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.grpLocationMoveSelect.ResumeLayout(false);
            this.grpLocationMoveSelect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ndnPosOther)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbrJogSpeed)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRobotConnect;
        private System.Windows.Forms.Button btnRobotStart;
        private System.Windows.Forms.Button btnRobotInit;
        private System.Windows.Forms.Button btnRobotElectrify;
        private System.Windows.Forms.Button btnRobotReset;
        private System.Windows.Forms.Button btnRobotStop;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkCarryAxisIsServoOn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCurrentPositionCarry;
        private System.Windows.Forms.Label lblCurrentSpeedCarry;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblJogSpeed;
        private System.Windows.Forms.TrackBar tbrJogSpeed;
        private System.Windows.Forms.GroupBox grpLocationMoveSelect;
        private System.Windows.Forms.NumericUpDown ndnPosOther;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbnPosOtherum;
        private System.Windows.Forms.RadioButton rbnPos1000um;
        private System.Windows.Forms.RadioButton rbnPos100um;
        private System.Windows.Forms.RadioButton rbnPos10um;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbnLocationMoveSelect;
        private System.Windows.Forms.RadioButton rbnContinueMoveSelect;
        private System.Windows.Forms.Button btnRightMove;
        private System.Windows.Forms.Button btnLeftMove;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSaveOrg;
        private System.Windows.Forms.Button btnSaveCooling;
        private System.Windows.Forms.Button btnGotoOrg;
        private System.Windows.Forms.Button btnGotoCooling;
        private System.Windows.Forms.Label labOrgPos;
        private System.Windows.Forms.Label labCoolingPos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnGotoMove;
        private System.Windows.Forms.Button btnSaveMove;
        private System.Windows.Forms.Label labMovePos;
        private System.Windows.Forms.Button btnLonFan;
        private System.Windows.Forms.Button btnReadyToAA;
        private System.Windows.Forms.Button btnProductToAA;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnscan;
        private System.Windows.Forms.TextBox SN;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnscan2;
        private System.Windows.Forms.Button btnChangeType;
        private System.Windows.Forms.Button btnCloseCylinder;
        private System.Windows.Forms.Button btnOpenCylinder;
        private System.Windows.Forms.Button btnCloseJaw;
        private System.Windows.Forms.Button btnOpenJaw;
    }
}