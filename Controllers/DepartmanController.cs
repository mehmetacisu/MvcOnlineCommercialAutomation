using MvcTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTicariOtomasyon.Controllers
{
    [Authorize]
    public class DepartmanController : Controller
    {
        Context db = new Context();
        public ActionResult Index()
        {
            var departmanlar = db.Departmans.Where(x=>x.Durum==true).ToList();
            return View(departmanlar);
        }

        [Authorize(Roles ="A")]
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Departman departman)
        {
            db.Departmans.Add(departman);
            departman.Durum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var dep = db.Departmans.Find(id);
            dep.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int id)
        {
            var dpt = db.Departmans.Find(id);
            return View("Guncelle",dpt);
        }
        
        public ActionResult DepartmanGuncelle(Departman departman)
        {
            var dep = db.Departmans.Find(departman.Id);
            dep.Ad = departman.Ad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Detay(int id)
        {
            var personel = db.Personels.Where(x => x.DepartmanID == id).ToList();
            var dep = db.Departmans.Where(x=>x.Id == id).Select(y=>y.Ad).FirstOrDefault();
            ViewBag.Departman = dep;
            return View("Detay",personel);
        }

        public ActionResult SatisDetay(int id)
        {
            var satisdetay = db.SatisHarekets.Where(x => x.PersonelID == id).ToList();
            ViewBag.Personel = db.Personels.Where(x => x.Id == id).Select(y => y.Ad + " " + y.Soyad).FirstOrDefault();
            return View(satisdetay);
        }
    }
}