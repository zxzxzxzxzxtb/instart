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
        public async Task GetById_Test()
        {
            var user = await _userRepository.GetByIdAsync(1);
            Assert.NotNull(user);
        }

        [Fact]
        public async Task GetByName_Test()
        {
            var user = await _userRepository.GetByNameAsync("admin");
            Assert.NotNull(user);
        }

        [Fact]
        public async Task GetList_Test()
        {
            var user = await _userRepository.GetListAsync(1, 10);
            Assert.NotNull(user);
        }

        [Fact]
        public async Task Insert_Test()
        {
            var result = await _userRepository.InsertAsync(new Models.User
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
        public async Task UpdatePassword_Test()
        {
            var result = await _userRepository.UpdatePasswordAsync(2, "666666");
            Assert.True(result);
        }

        [Fact]
        public async Task Delete_Test()
        {
            var result = await _userRepository.DeleteAsync(2);
            Assert.True(result);
        }
    }
}
