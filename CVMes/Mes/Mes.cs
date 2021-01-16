using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;

namespace CVMes
{
    /// <summary>
    /// MES数据采集类，包含一个EV_MSTR对象和ME_MSTR列表，列表的长度由要采集的参数个数决定 
    /// </summary>
    public class Mes
    {
        EV_MSTR evData ;
        ME_MSTR[] arr_ME_MSTR ;
        //private static log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// 获取EV_MSTR对象
        /// </summary>
        /// <returns></returns>
        public EV_MSTR get_EV_MSTR()
        {
            return evData;
        }
        /// <summary>
        /// 获取ME_MSTR对象数组
        /// </summary>
        /// <returns></returns>
        public ME_MSTR[] get_ME_MSTR()
        {
            return arr_ME_MSTR;
        }
        /// <summary>
        /// 将规定格式的配置文件输入以初始化对象
        /// </summary>
        /// <param name="strConfgFile">输入的配置文件名</param>
        public Mes(string strConfgFile)
        {
            set_EV_MSTR_Config(strConfgFile);
            set_ME_MSTR_Config(strConfgFile);
        }
        /// <summary>
        /// 设置配置文件
        /// </summary>
        /// <param name="strConfgFile">输入的配置文件名</param>
        /// <returns>如果更改成功则返回true,否则返回false</returns>
        public bool setConfigFile(string strConfgFile)
        {
            return set_ME_MSTR_Config(strConfgFile) && set_EV_MSTR_Config(strConfgFile);
        }
        /// <summary>
        /// 初始化EV_MSTR表的数据项，如果有对应的KEY则读取初始化的值，如果没有则此项的值为空
        /// </summary>
        /// <param name="strFile">指定初始化时读取的文件</param>
        /// <param name="index">节点在文件中的位置序列号(即第几个节点)，以0开始计数</param>
        /// <returns>如果没异常则返回true，否则返回false</returns>
       private bool set_EV_MSTR_Config(string strFile)
       {
           try
           {
               evData = new EV_MSTR();

               string section = IniOperate.INIGetAllSectionNames(strFile)[0];
               evData.UUT_Order = IniOperate.INIGetStringValue(strFile, section, "UUT_Order", "").Trim();
               evData.UUT_SOURCE = IniOperate.INIGetStringValue(strFile, section, "UUT_SOURCE", "").Trim();
               evData.DeviceA2C = IniOperate.INIGetStringValue(strFile, section, "DeviceA2C", "").Trim();
               evData.ScanLength = IniOperate.INIGetStringValue(strFile, section, "ScanLength", "").Trim();
               evData.ProgramNumber = IniOperate.INIGetStringValue(strFile, section, "ProgramNumber", "").Trim();
               evData.EquipmentFunction = IniOperate.INIGetStringValue(strFile, section, "EquipmentFunction", "").Trim();
               evData.SerialNumber = IniOperate.INIGetStringValue(strFile, section, "SerialNumber", "").Trim();
               evData.StationName = IniOperate.INIGetStringValue(strFile, section, "StationName", "").Trim();
               evData.ProductTime = IniOperate.INIGetStringValue(strFile, section, "ProductTime", "").Trim();
               evData.TestStandName = IniOperate.INIGetStringValue(strFile, section, "TestStandName", "").Trim();
               evData.LoginName = IniOperate.INIGetStringValue(strFile, section, "LoginName", "").Trim();
               evData.ExecutionTime = IniOperate.INIGetStringValue(strFile, section, "TestSocket", "").Trim();
               evData.BatchSerialNumber = IniOperate.INIGetStringValue(strFile, section, "BatchSerialNumber", "").Trim();
               evData.TestStatus = IniOperate.INIGetStringValue(strFile, section, "TestStatus", "").Trim();
               //数据库参数
               evData.DB_Password = IniOperate.INIGetStringValue(strFile, section, "DB_Password", "PEDTEST@sql2005").Trim();
               evData.DB_User = IniOperate.INIGetStringValue(strFile, section, "DB_User", "TE_IP").Trim();
               evData.DatabaseName = IniOperate.INIGetStringValue(strFile, section, "DatabaseName", "TKP_EPS_ECU").Trim();
               evData.ServerName = IniOperate.INIGetStringValue(strFile, section, "ServerName", "HZHE015A").Trim();
               evData.StationID = IniOperate.INIGetStringValue(strFile, section, "StationID", "EPS_FT").Trim();
               evData.LineGroup = IniOperate.INIGetStringValue(strFile, section, "LineGroup", "1").Trim();
               evData.SW_User = IniOperate.INIGetStringValue(strFile, section, "SW_User", "").Trim();
               evData.Debug = IniOperate.INIGetStringValue(strFile, section, "Debug", "False").Trim();
               evData.ShowWindow = IniOperate.INIGetStringValue(strFile, section, "ShowWindow", "False").Trim();
               evData.PassForNoDB = IniOperate.INIGetStringValue(strFile, section, "PassForNoDB", "False").Trim();
               evData.Function = IniOperate.INIGetStringValue(strFile, section, "Function", "5").Trim();
               return true;
           }
           catch 
           {
               evData = null;
               return false; 
           }
              
       }
        /// <summary>
        /// 初始化ME_MSTR表格的数据
        /// </summary>
        /// <param name="strFile">指定初始化时读取的文件</param>
        /// <param name="index">第一项参数在文件中的节点序列号，以0开始计数</param>
        /// <param name="count">每个工位中需要采集的参数个数</param>
       /// <returns>如果没异常则返回true，否则返回false</returns>
       private bool set_ME_MSTR_Config(string strFile)
       {
           try
           {
               string[] strSections = IniOperate.INIGetAllSectionNames(strFile);
               arr_ME_MSTR = new ME_MSTR[strSections.Length-1];
              
               for (int i = 0; i < strSections.Length-1; i++)
               {
                   ME_MSTR me = new ME_MSTR();
                   me.Step_Order = IniOperate.INIGetStringValue(strFile, strSections[i + 1], "Step_Order", "1").Trim();
                   me.Step_Source = IniOperate.INIGetStringValue(strFile, strSections[i + 1], "Step_Source", "ME").Trim();
                   me.ProductionTime = IniOperate.INIGetStringValue(strFile, strSections[i + 1], "ProductionTime", "").Trim();
                   me.ErrorCode = IniOperate.INIGetStringValue(strFile, strSections[i + 1], "ErrorCode", "0").Trim();
                   me.ErrorMessage = IniOperate.INIGetStringValue(strFile, strSections[i + 1], "ErrorMessage", "").Trim();
                   me.TotalTime = IniOperate.INIGetStringValue(strFile, strSections[i + 1], "TotalTime", "").Trim();
                   me.StepName = IniOperate.INIGetStringValue(strFile, strSections[i + 1], "StepName", "").Trim();
                   me.Step_Status = IniOperate.INIGetStringValue(strFile, strSections[i + 1], "Step_Status", "").Trim();
                   me.StepType = IniOperate.INIGetStringValue(strFile, strSections[i + 1], "StepType", "").Trim();
                   me.Step_PassFail = IniOperate.INIGetStringValue(strFile, strSections[i + 1], "Step_PassFail", "").Trim();
                   me.Units = IniOperate.INIGetStringValue(strFile, strSections[i + 1], "Units", "").Trim();
                   me.Comp = IniOperate.INIGetStringValue(strFile, strSections[i + 1], "Comp", "").Trim();
                   me.LimitStep_Data = IniOperate.INIGetStringValue(strFile, strSections[i + 1], "LimitStep_Data", "0").Trim();
                   me.limitLow = IniOperate.INIGetStringValue(strFile, strSections[i + 1], "limitLow", "0").Trim();
                   me.limitHigh = IniOperate.INIGetStringValue(strFile, strSections[i + 1], "limitHigh", "0").Trim();
                   me.Result_String = IniOperate.INIGetStringValue(strFile, strSections[i + 1], "Result_String", "").Trim();
                   me.limits_String = IniOperate.INIGetStringValue(strFile, strSections[i + 1], "limits_String", "").Trim();
                   arr_ME_MSTR[i] = me;
               }
               return true;
           }
           catch {
               arr_ME_MSTR = null;
               return false;
           }
          
       }
        /// <summary>
        /// 添加一项数值参数到ME_MSTR表中
        /// </summary>
        /// <param name="stepOrder">参数在工位采集数据中的序号，按此序号查找存放的位置</param>
        /// <param name="errorCode">错误代码，如果没有出错，则设为0</param>
        /// <param name="errorMessage">如果errorCode不为0，则输入错误信息；如果errorCode为0,则为空</param>
        /// <param name="startOfTime">此参数所在动作开始工作的时间,格式yyMMddHHmmSS</param>
        /// <param name="toalTime">采集参数的动作所花的时间,单位ms</param>
        /// <param name="data">参数的值</param>
        /// <returns>添加成功则返true,否则返回false</returns>
       public bool add_ME_MSTR_NumericTest(int stepOrder,int errorCode,string errorMessage , string startOfTime,string toalTime,double data)
       {
           if (arr_ME_MSTR == null)
           {
               writeInfo("未初始化对象,添加NumericLimitTest项失败!");
               return false;
           }
           try
           {
               int len = arr_ME_MSTR.Length;

               ME_MSTR me = null;
               int me_Order = -1;
               //按序号查找要插入的位置，如果没找到则返回false
               for (int i = 0; i < len; i++)
               {

                   if (arr_ME_MSTR[i].Step_Order.Equals(stepOrder.ToString()))
                   {
                       me = arr_ME_MSTR[i];
                       me_Order = i;
                       break;
                   }
                   if (i == len - 1)
                   {
                       return false;
                   }

               }
               //开始插入数据
               if (errorCode == 0)
               {
                  
                   me.LimitStep_Data = data.ToString();
                   bool result = CompareData(me.Comp, data, Convert.ToDouble(me.limitLow), Convert.ToDouble(me.limitHigh), ref me.ErrorCode, ref me.ErrorMessage);
                  
                   if (result)
                       me.Step_Status = "Passed";
                   else
                       me.Step_Status = "Failed";
               }
               else
               {
                   me.ErrorCode = errorCode.ToString();
                   me.ErrorMessage = errorMessage;
                   me.LimitStep_Data = "0";
                   me.Step_Status = "Error";
               }
               me.ProductionTime = startOfTime;
               me.TotalTime = toalTime;
               arr_ME_MSTR[me_Order] = me;
               writeInfo("添加NumericTest成功");
               return true;
           }
           catch(Exception ex) {
               writeDebug("添加NumericTest项异常", ex);
               return false;           
           }
           
       }

