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

        public async Task<Student> GetByIdAsync(int id) {
            if (id <= 0) {
                throw new ArgumentException(nameof(id));
            }

            return await _studentRepository.GetByIdAsync(id);
        }

        public async Task<PageModel<Student>> GetListAsync(int pageIndex, int pageSize, int division = -1, string name = null) {
            return await _studentRepository.GetListAsync(pageIndex, pageSize, division, name);
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _studentRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Student>> GetStarStudentsAsync()
        {
            return await _studentRepository.GetStarStudentsAsync();
        }

        public async Task<bool> InsertAsync(Student model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Name)) {
                throw new ArgumentNullException(nameof(model.Name));
            }

            return await _studentRepository.InsertAsync(model);
        }

        public async Task<bool> UpdateAsync(Student model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Name)) {
                throw new ArgumentNullException(nameof(model.Name));
            }

            if (model.Id <= 0) {
                throw new ArgumentException(nameof(model.Id));
            }
            
            return await _studentRepository.UpdateAsync(model);
        }

        public async Task<bool> DeleteAsync(int id) {
            if (id <= 0) {
                throw new ArgumentException(nameof(id));
            }

            return await _studentRepository.DeleteAsync(id);
        }

        public async Task<List<Student>> GetRecommendListAsync(int topCount)
        {
            if(topCount == 0)
            {
                return null;
            }

            return await _studentRepository.GetRecommendListAsync(topCount);
        }

        public async Task<bool> SetRecommend(int id, bool isRecommend)
        {
            return await _studentRepository.SetRecommend(id, isRecommend);
        }

        public async Task<IEnumerable<int>> GetCoursesByIdAsync(int id)
        {
            return await _studentRepository.GetCoursesByIdAsync(id);
        }

        public async Task<bool> SetCourses(int studentId, string courseIds)
        {
            return await _studentRepository.SetCourses(studentId, courseIds);
        }
    }
}
