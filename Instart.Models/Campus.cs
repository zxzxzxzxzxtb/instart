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
    public class Campus
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 校区名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string NameEn { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 文字介绍
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

        /// <summary>
        /// 校区特色
        /// </summary>
        public string Feature { get; set; }

        /// <summary>
        /// 校区设备
        /// </summary>
        public string Devices { get; set; }

        /// <summary>
        /// 地理位置
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 周边环境
        /// </summary>
        public string Environment { get; set; }

        /// <summary>
        /// 校区LOGO
        /// </summary>
        public string Avatar { get; set; }
    }
}
