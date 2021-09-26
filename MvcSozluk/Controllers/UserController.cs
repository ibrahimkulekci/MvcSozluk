using Business.Concrete;
using Business.ValidationRules;
using Data.EntityFramework;
using Entity.Concrete;
using FluentValidation.Results;
using MvcSozluk.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSozluk.Controllers
{
    public class UserController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterDal());
        ContentManager cm = new ContentManager(new EfContentDal());
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        // GET: User
        [AllowAnonymous]
        public ActionResult Index(int id)
        {
            DisplayViewModel model = new DisplayViewModel();

            model.Writer = wm.GetByID(id);
            ViewBag.BaslikSayisi = hm.GetListByWriter(id).Count();
            model.Content = cm.GetListByWriter(id);

            return View(model);
        }
        public ActionResult Settings()
        {
            int writerid = Int32.Parse(Session["WriterID"].ToString());
            if(writerid == null)
            {
                RedirectToAction("WriterLogin", "Login");
            }
            var writervalue = wm.GetByID(writerid);
            SettingsViewModel model = new SettingsViewModel();
            model.Writer = writervalue;
            return View(model);
        }
        [HttpPost]
        public ActionResult Settings(SettingsViewModel model)
        {
            WriterValidator writervalidator = new WriterValidator();
            ValidationResult results = writervalidator.Validate(model.Writer);

            if (results.IsValid)
            {
                if(model.NewPassword != null)
                {
                    model.Writer.WriterPassword = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
                }

                wm.WriterUpdate(model.Writer);
                Session["WriterUsername"] = model.Writer.WriterUsername;
                Session["WriterMail"] = model.Writer.WriterMail;
                Session["WriterImage"] = model.Writer.WriterImage;
                ViewBag.Message = "Güncellendi";
                return RedirectToAction("Settings");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
    }
}