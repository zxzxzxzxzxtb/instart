using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Repository;

namespace Instart.Service
{
    public class StudioService : ServiceBase, IStudioService
    {
        IStudioRepository _studioRepository = AutofacRepository.Resolve<IStudioRepository>();

        public StudioService()
        {
            base.AddDisposableObject(_studioRepository);
        }

        public int GetCountAsync()
        {
            return _studioRepository.GetCountAsync();
        }

        public Studio GetInfoAsync()
        {
            return _studioRepository.GetInfoAsync();
        }

        public bool InsertAsync(Studio model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            return _studioRepository.InsertAsync(model);
        }

        public bool UpdateAsync(Studio model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            return _studioRepository.UpdateAsync(model);
        }

        public IEnumerable<StudioImg> GetImgsAsync()
        {
            return _studioRepository.GetImgsAsync();
        }

        public bool InsertImgAsync(string imgUrl)
        {
            return _studioRepository.InsertImgAsync(imgUrl);
        }

        public bool DeleteImgAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _studioRepository.DeleteImgAsync(id);
        }
    }
}
