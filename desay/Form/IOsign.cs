using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Motion.Interfaces;

namespace Desay
{
    public partial class IOsign : UserControl
    {
        private readonly IoPoint _signalPoint;
        public IOsign()
        {
            InitializeComponent();
        }
        public IOsign(IoPoint ioPoint):this()
        {
            _signalPoint = ioPoint;
        }
        public void Refresh()
        {
            pic.Image = _signalPoint.Value ? _0R02.Properties.Resources.LedGreen : _0R02.Properties.Resources.LedRed;
            lblName.Text = _signalPoint.Name;
            lblDes.Text = _signalPoint.Description;
        }
    }
}
