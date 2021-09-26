using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class HeadingContentAddValidator: AbstractValidator<HeadingContentAdd>
    {
        public HeadingContentAddValidator()
        {
            RuleFor(x => x.HeadingName).NotEmpty().WithMessage("Başlık alanı boş geçemezsiniz.");
            RuleFor(x => x.HeadingName).MinimumLength(3).WithMessage("Başlık alanı en az 3 karakter olmalıdır.");

            RuleFor(x => x.ContentValue).NotEmpty().WithMessage("İçerik alanı boş geçemezsiniz.");
            RuleFor(x => x.ContentValue).MinimumLength(3).WithMessage("İçerik alanı en az 3 karakter olmalıdır.");
        }
    }
}
