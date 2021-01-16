namespace Desay
{
    partial class FrmMesTest
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbConnectPwd = new System.Windows.Forms.TextBox();
            this.tbConnectUser = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nudPort = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnFracture = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbRoutingKeysName = new System.Windows.Forms.TextBox();
            this.tbExchangeName = new System.Windows.Forms.TextBox();
            this.tbIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbReceptionContent = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.tbSendContent = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbConnectPwd);
            this.groupBox1.Controls.Add(this.tbConnectUser);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.nudPort);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnFracture);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbRoutingKeysName);
            this.groupBox1.Controls.Add(this.tbExchangeName);
            this.groupBox1.Controls.Add(this.tbIP);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(186, 313);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "通信设置";
            // 
            // tbConnectPwd
            // 
            this.tbConnectPwd.Location = new System.Drawing.Point(67, 124);
            this.tbConnectPwd.Name = "tbConnectPwd";
            this.tbConnectPwd.Size = new System.Drawing.Size(113, 21);
            this.tbConnectPwd.TabIndex = 14;
            this.tbConnectPwd.Text = "guest";
            this.tbConnectPwd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbConnectPwd.UseSystemPasswordChar = true;
            // 
            // tbConnectUser
            // 
            this.tbConnectUser.Location = new System.Drawing.Point(67, 92);
            this.tbConnectUser.Name = "tbConnectUser";
            this.tbConnectUser.Size = new System.Drawing.Size(113, 21);
            this.tbConnectUser.TabIndex = 13;
            this.tbConnectUser.Text = "guest";
            this.tbConnectUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "密码：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "用户：";
            // 
            // nudPort
            // 
            this.nudPort.Location = new System.Drawing.Point(67, 60);
            this.nudPort.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudPort.Name = "nudPort";
            this.nudPort.Size = new System.Drawing.Size(113, 21);
            this.nudPort.TabIndex = 10;
            this.nudPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudPort.Value = new decimal(new int[] {
            5672,
            0,
            0,
            0});
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(102, 270);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(78, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnFracture
            // 
            this.btnFracture.Location = new System.Drawing.Point(102, 233);
            this.btnFracture.Name = "btnFracture";
            this.btnFracture.Size = new System.Drawing.Size(78, 23);
            this.btnFracture.TabIndex = 8;
            this.btnFracture.Text = "断开";
            this.btnFracture.UseVisualStyleBackColor = true;
            this.btnFracture.Click += new System.EventHandler(this.btnFracture_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(6, 233);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(78, 23);
            this.btnConnect.TabIndex = 7;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "路由键名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "交换机名：";
            // 
            // tbRoutingKeysName
            // 
            this.tbRoutingKeysName.Location = new System.Drawing.Point(67, 188);
            this.tbRoutingKeysName.Name = "tbRoutingKeysName";
            this.tbRoutingKeysName.Size = new System.Drawing.Size(113, 21);
            this.tbRoutingKeysName.TabIndex = 4;
            this.tbRoutingKeysName.Text = "test.1";
            this.tbRoutingKeysName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbExchangeName
            // 
            this.tbExchangeName.Location = new System.Drawing.Point(67, 156);
            this.tbExchangeName.Name = "tbExchangeName";
            this.tbExchangeName.Size = new System.Drawing.Size(113, 21);
            this.tbExchangeName.TabIndex = 3;
            this.tbExchangeName.Text = "amq.topic";
            this.tbExchangeName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbIP
            // 
            this.tbIP.Location = new System.Drawing.Point(67, 28);
            this.tbIP.Name = "tbIP";
            this.tbIP.Size = new System.Drawing.Size(113, 21);
            this.tbIP.TabIndex = 2;
            this.tbIP.Text = "127.0.0.1";
            this.tbIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "端口：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP地址：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbReceptionContent);
            this.groupBox2.Location = new System.Drawing.Point(204, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(340, 238);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "接收区";
            // 
            // tbReceptionContent
            // 
            this.tbReceptionContent.AcceptsTab = true;
            this.tbReceptionContent.Location = new System.Drawing.Point(6, 20);
            this.tbReceptionContent.Multiline = true;
            this.tbReceptionContent.Name = "tbReceptionContent";
            this.tbReceptionContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbReceptionContent.Size = new System.Drawing.Size(328, 212);
            this.tbReceptionContent.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnSend);
            this.groupBox3.Controls.Add(this.tbSendContent);
            this.groupBox3.Location = new System.Drawing.Point(204, 255);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(340, 70);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "发送区";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(270, 28);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(64, 23);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // tbSendContent
            // 
            this.tbSendContent.Location = new System.Drawing.Point(6, 29);
            this.tbSendContent.Name = "tbSendContent";
            this.tbSendContent.Size = new System.Drawing.Size(258, 21);
            this.tbSendContent.TabIndex = 0;
            // 
            // FrmMesTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 341);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMesTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MES调试";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox tbSendContent;
        protected System.Windows.Forms.TextBox tbReceptionContent;
        private System.Windows.Forms.Button btnFracture;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbRoutingKeysName;
        private System.Windows.Forms.TextBox tbExchangeName;
        private System.Windows.Forms.TextBox tbIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.NumericUpDown nudPort;
        private System.Windows.Forms.TextBox tbConnectPwd;
        private System.Windows.Forms.TextBox tbConnectUser;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
    }
}