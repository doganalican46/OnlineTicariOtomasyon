using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Siniflar;
namespace OnlineTicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        // GET: Fatura
        Context c = new Context();
        public ActionResult Index()
        {
            var liste = c.Faturalars.ToList();
            return View(liste);
        }

        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }


        [HttpPost]
        public ActionResult FaturaEkle(Faturalar fatura)
        {
            c.Faturalars.Add(fatura);
            c.SaveChanges();
            return RedirectToAction("Index");
        }


        public  ActionResult FaturaGetir(int id)
        {
            var fatura = c.Faturalars.Find(id);
            return View("FaturaGetir",fatura);
        }

        public ActionResult FaturaGuncelle(Faturalar fatura)
        {
            var f = c.Faturalars.Find(fatura.FaturaID);
            f.FaturaSeriNo = fatura.FaturaSeriNo;
            f.FaturaSiraNo = fatura.FaturaSiraNo;
            f.Saat = fatura.Saat;
            f.Tarih = fatura.Tarih;
            f.TeslimEden = fatura.TeslimEden;
            f.TeslimAlan = fatura.TeslimAlan;
            f.VergiDairesi = fatura.VergiDairesi;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FaturaDetay(int id)
        {
            var degerler = c.FaturaKalems.Where(x => x.Faturaid == id).ToList();

            return View(degerler);
        }


        [HttpGet]
        public ActionResult YeniKalem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniKalem(FaturaKalem p)
        {
            c.FaturaKalems.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}