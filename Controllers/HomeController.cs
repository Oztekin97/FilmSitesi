using FilmSitesi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FilmSitesi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MvcMovieContext _db;
        public HomeController(ILogger<HomeController> logger, MvcMovieContext db)
        {
            _logger = logger;
            _db = db;
        }
        // GET: Movies
        [Authorize]
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
        public IActionResult Edit(int id)
        {
            var movie = _db.Movie.Find(id);
            return View("Edit", movie);
        }
        [HttpPost]
        public IActionResult Edit(Movie movie)
        {
            var guncelUrun = _db.Movie.Find(movie.Id);
            guncelUrun.Title = movie.Title;
            guncelUrun.ReleaseDate = movie.ReleaseDate;
            guncelUrun.Genre = movie.Genre;
            guncelUrun.Price = movie.Price;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EditKonu(int id)
        {
            var movie = _db.Konular.Find(id);
            return View("EditKonu", movie);
        }
        [HttpPost]
        public IActionResult EditKonu(Konu movie)
        {
            var guncelUrun = _db.Konular.Find(movie.Id);
            guncelUrun.FilmKonusu = movie.FilmKonusu;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            _db.Movie.Add(movie);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Movies/Details/5
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
        // GET: Movies/Delete/5
        public IActionResult Delete(int id)
        {
            var film = _db.Movie.Find(id);
            _db.Movie.Remove(film);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Izleyiciler(int? id)
        {
            IQueryable<Rezervasyon> rezerv = _db.Rezervasyonlar;
            var filtreleme = rezerv.Where(a => a.MovieId == id).ToList();
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
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
