using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Repository;

namespace Instart.Service
{
    public class SchoolService : ServiceBase, ISchoolService
    {
        ISchoolRepository _schoolRepository = AutofacRepository.Resolve<ISchoolRepository>();

        public SchoolService()
        {
            base.AddDisposableObject(_schoolRepository);
        }

        public async Task<School> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return await _schoolRepository.GetByIdAsync(id);
        }

        public async Task<PageModel<School>> GetListAsync(int pageIndex, int pageSize, string name = null)
        {
            return await _schoolRepository.GetListAsync(pageIndex, pageSize, name);
        }

        public async Task<IEnumerable<School>> GetAllAsync()
        {
            return await _schoolRepository.GetAllAsync();
        }

        public async Task<bool> InsertAsync(School model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException(nameof(model.Name));
            }
            return await _schoolRepository.InsertAsync(model);
        }

        public async Task<bool> UpdateAsync(School model)
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

            return await _schoolRepository.UpdateAsync(model);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return await _schoolRepository.DeleteAsync(id);
        }

        public async Task<List<School>> GetRecommendListAsync(int topCount)
        {
            if (topCount == 0)
            {
                return null;
            }

            return await _schoolRepository.GetRecommendListAsync(topCount);
        }

        public Task<bool> SetRecommendAsync(int id, bool isRecommend)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return _schoolRepository.SetRecommendAsync(id, isRecommend);
        }

        public async Task<List<School>> GetHotListAsync(int topCount)
        {
            if (topCount == 0)
            {
                return null;
            }

            return await _schoolRepository.GetHotListAsync(topCount);
        }

        public Task<bool> SetHotAsync(int id, bool isHot)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return _schoolRepository.SetHotAsync(id, isHot);
        }

        public async Task<PageModel<School>> GetListAsync(int pageIndex, int pageSize, string name = null, int country = -1, int major = -1)
        {
            return await _schoolRepository.GetListAsync(pageIndex, pageSize, name, country, major);
        }
    }
}
