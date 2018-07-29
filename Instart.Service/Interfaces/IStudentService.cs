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
         Student GetByIdAsync(int id);

         PageModel<Student> GetListAsync(int pageIndex, int pageSize, int division = -1, string name = null);

         bool InsertAsync(Student model);

         bool UpdateAsync(Student model);

         bool DeleteAsync(int id);

         List<Student> GetRecommendListAsync(int topCount);

         bool SetRecommend(int id, bool isRecommend);

         IEnumerable<Student> GetAllAsync();

         IEnumerable<Student> GetStarStudentsAsync();

         IEnumerable<int> GetCoursesByIdAsync(int id);

         bool SetCourses(int studentId, string courseIds);

         List<Student> GetListByCourseAsync(int courseId = -1);
    }
}
