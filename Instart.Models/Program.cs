using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models.Enums;

namespace Instart.Models
{
    /// <summary>
    /// 项目
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 项目英文名
        /// </summary>
        public string NameEn { get; set; }

        /// <summary>
        /// 项目图片
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// 项目介绍
        /// </summary>
        public string Introduce { get; set; }

        /// <summary>
        /// 项目详情
        /// </summary>
        public string Details { get; set; }

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
        /// 项目类型: 1-pretopro,2-workshop,3-艺术家孵化平台,4-驻地项目
        /// </summary>
        public EnumProgramType Type { get; set; }
    }
}
