using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Instart.Models;

namespace Instart.Repository
{
    public class PartnerRepository : IPartnerRepository
    {
        public async Task<List<Partner>> GetListAsync(int topCount)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = $"select top {topCount} * from [Partner] where Status=1 order by Id Desc;";
                return (await conn.QueryAsync<Partner>(sql, null))?.ToList();
            }
        }
    }
}
