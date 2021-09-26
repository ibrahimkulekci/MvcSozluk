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
    public class WriterPanelMessageController : Controller
    {
        MessageManager mm = new MessageManager(new EfMessageDal());
        MessageValidator messagevalidator = new MessageValidator();
        Context _context = new Context();

        public PartialViewResult MessageListMenu()
        {
            string p = Session["WriterMail"].ToString();
            var inboxmessagecount = _context.Messages.Count(x => x.ReceiverMail.Contains(p));
            ViewBag.inboxmessagecount = inboxmessagecount;

            var sendboxmessagecount = _context.Messages.Count(x => x.SenderMail.Contains(p));
            ViewBag.sendboxmessagecount = sendboxmessagecount;

            return PartialView();
        }

        public ActionResult Inbox()
        {
            string p= Session["WriterMail"].ToString();
            var messagevalues = mm.GetListInbox(p);
            return View(messagevalues);
        }
        public ActionResult Sendbox()
        {
            string p = Session["WriterMail"].ToString();
            var messagevalues = mm.GetListSendbox(p);
            return View(messagevalues);
        }
        public ActionResult GetInboxMessageDetails(int id)
        {
            var values = mm.GetByID(id);
            return View(values);
        }
        public ActionResult GetSendboxMessageDetails(int id)
        {
            var values = mm.GetByID(id);
            return View(values);
        }
        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewMessage(Message p)
        {
            ValidationResult result = messagevalidator.Validate(p);
            if (result.IsValid)
            {
                p.SenderMail = Session["WriterMail"].ToString();
                p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                mm.MessageAdd(p);
                return RedirectToAction("Sendbox");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

    }
}