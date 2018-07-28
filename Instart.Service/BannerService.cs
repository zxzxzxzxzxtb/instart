
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

        public Banner GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return _bannerRepository.GetByIdAsync(id);
        }

        public PageModel<Banner> GetListAsync(int pageIndex, int pageSize, string title = null, int pos = 1, int type = -1)
        {
            return _bannerRepository.GetListAsync(pageIndex, pageSize, title, pos, type);
        }

        public List<Banner> GetListByPosAsync(EnumBannerPos pos = EnumBannerPos.Index)
        {
            return _bannerRepository.GetListByPosAsync(pos);
        }

        public bool InsertAsync(Banner model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Title))
            {
                throw new ArgumentNullException(nameof(model.Title));
            }

            return _bannerRepository.InsertAsync(model);
        }

        public bool UpdateAsync(Banner model)
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

            return _bannerRepository.UpdateAsync(model);
        }

        public bool DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return _bannerRepository.DeleteAsync(id);
        }

        public List<Banner> GetBannerListByPosAsync(EnumBannerPos pos, int topCount = 20)
        {
            if (topCount == 0)
            {
                return null;
            }

            return _bannerRepository.GetBannerListByPosAsync(pos, topCount);
        }

        public bool SetShowAsync(int id, bool isShow)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return _bannerRepository.SetShowAsync(id, isShow);
        }

    }
}
