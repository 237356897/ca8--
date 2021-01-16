using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using log4net;
using System.Linq;
namespace Desay
{
    public partial class frmRecipe : Form
    {
        static ILog log = LogManager.GetLogger(typeof(frmRecipe));
        private readonly Action m_Load;
        private readonly Action m_Save;
        public frmRecipe()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 机种选择
        /// </summary>
        /// <param name="directoryPath">文件夹路径</param>
        /// <param name="currentProductType">产品类型</param>
        /// <param name="Load">读取方法</param>
        /// <param name="Save">保存方法</param>
        public frmRecipe(string directoryPath,string currentProductType, Action Load = null, Action Save = null) : this()
        {
            DirectoryPath = directoryPath;
            CurrentProductType = currentProductType;
            m_Load = Load;
            m_Save = Save;
        }
        public static string DirectoryPath { get; private set; }
        public static string CurrentProductType { get; private set; }
        private void FrmRecipe_Load(object sender, EventArgs e)
        {
            RefreshFileList();
            RefreshInfo();
            StoveLoad();
            cb_typelist.Items.Clear();
            if(RunPara2.data!=null && RunPara2.data.Count()>0)
            {
            foreach (var str in RunPara2.data)
            {
                cb_typelist.Items.Add(str.ProductName);
            }
                if (RunPara2.data[0].selindex >= 0)
                {
                tb_curtype.Text=    RunPara2.data[RunPara2.data[0].selindex].ProductName;
                    tbA2C.Text = RunPara.Instance.A2C;

                }

            }
        }
        private void RefreshFileList()
        {
            lstProductType.Items.Clear();
            FileInfo[] files = new DirectoryInfo(DirectoryPath).GetFiles();
            foreach (FileInfo file in files)
            {
                lstProductType.Items.Add(Path.GetFileNameWithoutExtension(file.Name));
            }
        }

        private void RefreshInfo()
        {
            txtCurrentType.Text = CurrentProductType;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            log.Info("用户点击进行机种新增操作！");
            string newType = txtTargetType.Text.Trim();
            if (string.IsNullOrEmpty(newType))
            {
                MessageBox.Show("目标型号不能为空！");
                return;
            }

            if (lstProductType.Items.Contains(newType))
            {
                MessageBox.Show("列表中已有相同型号，不能再次新增！");
                return;
            }

            foreach (char c in Path.GetInvalidFileNameChars())
            {
                if (newType.Contains(c))
                {
                    MessageBox.Show($"名称中不能包含特殊字符 '{c}' 请重新命名！");
                    return;
                }
            }
            CurrentProductType = newType;
            m_Save?.Invoke();
            RefreshFileList();
            RefreshInfo();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            log.Info("用户点击进行机种删除操作！");
            string deleteType = txtTargetType.Text.Trim();
            if (string.IsNullOrEmpty(deleteType))
            {
                MessageBox.Show("目标型号不能为空！");
                return;
            }

            if (deleteType == CurrentProductType)
            {
                MessageBox.Show("目标型号正在使用，不能删除！");
                return;
            }

            if (!lstProductType.Items.Contains(deleteType))
            {
                MessageBox.Show("列表中未找到目标，无法删除！");
                return;
            }

            if (MessageBox.Show(string.Format("是否删除型号：{0}", deleteType), "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                FileInfo[] files = new DirectoryInfo(DirectoryPath).GetFiles();
                foreach (FileInfo file in files)
                {
                    if (Path.GetFileNameWithoutExtension(file.Name) == deleteType)
                    {
                        file.Delete();
                        break;
                    }
                }
                RefreshFileList();
            }
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            log.Info("用户点击进行机种切换操作！");
            string selectType = txtTargetType.Text.Trim();
            if (string.IsNullOrEmpty(selectType))
            {
                MessageBox.Show("目标型号值为空，请确认是否选中对应型号！");
                return;
            }

            if (selectType == CurrentProductType)
            {
                MessageBox.Show("目标型号与正在使用的型号一直，无需切换！");
                return;
            }

            if (MessageBox.Show($"是否保存型号 {CurrentProductType} 的数据？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                m_Save?.Invoke();
            }

            CurrentProductType = selectType;
            m_Load?.Invoke(); 
            RefreshInfo();
        }
        private void lstProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtTargetType.Text = lstProductType.SelectedItem.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("未选中产品");
            }
        }

