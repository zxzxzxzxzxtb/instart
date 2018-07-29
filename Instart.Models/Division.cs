using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Models
{
    /// <summary>
    /// 学部
    /// </summary>
    public class Division
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 学部名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 学部英文名称
        /// </summary>
        public string NameEn { get; set; }

        /// <summary>
        /// 学部介绍
        /// </summary>
        public string Introduce { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int GroupIndex { get; set; }

        /// <summary>
        /// 状态，1：正常，0：删除
        /// </summary>
        public int Status { get; set; }

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
