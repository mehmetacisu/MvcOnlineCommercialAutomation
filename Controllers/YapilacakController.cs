using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTicariOtomasyon.Models.Siniflar;

namespace MvcTicariOtomasyon.Controllers
{
    public class YapilacakController : Controller
    {
        Context db = new Context();
        public ActionResult Index()
        {
            var cariSayisi = db.Caris.Count().ToString();
            ViewBag.cariSayi = cariSayisi;

            var urunSayisi = db.Uruns.Count().ToString();
            ViewBag.urunSayi = urunSayisi;

            var kategoriSayisi = db.Kategoris.Count().ToString();
            ViewBag.kategoriSayi = kategoriSayisi;

            var cariSehirSayisi = (from cari in db.Caris select cari.Sehir).Distinct().Count().ToString();
            ViewBag.cariSehirSayi = cariSehirSayisi;

            var yapilacaklar = db.Yapilacaks.ToList();
            return View(yapilacaklar);
        }
    }
}