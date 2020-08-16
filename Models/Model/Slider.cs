using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Slider")]
    public class Slider
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Slider Başlık")]
        [StringLength(50,ErrorMessage ="Slider Başlık Alanı Max 50 Karekter Olabilir!")]
        public string Baslik { get; set; }

        [DisplayName("Slider Açıklama")]
        [StringLength(150,ErrorMessage ="Slider Açıklama Alanı Max 150 Karekter Olabilir!")]
        public string Aciklama { get; set; }

        [DisplayName("Slider Resim")]
        [StringLength(250)]
        public string ResimUrl { get; set; }
    }
}