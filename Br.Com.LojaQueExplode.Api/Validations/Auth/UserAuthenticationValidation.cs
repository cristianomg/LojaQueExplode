using Br.Com.LojaQueExplode.Business.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Br.Com.LojaQueExplode.Api.Validations.Auth
{
    public class UserAuthenticationValidation : AbstractValidator<DTOUserCredentials>
    {
        public UserAuthenticationValidation()
        {
            RuleFor(x => x.Email).NotEmpty().NotEmpty().WithMessage("O Email não pode ser nulo ou vazio.");
            RuleFor(x => x.Password).NotEmpty().NotEmpty().WithMessage("A senha não pode ser nulo ou vazio.");

        }
    }
}
