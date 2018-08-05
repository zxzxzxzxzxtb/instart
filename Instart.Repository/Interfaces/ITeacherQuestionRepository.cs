using Instart.Models;
using Instart.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public interface ITeacherQuestionRepository
    {
        PageModel<TeacherQuestion> GetListAsync(int pageIndex, int pageSize, string name, EnumAccept accept);

        bool SetAcceptAsync(int id);

        bool InsertAsync(TeacherQuestion model);

        List<TeacherQuestion> GetTopListAsync(int topCount);
    }
}
