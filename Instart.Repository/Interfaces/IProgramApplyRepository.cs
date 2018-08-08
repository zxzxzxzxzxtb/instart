﻿using Instart.Models;
using Instart.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public interface IProgramApplyRepository
    {
        PageModel<ProgramApply> GetListAsync(int pageIndex, int pageSize, string programName, EnumAccept accept);

        bool SetAcceptAsync(int id);

        bool InsertAsync(ProgramApply model);

        List<ProgramApply> GetTopListAsync(int topCount);
    }
}
