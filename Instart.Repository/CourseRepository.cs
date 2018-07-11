using Dapper;
using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public class CourseRepository: ICourseRepository
    {
        public async Task<List<Course>> GetRecommendListAsync(int topCount)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = $@"select top {topCount} Id,Name,NameEn,Picture,Introduce from Course where Status=1 and IsRecommend=1 order by Id desc;";
                return (await conn.QueryAsync<Course>(sql, null))?.ToList();
            }
        }
    }
}
