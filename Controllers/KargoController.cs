using MvcTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTicariOtomasyon.Controllers
{
    public class KargoController : Controller
    {
        Context db = new Context();
        public ActionResult Index(string ara)
        {
            var kargolar = from kargo in db.KargoDetays select kargo;
            if (!string.IsNullOrEmpty(ara))
            {
                kargolar = kargolar.Where(y => y.TakipKodu.Contains(ara));
            }
            return View(kargolar.ToList());
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            Random random = new Random();
            string[] karakterler = { "A", "B", "C", "D", "E", "F" };
            int s1, s2, s3;
            s1 = random.Next(0, 4);
            s2 = random.Next(0, 4);
            s3 = random.Next(0, 4);
            int s4, s5, s6;
            s4 = random.Next(100, 1000);
            s5 = random.Next(10, 99);
            s6 = random.Next(10, 99);
            string kod = s4.ToString() + karakterler[s1] + s5 + karakterler[s2] + s6 + karakterler[s3];
            ViewBag.TakipKodu = kod;
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(KargoDetay kargoDetay)
        {
            db.KargoDetays.Add(kargoDetay);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Takip(string id)
        {
            var detay = db.KargoTakips.Where(x => x.TakipKodu ==id).ToList();
            return View(detay);
        }
    }
}