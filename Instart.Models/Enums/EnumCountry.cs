using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Models.Enums
{
    public enum EnumCountry
    {
        /// <summary>
        /// 英国
        /// </summary>
        [Description("英国")]
        England = 0,
        /// <summary>
        /// 美国
        /// </summary>
        [Description("美国")]
        America = 1,
        /// <summary>
        /// 欧洲
        /// </summary>
        [Description("欧洲")]
        Europe = 2,
        /// <summary>
        /// 亚洲
        /// </summary>
        [Description("亚洲")]
        Asia = 3,
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Others = 9,
    }
}
