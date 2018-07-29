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
    public class PartnerController : ManageControllerBase
    {
        IPartnerService _partnerService = AutofacService.Resolve<IPartnerService>();

        public PartnerController()
        {
            base.AddDisposableObject(_partnerService);
        }

        public ActionResult Index(int page = 1, string keyword = null)
        {
            int pageSize = 10;
            var list = _partnerService.GetListAsync(page, pageSize, keyword);
            ViewBag.Total = list.Total;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.Total * 1.0 / pageSize);
            ViewBag.Keyword = keyword;
            return View(list.Data);
        }

        public ActionResult Edit(int id = 0)
        {
            Partner model = new Partner();
            string action = "添加合作伙伴";

            if (id > 0)
            {
                model = _partnerService.GetByIdAsync(id);
                action = "修改合作伙伴";
            }
            ViewBag.Action = action;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [Operation("设置合作伙伴")]
        public JsonResult Set(Partner model)
        {
            if (model == null)
            {
                return Error("参数错误。");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                return Error("合作伙伴名称不能为空。");
            }

            model.Name = model.Name.Trim();

            var filePartner = Request.Files["filePartner"];

            if (filePartner != null)
            {
                string uploadResult = UploadHelper.Process(filePartner.FileName, filePartner.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.ImageUrl = uploadResult;
                }
            }
            var result = new ResultBase();

            if (model.Id > 0)
            {
                result.success = _partnerService.UpdateAsync(model);
            }
            else
            {
                result.success = _partnerService.InsertAsync(model);
            }

            return Json(result);
        }

        [HttpPost]
        [Operation("删除合作伙伴")]
        public JsonResult Delete(int id)
        {
            try
            {
                return Json(new ResultBase
                {
                    success = _partnerService.DeleteAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("PartnerController.Delete异常", ex);
                return Error(ex.Message);
            }
        }
    }
}