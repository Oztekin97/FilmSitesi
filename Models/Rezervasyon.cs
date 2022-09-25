using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FilmSitesi.Models
{
    public class Rezervasyon
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Bu Alanı Boş Bırakmayınız!")]
        [ForeignKey("Movie")]
        public int? MovieId { get; set; }
        public Movie movie { get; set; }
        [MaxLength(11)]
        public string TC { get; set; }
        [Required(ErrorMessage = "Bu Alanı Boş Bırakmayınız!")]
        public string AdSoyad { get; set; }
        public string EMail { get; set; }
    }
}