       private bool CompareData(string comp, double data, double low, double high, ref string errorcode, ref string errorMessage)
       {
           if (comp.Equals(">=<="))
           {

               if ((data >= low) && (data <= high))
               {
                   writeInfo("NumericTest比较数据成功");
                   errorcode = "0";
                   errorMessage = "";
                   return true;

               }
               else if (data > high)
               {
                   errorcode = "1";
                   errorMessage = "Out of Upper Limit";
                   writeInfo("NumericTest比较数据超出上限");
                   return false;

               }
               else
               {
                   errorcode = "1";
                   errorMessage = "Out of Lower Limit";
                   writeInfo("NumericTest比较数据超出下限");
                   return false;
               }
           }
           else
           {
               errorcode = "2";
               errorMessage = "Check NumericValue Failed";
               writeInfo("NumericTest比较数据失败");
               return false;
           }
       
       }
     /// <summary>
       /// 添加一项PassFail参数到ME_MSTR表中
     /// </summary>
     /// <param name="stepOrder">参数在工位采集数据中的序号，按此序号查找存放的位置</param>
     /// <param name="errorCode">错误代码，如果没有出错，则设为0</param>
     /// <param name="errorMessage">如果errorCode不为0，则输入错误信息；如果errorCode为0,则为空</param>
       /// <param name="startOfTime">此参数所在动作开始工作的时间,格式yymmddHHMMSS</param>
     /// <param name="toalTime">采集参数的动作所花的时间,单位ms</param>
     /// <param name="value">值，一般为Passed或Failed</param>
       /// <returns>添加成功则返true,否则返回false</returns>
        public bool add_ME_MSTR_PassFailTest(int stepOrder, int errorCode, string errorMessage, string startOfTime, int toalTime, string value)
       {
           if (arr_ME_MSTR==null)
           {
               writeInfo("未初始化对象,添加PassFailTest项失败!");
               return false;
           }
           try
           {
               int len = arr_ME_MSTR.Length;

               ME_MSTR me = null;
               int me_Order = -1;
               //按序号查找要插入的位置，如果没找到则返回false
               for (int i = 0; i < len; i++)
               {

                   if (arr_ME_MSTR[i].Step_Order.Equals(stepOrder.ToString()))
                   {
                       me = arr_ME_MSTR[i];
                       me_Order = i;
                       break;
                   }
                   if (i == len - 1)
                   {
                       return false;
                   }

               }
               //插入数据 
               if (errorCode == 0)
               {
                   //me.ErrorCode = errorCode.ToString();
                   if (value.Equals("Failed"))
                       me.ErrorCode = "1";
                   else
                       me.ErrorCode = "0";
                   me.ErrorMessage = value;
                   me.Step_PassFail = value;
                   me.Step_Status = value;
               }
               else
               {
                   me.ErrorCode = errorCode.ToString();
                   me.ErrorMessage = errorMessage;
                   me.Step_PassFail = value;
                   me.Step_Status = "Error";
               }
               me.ProductionTime = startOfTime;
               me.TotalTime = toalTime.ToString();
               arr_ME_MSTR[me_Order] = me;
               writeInfo("添加PasseFailTest项成功");
               return true;
           }
           catch(Exception ex)
           {
               writeDebug("添加PasseFailTest项异常", ex);
               return false;
           }

       }
        /// <summary>
        /// 添加一项字符串参数到ME_MSTR表中
        /// </summary>
        /// <param name="stepOrder">参数在工位采集数据中的序号，按此序号查找存放的位置</param>
        /// <param name="errorCode">错误代码，如果没有出错，则设为0</param>
        /// <param name="errorMessage">如果errorCode不为0，则输入错误信息；如果errorCode为0,则为空</param>
        /// <param name="startOfTime">此参数所在动作开始工作的时间,格式yymmddHHMMSS</param>
        /// <param name="toalTime">采集参数的动作所花的时间,单位ms</param>
        /// <param name="value">要比较的字符串的值</param>
        /// <returns>添加成功则返true,否则返回false</returns>
        public bool add_ME_MSTR_StringValueTest(int stepOrder, int errorCode, string errorMessage, string startOfTime, int toalTime, string value)
        {
            if (arr_ME_MSTR == null)
            {
                writeInfo("未初始化对象,添加StringValueTest项失败!");
                return false;
            }
            try
            {
                int len = arr_ME_MSTR.Length;

                ME_MSTR me = null;
                int me_Order = -1;
                //按序号查找要插入的位置，如果没找到则返回false
                for (int i = 0; i < len; i++)
                {

                    if (arr_ME_MSTR[i].Step_Order.Equals(stepOrder.ToString()))
                    {
                        me = arr_ME_MSTR[i];
                        me_Order = i;
                        break;
                    }
                    if (i == len - 1)
                    {
                        return false;
                    }

                }
                //插入数据 
                if (errorCode == 0)
                {
                    //me.ErrorCode = errorCode.ToString();
                    //me.ErrorMessage = "";
                    me.Result_String = value;
                    if (value.ToUpper().Equals(me.limits_String.ToUpper()))
                    {
                        me.Step_Status = "Passed";
                        me.ErrorCode = "0";
                        me.ErrorMessage = "";
                    }
                    else
                    {
                        me.Step_Status = "Failed";
                        me.ErrorMessage = "String Compare failed";
                        me.ErrorCode = "0";
                    }
                }
                else
                {
                    me.ErrorCode = errorCode.ToString();
                    me.ErrorMessage = errorMessage;
                    me.Result_String = value;
                    me.Step_Status = "Error";
                }
                me.ProductionTime = startOfTime;
                me.TotalTime = toalTime.ToString();
                arr_ME_MSTR[me_Order] = me;
                writeInfo("添加StringValueTest成功");
                return true;
            }
            catch(Exception ex)
            {
                writeDebug("添加StringValueTest异常", ex);
                return false;
            }

        
        }
       /// <summary>
       /// 添加工位检测完一个产品后的状态信息
       /// </summary>
       /// <param name="sn">产品的序列号</param>
        /// <param name="startOfTime">产品开始测试的时间，格式yymmddHHMMSS</param>
       /// <param name="excuteTime">产品执行检测的时间，单位S</param>
       /// <param name="batchSN">产品中绑定的其它的序列号，如果没有绑定则为空</param>
        public bool add_EV_MSTR_Param(string sn,string startOfTime,string excuteTime,string batchSN)
        {
            if (evData == null)
            {
                return false;
            }
            evData.SerialNumber = sn;
            evData.BatchSerialNumber = batchSN;
            evData.ExecutionTime = excuteTime;
            evData.ProductTime = startOfTime;
            return true;        
        }
             /// <summary>
             /// 添加员工工号
             /// </summary>
             /// <param name="StaffName"></param>
        public void add_EV_MSTR_Name(string StaffName)
        {
            if (evData != null)
            {
                evData.LoginName = StaffName;
            }           
                            
        }
        /// <summary>
        /// 生成Mes格式的字符串,如果未初始化则返回Error
        /// </summary>
        /// <returns>返回要写入文件的字符串</returns>
        public string ToMesString()
        {
          try
          {         
            if ((evData == null)||(arr_ME_MSTR == null))
            {
                writeInfo("成员对象为空,生成Mes数据失败");
                return "Error";
            }
            bool result = true;
            foreach (ME_MSTR me in arr_ME_MSTR)
            {
                result = result && me.Step_Status.Equals("Passed");
                
            }
            if (result)
            {
                evData.TestStatus = "Passed";
            }
            else
            {
                evData.TestStatus = "Failed";
            }

            StringBuilder strResult = new StringBuilder();
            strResult.AppendLine("UUT_Order|UUT_SOURCE|DeviceA2C|Equipment Function|SerialNumber|StationName||||ProductTime|TestStandName|Login Name|ExecutionTime|TestScoket|BatchSerialNumber|Error Code|Error Message||||||||||||||TestStatus|");
            strResult.AppendLine(evData.UUT_Order + "|" + evData.UUT_SOURCE + "|" + evData.DeviceA2C + "|" + evData.EquipmentFunction + "|" + evData.SerialNumber + "|"
                                + evData.StationName + "||||" + evData.ProductTime + "|" + evData.TestStandName + "|" + evData.LoginName + "|" + evData.ExecutionTime + "|"
                                + evData.TestSocket + "|" + evData.BatchSerialNumber + "||||||||||||||||" +evData.TestStatus + "|");
            strResult.AppendLine();
             strResult.AppendLine("Step_Order|Step_Source|ProductionTime|ErrorCode|ErrorMessage|TotalTime|StepName|Step_Status|ReportText|Num_Loops|Num_Passed|Num_Failed|StepGroup|StepType|Step_PassFail|Multiple_SubName|Units|Comp|Limit.Step_Data|Limit.Low|Limit.High|Result_String|Limits_String|");
                
            foreach (ME_MSTR me in arr_ME_MSTR)
            {
                strResult.AppendLine(me.Step_Order + "|" + me.Step_Source + "|" + me.ProductionTime + "|" + me.ErrorCode + "|" + me.ErrorMessage + "|" + me.TotalTime + "|" + me.StepName + "|" + me.Step_Status + "||1|1|0|Main|" 
                                    + me.StepType + "|" + me.Step_PassFail + "||" + me.Units + "|" + me.Comp + "|" + me.LimitStep_Data + "|" + me.limitLow + "|" + me.limitHigh + "|" + me.Result_String + "|" + me.limits_String + "|");
            }
            writeInfo("生成Mes数据成功");
            return strResult.ToString();

          }
          catch (System.Exception ex)
          {
              writeDebug("生成Mes数据失败", ex);
              return "Error";
          }                 
        }
        /// <summary>
        /// 将Mes数据写入到文件
        /// </summary>
        /// <param name="strPath">文件的路径/param>
        /// <returns>如果写入成功返回true,否则返回false</returns>
        public bool WriteMesToFile(String strPath)
        {
            string strMes = ToMesString();
            if (!strMes.Equals("Error"))
            {
                StreamWriter sw;
                try
                {
                    string strFile = "";
                    if (strPath.Trim().EndsWith(".txt"))
                    {
                        strFile = strPath;
                    }
                    else
                    {
                        strFile = strPath + evData.DeviceA2C + "_" + evData.SerialNumber + "_" + DateTime.Now.ToString("yyMMddHHmmss") + "_" + evData.TestStatus + ".txt";
                    }
                    if (!File.Exists(strFile))
                    {
                        FileStream fis = File.Create(strFile);
                        fis.Close();
                        fis.Dispose();
                    }
                    sw = new StreamWriter(strFile);
                    sw.Write(strMes);
                    sw.Close();
                    sw.Dispose();
                    writeInfo("写入Mes到文件成功!");
                    return true;
                }
                catch (System.Exception ex)
                {
                    writeDebug("写入Mes到文件异常!", ex);
                    return false;
                }
                
            }
            return false;
        }
        private static void writeInfo(string info)
        {
            //log.Info(info);
        }
        private static void writeDebug(string info, Exception e)
        {
            //log.Debug(info, e);
        }
        public long lCheckResult = 100;
        /// <summary>
        /// 查找SN之前的状态
        /// </summary>
        /// <param name="Sn">序列号</param>
       
