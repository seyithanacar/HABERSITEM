using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HABERSİTEM.Models.Sınıflar
{
    public class Kategori
    {
        [Key]
        public int Kategoriid { get; set; }
        public string Kategoriadi { get; set; }
        public ICollection<Haber> Habers { get; set; }

        public ICollection<OkunanHaberler> OkunanHaberlers { get; set; }

    }
}