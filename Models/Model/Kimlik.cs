using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Kimlik")]
    public class Kimlik
    {
        [Key]
        public int Id { get; set; }
       
        [Required(ErrorMessage ="Site Başlık Alanı Boş Bırakılamaz!"), StringLength(150, ErrorMessage = "Site Başlık Alnı Max 150 Karekter Olabilir!")]
        [DisplayName("Site Başlık")]
        public string Title { get; set; }
      
        [Required(ErrorMessage = "Anahtar Kelimeler Alanı Boş Bırakılamaz!"), StringLength(250, ErrorMessage = "Anahtar Kelimelerin Uzunluğu Max 250 Karekter Olabilir!")]
        [DisplayName("Anahtar Kelimeler")]
        public string Keywords { get; set; }

        [DisplayName("Site Açıklaması")]
        [Required(ErrorMessage = "Site Açıklama Alanı Boş Bırakılamaz!"), StringLength(300, ErrorMessage = "Description Alanı Max 300 Karekter Olabilir!")]
        public string Description { get; set; }

        [DisplayName("Site Logo")]
        public string Logo { get; set; }

        [DisplayName("Site Ünvan")]
        public string Unvan { get; set; }
    }
    
}