using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FilmSitesi.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Bu Alanı Boş Bırakmayınız!")]
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Required(ErrorMessage = "Bu Alanı Boş Bırakmayınız!")]
        public string Genre { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public int Total { get; set; }
        public int Stok { get; set; }
    }
}
