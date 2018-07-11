using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Repository;

namespace Instart.Service
{
    public class CourseService : ServiceBase, ICourseService
    {
        ICourseRepository _courseRepository = AutofacRepository.Resolve<ICourseRepository>();

        public async Task<List<Course>> GetRecommendListAsync(int topCount)
        {
            if(topCount == 0)
            {
                return null;
            }

            return await _courseRepository.GetRecommendListAsync(topCount);
        }
    }
}
