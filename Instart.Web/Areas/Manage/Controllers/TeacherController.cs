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
    public class TeacherController : ManageControllerBase
    {
        ITeacherService _teacherService = AutofacService.Resolve<ITeacherService>();
        ICourseService _courseService = AutofacService.Resolve<ICourseService>();
        IDivisionService _divisionService = AutofacService.Resolve<IDivisionService>();
        ISchoolService _schoolService = AutofacService.Resolve<ISchoolService>();
        IMajorService _majorService = AutofacService.Resolve<IMajorService>();

        public TeacherController()
        {
            base.AddDisposableObject(_teacherService);
            base.AddDisposableObject(_courseService);
            base.AddDisposableObject(_divisionService);
            base.AddDisposableObject(_schoolService);
            base.AddDisposableObject(_majorService);
        }

        public async Task<ActionResult> Index(int page = 1, int division = -1, string keyword = null)
        {
            int pageSize = 10;
            var list = await _teacherService.GetListAsync(page, pageSize, division, keyword);
            ViewBag.Total = list.Total;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.Total * 1.0 / pageSize);
            ViewBag.Keyword = keyword;

            ViewBag.divisionList = await _divisionService.GetAllAsync();
            ViewBag.division = division;
            return View(list.Data);
        }

        public async Task<ActionResult> Edit(int id = 0)
        {
            Teacher model = new Teacher();
            string action = "添加导师";

            if (id > 0)
            {
                model = await _teacherService.GetByIdAsync(id);
                action = "修改导师";
            }

            ViewBag.Action = action;

            List<SelectListItem> divisionList = new List<SelectListItem>();
            IEnumerable<Division> divisions = await _divisionService.GetAllAsync();
            foreach (var item in divisions)
            {
                divisionList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.divisionList = divisionList;

            List<SelectListItem> schoolList = new List<SelectListItem>();
            IEnumerable<School> schools = await _schoolService.GetAllAsync();
            foreach (var item in schools)
            {
                schoolList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.SchoolList = schoolList;

            List<SelectListItem> majorList = new List<SelectListItem>();
            IEnumerable<Major> majors = await _majorService.GetAllAsync();
            foreach (var item in majors)
            {
                majorList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.MajorList = majorList;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> Set(Teacher model)
        {
            if (model == null)
            {
                return Error("参数错误。");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                return Error("导师名称不能为空。");
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
                result.success = await _teacherService.UpdateAsync(model);
            }
            else
            {
                result.success = await _teacherService.InsertAsync(model);
            }

            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                return Json(new ResultBase
                {
                    success = await _teacherService.DeleteAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error($"TeacherController.Delete异常", ex);
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
                    success = await _teacherService.SetRecommendAsync(id, isRecommend)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error($"TeacherController.SetRecommend异常", ex);
                return Error(ex.Message);
            }
        }

        public async Task<ActionResult> CourseSelect(int id = 0)
        {
            IEnumerable<Course> courseList = await _courseService.GetAllAsync();
            IEnumerable<int> selectedList = await _teacherService.GetCoursesByIdAsync(id);
            if (courseList != null)
            {
                foreach (var course in courseList)
                {
                    if (selectedList != null && selectedList.Contains(course.Id))
                    {
                        course.IsSelected = true;
                    }
                    else
                    {
                        course.IsSelected = false;
                    }
                }
            }

            var teacher = await _teacherService.GetByIdAsync(id);
            if(teacher == null)
            {
                throw new Exception("导师不存在");
            }

            ViewBag.CourseList = courseList;
            ViewBag.TeacherId = id;
            ViewBag.TeacherName = teacher.Name;
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SetCourses(int teacherId, string courseIds)
        {
            try
            {
                return Json(new ResultBase
                {
                    success = await _teacherService.SetCourses(teacherId, courseIds)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error($"TeacherController.SetCourses异常", ex);
                return Error(ex.Message);
            }
        }
    }
}