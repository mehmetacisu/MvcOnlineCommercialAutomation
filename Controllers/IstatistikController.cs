using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTicariOtomasyon.Models.Siniflar;

namespace MvcTicariOtomasyon.Controllers
{
    public class IstatistikController : Controller
    {
        Context db = new Context();
        public ActionResult Index()
        {
            var CariSayisi = db.Caris.Count().ToString();
            ViewBag.toplamCari = CariSayisi;

            var UrunSayisi = db.Uruns.Count().ToString();
            ViewBag.toplamUrun = UrunSayisi;

            var PersonelSayisi = db.Personels.Count().ToString();
            ViewBag.toplamPersonel = PersonelSayisi;

            var KategoriSayisi = db.Kategoris.Count().ToString();
            ViewBag.toplamKategori = KategoriSayisi;

            var StokSayisi = db.Uruns.Sum(x => x.Stok).ToString();
            ViewBag.toplamStok = StokSayisi;

            var MarkaSayisi = (from urun in db.Uruns select urun.Marka).Distinct().Count().ToString();
            ViewBag.toplamMarka = MarkaSayisi;

            var KritikSv = db.Uruns.Count(x => x.Stok <= 20).ToString();
            ViewBag.kritik = KritikSv;

            var PahaliUrun = (from urun in db.Uruns orderby urun.SatisFiyat descending select urun.Ad).FirstOrDefault();
            ViewBag.UrunPahali = PahaliUrun;

            var UcuzUrun = (from urun in db.Uruns orderby urun.SatisFiyat ascending select urun.Ad).FirstOrDefault();
            ViewBag.UrunUcuz = UcuzUrun;

            var MaxMarka = db.Uruns.GroupBy(x => x.Marka).
                OrderByDescending(y => y.Count()).Select(z => z.Key)
                .FirstOrDefault();
            ViewBag.MarkaMax = MaxMarka;

            var BuzdolabiSayisi = db.Uruns.Count(x => x.Ad.Equals("Buzdolabı")).ToString();
            ViewBag.toplamBuzdolabi = BuzdolabiSayisi;

            var LaptopSayisi = db.Uruns.Count(x => x.Ad.Equals("Laptop")).ToString();
            ViewBag.toplamLaptop = LaptopSayisi;

            var KasaTutari = db.SatisHarekets.Sum(x => x.Tutar).ToString();
            ViewBag.kasaToplam = KasaTutari;


            var bugunkuSatis = db.SatisHarekets.Count(x => x.Tarih == DateTime.Today).ToString();
            ViewBag.bugunSatisToplam = bugunkuSatis;

            try
            {
                var bugunkuKasa = db.SatisHarekets.Where(x => x.Tarih == DateTime.Today)
               .Sum(y => y.Tutar).ToString();
                ViewBag.bugunkuKasaToplami = bugunkuKasa;
            }
            catch (Exception)
            {

                ViewBag.bugunkuKasaToplami = "0";

            }
            var enCokSatan = db.Uruns.Where(u => u.Id == (db.SatisHarekets.GroupBy(x => x.UrunID)
                 .OrderByDescending(z => z.Count()).Select(y => y.Key)
                 .FirstOrDefault()))
                .Select(k => k.Ad).FirstOrDefault();
            ViewBag.cokSatan = enCokSatan;

            return View();
        }


        public ActionResult KolayTablo()
        {
            var sorgu = from cari in db.Caris
                        group cari by cari.Sehir into g
                        select new SinifGrup
                        {
                            Sehir = g.Key,
                            Sayi = g.Count()
                        };
            return View(sorgu.ToList());
        }

        public PartialViewResult PartialTablo()
        {
            var personelSorgu = from pers in db.Personels
                                group pers by pers.Departman.Ad into persDep
                                select new SinifGrup2
                                {
                                    Departman = persDep.Key,
                                    Sayi = persDep.Count()
                                };
            return PartialView(personelSorgu.ToList());
        }

        public PartialViewResult PartialCariTablo()
        {
            var cariSorgu = db.Caris.ToList();
            return PartialView(cariSorgu);
        }

        public PartialViewResult PartialUrunTablo()
        {
            var urunSorgu = db.Uruns.ToList();
            return PartialView(urunSorgu);
        }

        public PartialViewResult PartialMarkaTablo()
        {
            var markaSorgu = from urun in db.Uruns
                             group urun by urun.Marka into kat
                             select new SinifGrup3
                             {
                                 Marka = kat.Key,
                                 Sayi = kat.Count()
                             };
            return PartialView(markaSorgu.ToList());
        }
    }
}