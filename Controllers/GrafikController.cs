using MvcTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MvcTicariOtomasyon.Controllers
{
    public class GrafikController : Controller
    {
        Context db = new Context();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            var grafikCiz = new Chart(600, 600);
            grafikCiz.AddTitle("Kategori - Ürün Stok Sayısı")
                .AddLegend("Stok").
                AddSeries("Değerler",
                xValue: new[] { "Mobilya", "Ofis Eşyaları", "Bilgisayar" }
                ,yValues: new[] { 85, 66, 98 }).Write();
            return File(grafikCiz.ToWebImage().GetBytes(),"image/jpeg");
        }

        public ActionResult Index3()
        {
            ArrayList xValues = new ArrayList();
            ArrayList yValuess = new ArrayList();
            var sonuclar = db.Uruns.ToList();
            sonuclar.ToList().ForEach(x => xValues.Add(x.Ad));
            sonuclar.ToList().ForEach(y => yValuess.Add(y.Stok));
            var grafik = new Chart(width: 500, height: 500)
                .AddTitle("Stoklar")
                .AddSeries(chartType: "Column", name: "Stok", xValue: xValues, yValues: yValuess);
            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");
        }

        public ActionResult Index4()
        {

            return View();
        }

        public ActionResult VisualizeUrunResult()
        {

            return Json(UrunListesi(),JsonRequestBehavior.AllowGet);
        }

        public List<Sinif1> UrunListesi()
        {
            List<Sinif1> sinif = new List<Sinif1>();
            sinif.Add(new Sinif1()
            {
                UrunAd = "Bilgisayar",
                Stok = 120
            });
            sinif.Add(new Sinif1()
            {
                UrunAd = "Beyaz Eşya",
                Stok = 150
            });
            sinif.Add(new Sinif1()
            {
                UrunAd = "Mobilya",
                Stok = 70
            });
            sinif.Add(new Sinif1()
            {
                UrunAd = "Küçük Ev Aletleri",
                Stok = 180
            });
            sinif.Add(new Sinif1()
            {
                UrunAd = "Mobil Cihaz",
                Stok = 90
            });
            return sinif;
        }

        public ActionResult Index5()
        {
            return View();
        }
        public ActionResult VisualizeUrunResultDB()
        {
            return Json(UrunListesiDB(), JsonRequestBehavior.AllowGet);
        }

        public List<Sinif2> UrunListesiDB()
        {
            List<Sinif2> sinif = new List<Sinif2>();
            using (var context = new Context())
            {
                sinif = context.Uruns.Select(x => new Sinif2
                {
                    Urn = x.Ad,
                    Stk = x.Stok
                }).ToList();
            }
            return sinif;
        }


        public ActionResult Index6()
        {

            return View();
        }

        public ActionResult Index7()
        {

            return View();
        }
    }
}