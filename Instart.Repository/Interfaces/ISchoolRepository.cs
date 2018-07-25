using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public interface ISchoolRepository
    {
        Task<School> GetByIdAsync(int id);        

        Task<PageModel<School>> GetListAsync(int pageIndex, int pageSize, string name = null);

        Task<IEnumerable<School>> GetAllAsync();

        Task<bool> InsertAsync(School model);

        Task<bool> UpdateAsync(School model);

        Task<bool> DeleteAsync(int id);

        Task<List<School>> GetRecommendListAsync(int topCount);

        Task<bool> SetRecommendAsync(int id, bool isRecommend);

        Task<List<School>> GetHotListAsync(int topCount);

        Task<bool> SetHotAsync(int id, bool isHot);

        Task<PageModel<School>> GetListAsync(int pageIndex, int pageSize, string name = null, int country = -1, int major = -1);

        Task<IEnumerable<SchoolMajor>> GetMajorsByIdAsync(int id);

        Task<bool> SetMajors(int schoolId, string majorIds, string introduces);
    }
}
