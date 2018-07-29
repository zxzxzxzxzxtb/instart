using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Models
{
    /// <summary>
    /// 课程首页信息
    /// </summary>
    public class CourseInfo
    {
        /// <summary>
        /// 教学体系
        /// </summary>
        public string TeachingSys { get; set; }

        /// <summary>
        /// 课程流程-前期
        /// </summary>
        public string Early { get; set; }

        /// <summary>
        /// 课程流程-中期
        /// </summary>
        public string Mid { get; set; }

        /// <summary>
        /// 课程流程-后期
        /// </summary>
        public string Laster { get; set; }

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
