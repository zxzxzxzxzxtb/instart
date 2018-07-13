using Instart.Common;
using Instart.Models;
using Instart.Service;
using Instart.Service.Base;
using Instart.Web.Attributes;
using Instart.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web.Areas.Manage.Controllers
{
    [AdminValidation]
    public class PartnerController : ManageControllerBase
    {
        IPartnerService _partnerService = AutofacService.Resolve<IPartnerService>();

        public PartnerController()
        {
            base.AddDisposableObject(_partnerService);
        }

        public async Task<ActionResult> Index(int page = 1, string name = null)
        {
            int pageSize = 10;
            var list = await _partnerService.GetListAsync(page, pageSize, name);
            ViewBag.Total = list.Total;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.Total * 1.0 / pageSize);
            return View(list.Data);
        }

        public async Task<ActionResult> Edit(int id = 0)
        {
            Partner model = new Partner();
            string action = "添加合作伙伴";

            if (id > 0)
            {
                model = await _partnerService.GetByIdAsync(id);
                action = "修改合作伙伴";
            }
            ViewBag.Action = action;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> AddOrUpdate(Partner model)
        {
            Stream uploadStream = null;
            FileStream fs = null;
            try
            {
                string msg = this.Validate(model, false);

                if (!string.IsNullOrEmpty(msg))
                {
                    return Error(msg);
                }
                //文件上传，一次上传1M的数据，防止出现大文件无法上传
                HttpPostedFileBase postFileBase = Request.Files["ImageUrl"];
                if (postFileBase != null && postFileBase.ContentLength != 0)
                {
                    uploadStream = postFileBase.InputStream;
                    int bufferLen = 1024;
                    byte[] buffer = new byte[bufferLen];
                    int contentLen = 0;

                    string fileName = Path.GetFileName(postFileBase.FileName);
                    string baseUrl = Server.MapPath("/");
                    string uploadPath = baseUrl + @"\Content\Images\Partner\";
                    fs = new FileStream(uploadPath + fileName, FileMode.Create, FileAccess.ReadWrite);

                    while ((contentLen = uploadStream.Read(buffer, 0, bufferLen)) != 0)
                    {
                        fs.Write(buffer, 0, bufferLen);
                        fs.Flush();
                    }

                    //保存页面数据，上传的文件只保存路径
                    string imgUrl = "/Content/Images/Partner/" + fileName;
                    model.ImageUrl = imgUrl;
                }
                if (model.Id > 0)
                {
                    await _partnerService.UpdateAsync(model);
                }
                else
                {
                    await _partnerService.InsertAsync(model);
                }
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
            finally
            {
                if (null != fs)
                {
                    fs.Close();
                }
                if (null != uploadStream)
                {
                    uploadStream.Close();
                }
            }
            return Json(new ResultBase
            {
                success = true
            });
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                return Json(new ResultBase
                {
                    success = await _partnerService.DeleteAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error($"PartnerController.Delete异常", ex);
                return Error(ex.Message);
            }
        }

        [NonAction]
        private string Validate(Partner model, bool isUpdate = false)
        {
            if (model == null)
            {
                return "参数错误。";
            }

            if (isUpdate && model.Id <= 0)
            {
                return "Id不正确。";
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                return "名称不能为空。";
            }

            return string.Empty;
        }
    }
}