using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class HizmetController : Controller
    {
        // GET: Hizmet
        KurumsalDBContext db = new KurumsalDBContext();
        public ActionResult Index()
        {
            var degerler = db.Hizmet.ToList();
            return View(degerler);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Hizmet hizmet, HttpPostedFileBase resimUrl)
        {
            if (ModelState.IsValid)
            {
                if (resimUrl != null)
                {
                    WebImage img = new WebImage(resimUrl.InputStream);
                    FileInfo imgInfo = new FileInfo(resimUrl.FileName);

                    string resimName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Resize(500, 500, false, false);
                    img.Save("~/Uploads/Hizmet/" + resimName);

                    hizmet.ResimUrl = "/Uploads/Hizmet/" + resimName;
                }
                db.Hizmet.Add(hizmet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hizmet);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var hizmet = db.Hizmet.Find(id);
                if (System.IO.File.Exists(Server.MapPath(hizmet.ResimUrl)))
                {
                    System.IO.File.Delete(Server.MapPath(hizmet.ResimUrl));
                }
                db.Hizmet.Remove(hizmet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                ViewBag.Uyari = "Güncellenecek Hizmet Bulunamadı!";
            }
            var deger = db.Hizmet.Find(id);
            if (deger == null)
            {
                return HttpNotFound();
            }
            return View(deger);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Hizmet hizmet, HttpPostedFileBase resimUrl, int? id)
        {
            if (ModelState.IsValid)
            {
                var h = db.Hizmet.Find(hizmet.Id);
                if (resimUrl != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(h.ResimUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath(h.ResimUrl));
                    }
                    WebImage img = new WebImage(resimUrl.InputStream);
                    FileInfo imgInfo = new FileInfo(resimUrl.FileName);

                    string resimName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Resize(500, 500, false, false);
                    img.Save("~/Uploads/Hizmet/" + resimName);
                    hizmet.ResimUrl = "/Uploads/Hizmet/" + resimName;
                    h.ResimUrl = hizmet.ResimUrl;
                }
                h.Baslik = hizmet.Baslik;
                h.Aciklama = hizmet.Aciklama;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hizmet);
        }
    }
}