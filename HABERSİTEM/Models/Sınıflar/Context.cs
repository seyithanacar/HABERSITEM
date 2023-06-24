using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace HABERSİTEM.Models.Sınıflar
{
    public class Context:DbContext
    {
        public DbSet<Yönetici> Yöneticis { get; set; }
        public DbSet<Etiket> Etikets { get; set; }
        public DbSet<Haber> Habers { get; set; }
        public DbSet<Kategori> Kategoris { get; set; }
        public DbSet<Kullanici> Kullanicis { get; set; }
        public DbSet<Mesaj> Mesajs{ get; set; }
        public DbSet<Resim> Resims { get; set; }
        public DbSet<SifreTalep> SifreTaleps { get; set; }
        public DbSet<OkunanHaberler> OkunanHaberlers { get; set; }
    }
}