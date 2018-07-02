using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Models
{
    /// <summary>
    /// 艺术院校
    /// </summary>
    public class School
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string NameEn { get; set; }

        /// <summary>
        /// LOGO
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// 学校图片
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// 学校类别
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 网址
        /// </summary>
        public string WebSite { get; set; }

        /// <summary>
        /// 优势专业/专业推荐
        /// </summary>
        public string FeatureMajor { get; set; }

        /// <summary>
        /// 申请开始日期
        /// </summary>
        public DateTime? ApplyStartDate { get; set; }

        /// <summary>
        /// 申请结束日期
        /// </summary>
        public DateTime? ApplyEndDate { get; set; }

        /// <summary>
        /// 申请难度
        /// </summary>
        public double ApplyDifficulty { get; set; }

        /// <summary>
        /// 托福
        /// </summary>
        public string TuoFu { get; set; }

        /// <summary>
        /// 雅思
        /// </summary>
        public string YaSi { get; set; }

        /// <summary>
        /// 研
        /// </summary>
        public string Yan { get; set; }

        /// <summary>
        /// SAT
        /// </summary>
        public string SAT { get; set; }

        /// <summary>
        /// 学费
        /// </summary>
        public string Cost { get; set; }

        /// <summary>
        /// 学校介绍
        /// </summary>
        public string Introduce { get; set; }

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
