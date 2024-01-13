using EntityLayer.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class UserRegisterViewModelValidator : AbstractValidator<UserRegisterViewModel>
    {
        public UserRegisterViewModelValidator()
        {
            RuleFor(x => x.NameSurname).NotEmpty().WithMessage("Lütfen adınızı ve soyadınızı giriniz!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Lütfen şifrenizi giriniz!")
                .MinimumLength(6).WithMessage("Şifrenizi 6 karakterden fazla giriniz!")
                .Matches(@"[a-z]+").WithMessage("Şifrede en az bir küçük harf olmalıdır.")
                //.Matches(@"[A-Z]+").WithMessage("Şifrede en az bir büyük harf olmalıdır.")
                .Matches(@"[0-9]+").WithMessage("Şifrede en az bir rakam olmalıdır");

            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).WithMessage("Şifreler birbirleriyle uyuşmuyor..");

            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Lütfen şifrenizi tekrar giriniz!");
                //.MinimumLength(6).WithMessage("Şifre tekrarını 6 karakterden fazla giriniz!");

            RuleFor(x => x.Mail).NotEmpty().WithMessage("Lütfen E-postanızı giriniz!");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Lütfen kullanıcı adınızı giriniz!");

        }
    }
}
