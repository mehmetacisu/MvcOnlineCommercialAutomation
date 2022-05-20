using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTicariOtomasyon.Models.Siniflar;

namespace MvcTicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        Context db = new Context();
        public ActionResult Index()
        {
            var faturalar = db.Faturas.ToList();
            return View(faturalar);
        }

        public ActionResult DinamikIndex()
        {
            DinamikFatura df = new DinamikFatura();
            df.degerFatura = db.Faturas.ToList();
            df.degerKalem = db.FaturaKalems.ToList();
            return View(df);
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Fatura fatura)
        {
            db.Faturas.Add(fatura);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int id)
        {
            var fatura = db.Faturas.Find(id);
            return View("Guncelle", fatura);
        }

        public ActionResult FaturaGuncelle(Fatura fatura)
        {
            var fat = db.Faturas.Find(fatura.Id);
            fat.SeriNo = fatura.SeriNo;
            fat.SiraNo = fatura.SiraNo;
            fat.VergiDairesi = fatura.VergiDairesi;
            fat.Tarih = DateTime.Parse(fatura.Tarih.ToShortDateString());
            fat.Saat = fatura.Saat;
            fat.TeslimEden = fatura.TeslimEden;
            fat.TeslimAlan = fatura.TeslimAlan;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Detay(int id)
        {
            var fatura = db.FaturaKalems.Where(x => x.FaturaID == id).ToList();
            return View(fatura);
        }

        [HttpGet]
        public ActionResult YeniKalem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniKalem(FaturaKalem faturaKalem)
        {
            db.FaturaKalems.Add(faturaKalem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FaturaKaydet(string SeriNo,string SiraNo,DateTime Tarih,string VergiDairesi,string Saat,string TeslimEden,string TeslimAlan,string Toplam,
            FaturaKalem[] kalemler)
        {
            Fatura fatura = new Fatura();
            fatura.SeriNo = SeriNo;
            fatura.SiraNo = SiraNo;
            fatura.Tarih = Tarih;
            fatura.VergiDairesi = VergiDairesi;
            fatura.Saat = Saat;
            fatura.TeslimEden = TeslimEden;
            fatura.TeslimAlan = TeslimAlan;
            fatura.Toplam = decimal.Parse(Toplam);
            db.Faturas.Add(fatura);
            foreach (var fk in kalemler)
            {
                FaturaKalem fatkal = new FaturaKalem();
                fatkal.Aciklama = fk.Aciklama;
                fatkal.Miktar = fk.Miktar;
                fatkal.BirimFiyat = fk.BirimFiyat;
                fatkal.FaturaID = fk.Id;
                fatkal.Tutar = fk.Tutar;
                db.FaturaKalems.Add(fatkal);
            }
            db.SaveChanges();
            return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);
            
        }
    }
}