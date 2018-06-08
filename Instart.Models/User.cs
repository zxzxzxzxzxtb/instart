using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Models
{
    [Table("User")]
    public class User
    {
        public long Id { get; set; }

        public string Account { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int Role { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
