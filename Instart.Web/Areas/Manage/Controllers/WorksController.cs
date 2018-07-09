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
    public class WorksController : ManageControllerBase
    {
        IWorksService _worksService = AutofacService.Resolve<IWorksService>();
        IMajorService _majorService = AutofacService.Resolve<IMajorService>();

        public WorksController()
        {
            base.AddDisposableObject(_worksService);
            base.AddDisposableObject(_majorService);
        }

        public async Task<ActionResult> Index(int page = 1, string name = null)
        {
            int pageSize = 10;
            var list = await _worksService.GetListAsync(page, pageSize, name);
            ViewBag.Total = list.Total;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.Total * 1.0 / pageSize);
            return View(list.Data);
        }

        public async Task<ActionResult> Edit()
        {
            int id = Request.QueryString["id"].ToInt32();
            Works model;
            if (id > 0)
            {
                model = await _worksService.GetByIdAsync(id);
            }
            else
            {
                model = new Works();
            }
            List<SelectListItem> majorList = new List<SelectListItem>();
            IEnumerable<Major> majors = await _majorService.GetAllAsync();
            foreach (var item in majors)
            {
                majorList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.majorList = majorList;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> AddOrUpdate(Works model)
        {
            Stream uploadStream = null;
            FileStream fs = null;
            try
            {
                //文件上传，一次上传1M的数据，防止出现大文件无法上传
                HttpPostedFileBase postFileBase = Request.Files["WorksImage"];
                if (postFileBase == null || postFileBase.ContentLength == 0)
                {
                    return Error("作品不能为空。");
                }

                uploadStream = postFileBase.InputStream;
                int bufferLen = 1024;
                byte[] buffer = new byte[bufferLen];
                int contentLen = 0;

                string fileName = Path.GetFileName(postFileBase.FileName);
                string baseUrl = Server.MapPath("/");
                string uploadPath = baseUrl + @"\Content\Images\Works\";
                fs = new FileStream(uploadPath + fileName, FileMode.Create, FileAccess.ReadWrite);

                while ((contentLen = uploadStream.Read(buffer, 0, bufferLen)) != 0)
                {
                    fs.Write(buffer, 0, bufferLen);
                    fs.Flush();
                }

                //保存页面数据，上传的文件只保存路径
                string imgUrl = "/Content/Images/Works/" + fileName;
                model.ImgName = fileName;
                model.ImgUrl = imgUrl;
                if (model.Id > 0)
                {
                    await _worksService.UpdateAsync(model);
                }
                else
                {
                    await _worksService.InsertAsync(model);
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
                    success = await _worksService.DeleteAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error($"WorksController.Delete异常", ex);
                return Error(ex.Message);
            }
        }
    }
}