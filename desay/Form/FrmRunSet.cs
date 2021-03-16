using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Toolkit.Helpers;

namespace Desay
{
    public partial class FrmRunSet : Form
    {
        public FrmRunSet()
        {
            InitializeComponent();
        }

        private void FrmRunSet_Load(object sender, EventArgs e)
        {
            numRotatingDisk.Value = decimal.Parse(RunPara.Instance.RotatingDisk.ToString());
            cbFirstStart.Checked = RunPara.Instance.FirstStart;
            cbImmediatelyUpStove.Checked = Marking.ImmediatelyUpStove;
            cbImmediatelyDownStove.Checked = Marking.ImmediatelyDownStove;
            cbshieldProductCode.Checked = RunPara.Instance.ShieldProductCode;
            cbshieldTrayCode.Checked = RunPara.Instance.ShieldTrayCode;
            cbTraySolidify.Checked = RunPara.Instance.TraySolidify;

            //nudSupplyDelayTime.Value = (decimal)RunPara.Instance.SupplyDelayTime / 1000;
            nudQRCodeDelayTime.Value = (decimal)RunPara.Instance.QRCodeDelayTime / 1000;
            nudWishoutDiskWaitTime.Value = (decimal)RunPara.Instance.WishoutDiskWaitTime / 1000;
            nudCoolingTime.Value = (decimal)RunPara.Instance.CoolingTime / 1000;

            cbIsUseMesLock.Checked = RunPara.Instance.IsUseMesLock;
            cbShieldEntraceGuard.Checked = RunPara.Instance.ShieldEntraceGuard;
            cbShieldAAReady.Checked = RunPara.Instance.ShieldAAReady;
            cbShieldAAOK.Checked = RunPara.Instance.ShieldAAOK;
            cbShieldAANG.Checked = RunPara.Instance.ShieldAANG;
            cbShieldNGTray.Checked = RunPara.Instance.ShieldNGTray;
            cbShieldFixtureInduction.Checked = RunPara.Instance.ShieldFixtureInduction;

            cbStove1Shield.Checked = RunPara.Instance.Stove1Shield;
            cbStove2Shield.Checked = RunPara.Instance.Stove2Shield;
            cbStove3Shield.Checked = RunPara.Instance.Stove3Shield;
            cbStove4Shield.Checked = RunPara.Instance.Stove4Shield;
            cbStove5Shield.Checked = RunPara.Instance.Stove5Shield;
            cbStove6Shield.Checked = RunPara.Instance.Stove6Shield;
        }

        private void cbShieldAAOK_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShieldAAOK.Checked)
            {
                cbShieldAANG.Checked = false;
            }
        }

        private void cbShieldAANG_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShieldAANG.Checked)
            {
                cbShieldAAOK.Checked = false;
            }
        }

        private void btnSaveParam_Click(object sender, EventArgs e)
        {
            RunPara.Instance.RotatingDisk = (int)numRotatingDisk.Value;
            RunPara.Instance.FirstStart = cbFirstStart.Checked;
            Marking.ImmediatelyUpStove = cbImmediatelyUpStove.Checked;
            Marking.ImmediatelyDownStove = cbImmediatelyDownStove.Checked;

            //RunPara.Instance.SupplyDelayTime = Convert.ToInt32(Convert.ToDouble(nudSupplyDelayTime.Value) * 1000);
            RunPara.Instance.QRCodeDelayTime = Convert.ToInt32(Convert.ToDouble(nudQRCodeDelayTime.Value) * 1000);
            RunPara.Instance.WishoutDiskWaitTime = Convert.ToInt32(Convert.ToDouble(nudWishoutDiskWaitTime.Value) * 1000);
            RunPara.Instance.CoolingTime = Convert.ToInt32(Convert.ToDouble(nudCoolingTime.Value) * 1000);
            RunPara.Instance.IsUseMesLock = cbIsUseMesLock.Checked;
            RunPara.Instance.ShieldEntraceGuard = cbShieldEntraceGuard.Checked;


            if (RunPara.Instance.ShieldEntraceGuard)
            {
                IoPoints.I2DO29.Value = false;

            }
            else

                IoPoints.I2DO29.Value = true;


            RunPara.Instance.ShieldAAReady = cbShieldAAReady.Checked;
            RunPara.Instance.ShieldAAOK = cbShieldAAOK.Checked;
            RunPara.Instance.ShieldAANG = cbShieldAANG.Checked;
            RunPara.Instance.ShieldFixtureInduction = cbShieldFixtureInduction.Checked;
            RunPara.Instance.ShieldProductCode = cbshieldProductCode.Checked;
            RunPara.Instance.ShieldTrayCode = cbshieldTrayCode.Checked;
            RunPara.Instance.TraySolidify = cbTraySolidify.Checked;
            RunPara.Instance.ShieldNGTray = cbShieldNGTray.Checked;

            Marking.StoveProductClear[0] = cbStove1ProductClear.Checked;
            Marking.StoveProductClear[1] = cbStove2ProductClear.Checked;
            Marking.StoveProductClear[2] = cbStove3ProductClear.Checked;
            Marking.StoveProductClear[3] = cbStove4ProductClear.Checked;
            Marking.StoveProductClear[4] = cbStove5ProductClear.Checked;
            Marking.StoveProductClear[5] = cbStove6ProductClear.Checked;
            Marking.TaryProductClear = cbTaryProductClear.Checked;

            RunPara.Instance.Stove1Shield = cbStove1Shield.Checked;
            RunPara.Instance.Stove2Shield = cbStove2Shield.Checked;
            RunPara.Instance.Stove3Shield = cbStove3Shield.Checked;
            RunPara.Instance.Stove4Shield = cbStove4Shield.Checked;
            RunPara.Instance.Stove5Shield = cbStove5Shield.Checked;
            RunPara.Instance.Stove6Shield = cbStove6Shield.Checked;

            SerializerManager<RunPara>.Instance.Save(Product.Instance.ProductDataFile, RunPara.Instance);

            Thread.Sleep(100);
            this.Close();
        }
    }
}
