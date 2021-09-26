using Business.Concrete;
using Business.ValidationRules;
using Data.Concrete;
using Data.EntityFramework;
using Entity.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcSozluk.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        WriterLoginManager wlm = new WriterLoginManager(new EfWriterDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        WriterValidator writervalitador = new WriterValidator();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Admin p)
        {
            Context c = new Context();

            var adminuserinfo = c.Admins.FirstOrDefault(x => x.AdminUserName == p.AdminUserName && x.AdminPassword == p.AdminPassword);
            if (adminuserinfo != null)
            {
                FormsAuthentication.SetAuthCookie(adminuserinfo.AdminUserName, false);
                Session["AdminUserName"] = adminuserinfo.AdminUserName;
                return RedirectToAction("Index", "AdminCategory");
            }
            else
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult WriterLogin()
        {
            if (Session["WriterID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult WriterLogin(Writer p)
        {
            p.WriterUsername = p.WriterMail;
            //Context c = new Context();

            //var writeruserinfo = c.Writers.FirstOrDefault(x => x.WriterMail == p.WriterMail && x.WriterPassword == p.WriterPassword);
            ValidationResult results = writervalitador.Validate(p);
            var writeruserinfo = wlm.GetWriter(p.WriterMail);

            if (results.IsValid)
            {                
                if (writeruserinfo != null && BCrypt.Net.BCrypt.Verify(p.WriterPassword, writeruserinfo.WriterPassword))
                {
                    FormsAuthentication.SetAuthCookie(writeruserinfo.WriterMail, false);
                    Session["WriterID"] = writeruserinfo.WriterID;
                    Session["WriterUsername"] = writeruserinfo.WriterUsername;
                    Session["WriterMail"] = writeruserinfo.WriterMail;
                    Session["WriterImage"] = writeruserinfo.WriterImage;
                    Session["WriterStatus"] = writeruserinfo.WriterStatus;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "E-mail veya Şifre alanı hatalı. Lütfen tekrar deneyiniz..";
                    return View();
                }
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

        [HttpGet]
        public ActionResult WriterRegister()
        {
            if (Session["WriterID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult WriterRegister(Writer model)
        {
            ValidationResult results = writervalitador.Validate(model);
            if (results.IsValid)
            {
                model.WriterStatus = true;
                model.WriterPassword = BCrypt.Net.BCrypt.HashPassword(model.WriterPassword);
                model.WriterImage = "/AdminLTE-3.0.4/images/1.jpg";
                wm.WriterAdd(model);

                return RedirectToAction("WriterLogin");
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

        public ActionResult WriterLogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

    }
}