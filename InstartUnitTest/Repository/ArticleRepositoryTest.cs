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
    public class ArticleRepositoryTest
    {
        IArticleRepository _articleRepository = AutofacRepository.Resolve<IArticleRepository>();

        [Fact]
        public void Insert_Test()
        {
            Article model = new Article()
            {
                Title = "test",
                SubTitle = "sub",
                Author = "feifei",
                Source = "baidu",
                CategoryId = 1,
                CategoryName = "默认分类",
                Content = "hehe,this is test content",
                CreateTime = DateTime.Now,
            };

            var result = _articleRepository.InsertAsync(model);
            Assert.True(result);
        }

        [Fact]
        public void Update_Test()
        {
            Article model = new Article()
            {
                Id = 1,
                Title = "test2",
                SubTitle = "sub2",
                Author = "feifei2",
                Source = "baidu2",
                CategoryId = 2,
                CategoryName = "默认分类2",
                Content = "hehe,this is test content2",
                CreateTime = DateTime.Now,
            };

            var result = _articleRepository.UpdateAsync(model);
            Assert.True(result);
        }

        [Fact]
        public void GetById_Test()
        {
            var article = _articleRepository.GetByIdAsync(1);
            Assert.NotNull(article);
        }

        [Fact]
        public void GetList_Test()
        {
            var article = _articleRepository.GetListAsync(1, 10 , 2);
            Assert.NotNull(article);
        }

        [Fact]
        public void Delete_Test()
        {
            var result = _articleRepository.DeleteAsync(1);
            Assert.True(result);
        }
    }
}
