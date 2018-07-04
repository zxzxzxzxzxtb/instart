using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Models
{
    /// <summary>
    /// 艺术导师
    /// </summary>
    public class Teacher
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 分部Id
        /// </summary>
        public int DivisionId { get; set; }

        /// <summary>
        /// 学校Id
        /// </summary>
        public int SchoolId { get; set; }

        /// <summary>
        /// 专业Id
        /// </summary>
        public int MajorId { get; set; }

        /// <summary>
        /// 导师姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 导师头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 导师毕业学校
        /// </summary>
        public string FromSchool { get; set; }

        /// <summary>
        /// 导师所学专业
        /// </summary>
        public string FromMajor { get; set; }

        /// <summary>
        /// 擅长课程
        /// </summary>
        public string FromCourse { get; set; }

        /// <summary>
        /// 导师介绍
        /// </summary>
        public string Introduce { get; set; }

        /// <summary>
        /// 导师作品（json）
        /// </summary>
        public string Cases { get; set; }

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
