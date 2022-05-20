using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcTicariOtomasyon.Models.Siniflar
{
    public class Kategori
    {
        [Key]
        public int Id { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Ad { get; set; }

        //Products
        public ICollection<Urun> Uruns { get; set; }
    }
}