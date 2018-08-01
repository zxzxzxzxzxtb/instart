using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Models
{
    /// <summary>
    /// 24小时工作室
    /// </summary>
    public class Studio
    {
        /// <summary>
        /// 工作室名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string NameEn { get; set; }

        /// <summary>
        /// 工作室Logo
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 工作室介绍
        /// </summary>
        public string Introduce { get; set; }

        /// <summary>
        /// 工作室详情
        /// </summary>
        public string Details { get; set; }

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
