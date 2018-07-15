
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Models.Enums;
using Instart.Repository;

namespace Instart.Service
{
    public class BannerService : ServiceBase, IBannerService
    {
        IBannerRepository _bannerRepository = AutofacRepository.Resolve<IBannerRepository>();

        public BannerService()
        {
            base.AddDisposableObject(_bannerRepository);
        }

        public async Task<Banner> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return await _bannerRepository.GetByIdAsync(id);
        }

        public async Task<PageModel<Banner>> GetListAsync(int pageIndex, int pageSize, string title = null)
        {
            return await _bannerRepository.GetListAsync(pageIndex, pageSize, title);
        }

        public async Task<List<Banner>> GetListByPosAsync(EnumBannerPos pos = EnumBannerPos.Index)
        {
            return await _bannerRepository.GetListByPosAsync(pos);
        }

        public async Task<bool> InsertAsync(Banner model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Title))
            {
                throw new ArgumentNullException(nameof(model.Title));
            }

            return await _bannerRepository.InsertAsync(model);
        }

        public async Task<bool> UpdateAsync(Banner model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Title))
            {
                throw new ArgumentNullException(nameof(model.Title));
            }

            if (model.Id <= 0)
            {
                throw new ArgumentException(nameof(model.Id));
            }

            return await _bannerRepository.UpdateAsync(model);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return await _bannerRepository.DeleteAsync(id);
        }

        public async Task<List<Banner>> GetBannerListByPosAsync(EnumBannerPos pos, int topCount = 20)
        {
            if (topCount == 0)
            {
                return null;
            }

            return await _bannerRepository.GetBannerListByPosAsync(pos, topCount);
        }

        public async Task<bool> SetShowAsync(int id, bool isShow)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return await _bannerRepository.SetShowAsync(id, isShow);
        }

    }
}
