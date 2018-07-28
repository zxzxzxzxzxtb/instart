using Instart.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Instart.UnitTest.Repository
{
    public class UserRepositoryTest
    {
        IUserRepository _userRepository = AutofacRepository.Resolve<IUserRepository>();

        [Fact]
        public void GetById_Test()
        {
            var user = _userRepository.GetByIdAsync(1);
            Assert.NotNull(user);
        }

        [Fact]
        public void GetByName_Test()
        {
            var user = _userRepository.GetByNameAsync("admin");
            Assert.NotNull(user);
        }

        [Fact]
        public void GetList_Test()
        {
            var user = _userRepository.GetListAsync(1, 10);
            Assert.NotNull(user);
        }

        [Fact]
        public void Insert_Test()
        {
            var result = _userRepository.InsertAsync(new Models.User
            {
                UserName = "test",
                Password = "123456",
                Role = 1,
                Status = 1,
                CreateTime = DateTime.Now,
                ModifyTime = DateTime.Now
            });
            Assert.True(result);
        }

        [Fact]
        public void UpdatePassword_Test()
        {
            var result = _userRepository.UpdatePasswordAsync(2, "666666");
            Assert.True(result);
        }

        [Fact]
        public void Delete_Test()
        {
            var result = _userRepository.DeleteAsync(2);
            Assert.True(result);
        }
    }
}
