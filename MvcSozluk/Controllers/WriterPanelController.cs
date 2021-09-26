using Business.Concrete;
using Data.EntityFramework;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Business.ValidationRules;
using FluentValidation.Results;

namespace MvcSozluk.Controllers
{
    public class WriterPanelController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        public ActionResult Profile()
        {
            int writerid = Int32.Parse(Session["WriterID"].ToString());
            var writervalue = wm.GetByID(writerid);
            return View(writervalue);
        }
        [HttpPost]
        public ActionResult Profile(Writer model)
        {
            WriterValidator writervalidator = new WriterValidator();
            ValidationResult results = writervalidator.Validate(model);
            if (results.IsValid)
            {
                wm.WriterUpdate(model);
                Session["WriterUsername"] = model.WriterUsername;
                Session["WriterMail"] = model.WriterMail;
                Session["WriterImage"] = model.WriterImage;
                return RedirectToAction("Profile");
            }
            else
            {
                foreach(var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        public ActionResult Heading(int page=1)
        {
           int id = Int32.Parse(Session["WriterID"].ToString());
            var values = hm.GetListByWriter(id).ToPagedList(page,20);
            return View(values);
        }
        [HttpGet]
        public ActionResult NewHeading()
        {
            List<SelectListItem> valuecategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.categoryID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;
            return View();
        }
        [HttpPost]
        public ActionResult NewHeading(Heading p)
        {
            p.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.WriterID = Int32.Parse(Session["WriterID"].ToString());
            p.HeadingStatus = true;
            hm.HeadingAdd(p);
            return RedirectToAction("Heading");
        }
        [HttpGet]
        public ActionResult EditHeading(int id)
        {
            List<SelectListItem> valuecategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.categoryID.ToString()
                                                  }).ToList();

            ViewBag.vlc = valuecategory;
            var headingvalue = hm.GetByID(id);
            return View(headingvalue);
        }
        [HttpPost]
        public ActionResult EditHeading(Heading p)
        {
            hm.HeadingUpdate(p);
            return RedirectToAction("Heading");
        }
        public ActionResult DeleteHeading(int id)
        {
            var headingvalue = hm.GetByID(id);

            if (headingvalue.HeadingStatus == false)
            {
                headingvalue.HeadingStatus = true;
            }
            else if (headingvalue.HeadingStatus == true)
            {
                headingvalue.HeadingStatus = false;
            }

            hm.HeadingDelete(headingvalue);
            return RedirectToAction("Heading");
        }
    }
}