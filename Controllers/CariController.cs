using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTicariOtomasyon.Models.Siniflar;

namespace MvcTicariOtomasyon.Controllers
{
    public class CariController : Controller
    {
        Context db = new Context();
        public ActionResult Index()
        {
            var cariler = db.Caris.Where(x => x.Durum == true).ToList();
            return View(cariler);
        }

        public ActionResult Detay(int id)
        {
            var satisdetay = db.SatisHarekets.Where(x => x.CariID == id).ToList();
            var cari = db.Caris.Where(x => x.Id == id).Select(y => y.Ad + " " + y.Soyad).FirstOrDefault();
            ViewBag.Cari = cari;
            return View(satisdetay);
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Ekle(Cari cari)
        {
            cari.Durum = true;
            db.Caris.Add(cari);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var cari = db.Caris.Find(id);
            cari.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int id)
        {
            var cari = db.Caris.Find(id);
            return View("Guncelle", cari);
        }

        public ActionResult CariGuncelle(Cari cari)
        {
            if (!ModelState.IsValid)
            {
                return View("Guncelle");
            }
            else
            {
                var cariler = db.Caris.Find(cari.Id);
                cariler.Ad = cari.Ad;
                cariler.Soyad = cari.Soyad;
                cariler.Sehir = cari.Sehir;
                cariler.Mail = cari.Mail;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}