using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desay
{
    /// <summary>
    ///   二维码结构体
    /// </summary>
    public struct QRCodeStruct
    {
        public string SN;
        public int dgvRows;
        public bool sign;
        public DateTime scantime;   //1007
        public DateTime endtime;  //1007
        public int timespanScan;  //1007
        public string A2C;  //1007

        //mes
       // public double timespanPLC; //1007



        public StoveData MesStoveTimeX;
        public StoveData MesStoveTemperatureX;
        public StoveData MesStoveNoX ;


    }


    /// <summary>
    ///  工作盘结构体
    /// </summary>
    public struct TaryStruct
    {
        /// <summary>
        /// 产品位置
        /// </summary>
        public int ProductPos;
        /// <summary>
        /// 产品现在位置
        /// </summary>
        public int CurProductPos;
        /// <summary>
        /// 料盘码
        /// </summary>
        public string TrayCode;
        /// <summary>
        /// 产品码
        /// </summary>
        public QRCodeStruct[] QRCode;
        
        //public string A2C;  //1007
        
        /// <summary>
        /// 1007
        /// </summary>
        public string productInfo; 


        public int timeStove;

    }

    /// <summary>
    ///  炉结构体
    /// </summary>
    public struct StoveStruct
    {
        /// <summary>
        /// 炉位号
        /// </summary>
        public int StoveNo;          
        /// <summary>
        /// 有无料盘
        /// </summary>
        public bool AnyMaterialTary;  
        /// <summary>
        /// 炉温度
        /// </summary>
        public double Temperature;    
        /// <summary>
        /// 盘
        /// </summary>
        public TaryStruct Tray;       
        /// <summary>
        /// 固化时间
        /// </summary>
        public int timeStove;

        public bool ifcaltime;

    }
}
