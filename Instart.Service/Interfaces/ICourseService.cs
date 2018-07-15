using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Service
{
    public interface ICourseService
    {
        Task<Course> GetByIdAsync(int id);

        Task<PageModel<Course>> GetListAsync(int pageIndex, int pageSize, string name = null);

        Task<IEnumerable<Course>> GetAllAsync();

        Task<bool> InsertAsync(Course model);

        Task<bool> UpdateAsync(Course model);

        Task<bool> DeleteAsync(int id);

        Task<List<Course>> GetRecommendListAsync(int topCount);

        Task<bool> SetRecommendAsync(int id, bool isRecommend);

        Task<int> GetInfoCountAsync();

        Task<CourseInfo> GetInfoAsync();

        Task<bool> InsertInfoAsync(CourseInfo model);

        Task<bool> UpdateInfoAsync(CourseInfo model);

        Task<IEnumerable<int>> GetTeachersByIdAsync(int id);

        Task<bool> SetTeachers(int courseId, string teacherIds);
    }
}
