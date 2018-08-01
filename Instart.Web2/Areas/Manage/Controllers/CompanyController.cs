using Instart.Common;
using Instart.Models;
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
    public class CompanyController : ManageControllerBase
    {
        ICompanyService _companyService = AutofacService.Resolve<ICompanyService>();

        public CompanyController()
        {
            base.AddDisposableObject(_companyService);
        }

        public ActionResult Index(int page = 1, string keyword = null)
        {
            int pageSize = 10;
            var list = _companyService.GetListAsync(page, pageSize, keyword);
            ViewBag.Total = list.Total;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.Total * 1.0 / pageSize);
            ViewBag.Keyword = keyword;
            return View(list.Data);
        }

        public ActionResult Edit(int id = 0)
        {
            Company model = new Company();
            string action = "添加实习单位";

            if (id > 0)
            {
                model = _companyService.GetByIdAsync(id);
                action = "修改实习单位";
            }

            ViewBag.Action = action;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [Operation("设置实习单位")]
        public JsonResult Set(Company model)
        {
            if (model == null)
            {
                return Error("参数错误。");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                return Error("单位名称不能为空。");
            }

            model.Name = model.Name.Trim();

            var fileAvatar = Request.Files["fileAvatar"];

            if (fileAvatar != null)
            {
                string uploadResult = UploadHelper.Process(fileAvatar.FileName, fileAvatar.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.ImgUrl = uploadResult;
                }
            }

            var fileEnvironment = Request.Files["fileEnvironment"];

            if (fileEnvironment != null)
            {
                string uploadResult = UploadHelper.Process(fileEnvironment.FileName, fileEnvironment.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.Environment = uploadResult;
                }
            }

            var fileWorks = Request.Files["fileWorks"];

            if (fileWorks != null)
            {
                string uploadResult = UploadHelper.Process(fileWorks.FileName, fileWorks.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.Works = uploadResult;
                }
            }
            var result = new ResultBase();

            if (model.Id > 0)
            {
                result.success = _companyService.UpdateAsync(model);
            }
            else
            {
                result.success = _companyService.InsertAsync(model);
            }

            return Json(result);
        }

        [HttpPost]
        [Operation("删除实习单位")]
        public JsonResult Delete(int id)
        {
            try
            {
                return Json(new ResultBase
                {
                    success = _companyService.DeleteAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("CompanyController.Delete异常", ex);
                return Error(ex.Message);
            }
        }
    }
}