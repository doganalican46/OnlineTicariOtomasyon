using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Siniflar;
namespace OnlineTicariOtomasyon.Controllers
{
    public class CariController : Controller
    {
        // GET: Cari
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.Carilers.Where(x=>x.Durum==true).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult CariEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CariEkle(Cariler cari)
        {
            c.Carilers.Add(cari);
            cari.Durum = true;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CariSil(int id)
        {
            var cari = c.Carilers.Find(id);
            cari.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CariGetir(int id)
        {
            var cari = c.Carilers.Find(id);
            return View("CariGetir", cari);
        }


        public ActionResult CariGuncelle(Cariler carii)
        {
            if (!ModelState.IsValid)
            {
                return View("CariGetir");
            }
            var cari = c.Carilers.Find(carii.CariID);
            cari.CariAd=(carii.CariAd);
           cari.CariSoyad=(carii.CariSoyad);
           cari.CariSehir=(carii.CariSehir);
            cari.CariMail=(carii.CariMail);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriSatis(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            var cari = c.Carilers.Where(x => x.CariID == id).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.p = cari;
            return View(degerler);
        }
       
    }
}