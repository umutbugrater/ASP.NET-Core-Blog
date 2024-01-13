using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Kategori Adı Boş Geçilemez"),MinLength(3,ErrorMessage = "3 harften az kategori adı olamaz")]
        public string? CategoryName { get; set; }

        public List<Blog>? Blogs { get; set; }

    }
}
