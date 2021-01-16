using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Desay
{
    public class AppConfig
    {
        /// <summary>
        ///     运动控制卡参数文件存放路径
        /// </summary>
        public static string CardParamFilePath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\CardParam.xml");
            }
        }

        /// <summary>
        ///     Tray盘参数文件存放路径
        /// </summary>
        public static string TrayFilePath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\Tray.ini");
            }
        }

        /// <summary>
        ///     轴参数文件存放路径
        /// </summary>
        public static string AixsParamFilePath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\AixsParam.xml");
            }
        }

        /// <summary>
        ///     全局参数文件存放路径
        /// </summary>
        public static string GlobalFilePath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\Global.xml");
            }
        }

        /// <summary>
        ///     运行参数文件存放路径
        /// </summary>
        public static string RunParaFilePath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\RunPara.xml");
            }
        }

        /// <summary>
        ///     延时参数文件存放路径
        /// </summary>
        public static string DelayFilePath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\Delay.xml");
            }
        }

        /// <summary>
        ///     统计文件存放路径
        /// </summary>
        public static string StatisticsFilePath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\Statistics.xml");
            }
        }

        /// <summary>
        ///     配方文件路径
        /// </summary>
        public static string RecipeFilePath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\Product.xml");
            }
        }

        /// <summary>
        ///     日志文件存放路径
        /// </summary>
        public static string LogFileName
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
            }
        }

        /// <summary>
        ///     MES配置参数文件路径
        /// </summary>
        public static string MESConfigFileName
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Model\\mes.ini");
            }
        }

        /// <summary>
        ///     本地数据库文件夹路径
        /// </summary>
        public static string DataBaseDirectoryPath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"DataBase");
            }
        }

        /// <summary>
        ///     本地数据库文件路径
        /// </summary>
        public static string DataBaseFileName
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format("{0}.db3", DateTime.Now.ToString("yyyyMMdd")));
            }
        }
    }
}
