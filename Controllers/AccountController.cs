using FilmSitesi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FilmSitesi.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly MvcMovieContext _db;
        public AccountController(ILogger<AccountController> logger, MvcMovieContext db)
        {
            _logger = logger;
            _db = db;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Account p)
        {
            var datavalue = _db.Accounts.FirstOrDefault(x => x.EMail == p.EMail && x.password == p.password && x.yetki==p.yetki);
            if (datavalue != null)
            {
                if (p.yetki.Equals("Personel"))
                {
                    var claims = new List<Claim>{
                       new Claim(ClaimTypes.Name, p.EMail)
        };
                    var userIdentity = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);
                    //Just redirect to our index after logging in. 
                    return RedirectToAction("Index", "Home");
                }
               if (p.yetki.Equals("Musteri")){
                    var claims = new List<Claim>{
                       new Claim(ClaimTypes.Name, p.EMail)
        };
                    var userIdentity = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);
                    //Just redirect to our index after logging in. 
                    return RedirectToAction("Index", "Uye");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Account account)
        {
            _db.Accounts.Add(account);
            SqlConnection baglanti1 = new SqlConnection(@"Data Source=LAPTOP-VKU0E66J\SQLEXPRESS;Initial Catalog=MOVIE;Integrated Security=True");
             SqlCommand komut = new SqlCommand("INSERT INTO Musteriler(TC,AdSoyad,EMail) VALUES(@TC,@AdSoyad,@EMail)", baglanti1);
             komut.Parameters.AddWithValue("@TC",account.TC);
             komut.Parameters.AddWithValue("@AdSoyad",account.AdSoyad);
           komut.Parameters.AddWithValue("@EMail",account.EMail);
             baglanti1.Open();
             komut.ExecuteNonQuery();
             baglanti1.Close();
            _db.SaveChanges();
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
