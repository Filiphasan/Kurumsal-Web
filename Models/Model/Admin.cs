using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        [Required,StringLength(100,ErrorMessage ="Max Karekter uzunluğu 50 olmalıdır!")]
        public string Eposta { get; set; }
        [Required,StringLength(100,ErrorMessage ="Max Karekter uzunluğu 20 olmalıdır!")]
        public string Sifre  { get; set; }
        public string Yetki { get; set; }
    }
}