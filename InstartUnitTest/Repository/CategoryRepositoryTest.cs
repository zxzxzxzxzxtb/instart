using Instart.Models;
using Instart.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Instart.UnitTest.Repository
{
    public class CategoryRepositoryTest
    {
        ICategoryRepository _categoryRepository = AutofacRepository.Resolve<ICategoryRepository>();

        [Fact]
        public void Insert_Test()
        {
            Category model = new Category()
            {
                Name= "默认分类",
                Code = "default",
                GroupIndex=1,
                ParentId = 0, 
                CreateTime = DateTime.Now,
            };

            var result = _categoryRepository.InsertAsync(model);
            Assert.True(result);
        }

        [Fact]
        public void Update_Test()
        {
            Category model = new Category()
            {
                Id = 1,
                Name = "默认分类2",
                Code = "default2",
                GroupIndex = 2,
                ParentId = 0,
                CreateTime = DateTime.Now,
                ModifyTime = DateTime.Now,
            };

            var result = _categoryRepository.UpdateAsync(model);
            Assert.True(result);
        }

        [Fact]
        public void GetById_Test()
        {
            var category = _categoryRepository.GetByIdAsync(1);
            Assert.NotNull(category);
        }

        [Fact]
        public void GetList_Test()
        {
            var list = _categoryRepository.GetListAsync(1, 10, "");
            Assert.NotNull(list);
        }

        [Fact]
        public void Delete_Test()
        {
            var result = _categoryRepository.DeleteAsync(1);
            Assert.True(result);
        }
    }
}
