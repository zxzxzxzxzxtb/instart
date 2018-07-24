using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Models.Enums
{
    /// <summary>
    /// 日志操作类型
    /// </summary>
    public enum EnumOperType
    {
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other = 0,
        /// <summary>
        /// 增加
        /// </summary>
        [Description("增加")]
        Insert = 1,
        /// <summary>
        /// 修改
        /// </summary>
        [Description("修改")]
        Update = 2,
        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete = 3
    }
}
