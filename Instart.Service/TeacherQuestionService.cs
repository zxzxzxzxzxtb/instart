using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Models.Enums;
using Instart.Repository;

namespace Instart.Service
{
    public class TeacherQuestionService : ServiceBase, ITeacherQuestionService
    {
        ITeacherQuestionRepository _teacherQuestionRepository = AutofacRepository.Resolve<ITeacherQuestionRepository>();

        public TeacherQuestionService()
        {
            base.AddDisposableObject(_teacherQuestionRepository);
        }

        public PageModel<TeacherQuestion> GetListAsync(int pageIndex, int pageSize, string name, EnumAccept accept)
        {
            return _teacherQuestionRepository.GetListAsync(pageIndex, pageSize, name, accept);
        }

        public bool InsertAsync(TeacherQuestion model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException("姓名不能为空");
            }

            if (string.IsNullOrEmpty(model.Phone))
            {
                throw new ArgumentNullException("手机不能为空");
            }

            return _teacherQuestionRepository.InsertAsync(model);
        }

        public bool SetAcceptAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id错误");
            }

            return _teacherQuestionRepository.SetAcceptAsync(id);
        }

        public List<TeacherQuestion> GetTopListAsync(int topCount)
        {
            if (topCount == 0)
            {
                return null;
            }

            return _teacherQuestionRepository.GetTopListAsync(topCount);
        }
    }
}
