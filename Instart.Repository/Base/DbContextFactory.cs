using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public class DbContextFactory
    {
        public static DbContext Create() {
            DbContext dbContext = CallContext.GetData("DbContext") as DbContext;
            if (dbContext == null) {
                dbContext = new InstartDbContext();
                CallContext.SetData("DbContext", dbContext);
            }
            return dbContext;
        }
    }
}