        public long CheckSN(string Sn)
        {

          string info = evData.DB_Password + "," + evData.DB_User + "," + evData.DatabaseName + "," + evData.ServerName + "," + Sn +
                "," + evData.StationID + "," + evData.LineGroup + "," + evData.SW_User + "," + evData.Debug + "," + evData.ShowWindow +
                "," + evData.PassForNoDB + "," + evData.Function;
          long result = Interlocking(info);
            //Func<string, long> f = EV_MSTR.Sv_Interlocking_Main;
            //f.BeginInvoke(info, CheckSNCallBack, null);
          return result;
        }
        private long Interlocking(string info)
        {
            //举例://"D:\CSharp\CSharp Lesson\SV_Interlocking\SV_Interlocking .net 3.5 命令行参数\bin\x86\Debug\SV_Interlocking.exe" PedTest @rn_radio, TE_RN, HD_Camera_IDA, HZHE015A,9001855036910011711, AA,1, Op, True, True, False,5
            string cmd = $@"cmd /c ""{Application.StartupPath}\sv_Interlocking\SV_Interlocking.exe""  {info}";
            RunCmd(cmd);
            return long.Parse(File.ReadAllText($@"{Application.StartupPath}\sv_Interlocking\SV_Interlocking.ini"));
        }
        private static string RunCmd(string cmdArgs)
        {
            string Tstr = "";
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();

            p.StandardInput.WriteLine(cmdArgs);
            p.StandardInput.WriteLine("exit");
            Tstr = p.StandardOutput.ReadToEnd();
            p.Close();
            return Tstr;
        }
        private void CheckSNCallBack(IAsyncResult result)
        {
            AsyncResult _Result = (AsyncResult)result;
            Func<string, long> f = (Func<string, long>)_Result.AsyncDelegate;
            lCheckResult = f.EndInvoke(_Result);           
        }
        
    }
    
}
