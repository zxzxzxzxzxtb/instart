using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public interface IStudentRepository
    {
        Task<Student> GetByIdAsync(int id);        

        Task<PageModel<Student>> GetListAsync(int pageIndex, int pageSize, int division = -1, string name = null);

        Task<bool> InsertAsync(Student model);

        Task<bool> UpdateAsync(Student model);

        Task<bool> DeleteAsync(int id);

        Task<List<Student>> GetRecommendListAsync(int topCount);

        Task<bool> SetRecommend(int id, bool isRecommend);

        Task<IEnumerable<Student>> GetAllAsync();

        Task<IEnumerable<Student>> GetStarStudentsAsync();
    }
}
