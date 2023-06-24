using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HABERSİTEM.Models.Sınıflar;

namespace HABERSİTEM.Controllers
{
    public class HaberController : Controller
    {
        Context c = new Context();
        public ActionResult Index()
        {
            var bilgiler = c.Habers.OrderByDescending(x => x.Habertarih).Take(20).ToList();
            return View(bilgiler);
        }
        
    //[Route("Detay/{Haberid}")]
        public ActionResult Detay(int id,OkunanHaberler ok_h)
        {
            var degerler = c.Habers.Where(x => x.Haberid == id).ToList();
            var h = c.Habers.FirstOrDefault(x => x.Haberid == id);
            if (Session["ad-soyad" ] != null)
            {
                ok_h.Kullaniciid = (int?)Session["Kullanici-id"];
               
                ok_h.MyId = h.Haberid; 
               
                ok_h.Kategoriid = h.Kategoriid;
                c.OkunanHaberlers.Add(ok_h);
                c.SaveChanges();
            }
            
            return View(degerler);
        }
        //[Route("Kategori/{Kategoriid}")]
        public ActionResult Kategori(int id)
        {
            var degerler = c.Habers.Where(x => x.Kategori.Kategoriid == id).OrderByDescending(i => i.Habertarih).Take(16).ToList();
            //idsi iye eşit olanları getir tarihe göre sırala take(15)=>15ini al ToList();
            return View(degerler);
        }
        public ActionResult İletisim()
        {
          
            return View();
        }
        [HttpPost]
        public ActionResult İletisim(Mesaj m)
        {
            c.Mesajs.Add(m);
            c.SaveChanges();
            return View();
        }
    
    }
}