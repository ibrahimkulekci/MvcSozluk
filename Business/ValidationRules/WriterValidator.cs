using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class WriterValidator: AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.WriterUsername).NotEmpty().WithMessage("Kullanıcı adını boş geçemezsiniz.");
            RuleFor(x => x.WriterMail).NotEmpty().WithMessage("E-mail alanını boş geçemezsiniz.");
            RuleFor(x => x.WriterPassword).NotEmpty().WithMessage("Şifre alanını boş geçemezsiniz.");
            RuleFor(x => x.WriterPassword).MinimumLength(6).WithMessage("Şifre alanını en az 6 karakter olabilir.");
            RuleFor(x => x.WriterUsername).MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakter olabilir.");
            RuleFor(x => x.WriterUsername).MaximumLength(50).WithMessage("Kullanıcı adı en fazla 50 karakter olabilir.");
            RuleFor(x => x.WriterAbout).MaximumLength(100).WithMessage("Hakkımda alanı en fazla 100 karakter olabilir.");
        }
    }
}
