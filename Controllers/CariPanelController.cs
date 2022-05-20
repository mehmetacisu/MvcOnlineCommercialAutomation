using MvcTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcTicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {

        Context db = new Context();

        [Authorize]
        public ActionResult Index()
        {
            if (Session["CariMail"] != null)
            {
                var mail = Session["CariMail"].ToString();
                var mesajlar = db.Mesajlars.Where(x => x.Alici == mail).ToList();
                ViewBag.cariMail = mail;
                var mailID = db.Caris.Where(x => x.Mail == mail).Select(y => y.Id).FirstOrDefault();
                ViewBag.MailId = mailID;
                var toplamSatis = db.SatisHarekets.Where(X => X.CariID == mailID).Count();
                ViewBag.satis = toplamSatis;
                var toplamTutar = db.SatisHarekets.Where(x => x.CariID == mailID).Sum(y => y.Tutar).ToString();
                ViewBag.tutar = toplamTutar;
                var toplamUrunSayisi = db.SatisHarekets.Where(x => x.CariID == mailID).Sum(y => y.Adet);
                ViewBag.urunSayi = toplamUrunSayisi;
                var adSoyad = db.Caris.Where(x => x.Mail == mail).Select(y => y.Ad + " " + y.Soyad).FirstOrDefault();
                ViewBag.isim = adSoyad;
                var cariMail = db.Caris.Where(x => x.Mail == mail).Select(y => y.Mail).FirstOrDefault();
                ViewBag.mail = cariMail;
                return View(mesajlar);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        public ActionResult Guncelle(Cari cari)
        {
            var mail = Session["CariMail"].ToString();
            var id = db.Caris.Where(x => x.Mail == mail).Select(y => y.Id).FirstOrDefault();
            var cr = db.Caris.Find(id);
            cr.Ad = cari.Ad;
            cr.Soyad = cari.Soyad;
            cr.Mail = cari.Mail;
            cr.Sehir = cari.Sehir;
            cr.Sifre = cari.Sifre;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Siparislerim()
        {
            if (Session["CariMail"] != null)
            {
                var mail = Session["CariMail"].ToString();
                var id = db.Caris.Where(x => x.Mail == mail).Select(y => y.Id).FirstOrDefault();
                var siparisler = db.SatisHarekets.Where(x => x.CariID == id).ToList();
                return View(siparisler);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Mesajlar()
        {
            var mail = Session["CariMail"].ToString();
            var mesajlar = db.Mesajlars.Where(x => x.Alici == mail).OrderByDescending(x => x.Tarih).ToList();
            var gelenMesajSayisi = db.Mesajlars.Count(X => X.Alici == mail).ToString();
            ViewBag.mesajSayisi = gelenMesajSayisi;
            var gidenMesajSayisi = db.Mesajlars.Count(X => X.Gonderici == mail).ToString();
            ViewBag.gidenMesajSayisi = gidenMesajSayisi;
            return View(mesajlar);
        }

        public ActionResult GidenMesajlar()
        {
            var mail = Session["CariMail"].ToString();
            var mesajlar = db.Mesajlars.Where(x => x.Gonderici == mail).ToList();
            var gelenMesajSayisi = db.Mesajlars.Count(X => X.Alici == mail).ToString();
            ViewBag.mesajSayisi = gelenMesajSayisi;
            var gidenMesajSayisi = db.Mesajlars.Count(X => X.Gonderici == mail).ToString();
            ViewBag.gidenMesajSayisi = gidenMesajSayisi;
            return View(mesajlar);
        }

        public ActionResult Detay(int id)
        {
            var degerler = db.Mesajlars.Where(X => X.Id == id).OrderByDescending(x => x.Id).ToList();
            var mail = Session["CariMail"].ToString();
            var gelenMesajSayisi = db.Mesajlars.Count(X => X.Alici == mail).ToString();
            ViewBag.mesajSayisi = gelenMesajSayisi;
            var gidenMesajSayisi = db.Mesajlars.Count(X => X.Gonderici == mail).ToString();
            ViewBag.gidenMesajSayisi = gidenMesajSayisi;
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var mail = Session["CariMail"].ToString();
            var gelenMesajSayisi = db.Mesajlars.Count(X => X.Alici == mail).ToString();
            ViewBag.mesajSayisi = gelenMesajSayisi;
            var gidenMesajSayisi = db.Mesajlars.Count(X => X.Gonderici == mail).ToString();
            ViewBag.gidenMesajSayisi = gidenMesajSayisi;
            return View();
        }

        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar mesaj)
        {
            mesaj.Tarih = DateTime.Parse(DateTime.Now.ToString());
            var mail = Session["CariMail"].ToString();
            mesaj.Gonderici = mail;
            db.Mesajlars.Add(mesaj);
            db.SaveChanges();
            return RedirectToAction("Mesajlar", "CariPanel");
        }

        public ActionResult KargoTakip(string ara)
        {
            var kargolar = from kargo in db.KargoDetays select kargo;
            kargolar = kargolar.Where(y => y.TakipKodu.Contains(ara));
            return View(kargolar.ToList());
        }

        public ActionResult KargoDetay(string id)
        {
            var degerler = db.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(degerler);
        }

        public PartialViewResult SettingsPartial()
        {
            var mail = Session["CariMail"].ToString();
            var id = db.Caris.Where(X => X.Mail == mail).Select(y => y.Id).FirstOrDefault();
            var cari = db.Caris.Find(id);
            return PartialView("SettingsPartial",cari);
        }

        public PartialViewResult DuyurularPartial()
        {
            var duyurular = db.Mesajlars.Where(x => x.Alici == "admin").ToList();
            return PartialView(duyurular);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}