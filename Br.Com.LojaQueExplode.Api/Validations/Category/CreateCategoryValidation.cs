using Br.Com.LojaQueExplode.Business.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Br.Com.LojaQueExplode.Api.Validations.Category
{
    public class CreateCategoryValidation : AbstractValidator<DTOCreateCategory>
    {
        public CreateCategoryValidation()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("O nome da categoria deve ser informado.");
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("A descrição deve ser informada");
        }
    }
}
