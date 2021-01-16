namespace CVMes
{
    partial class FrmMesSet
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
            this.txtShowFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblShowEv = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtShowMe = new System.Windows.Forms.TextBox();
            this.txtSavePath = new System.Windows.Forms.TextBox();
            this.btnSavePath = new System.Windows.Forms.Button();
            this.btnMesExample = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoadConfig = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboxSN = new System.Windows.Forms.CheckBox();
            this.lblLinkResult = new System.Windows.Forms.Label();
            this.btnLink = new System.Windows.Forms.Button();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblLinkStr = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtShowFile
            // 
            this.txtShowFile.Location = new System.Drawing.Point(104, 12);
            this.txtShowFile.Name = "txtShowFile";
            this.txtShowFile.ReadOnly = true;
            this.txtShowFile.Size = new System.Drawing.Size(785, 21);
            this.txtShowFile.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "EV_MSTR参数：";
            // 
            // lblShowEv
            // 
            this.lblShowEv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblShowEv.Location = new System.Drawing.Point(12, 74);
            this.lblShowEv.Name = "lblShowEv";
            this.lblShowEv.Size = new System.Drawing.Size(968, 46);
            this.lblShowEv.TabIndex = 4;
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(12, 128);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(71, 12);
            this.lbl2.TabIndex = 5;
            this.lbl2.Text = "ME_MSTR参数";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(905, 335);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 39);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "退出(&X)";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Visible = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtShowMe
            // 
            this.txtShowMe.Location = new System.Drawing.Point(12, 143);
            this.txtShowMe.Multiline = true;
            this.txtShowMe.Name = "txtShowMe";
            this.txtShowMe.ReadOnly = true;
            this.txtShowMe.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtShowMe.Size = new System.Drawing.Size(968, 106);
            this.txtShowMe.TabIndex = 6;
            // 
            // txtSavePath
            // 
            this.txtSavePath.Location = new System.Drawing.Point(14, 343);
            this.txtSavePath.Name = "txtSavePath";
            this.txtSavePath.ReadOnly = true;
            this.txtSavePath.Size = new System.Drawing.Size(694, 21);
            this.txtSavePath.TabIndex = 7;
            // 
            // btnSavePath
            // 
            this.btnSavePath.Image = global::MesData.Properties.Resources.ic_action_folder_closed;
            this.btnSavePath.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSavePath.Location = new System.Drawing.Point(714, 343);
            this.btnSavePath.Name = "btnSavePath";
            this.btnSavePath.Size = new System.Drawing.Size(176, 23);
            this.btnSavePath.TabIndex = 8;
            this.btnSavePath.Text = "SV追溯系统保存路径(&F)";
            this.btnSavePath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSavePath.UseVisualStyleBackColor = true;
            this.btnSavePath.Click += new System.EventHandler(this.btnSavePath_Click);
            // 
            // btnMesExample
            // 
            this.btnMesExample.Image = global::MesData.Properties.Resources.ic_action_list;
            this.btnMesExample.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMesExample.Location = new System.Drawing.Point(563, 376);
            this.btnMesExample.Name = "btnMesExample";
            this.btnMesExample.Size = new System.Drawing.Size(145, 39);
            this.btnMesExample.TabIndex = 2;
            this.btnMesExample.Text = "SV追溯系统示例(&M)";
            this.btnMesExample.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMesExample.UseVisualStyleBackColor = true;
            this.btnMesExample.Click += new System.EventHandler(this.btnMesExample_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::MesData.Properties.Resources.ic_action_save;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(770, 376);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 39);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存配置(&S)";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoadConfig
            // 
            this.btnLoadConfig.Image = global::MesData.Properties.Resources.ic_action_folder_open;
            this.btnLoadConfig.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoadConfig.Location = new System.Drawing.Point(838, 48);
            this.btnLoadConfig.Name = "btnLoadConfig";
            this.btnLoadConfig.Size = new System.Drawing.Size(142, 23);
            this.btnLoadConfig.TabIndex = 0;
            this.btnLoadConfig.Text = "导入配置文件(&L)";
            this.btnLoadConfig.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLoadConfig.UseVisualStyleBackColor = true;
            this.btnLoadConfig.Visible = false;
            this.btnLoadConfig.Click += new System.EventHandler(this.btnLoadConfig_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboxSN);
            this.groupBox1.Controls.Add(this.lblLinkResult);
            this.groupBox1.Controls.Add(this.btnLink);
            this.groupBox1.Controls.Add(this.txtSN);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblLinkStr);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(14, 257);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(966, 72);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据库测试";
            // 
            // cboxSN
            // 
            this.cboxSN.AutoSize = true;
            this.cboxSN.Location = new System.Drawing.Point(532, 43);
            this.cboxSN.Name = "cboxSN";
            this.cboxSN.Size = new System.Drawing.Size(84, 16);
            this.cboxSN.TabIndex = 6;
            this.cboxSN.Text = "屏蔽SN检查";
            this.cboxSN.UseVisualStyleBackColor = true;
            // 
            // lblLinkResult
            // 
            this.lblLinkResult.AutoSize = true;
            this.lblLinkResult.Location = new System.Drawing.Point(452, 48);
            this.lblLinkResult.Name = "lblLinkResult";
            this.lblLinkResult.Size = new System.Drawing.Size(0, 12);
            this.lblLinkResult.TabIndex = 5;
            // 
            // btnLink
            // 
            this.btnLink.Location = new System.Drawing.Point(343, 43);
            this.btnLink.Name = "btnLink";
            this.btnLink.Size = new System.Drawing.Size(75, 23);
            this.btnLink.TabIndex = 4;
            this.btnLink.Text = "连接";
            this.btnLink.UseVisualStyleBackColor = true;
            this.btnLink.Click += new System.EventHandler(this.btnLink_Click);
            // 
            // txtSN
            // 
            this.txtSN.Location = new System.Drawing.Point(103, 45);
            this.txtSN.Name = "txtSN";
            this.txtSN.Size = new System.Drawing.Size(218, 21);
            this.txtSN.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "测试SN：";
            // 
            // lblLinkStr
            // 
            this.lblLinkStr.AutoSize = true;
            this.lblLinkStr.Location = new System.Drawing.Point(89, 22);
            this.lblLinkStr.Name = "lblLinkStr";
            this.lblLinkStr.Size = new System.Drawing.Size(0, 12);
            this.lblLinkStr.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "连接字符串：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "配置文件路径：";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(896, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(83, 23);
            this.btnRefresh.TabIndex = 11;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // FrmMesSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 424);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSavePath);
            this.Controls.Add(this.txtSavePath);
            this.Controls.Add(this.txtShowMe);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.lblShowEv);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnMesExample);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtShowFile);
            this.Controls.Add(this.btnLoadConfig);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmMesSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SV追溯系统设置";
            this.Load += new System.EventHandler(this.FrmMesSet_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadConfig;
        private System.Windows.Forms.TextBox txtShowFile;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblShowEv;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Button btnMesExample;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtShowMe;
        private System.Windows.Forms.TextBox txtSavePath;
        private System.Windows.Forms.Button btnSavePath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblLinkResult;
        private System.Windows.Forms.Button btnLink;
        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblLinkStr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cboxSN;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRefresh;
    }
}