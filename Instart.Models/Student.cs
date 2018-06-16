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
        /// 录取学校Id
        /// </summary>
        public int SchoolId { get; set; }

        /// <summary>
        /// 录取学校名称
        /// </summary>
        public string SchoolName { get; set; }

        /// <summary>
        /// 录取专业
        /// </summary>
        public string Major { get; set; }

        /// <summary>
        /// 奖学金
        /// </summary>
        public decimal Scholarship { get; set; }

        /// <summary>
        /// 学员姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 学员头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Offer图片
        /// </summary>
        public string OfferImage { get; set; }

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
