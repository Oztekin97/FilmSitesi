using FilmSitesi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmSitesi.Controllers
{
    public class RezervasyonKayitController : Controller
    {
        private readonly MvcMovieContext _db;
        public RezervasyonKayitController(MvcMovieContext db)
        {
            _db = db;
        }
        public IActionResult Index(string p)
        {
            var liste = from d in _db.Rezervasyonlar select d;
            if (!string.IsNullOrEmpty(p))
            {
                liste = liste.Where(m => m.AdSoyad.Contains(p));
            }
            return View(liste.ToList());
        }
        [HttpGet]
        public IActionResult YeniSatis()
        {
            return View();
        }
        [HttpPost]
        public IActionResult YeniSatis(Rezervasyon rezervasyon)
        {
            _db.Rezervasyonlar.Add(rezervasyon);
            SqlConnection baglanti = new SqlConnection(@"Data Source=LAPTOP-VKU0E66J\SQLEXPRESS;Initial Catalog=MOVIE;Integrated Security=True");
            SqlCommand komut = new SqlCommand("UPDATE Movie SET Stok=Stok+1,Total=(Stok+1)*Price  WHERE  Id=@mid", baglanti);
             komut.Parameters.AddWithValue("@mid", rezervasyon.MovieId);
             baglanti.Open();
             komut.ExecuteNonQuery();
             baglanti.Close();
             _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
