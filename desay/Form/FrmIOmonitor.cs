using System;
using System.Windows.Forms;
using System.Drawing;


namespace Desay
{
    public partial class FrmIOmonitor : Form
    {
        private IOsign[] Input0, Input1, Input2;
        private IOsign[] Output0, Output1, Output2;

        public FrmIOmonitor()
        {
            InitializeComponent();

            Input0 = new IOsign[]
            { 
                new IOsign(IoPoints.T1DI00),new IOsign(IoPoints.T1DI01),new IOsign(IoPoints.T1DI02),new IOsign(IoPoints.T1DI03),
                new IOsign(IoPoints.T1DI04),new IOsign(IoPoints.T1DI05), new IOsign(IoPoints.T1DI06),new IOsign(IoPoints.T1DI07),
                new IOsign(IoPoints.T1DI08),new IOsign(IoPoints.T1DI09),new IOsign(IoPoints.T1DI10),new IOsign(IoPoints.T1DI11),
                new IOsign(IoPoints.T1DI12),new IOsign(IoPoints.T1DI13),new IOsign(IoPoints.T1DI14),new IOsign(IoPoints.T1DI15)
            };
            Input1 = new IOsign[]
            {
                new IOsign(IoPoints.I2DI00),new IOsign(IoPoints.I2DI01),new IOsign(IoPoints.I2DI02),new IOsign(IoPoints.I2DI03),
                new IOsign(IoPoints.I2DI04),new IOsign(IoPoints.I2DI05), new IOsign(IoPoints.I2DI06),new IOsign(IoPoints.I2DI07),
                new IOsign(IoPoints.I2DI08),new IOsign(IoPoints.I2DI09),new IOsign(IoPoints.I2DI10),new IOsign(IoPoints.I2DI11),
                new IOsign(IoPoints.I2DI12),new IOsign(IoPoints.I2DI13),new IOsign(IoPoints.I2DI14),new IOsign(IoPoints.I2DI15)
            };

            Input2 = new IOsign[]
            {
                new IOsign(IoPoints.I2DI16),new IOsign(IoPoints.I2DI17),new IOsign(IoPoints.I2DI18),new IOsign(IoPoints.I2DI19),
                new IOsign(IoPoints.I2DI20),new IOsign(IoPoints.I2DI21), new IOsign(IoPoints.I2DI22),new IOsign(IoPoints.I2DI23),
                new IOsign(IoPoints.I2DI24),new IOsign(IoPoints.I2DI25),new IOsign(IoPoints.I2DI26),new IOsign(IoPoints.I2DI27),
                new IOsign(IoPoints.I2DI28),new IOsign(IoPoints.I2DI29),new IOsign(IoPoints.I2DI30),new IOsign(IoPoints.I2DI31)
            };

            Output0 = new IOsign[]
            {
                new IOsign(IoPoints.T1DO00),new IOsign(IoPoints.T1DO01),new IOsign(IoPoints.T1DO02),new IOsign(IoPoints.T1DO03),
                new IOsign(IoPoints.T1DO04),new IOsign(IoPoints.T1DO05), new IOsign(IoPoints.T1DO06),new IOsign(IoPoints.T1DO07),
                new IOsign(IoPoints.T1DO08),new IOsign(IoPoints.T1DO09),new IOsign(IoPoints.T1DO10),new IOsign(IoPoints.T1DO11),
                new IOsign(IoPoints.T1DO12),new IOsign(IoPoints.T1DO13),new IOsign(IoPoints.T1DO14),new IOsign(IoPoints.T1DO15)
            };

            Output1 = new IOsign[]
            {
               new IOsign(IoPoints.I2DO00),new IOsign(IoPoints.I2DO01),new IOsign(IoPoints.I2DO02),new IOsign(IoPoints.I2DO03),
                new IOsign(IoPoints.I2DO04),new IOsign(IoPoints.I2DO05),new IOsign(IoPoints.I2DO06),new IOsign(IoPoints.I2DO07),
                new IOsign(IoPoints.I2DO08),new IOsign(IoPoints.I2DO09),new IOsign(IoPoints.I2DO10),new IOsign(IoPoints.I2DO11),
                new IOsign(IoPoints.I2DO12),new IOsign(IoPoints.I2DO13),new IOsign(IoPoints.I2DO14),new IOsign(IoPoints.I2DO15)
            };

            Output2 = new IOsign[]
            {
                 new IOsign(IoPoints.I2DO16),new IOsign(IoPoints.I2DO17),new IOsign(IoPoints.I2DO18),new IOsign(IoPoints.I2DO19),
                new IOsign(IoPoints.I2DO20),new IOsign(IoPoints.I2DO21),new IOsign(IoPoints.I2DO22),new IOsign(IoPoints.I2DO23),
                new IOsign(IoPoints.I2DO24),new IOsign(IoPoints.I2DO25),new IOsign(IoPoints.I2DO26),new IOsign(IoPoints.I2DO27),
                new IOsign(IoPoints.I2DO28),new IOsign(IoPoints.I2DO29),new IOsign(IoPoints.I2DO30),new IOsign(IoPoints.I2DO31)
            };
        }

        private void frmIOmonitor_Load(object sender, EventArgs e)
        {
            for (var i = 0; i < 16; i++) gbxInput0.Controls.Add(Input0[i]);
            for (var i = 0; i < 16; i++) gbxInput1.Controls.Add(Input1[i]);
            for (var i = 0; i < 16; i++) gbxInput2.Controls.Add(Input2[i]);
            for (var i = 0; i < 16; i++) gbxOutput0.Controls.Add(Output0[i]);
            for (var i = 0; i < 16; i++) gbxOutput1.Controls.Add(Output1[i]);
            for (var i = 0; i < 16; i++) gbxOutput2.Controls.Add(Output2[i]);

            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            for (var i = 0; i < 16; i++) Input0[i].Refresh();
            for (var i = 0; i < 16; i++) Input1[i].Refresh();
            for (var i = 0; i < 16; i++) Input2[i].Refresh();
            for (var i = 0; i < 16; i++) Output0[i].Refresh();
            for (var i = 0; i < 16; i++) Output1[i].Refresh();
            for (var i = 0; i < 16; i++) Output2[i].Refresh();

            timer1.Enabled = true;
        }
    }
}
