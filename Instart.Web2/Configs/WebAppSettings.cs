using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Instart.Web2
{
    public class WebAppSettings
    {
        /// <summary>
        /// session名称
        /// </summary>
        public static string SessionName
        {
            get
            {
                return "instart_session";
            }
        }

        /// <summary>
        /// cookie名称
        /// </summary>
        public static string CookieName
        {
            get
            {
                return "instart_cookie";
            }
        }

        /// <summary>
        /// 是否开始模式
        /// </summary>
        public static bool IsDevelop
        {
            get
            {
                return Boolean.Parse(System.Configuration.ConfigurationManager.AppSettings["IsDevelop"]);
            }
        }

        /// <summary>
        /// DES加密key
        /// </summary>
        public static string DesEncryptKey
        {
            get
            {
                return "we934okx09krefgj@sdlk$sdklwwsggc";
            }
        }
    }
}