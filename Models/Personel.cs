using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmSitesi.Models
{
    public class Personel
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(11)]
        public string TC { get; set; }
        [Required(ErrorMessage = "Bu Alanı Boş Bırakmayınız!")]
        public string AdSoyad { get; set; }
        [MaxLength(50)]
        public string EMail { get; set; }
    }
}
