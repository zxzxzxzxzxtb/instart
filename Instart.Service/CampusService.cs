using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Repository;

namespace Instart.Service
{
    public class CampusService : ServiceBase, ICampusService
    {
        ICampusRepository _campusRepository = AutofacRepository.Resolve<ICampusRepository>();

        public CampusService()
        {
            base.AddDisposableObject(_campusRepository);
        }

        public async Task<Campus> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }
            Campus model = await _campusRepository.GetByIdAsync(id);
            IEnumerable<String> imgs = await _campusRepository.GetImgsByIdAsync(id);
            model.ImgUrls = imgs;

            return model;
        }

        public async Task<PageModel<Campus>> GetListAsync(int pageIndex, int pageSize, string name = null)
        {
            return await _campusRepository.GetListAsync(pageIndex, pageSize, name);
        }

        public async Task<IEnumerable<Campus>> GetAllAsync()
        {
            return await _campusRepository.GetAllAsync();
        }

        public async Task<bool> InsertAsync(Campus model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException(nameof(model.Name));
            }

            return await _campusRepository.InsertAsync(model);
        }

        public async Task<bool> UpdateAsync(Campus model)
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

            return await _campusRepository.UpdateAsync(model);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return await _campusRepository.DeleteAsync(id);
        }

        public async Task<bool> DeleteImgAsync(int id, string imgUrl)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return await _campusRepository.DeleteImgAsync(id, imgUrl);
        }
    }
}
