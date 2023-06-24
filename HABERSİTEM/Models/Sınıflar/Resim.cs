using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HABERSİTEM.Models.Sınıflar
{
    public class Resim
    {
        public int Resimid { get; set; }
        public string Resimadi { get; set; }
        public ICollection<Haber> Habers { get; set; }
    }
}