using System.ComponentModel.DataAnnotations;

namespace BlogProject.Areas.Admin.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Lütfen rol adı giriniz..")]
        public string name { get; set; }
    }
}
