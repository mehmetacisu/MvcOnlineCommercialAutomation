using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcTicariOtomasyon.Models.Siniflar
{
    public class Personel
    {
        [Key]
        public int Id { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Ad { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Soyad { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(250)]
        public string Fotograf { get; set; }
        public ICollection<SatisHareket> SatisHarekets { get; set; }

        public int DepartmanID { get; set; }
        public virtual Departman Departman { get; set; }

    }
}