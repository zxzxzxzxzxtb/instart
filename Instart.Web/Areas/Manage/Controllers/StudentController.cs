using Instart.Common;
using Instart.Models;
using Instart.Service;
using Instart.Service.Base;
using Instart.Web.Attributes;
using Instart.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web.Areas.Manage.Controllers
{
    [AdminValidation]
    public class StudentController : ManageControllerBase
    {
        IStudentService _studentService = AutofacService.Resolve<IStudentService>();
        ISchoolService _schoolService = AutofacService.Resolve<ISchoolService>();
        ITeacherService _teacherService = AutofacService.Resolve<ITeacherService>();
        IMajorService _majorService = AutofacService.Resolve<IMajorService>();
        IDivisionService _divisionService = AutofacService.Resolve<IDivisionService>();

        public StudentController()
        {
            base.AddDisposableObject(_studentService);
        }

        public async Task<ActionResult> Index(int page = 1, int division = -1, string keyword = null)
        {
            int pageSize = 10;
            var list = await _studentService.GetListAsync(page, pageSize, division, keyword);
            ViewBag.Total = list.Total;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.Total * 1.0 / pageSize);
            ViewBag.Keyword = keyword;

            ViewBag.divisionList = await _divisionService.GetAllAsync();
            ViewBag.division = division;
            return View(list.Data);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id = 0)
        {
            Student model = new Student();
            string action = "添加学员";

            if (id > 0)
            {
                model = await _studentService.GetByIdAsync(id);
                action = "修改学员";
            }

            ViewBag.Action = action;

            List<SelectListItem> schoolList = new List<SelectListItem>();
            IEnumerable<School> schools = await _schoolService.GetAllAsync();
            foreach (var item in schools)
            {
                schoolList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.SchoolList = schoolList;

            List<SelectListItem> teacherList = new List<SelectListItem>();
            IEnumerable<Teacher> teachers = await _teacherService.GetAllAsync();
            foreach (var item in teachers)
            {
                teacherList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.TeacherList = teacherList;

            List<SelectListItem> majorList = new List<SelectListItem>();
            IEnumerable<Major> majors = await _majorService.GetAllAsync();
            foreach (var item in majors)
            {
                majorList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.MajorList = majorList;

            List<SelectListItem> divisionList = new List<SelectListItem>();
            IEnumerable<Division> divisions = await _divisionService.GetAllAsync();
            foreach (var item in divisions)
            {
                divisionList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.DivisionList = divisionList;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> Set(Student model)
        {
            if (model == null)
            {
                return Error("参数错误。");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                return Error("学员名称不能为空。");
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

            var bannerImgFile = Request.Files["fileBannerImg"];
            if (bannerImgFile != null)
            {
                string uploadResult = UploadHelper.Process(bannerImgFile.FileName, bannerImgFile.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.BannerImg = uploadResult;
                }
            }

            var bannerVideoFile = Request.Files["fileBannerVideo"];
            if (bannerVideoFile != null)
            {
                string uploadResult = UploadHelper.Process(bannerVideoFile.FileName, bannerVideoFile.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.BannerVideo = uploadResult;
                }
            }

            var videoImgFile = Request.Files["fileImg"];
            if (videoImgFile != null)
            {
                string uploadResult = UploadHelper.Process(videoImgFile.FileName, videoImgFile.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.ImgUrl = uploadResult;
                }
            }

            var videoFile = Request.Files["fileVideo"];
            if (videoFile != null)
            {
                string uploadResult = UploadHelper.Process(videoFile.FileName, videoFile.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.VideoUrl = uploadResult;
                }
            }

            var result = new ResultBase();

            if (model.Id > 0)
            {
                result.success = await _studentService.UpdateAsync(model);
            }
            else
            {
                result.success = await _studentService.InsertAsync(model);
            }

            return Json(result);
        }


        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            if (id <= 0)
            {
                return Error("id错误");
            }

            try
            {
                return Json(new ResultBase
                {
                    success = await _studentService.DeleteAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error($"SchoolController.Delete异常", ex);
                return Error(ex.Message);
            }
        }

        [HttpPost]
        public async Task<JsonResult> SetRecommend(int id, bool isRecommend)
        {
            if (id <= 0)
            {
                return Error("id错误");
            }

            try
            {
                return Json(new ResultBase
                {
                    success = await _studentService.SetRecommend(id, isRecommend)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error($"StudentController.SetRecommend异常", ex);
                return Error(ex.Message);
            }
        }
    }
}