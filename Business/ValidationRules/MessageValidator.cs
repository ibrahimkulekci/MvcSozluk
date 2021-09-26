using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(x => x.ReceiverMail).NotEmpty().WithMessage("Alıcı alanını boş geçemezsiniz.");
            RuleFor(x => x.ReceiverMail).EmailAddress().WithMessage("Alıcı alanına E-mail olmalıdır.");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu alanını boş geçemezsiniz.");
            RuleFor(x => x.Subject).MaximumLength(50).WithMessage("Konu alanı en fazla 50 karakter olabilir.");
            RuleFor(x => x.MessageContent).NotEmpty().WithMessage("Mesaj alanını boş geçemezsiniz.");
        }
    }
}
