namespace Desay
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnMain = new System.Windows.Forms.ToolStripButton();
            this.btnIOMonitor = new System.Windows.Forms.ToolStripButton();
            this.btnPLCMonitor = new System.Windows.Forms.ToolStripButton();
            this.btnRecipe = new System.Windows.Forms.ToolStripButton();
            this.btnOperation = new System.Windows.Forms.ToolStripButton();
            this.btnQRCodeSet = new System.Windows.Forms.ToolStripButton();
            this.btnTrayCodeSet = new System.Windows.Forms.ToolStripButton();
            this.btnMes = new System.Windows.Forms.ToolStripButton();
            this.btnLogin = new System.Windows.Forms.ToolStripButton();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.IabPlcCom = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.labQRCodeCom = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.labRobot = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.labTrayCodeCom = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tstrobotstatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tstrobotstep = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.refreshStateTimer = new System.Windows.Forms.Timer(this.components);
            this.dgvTaryResult = new System.Windows.Forms.DataGridView();
            this.HoleNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QRCodeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbcMain = new System.Windows.Forms.TabControl();
            this.tbgMain = new System.Windows.Forms.TabPage();
            this.tbOven6Temperature = new System.Windows.Forms.TextBox();
            this.cb_down = new System.Windows.Forms.CheckBox();
            this.tbOven5Temperature = new System.Windows.Forms.TextBox();
            this.tbOven4Temperature = new System.Windows.Forms.TextBox();
            this.tbOven3Temperature = new System.Windows.Forms.TextBox();
            this.tbOven2Temperature = new System.Windows.Forms.TextBox();
            this.labOven1Start = new System.Windows.Forms.Label();
            this.labOven2Start = new System.Windows.Forms.Label();
            this.labOven3Start = new System.Windows.Forms.Label();
            this.labOven4Start = new System.Windows.Forms.Label();
            this.labOven5Start = new System.Windows.Forms.Label();
            this.labOven6Start = new System.Windows.Forms.Label();
            this.tbOven1Temperature = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dgvStoveResult = new System.Windows.Forms.DataGridView();
            this.StoveNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StoveHoleNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StoveQRCodeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StoveTemperature = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.temperatureChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tbgPLCMonitor = new System.Windows.Forms.TabPage();
            this.tbgIOMonitor = new System.Windows.Forms.TabPage();
            this.tbgRecipe = new System.Windows.Forms.TabPage();
            this.tbgOperation = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStationName = new System.Windows.Forms.Label();
            this.btnAlarmClean = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblProductType = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnRunSetting = new System.Windows.Forms.Button();
            this.btnTricolorLamp = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.lblMachineStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblOK = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblNG = new System.Windows.Forms.Label();
            this.lblSum = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnClearCount = new System.Windows.Forms.Button();
            this.btnIllumination = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnReset = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstInfo = new System.Windows.Forms.ListBox();
            this.rbHand = new System.Windows.Forms.RadioButton();
            this.rbAuto = new System.Windows.Forms.RadioButton();
            this.refreshDatatimer = new System.Windows.Forms.Timer(this.components);
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cb_doorok = new System.Windows.Forms.CheckBox();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaryResult)).BeginInit();
            this.tbcMain.SuspendLayout();
            this.tbgMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStoveResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.temperatureChart)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMain,
            this.btnIOMonitor,
            this.btnPLCMonitor,
            this.btnRecipe,
            this.btnOperation,
            this.btnQRCodeSet,
            this.btnTrayCodeSet,
            this.btnMes,
            this.btnLogin,
            this.btnExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1227, 83);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnMain
            // 
            this.btnMain.Image = global::_0R02.Properties.Resources.ic_action_home;
            this.btnMain.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnMain.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMain.Name = "btnMain";
            this.btnMain.Size = new System.Drawing.Size(68, 80);
            this.btnMain.Text = "主界面";
            this.btnMain.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMain.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnMain.Click += new System.EventHandler(this.btnMain_Click);
            // 
            // btnIOMonitor
            // 
            this.btnIOMonitor.Image = global::_0R02.Properties.Resources.ic_action_settings;
            this.btnIOMonitor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnIOMonitor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIOMonitor.Name = "btnIOMonitor";
            this.btnIOMonitor.Size = new System.Drawing.Size(68, 80);
            this.btnIOMonitor.Text = "IO监控";
            this.btnIOMonitor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnIOMonitor.Click += new System.EventHandler(this.btnIOMonitor_Click);
            // 
            // btnPLCMonitor
            // 
            this.btnPLCMonitor.Image = global::_0R02.Properties.Resources.ic_action_settings1;
            this.btnPLCMonitor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnPLCMonitor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPLCMonitor.Name = "btnPLCMonitor";
            this.btnPLCMonitor.Size = new System.Drawing.Size(68, 80);
            this.btnPLCMonitor.Text = "PLC监控";
            this.btnPLCMonitor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPLCMonitor.Click += new System.EventHandler(this.btnPLCMonitor_Click);
            // 
            // btnRecipe
            // 
            this.btnRecipe.Image = global::_0R02.Properties.Resources.ic_action_mes;
            this.btnRecipe.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnRecipe.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRecipe.Name = "btnRecipe";
            this.btnRecipe.Size = new System.Drawing.Size(68, 80);
            this.btnRecipe.Text = "型号选择";
            this.btnRecipe.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRecipe.Click += new System.EventHandler(this.btnRecipe_Click);
            // 
            // btnOperation
            // 
            this.btnOperation.Image = global::_0R02.Properties.Resources.ic_action_gear;
            this.btnOperation.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnOperation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOperation.Name = "btnOperation";
            this.btnOperation.Size = new System.Drawing.Size(68, 80);
            this.btnOperation.Text = "设备操作";
            this.btnOperation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOperation.Click += new System.EventHandler(this.btnOperation_Click);
            // 
            // btnQRCodeSet
            // 
            this.btnQRCodeSet.Image = global::_0R02.Properties.Resources.ic_action_barcode_2;
            this.btnQRCodeSet.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnQRCodeSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnQRCodeSet.Name = "btnQRCodeSet";
            this.btnQRCodeSet.Size = new System.Drawing.Size(68, 80);
            this.btnQRCodeSet.Text = "产品扫码";
            this.btnQRCodeSet.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnQRCodeSet.Click += new System.EventHandler(this.btnQRCodeCom_Click);
            // 
            // btnTrayCodeSet
            // 
            this.btnTrayCodeSet.Image = global::_0R02.Properties.Resources.ic_action_barcode_2;
            this.btnTrayCodeSet.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnTrayCodeSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTrayCodeSet.Name = "btnTrayCodeSet";
            this.btnTrayCodeSet.Size = new System.Drawing.Size(68, 80);
            this.btnTrayCodeSet.Text = "料盘扫描";
            this.btnTrayCodeSet.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnTrayCodeSet.Click += new System.EventHandler(this.btnTrayCodeSet_Click);
            // 
            // btnMes
            // 
            this.btnMes.Image = global::_0R02.Properties.Resources.ic_action_gmail;
            this.btnMes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnMes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMes.Name = "btnMes";
            this.btnMes.Size = new System.Drawing.Size(68, 80);
            this.btnMes.Text = "MES";
            this.btnMes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnMes.Click += new System.EventHandler(this.btnMes_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Image = global::_0R02.Properties.Resources.ic_action_users;
            this.btnLogin.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnLogin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(68, 80);
            this.btnLogin.Text = "登入";
            this.btnLogin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnExit
            // 
            this.btnExit.Image = global::_0R02.Properties.Resources.ic_action_exit;
            this.btnExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(68, 80);
            this.btnExit.Text = "退出";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel3,
            this.IabPlcCom,
            this.toolStripStatusLabel5,
            this.labQRCodeCom,
            this.toolStripStatusLabel1,
            this.labRobot,
            this.toolStripStatusLabel2,
            this.labTrayCodeCom,
            this.toolStripStatusLabel7,
            this.toolStripStatusLabel4,
            this.tstrobotstatus,
            this.toolStripStatusLabel6,
            this.tstrobotstep});
            this.statusStrip1.Location = new System.Drawing.Point(0, 743);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1227, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(41, 17);
            this.toolStripStatusLabel3.Text = "PLC：";
            // 
            // IabPlcCom
            // 
            this.IabPlcCom.BackColor = System.Drawing.Color.Red;
            this.IabPlcCom.ForeColor = System.Drawing.Color.Green;
            this.IabPlcCom.Name = "IabPlcCom";
            this.IabPlcCom.Size = new System.Drawing.Size(32, 17);
            this.IabPlcCom.Text = "      ";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(68, 17);
            this.toolStripStatusLabel5.Text = "产品扫码：";
            // 
            // labQRCodeCom
            // 
            this.labQRCodeCom.BackColor = System.Drawing.Color.Red;
            this.labQRCodeCom.ForeColor = System.Drawing.Color.Green;
            this.labQRCodeCom.Name = "labQRCodeCom";
            this.labQRCodeCom.Size = new System.Drawing.Size(32, 17);
            this.labQRCodeCom.Text = "      ";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(47, 17);
            this.toolStripStatusLabel1.Text = "机器人:";
            // 
            // labRobot
            // 
            this.labRobot.BackColor = System.Drawing.Color.Red;
            this.labRobot.Name = "labRobot";
            this.labRobot.Size = new System.Drawing.Size(32, 17);
            this.labRobot.Text = "      ";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(68, 17);
            this.toolStripStatusLabel2.Text = "料盘扫码：";
            // 
            // labTrayCodeCom
            // 
            this.labTrayCodeCom.BackColor = System.Drawing.Color.Red;
            this.labTrayCodeCom.Name = "labTrayCodeCom";
            this.labTrayCodeCom.Size = new System.Drawing.Size(36, 17);
            this.labTrayCodeCom.Text = "       ";
            // 
            // toolStripStatusLabel7
            // 
            this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            this.toolStripStatusLabel7.Size = new System.Drawing.Size(40, 17);
            this.toolStripStatusLabel7.Text = "        ";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(68, 17);
            this.toolStripStatusLabel4.Text = "输送状态：";
            // 
            // tstrobotstatus
            // 
            this.tstrobotstatus.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tstrobotstatus.Name = "tstrobotstatus";
            this.tstrobotstatus.Size = new System.Drawing.Size(43, 17);
            this.tstrobotstatus.Text = "Status";
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(80, 17);
            this.toolStripStatusLabel6.Text = "机械手状态：";
            // 
            // tstrobotstep
            // 
            this.tstrobotstep.ActiveLinkColor = System.Drawing.Color.Red;
            this.tstrobotstep.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tstrobotstep.Name = "tstrobotstep";
            this.tstrobotstep.Size = new System.Drawing.Size(33, 17);
            this.tstrobotstep.Text = "step";
            // 
            // txtMessage
            // 
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessage.Location = new System.Drawing.Point(3, 17);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(588, 92);
            this.txtMessage.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtMessage);
            this.groupBox4.Location = new System.Drawing.Point(614, 613);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(594, 112);
            this.groupBox4.TabIndex = 76;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "运行日志";
            // 
            // refreshStateTimer
            // 
            this.refreshStateTimer.Interval = 1500;
            this.refreshStateTimer.Tick += new System.EventHandler(this.refreshStateTimer_Tick);
            // 
            // dgvTaryResult
            // 
            this.dgvTaryResult.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTaryResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTaryResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTaryResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HoleNo,
            this.QRCodeName});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTaryResult.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTaryResult.Location = new System.Drawing.Point(6, 5);
            this.dgvTaryResult.Margin = new System.Windows.Forms.Padding(0);
            this.dgvTaryResult.Name = "dgvTaryResult";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTaryResult.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTaryResult.RowHeadersVisible = false;
            this.dgvTaryResult.RowTemplate.Height = 23;
            this.dgvTaryResult.Size = new System.Drawing.Size(352, 234);
            this.dgvTaryResult.TabIndex = 78;
            // 
            // HoleNo
            // 
            this.HoleNo.HeaderText = "穴号";
            this.HoleNo.Name = "HoleNo";
            // 
            // QRCodeName
            // 
            this.QRCodeName.HeaderText = "二维码";
            this.QRCodeName.Name = "QRCodeName";
            this.QRCodeName.Width = 230;
            // 
            // tbcMain
            // 
            this.tbcMain.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tbcMain.Controls.Add(this.tbgMain);
            this.tbcMain.Controls.Add(this.tbgPLCMonitor);
            this.tbcMain.Controls.Add(this.tbgIOMonitor);
            this.tbcMain.Controls.Add(this.tbgRecipe);
            this.tbcMain.Controls.Add(this.tbgOperation);
            this.tbcMain.ItemSize = new System.Drawing.Size(0, 1);
            this.tbcMain.Location = new System.Drawing.Point(280, 81);
            this.tbcMain.Name = "tbcMain";
            this.tbcMain.SelectedIndex = 0;
            this.tbcMain.Size = new System.Drawing.Size(932, 526);
            this.tbcMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tbcMain.TabIndex = 81;
            // 
            // tbgMain
            // 
            this.tbgMain.Controls.Add(this.tbOven6Temperature);
            this.tbgMain.Controls.Add(this.cb_down);
            this.tbgMain.Controls.Add(this.tbOven5Temperature);
            this.tbgMain.Controls.Add(this.tbOven4Temperature);
            this.tbgMain.Controls.Add(this.tbOven3Temperature);
            this.tbgMain.Controls.Add(this.tbOven2Temperature);
            this.tbgMain.Controls.Add(this.labOven1Start);
            this.tbgMain.Controls.Add(this.labOven2Start);
            this.tbgMain.Controls.Add(this.labOven3Start);
            this.tbgMain.Controls.Add(this.labOven4Start);
            this.tbgMain.Controls.Add(this.labOven5Start);
            this.tbgMain.Controls.Add(this.labOven6Start);
            this.tbgMain.Controls.Add(this.tbOven1Temperature);
            this.tbgMain.Controls.Add(this.label8);
            this.tbgMain.Controls.Add(this.dgvStoveResult);
            this.tbgMain.Controls.Add(this.dgvTaryResult);
            this.tbgMain.Controls.Add(this.temperatureChart);
            this.tbgMain.Location = new System.Drawing.Point(4, 5);
            this.tbgMain.Name = "tbgMain";
            this.tbgMain.Padding = new System.Windows.Forms.Padding(3);
            this.tbgMain.Size = new System.Drawing.Size(924, 517);
            this.tbgMain.TabIndex = 0;
            this.tbgMain.Text = "主界面";
            this.tbgMain.UseVisualStyleBackColor = true;
            // 
            // tbOven6Temperature
            // 
            this.tbOven6Temperature.Enabled = false;
            this.tbOven6Temperature.Location = new System.Drawing.Point(785, 480);
            this.tbOven6Temperature.Name = "tbOven6Temperature";
            this.tbOven6Temperature.Size = new System.Drawing.Size(52, 21);
            this.tbOven6Temperature.TabIndex = 80;
            this.tbOven6Temperature.Text = "0.000";
            this.tbOven6Temperature.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cb_down
            // 
            this.cb_down.AutoSize = true;
            this.cb_down.Location = new System.Drawing.Point(870, 251);
            this.cb_down.Name = "cb_down";
            this.cb_down.Size = new System.Drawing.Size(48, 16);
            this.cb_down.TabIndex = 83;
            this.cb_down.Text = "下炉";
            this.cb_down.UseVisualStyleBackColor = true;
            // 
            // tbOven5Temperature
            // 
            this.tbOven5Temperature.Enabled = false;
            this.tbOven5Temperature.Location = new System.Drawing.Point(785, 439);
            this.tbOven5Temperature.Name = "tbOven5Temperature";
            this.tbOven5Temperature.Size = new System.Drawing.Size(52, 21);
            this.tbOven5Temperature.TabIndex = 80;
            this.tbOven5Temperature.Text = "0.000";
            this.tbOven5Temperature.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbOven4Temperature
            // 
            this.tbOven4Temperature.Enabled = false;
            this.tbOven4Temperature.Location = new System.Drawing.Point(785, 398);
            this.tbOven4Temperature.Name = "tbOven4Temperature";
            this.tbOven4Temperature.Size = new System.Drawing.Size(52, 21);
            this.tbOven4Temperature.TabIndex = 80;
            this.tbOven4Temperature.Text = "0.000";
            this.tbOven4Temperature.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbOven3Temperature
            // 
            this.tbOven3Temperature.Enabled = false;
            this.tbOven3Temperature.Location = new System.Drawing.Point(785, 357);
            this.tbOven3Temperature.Name = "tbOven3Temperature";
            this.tbOven3Temperature.Size = new System.Drawing.Size(52, 21);
            this.tbOven3Temperature.TabIndex = 80;
            this.tbOven3Temperature.Text = "0.000";
            this.tbOven3Temperature.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbOven2Temperature
            // 
            this.tbOven2Temperature.Enabled = false;
            this.tbOven2Temperature.Location = new System.Drawing.Point(785, 316);
            this.tbOven2Temperature.Name = "tbOven2Temperature";
            this.tbOven2Temperature.Size = new System.Drawing.Size(52, 21);
            this.tbOven2Temperature.TabIndex = 80;
            this.tbOven2Temperature.Text = "0.000";
            this.tbOven2Temperature.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labOven1Start
            // 
            this.labOven1Start.AutoSize = true;
            this.labOven1Start.BackColor = System.Drawing.Color.Red;
            this.labOven1Start.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labOven1Start.Location = new System.Drawing.Point(876, 275);
            this.labOven1Start.Name = "labOven1Start";
            this.labOven1Start.Size = new System.Drawing.Size(42, 19);
            this.labOven1Start.TabIndex = 82;
            this.labOven1Start.Text = " 1 ";
            // 
            // labOven2Start
            // 
            this.labOven2Start.AutoSize = true;
            this.labOven2Start.BackColor = System.Drawing.Color.Red;
            this.labOven2Start.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labOven2Start.Location = new System.Drawing.Point(876, 316);
            this.labOven2Start.Name = "labOven2Start";
            this.labOven2Start.Size = new System.Drawing.Size(42, 19);
            this.labOven2Start.TabIndex = 82;
            this.labOven2Start.Text = " 2 ";
            // 
            // labOven3Start
            // 
            this.labOven3Start.AutoSize = true;
            this.labOven3Start.BackColor = System.Drawing.Color.Red;
            this.labOven3Start.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labOven3Start.Location = new System.Drawing.Point(876, 357);
            this.labOven3Start.Name = "labOven3Start";
            this.labOven3Start.Size = new System.Drawing.Size(42, 19);
            this.labOven3Start.TabIndex = 82;
            this.labOven3Start.Text = " 3 ";
            // 
            // labOven4Start
            // 
            this.labOven4Start.AutoSize = true;
            this.labOven4Start.BackColor = System.Drawing.Color.Red;
            this.labOven4Start.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labOven4Start.Location = new System.Drawing.Point(876, 399);
            this.labOven4Start.Name = "labOven4Start";
            this.labOven4Start.Size = new System.Drawing.Size(42, 19);
            this.labOven4Start.TabIndex = 82;
            this.labOven4Start.Text = " 4 ";
            // 
            // labOven5Start
            // 
            this.labOven5Start.AutoSize = true;
            this.labOven5Start.BackColor = System.Drawing.Color.Red;
            this.labOven5Start.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labOven5Start.Location = new System.Drawing.Point(876, 440);
            this.labOven5Start.Name = "labOven5Start";
            this.labOven5Start.Size = new System.Drawing.Size(42, 19);
            this.labOven5Start.TabIndex = 82;
            this.labOven5Start.Text = " 5 ";
            // 
            // labOven6Start
            // 
            this.labOven6Start.AutoSize = true;
            this.labOven6Start.BackColor = System.Drawing.Color.Red;
            this.labOven6Start.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labOven6Start.Location = new System.Drawing.Point(876, 481);
            this.labOven6Start.Name = "labOven6Start";
            this.labOven6Start.Size = new System.Drawing.Size(42, 19);
            this.labOven6Start.TabIndex = 82;
            this.labOven6Start.Text = " 6 ";
            // 
            // tbOven1Temperature
            // 
            this.tbOven1Temperature.Enabled = false;
            this.tbOven1Temperature.Location = new System.Drawing.Point(785, 275);
            this.tbOven1Temperature.Name = "tbOven1Temperature";
            this.tbOven1Temperature.Size = new System.Drawing.Size(52, 21);
            this.tbOven1Temperature.TabIndex = 80;
            this.tbOven1Temperature.Text = "0.000";
            this.tbOven1Temperature.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(789, 251);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 79;
            this.label8.Text = "炉温度";
            // 
            // dgvStoveResult
            // 
            this.dgvStoveResult.AllowUserToAddRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStoveResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvStoveResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStoveResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StoveNo,
            this.StoveHoleNo,
            this.StoveQRCodeName,
            this.StoveTemperature});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStoveResult.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvStoveResult.Location = new System.Drawing.Point(368, 5);
            this.dgvStoveResult.Margin = new System.Windows.Forms.Padding(0);
            this.dgvStoveResult.Name = "dgvStoveResult";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStoveResult.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvStoveResult.RowHeadersVisible = false;
            this.dgvStoveResult.RowTemplate.Height = 23;
            this.dgvStoveResult.Size = new System.Drawing.Size(551, 234);
            this.dgvStoveResult.TabIndex = 78;
            // 
            // StoveNo
            // 
            this.StoveNo.HeaderText = "炉号";
            this.StoveNo.Name = "StoveNo";
            // 
            // StoveHoleNo
            // 
            this.StoveHoleNo.HeaderText = "穴号";
            this.StoveHoleNo.Name = "StoveHoleNo";
            // 
            // StoveQRCodeName
            // 
            this.StoveQRCodeName.HeaderText = "二维码";
            this.StoveQRCodeName.Name = "StoveQRCodeName";
            this.StoveQRCodeName.Width = 230;
            // 
            // StoveTemperature
            // 
            this.StoveTemperature.HeaderText = "温度";
            this.StoveTemperature.Name = "StoveTemperature";
            // 
            // temperatureChart
            // 
            chartArea1.AxisX.LineColor = System.Drawing.Color.Blue;
            chartArea1.AxisX.MajorTickMark.LineWidth = 0;
            chartArea1.AxisX.Maximum = 18000D;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.MinorGrid.LineWidth = 0;
            chartArea1.AxisX.MinorTickMark.LineWidth = 0;
            chartArea1.AxisX.ScaleView.SmallScrollMinSize = 2D;
            chartArea1.AxisX.ScrollBar.Size = 10D;
            chartArea1.AxisX.Title = "采集次数";
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.Red;
            chartArea1.AxisY.LineColor = System.Drawing.Color.Blue;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisY.Maximum = 100D;
            chartArea1.AxisY.Minimum = 60D;
            chartArea1.AxisY.Title = "温度";
            chartArea1.AxisY.TitleForeColor = System.Drawing.Color.Red;
            chartArea1.BorderColor = System.Drawing.Color.Transparent;
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.Name = "ChartAreaStove";
            chartArea1.ShadowColor = System.Drawing.Color.White;
            this.temperatureChart.ChartAreas.Add(chartArea1);
            legend1.ItemColumnSpacing = 100;
            legend1.Name = "Legend1";
            this.temperatureChart.Legends.Add(legend1);
            this.temperatureChart.Location = new System.Drawing.Point(9, 249);
            this.temperatureChart.Name = "temperatureChart";
            series1.ChartArea = "ChartAreaStove";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "stove1";
            series2.ChartArea = "ChartAreaStove";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "stove2";
            series3.ChartArea = "ChartAreaStove";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Legend = "Legend1";
            series3.Name = "stove3";
            series4.ChartArea = "ChartAreaStove";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series4.Legend = "Legend1";
            series4.Name = "stove4";
            series5.ChartArea = "ChartAreaStove";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series5.Legend = "Legend1";
            series5.Name = "stove5";
            series6.ChartArea = "ChartAreaStove";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series6.Legend = "Legend1";
            series6.Name = "stove6";
            this.temperatureChart.Series.Add(series1);
            this.temperatureChart.Series.Add(series2);
            this.temperatureChart.Series.Add(series3);
            this.temperatureChart.Series.Add(series4);
            this.temperatureChart.Series.Add(series5);
            this.temperatureChart.Series.Add(series6);
            this.temperatureChart.Size = new System.Drawing.Size(764, 262);
            this.temperatureChart.TabIndex = 77;
            this.temperatureChart.Text = "温度";
            title1.Name = "temperatureTitle";
            title1.Text = "温度波形图";
            this.temperatureChart.Titles.Add(title1);
            // 
            // tbgPLCMonitor
            // 
            this.tbgPLCMonitor.Location = new System.Drawing.Point(4, 5);
            this.tbgPLCMonitor.Name = "tbgPLCMonitor";
            this.tbgPLCMonitor.Size = new System.Drawing.Size(924, 517);
            this.tbgPLCMonitor.TabIndex = 3;
            this.tbgPLCMonitor.Text = "电流监控";
            this.tbgPLCMonitor.UseVisualStyleBackColor = true;
            // 
            // tbgIOMonitor
            // 
            this.tbgIOMonitor.BackColor = System.Drawing.Color.Transparent;
            this.tbgIOMonitor.Location = new System.Drawing.Point(4, 5);
            this.tbgIOMonitor.Name = "tbgIOMonitor";
            this.tbgIOMonitor.Padding = new System.Windows.Forms.Padding(3);
            this.tbgIOMonitor.Size = new System.Drawing.Size(924, 517);
            this.tbgIOMonitor.TabIndex = 1;
            this.tbgIOMonitor.Text = "型号选择";
            // 
            // tbgRecipe
            // 
            this.tbgRecipe.Location = new System.Drawing.Point(4, 5);
            this.tbgRecipe.Name = "tbgRecipe";
            this.tbgRecipe.Size = new System.Drawing.Size(924, 517);
            this.tbgRecipe.TabIndex = 4;
            this.tbgRecipe.Text = "烤箱设置";
            this.tbgRecipe.UseVisualStyleBackColor = true;
            // 
            // tbgOperation
            // 
            this.tbgOperation.Location = new System.Drawing.Point(4, 5);
            this.tbgOperation.Name = "tbgOperation";
            this.tbgOperation.Size = new System.Drawing.Size(924, 517);
            this.tbgOperation.TabIndex = 5;
            this.tbgOperation.Text = "tbgOperation";
            this.tbgOperation.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel3);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(3, 89);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(271, 373);
            this.groupBox2.TabIndex = 81;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "产品检测信息";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblStationName, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnAlarmClean, 1, 11);
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblProductType, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.panel6, 0, 10);
            this.tableLayoutPanel3.Controls.Add(this.button5, 0, 11);
            this.tableLayoutPanel3.Controls.Add(this.lblMachineStatus, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.lblOK, 1, 5);
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 6);
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 7);
            this.tableLayoutPanel3.Controls.Add(this.lblNG, 1, 6);
            this.tableLayoutPanel3.Controls.Add(this.lblSum, 1, 7);
            this.tableLayoutPanel3.Controls.Add(this.label7, 0, 8);
            this.tableLayoutPanel3.Controls.Add(this.btnClearCount, 1, 8);
            this.tableLayoutPanel3.Controls.Add(this.btnIllumination, 1, 9);
            this.tableLayoutPanel3.Controls.Add(this.label14, 0, 9);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 22);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 12;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.076819F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.076819F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.076819F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.076819F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.076819F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.076819F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.076819F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.076819F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.803734F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.803734F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.974244F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.803734F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(265, 348);
            this.tableLayoutPanel3.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(1, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 27);
            this.label3.TabIndex = 3;
            this.label3.Text = "设备状态:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStationName
            // 
            this.lblStationName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStationName.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStationName.ForeColor = System.Drawing.Color.Black;
            this.lblStationName.Location = new System.Drawing.Point(109, 29);
            this.lblStationName.Name = "lblStationName";
            this.lblStationName.Size = new System.Drawing.Size(152, 27);
            this.lblStationName.TabIndex = 1;
            this.lblStationName.Text = "固化上料设备";
            this.lblStationName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAlarmClean
            // 
            this.btnAlarmClean.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAlarmClean.Location = new System.Drawing.Point(107, 317);
            this.btnAlarmClean.Margin = new System.Windows.Forms.Padding(1);
            this.btnAlarmClean.Name = "btnAlarmClean";
            this.btnAlarmClean.Size = new System.Drawing.Size(156, 29);
            this.btnAlarmClean.TabIndex = 105;
            this.btnAlarmClean.Text = "报警清除";
            this.btnAlarmClean.UseVisualStyleBackColor = true;
            this.btnAlarmClean.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAlarmClean_MouseDown_1);
            this.btnAlarmClean.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAlarmClean_MouseUp_1);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(1, 57);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 27);
            this.label4.TabIndex = 0;
            this.label4.Text = "产品型号:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(1, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "设备名称:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProductType
            // 
            this.lblProductType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductType.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblProductType.Location = new System.Drawing.Point(109, 57);
            this.lblProductType.Name = "lblProductType";
            this.lblProductType.Size = new System.Drawing.Size(152, 27);
            this.lblProductType.TabIndex = 66;
            this.lblProductType.Text = "MTF001";
            this.lblProductType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tableLayoutPanel3.SetColumnSpan(this.panel6, 2);
            this.panel6.Controls.Add(this.btnRunSetting);
            this.panel6.Controls.Add(this.btnTricolorLamp);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(1, 285);
            this.panel6.Margin = new System.Windows.Forms.Padding(0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(263, 30);
            this.panel6.TabIndex = 9;
            // 
            // btnRunSetting
            // 
            this.btnRunSetting.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRunSetting.Location = new System.Drawing.Point(0, 0);
            this.btnRunSetting.Margin = new System.Windows.Forms.Padding(1);
            this.btnRunSetting.Name = "btnRunSetting";
            this.btnRunSetting.Size = new System.Drawing.Size(104, 30);
            this.btnRunSetting.TabIndex = 8;
            this.btnRunSetting.Text = "设置";
            this.btnRunSetting.UseVisualStyleBackColor = true;
            this.btnRunSetting.Click += new System.EventHandler(this.btnRunSetting_Click);
            // 
            // btnTricolorLamp
            // 
            this.btnTricolorLamp.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnTricolorLamp.Location = new System.Drawing.Point(106, 0);
            this.btnTricolorLamp.Name = "btnTricolorLamp";
            this.btnTricolorLamp.Size = new System.Drawing.Size(157, 30);
            this.btnTricolorLamp.TabIndex = 7;
            this.btnTricolorLamp.Text = "蜂鸣";
            this.btnTricolorLamp.UseVisualStyleBackColor = true;
            this.btnTricolorLamp.Click += new System.EventHandler(this.btnTricolorLamp_Click);
            // 
            // button5
            // 
            this.button5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button5.Location = new System.Drawing.Point(2, 317);
            this.button5.Margin = new System.Windows.Forms.Padding(1);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(102, 29);
            this.button5.TabIndex = 97;
            this.button5.Text = "生产模式";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // lblMachineStatus
            // 
            this.lblMachineStatus.BackColor = System.Drawing.Color.Gainsboro;
            this.lblMachineStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMachineStatus.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMachineStatus.ForeColor = System.Drawing.Color.Green;
            this.lblMachineStatus.Location = new System.Drawing.Point(106, 1);
            this.lblMachineStatus.Margin = new System.Windows.Forms.Padding(0);
            this.lblMachineStatus.Name = "lblMachineStatus";
            this.lblMachineStatus.Size = new System.Drawing.Size(158, 27);
            this.lblMachineStatus.TabIndex = 71;
            this.lblMachineStatus.Text = "设备未准备好";
            this.lblMachineStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(1, 141);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 27);
            this.label2.TabIndex = 0;
            this.label2.Text = "OK:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOK
            // 
            this.lblOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOK.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOK.ForeColor = System.Drawing.Color.Black;
            this.lblOK.Location = new System.Drawing.Point(109, 141);
            this.lblOK.Name = "lblOK";
            this.lblOK.Size = new System.Drawing.Size(152, 27);
            this.lblOK.TabIndex = 6;
            this.lblOK.Text = "0";
            this.lblOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.ForeColor = System.Drawing.Color.Gray;
            this.label6.Location = new System.Drawing.Point(1, 169);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 27);
            this.label6.TabIndex = 5;
            this.label6.Text = "NG:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(1, 197);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 27);
            this.label5.TabIndex = 4;
            this.label5.Text = "总数:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNG
            // 
            this.lblNG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNG.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNG.ForeColor = System.Drawing.Color.Black;
            this.lblNG.Location = new System.Drawing.Point(109, 169);
            this.lblNG.Name = "lblNG";
            this.lblNG.Size = new System.Drawing.Size(152, 27);
            this.lblNG.TabIndex = 6;
            this.lblNG.Text = "0";
            this.lblNG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSum
            // 
            this.lblSum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSum.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSum.ForeColor = System.Drawing.Color.Black;
            this.lblSum.Location = new System.Drawing.Point(109, 197);
            this.lblSum.Name = "lblSum";
            this.lblSum.Size = new System.Drawing.Size(152, 27);
            this.lblSum.TabIndex = 6;
            this.lblSum.Text = "0";
            this.lblSum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.ForeColor = System.Drawing.Color.Gray;
            this.label7.Location = new System.Drawing.Point(4, 225);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 29);
            this.label7.TabIndex = 74;
            this.label7.Text = "统计清零:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnClearCount
            // 
            this.btnClearCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClearCount.Location = new System.Drawing.Point(107, 226);
            this.btnClearCount.Margin = new System.Windows.Forms.Padding(1);
            this.btnClearCount.Name = "btnClearCount";
            this.btnClearCount.Size = new System.Drawing.Size(156, 27);
            this.btnClearCount.TabIndex = 75;
            this.btnClearCount.Text = "计数清零";
            this.btnClearCount.UseVisualStyleBackColor = true;
            this.btnClearCount.Click += new System.EventHandler(this.btnClearCount_Click);
            // 
            // btnIllumination
            // 
            this.btnIllumination.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnIllumination.Location = new System.Drawing.Point(107, 256);
            this.btnIllumination.Margin = new System.Windows.Forms.Padding(1);
            this.btnIllumination.Name = "btnIllumination";
            this.btnIllumination.Size = new System.Drawing.Size(156, 27);
            this.btnIllumination.TabIndex = 9;
            this.btnIllumination.Text = "照明灯开";
            this.btnIllumination.UseVisualStyleBackColor = true;
            this.btnIllumination.Click += new System.EventHandler(this.btnIllumination_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.ForeColor = System.Drawing.Color.Gray;
            this.label14.Location = new System.Drawing.Point(4, 255);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(98, 29);
            this.label14.TabIndex = 76;
            this.label14.Text = "照明:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // btnReset
            // 
            this.btnReset.Image = global::_0R02.Properties.Resources.ic_action_reload;
            this.btnReset.Location = new System.Drawing.Point(201, 513);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(74, 70);
            this.btnReset.TabIndex = 79;
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnReset_MouseDown);
            this.btnReset.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnReset_MouseUp);
            // 
            // btnStop
            // 
            this.btnStop.Image = global::_0R02.Properties.Resources.ic_action_playback_stop;
            this.btnStop.Location = new System.Drawing.Point(104, 513);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(74, 70);
            this.btnStop.TabIndex = 79;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnStop_MouseDown);
            this.btnStop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnStop_MouseUp);
            // 
            // btnStart
            // 
            this.btnStart.Image = global::_0R02.Properties.Resources.ic_action_playback_play;
            this.btnStart.Location = new System.Drawing.Point(7, 513);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(74, 70);
            this.btnStart.TabIndex = 79;
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnStart_MouseDown);
            this.btnStart.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnStart_MouseUp);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstInfo);
            this.groupBox1.Location = new System.Drawing.Point(6, 613);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(602, 112);
            this.groupBox1.TabIndex = 84;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "报警信息";
            // 
            // lstInfo
            // 
            this.lstInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstInfo.FormattingEnabled = true;
            this.lstInfo.ItemHeight = 12;
            this.lstInfo.Location = new System.Drawing.Point(3, 17);
            this.lstInfo.Name = "lstInfo";
            this.lstInfo.Size = new System.Drawing.Size(596, 92);
            this.lstInfo.TabIndex = 0;
            // 
            // rbHand
            // 
            this.rbHand.AutoSize = true;
            this.rbHand.Checked = true;
            this.rbHand.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbHand.Location = new System.Drawing.Point(13, 478);
            this.rbHand.Name = "rbHand";
            this.rbHand.Size = new System.Drawing.Size(111, 24);
            this.rbHand.TabIndex = 85;
            this.rbHand.TabStop = true;
            this.rbHand.Text = "手动模式";
            this.rbHand.UseVisualStyleBackColor = true;
            // 
            // rbAuto
            // 
            this.rbAuto.AutoSize = true;
            this.rbAuto.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbAuto.Location = new System.Drawing.Point(137, 477);
            this.rbAuto.Name = "rbAuto";
            this.rbAuto.Size = new System.Drawing.Size(116, 25);
            this.rbAuto.TabIndex = 86;
            this.rbAuto.Text = "自动模式";
            this.rbAuto.UseVisualStyleBackColor = true;
            // 
            // refreshDatatimer
            // 
            this.refreshDatatimer.Interval = 2000;
            this.refreshDatatimer.Tick += new System.EventHandler(this.refreshDatatimer_Tick);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(26, 585);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 12);
            this.label11.TabIndex = 87;
            this.label11.Text = "运行";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(124, 585);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(31, 12);
            this.label12.TabIndex = 87;
            this.label12.Text = "停止";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(222, 585);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(31, 12);
            this.label13.TabIndex = 87;
            this.label13.Text = "复位";
            // 
            // timer1
            // 
            this.timer1.Interval = 8000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cb_doorok
            // 
            this.cb_doorok.AutoSize = true;
            this.cb_doorok.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_doorok.Location = new System.Drawing.Point(1046, 37);
            this.cb_doorok.Name = "cb_doorok";
            this.cb_doorok.Size = new System.Drawing.Size(113, 25);
            this.cb_doorok.TabIndex = 104;
            this.cb_doorok.Text = "门禁开启";
            this.cb_doorok.UseVisualStyleBackColor = true;
            this.cb_doorok.CheckedChanged += new System.EventHandler(this.cb_doorok_CheckedChanged);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 765);
            this.Controls.Add(this.cb_doorok);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.rbAuto);
            this.Controls.Add(this.rbHand);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tbcMain);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "固化设备(V0.1.2.0)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaryResult)).EndInit();
            this.tbcMain.ResumeLayout(false);
            this.tbgMain.ResumeLayout(false);
            this.tbgMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStoveResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.temperatureChart)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnMain;
        private System.Windows.Forms.ToolStripButton btnQRCodeSet;
        private System.Windows.Forms.ToolStripButton btnIOMonitor;
        private System.Windows.Forms.ToolStripButton btnLogin;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel IabPlcCom;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel labQRCodeCom;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Timer refreshStateTimer;
        private System.Windows.Forms.ToolStripButton btnRecipe;
        private System.Windows.Forms.DataGridView dgvTaryResult;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TabControl tbcMain;
        private System.Windows.Forms.TabPage tbgMain;
        private System.Windows.Forms.TabPage tbgIOMonitor;
        private System.Windows.Forms.TabPage tbgPLCMonitor;
        private System.Windows.Forms.TabPage tbgRecipe;
        private System.Windows.Forms.TabPage tbgOperation;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblStationName;
        private System.Windows.Forms.Label lblSum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblNG;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblProductType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblOK;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnRunSetting;
        private System.Windows.Forms.Button btnTricolorLamp;
        private System.Windows.Forms.Label lblMachineStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnClearCount;
        private System.Windows.Forms.ToolStripButton btnMes;
        private System.Windows.Forms.ToolStripButton btnPLCMonitor;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox tbOven6Temperature;
        private System.Windows.Forms.TextBox tbOven5Temperature;
        private System.Windows.Forms.TextBox tbOven4Temperature;
        private System.Windows.Forms.TextBox tbOven3Temperature;
        private System.Windows.Forms.TextBox tbOven2Temperature;
        private System.Windows.Forms.TextBox tbOven1Temperature;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataVisualization.Charting.Chart temperatureChart;
        private System.Windows.Forms.Label labOven1Start;
        private System.Windows.Forms.Label labOven2Start;
        private System.Windows.Forms.Label labOven3Start;
        private System.Windows.Forms.Label labOven4Start;
        private System.Windows.Forms.Label labOven5Start;
        private System.Windows.Forms.Label labOven6Start;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel labRobot;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstInfo;
        private System.Windows.Forms.RadioButton rbHand;
        private System.Windows.Forms.RadioButton rbAuto;
        private System.Windows.Forms.Timer refreshDatatimer;
        private System.Windows.Forms.ToolStripButton btnOperation;
        private System.Windows.Forms.DataGridView dgvStoveResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoleNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn QRCodeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StoveNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn StoveHoleNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn StoveQRCodeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StoveTemperature;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnIllumination;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox cb_down;
        private System.Windows.Forms.CheckBox cb_doorok;
        private System.Windows.Forms.Button btnAlarmClean;
        private System.Windows.Forms.ToolStripStatusLabel tstrobotstatus;
        private System.Windows.Forms.ToolStripStatusLabel tstrobotstep;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel labTrayCodeCom;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripButton btnTrayCodeSet;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
    }
}

