namespace Desay
{
    partial class FrmIOmonitor
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
            this.gbxOutput0 = new System.Windows.Forms.FlowLayoutPanel();
            this.gbxOutput1 = new System.Windows.Forms.FlowLayoutPanel();
            this.gbxOutput2 = new System.Windows.Forms.FlowLayoutPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabInput = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.gbxInput0 = new System.Windows.Forms.FlowLayoutPanel();
            this.gbxInput1 = new System.Windows.Forms.FlowLayoutPanel();
            this.gbxInput2 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabOutput = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl2.SuspendLayout();
            this.tabInput.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabOutput.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxOutput0
            // 
            this.gbxOutput0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxOutput0.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbxOutput0.Location = new System.Drawing.Point(4, 4);
            this.gbxOutput0.Name = "gbxOutput0";
            this.gbxOutput0.Size = new System.Drawing.Size(241, 470);
            this.gbxOutput0.TabIndex = 83;
            // 
            // gbxOutput1
            // 
            this.gbxOutput1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxOutput1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbxOutput1.Location = new System.Drawing.Point(252, 4);
            this.gbxOutput1.Name = "gbxOutput1";
            this.gbxOutput1.Size = new System.Drawing.Size(241, 470);
            this.gbxOutput1.TabIndex = 84;
            // 
            // gbxOutput2
            // 
            this.gbxOutput2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxOutput2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbxOutput2.Location = new System.Drawing.Point(500, 4);
            this.gbxOutput2.Name = "gbxOutput2";
            this.gbxOutput2.Size = new System.Drawing.Size(242, 470);
            this.gbxOutput2.TabIndex = 85;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tabControl2
            // 
            this.tabControl2.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControl2.Controls.Add(this.tabInput);
            this.tabControl2.Controls.Add(this.tabOutput);
            this.tabControl2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl2.ItemSize = new System.Drawing.Size(150, 40);
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.Padding = new System.Drawing.Point(0, 0);
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(894, 532);
            this.tabControl2.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl2.TabIndex = 2;
            // 
            // tabInput
            // 
            this.tabInput.Controls.Add(this.tableLayoutPanel2);
            this.tabInput.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabInput.Location = new System.Drawing.Point(4, 44);
            this.tabInput.Margin = new System.Windows.Forms.Padding(0);
            this.tabInput.Name = "tabInput";
            this.tabInput.Size = new System.Drawing.Size(886, 484);
            this.tabInput.TabIndex = 0;
            this.tabInput.Text = "输入信号";
            this.tabInput.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.gbxInput0, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.gbxInput1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.gbxInput2, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 483F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 483F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(886, 484);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // gbxInput0
            // 
            this.gbxInput0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxInput0.Location = new System.Drawing.Point(4, 4);
            this.gbxInput0.Name = "gbxInput0";
            this.gbxInput0.Size = new System.Drawing.Size(288, 476);
            this.gbxInput0.TabIndex = 78;
            // 
            // gbxInput1
            // 
            this.gbxInput1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxInput1.Location = new System.Drawing.Point(299, 4);
            this.gbxInput1.Name = "gbxInput1";
            this.gbxInput1.Size = new System.Drawing.Size(288, 476);
            this.gbxInput1.TabIndex = 79;
            // 
            // gbxInput2
            // 
            this.gbxInput2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxInput2.Location = new System.Drawing.Point(594, 4);
            this.gbxInput2.Name = "gbxInput2";
            this.gbxInput2.Size = new System.Drawing.Size(288, 476);
            this.gbxInput2.TabIndex = 80;
            // 
            // tabOutput
            // 
            this.tabOutput.Controls.Add(this.tableLayoutPanel4);
            this.tabOutput.Location = new System.Drawing.Point(4, 44);
            this.tabOutput.Name = "tabOutput";
            this.tabOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabOutput.Size = new System.Drawing.Size(752, 484);
            this.tabOutput.TabIndex = 1;
            this.tabOutput.Text = "输出信号";
            this.tabOutput.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.gbxOutput0, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.gbxOutput2, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.gbxOutput1, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(746, 478);
            this.tableLayoutPanel4.TabIndex = 4;
            // 
            // FrmIOmonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 532);
            this.Controls.Add(this.tabControl2);
            this.Name = "FrmIOmonitor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IO监控表";
            this.Load += new System.EventHandler(this.frmIOmonitor_Load);
            this.tabControl2.ResumeLayout(false);
            this.tabInput.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tabOutput.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel gbxOutput2;
        private System.Windows.Forms.FlowLayoutPanel gbxOutput1;
        private System.Windows.Forms.FlowLayoutPanel gbxOutput0;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabOutput;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TabPage tabInput;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel gbxInput0;
        private System.Windows.Forms.FlowLayoutPanel gbxInput1;
        private System.Windows.Forms.FlowLayoutPanel gbxInput2;
    }
}