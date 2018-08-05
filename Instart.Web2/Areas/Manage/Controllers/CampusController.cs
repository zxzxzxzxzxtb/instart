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
    public class CampusController : ManageControllerBase
    {
        ICampusService _campusService = AutofacService.Resolve<ICampusService>();

        public CampusController()
        {
            base.AddDisposableObject(_campusService);
        }

        public ActionResult Index(int page = 1, string keyword = null)
        {
            int pageSize = 10;
            var list = _campusService.GetListAsync(page, pageSize, keyword);
            ViewBag.Total = list.Total;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.Total * 1.0 / pageSize);
            ViewBag.Keyword = keyword;
            return View(list.Data);
        }

        public ActionResult Edit(int id = 0)
        {
            Campus model = new Campus();
            string action = "添加校区";

            if (id > 0)
            {
                model = _campusService.GetByIdAsync(id);
                action = "修改校区";
            }

            ViewBag.Action = action;
            return View(model);
        }

        [HttpPost]
        [Operation("设置学区")]
        [ValidateInput(false)]
        public JsonResult Set(Campus model, List<HttpPostedFileBase> imgs)
        {
            if (model == null)
            {
                return Error("参数错误。");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                return Error("校区名称不能为空。");
            }

            model.Name = model.Name.Trim();

            var avatarFile = Request.Files["fileAvatar"];

            if (avatarFile != null)
            {
                string uploadResult = UploadHelper.Process(avatarFile.FileName, avatarFile.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.Avatar = uploadResult;
                }
            }

            var bannerFile = Request.Files["fileBanner"];

            if (bannerFile != null)
            {
                string uploadResult = UploadHelper.Process(bannerFile.FileName, bannerFile.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.BannerImg = uploadResult;
                }
            }
            var result = new ResultBase();

            if (model.Id > 0)
            {
                result.success = _campusService.UpdateAsync(model);
            }
            else
            {
                result.success = _campusService.InsertAsync(model);
            }

            return Json(result);
        }

        [HttpPost]
        [Operation("删除学区")]
        public JsonResult Delete(int id)
        {
            try
            {
                return Json(new ResultBase
                {
                    success = _campusService.DeleteAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("CampusController.Delete异常", ex);
                return Error(ex.Message);
            }
        }

        public ActionResult ImgIndex(int campusId)
        {
            ViewBag.CampusId = campusId;
            ViewBag.ImgList = _campusService.GetImgsByCampusIdAsync(campusId);
            return View();
        }

        [HttpPost]
        [Operation("设置学区图片")]
        public JsonResult SetImg(CampusImg model)
        {
            if (model == null)
            {
                return Error("参数错误。");
            }
            var campusImg = Request.Files["campusImg"];

            if (campusImg == null)
            {
                return Error("请选择图片。");
            }
            string uploadResult = UploadHelper.Process(campusImg.FileName, campusImg.InputStream);
            if (!string.IsNullOrEmpty(uploadResult))
            {
                model.ImgUrl = uploadResult;
            }

            var result = new ResultBase();
            result.success = _campusService.InsertImgAsync(model);

            return Json(result);
        }

        [HttpPost]
        [Operation("删除学区图片")]
        public JsonResult DeleteImg(int id)
        {
            try
            {
                return Json(new ResultBase
                {
                    success = _campusService.DeleteImgAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("CampusController.DeleteImg异常", ex);
                return Error(ex.Message);
            }
        }
    }
}