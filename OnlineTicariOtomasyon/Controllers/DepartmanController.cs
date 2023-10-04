using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Siniflar;
namespace OnlineTicariOtomasyon.Controllers
{
    public class DepartmanController : Controller
    {
        // GET: Departman

        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.Departmans.Where(x=>x.Durum==true).ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult DepartmanEkle()
        {
            return View();

        }

        [HttpPost]
        public ActionResult DepartmanEkle(Departman departman)
        {
            c.Departmans.Add(departman);
            departman.Durum = true;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepartmanSil(int id)
        {
            var departman = c.Departmans.Find(id);
            departman.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepartmanGetir(int id)
        {
            var departman = c.Departmans.Find(id);
            return View("DepartmanGetir", departman);
        }


        public ActionResult DepartmanGuncelle(Departman k)
        {
            var departman = c.Departmans.Find(k.DepartmanID);
            departman.DepartmanAd = k.DepartmanAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult DepartmanDetay(int id)
        {
            var degerler = c.Personels.Where(x => x.Departmanid == id).ToList();

            var departman = c.Departmans.Where(x => x.DepartmanID == id).Select(y => y.DepartmanAd).FirstOrDefault();
            ViewBag.d = departman;
            return View(degerler);
        }


        public ActionResult DepartmanPersonelSatis(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.Personelid == id).ToList();
            var personel = c.Personels.Where(x => x.PersonelID == id).Select(y => y.PersonelAd + " " + y.PersonelSoyad).FirstOrDefault();
            ViewBag.p = personel;
            return View(degerler);
        }

    }
}