using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTicariOtomasyon.Models.Siniflar;
namespace MvcTicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        Context db = new Context();
        public ActionResult Index(string ara)
        {
            var urunler = from urun in db.Uruns select urun;
            if (!string.IsNullOrEmpty(ara))
            {
                urunler = urunler.Where(y => y.Ad.Contains(ara));
            }
            return View(urunler.ToList());
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            List<SelectListItem> kategoriler = (from x in db.Kategoris.ToList()
                                                select new SelectListItem
                                                {
                                                    Value = x.Id.ToString(),
                                                    Text = x.Ad
                                                }).ToList();
            ViewBag.ktg = kategoriler;
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Urun urun)
        {
            db.Uruns.Add(urun);
            urun.Durum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var urun = db.Uruns.Find(id);
            urun.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int id)
        {
            List<SelectListItem> kategoriler = (from x in db.Kategoris.ToList()
                                                select new SelectListItem
                                                {
                                                    Value = x.Id.ToString(),
                                                    Text = x.Ad
                                                }).ToList();
            ViewBag.ktg = kategoriler;

            var urun = db.Uruns.Find(id);
            return View("Guncelle",urun);
        }

        public ActionResult UrunGuncelle(Urun urun)
        {
            var urn = db.Uruns.Find(urun.Id);
            urn.Ad = urun.Ad;
            urn.Marka = urun.Marka;
            urn.Stok = urun.Stok;
            urn.KategoriID = urun.KategoriID;
            urn.AlisFiyat = urun.AlisFiyat;
            urn.SatisFiyat = urun.SatisFiyat;
            urn.Fotograf = urun.Fotograf;
            urn.Durum = urun.Durum;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Liste()
        {
            var urunler = db.Uruns.ToList();
            return View(urunler);
        }

        [HttpGet]
        public ActionResult Satis(int id)
        {
            List<SelectListItem> personeller = (from personel in db.Personels.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = personel.Ad + " " + personel.Soyad,
                                                    Value = personel.Id.ToString()
                                                }).ToList();
            ViewBag.personel = personeller;
            var urunId = db.Uruns.Find(id);
            ViewBag.Id = urunId.Id;
            var fiyat = urunId.SatisFiyat.ToString();
            ViewBag.urunFiyat = fiyat;
            return View();
        }

        [HttpPost]
        public ActionResult Satis(SatisHareket satis)
        {
            satis.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.SatisHarekets.Add(satis);
            db.SaveChanges();
            return RedirectToAction("Index","Satis");
        }
    }
}