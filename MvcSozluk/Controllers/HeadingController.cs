using Business.Concrete;
using Business.ValidationRules;
using Data.Concrete;
using Data.EntityFramework;
using Entity.Concrete;
using FluentValidation.Results;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSozluk.Controllers
{
    public class HeadingController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        ContentManager com = new ContentManager(new EfContentDal());

        HeadingContentAddValidator headingcontentvalidator = new HeadingContentAddValidator();

        Context con = new Context();

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Content(int id,int page=1)
        {
            ViewBag.headingID = id;
            var contentvalue = com.GetListByHeadingID(id).ToPagedList(page, 10);
            ViewBag.baslik = hm.GetByID(id);
            return View(contentvalue);
        }
        [HttpPost]
        public ActionResult Content(Content model)
        {
            if(model.ContentValue != null)
            {
                model.WriterID = Int32.Parse(Session["WriterID"].ToString());
                model.ContentDate = DateTime.Parse(DateTime.Now.ToLongTimeString());
                com.ContentAdd(model);
                return RedirectToAction("/Content/" + model.HeadingID);
            }
            else
            {
                ViewBag.ErrorMessage = "Yorum alanı boş bırakılamaz.";
            }
            //return View("Content/"+model.HeadingID+"/"+1);
            return RedirectToAction("Content/" + model.HeadingID);

        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult NewHeading()
        {
            if (Session["WriterID"] == null)
            {
                return RedirectToAction("WriterLogin", "Login");
            }

            List<SelectListItem> valuecategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.categoryID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult NewHeading(HeadingContentAdd p)
        {
            ValidationResult results = headingcontentvalidator.Validate(p);

            if (results.IsValid)
            {
                var headingadd = new Heading
                {
                    HeadingName = p.HeadingName,
                    HeadingDate = DateTime.Parse(DateTime.Now.ToLongTimeString()),
                    CategoryID = p.CategoryID,
                    WriterID = Int32.Parse(Session["WriterID"].ToString()),
                    HeadingStatus = true
                };

                hm.HeadingAdd(headingadd);

                var contentadd = new Content
                {
                    ContentValue = p.ContentValue,
                    ContentDate = DateTime.Parse(DateTime.Now.ToLongTimeString()),
                    HeadingID = headingadd.HeadingID,
                    WriterID = Int32.Parse(Session["WriterID"].ToString()),
                    ContentStatus = true
                };
                com.ContentAdd(contentadd);

                return RedirectToAction("/Content/" + headingadd.HeadingID);

            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            List<SelectListItem> valuecategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.categoryID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;

            return View();              
        }

        public ActionResult List()
        {
            var headingvalues = hm.GetList();
            return View(headingvalues);
        }

        [HttpGet]
        public ActionResult AddHeading()
        {
            List<SelectListItem> valuecategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text=x.CategoryName,
                                                      Value=x.categoryID.ToString()
                                                  }).ToList();
            List<SelectListItem> valuewriter = (from x in wm.GetList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.WriterUsername,
                                                     Value = x.WriterID.ToString()
                                                 }).ToList();
            ViewBag.vlc = valuecategory;
            ViewBag.vlw = valuewriter;
            return View();
        }
        [HttpPost]
        public ActionResult AddHeading(Heading p)
        {           
            p.HeadingDate = DateTime.Parse(DateTime.Now.ToLongTimeString());
            hm.HeadingAdd(p);
            return RedirectToAction("Index");
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
            return RedirectToAction("Index");
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
            return RedirectToAction("List");
        }
        [AllowAnonymous]
        public ActionResult Category(int id)
        {
            var headingcategory = hm.GetListByCategory(id);
            return View(headingcategory);
        }
        [AllowAnonymous]
        public ActionResult Search(string p)
        {
            ViewBag.Arama = p;
            var searchvalues = hm.GetListBySearch(p);
            return View(searchvalues);
        }
    }
}