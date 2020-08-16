using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        KurumsalDBContext db = new KurumsalDBContext();
        [Route("yonetimpaneli")]
        public ActionResult Index()
        {
            ViewBag.blogSay = db.Blog.Count();
            ViewBag.kategoriSay = db.Kategoriler.Count();
            ViewBag.yorumSay = db.Yorumlar.Count();
            ViewBag.hizmetSay = db.Hizmet.Count();
            ViewBag.sliderSayisi = db.Slider.Count();
            ViewBag.okunmamisMesajSayisi = db.Mesajlar.Where(x => x.Durum == false).Count();
            ViewBag.okunmusMesajSayisi = db.Mesajlar.Where(x => x.Durum == true).Count();
            var encokYorumAlanBlogId = (from x in db.Yorumlar.GroupBy(x => x.BlogId)
                                        orderby x.Count() descending
                                        select new
                                        {
                                            x.Key
                                        }).Select(x => x.Key).FirstOrDefault();
            ViewBag.encokYorumAlanBlog = db.Blog.Find(encokYorumAlanBlogId);
            ViewBag.Bildirim = db.Yorumlar.Where(x => x.Onay == false).OrderByDescending(x => x.Id).Count();
            return View();
        }
        [Route("yonetimpaneli/giris")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            string admn = Crypto.Hash(admin.Sifre, "MD5");
            var login = db.Admin.Where(x => x.Eposta == admin.Eposta && x.Sifre == admn).SingleOrDefault();
            if (login != null)
            {
                Session["adminId"] = login.Id;
                Session["eposta"] = login.Eposta;
                Session["yetki"] = login.Yetki;
                return RedirectToAction("Index", "Admin");
            }
            ViewBag.uyari = "E-Posta veya Şifre Hatalı!";
            return View();
        }

        public ActionResult LogOut()
        {
            Session["adminId"] = null;
            Session["eposta"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
        }

        public ActionResult SifremiUnuttum()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SifremiUnuttum(string Eposta)
        {
            if (Eposta != null)
            {
                var sorgu = db.Admin.Where(x => x.Eposta == Eposta).SingleOrDefault();
                if (sorgu != null)
                {
                    Random random = new Random();
                    int rnd = random.Next(1000, 99999);
                    sorgu.Sifre = Crypto.Hash(rnd.ToString(), "MD5");
                    db.SaveChanges();
                    WebMail.SmtpServer = "smtp.gmail.com";
                    WebMail.EnableSsl = true;
                    WebMail.SmtpPort = 587;
                    WebMail.UserName = "hasaerda@gmail.com";
                    WebMail.Password = "filip483706";
                    WebMail.Send(Eposta, "Hesap Şifresi", "Şifreniz:" + rnd.ToString());
                    return RedirectToAction("Login");
                }
                return View();
            }
            return View();            
        }

        public ActionResult Adminler()
        {
            var degerler = db.Admin.ToList();
            return View(degerler);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Admin admin, string Eposta, string Sifre)
        {
            if (ModelState.IsValid)
            {
                admin.Sifre = Crypto.Hash(Sifre, "MD5");
                db.Admin.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Adminler");
            }
            return View(admin);
        }

        public ActionResult Edit(int id)
        {
            var deger = db.Admin.Find(id);
            return View(deger);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Admin admin,string Sifre)
        {
            if (ModelState.IsValid)
            {
                var admn = db.Admin.Find(admin.Id);
                if (admin.Sifre != admn.Sifre)
                {
                    admn.Sifre = Crypto.Hash(Sifre, "MD5");
                }
                admn.Eposta = admin.Eposta;
                admn.Yetki = admin.Yetki;
                db.SaveChanges();
                return RedirectToAction("Adminler");
            }
            return View(admin);
        }

        public ActionResult Delete(int? id)
        {
            if (db.Admin.Count()>1)
            {
                var admin = db.Admin.Find(id);
                db.Admin.Remove(admin);
                db.SaveChanges();
                return RedirectToAction("Adminler");
            }
            return RedirectToAction("Adminler");
        }
    }
}