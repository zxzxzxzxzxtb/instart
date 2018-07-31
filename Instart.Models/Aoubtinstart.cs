using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Models
{
    /// <summary>
    /// 校区
    /// </summary>
    public class AboutInstart
    {
        /// <summary>
        /// 视频封面路径
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 视频介绍路径
        /// </summary>
        public string VideoUrl { get; set; }

        /// <summary>
        /// 一沙简介
        /// </summary>
        public string Introduce { get; set; }

        /// <summary>
        /// 一沙团队
        /// </summary>
        public string Team { get; set; }

        /// <summary>
        /// 一沙发展
        /// </summary>
        public string Develop { get; set; }

        /// <summary>
        /// pre to pro图片
        /// </summary>
        public string PreToProImg { get; set; }

        /// <summary>
        /// 学部制度图片
        /// </summary>
        public string DivisionImg { get; set; }

        /// <summary>
        /// 跨学科教学
        /// </summary>
        public string PassLearning { get; set; }

        /// <summary>
        /// 跨学科教学图片
        /// </summary>
        public string PassLearningImg { get; set; }

        /// <summary>
        /// WorkShop图片
        /// </summary>
        public string WorkShopImg { get; set; }

        /// <summary>
        /// 24小时工作室图片
        /// </summary>
        public string StudioImg { get; set; }

        /// <summary>
        /// 实习推荐图片
        /// </summary>
        public string CompanyImg { get; set; }

        /// <summary>
        /// 艺术家孵化平台图片
        /// </summary>
        public string ActorImg { get; set; }

        /// <summary>
        /// 驻地项目图片
        /// </summary>
        public string ProgramsImg { get; set; }

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
