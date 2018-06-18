using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Instart.Web.Models
{
    public class ResultBase
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public int ErrorCode { get; set; }

        public object Data { get; set; }
    }

    public class ResultBase<T> where T : class
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public int ErrorCode { get; set; }

        public T Data { get; set; }
    }
}