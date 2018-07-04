using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Models
{
    /// <summary>
    /// 成功学员
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 照片
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 录取学校Id
        /// </summary>
        public int SchoolId { get; set; }

        /// <summary>
        /// 录取专业Id
        /// </summary>
        public int MajorId { get; set; }

        /// <summary>
        /// 导师Id
        /// </summary>
        public int TeacherId { get; set; }

        /// <summary>
        /// 奖学金
        /// </summary>
        public decimal Scholarship { get; set; }

        /// <summary>
        /// Offer图片地址
        /// </summary>
        public string OfferImageUrl { get; set; }

        /// <summary>
        /// 作品集（json）
        /// </summary>
        public string Portfolio { get; set; }

        /// <summary>
        /// 导师评语
        /// </summary>
        public string TeacherComment { get; set; }

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
