using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Models.Enums
{
    /// <summary>
    /// banner图片显示位置
    /// </summary>
    public enum EnumBannerPos
    {
        /// <summary>
        /// 首页
        /// </summary>
        [Description("首页")]
        Index = 0,
        /// <summary>
        /// 艺术课程
        /// </summary>
        [Description("艺术课程")]
        Course = 1,
        /// <summary>
        /// 艺术导师
        /// </summary>
        [Description("艺术导师")]
        Teacher = 2,
    }
}
