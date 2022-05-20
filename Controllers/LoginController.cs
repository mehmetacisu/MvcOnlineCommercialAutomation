using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcTicariOtomasyon.Models.Siniflar;
namespace MvcTicariOtomasyon.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        Context db = new Context();


        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult CariKayit()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult CariKayit(Cari cari)
        {
            db.Caris.Add(cari);
            cari.Durum = true;
            db.SaveChanges();
            return PartialView();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Cari cari)
        {
            var cariBilgi = db.Caris.FirstOrDefault(x => x.Mail == cari.Mail && x.Sifre == cari.Sifre);
            if (cariBilgi != null)
            {
                FormsAuthentication.SetAuthCookie(cariBilgi.Mail, false);
                Session["CariMail"] = cariBilgi.Mail.ToString();
                return RedirectToAction("Index", "CariPanel");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(Admin admin)
        {
            var adminBilgi = db.Admins.FirstOrDefault(x => x.KullaniciAd == admin.KullaniciAd && x.Sifre == admin.Sifre);
            if (adminBilgi != null)
            {
                FormsAuthentication.SetAuthCookie(adminBilgi.KullaniciAd, false);
                Session["KullaniciAd"] = adminBilgi.KullaniciAd;
                return RedirectToAction("Index", "Kategori");
            }
            else
            {
                return RedirectToAction("Index","Login");
            }
        }
    }
}