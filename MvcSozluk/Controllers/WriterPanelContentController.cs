using Business.Concrete;
using Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSozluk.Controllers
{
    public class WriterPanelContentController : Controller
    {
        ContentManager cm = new ContentManager(new EfContentDal());
        public ActionResult Index()
        {
            int id = Int32.Parse(Session["WriterID"].ToString());
            var contentvalues = cm.GetListByWriter(id);
            return View(contentvalues);
        }
    }
}