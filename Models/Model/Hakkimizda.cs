using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Hakkimizda")]
    public class Hakkimizda
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Hakkımızda Açıklama")]
        public string Aciklama { get; set; }

        [DisplayName("Hakkımızda Resim")]
        public string ResimUrl { get; set; }
    }
}