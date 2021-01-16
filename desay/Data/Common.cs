using System;
using SV.Core.Common;
using sv_Interlocking_Main;
using System.Data.SqlClient;
using System.Data;
namespace Desay
{
    class Common
    {

        #region 参数    
        public static SvMesHelper mes;

        //路径参数
        public static string OiIniPath = FileHelper.AppPath + @"Config\OI_Production_Info_Config.ini";
        public static string CommonIniPath;
        public static string ModelIniPath;
        /// <summary>
        /// MesTxt用于数据采集上传的TXT所在的文件夹路径
        /// </summary>
        public static string MesTxtSaveDirPath;
        public static string MesCsvSaveDirPath;
        public static string ImageSaveDirPath;
        /// <summary>
        /// MesTxt中用于上传的测试项所在的文件路径
        /// </summary>
        public static string MesTestItemsFilePath = FileHelper.AppPath + @"Config\MesData.txt";

        //设备参数
        public static string DMMTestAddress;
        public static bool IsAdmin;
        public static string Line;
        public static Object obj { get; set; } = new object();

        //机型参数
        public static string ModelSerial;
        public static string SN_Prefix;
        public static int SN_Length;
        public static string TestResult;
        public static string SersorFilePath;
        public static string[] AllA2Cs;
        public static int PassNum;
        public static int FailNum;
        //软件参数
        public static bool IsLogShowClassAndFunctionName { get; set; } = true;
        public static INIFile INIFile = new INIFile();
        public static bool IsToPutImage { get; set; } = false;

        #endregion

