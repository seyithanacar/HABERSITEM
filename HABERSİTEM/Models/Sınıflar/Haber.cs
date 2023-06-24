using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HABERSİTEM.Models.Sınıflar
{
    public class Haber
    {
        [Key]
        public int Haberid { get; set; }
        public string Haberbaslik { get; set; }
        public string Habermetin { get; set; }
        public string Haberresim { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Habertarih { get; set; }

        public int? Kategoriid { get; set; }
        public virtual Kategori Kategori { get; set; }

        public int? Resimid { get; set; }
        public virtual Resim Resim { get; set; }
        
        public int? Etiketid { get; set; }
        public virtual Etiket Etiket { get; set; }

        public int? Yöneticiid { get; set; }
        public virtual Yönetici Yönetici { get; set; }
    }
}