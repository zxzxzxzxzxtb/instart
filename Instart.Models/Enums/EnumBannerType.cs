using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Models.Enums
{
    public enum EnumBannerType
    {
        /// <summary>
        /// 图片
        /// </summary>
        [Description("图片")]
        Image = 0,
        /// <summary>
        /// 视频
        /// </summary>
        [Description("视频")]
        Video = 1,
    }
}
