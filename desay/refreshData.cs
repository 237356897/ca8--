using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desay
{
    /// <summary>
    ///     刷新表格控件
    /// </summary>
    public enum RefreshDataGridView
    {
        /// <summary>
        ///   上炉
        /// </summary>
        UpStove,

        /// <summary>
        ///  下炉
        /// </summary>
        DownStove,

        /// <summary>
        ///  手动取盘
        /// </summary>
        ManualTakeDish,
    }

    public delegate void RefreshDataCompleteEventHandler(object sender, RefreshDataGridView dgv);
}
