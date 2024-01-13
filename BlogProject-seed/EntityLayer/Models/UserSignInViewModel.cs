using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Models
{
    public class UserSignInViewModel
    {
        public string? email { get; set; }

        public string? password { get; set; }
    }
}
