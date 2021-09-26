using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class ContentValidator: AbstractValidator<Content>
    {
        public ContentValidator()
        {
            RuleFor(x => x.ContentValue).NotEmpty().WithMessage("Başlık alanı boş geçemezsiniz.");
            RuleFor(x => x.ContentValue).MinimumLength(3).WithMessage("Başlık alanı en az 3 karakter olmalıdır.");
        }
    }
}
