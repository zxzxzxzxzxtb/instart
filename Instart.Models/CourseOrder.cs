using Instart.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Models
{
    /// <summary>
    /// 课程预约
    /// </summary>
    public class CourseOrder
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
        /// 课程英文名
        /// </summary>
        public string CourseNameEn { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public EnumCountry Country { get; set; }

        /// <summary>
        /// 专业Id
        /// </summary>
        public int MajorId { get; set; }

        /// <summary>
        /// 专业名称
        /// </summary>
        public string MajorName { get; set; }

        /// <summary>
        /// 专业英文名称
        /// </summary>
        public string MajorNameEn { get; set; }

        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 申请人电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

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
