using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HABERSİTEM.Models.Sınıflar
{
    public class SifreTalep
    {
        [Key]

        public int Kullaniciid_t { get; set; }
        public string Kullaniciadi_t_t{ get; set; }
        public string Kullanicisoyadi_t { get; set; }
        public string Kullanicimail_t { get; set; }
        public string Kullanicisifre_t { get; set; }
        public string Kullanicisifretekrarı_t { get; set; }
    }
}