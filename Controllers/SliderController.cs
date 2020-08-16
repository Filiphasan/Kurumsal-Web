using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;

namespace KurumsalWeb.Controllers
{
    public class SliderController : Controller
    {
        private KurumsalDBContext db = new KurumsalDBContext();

        // GET: Slider
        public ActionResult Index()
        {
            return View(db.Slider.ToList());
        }

        // GET: Slider/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: Slider/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Slider/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Baslik,Aciklama,ResimUrl")] Slider slider, HttpPostedFileBase resimUrl)
        {
            if (ModelState.IsValid)
            {
                if (resimUrl != null)
                {
                    WebImage img = new WebImage(resimUrl.InputStream);
                    FileInfo imgInfo = new FileInfo(resimUrl.FileName);
                    string resimName = Guid.NewGuid().ToString() + imgInfo.Extension;

                    img.Resize(1120, 460, false, false);
                    img.Save("~/Uploads/Slider/"+resimName);
                    slider.ResimUrl = "/Uploads/Slider/" + resimName;
                }
                db.Slider.Add(slider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(slider);
        }

        // GET: Slider/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Slider/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Baslik,Aciklama,ResimUrl")] Slider slider, HttpPostedFileBase resimUrl)
        {
            if (ModelState.IsValid)
            {
                var s = db.Slider.Find(slider.Id);
                if (resimUrl != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(s.ResimUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath(s.ResimUrl));
                    }
                    WebImage img = new WebImage(resimUrl.InputStream);
                    FileInfo imgInfo = new FileInfo(resimUrl.FileName);
                    string resimName = Guid.NewGuid().ToString() + imgInfo.Extension;

                    img.Resize(1120, 460, false, false);
                    img.Save("~/Uploads/Slider/"+resimName);
                    slider.ResimUrl = "/Uploads/Slider/" + resimName;
                    s.ResimUrl = slider.ResimUrl;
                }
                s.Baslik = slider.Baslik;
                s.Aciklama = slider.Aciklama;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        // GET: Slider/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Slider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = db.Slider.Find(id);
            db.Slider.Remove(slider);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
