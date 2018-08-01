using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Instart.Web
{
    public static class AutoMapperExtensions
    {
        /// <summary>
        /// 泛型映射
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TDestination Mapper<TDestination>(this object source)
        {
            return AutoMapper.Mapper.Map<TDestination>(source);
        }
    }
}