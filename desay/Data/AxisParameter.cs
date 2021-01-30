using Motion.AdlinkAps;
using Motion.Interfaces;

namespace Desay
{
    /// <summary>
    /// 各轴参数
    /// </summary>
    public class AxisParameter
    {
        #region 单例

        public static AxisParameter Instance = new AxisParameter();

        private AxisParameter() { }

        #endregion
        #region ca6

        //最大速度
        //public int CarryvelocityMax = 120;


        ////速度比率
        //public int CarryvelocityRate = 40;


        ////传动参数 减速比1:10
        //public TransmissionParams CarrytransParams = new TransmissionParams() { Lead = 20, SubDivisionNum = 200000 };

        ////速度参数
        //public VelocityCurve CarryvelocityCurve
        //{
        //    get
        //    {
        //        return new VelocityCurve(8, (CarryvelocityMax * CarryvelocityRate) / 100, 0.20);
        //    }
        //}

        //public HomeParams CarryHomeParams
        //{
        //    get
        //    {
        //        return new HomeParams(0, 1, 1, 100000, 35000, 0);
        //    }
        //}

        #endregion

        //#region ca7
        /// <summary>
        /// 最大速度
        /// </summary>
        public int CarryvelocityMax = RunPara.Instance.CarryvelocityMax;

        /// <summary>
        /// 速度比率
        /// </summary>
        public int CarryvelocityRate = RunPara.Instance.CarryvelocityRate;

        /// <summary>
        /// 传动参数 减速比1:10
        /// </summary>
        public TransmissionParams CarrytransParams = new TransmissionParams() { Lead = 20, SubDivisionNum = 20000 };

        /// <summary>
        /// 速度参数 （最大速度 * 速度比率）
        /// </summary>
        public VelocityCurve CarryvelocityCurve
        {
            get
            {
                return new VelocityCurve(8, (CarryvelocityMax * CarryvelocityRate) / 100, 1.00);
            }
        }

        public HomeParams CarryHomeParams
        {
            get
            {
                return new HomeParams(0, 1, 0, 40000, 4000, 0);
            }
        }


        //#endregion


    }
}
