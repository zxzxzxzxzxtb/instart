using Instart.Common;
using Instart.Models;
using Instart.Service;
using Instart.Service.Base;
using Instart.Web2.Attributes;
using Instart.Web2.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Instart.Web2.Areas.Manage.Controllers
{
    [AdminValidation]
    public class UserController : ManageControllerBase
    {
        IUserService _userService = AutofacService.Resolve<IUserService>();

        public UserController()
        {
            base.AddDisposableObject(_userService);
        }

        public ActionResult Index(int page = 1)
        {
            int pageSize = 10;
            var list = _userService.GetListAsync(page, pageSize);
            ViewBag.Total = list.Total;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.Total * 1.0 / pageSize);
            return View(list.Data);
        }

        public ActionResult Add()
        {
            User model = new User();
            return View(model);
        }

        [HttpPost]
        public JsonResult Set(User model)
        {
            if (model == null)
            {
                return Error("参数错误");
            }

            var fileAvatar = Request.Files["fileAvatar"];

            if (fileAvatar != null)
            {
                string uploadResult = UploadHelper.Process(fileAvatar.FileName, fileAvatar.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.Avatar = uploadResult;
                }
            }
            var result = new ResultBase();
            if (model.Id > 0)
            {
                result.success = _userService.UpdateAsync(model);
            }
            else
            {
                result.success = _userService.InsertAsync(model);
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
                    success = _userService.DeleteAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("UserController.Delete异常", ex);
                return Error(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult UpdatePassword(int userId, string oldPwd, string newPwd)
        {
            User user = _userService.GetByIdAsync(userId);
            if (user == null || Md5Helper.Encrypt(oldPwd) != user.Password)
            {
                return Error("旧密码错误");
            }

            return Json(new ResultBase
            {
                success = _userService.UpdatePasswordAsync(userId, newPwd)
            });
        }

        public ActionResult EditInfo()
        {
            Instart.Models.User model;
            if (Session[WebAppSettings.SessionName] != null)
            {
                LoginUser user = (LoginUser)Session[WebAppSettings.SessionName];
                model = (_userService.GetByIdAsync(user.UserId)) ?? new Instart.Models.User();
            }
            else
            {
                model = new Instart.Models.User();
            }
            return View(model);
        }

        public ActionResult UpdatePwd()
        {
            int id = 0;
            if (Session[WebAppSettings.SessionName] != null)
            {
                LoginUser user = (LoginUser)Session[WebAppSettings.SessionName];
                id = user.UserId;
            }
            ViewBag.Id = id;
            return View();
        }
    }
}