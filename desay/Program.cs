using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Toolkit;
using System.Toolkit.Helpers;

namespace Desay
{
    static class Program
    {
        static System.Threading.Mutex mutex;
        static bool isRunning;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            mutex = new System.Threading.Mutex(true, "RunOneInstanceOnly", out isRunning);

            if (isRunning)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Delay.Instance = SerializerManager<Delay>.Instance.Load(AppConfig.DelayFilePath);
                Global.Instance = SerializerManager<Global>.Instance.Load(AppConfig.GlobalFilePath);

               // ca6
                 //Global.Instance.QRCodeComParam= "COM5,115200,None,8,One,1500,1500";
            
                //ca7
                //  Global.Instance.QRCodeComParam= "COM6,115200,None,8,One,1500,1500";

                Product.Instance = SerializerManager<Product>.Instance.Load(AppConfig.RecipeFilePath);
                RunPara.Instance = SerializerManager<RunPara>.Instance.Load(Product.Instance.ProductDataFile);

                //SerializerManager<RunPara>.Instance.Save(Product.Instance.ProductDataFile, RunPara.Instance);

                Application.Run(new FrmMain());
            }
            else
            {
                MessageBox.Show("程序已经启动！");
                Application.Exit();
            }
        }
    }
}
