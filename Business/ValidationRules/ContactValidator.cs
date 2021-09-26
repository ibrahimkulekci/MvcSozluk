using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class ContactValidator: AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(x => x.UserMail).NotEmpty().WithMessage("E-Mail alanını boş geçemezsiniz!");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Ad Soyad alanını boş geçemezsiniz!");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu alanını boş geçemezsiniz!");
            RuleFor(x => x.UserName).MaximumLength(50).WithMessage("En fazla 50 karakter girişi yapabilirsiniz!");
            RuleFor(x => x.Message).NotEmpty().WithMessage("Mesaj alanını boş geçemezsiniz!");
        }
    }
}
