using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Kategoriler")]
    public class Kategoriler
    {
        [Key]
        public int Id { get; set; }

        [Required,StringLength(50,ErrorMessage ="Kategori Adı Max 50 Karekter Olabilir!")]
        public string KategoriAd { get; set; }

        public string Aciklama { get; set; }

        public ICollection<Blog> Blogs { get; set; }
    }
}