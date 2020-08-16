using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Mesajlar")]
    public class Mesajlar
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Gönderen Ad")]
        [Required(ErrorMessage = "Lütfen Boş Bırakmayınız!"), StringLength(30, ErrorMessage = "Ad Alanı Max 30 Karekter Olabilir!")]
        public string Ad { get; set; }

        [DisplayName("Gönderen Soyad")]
        [Required(ErrorMessage = "Lütfen Boş Bırakmayınız!"), StringLength(30, ErrorMessage = "Soyad Alanı Max 30 Karekter Olabilir!")]
        public string Soyad { get; set; }

        [DisplayName("Gönderen Mail")]
        [Required(ErrorMessage = "Lütfen Boş Bırakmayınız!"), StringLength(150, ErrorMessage = "Mail Alanı Max 150 Karekter Olabilir!")]
        public string Mail { get; set; }

        [DisplayName("Mesaj Konu")]
        [Required(ErrorMessage = "Lütfen Boş Bırakmayınız!"), StringLength(50, ErrorMessage = "Konu Alanı Max 50 Karekter Olabilir!")]
        public string Konu { get; set; }

        [DisplayName("Mesaj")]
        [Required(ErrorMessage = "Lütfen Boş Bırakmayınız!")]
        public string Mesaj { get; set; }

        public Boolean Durum { get; set; }
    }
}