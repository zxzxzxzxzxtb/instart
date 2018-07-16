using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Service
{
    public interface ITeacherService
    {
        Task<Teacher> GetByIdAsync(int id);

        Task<PageModel<Teacher>> GetListAsync(int pageIndex, int pageSize, int division = -1, string name = null);

        Task<IEnumerable<Teacher>> GetAllAsync();

        Task<bool> InsertAsync(Teacher model);

        Task<bool> UpdateAsync(Teacher model);

        Task<bool> DeleteAsync(int id);

        Task<List<Teacher>> GetRecommendListAsync(int topCount);

        Task<bool> SetRecommendAsync(int id, bool isRecommend);

        Task<IEnumerable<int>> GetCoursesByIdAsync(int id);

        Task<bool> SetCourses(int teacherId, string courseIds);
    }
}
