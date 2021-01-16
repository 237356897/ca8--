namespace Desay
{
    partial class frmRecipe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRecipe));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.修改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新增ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label57 = new System.Windows.Forms.Label();
            this.txtCurrentType = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtTargetType = new System.Windows.Forms.TextBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSwitch = new System.Windows.Forms.Button();
            this.label60 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.lstProductType = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nudTemperatureMax = new System.Windows.Forms.NumericUpDown();
            this.nudTemperatureMin = new System.Windows.Forms.NumericUpDown();
            this.nudStoveTimeMax = new System.Windows.Forms.NumericUpDown();
            this.nudStoveTimeMin = new System.Windows.Forms.NumericUpDown();
            this.nudStoveNoMax = new System.Windows.Forms.NumericUpDown();
            this.nudStoveNoMin = new System.Windows.Forms.NumericUpDown();
            this.btnStoveSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbA2C = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cb_typelist = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_curtype = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.bt_new = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTemperatureMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTemperatureMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStoveTimeMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStoveTimeMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStoveNoMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStoveNoMin)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.修改ToolStripMenuItem,
            this.删除ToolStripMenuItem,
            this.新增ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 70);
            // 
            // 修改ToolStripMenuItem
            // 
            this.修改ToolStripMenuItem.Name = "修改ToolStripMenuItem";
            this.修改ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.修改ToolStripMenuItem.Text = "修改";
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            // 
            // 新增ToolStripMenuItem
            // 
            this.新增ToolStripMenuItem.Name = "新增ToolStripMenuItem";
            this.新增ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.新增ToolStripMenuItem.Text = "新增";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label57);
            this.groupBox1.Controls.Add(this.txtCurrentType);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.txtTargetType);
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Controls.Add(this.btnSwitch);
            this.groupBox1.Controls.Add(this.label60);
            this.groupBox1.Location = new System.Drawing.Point(301, 395);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(183, 377);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作面板";
            this.groupBox1.Visible = false;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(19, 20);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(89, 12);
            this.label57.TabIndex = 25;
            this.label57.Text = "当前产品型号：";
            // 
            // txtCurrentType
            // 
            this.txtCurrentType.Location = new System.Drawing.Point(22, 48);
            this.txtCurrentType.Name = "txtCurrentType";
            this.txtCurrentType.ReadOnly = true;
            this.txtCurrentType.Size = new System.Drawing.Size(138, 21);
            this.txtCurrentType.TabIndex = 31;
            this.txtCurrentType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(22, 221);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(138, 36);
            this.btnDelete.TabIndex = 30;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtTargetType
            // 
            this.txtTargetType.Location = new System.Drawing.Point(22, 113);
            this.txtTargetType.Name = "txtTargetType";
            this.txtTargetType.Size = new System.Drawing.Size(138, 21);
            this.txtTargetType.TabIndex = 27;
            this.txtTargetType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(21, 163);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(138, 36);
            this.btnNew.TabIndex = 28;
            this.btnNew.Text = "新增";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSwitch
            // 
            this.btnSwitch.Location = new System.Drawing.Point(22, 279);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(138, 36);
            this.btnSwitch.TabIndex = 29;
            this.btnSwitch.Text = "切换型号";
            this.btnSwitch.UseVisualStyleBackColor = true;
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(20, 85);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(89, 12);
            this.label60.TabIndex = 24;
            this.label60.Text = "目标产品型号：";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.lstProductType);
            this.groupBox8.Location = new System.Drawing.Point(31, 395);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(246, 377);
            this.groupBox8.TabIndex = 33;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "型号列表";
            this.groupBox8.Visible = false;
            // 
            // lstProductType
            // 
            this.lstProductType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstProductType.FormattingEnabled = true;
            this.lstProductType.ItemHeight = 12;
            this.lstProductType.Location = new System.Drawing.Point(3, 17);
            this.lstProductType.Name = "lstProductType";
            this.lstProductType.Size = new System.Drawing.Size(240, 357);
            this.lstProductType.TabIndex = 26;
            this.lstProductType.SelectedIndexChanged += new System.EventHandler(this.lstProductType_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.nudTemperatureMax);
            this.groupBox2.Controls.Add(this.nudTemperatureMin);
            this.groupBox2.Controls.Add(this.nudStoveTimeMax);
            this.groupBox2.Controls.Add(this.nudStoveTimeMin);
            this.groupBox2.Controls.Add(this.nudStoveNoMax);
            this.groupBox2.Controls.Add(this.nudStoveNoMin);
            this.groupBox2.Controls.Add(this.btnStoveSave);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(453, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(272, 377);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "参数设置";
            // 
            // nudTemperatureMax
            // 
            this.nudTemperatureMax.DecimalPlaces = 1;
            this.nudTemperatureMax.Location = new System.Drawing.Point(190, 99);
            this.nudTemperatureMax.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudTemperatureMax.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.nudTemperatureMax.Name = "nudTemperatureMax";
            this.nudTemperatureMax.Size = new System.Drawing.Size(59, 21);
            this.nudTemperatureMax.TabIndex = 5;
            this.nudTemperatureMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nudTemperatureMin
            // 
            this.nudTemperatureMin.DecimalPlaces = 1;
            this.nudTemperatureMin.Location = new System.Drawing.Point(99, 99);
            this.nudTemperatureMin.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudTemperatureMin.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.nudTemperatureMin.Name = "nudTemperatureMin";
            this.nudTemperatureMin.Size = new System.Drawing.Size(59, 21);
            this.nudTemperatureMin.TabIndex = 5;
            this.nudTemperatureMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nudStoveTimeMax
            // 
            this.nudStoveTimeMax.DecimalPlaces = 1;
            this.nudStoveTimeMax.Location = new System.Drawing.Point(190, 64);
            this.nudStoveTimeMax.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudStoveTimeMax.Name = "nudStoveTimeMax";
            this.nudStoveTimeMax.Size = new System.Drawing.Size(59, 21);
            this.nudStoveTimeMax.TabIndex = 5;
            this.nudStoveTimeMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nudStoveTimeMin
            // 
            this.nudStoveTimeMin.DecimalPlaces = 1;
            this.nudStoveTimeMin.Location = new System.Drawing.Point(99, 64);
            this.nudStoveTimeMin.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudStoveTimeMin.Name = "nudStoveTimeMin";
            this.nudStoveTimeMin.Size = new System.Drawing.Size(59, 21);
            this.nudStoveTimeMin.TabIndex = 5;
            this.nudStoveTimeMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nudStoveNoMax
            // 
            this.nudStoveNoMax.Location = new System.Drawing.Point(190, 136);
            this.nudStoveNoMax.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.nudStoveNoMax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudStoveNoMax.Name = "nudStoveNoMax";
            this.nudStoveNoMax.Size = new System.Drawing.Size(59, 21);
            this.nudStoveNoMax.TabIndex = 4;
            this.nudStoveNoMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudStoveNoMax.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // nudStoveNoMin
            // 
            this.nudStoveNoMin.Location = new System.Drawing.Point(99, 136);
            this.nudStoveNoMin.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.nudStoveNoMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudStoveNoMin.Name = "nudStoveNoMin";
            this.nudStoveNoMin.Size = new System.Drawing.Size(59, 21);
            this.nudStoveNoMin.TabIndex = 4;
            this.nudStoveNoMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudStoveNoMin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnStoveSave
            // 
            this.btnStoveSave.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStoveSave.Location = new System.Drawing.Point(87, 232);
            this.btnStoveSave.Name = "btnStoveSave";
            this.btnStoveSave.Size = new System.Drawing.Size(128, 42);
            this.btnStoveSave.TabIndex = 3;
            this.btnStoveSave.Text = "保存";
            this.btnStoveSave.UseVisualStyleBackColor = true;
            this.btnStoveSave.Click += new System.EventHandler(this.btnStoveSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(204, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Max";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 12);
            this.label5.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "固化炉号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "固化温度：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "固化时间(S)：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(117, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Min";
            // 
            // tbA2C
            // 
            this.tbA2C.Location = new System.Drawing.Point(57, 106);
            this.tbA2C.Name = "tbA2C";
            this.tbA2C.Size = new System.Drawing.Size(121, 21);
            this.tbA2C.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 111);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "A2C：";
            // 
            // cb_typelist
            // 
            this.cb_typelist.FormattingEnabled = true;
            this.cb_typelist.Location = new System.Drawing.Point(57, 29);
            this.cb_typelist.Name = "cb_typelist";
            this.cb_typelist.Size = new System.Drawing.Size(121, 20);
            this.cb_typelist.TabIndex = 36;
            this.cb_typelist.SelectedIndexChanged += new System.EventHandler(this.cb_typelist_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 37;
            this.label8.Text = "型号";
            // 
            // tb_curtype
            // 
            this.tb_curtype.Location = new System.Drawing.Point(57, 69);
            this.tb_curtype.Name = "tb_curtype";
            this.tb_curtype.Size = new System.Drawing.Size(121, 21);
            this.tb_curtype.TabIndex = 38;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 39;
            this.label9.Text = "选择型号";
            // 
            // bt_new
            // 
            this.bt_new.Location = new System.Drawing.Point(57, 146);
            this.bt_new.Name = "bt_new";
            this.bt_new.Size = new System.Drawing.Size(121, 23);
            this.bt_new.TabIndex = 40;
            this.bt_new.Text = "保存";
            this.bt_new.UseVisualStyleBackColor = true;
            this.bt_new.Click += new System.EventHandler(this.bt_new_Click);
            // 
            // frmRecipe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 498);
            this.Controls.Add(this.tbA2C);
            this.Controls.Add(this.bt_new);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tb_curtype);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cb_typelist);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.label7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRecipe";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "机种选择";
            this.Load += new System.EventHandler(this.FrmRecipe_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTemperatureMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTemperatureMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStoveTimeMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStoveTimeMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStoveNoMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStoveNoMin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 修改ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新增ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.TextBox txtCurrentType;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtTargetType;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSwitch;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ListBox lstProductType;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStoveSave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudStoveNoMax;
        private System.Windows.Forms.NumericUpDown nudStoveNoMin;
        private System.Windows.Forms.NumericUpDown nudStoveTimeMax;
        private System.Windows.Forms.NumericUpDown nudStoveTimeMin;
        private System.Windows.Forms.NumericUpDown nudTemperatureMax;
        private System.Windows.Forms.NumericUpDown nudTemperatureMin;
        private System.Windows.Forms.TextBox tbA2C;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cb_typelist;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb_curtype;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button bt_new;
    }
}