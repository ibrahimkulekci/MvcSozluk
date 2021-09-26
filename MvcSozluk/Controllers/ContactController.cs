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

namespace MvcSozluk.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        ContactManager cm = new ContactManager(new EfContactDal());
        ContactValidator cv = new ContactValidator();
        Context _context = new Context();

        public PartialViewResult MessageListMenu()
        {
            var contactmessagecount = _context.Contacts.Count();
            ViewBag.contactmessagecount = contactmessagecount;

            var inboxmessagecount = _context.Messages.Count(x => x.ReceiverMail.Contains("admin@gmail.com"));
            ViewBag.inboxmessagecount = inboxmessagecount;

            var sendboxmessagecount = _context.Messages.Count(x => x.SenderMail.Contains("admin@gmail.com"));
            ViewBag.sendboxmessagecount = sendboxmessagecount;

            return PartialView();
        }

        public ActionResult Index()
        {
            var contactvalues = cm.GetList();
            return View(contactvalues);
        }
        public ActionResult GetContactDetails(int id)
        {
            var contactvalues = cm.GetByID(id);
            return View(contactvalues);
        }
        
        [AllowAnonymous]
        public ActionResult NewContact()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult NewContact(Contact model)
        {
            ValidationResult results = cv.Validate(model);

            if (results.IsValid)
            {
                model.ContactDate= DateTime.Parse(DateTime.Now.ToLongTimeString());
                cm.ContactAdd(model);
                ViewBag.Status = "1";
                ViewBag.Message = "İletişim mesajınız başarıyla gönderilmiştir. En kısa zamanda E-Mail üzerinden dönüş yapılacaktır. Teşekkürler.";
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                ViewBag.Status = "0";
                ViewBag.Message = "İletişim mesajınız hata sonucu gönderilmemiştir. Lütfen tekrar deneyiniz.";
            }
            return View();
        }
    }
}