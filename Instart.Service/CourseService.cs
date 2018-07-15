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

        public CourseService()
        {
            base.AddDisposableObject(_courseRepository);
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return await _courseRepository.GetByIdAsync(id);
        }

        public async Task<PageModel<Course>> GetListAsync(int pageIndex, int pageSize, string name = null)
        {
            return await _courseRepository.GetListAsync(pageIndex, pageSize, name);
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _courseRepository.GetAllAsync();
        }

        public async Task<bool> InsertAsync(Course model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException(nameof(model.Name));
            }

            return await _courseRepository.InsertAsync(model);
        }

        public async Task<bool> UpdateAsync(Course model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException(nameof(model.Name));
            }

            if (model.Id <= 0)
            {
                throw new ArgumentException(nameof(model.Id));
            }

            return await _courseRepository.UpdateAsync(model);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return await _courseRepository.DeleteAsync(id);
        }

        public async Task<List<Course>> GetRecommendListAsync(int topCount)
        {
            if(topCount == 0)
            {
                return null;
            }

            return await _courseRepository.GetRecommendListAsync(topCount);
        }

        public Task<bool> SetRecommendAsync(int id, bool isRecommend)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return _courseRepository.SetRecommend(id, isRecommend);
        }

        public async Task<int> GetInfoCountAsync()
        {
            return await _courseRepository.GetInfoCountAsync();
        }

        public async Task<CourseInfo> GetInfoAsync()
        {
            return await _courseRepository.GetInfoAsync();
        }

        public async Task<bool> InsertInfoAsync(CourseInfo model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return await _courseRepository.InsertInfoAsync(model);
        }

        public async Task<bool> UpdateInfoAsync(CourseInfo model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return await _courseRepository.UpdateInfoAsync(model);
        }

        public async Task<IEnumerable<int>> GetTeachersByIdAsync(int id)
        {
            return await _courseRepository.GetTeachersByIdAsync(id);
        }

        public async Task<bool> SetTeachers(int courseId, string teacherIds)
        {
            return await _courseRepository.SetTeachers(courseId, teacherIds);
        }
    }
}
