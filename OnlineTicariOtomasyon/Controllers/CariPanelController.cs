using System;
using System.Collections.Generic;
using System.Linq;
using OnlineTicariOtomasyon.Models.Siniflar;
using System.Web;
using System.Web.Mvc;

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
            var degerler = c.Carilers.FirstOrDefault(x => x.CariMail == carimail);
            ViewBag.m = carimail;
            return View(degerler);
        }

        public ActionResult Siparislerim()
        {
            var carimail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == carimail.ToString()).Select(y => y.CariID).FirstOrDefault();
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();

            return View(degerler); 
        }

        public ActionResult GelenMesajlar()
        {
            var carimail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x=>x.Alici==carimail).ToList();
            var gelenSayisi = c.Mesajlars.Count(x=>x.Alici==carimail).ToString();
            ViewBag.gelenmail = gelenSayisi;

            var gidenSayisi = c.Mesajlars.Count(x => x.Goncerici == carimail).ToString();
            ViewBag.gidenmail = gidenSayisi;
            return View(mesajlar);
        }

        public ActionResult GidenMesajlar()
        {
            var carimail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x => x.Goncerici == carimail).ToList();
            var gidenSayisi = c.Mesajlars.Count(x => x.Goncerici == carimail).ToString();
            ViewBag.gidenmail = gidenSayisi;

            var gelenSayisi = c.Mesajlars.Count(x => x.Alici == carimail).ToString();
            ViewBag.gelenmail = gelenSayisi;
            return View(mesajlar);
        }

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


        //[HttpGet]
        //public ActionResult YeniMesaj()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult YeniMesaj()
        //{
        //    return View();
        //}
    }
}