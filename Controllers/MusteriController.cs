using FilmSitesi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmSitesi.Controllers
{
    public class MusteriController : Controller
    {
        private readonly MvcMovieContext _db ;
        public MusteriController( MvcMovieContext db)
        {
            _db = db;
        }
        // GET: Musteri
        public ActionResult Index(string p)
        {
            var liste = from d in _db.Musteriler select d;
            if (!string.IsNullOrEmpty(p))
            {
                liste = liste.Where(m => m.AdSoyad.Contains(p));
            }
            return View(liste.ToList());
        }
        [HttpGet]
        public ActionResult EKLE()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EKLE(Musteri musteri)
        {
            _db.Musteriler.Add(musteri);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SIL(int id)
        {
            var musteri = _db.Musteriler.Find(id);
            _db.Musteriler.Remove(musteri);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult GUNCELLE(int id)
        {
            var musteri = _db.Musteriler.Find(id);
            return View("GUNCELLE", musteri);
        }
        [HttpPost]
        public ActionResult GUNCELLE(Musteri musteri)
        {
            var guncel = _db.Musteriler.Find(musteri.Id);
            guncel.AdSoyad = musteri.AdSoyad;
            guncel.EMail = musteri.EMail;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
