using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcTicariOtomasyon.Models.Siniflar
{
    public class DinamikFatura
    {
        public IEnumerable<Fatura> degerFatura { get; set; }
        public IEnumerable<FaturaKalem> degerKalem { get; set; }

    }
}