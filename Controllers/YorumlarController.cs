using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using PagedList;
using PagedList.Mvc;

namespace KurumsalWeb.Controllers
{
    public class YorumlarController : Controller
    {
        private KurumsalDBContext db = new KurumsalDBContext();

        // GET: Yorumlar
        public ActionResult Index(int sayfa=1)
        {
            var yorumlar = db.Yorumlar.Include(y => y.Blog).OrderByDescending(x => x.Id);
            return View(yorumlar.ToPagedList(sayfa,10));
        }

        // GET: Yorumlar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yorumlar yorumlar = db.Yorumlar.Find(id);
            if (yorumlar == null)
            {
                return HttpNotFound();
            }
            return View(yorumlar);
        }

        // GET: Yorumlar/Create
        public ActionResult Create()
        {
            ViewBag.BlogId = new SelectList(db.Blog, "Id", "Baslik");
            return View();
        }

        // POST: Yorumlar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AdSoyad,Eposta,Yorum,BlogId,Onay")] Yorumlar yorumlar)
        {
            if (ModelState.IsValid)
            {
                db.Yorumlar.Add(yorumlar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BlogId = new SelectList(db.Blog, "Id", "Baslik", yorumlar.BlogId);
            return View(yorumlar);
        }

        // GET: Yorumlar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                var yorum = db.Yorumlar.Find(id);
                yorum.Onay = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Yorumlar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yorumlar yorumlar = db.Yorumlar.Find(id);
            if (yorumlar == null)
            {
                return HttpNotFound();
            }
            return View(yorumlar);
        }

        // POST: Yorumlar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Yorumlar yorumlar = db.Yorumlar.Find(id);
            db.Yorumlar.Remove(yorumlar);
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
