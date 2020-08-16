using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace KurumsalWeb.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        private KurumsalDBContext db = new KurumsalDBContext();
        public ActionResult Index(int sayfa = 1)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var degerler = db.Blog.Include("Kategori").OrderByDescending(x => x.Id).ToPagedList(sayfa, 5);
            return View(degerler);
        }

        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "Id", "KategoriAd");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Blog blog, HttpPostedFileBase resimUrl)
        {
            if (ModelState.IsValid)
            {
                if (blog != null)
                {
                    if (resimUrl != null)
                    {
                        WebImage img = new WebImage(resimUrl.InputStream);
                        FileInfo imgInfo = new FileInfo(resimUrl.FileName);
                        string resimName = Guid.NewGuid().ToString() + imgInfo.Extension;
                        img.Resize(900, 600, false, false);
                        img.Save("~/Uploads/Blog/" + resimName);
                        blog.ResimUrl = "/Uploads/Blog/" + resimName;
                    }
                    db.Blog.Add(blog);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(blog);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var b = db.Blog.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "Id", "KategoriAd", b.KategoriId);
            return View(b);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Blog blog, HttpPostedFileBase resimUrl)
        {
            if (ModelState.IsValid)
            {
                var b = db.Blog.Find(blog.Id);
                if (resimUrl != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(b.ResimUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath(b.ResimUrl));
                    }
                    WebImage img = new WebImage(resimUrl.InputStream);
                    FileInfo imgInfo = new FileInfo(resimUrl.FileName);
                    string resimName = Guid.NewGuid().ToString() + imgInfo.Extension;

                    img.Resize(900, 600, false, false);
                    img.Save("~/Uploads/Blog/" + resimName);

                    blog.ResimUrl = "/Uploads/Blog/" + resimName;
                    b.ResimUrl = blog.ResimUrl;
                }
                b.Baslik = blog.Baslik;
                b.Icerik = blog.Icerik;
                b.KategoriId = blog.KategoriId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(blog);
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var blog = db.Blog.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            db.Blog.Remove(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                db.Configuration.LazyLoadingEnabled = false;
                var degerler = db.Blog.Include("Kategori").Where(x => x.Id == id).SingleOrDefault();
                return View(degerler);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}