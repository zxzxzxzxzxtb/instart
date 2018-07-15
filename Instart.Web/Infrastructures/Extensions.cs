using Instart.Common;
using Instart.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web.Infrastructures
{
    public static class Extensions
    {
        public static string GetDescription(this Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memberInfos = type.GetMember(en.ToString());
            if (memberInfos != null && memberInfos.Length > 0)
            {
                DescriptionAttribute[] attrs = memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

                if (attrs != null && attrs.Length > 0)
                {
                    return attrs[0].Description;
                }
            }
            return en.ToString();
        }

        public static MvcHtmlString SelectForEnum<TEnum>(this HtmlHelper helper, string id, string name, string @class)
        {
            var list = EnumberHelper.EnumToList<TEnum>();

            var sb = new StringBuilder();

            sb.Append($"<select id=\"{id}\" name=\"{name}\" class=\"{@class}\">");

            foreach(var item in list)
            {
                sb.Append($"<option value=\"{item.EnumValue}\">{item.Description}</option>");
            }

            sb.Append("</select>");

            return new MvcHtmlString(sb.ToString());
        }
    }
}