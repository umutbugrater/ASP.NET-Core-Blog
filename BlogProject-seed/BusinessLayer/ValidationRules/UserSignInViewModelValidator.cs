using EntityLayer.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class UserSignInViewModelValidator : AbstractValidator<UserSignInViewModel>
    {
        public UserSignInViewModelValidator()
        {
            RuleFor(x => x.email).NotEmpty().WithMessage("Lütfen e-posta adresinizi giriniz!");
            RuleFor(x => x.password).NotEmpty().WithMessage("Lütfen şifrenizi giriniz!")
                .MinimumLength(6).WithMessage("Şifre 6 karakterden az olamaz!");
        }
    }
}
