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
    public class CourseController : ManageControllerBase
    {
        ICourseService _courseService = AutofacService.Resolve<ICourseService>();
        ITeacherService _teacherService = AutofacService.Resolve<ITeacherService>();

        public CourseController()
        {
            base.AddDisposableObject(_courseService);
            base.AddDisposableObject(_teacherService);
        }

        public async Task<ActionResult> Index(int page = 1, string keyword = null)
        {
            int pageSize = 10;
            var list = await _courseService.GetListAsync(page, pageSize, keyword);
            ViewBag.Total = list.Total;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.Total * 1.0 / pageSize);
            ViewBag.Keyword = keyword;
            return View(list.Data);
        }

        public async Task<ActionResult> Edit(int id = 0)
        {
            Course model = new Course();
            string action = "添加课程";

            if (id > 0)
            {
                model = await _courseService.GetByIdAsync(id);
                action = "修改课程";
            }

            ViewBag.Action = action;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> Set(Course model)
        {
            if (model == null)
            {
                return Error("参数错误。");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                return Error("课程名称不能为空。");
            }
            var fileCourse = Request.Files["fileCourse"];

            if (fileCourse != null)
            {
                string uploadResult = UploadHelper.Process(fileCourse.FileName, fileCourse.InputStream);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    model.Picture = uploadResult;
                }
            }
            var result = new ResultBase();

            if (model.Id > 0)
            {
                result.success = await _courseService.UpdateAsync(model);
            }
            else
            {
                result.success = await _courseService.InsertAsync(model);
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
                    success = await _courseService.DeleteAsync(id)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error($"CourseController.Delete异常", ex);
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
                    success = await _courseService.SetRecommendAsync(id, isRecommend)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error($"CourseController.SetRecommend异常", ex);
                return Error(ex.Message);
            }
        }

        public async Task<ActionResult> Info()
        {
            CourseInfo model = await _courseService.GetInfoAsync();
            if (model == null) model = new CourseInfo();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> SetInfo(CourseInfo model)
        {
            if (model == null)
            {
                return Error("参数错误。");
            }

            var result = new ResultBase();

            int count = await _courseService.GetInfoCountAsync();
            if (count > 0)
            {
                result.success = await _courseService.UpdateInfoAsync(model);
            }
            else
            {
                result.success = await _courseService.InsertInfoAsync(model);
            }

            return Json(result);
        }

        public async Task<ActionResult> TeacherSelect(int id = 0)
        {
            IEnumerable<Teacher> teacherList = await _teacherService.GetAllAsync();
            IEnumerable<int> selectedList = await _courseService.GetTeachersByIdAsync(id);
            if (teacherList != null)
            {
                foreach (var teacher in teacherList)
                {
                    if (selectedList != null && selectedList.Contains(teacher.Id))
                    {
                        teacher.IsSelected = true;
                    }
                    else
                    {
                        teacher.IsSelected = false;
                    }
                }
            }

            var course = await _courseService.GetByIdAsync(id);
            if(course == null)
            {
                throw new Exception("课程不存在");
            }

            ViewBag.TeacherList = teacherList;
            ViewBag.CourseId = id;
            ViewBag.CourseName = course.Name;
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SetTeachers(int courseId, string teacherIds)
        {
            try
            {
                return Json(new ResultBase
                {
                    success = await _courseService.SetTeachers(courseId, teacherIds)
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error($"CourseController.SetTeachers异常", ex);
                return Error(ex.Message);
            }
        }
    }
}