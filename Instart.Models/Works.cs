using Instart.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Models
{
    /// <summary>
    /// 作品集
    /// </summary>
    public class Works
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }        

        /// <summary>
        /// 作品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图片链接
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Introduce { get; set; }

        /// <summary>
        /// 所属专业
        /// </summary>
        public int MajorId { get; set; }

        /// <summary>
        /// 专业名称
        /// </summary>
        public string MajorName { get; set; }

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
