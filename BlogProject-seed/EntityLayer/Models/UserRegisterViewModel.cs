using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Models
{
    public class UserRegisterViewModel
    {
        public string? NameSurname { get; set; }

        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }

        public string? Mail { get; set; }

        public string? UserName { get; set; }

        [Required(ErrorMessage = "Lütfen resminizi yükleyiniz..")]
        public string? UserImage { get; set; }
    }
}
