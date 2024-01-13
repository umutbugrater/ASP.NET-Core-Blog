using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(x => x.MessageSubject).NotEmpty().WithMessage("Mesaj Konusu boş bırakılamaz !!");

            RuleFor(x => x.MessageDetails).NotEmpty().WithMessage("Mesaj içeriği boş bırakılamaz !");

            RuleFor(x => x.ReceiverID).NotEmpty().WithMessage("Alıcının e-postasını seçmeyi unutmayınız");
            //RuleFor(x=>x.MessageDetails).NotEmpty().WithMessage($"{nameof(MessageValidator)}");
        }
    }
}
