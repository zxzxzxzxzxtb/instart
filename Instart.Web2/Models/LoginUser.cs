using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Instart.Web2.Models
{
    public class LoginUser
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string NickName { get; set; }

        public string Avatar { get; set; }
    }
}