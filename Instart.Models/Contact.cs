using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Models
{
    /// <summary>
    /// 校区
    /// </summary>
    public class Contact
    {

        /// <summary>
        /// 电话号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Ins
        /// </summary>
        public string Ins { get; set; }

        /// <summary>
        /// 微博
        /// </summary>
        public string Blog { get; set; }

        /// <summary>
        /// 商务合作
        /// </summary>
        public string Cooperation { get; set; }

        /// <summary>
        /// 二维码
        /// </summary>
        public string Qrcode { get; set; }

        /// <summary>
        /// 招聘信息
        /// </summary>
        public string Recruit { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }
    }
}
