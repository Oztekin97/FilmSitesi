using FilmSitesi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FilmSitesi.Controllers
{
    public class UyeController : Controller
    {
        private readonly MvcMovieContext _db;
        public UyeController( MvcMovieContext db)
        {
            _db = db;
        }
        // GET: Movies
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _db.Movie
                                            orderby m.Genre
                                            select m.Genre;

            var movies = from m in _db.Movie
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            var movieGenreVM = new MovieGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Movies = await movies.ToListAsync()
            };

            return View(movieGenreVM);
        }
        [HttpGet]
        public IActionResult RezervasyonAdd()
        {
            List<SelectListItem> values=(from x in _db.Movie.ToList()
                                         select new SelectListItem
                                         {
                                             Text=x.Title,
                                             Value=x.Id.ToString()
                                         }).ToList();
            ViewBag.v1 = values;
            return View();
        }
        [HttpPost]
        public IActionResult RezervasyonAdd(Rezervasyon rezervasyon)
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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _db.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }
        public async Task<IActionResult> Konu(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _db.Konular
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }
        public IActionResult ListOrCancel()
        {
            var name = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
            IQueryable<Rezervasyon> filmler = _db.Rezervasyonlar;
            var filtreleme = filmler.Where(a => a.EMail.Equals(name)).OrderByDescending(a => a.Id).ToList();
            return View(filtreleme);
        }
        public IActionResult Cancel(int id)
        {
            var bilet = _db.Rezervasyonlar.Find(id);
            _db.Rezervasyonlar.Remove(bilet);
            SqlConnection baglanti1 = new SqlConnection(@"Data Source=LAPTOP-VKU0E66J\SQLEXPRESS;Initial Catalog=MOVIE;Integrated Security=True");
            SqlCommand komut = new SqlCommand("UPDATE Movie SET Stok=Stok-1,Total=(Stok-1)*Price  WHERE  Id=@mid", baglanti1);
            komut.Parameters.AddWithValue("@mid", bilet.MovieId);
            baglanti1.Open();
            komut.ExecuteNonQuery();
            baglanti1.Close();
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Populer()
        {
            IQueryable<Movie> filmler = _db.Movie;
            var filtreleme = filmler.Where(a => a.Stok != 0).OrderByDescending(a=>a.Stok).ToList(); 
            return View(filtreleme);
        }
    }
}
