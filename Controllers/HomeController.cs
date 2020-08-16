using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace KurumsalWeb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private KurumsalDBContext db = new KurumsalDBContext();

        [Route("")]
        [Route("AnaSayfa")]
        public ActionResult Index()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View();
        }

        public ActionResult SliderPartial()
        {
            var degerler = db.Slider.ToList().OrderByDescending(x => x.Id);
            return PartialView(degerler);
        }

        public ActionResult HizmetPartial()
        {
            var degerler = db.Hizmet.ToList().Take(4);
            return PartialView(degerler);
        }

        [Route("Hakkimizdakiler")]
        public ActionResult Hakkimizda()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            var degerler = db.Hakkimizda.SingleOrDefault();
            return View(degerler);
        }

        [Route("Hizmetlerimiz")]
        public ActionResult Hizmetlerimiz()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            var degerler = db.Hizmet.ToList();
            return View(degerler);
        }

        [Route("BizeUlasin")]
        public ActionResult Iletisim()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View();
        }

        [HttpPost]
        public ActionResult Iletisim(Mesajlar mesajlar)
        {
            if (ModelState.IsValid)
            {
                if (mesajlar != null)
                {
                    mesajlar.Durum = true;
                    db.Mesajlar.Add(mesajlar);
                    db.SaveChanges();
                }
                return RedirectToAction("Iletisim");
            }
            else
            {
                return View();
            }
        }

        public ActionResult FooterPartial()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            ViewBag.SonBloglar = db.Blog.ToList().OrderByDescending(x => x.Id).Take(3);
            return PartialView();
        }
        [Route("BlogPost")]
        public ActionResult Blog(int? id,int sayfa = 1)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            if (id != null)
            {
                var deger = db.Blog.Include("Kategori").Where(x => x.KategoriId == id).OrderByDescending(x => x.Id).ToPagedList(sayfa, 4);
                return View(deger);
            }
            var degerler = db.Blog.Include("Kategori").OrderByDescending(x => x.Id).ToPagedList(sayfa, 4);
            return View(degerler);
        }

        public ActionResult YorumYap(Yorumlar yorumlar, int id)
        {
            var blog = db.Blog.Find(id);
            yorumlar.BlogId = id;
            yorumlar.Onay = false;
            db.Yorumlar.Add(yorumlar);
            db.SaveChanges();
            Response.Redirect("/BlogPost/"+blog.Baslik.ToLower()+"-"+id);
            return View();
        }
        [Route("BlogPost/{Baslik}-{id:int}")]
        public ActionResult BlogDetay(int? id)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            var deger = db.Blog.Include("Kategori").Include("Yorumlar").Where(x => x.Id == id).SingleOrDefault();
            return View(deger);
        }

        
        public ActionResult BlogKategori(int? id)
        {
            if (id != null)
            {
                ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
                db.Configuration.LazyLoadingEnabled = false;
                var degerler = db.Blog.Include("Kategori").Where(x => x.KategoriId == id).OrderByDescending(x => x.Id).ToList();
                return View(degerler);
            }
            else
            {
                return View();
            }
        }

        public ActionResult SonBloglarPartial()
        {
            var degerler = db.Blog.ToList().OrderByDescending(x => x.Id).Take(3);
            return PartialView(degerler);
        }

        public ActionResult BlogKategoriPartial()
        {
            var degerler = db.Kategoriler.Include("Blogs").ToList();
            return PartialView(degerler);
        }
    }
}