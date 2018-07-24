using Instart.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Common;

namespace Instart.Service
{
    public class UserService : ServiceBase, IUserService
    {
        IUserRepository _userRepository = AutofacRepository.Resolve<IUserRepository>();

        public UserService() {
            base.AddDisposableObject(_userRepository);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> GetByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return await _userRepository.GetByNameAsync(name);
        }

        public async Task<PageModel<User>> GetListAsync(int pageIndex, int pageSize)
        {
            return await _userRepository.GetListAsync(pageIndex, pageSize);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return await _userRepository.DeleteAsync(id);
        }

        public async Task<bool> UpdatePasswordAsync(int id, string password)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            string encryptPwd = Md5Helper.Encrypt(password);

            return await _userRepository.UpdatePasswordAsync(id, encryptPwd);
        }

        public async Task<bool> InsertAsync(User model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.UserName))
            {
                throw new ArgumentNullException(nameof(model.UserName));
            }

            model.Password = Md5Helper.Encrypt(model.UserName);
            model.CreateTime = DateTime.Now;
            model.ModifyTime = DateTime.Now;

            return await _userRepository.InsertAsync(model);
        }

        public async Task<bool> UpdateAsync(User model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.UserName))
            {
                throw new ArgumentNullException(nameof(model.UserName));
            }

            return await _userRepository.UpdateAsync(model);
        }
    }
}
