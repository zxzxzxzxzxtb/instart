using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Repository;

namespace Instart.Service
{
    public class TeacherService : ServiceBase, ITeacherService
    {
        ITeacherRepository _teacherRepository = AutofacRepository.Resolve<ITeacherRepository>();

        public TeacherService() {
            base.AddDisposableObject(_teacherRepository);
        }

        public async Task<Teacher> GetByIdAsync(int id) {
            if (id <= 0) {
                throw new ArgumentException(nameof(id));
            }

            return await _teacherRepository.GetByIdAsync(id);
        }

        public async Task<PageModel<Teacher>> GetListAsync(int pageIndex, int pageSize, int division = -1, string name = null) {
            return await _teacherRepository.GetListAsync(pageIndex, pageSize, division, name);
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            return await _teacherRepository.GetAllAsync();
        }

        public async Task<bool> InsertAsync(Teacher model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Name)) {
                throw new ArgumentNullException(nameof(model.Name));
            }

            return await _teacherRepository.InsertAsync(model);
        }

        public async Task<bool> UpdateAsync(Teacher model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Name)) {
                throw new ArgumentNullException(nameof(model.Name));
            }

            if (model.Id <= 0) {
                throw new ArgumentException(nameof(model.Id));
            }
            
            return await _teacherRepository.UpdateAsync(model);
        }

        public async Task<bool> DeleteAsync(int id) {
            if (id <= 0) {
                throw new ArgumentException(nameof(id));
            }

            return await _teacherRepository.DeleteAsync(id);
        }

        public async Task<List<Teacher>> GetRecommendListAsync(int topCount)
        {
            if(topCount == 0)
            {
                return null;
            }

            return await _teacherRepository.GetRecommendListAsync(topCount);
        }

        public Task<bool> SetRecommendAsync(int id, bool isRecommend)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return _teacherRepository.SetRecommend(id, isRecommend);
        }

        public async Task<IEnumerable<int>> GetCoursesByIdAsync(int id)
        {
            return await _teacherRepository.GetCoursesByIdAsync(id);
        }

        public async Task<bool> SetCourses(int teacherId, string courseIds)
        {
            return await _teacherRepository.SetCourses(teacherId, courseIds);
        }

        public async Task<PageModel<Teacher>> GetListByDivsionAsync(int divisionId, int pageIndex, int pageSize)
        {
            if(divisionId <= 0)
            {
                throw new ArgumentException(nameof(divisionId));
            }

            return await _teacherRepository.GetListByDivsionAsync(divisionId, pageIndex, pageSize);
        }
    }
}
