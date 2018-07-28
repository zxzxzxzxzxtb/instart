using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Repository;

namespace Instart.Service
{
    public class StudentService : ServiceBase, IStudentService
    {
        IStudentRepository _studentRepository = AutofacRepository.Resolve<IStudentRepository>();

        public StudentService() {
            base.AddDisposableObject(_studentRepository);
        }

        public  Student GetByIdAsync(int id) {
            if (id <= 0) {
                throw new ArgumentException(nameof(id));
            }

            return _studentRepository.GetByIdAsync(id);
        }

        public  PageModel<Student> GetListAsync(int pageIndex, int pageSize, int division = -1, string name = null) {
            return _studentRepository.GetListAsync(pageIndex, pageSize, division, name);
        }

        public  IEnumerable<Student> GetAllAsync()
        {
            return _studentRepository.GetAllAsync();
        }

        public  IEnumerable<Student> GetStarStudentsAsync()
        {
            return _studentRepository.GetStarStudentsAsync();
        }

        public  bool InsertAsync(Student model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Name)) {
                throw new ArgumentNullException(nameof(model.Name));
            }

            return _studentRepository.InsertAsync(model);
        }

        public  bool UpdateAsync(Student model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Name)) {
                throw new ArgumentNullException(nameof(model.Name));
            }

            if (model.Id <= 0) {
                throw new ArgumentException(nameof(model.Id));
            }
            
            return _studentRepository.UpdateAsync(model);
        }

        public  bool DeleteAsync(int id) {
            if (id <= 0) {
                throw new ArgumentException(nameof(id));
            }

            return _studentRepository.DeleteAsync(id);
        }

        public  List<Student> GetRecommendListAsync(int topCount)
        {
            if(topCount == 0)
            {
                return null;
            }

            return _studentRepository.GetRecommendListAsync(topCount);
        }

        public  bool SetRecommend(int id, bool isRecommend)
        {
            return _studentRepository.SetRecommend(id, isRecommend);
        }

        public  IEnumerable<int> GetCoursesByIdAsync(int id)
        {
            return _studentRepository.GetCoursesByIdAsync(id);
        }

        public  bool SetCourses(int studentId, string courseIds)
        {
            return _studentRepository.SetCourses(studentId, courseIds);
        }
    }
}
