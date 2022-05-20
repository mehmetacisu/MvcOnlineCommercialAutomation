using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTicariOtomasyon.Models.Siniflar;
using PagedList;
using PagedList.Mvc;
namespace MvcTicariOtomasyon.Controllers
{
    public class KategoriController : Controller
    {
        Context db = new Context();
        public ActionResult Index(int sayfa = 1)
        {
            var kategoriler = db.Kategoris.ToList().ToPagedList(sayfa, 5);
            return View(kategoriler);
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Kategori ktg)
        {
            db.Kategoris.Add(ktg);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var ktg = db.Kategoris.Find(id);
            db.Kategoris.Remove(ktg);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int id)
        {
            var ktg = db.Kategoris.Find(id);
            return View("Guncelle", ktg);
        }

        public ActionResult KategoriGuncelle(Kategori ktg)
        {
            var kategori = db.Kategoris.Find(ktg.Id);
            kategori.Ad = ktg.Ad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CascadingDeneme()
        {
            Cascading cascading = new Cascading();
            cascading.Kategoriler = new SelectList(db.Kategoris, "Id", "Ad");
            cascading.Urunler = new SelectList(db.Uruns, "Id", "Ad");
            return View(cascading);
        }

        public JsonResult UrunGetir(int ktgID)
        {
            var urunler = (from urun in db.Uruns
                           join kategori in db.Kategoris
                           on urun.KategoriID equals kategori.Id
                           where urun.Kategori.Id == ktgID
                           select new
                           {
                               Text = urun.Ad,
                               Value = urun.Id.ToString()
                           }).ToList();
            return Json(urunler,JsonRequestBehavior.AllowGet);
        }
    }
}