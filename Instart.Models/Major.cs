using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Models
{
    /// <summary>
    /// 艺术专业
    /// </summary>
    public class Major
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 学校Id
        /// </summary>
        public int SchoolId { get; set; }

        /// <summary>
        /// 学校名称
        /// </summary>
        public string SchoolName { get; set; }

        /// <summary>
        /// 专业名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 专业介绍
        /// </summary>
        public string Introduce { get; set; }

        /// <summary>
        /// 专业详情
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// 就业前景
        /// </summary>
        public string Feature { get; set; }

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
        public DateTime? ModifyTime { get; set; }
    }
}
