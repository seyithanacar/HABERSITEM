using HABERSİTEM.Models.Sınıflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HABERSİTEM.Controllers
{
    public class OneriAlgoritmalarıController : Controller
    {
        // GET: OneriAlgoritmaları

        Context c = new Context();

        public ActionResult CONTENTBASED()
        {
            if (Session["Kullanici-id"] == null)
            {
                TempData["msg"] = "<script>alert('KULLANICI GİRİŞİ YAPMALISINIZ!');</script>";
                return RedirectToAction("Index", "Haber", TempData["msg"]);
            }
            int id = (int)Session["Kullanici-id"];
           

            var OkunanHaberler = c.OkunanHaberlers.Where(x => x.Kullaniciid == id).ToList();
            // Kullanıcının daha önce okuduğu haberleri getir 

            //var ctrl = c.OkunanHaberlers.Where(x => x.Kullaniciid == id).ToList();
            if (OkunanHaberler.Count == 0)
            {
                // Oturum açmamış bir kullanıcı olduğunda uyarı mesajı gösteriliyor ve Index action'ına yönlendiriliyor.
                TempData["msg1"] = "<script>alert('HABER ÖNERSİ ALMAK İÇİN BİR KAÇ HABERE GÖZ ATINIZ!');</script>";
                return RedirectToAction("Index", "Haber", TempData["msg"]);
            }

            var okunanKategoriler = c.OkunanHaberlers
                .Where(x => x.Kullaniciid == id)
                .GroupBy(x => x.Kategoriid)
                .Select(g => new { KategoriId = g.Key, OkunanHaberSayisi = g.Count() })
                .OrderByDescending(g => g.OkunanHaberSayisi)
                .ToList();
            //getirilen haberleri kategorilere göre gruplandır okunan  haber sayısını tut ve buna göre kategorileri büyükten küçüğe sırala

            var onerilecekHaberler = new List<Haber>();


            foreach (var okunanKategori in okunanKategoriler)
            {
                var benzerHaberler = new List<Haber>();


                int haberSayisi = (10) * okunanKategori.OkunanHaberSayisi / okunanKategoriler.Sum(x => x.OkunanHaberSayisi);

                benzerHaberler = c.Habers
                     .OrderByDescending(h => h.Habertarih)
                     .Where(h => h.Kategoriid == okunanKategori.KategoriId)
                     //&& c.OkunanHaberlers.Any(o => o.MyId == h.Haberid && o.Kullaniciid == id) 
                     //kullanıcının daha önce okuduklarını önermemesi için üstteki kod
                     .Take(haberSayisi)
                     .ToList();

                onerilecekHaberler.AddRange(benzerHaberler);

                //kullanıcının okuduğu haberlerin kategorileriyle aynı kategorideki haberleri benzerhaberler listesinde tuttuk
                //kategorilerin okunan haber sayısına göre 10 haberi oransal olarak kaydet


            }
            var oneriler = onerilecekHaberler.OrderByDescending(h => h.Habertarih).ToList();
            return View(oneriler);
        }
        public ActionResult COLLABORATIVEFİLTERING()
        {
            if (Session["Kullanici-id"] == null)
            {
                // Oturum açmamış bir kullanıcı olduğunda uyarı mesajı gösteriliyor ve Index action'ına yönlendiriliyor.
                TempData["msg"] = "<script>alert('KULLANICI GİRİŞİ YAPMALISINIZ!');</script>";
                return RedirectToAction("Index", "Haber", TempData["msg"]);
            }

            int id = (int)Session["Kullanici-id"];



            var kullanicilar = c.Kullanicis;
            // Farklı kullanıcıların listesi alınıyor.

            var okunanKategoriler = c.OkunanHaberlers
               .Where(x => x.Kullaniciid == id)
               .GroupBy(x => x.Kategoriid)
               .Select(g => new { KategoriId = g.Key, OkunanHaberSayisi = g.Count() })
               .OrderByDescending(g => g.OkunanHaberSayisi)
               .ToList();

            // Oturum açmış kullanıcının okuduğu haberlerin kategorileri ve her kategori için okunma sayısı alınıyor.

            //var ctrl = c.OkunanHaberlers.Where(x => x.Kullaniciid == id).ToList();
            

            var haberler = c.OkunanHaberlers
              .Where(x => x.Kullaniciid != id)
              .GroupBy(x => x.MyId)
              .Select(g => new
              {
                  HaberId = g.Key,
                  OkumaSayisi = g.Count()
              })
              .ToList();

            // Diğer kullanıcıların okuduğu haberleri okunma sayısıyla birlikte gruplandırdık

            var onerilecekHaberler = new List<Haber>();

            foreach (var okunanKategori in okunanKategoriler)
            {
                var benzerHaberler = new List<Haber>();

                int haberSayisi = (10 * okunanKategori.OkunanHaberSayisi) / okunanKategoriler.Sum(x => x.OkunanHaberSayisi);

                var haberIdListesi = haberler
                    .OrderByDescending(h => h.OkumaSayisi)
                    .Select(h => h.HaberId)
                    .ToList();
               // diğer kullanıcıların okuduğu haberlerin  idleri okuma sayılarına göre belirlendi 

                benzerHaberler = c.Habers
                    .Where(h => haberIdListesi.Contains(h.Haberid) && h.Kategoriid == okunanKategori.KategoriId)
                    .Take(haberSayisi)
                    .ToList();
                // önerilecek haberler daha önce berlirlenen adet sayısına göre listelendi 

                onerilecekHaberler.AddRange(benzerHaberler.OrderByDescending(h => h.Habertarih));
            }

            // Benzer haberler listesine eklenen haberlerin tarihine göre sıralama yapıldı.


            var siraliHaberler = onerilecekHaberler
                .OrderByDescending(h => h.Habertarih)
                .ToList();

            if (siraliHaberler.Count == 0)
            {
                // Oturum açmamış bir kullanıcı olduğunda uyarı mesajı gösteriliyor ve Index action'ına yönlendiriliyor.
                TempData["msg1"] = "<script>alert('HABER ÖNERSİ ALMAK İÇİN BİR KAÇ HABERE GÖZ ATINIZ!');</script>";
                return RedirectToAction("Index", "Haber", TempData["msg"]);
            }

            return View(siraliHaberler);
        }





    }
}