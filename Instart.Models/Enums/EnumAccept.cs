using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Models.Enums
{
    public enum EnumAccept
    {
        /// <summary>
        /// 全部
        /// </summary>
        [Description("全部")]
        All = -1,
        /// <summary>
        /// 未受理
        /// </summary>
        [Description("未受理")]
        No = 0,
        /// <summary>
        /// 已受理
        /// </summary>
        [Description("已受理")]
        Yes = 1,        
    }
}
