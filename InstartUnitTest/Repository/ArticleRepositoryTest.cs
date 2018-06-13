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
    }
}
