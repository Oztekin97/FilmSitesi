using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmSitesi.Models
{
    public class Konu
    {
        [Key]
        public int Id { get; set; }
        public int? MovieId { get; set; }
        [MaxLength(500)]
        public string FilmKonusu { get; set; }
    }
}
