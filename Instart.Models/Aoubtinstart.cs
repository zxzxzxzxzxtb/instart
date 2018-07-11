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
        /// 视频介绍路径
        /// </summary>
        public string VideoUrl { get; set; }

        /// <summary>
        /// 一沙简介
        /// </summary>
        public string Introduce { get; set; }

        /// <summary>
        /// pre to pro
        /// </summary>
        public string PreToPro { get; set; }

        /// <summary>
        /// 跨学科教学
        /// </summary>
        public string PassLearning { get; set; }

        /// <summary>
        /// WorkShop
        /// </summary>
        public string WorkShop { get; set; }

        /// <summary>
        /// 24小时工作室
        /// </summary>
        public string Studio { get; set; }

        /// <summary>
        /// 实习推荐
        /// </summary>
        public string Recommand { get; set; }

        /// <summary>
        /// 驻地项目
        /// </summary>
        public string Programs { get; set; }

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
