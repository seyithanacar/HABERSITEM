
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using HABERSİTEM.Models.Sınıflar;

namespace HABERSİTEM.Controllers
{
    public class YöneticiController : Controller
    {
        Context c = new Context();

        public ActionResult Index()
        {
            var degerler = c.Habers.OrderByDescending(x => x.Habertarih).Take(15).ToList();
            return View(degerler);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Yönetici y)
        {
            var bilgiler = c.Yöneticis.FirstOrDefault(x => x.Yöneticimail == y.Yöneticimail && x.Yöneticisifre == y.Yöneticisifre); //Gelen user db'de mevcut mu?

            if (bilgiler != null)
            {
                //FormsAuthentication.SetAuthCookie(userInDb.Mail, false);    //Kullanıcı artık authantice yaptık (yetkilendirdik) yani sayfalarda gezebilir
                Session["ad_soyad"] = bilgiler.Yöneticiadi + " " + bilgiler.Yöneticisoyadi;
                Session["yönetici_id"] = bilgiler.Yöneticiid;
                ViewBag.Mesaage1 = Session["ad_soyad"];
                idup(bilgiler.Yöneticiid);
                return RedirectToAction("HaberEkle", "Yönetici", new { bilgiler.Yöneticiid });

            }
            else
            {
                TempData["msg"] = "<script>alert('MAİL VE YA ŞİFRE HATALI !');</script>";

                return View();
            }
        }


        [HttpGet]
        public ActionResult HaberEkle()
        {
            List<SelectListItem> degerler = (from i in c.Kategoris.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.Kategoriadi,
                                                 Value = i.Kategoriid.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;

            return View();
        }

        [HttpPost]
        public ActionResult HaberEkle(Haber h)
        {
            if (Request.Files.Count > 0)
            {
               
                if (Path.GetFileName(Request.Files[0].FileName) != null)
                {
                    var fileName = Path.GetFileName(Request.Files[0].FileName);
                    var filePath = Path.Combine(Server.MapPath("~/Image"), fileName);
                    Request.Files[0].SaveAs(filePath);
                    h.Haberresim = "/Image/" + fileName;
                    h.Resimid = ResimEkle(h.Haberresim);
                }
                if (Session["yönetici_id"] == null)
                    h.Yöneticiid = 1;
                else
                    h.Yöneticiid = (int)Session["yönetici_id"];

            }

            h.Habertarih = DateTime.Parse(DateTime.Now.ToLongDateString());

            c.Habers.Add(h);
            c.SaveChanges();
            return RedirectToAction("HaberEkle");
        }

        [HttpGet]
        public ActionResult HaberGüncelle(int id)
        {

            var haber = c.Habers.Find(id);

            List<SelectListItem> degerler = (from i in c.Kategoris.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.Kategoriadi,
                                                 Value = i.Kategoriid.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;


            ViewBag.resim = haber.Haberresim;
            return View(haber);
        }

        [HttpPost]
        public ActionResult HaberGüncelle(Haber haber)
        {
            var hbr = c.Habers.Find(haber.Haberid);
            int k = haber.Haberid;
            if (hbr == null)
            {
                return View();
            }

            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/Image"), fileName);
                    file.SaveAs(filePath);
                    hbr.Haberresim = "/Image/" + fileName;
                }
            }
            
            hbr.Haberbaslik = haber.Haberbaslik;
            hbr.Habermetin= haber.Habermetin;
            hbr.Kategoriid = haber.Kategoriid;
            hbr.Haberid = haber.Haberid;

            hbr.Habertarih = haber.Habertarih;
            
            c.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult KategoriEkle()
        {

            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori k)
        {
            c.Kategoris.Add(k);
            c.SaveChanges();
            return RedirectToAction("KategoriListele");
        }
        public ActionResult KategoriListele()
        {
            var degerler = c.Kategoris.ToList();
            return View(degerler);
        }
        public ActionResult KategoriSil(int id)
        {
            var s = c.Kategoris.Find(id);
            c.Kategoris.Remove(s);
            c.SaveChanges();
            return RedirectToAction("KategoriListele");
        }
        public ActionResult HaberSil(int id)
        {
            var s = c.Habers.Find(id);
            c.Habers.Remove(s);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SifreTalepListele()
        {
            var degerler = c.SifreTaleps.ToList();

            return View(degerler);
        }
        public ActionResult MesajListele()
        {
            var degerler = c.Mesajs.ToList();

            return View(degerler);
        }
        public ActionResult MesajSil(int id)
        {
            var s = c.Mesajs.Find(id);
            c.Mesajs.Remove(s);
            c.SaveChanges();
            return RedirectToAction("MesajListele");
        }
        public int ResimEkle(string s)
        {
            Resim r = new Resim();
            r.Resimadi = s;
            c.Resims.Add(r);
            c.SaveChanges();
            return r.Resimid;
        }
        public int EtiketEkle(string s)
        {
            var controll = c.Etikets.FirstOrDefault(x => x.Etiketadi == s);
            if (controll != null)
            {
                return controll.Etiketid;
            }

            Etiket e = new Etiket();
            e.Etiketadi = s;
            c.Etikets.Add(e);
            c.SaveChanges();
            return e.Etiketid;
        }

        int adminid;
        public int idup(int id)
        {
            adminid = id;
            return adminid;
        }
        public ActionResult SifreTalepSil(int id)
        {
            var controll = c.SifreTaleps.FirstOrDefault(x => x.Kullaniciid_t == id);


            c.SifreTaleps.Remove(controll);
            c.SaveChanges();
            return RedirectToAction("SifreTalepListele");
        }
        public ActionResult SifreTalepOnayla(int id)
        {
            var controll = c.SifreTaleps.FirstOrDefault(x => x.Kullaniciid_t == id);
            var k = c.Kullanicis.FirstOrDefault(x => x.Kullanicimail == controll.Kullanicimail_t);
            k.Kullanicisifre = controll.Kullanicisifre_t;
            c.SifreTaleps.Remove(controll);
            c.SaveChanges();
            return RedirectToAction("SifreTalepListele");
        }
    }
}