        #region 常规与机型配置读写
        public static void ReadCommonIniFile()
        {
            try
            {
                string computerName = Environment.MachineName;
                Line = Common.INIFile.ReadValue(OiIniPath, computerName, "Line", "");
                CommonIniPath = FileHelper.AppPath + @"Config\" + Common.INIFile.ReadValue(OiIniPath, computerName, "CommonIniPath", "");
                ModelIniPath = FileHelper.AppPath + @"Config\" + Common.INIFile.ReadValue(OiIniPath, computerName, "ModelIniPath", "");
                mes = new SvMesHelper(CommonIniPath, MesTestItemsFilePath);
                CommonIniPath = FileHelper.AppPath + @"Config\";
                ModelIniPath = FileHelper.AppPath + @"Config\";

                DMMTestAddress = Common.INIFile.ReadValue(CommonIniPath + "MesConfig.ini", "DMMTest", "Address");
                MesTxtSaveDirPath = Common.INIFile.ReadValue(CommonIniPath + "MesConfig.ini", "MesData", "MesTxtSaveDirPath");
                MesCsvSaveDirPath = Common.INIFile.ReadValue(CommonIniPath+ "MesConfig.ini", "MesData", "MesCsvSaveDirPath");
                ImageSaveDirPath = Common.INIFile.ReadValue(CommonIniPath + "MesConfig.ini", "Common", "ImageSaveDirPath");

               //mes.EvData.BatchSerialNumber = "SV";//用于区分供应商软件与自主软件
               //mes.EvData.StationID = Common.INIFile.ReadValue(CommonIniPath, "MesData", "StationID");
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region 生成报告
        /// <summary>
        /// 生产报告
        /// </summary>
        /// <param name="ProductDate">当前产品测试数据</param>
        /// <param name="cameraTest">相机设定参数数据</param>
        public static void SendTestDataToME_MSTR(string sn, StoveData d1, StoveData d2, StoveData d3)
        {
            int order = 0;
            mes.add_ME_MSTR_NumericTest(ref order, d1.min, d1.max, d1.data);
            mes.add_ME_MSTR_NumericTest(ref order, d2.min, d2.max, d2.data);
            mes.add_ME_MSTR_NumericTest(ref order, d3.min, d3.max, d3.data);
        }

        public static bool Test_WriteMesTxtAndCsvFile(string sn,string tool3,string   scantime  , bool ifok, StoveData d1, StoveData d2, StoveData d3)
        {
            lock (obj)
            {
                bool result = false;
                try
                {
                    mes.EvData.BatchSerialNumber = tool3;//用于区分供应商软件与自主软件
                   mes.EvData.DeviceA2C = sn.Substring(0, 12);//9.22临时修改
                    mes.EvData.SerialNumber = sn;
                  //  mes.EvData.ExecutionTime = d1.data.ToString();
                    mes.EvData.ExecutionTime = scantime;



                    if (ifok)
                    mes.EvData.TestStatus = "Passed";
                    else
                        mes.EvData.TestStatus = "Failed";

                    SendTestDataToME_MSTR(sn,d1,d2,d3);
                    bool isWriteMesTxtSucceed = mes.WriteMesTxtToFile($"{MesTxtSaveDirPath}{mes.EvData.DeviceA2C}_{mes.EvData.SerialNumber}_{DateTime.Now.ToString("yyMMddHHmmss")}_{mes.EvData.TestStatus}.txt");
                    //bool isWriteCsvSucceed = mes.AppendTestValuesRoCsvFile($"{MesCsvSaveDirPath}{mes.EvData.ModelName.Replace("/", "-")}_Camera Test_{DateTime.Now.ToString("yyMMdd")}.csv");
                    if (isWriteMesTxtSucceed) { LogHelper.Info("MES写入成功!"); }
                    else { LogHelper.Info("MES写入失败!"); }
                }
                catch (Exception ex)
                {
                    LogHelper.Info("写入失败:" + ex.ToString());
                }
                return result;
            }
        }

        public static bool Test_ScanSN(bool IsLock, string sn)
        {
            bool Result = true;
            string mesInfo = $"{Common.mes.EvData.DB_Password},{Common.mes.EvData.DB_User},{Common.mes.EvData.DatabaseName},{Common.mes.EvData.ServerName},{sn},{Common.mes.EvData.StationID},{Common.mes.EvData.LineGroup},{Common.mes.EvData.LoginName},True,False,False,5";

            if (IsLock)
            {
                try
                {
                    Result = SNStatus(sn, Common.mes.EvData.StationID).ToUpper() == "PASSED";
                    //Result = Common.mes.InterlockingTask(sn, Common.mes.EvData.LoginName);
                    //Result = sv_Interlocking_Main_Class.SV_Interlocking_Main(mesInfo)==0?true:false;
                }
                catch (Exception ex)
                {
                    Result = false;
                    LogHelper.Error("SV_Interlocking_Main Error,false");
                }
            }
              
            return Result;
        }



        //public static bool TestMSA_WriteMesTxtAndCsvFile(Product Product, WirteResultData WirteResultData, BlackResultData BlackResultData, string productSn, CameraHDTest cameraTest, double current, string Version, string PATH, string SenserID)
        //{

        //    lock (obj)
        //    {

        //        bool result = false;
        //        try
        //        {

        //            mes.EvData.SerialNumber = productSn;
        //            bool CurrentTestResult = current >= cameraTest.CurrentTest_Limit.Split('|')[0].ToDouble() &&
        //                    current <= cameraTest.CurrentTest_Limit.Split('|')[1].ToDouble(); //电流结果
        //            bool ix = Product.Colorresulr && Product.DFOVresulr && Product.OCresulr
        //                && Product.Grayresulr && Product.SFRresulr;  //图卡结果
        //            bool WirteTestResult = WirteResultData.WBresulr && WirteResultData.Darkresulr
        //                && WirteResultData.GrayAreaIResult && WirteResultData.GrayCountIResult;  //白板结果
        //            bool BlackTestResult = BlackResultData.Lightresulr &&
        //               Version == cameraTest.VersionLimit;  //黑板结果
        //            mes.EvData.TestStatus = CurrentTestResult && ix && WirteTestResult && BlackTestResult ? "Pass" : "Fail";
        //            SendTestDataToME_MSTR(Product, WirteResultData, BlackResultData, cameraTest, current, Version, SenserID);
        //            bool isWriteMesTxtSucceed = mes.WriteMesTxtToFile($"{PATH}{mes.EvData.DeviceA2C}_{mes.EvData.SerialNumber}_{DateTime.Now.ToString("yyMMddHHmmss")}_{mes.EvData.TestStatus}.txt");
        //            bool isWriteCsvSucceed = mes.AppendTestValuesRoCsvFile($"{PATH}{mes.EvData.ModelName.Replace("/", "-")}_Camera Test_{DateTime.Now.ToString("yyMMdd")}.csv");
        //            System.ToolKit.LogHelper.Info("写入ok:");
        //            result = isWriteCsvSucceed && isWriteMesTxtSucceed;
        //        }
        //        catch (Exception ex)
        //        {
        //            System.ToolKit.LogHelper.Info("写入失败:" + ex.ToString());

        //        }
        //        return result;
        //    }


        //}


        #endregion

        public static string SNStatus(string SN, string frontStationID)
        {
            string strResult = "Error";
            using (SqlConnection conn_HZHE015A = new SqlConnection())
            {
                string constr = "Server=" + Common.mes.EvData.ServerName + ";" + "user=" + Common.mes.EvData.DB_User + ";" + "pwd=" + Common.mes.EvData.DB_Password + ";" + "database=" + Common.mes.EvData.DatabaseName;
                conn_HZHE015A.ConnectionString = constr;
                //ca6 
                string snOrder = "SELECT TOP 1  UUT_STATUS  from UUT_RESULT where UUT_SERIAL_NUMBER = '" + SN + "' AND STATION_ID = '" + "MED-AA-0005" + "' order by START_DATE_TIME desc";//查询数据库数据结果 //frontStationID MED-MSCREW-0017

                //ca7
                //string snOrder = "SELECT TOP 1  UUT_STATUS  from UUT_RESULT where UUT_SERIAL_NUMBER = '" + SN + "' AND STATION_ID = '" + "MED-AA-0006" + "' order by START_DATE_TIME desc";//查询数据库数据结果 //frontStationID MED-MSCREW-0017
                DataTable status = ReadOrderData(constr, snOrder);
                try
                {
                    strResult = status.Rows[0][0].ToString();
                    //LogHelper.Info(SN + ",SN查询结果：" + strResult);
                }
                catch (Exception ex)
                {

                    //LogHelper.Error(SN + ",查询异常" + ex);
                }

            }
            return strResult;
        }
        /// <summary>
        /// 数据库查询Conm
        /// </summary>
        /// 
        private static DataTable ReadOrderData(string connectionString, string queryString)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    data.Load(reader);
                    reader.Close();

                }
                catch (Exception ex)
                {
                    //LogHelper.Error("查询异常" + ex);
                }
                return data;

            }
        }
    }


    public class StoveData
    {
        public double min;
        public double max;
        public double data;
    }
}
