using Instart.Common;
using Instart.Models;
using Instart.Models.Enums;
using Instart.Service;
using Instart.Service.Base;
using Instart.Web2.Attributes;
using Instart.Web2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web2.Areas.Manage.Controllers
{
    [AdminValidation]
    public class ProgramController : ManageControllerBase
    {
        IProgramService _programService = AutofacService.Resolve<IProgramService>();

        public ProgramController()
        {
            base.AddDisposableObject(_programService);
        }

        public ActionResult Index(int page = 1, int type = -1, string keyword = null)
        {
            int pageSize = 10;
            var list = _programService.GetListAsync(page, pageSize, type, keyword);
            ViewBag.Total = list.Total;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.Total * 1.0 / pageSize);
            ViewBag.Keyword = keyword;
            ViewBag.Type = type;
            ViewBag.TypeList = EnumberHelper.EnumToList<EnumProgramType>();
            return View(list.Data);
        }

        public ActionResult Edit(int id = 0)
        {
            Program model = new Program();
            string action = "添加项目";

            if (id > 0)
            {
                model = _programService.GetByIdAsync(id);
                action = "修改项目";
            }

            ViewBag.Action = action;
            ViewBag.TypeList = EnumberHelper.EnumToList<EnumProgramType>();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Set(Program model)
        {
            if (model == null)
            {
                return Error("参数错误");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                return Error("项目名称不能为空");
            }
            var fileProgram = Request.Files["fileProgram"];

            if (fileProgram != null)
            {
                string uploadResult = UploadHelper.Process(fileProgram.FileName, fileProgram.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.Picture = uploadResult;
                }
            }
            var result = new ResultBase();

            if (model.Id > 0)
            {
                result.success = _programService.UpdateAsync(model);
            }
            else
            {
                result.success = _programService.InsertAsync(model);
            }

            return Json(result);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                return Json(new ResultBase
                {
                    success = _programService.DeleteAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("ProgramController.Delete异常", ex);
                return Error(ex.Message);
            }
        }
    }
}