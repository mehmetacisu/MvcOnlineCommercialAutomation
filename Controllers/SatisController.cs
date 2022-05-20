using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTicariOtomasyon.Models.Siniflar;
namespace MvcTicariOtomasyon.Controllers
{
    public class SatisController : Controller
    {
        Context db = new Context();
        public ActionResult Index()
        {
            var satislar = db.SatisHarekets.ToList();
            return View(satislar);
        }

        [HttpGet]
        public ActionResult Yap()
        {
            List<SelectListItem> urunler = (from urun in db.Uruns.ToList()
                                            select new SelectListItem
                                            {
                                                Text = urun.Ad,
                                                Value = urun.Id.ToString()
                                            }).ToList();
            ViewBag.urun = urunler;

            List<SelectListItem> cariler = (from cari in db.Caris.ToList()
                                            select new SelectListItem
                                            {
                                                Text = cari.Ad +" "+cari.Soyad,
                                                Value = cari.Id.ToString()
                                            }).ToList();
            ViewBag.cari = cariler;

            List<SelectListItem> personeller = (from personel in db.Personels.ToList()
                                            select new SelectListItem
                                            {
                                                Text = personel.Ad + " " + personel.Soyad,
                                                Value = personel.Id.ToString()
                                            }).ToList();
            ViewBag.personel = personeller;

            return View();
        }

        [HttpPost]
        
        public ActionResult Yap(SatisHareket sh)
        {
            sh.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.SatisHarekets.Add(sh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int id)
        {
            List<SelectListItem> urunler = (from urun in db.Uruns.ToList()
                                            select new SelectListItem
                                            {
                                                Text = urun.Ad,
                                                Value = urun.Id.ToString()
                                            }).ToList();
            ViewBag.urun = urunler;

            List<SelectListItem> cariler = (from cari in db.Caris.ToList()
                                            select new SelectListItem
                                            {
                                                Text = cari.Ad + " " + cari.Soyad,
                                                Value = cari.Id.ToString()
                                            }).ToList();
            ViewBag.cari = cariler;

            List<SelectListItem> personeller = (from personel in db.Personels.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = personel.Ad + " " + personel.Soyad,
                                                    Value = personel.Id.ToString()
                                                }).ToList();
            ViewBag.personel = personeller;
            var satis = db.SatisHarekets.Find(id);

            return View("Guncelle", satis);
        }

        public ActionResult SatisGuncelle(SatisHareket sh)
        {
            var satis = db.SatisHarekets.Find(sh.Id);
            satis.CariID = sh.CariID;
            satis.PersonelID = sh.PersonelID;
            satis.UrunID = sh.UrunID;
            satis.Adet = sh.Adet;
            satis.Tarih = DateTime.Parse(DateTime.Now.ToString());
            satis.Tutar = sh.Tutar;
            satis.Fiyat = sh.Fiyat;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Detay(int id)
        {
            var detaylar = db.SatisHarekets.Where(x => x.Id == id).ToList();
            return View(detaylar);
        }
    }
}
