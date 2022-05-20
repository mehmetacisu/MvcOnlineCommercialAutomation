using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcTicariOtomasyon.Models.Siniflar
{
    public class SatisHareket
    {
        [Key]
        public int Id { get; set; }

        //ürün , cari (müşteri),personel

        public DateTime Tarih { get; set; }
        public int Adet { get; set; }
        public decimal Fiyat { get; set; }
        public decimal Tutar { get; set; }

        public int UrunID { get; set; }

        public int CariID { get; set; }

        public int PersonelID { get; set; }
        public virtual Urun Urun { get; set; }
        public virtual Cari Cari { get; set; }
        public virtual Personel Personel { get; set; }


    }
}