using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmSitesi.Models
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext(DbContextOptions<MvcMovieContext> options): base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Rezervasyon> Rezervasyonlar { get; set; }
        public DbSet<Personel> Personeller { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Konu> Konular { get; set; }
    }
}
