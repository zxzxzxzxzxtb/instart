using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Instart.Web.Configs
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
    }
}