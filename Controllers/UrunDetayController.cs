using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTicariOtomasyon.Models.Siniflar;

namespace MvcTicariOtomasyon.Controllers
{
    public class UrunDetayController : Controller
    {
        Context db = new Context();
        Class1 c1 = new Class1();
        public ActionResult Index()
        {
            c1.Urun1 = db.Uruns.Where(x => x.Id == 1).ToList();
            c1.Detay1 = db.Detays.Where(y => y.DetayID == 1).ToList();
            return View(c1);
        }
    }
}