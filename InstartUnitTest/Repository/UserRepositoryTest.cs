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
    }
}
