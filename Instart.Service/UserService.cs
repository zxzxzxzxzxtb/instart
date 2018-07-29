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

        public UserService()
        {
            base.AddDisposableObject(_userRepository);
        }

        public User GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _userRepository.GetByIdAsync(id);
        }

        public User GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _userRepository.GetById(id);
        }

        public User GetByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name不能为null");
            }

            return _userRepository.GetByNameAsync(name);
        }

        public PageModel<User> GetListAsync(int pageIndex, int pageSize)
        {
            return _userRepository.GetListAsync(pageIndex, pageSize);
        }

        public bool DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _userRepository.DeleteAsync(id);
        }

        public bool UpdatePasswordAsync(int id, string password)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password不能为空");
            }

            string encryptPwd = Md5Helper.Encrypt(password);

            return _userRepository.UpdatePasswordAsync(id, encryptPwd);
        }

        public bool InsertAsync(User model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.UserName))
            {
                throw new ArgumentNullException("UserName错误");
            }

            model.Password = Md5Helper.Encrypt(model.UserName);
            model.CreateTime = DateTime.Now;
            model.ModifyTime = DateTime.Now;

            return _userRepository.InsertAsync(model);
        }

        public bool UpdateAsync(User model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.UserName))
            {
                throw new ArgumentNullException("UserName错误");
            }

            return _userRepository.UpdateAsync(model);
        }
    }
}
