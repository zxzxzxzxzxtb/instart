using Dapper;
using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public class StatisticsRepository : IStatisticsRepository
    {
        public async Task<Statistics> GetAsync()
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = @"select 
                            (select count(1) from School where status=1) as SchoolCount,
                            (select COUNT(1) from Teacher where Status=1) as TeacherCount,
                            (select COUNT(1) from Student where Status=1) as StudentCount,
                            (select COUNT(1) from Major where Status=1) as MajorCount,
                            (select COUNT(1) from Course where Status=1) as CourseCount,
                            (select COUNT(1) from SchoolApply) as SchoolApplyCount,
                            (select COUNT(1) from CourseApply) as CourseApplyCount;";
                return await conn.QueryFirstOrDefaultAsync<Statistics>(sql, null);
            }
        }
    }
}
