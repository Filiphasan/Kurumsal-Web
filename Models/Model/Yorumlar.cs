using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Yorumlar")]
    public class Yorumlar
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Ad Soyad")]
        [Required,StringLength(60)]
        public string AdSoyad { get; set; }
        [DisplayName("E-Posta Adresi")]
        [Required]
        public string Eposta { get; set; }
        public string Yorum { get; set; }
        public int? BlogId { get; set; }
        public Blog Blog { get; set; }
        public Boolean Onay { get; set; }

    }
}