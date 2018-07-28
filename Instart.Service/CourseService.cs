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

        public Course GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return _courseRepository.GetByIdAsync(id);
        }

        public PageModel<Course> GetListAsync(int pageIndex, int pageSize, string name = null)
        {
            return _courseRepository.GetListAsync(pageIndex, pageSize, name);
        }

        public IEnumerable<Course> GetAllAsync()
        {
            return _courseRepository.GetAllAsync();
        }

        public bool InsertAsync(Course model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException(nameof(model.Name));
            }

            return _courseRepository.InsertAsync(model);
        }

        public bool UpdateAsync(Course model)
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

            return _courseRepository.UpdateAsync(model);
        }

        public bool DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return _courseRepository.DeleteAsync(id);
        }

        public List<Course> GetRecommendListAsync(int topCount)
        {
            if (topCount == 0)
            {
                return null;
            }

            return _courseRepository.GetRecommendListAsync(topCount);
        }

        public bool SetRecommendAsync(int id, bool isRecommend)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return _courseRepository.SetRecommend(id, isRecommend);
        }

        public int GetInfoCountAsync()
        {
            return _courseRepository.GetInfoCountAsync();
        }

        public CourseInfo GetInfoAsync()
        {
            return _courseRepository.GetInfoAsync();
        }

        public bool InsertInfoAsync(CourseInfo model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return _courseRepository.InsertInfoAsync(model);
        }

        public bool UpdateInfoAsync(CourseInfo model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return _courseRepository.UpdateInfoAsync(model);
        }

        public IEnumerable<int> GetTeachersByIdAsync(int id)
        {
            return _courseRepository.GetTeachersByIdAsync(id);
        }

        public bool SetTeachers(int courseId, string teacherIds)
        {
            return _courseRepository.SetTeachers(courseId, teacherIds);
        }
    }
}
