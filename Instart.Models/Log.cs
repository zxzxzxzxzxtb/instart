using Instart.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Models
{
    public class Log
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string ActionName{get;set;}

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName{ get; set;}

        /// <summary>
        /// 方法参数
        /// </summary>
        public string ActionParameters{get;set;}        

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark{get;set;}

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
