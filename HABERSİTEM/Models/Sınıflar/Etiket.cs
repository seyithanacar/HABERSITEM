using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HABERSİTEM.Models.Sınıflar
{
    public class Etiket
    {
        public int Etiketid { get; set; }
        public string Etiketadi { get; set; }
        public ICollection<Haber> Habers { get; set; }  
        public ICollection<OkunanHaberler> OkunanHaberlers { get; set; }  
    }
}