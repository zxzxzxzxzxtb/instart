using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Instart.Models;
using Instart.Service;
using Instart.Service.Base;

namespace Instart.Web2.Controllers
{
    /// <summary>
    /// 关于Instart
    /// </summary>
    public class AboutController : ControllerBase
    {
        IAboutInstartService _aboutService = AutofacService.Resolve<IAboutInstartService>();
        IBannerService _bannerService = AutofacService.Resolve<IBannerService>();
        ICourseService _courseService = AutofacService.Resolve<ICourseService>();

        public AboutController()
        {
            this.AddDisposableObject(_aboutService);
            this.AddDisposableObject(_bannerService);
            this.AddDisposableObject(_courseService);
        }

        public  ActionResult Index()
        {
            AboutInstart model = ( _aboutService.GetInfoAsync()) ?? new AboutInstart();
            ViewBag.CourseList = ( _courseService.GetRecommendListAsync(3)) ?? new List<Instart.Models.Course>();
            List<Banner> bannerList =  _bannerService.GetBannerListByPosAsync(Instart.Models.Enums.EnumBannerPos.Student);
            ViewBag.BannerUrl = "";
            if (bannerList != null && bannerList.Count() > 0)
            {
                ViewBag.BannerUrl = bannerList[0].ImageUrl;
            }
            return View(model);
        }
    }
}