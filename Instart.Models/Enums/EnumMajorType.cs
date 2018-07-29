using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Models.Enums
{
    public enum EnumMajorType
    {
        /// <summary>
        /// 本科
        /// </summary>
        [Description("本科")]
        BengKe = 0,

        /// <summary>
        /// 研究生
        /// </summary>
        [Description("研究生")]
        YanJiuSheng = 1
    }
}
