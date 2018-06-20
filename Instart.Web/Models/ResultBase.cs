using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Instart.Web.Models
{
    public class ResultBase
    {
        public bool success { get; set; }

        public string message { get; set; }

        public int errorcode { get; set; }

        public object data { get; set; }
    }
}