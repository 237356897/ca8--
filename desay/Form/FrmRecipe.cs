using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using log4net;
using System.Linq;
using System.Toolkit.Helpers;

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
        public frmRecipe(string directoryPath, string currentProductType, Action Load = null, Action Save = null) : this()
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
        }

        /// <summary>
        /// 刷新机种类型
        /// </summary>
        private void RefreshFileList()
        {
            lstProductType.Items.Clear();
            FileInfo[] files = new DirectoryInfo(DirectoryPath).GetFiles();
            foreach (FileInfo file in files)
            {
                lstProductType.Items.Add(Path.GetFileNameWithoutExtension(file.Name));
            }
        }

        /// <summary>
        /// 显示当前机种类型
        /// </summary>
        private void RefreshInfo()
        {
            txtCurrentType.Text = CurrentProductType;
        }

        /// <summary>
        /// 新增机种
        /// </summary>
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

        /// <summary>
        /// 删除机种
        /// </summary>
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

        /// <summary>
        /// 切换机种
        /// </summary>
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

            m_Save?.Invoke();
            CurrentProductType = selectType;
            m_Load?.Invoke();
            RefreshInfo();
        }

        /// <summary>
        /// 选择目标机种
        /// </summary>
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

        /// <summary>
        /// 加载固化炉参数
        /// </summary>
        private void StoveLoad()
        {
            nudStoveTimeMin.Value = (decimal)RunPara.Instance.MesStoveTime.min;
            nudStoveTimeMax.Value = (decimal)RunPara.Instance.MesStoveTime.max;
            nudTemperatureMin.Value = (decimal)RunPara.Instance.MesStoveTemperature.min;
            nudTemperatureMax.Value = (decimal)RunPara.Instance.MesStoveTemperature.max;
            nudStoveNoMin.Value = (decimal)RunPara.Instance.MesStoveNo.min;
            nudStoveNoMax.Value = (decimal)RunPara.Instance.MesStoveNo.max;
        }

        string _configNamg_find;
        string producttype;

        /// <summary>
        /// 保存固化炉参数
        /// </summary>
        private void bt_new_Click(object sender, EventArgs e)
        {
            RunPara.Instance.MesStoveTime.min = Convert.ToDouble(nudStoveTimeMin.Value);
            RunPara.Instance.MesStoveTime.max = Convert.ToDouble(nudStoveTimeMax.Value);
            RunPara.Instance.MesStoveTemperature.min = Convert.ToDouble(nudTemperatureMin.Value);
            RunPara.Instance.MesStoveTemperature.max = Convert.ToDouble(nudTemperatureMax.Value);
            RunPara.Instance.MesStoveNo.min = Convert.ToInt32(nudStoveNoMin.Value);
            RunPara.Instance.MesStoveNo.max = Convert.ToInt32(nudStoveNoMax.Value);

            SerializerManager<RunPara>.Instance.Save(Product.Instance.ProductDataFile, RunPara.Instance);
            MessageBox.Show("保存成功！", "提示",MessageBoxButtons.OK);

        }
    }
}
