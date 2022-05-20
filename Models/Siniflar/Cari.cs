using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcTicariOtomasyon.Models.Siniflar
{
    public class Cari
    {
        [Key]
        public int Id { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(30,ErrorMessage ="En fazla 30 karakter girebilirsiniz.")]
        public string Ad { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        [Required(ErrorMessage ="Bu alan boş geçilemez.")]
        public string Soyad { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(13)]
        public string Sehir { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string Mail { get; set; }



        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string Sifre { get; set; }

        public bool Durum { get; set; }

        public ICollection<SatisHareket> SatisHarekets { get; set; }


    }
}