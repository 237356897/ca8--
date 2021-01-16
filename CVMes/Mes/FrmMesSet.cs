using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CVMes
{
    public partial class FrmMesSet : Form
    {
        //private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        string strMesFile;//Mes文件名
        string strConfigfile;//配置文件路径
        Mes m ;
        StringBuilder strEV =  new StringBuilder();
        StringBuilder strMe = new StringBuilder();
        string strSection = "MesData";
        public FrmMesSet(string filePath)
        {
            strConfigfile = filePath;
            InitializeComponent();
        }
        public void init()
        {
            strMesFile = IniOperate.INIGetStringValue(strConfigfile, strSection, "MesFilePath", "");
            txtSavePath.Text = IniOperate.INIGetStringValue(strConfigfile, strSection, "MesSaveFilePath", "");
            txtShowFile.Text = strMesFile;
            cboxSN.Checked = Convert.ToBoolean(IniOperate.INIGetStringValue(strConfigfile,strSection,"UnCheckSN","True"));
            writeInfo("初始化Mes设置参数成功！路径为:"+strMesFile);
            initMesParam();
        }
        //初始化Mes数据
        private void initMesParam()
        {
            m = new Mes(strMesFile);
            EV_MSTR ev = m.get_EV_MSTR();
            if (ev != null)
            {
                strEV.AppendLine();
                strEV.Append(string.Format("{0}", ev.UUT_Order + new string(' ', 11 - Encoding.GetEncoding("gb2312").GetBytes(ev.UUT_Order).Length)));
                strEV.Append(string.Format("{0}", ev.UUT_SOURCE + new string(' ', 12 - Encoding.GetEncoding("gb2312").GetBytes(ev.UUT_SOURCE).Length)));
                strEV.Append(string.Format("{0}", ev.DeviceA2C + new string(' ', 15 - Encoding.GetEncoding("gb2312").GetBytes(ev.DeviceA2C).Length)));
                strEV.Append(string.Format("{0}", ev.EquipmentFunction + new string(' ', 22 - Encoding.GetEncoding("gb2312").GetBytes(ev.EquipmentFunction).Length)));
                strEV.Append(string.Format("{0}", ev.StationName + new string(' ', 22 - Encoding.GetEncoding("gb2312").GetBytes(ev.StationName).Length)));
                strEV.Append(string.Format("{0}", ev.TestStandName + new string(' ', 22 - Encoding.GetEncoding("gb2312").GetBytes(ev.TestStandName).Length)));
                strEV.Append(string.Format("{0}", ev.LoginName + new string(' ', 22 - Encoding.GetEncoding("gb2312").GetBytes(ev.LoginName).Length)));
                strEV.Append(string.Format("{0}", ev.TestSocket + new string(' ', 12 - Encoding.GetEncoding("gb2312").GetBytes(ev.TestSocket).Length)));
                lblLinkStr.Text = ev.DB_Password + "," + ev.DB_User + "," + ev.DatabaseName + "," + ev.ServerName + ", ," + ev.StationID + "," + 
                   ev.LineGroup + "," + ev.SW_User + "," + ev.Debug + "," + ev.ShowWindow + "," + ev.PassForNoDB + "," + ev.Function;
            }
           
            ME_MSTR[] arr = m.get_ME_MSTR();
            if (arr != null)
            {                
                foreach (ME_MSTR me in arr)
                {
                    strMe.Append(string.Format("{0}", me.Step_Order + new string(' ', 12 - Encoding.GetEncoding("gb2312").GetBytes(me.Step_Order).Length)));
                    strMe.Append(string.Format("{0}", me.Step_Source + new string(' ', 13 - Encoding.GetEncoding("gb2312").GetBytes(me.Step_Source).Length)));
                    strMe.Append(string.Format("{0}", me.StepName + new string(' ',22 - Encoding.GetEncoding("gb2312").GetBytes(me.StepName).Length)));
                    strMe.Append(string.Format("{0}", me.StepType + new string(' ', 22 - Encoding.GetEncoding("gb2312").GetBytes(me.StepType).Length)));
                    strMe.Append(string.Format("{0}", me.Units + new string(' ', 7 - Encoding.GetEncoding("gb2312").GetBytes(me.Units).Length)));
                    strMe.Append(string.Format("{0}", me.Comp + new string(' ', 10 - Encoding.GetEncoding("gb2312").GetBytes(me.Comp).Length)));
                    strMe.Append(string.Format("{0}", me.limitLow + new string(' ', 10 - Encoding.GetEncoding("gb2312").GetBytes(me.limitLow).Length)));
                    strMe.Append(string.Format("{0}", me.limitHigh + new string(' ', 11 - Encoding.GetEncoding("gb2312").GetBytes(me.limitHigh).Length)));
                    strMe.Append(string.Format("{0}", me.limits_String + new string(' ', 15 - Encoding.GetEncoding("gb2312").GetBytes(me.limits_String).Length)));
                    strMe.Append("\r\n");

                }
            }
            lblShowEv.Text = strEV.ToString();
            txtShowMe.Text = strMe.ToString();
           
            
        }
        public void initStr()
        {
            strEV.Remove(0, strEV.Length);
            strMe.Remove(0, strMe.Length);
            strEV.Append(string.Format("{0,-11}", "UUT_Order"));
            strEV.Append(string.Format("{0,-12}", "UUT_SOURCE"));
            strEV.Append(string.Format("{0,-15}", "DeviceA2C"));
            strEV.Append(string.Format("{0,-22}", "EquipFun"));
            strEV.Append(string.Format("{0,-22}", "StationName"));
            strEV.Append(string.Format("{0,-22}", "StandName"));
            strEV.Append(string.Format("{0,-22}", "LoginName"));
            strEV.Append(string.Format("{0,-12}", "TestSocket"));
            //strMe.Append("\r\n");


            strMe.Append(string.Format("{0,-12}", "Step_Order"));
            strMe.Append(string.Format("{0,-13}", "Step_Source"));
            strMe.Append(string.Format("{0,-22}", "StepName"));
            strMe.Append(string.Format("{0,-22}", "StepType"));
            strMe.Append(string.Format("{0,-7}", "Units"));
            strMe.Append(string.Format("{0,-10}", "Comp"));
            strMe.Append(string.Format("{0,-10}", "limitLow"));
            strMe.Append(string.Format("{0,-11}", "limitHigh"));
            strMe.Append(string.Format("{0,-15}", "limits_String"));
            strMe.Append("\r\n");
            lblShowEv.Text = strEV.ToString();
            txtShowMe.Text = strMe.ToString();

        }
        private void btnLoadConfig_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.Filter = "ini files(*.ini)|*.ini";
            openFileDlg.RestoreDirectory = true;
            //openFileDlg.InitialDirectory = Application.StartupPath;
            if (openFileDlg.ShowDialog()==DialogResult.OK)
            {
                strMesFile = openFileDlg.FileName;
                txtShowFile.Text = strMesFile;
                writeInfo("选择的文件为：" + strMesFile);

                initStr();
                initMesParam();
            }          
        }       

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                IniOperate.INIWriteValue(strConfigfile, strSection, "MesFilePath", strMesFile);
                IniOperate.INIWriteValue(strConfigfile, strSection, "MesSaveFilePath", txtSavePath.Text);
                IniOperate.INIWriteValue(strConfigfile, strSection, "UnCheckSN", cboxSN.Checked.ToString());
                writeInfo("Mes保存设置参数成功");
                MessageBox.Show("Mes保存设置参数成功");
            }
            catch (System.Exception ex)
            {
                writeDebug("Mes保存设置参数出错", ex);
                MessageBox.Show("Mes保存设置参数出错");
            }
        }
       

        private void FrmMesSet_Load(object sender, EventArgs e)
        {
            initStr();          
            init();
        }
        

        private static void writeInfo(string info)
        {
            //log.Info(info);
            //Mes.writeInfo(info);
        }
        private static void writeDebug(string info, Exception e)
        {
            //log.Debug(info, e);
            //Mes.writeDebug(info, e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMesExample_Click(object sender, EventArgs e)
        {
            if ((m == null) ||(m.get_EV_MSTR() == null) ||(m.get_ME_MSTR() ==null) )
            {
                writeInfo("配置文件不存在或配置文件格式不正确!");
                MessageBox.Show("配置文件不存在或配置文件格式不正确!");
                return;
            }
            m.add_EV_MSTR_Param(m.get_EV_MSTR().DeviceA2C +"1234567", DateTime.Now.ToString("yyMMddHHmmss"), "500", "");
            foreach (ME_MSTR me in m.get_ME_MSTR())
            {
                if (me.StepType.Equals("PassFailTest"))
                {
                    m.add_ME_MSTR_PassFailTest(Convert.ToInt32(me.Step_Order), 0, "", DateTime.Now.ToString("yyMMddHHmmss"), 50, "Passed");
                    continue;
                }
                if (me.StepType.Equals("NumericLimitTest"))
                {
                    m.add_ME_MSTR_NumericTest(Convert.ToInt32(me.Step_Order), 0, "", DateTime.Now.ToString("yyMMddHHmmss"), "50", 30);
                    continue;
                }
                if (me.StepType.Equals("StringValueTest"))
                {
                    m.add_ME_MSTR_StringValueTest(Convert.ToInt32(me.Step_Order), 0, "", DateTime.Now.ToString("yyMMddhhmmss"), 50, "testString");
                    continue;
                }
            }
          
            if (m.WriteMesToFile(Application.StartupPath + "\\testMes.txt"))
            {
                System.Diagnostics.Process.Start("notepad.exe", Application.StartupPath + "\\testMes.txt");
            }else
            {
                MessageBox.Show("写入失败!");
            }                     
        }

        private void btnSavePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!fbd.SelectedPath.EndsWith("\\"))
                    txtSavePath.Text = fbd.SelectedPath + "\\";
                else
                    txtSavePath.Text = fbd.SelectedPath;
            }
        }

        private void btnLink_Click(object sender, EventArgs e)
        {
            lblLinkResult.Text = "";
            if(txtSN.ToString().Trim().Equals(""))
            {
                MessageBox.Show("SN不能为空");
                return;
            }
            m.CheckSN(txtSN.Text.Trim());
           // lblLinkResult.Text = m.CheckSN(txtSN.Text.Trim()).ToString();
        }

        //刷新
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            initStr();
            init();
        }
    }
}
