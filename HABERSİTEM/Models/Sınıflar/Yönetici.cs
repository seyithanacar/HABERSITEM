using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HABERSİTEM.Models.Sınıflar
{
    public class Yönetici
    {
        public int Yöneticiid { get; set; }
        public string Yöneticiadi { get; set; }
        public string Yöneticisoyadi { get; set; }
        public string Yöneticimail { get; set; }
        public string Yöneticisifre { get; set; }
        public ICollection<Haber>Habers { get; set; }   
    }
}