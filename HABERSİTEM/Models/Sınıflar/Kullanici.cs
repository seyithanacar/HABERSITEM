using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HABERSİTEM.Models.Sınıflar
{
    public class Kullanici
    {
        public int Kullaniciid { get; set; }
        public string Kullaniciadi { get; set; }
        public string Kullanicisoyadi { get; set; }
        public string Kullanicimail { get; set; }
        public string Kullanicisifre { get; set; }
        public ICollection<Mesaj> Mesajs { get; set; }
        public ICollection<OkunanHaberler> OkunanHaberlers { get; set; }

    }
}