using Instart.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public class InstartDbContext : DbContext
    {
        public InstartDbContext() : base(AppSettings.ConnectionString) 
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
