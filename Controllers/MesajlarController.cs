using KurumsalWeb.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;

namespace KurumsalWeb.Controllers
{
    public class MesajlarController : Controller
    {
        // GET: Mesajlar
        KurumsalDBContext db = new KurumsalDBContext();
        public ActionResult Index()
        {
            var degerler = db.Mesajlar.Where(x => x.Durum == true);
            return View(degerler);
        }

        public ActionResult OkunmusMesajlar()
        {
            var degerler = db.Mesajlar.Where(x => x.Durum == false).ToList();
            return View(degerler);
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var mesaj = db.Mesajlar.Find(id);
                mesaj.Durum = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }

        }

        public ActionResult RealDelete(int? id)
        {
            if (id != null)
            {
                var mesaj = db.Mesajlar.Find(id);
                db.Mesajlar.Remove(mesaj);
                db.SaveChanges();
                return RedirectToAction("OkunmusMesajlar");
            }
            else
            {
                return RedirectToAction("OkunmusMesajlar");
            }
        }

        public ActionResult Cevapla(int id)
        {
            var deger = db.Mesajlar.Find(id);
            return View(deger);
        }

        [HttpPost]
        public ActionResult Cevapla(int? id, string aliciMail, string konu, string mesaj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    WebMail.SmtpServer = "smtp.gmail.com";
                    WebMail.EnableSsl = true;
                    WebMail.UserName = "hasaerda@gmail.com";
                    WebMail.Password = "filip483706";
                    WebMail.SmtpPort = 587;
                    WebMail.Send(aliciMail, konu, mesaj);
                    if (id != null)
                    {
                        var m = db.Mesajlar.Find(id);
                        m.Durum = false;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                if (id != null)
                {
                    var m = db.Mesajlar.Find(id);
                    m.Durum = false;
                    db.SaveChanges();
                }
                return View();
            }

        }
    }
}