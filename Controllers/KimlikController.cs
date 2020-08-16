using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;

namespace KurumsalWeb.Controllers
{
    public class KimlikController : Controller
    {
        KurumsalDBContext db = new KurumsalDBContext();
        // GET: Kimlik
        public ActionResult Index()
        {
            var degerler = db.Kimlik.ToList();
            return View(degerler);
        }

        // GET: Kimlik/Edit/5
        public ActionResult Edit(int id)
        {
            var kimlik = db.Kimlik.Find(id);
            return View(kimlik);
        }

        // POST: Kimlik/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Kimlik kimlik, HttpPostedFileBase LogoUrl, int id)
        {
            if (ModelState.IsValid)
            {
                var k = db.Kimlik.Find(id);
                if (LogoUrl != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(k.Logo)))
                    {
                        System.IO.File.Delete(Server.MapPath(k.Logo));
                    }
                    WebImage img = new WebImage(LogoUrl.InputStream);
                    FileInfo imgInfo = new FileInfo(LogoUrl.FileName);
                    string logoName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Resize(300, 200, false, false);
                    img.Save("~/Uploads/Kimlik/" + logoName);
                    kimlik.Logo = "/Uploads/Kimlik/" + logoName;
                    k.Logo = kimlik.Logo;
                }
                k.Title = kimlik.Title;
                k.Keywords = kimlik.Keywords;
                k.Description = kimlik.Description;
                k.Unvan = kimlik.Unvan;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(kimlik);
            }
        }

    }
}
