﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcTicariOtomasyon.Models.Siniflar
{
    public class Fatura
    {
        [Key]
        public int Id { get; set; }



        [Column(TypeName = "Varchar")]
        [StringLength(1)]
        public string SeriNo { get; set; }

        public string SiraNo { get; set; }
        public DateTime Tarih { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(60)]
        public string VergiDairesi { get; set; }

        [Column(TypeName = "char")]
        [StringLength(5)]
        public string Saat { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string TeslimEden { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string TeslimAlan { get; set; }

        public decimal Toplam { get; set; }

        public ICollection<FaturaKalem> FaturaKalems { get; set; } 


    }
}