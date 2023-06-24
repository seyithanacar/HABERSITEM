using HABERSİTEM.Models.Sınıflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HABERSİTEM.Controllers
{
    public class KullaniciController : Controller
    {
        // GET: Kullanici
        Context c = new Context();
        public ActionResult Login()  
        {
            Session["ad-soyad"] = null;


            return View();
        }
        [HttpPost]
        public ActionResult Login(Kullanici k)
        {
            var control = c.Kullanicis.FirstOrDefault(x=> x.Kullanicimail==k.Kullanicimail && x.Kullanicisifre==k.Kullanicisifre);
            if(control!= null)
            {
                FormsAuthentication.SetAuthCookie(control.Kullanicimail,false);
                Session["Kullanici-id"] = control.Kullaniciid;
                Session["ad-soyad"] = control.Kullaniciadi+ " " +control.Kullanicisoyadi;
                return RedirectToAction("Index","Haber");
            }
            else 
            {
                TempData["msg"] = "<script>alert('MAİL VE YA ŞİFRE HATALI !');</script>";
                return View();
            }
            
        }
        public ActionResult Kayit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Kayit(Kullanici k)
        {
            c.Kullanicis.Add(k);
            c.SaveChanges();
            TempData["msg"] = "<script>alert('KULLANICI KAYDI OLUŞTURULDU!');</script>";
            TempData["msg2"] = "<script>alert('KULLANICI GİRİŞ SAYFASINA YÖNLENDİRİLİYORSUNUZ!');</script>";
            return RedirectToAction("Login","Kullanici", TempData["msg"]);
        }
        public ActionResult SifreTalebi()
        {

             return View();
        }
        [HttpPost]
        public ActionResult SifreTalebi(SifreTalep t)
        {

            c.SifreTaleps.Add(t);
            c.SaveChanges();
            TempData["msg"] = "<script>alert('ŞİFRE TALEBİ OLUŞTURULDU!');</script>";
            return View(TempData["msg"]);
        }
    }
}