        private void StoveLoad()
        {
            nudStoveTimeMin.Value = (decimal)RunPara.Instance.MesStoveTime.min;
            nudStoveTimeMax.Value = (decimal)RunPara.Instance.MesStoveTime.max;
            nudTemperatureMin.Value = (decimal)RunPara.Instance.MesStoveTemperature.min;
            nudTemperatureMax.Value = (decimal)RunPara.Instance.MesStoveTemperature.max;
            nudStoveNoMin.Value = (decimal)RunPara.Instance.MesStoveNo.min;
            nudStoveNoMax.Value = (decimal)RunPara.Instance.MesStoveNo.max;
            tbA2C.Text = RunPara.Instance.A2C;
        }

        private void btnStoveSave_Click(object sender, EventArgs e)
        {
            RunPara.Instance.MesStoveTime.min = Convert.ToDouble(nudStoveTimeMin.Value);
            RunPara.Instance.MesStoveTime.max = Convert.ToDouble(nudStoveTimeMax.Value);
            RunPara.Instance.MesStoveTemperature.min = Convert.ToDouble(nudTemperatureMin.Value);
            RunPara.Instance.MesStoveTemperature.max = Convert.ToDouble(nudTemperatureMax.Value);
            RunPara.Instance.MesStoveNo.min = Convert.ToInt32(nudStoveNoMin.Value);
            RunPara.Instance.MesStoveNo.max = Convert.ToInt32(nudStoveNoMax.Value);
           
        }
        string _configNamg_find;
        string producttype;
        private void bt_new_Click(object sender, EventArgs e)
        {
            producttype = System.Windows.Forms.Application.StartupPath + "\\Product.md";

            if (tb_curtype.Text.Length>2 && tbA2C.Text.Length > 2)
            {
                if (RunPara2.data!=null && RunPara2.data.Count()>0)
                {

                RunPara.Instance.A2C = tbA2C.Text;
                _configNamg_find = tb_curtype.Text;
                int index = -1;
                try
                {
                    index = RunPara2.data.FindIndex(_check_CConfigName);
                }
                catch
                {

                    index = -2;
                }

                 if (index >=0)
                {
                    //已有

                    if (tbA2C.Text != RunPara2.data[index].A2CX)
                    {
                        DialogResult result = MessageBox.Show("警告！！！ 已有该型号，是否修改当前A2C？", "注意", MessageBoxButtons.YesNoCancel);
                        if (result == DialogResult.Yes)
                        {
                               
                            RunPara2.data[index].A2CX = tbA2C.Text;
                          
                        }

                    }
                    
                        RunPara2.data[0].selindex = index;
                        RunPara.Instance.A2C = RunPara2.data[index].A2CX;
                        ProductA2C.SerializeMethod(RunPara2.data, producttype);
                    }
           else
                {
                    //新
                    ProductA2C d1 = new ProductA2C();
                    d1.A2CX = tbA2C.Text;
                    d1.ProductName = tb_curtype.Text;

                    RunPara2.data.Add(d1);
                        RunPara2.data[0].selindex = RunPara2.data.Count() - 1;
                        ProductA2C.SerializeMethod(RunPara2.data,producttype);
                }
                }
                else
                {
                    //首次
                    RunPara2.data = new System.Collections.Generic.List<ProductA2C>();
                    ProductA2C d1 = new ProductA2C();
                    d1.selindex = -1;
                    d1.ProductName = "备用";
                    RunPara2.data.Add(d1);
                    ProductA2C.SerializeMethod(RunPara2.data, producttype);
                }
            }
            else
            {
                MessageBox.Show("型号名称过短，或A2C名称过短2");
            }

        }

        private  bool _check_CConfigName(ProductA2C dd)
        {
            if (dd != null)
            {
                string value = dd.ProductName;
                if (value == _configNamg_find)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        private void cb_typelist_SelectedIndexChanged(object sender, EventArgs e)
        {
            try

            {
                RunPara2.data[0].selindex = cb_typelist.SelectedIndex; ;

                tbA2C.Text = RunPara2.data[cb_typelist.SelectedIndex].A2CX;
                tb_curtype.Text = RunPara2.data[cb_typelist.SelectedIndex].ProductName;



            }
            catch
            { }


        }
    }
}
