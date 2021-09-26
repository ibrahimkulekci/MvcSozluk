using Business.Concrete;
using Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSozluk.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        ContentManager cm = new ContentManager(new EfContentDal());
        CategoryManager catm = new CategoryManager(new EfCategoryDal());

        public PartialViewResult HeadingListLeft()
        {
            var values = hm.GetListOrderByDesc();

            return PartialView(values);
        }
        public PartialViewResult NavbarCategoryList()
        {
            var values = catm.GetList();
            return PartialView(values);
        }

        public ActionResult Index()
        {
            var contentlist = cm.GetListOrderByDesc();
            return View(contentlist);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}