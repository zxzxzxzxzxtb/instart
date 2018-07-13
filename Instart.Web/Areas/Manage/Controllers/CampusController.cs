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
    public class CampusController : ManageControllerBase
    {
        ICampusService _campusService = AutofacService.Resolve<ICampusService>();

        public CampusController()
        {
            base.AddDisposableObject(_campusService);
        }

        public async Task<ActionResult> Index(int page = 1, string keyword = null)
        {
            int pageSize = 10;
            var list = await _campusService.GetListAsync(page, pageSize, keyword);
            ViewBag.Total = list.Total;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.Total * 1.0 / pageSize);
            ViewBag.Keyword = keyword;
            return View(list.Data);
        }

        public async Task<ActionResult> Edit(int id = 0)
        {
            Campus model = new Campus();
            string action = "添加校区";

            if (id > 0)
            {
                model = await _campusService.GetByIdAsync(id);
                action = "修改校区";
            }

            ViewBag.Action = action;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> AddOrUpdate(Campus model, List<HttpPostedFileBase> imgs)
        {
            int bufferLen = 1024;
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
                if (imgs != null && imgs.Count != 0)
                {
                    if (model.ImgUrls == null)
                    {
                        model.ImgUrls = new List<String>();
                    }
                    foreach (var img in imgs)
                    {
                        if (img != null && img.ContentLength != 0)
                        {
                            uploadStream = img.InputStream;
                            byte[] buffer = new byte[bufferLen];
                            int contentLen = 0;

                            string fileName = Path.GetFileName(img.FileName);
                            string baseUrl = Server.MapPath("/");
                            string uploadPath = baseUrl + @"\Content\Images\Campus\";
                            fs = new FileStream(uploadPath + fileName, FileMode.Create, FileAccess.ReadWrite);

                            while ((contentLen = uploadStream.Read(buffer, 0, bufferLen)) != 0)
                            {
                                fs.Write(buffer, 0, bufferLen);
                                fs.Flush();
                            }

                            //保存页面数据，上传的文件只保存路径
                            string imgUrl = "/Content/Images/Campus/" + fileName;
                            (model.ImgUrls as List<string>).Add(imgUrl);
                        }
                    }
                }
                if (model.Id > 0)
                {
                    await _campusService.UpdateAsync(model);
                }
                else
                {
                    await _campusService.InsertAsync(model);
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
                    success = await _campusService.DeleteAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error($"CampusController.Delete异常", ex);
                return Error(ex.Message);
            }
        }

        [NonAction]
        private string Validate(Campus model, bool isUpdate = false)
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
                return "校区名称不能为空。";
            }

            return string.Empty;
        }

        [HttpPost]
        public async Task<JsonResult> DeleteFile(int id, string imgUrl)
        {
            try
            {
                return Json(new ResultBase
                {
                    success = await _campusService.DeleteImgAsync(id, imgUrl)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error($"CampusController.DeleteImg异常", ex);
                return Error(ex.Message);
            }
        }
    }
}