using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Desay
{
    public class Product
    {
        #region 单例

        public static Product Instance = new Product();

        private Product() { }

        #endregion

        public string ProductDataFilePath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data\\");
            }
        }

        public string ProductDataFile
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Data\\{CurrentProductType}.xml");
            }
        }

        /// <summary>
        /// 当前产品型号
        /// </summary>
        public string CurrentProductType = "Default";

        /// <summary>
        /// 产品型号列表
        /// </summary>
        public List<string> ProductType = new List<string>();
        /// <summary>
        /// 是否有接驳台组件
        /// </summary>
        public bool HaveConnection = false;

    }
}
