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
    public class MajorController : ManageControllerBase
    {
        IMajorService _majorService = AutofacService.Resolve<IMajorService>();
        IDivisionService _divisionService = AutofacService.Resolve<IDivisionService>();

        public MajorController()
        {
            base.AddDisposableObject(_majorService);
        }

        public async Task<ActionResult> Index(int page = 1, string name = null)
        {
            int pageSize = 2;
            var list = await _majorService.GetListAsync(page, pageSize, name);
            ViewBag.Total = list.Total;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.Total * 1.0 / pageSize);
            return View(list.Data);
        }

        public async Task<ActionResult> Edit()
        {
            int id = Request.QueryString["id"].ToInt32();
            Major model;
            if (id > 0)
            {
                model = await _majorService.GetByIdAsync(id);
            }
            else
            {
                model = new Major();
            }
            List<SelectListItem> typeList = new List<SelectListItem>();
            typeList.Add(new SelectListItem { Text = "本科", Value = "0" });
            typeList.Add(new SelectListItem { Text = "研究生", Value = "1" });
            ViewBag.typeList = typeList;

            List<SelectListItem> divisionList = new List<SelectListItem>();
            IEnumerable<Division> divisions = await _divisionService.GetAllAsync();
            foreach (var item in divisions)
            {
                divisionList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.divisionList = divisionList;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> AddOrUpdate(Major model)
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
                HttpPostedFileBase postFileBase = Request.Files["MajorImage"];
                if (postFileBase.ContentLength != 0)
                {
                    uploadStream = postFileBase.InputStream;
                    int bufferLen = 1024;
                    byte[] buffer = new byte[bufferLen];
                    int contentLen = 0;

                    string fileName = Path.GetFileName(postFileBase.FileName);
                    string baseUrl = Server.MapPath("/");
                    string uploadPath = baseUrl + @"\Content\Images\";
                    fs = new FileStream(uploadPath + fileName, FileMode.Create, FileAccess.ReadWrite);

                    while ((contentLen = uploadStream.Read(buffer, 0, bufferLen)) != 0)
                    {
                        fs.Write(buffer, 0, bufferLen);
                        fs.Flush();
                    }

                    //保存页面数据，上传的文件只保存路径
                    string imgUrl = "/Content/Images/" + fileName;
                    model.ImgUrl = imgUrl;
                }
                if (model.Id > 0)
                {
                    await _majorService.UpdateAsync(model);
                }
                else
                {
                    await _majorService.InsertAsync(model);
                }
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
            finally
            {
                Console.Write("hello撒大大");
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
                    success = await _majorService.DeleteAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error($"MajorController.Delete异常", ex);
                return Error(ex.Message);
            }
        }

        [NonAction]
        private string Validate(Major model, bool isUpdate = false)
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
                return "专业名称不能为空。";
            }

            return string.Empty;
        }
    }
}