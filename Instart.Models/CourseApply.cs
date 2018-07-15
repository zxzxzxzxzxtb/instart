using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Models
{
    /// <summary>
    /// 课程申请
    /// </summary>
    public class CourseApply
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 学校Id
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 申请人电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 申请人专业
        /// </summary>
        public string Major { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 是否沟通受理
        /// </summary>
        public bool IsAccept { get; set; }

        /// <summary>
        /// 受理时间
        /// </summary>
        public DateTime? AcceptTime { get; set; }
    }
}
