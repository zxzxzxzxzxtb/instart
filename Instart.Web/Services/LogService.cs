using Instart.Models;
using Instart.Service;
using Instart.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Instart.Web
{
    public class LogService
    {
        public static void Write(Log log)
        {
            if(log == null)
            {
                return;
            }

            var slt = AutofacService.Resolve<ILogService>().Insert(log);
        }
    }
}