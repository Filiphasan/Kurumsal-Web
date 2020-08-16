using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class HakkimizdaController : Controller
    {
        // GET: Hakkimizda
        KurumsalDBContext db = new KurumsalDBContext();
        public ActionResult Index()
        {
            var degerler = db.Hakkimizda.ToList();
            return View(degerler);
        }

        public ActionResult Edit(int id)
        {
            var degerler = db.Hakkimizda.Find(id);
            return View(degerler);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Hakkimizda hakkimizda, HttpPostedFileBase resimUrl)
        {
            if (ModelState.IsValid)
            {
                var h = db.Hakkimizda.Find(hakkimizda.Id);
                if (resimUrl != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(h.ResimUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath(h.ResimUrl));
                    }
                    WebImage img = new WebImage(resimUrl.InputStream);
                    FileInfo imgInfo = new FileInfo(resimUrl.FileName);
                    string resimName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Resize(1200, 675, false, false);
                    img.Save("~/Uploads/Hakkimizda/" + resimName);
                    hakkimizda.ResimUrl = "/Uploads/Hakkimizda/" + resimName;
                    h.ResimUrl = hakkimizda.ResimUrl;
                }
                h.Aciklama = hakkimizda.Aciklama;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}