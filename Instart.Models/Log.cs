using Instart.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Models
{
    public class Log
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 日志标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 日志明细
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public EnumOperType Type { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
