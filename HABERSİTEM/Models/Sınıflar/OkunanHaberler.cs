using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HABERSİTEM.Models.Sınıflar
{
    public class OkunanHaberler
    {

        [Key]
        public int MyId { get; set; }

        public int? Kullaniciid { get; set; }
        public virtual Kullanici Kullanici{ get; set; }

        public int? Kategoriid { get; set; }
        public virtual Kategori Kategori { get; set; }

        public int? Etiketid { get; set; }
        public virtual Etiket Etiket { get; set; }
    }
}