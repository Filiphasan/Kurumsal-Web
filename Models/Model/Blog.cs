using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Blog")]
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public string ResimUrl { get; set; }
        public int? KategoriId { get; set; }
        public Kategoriler Kategori { get; set; }
        public ICollection<Yorumlar> Yorumlar { get; set; }
    }
}