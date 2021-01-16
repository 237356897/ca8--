using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Desay
{
    public class RunPara
    {
        #region 单例

        public static RunPara Instance = new RunPara();

        private RunPara()
        {
            // 6炉 20个产品
            for(int i = 0; i < Stove.Length; i++)
            {
                Stove[i].Tray.QRCode = new QRCodeStruct[20];
            }
            OKTary.QRCode = new QRCodeStruct[20];
            NGTary.QRCode = new QRCodeStruct[20];

            OutTary.QRCode = new QRCodeStruct[20];


        }

        #endregion

        public int OKCount;
        public int NGCount;
        public int TotalCount;
        public string OrgTrayCode;
        /// <summary>
        /// OK盘
        /// </summary>
        public TaryStruct OKTary = new TaryStruct();          
        /// <summary>
        /// NG盘
        /// </summary>
        public TaryStruct NGTary = new TaryStruct();
        /// <summary>
        /// 待料盘1
        /// </summary>
        public TaryStruct WaitTary1 = new TaryStruct();
        /// <summary>
        /// 待料盘2
        /// </summary>
        public TaryStruct WaitTary2 = new TaryStruct();
        /// <summary>
        /// 输出盘
        /// </summary>
        public TaryStruct OutTary = new TaryStruct();          
        /// <summary>
        /// 6个炉
        /// </summary>
        public StoveStruct[] Stove = new StoveStruct[6];      
        /// <summary>
        /// OK数
        /// </summary>
        public int OkNumber;                                  
        /// <summary>
        /// NG数
        /// </summary>
        public int NgNumber;                                 
        /// <summary>
        /// 总数
        /// </summary>
        public int TotalNumber;                               
        /// <summary>
        /// 输送轴原点位
        /// </summary>
        public double CarryAxisOrgPos;                     
        /// <summary>
        /// 输送轴降温点位
        /// </summary>
        public double CarryAxisCoolingPos;                  
        /// <summary>
        /// 输送轴动点位
        /// </summary>
        public double CarryAxisMovePos;                      
        /// <summary>
        /// 周转盘数
        /// </summary>
        public int RotatingDisk;                               
        /// <summary>
        /// 首次开启
        /// </summary>
        public bool FirstStart;
        /// <summary>
        /// 连续报警上限
        /// </summary>
        public int ContinuousAlarmLimt = 3;
        /// <summary>
        /// 连续NG报警次数
        /// </summary>
        public double Continuous3Alarm;                      
        /// <summary>
        /// 待料时间(设定的时间内无料就上固化炉)
        /// </summary>
        public int SupplyDelayTime;                           
        /// <summary>
        /// 等待扫产品码时间
        /// </summary>
        public int QRCodeDelayTime; 
        /// <summary>
        /// 等待扫料盘时间
        /// </summary>
        public int TrayCodeDelayTime;
        /// <summary>
        /// 无盘等待时间
        /// </summary>
        public int WishoutDiskWaitTime;                          
        /// <summary>
        /// 降温时间
        /// </summary>
        public int CoolingTime;                                  
        /// <summary>
        /// 屏蔽门禁
        /// </summary>
        public bool ShieldEntraceGuard;                        
        /// <summary>
        /// 屏蔽AAReady
        /// </summary>
        public bool ShieldAAReady;                              
        /// <summary>
        /// 屏蔽AA 产品OK
        /// </summary>
        public bool ShieldAAOK;                                
        /// <summary>
        /// 屏蔽AA 产品NG
        /// </summary>
        public bool ShieldAANG;                                
        /// <summary>
        /// 屏蔽接驳台夹具有料感应
        /// </summary>
        public bool ShieldFixtureInduction;                     
        /// <summary>
        /// 炉1屏蔽
        /// </summary>
        public bool Stove1Shield;                              
        /// <summary>
        /// 炉2屏蔽
        /// </summary>
        public bool Stove2Shield;                               
        /// <summary>
        /// 炉3屏蔽
        /// </summary>
        public bool Stove3Shield;                              
        /// <summary>
        /// 炉4屏蔽
        /// </summary>
        public bool Stove4Shield;                             
        /// <summary>
        /// 炉5屏蔽
        /// </summary>
        public bool Stove5Shield;                               
        /// <summary>
        /// 炉6屏蔽
        /// </summary>
        public bool Stove6Shield;
        /// <summary>
        /// 自动
        /// </summary>
        public bool cbAuto;
        /// <summary>
        /// 整线
        /// </summary>
        public bool cbWholeline;

        public int TrayPoint = 25;

        #region Mes
        //机型 505941
        //a2c P01001505941
        //Sn  P01001505941 0999999
        //扫描出来的 sn 50594102 999999
        public string A2C = "900168503941";
        /// <summary>
        /// 是否启用Mes互锁
        /// </summary>
        public bool IsUseMesLock = true;

        public StoveData MesStoveTime = new StoveData();
        public StoveData MesStoveTemperature = new StoveData();
        public StoveData MesStoveNo = new StoveData();
        #endregion

        /// <summary>
        /// 炉固化时间
        /// </summary>
        public double[] StoveCuringTime = new double[6];     
        /// <summary>
        /// 每炉平均温度
        /// </summary>
        public double[] AverageTemperature = new double[6];   
        
    }

    [Serializable]
    public class RunPara2
    {
        public static List<ProductA2C> data = new List<ProductA2C>();
       

    }



    [Serializable]
    public class ProductA2C
    {
      
        public string ProductName;
        public string A2CX;
        public string str1;
        public string str2;

        public int selindex = 0;

        public static object ReserializeMethod(string filepath)
        {
            using (FileStream fs = new FileStream(filepath, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return bf.Deserialize(fs);
            }
        }
        public static void SerializeMethod(object obj, string filepath)
        {
            using (FileStream fs = new FileStream(filepath, FileMode.Create))
            {

                BinaryFormatter bf = new BinaryFormatter();

                bf.Serialize(fs, obj);
            }
        }

    }

  

}
