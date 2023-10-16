using System;
using System.Collections.Generic;
using System.Linq;
using OnlineTicariOtomasyon.Models.Siniflar;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineTicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
        // GET: CariPanel
        Context c = new Context();
        [Authorize]
        public ActionResult Index()
        {
            var carimail = (string)Session["CariMail"];
            var degerler = c.Mesajlars.Where(x => x.Alici == carimail).ToList();
            ViewBag.m = carimail;
            var mailid = c.Carilers.Where(x => x.CariMail == carimail).Select(y => y.CariID).FirstOrDefault();
            ViewBag.mid = mailid;
            var cariSehir = c.Carilers.Where(x => x.CariMail == carimail).Select(y => y.CariSehir).FirstOrDefault();
            ViewBag.cariSehir = cariSehir;

            var toplamSatis = c.SatisHarekets.Where(x => x.Cariid == mailid).Count();
            ViewBag.toplamSatis = toplamSatis;

            

            //var toplamUrun = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => y.Adet);
            //ViewBag.toplamUrun = toplamUrun;

            var adsoyad = c.Carilers.Where(x => x.CariMail == carimail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;

            return View(degerler);
        }
        [Authorize]
        public ActionResult Siparislerim()
        {
            var carimail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == carimail.ToString()).Select(y => y.CariID).FirstOrDefault();
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();

            return View(degerler);
        }
        [Authorize]
        public ActionResult GelenMesajlar()
        {
            var carimail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x => x.Alici == carimail).OrderByDescending(y => y.MesajID).ToList();
            var gelenSayisi = c.Mesajlars.Count(x => x.Alici == carimail).ToString();
            ViewBag.gelenmail = gelenSayisi;

            var gidenSayisi = c.Mesajlars.Count(x => x.Goncerici == carimail).ToString();
            ViewBag.gidenmail = gidenSayisi;
            return View(mesajlar);
        }
        [Authorize]
        public ActionResult GidenMesajlar()
        {
            var carimail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x => x.Goncerici == carimail).OrderByDescending(y => y.MesajID).ToList();
            var gidenSayisi = c.Mesajlars.Count(x => x.Goncerici == carimail).ToString();
            ViewBag.gidenmail = gidenSayisi;

            var gelenSayisi = c.Mesajlars.Count(x => x.Alici == carimail).ToString();
            ViewBag.gelenmail = gelenSayisi;
            return View(mesajlar);
        }
        [Authorize]
        public ActionResult MesajDetay(int id)
        {
            var degerler = c.Mesajlars.Where(x => x.MesajID == id).ToList();

            var carimail = (string)Session["CariMail"];

            var gidenSayisi = c.Mesajlars.Count(x => x.Goncerici == carimail).ToString();
            ViewBag.gidenmail = gidenSayisi;

            var gelenSayisi = c.Mesajlars.Count(x => x.Alici == carimail).ToString();
            ViewBag.gelenmail = gelenSayisi;
            return View(degerler);
        }


        [Authorize]
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var carimail = (string)Session["CariMail"];

            var gidenSayisi = c.Mesajlars.Count(x => x.Goncerici == carimail).ToString();
            ViewBag.gidenmail = gidenSayisi;

            var gelenSayisi = c.Mesajlars.Count(x => x.Alici == carimail).ToString();
            ViewBag.gelenmail = gelenSayisi;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar m)
        {
            var carimail = (string)Session["CariMail"];
            m.Goncerici = carimail;
            m.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.Mesajlars.Add(m);
            c.SaveChanges();
            return View();
        }
        [Authorize]
        public ActionResult KargoTakip(string p)
        {
            var k = from x in c.KargoDetays select x;

            k = k.Where(y => y.TakipKodu.Contains(p));

            return View(k.ToList());

        }
        [Authorize]
        public ActionResult CariKargoTakip(string id)
        {
            var degerler = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();

            return View(degerler);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }


        public PartialViewResult Partial1()
        {
            var carimail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == carimail).Select(y => y.CariID).FirstOrDefault();
            var caribul = c.Carilers.Find(id);
            return PartialView("Partial1",caribul);
        }

        public ActionResult CariBilgiGuncelle(Cariler cr)
        {
            var cari = c.Carilers.Find(cr.CariID);
            cari.CariAd = cr.CariAd;
            cari.CariSoyad = cr.CariSoyad;
            cari.CariMail = cr.CariMail;
            cari.CariSehir = cr.CariSehir;
            cari.CariSifre = cr.CariSifre;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}