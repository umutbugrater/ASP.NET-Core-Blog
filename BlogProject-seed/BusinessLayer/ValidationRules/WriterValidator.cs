using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidator : AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar adı soyadı kısmı boş geçilemez")
                 .MinimumLength(2).WithMessage("Lütfen en az 2 karakter girişi yapın")
                 .MaximumLength(50).WithMessage("Lütfen en fazla 50 karakterlik veri girişi yapın");

            RuleFor(x => x.WriterMail).NotEmpty().WithMessage("Mail adresi boş geçilemez")
                .Matches("^((?!admin).)*$").WithMessage("Email Address should not contain admin") // admin kelimesini içermemeli
                .EmailAddress().WithMessage(" `@` ve `.` işaretlerini kullanmak zorundasınız."); // . ve @ işaretlerinini aynı anda kullanılmasına yarıyor

            RuleFor(x => x.WriterPassword).NotEmpty().WithMessage("Şifre Boş Geçilemez")
                .Length(8, 16).WithMessage("Şifreniz 8 ile 16 karakter arasında olmalıdır.")
                .Matches(@"[A-Z]+").WithMessage("Şifrede en az bir büyük harf olmalıdır.")
                .Matches(@"[a-z]+").WithMessage("Şifrede en az bir küçük harf olmalıdır.")
                .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.) ")
                //RuleFor(customer => customer.Password).Equal(customer => customer.PasswordConfirmation);       <input type="password" @asp-for="Model.Password">
                .Matches(@"[0-9]+").WithMessage("Şifrede en az bir rakam olmalıdır");
        }

    }
}
