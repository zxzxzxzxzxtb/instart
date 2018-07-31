using Instart.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Models
{
    /// <summary>
    /// 实习推荐
    /// </summary>
    public class Company
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }        

        /// <summary>
        /// 单位名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 单位英文名称
        /// </summary>
        public string NameEn { get; set; }

        /// <summary>
        /// 单位图片
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 单位介绍
        /// </summary>
        public string Introduce { get; set; }

        /// <summary>
        /// 招聘职位
        /// </summary>
        public string Postions { get; set; }
        
        /// <summary>
        /// 单位详情
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// 工作环境
        /// </summary>
        public string Environment { get; set; }

        /// <summary>
        /// 作品
        /// </summary>
        public string Works { get; set; }

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
