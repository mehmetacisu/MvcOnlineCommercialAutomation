using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTicariOtomasyon.Models.Siniflar;

namespace MvcTicariOtomasyon.Controllers
{
    public class PersonelController : Controller
    {
        Context db = new Context();
        public ActionResult Index()
        {
            var personel = db.Personels.ToList();
            return View(personel);
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            var departmanlar = (from dep in db.Departmans.ToList()
                                select new SelectListItem
                                {
                                    Text = dep.Ad,
                                    Value = dep.Id.ToString()
                                }).ToList();
            ViewBag.departman = departmanlar;
            return View();
        }

        [HttpPost]

        public ActionResult Ekle(Personel personel)
        {
            if (Request.Files.Count>0)
            {
                string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "/Images/"+dosyaAdi;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                personel.Fotograf = "/Images/" + dosyaAdi;
            }
            db.Personels.Add(personel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int id)
        {
            var pers = db.Personels.Find(id);
            ViewBag.Foto = pers.Fotograf;
            var departmanlar = (from dep in db.Departmans.ToList()
                                select new SelectListItem
                                {
                                    Text = dep.Ad,
                                    Value = dep.Id.ToString()
                                }).ToList();
            ViewBag.departman = departmanlar;
            return View("Guncelle",pers);
        }

        public ActionResult PersonelGuncelle(Personel personel)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "/Images/" + dosyaAdi;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                personel.Fotograf = "/Images/" + dosyaAdi;
            }
            var pers = db.Personels.Find(personel.Id);
            pers.Ad = personel.Ad;
            pers.Soyad = personel.Soyad;
            pers.Fotograf = personel.Fotograf;
            pers.DepartmanID = personel.DepartmanID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelListe()
        {
            var personel = db.Personels.ToList();
            return View(personel);
        }
    }
}