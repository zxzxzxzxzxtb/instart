using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Service
{
    public interface IStudentService
    {
        Task<Student> GetByIdAsync(int id);

        Task<PageModel<Student>> GetListAsync(int pageIndex, int pageSize, string name = null);

        Task<bool> InsertAsync(Student model);

        Task<bool> UpdateAsync(Student model);

        Task<bool> DeleteAsync(int id);
    }
}
