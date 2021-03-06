﻿using System;
using System.Windows.Forms;

namespace Desay
{
    public partial class frmStarting : Form
    {
        public frmStarting()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public frmStarting(byte maxValue)
            : this()
        {            
            PBress.Maximum = maxValue;
            PBress.Step = 1;
            label1.Text = "加载中";
        }

        public void ShowMessage(string str)
        {
            if (InvokeRequired)
            {
                try
                {
                    Invoke(new Action<string>(ShowMessage), str);
                }
                catch { }
            }
            else
            {
                label1.Text = str;
                PBress.PerformStep();
                if (PBress.Value >= PBress.Maximum)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }
    }
}
