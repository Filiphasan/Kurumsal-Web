using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Iletisim")]
    public class Iletisim
    {
        [Key]
        public int Id { get; set; }
        [Required,StringLength(500,ErrorMessage ="Adres Alanı Max 500 Karekter Olmalıdır!")]
        public string Adres { get; set; }
        [Required,StringLength(50,ErrorMessage ="Telefon Alanı Max 50 Karekter Olmalıdır!")]
        public string Tel { get; set; }
        [Required,StringLength(150,ErrorMessage ="Mail Alanı Max 150 Karekter olmalıdır!")]
        public string Mail { get; set; }
        public string Fax { get; set; }
        public string Whatsapp { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }

    }
}