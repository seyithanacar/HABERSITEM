using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HABERSİTEM.Models.Sınıflar
{
    public class Mesaj
    {
        public int Mesajid { get; set; }
        public string Mesajbaşlık { get; set; }
        public string Mesajiçerik { get; set; }
        public int Kullaniciid { get; set; }
        public virtual Kullanici Kullanici { get; set; }
    }
}