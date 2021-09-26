using Data.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSozluk.Controllers
{
    public class IstatistikController : Controller
    {
        // GET: Istatistik
        Context _context = new Context();

        public ActionResult Index()
        {
            var toplamkategori = _context.Categories.Count(); //Toplam kategori sayısı.
            ViewBag.toplamkategori = toplamkategori;

            var basliksayisibyyazilim = _context.Headings.Count(x => x.CategoryID == 13); //Yazılım(id 13) kategorisinde bulunan başlık sayısı.
            ViewBag.basliksayisibyyazilim = basliksayisibyyazilim;

            var yazaradibya = _context.Writers.Count(x => x.WriterUsername.Contains("a")); //Yazar adında a harfi olanların sayısı.
            ViewBag.yazaradibya = yazaradibya;

            var enfazlabaslikbykategori = _context.Headings.Max(x => x.Category.CategoryName); //En fazla başlık bulunan kategori adı.
            ViewBag.enfazlabaslikbykategori = enfazlabaslikbykategori;

            var truevefalsefarki = _context.Categories.Count(x => x.CategoryStatus == true) - _context.Categories.Count(x => x.CategoryStatus == false);
            ViewBag.truevefalsefarki = truevefalsefarki;

            var toplamyazar = _context.Writers.Count(); //Toplam kategori sayısı.
            ViewBag.toplamyazar = toplamyazar;

            return View();
        }
    }
}