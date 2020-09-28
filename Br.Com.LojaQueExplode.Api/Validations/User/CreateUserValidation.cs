using Br.Com.LojaQueExplode.Business.DTOs;
using FluentValidation;

namespace Br.Com.LojaQueExplode.Api.Validations.User
{
    public class CreateUserValidation : AbstractValidator<DTOCreateUser>
    {
        public CreateUserValidation()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("Email não pode ser nulo ou vazio.");
            RuleFor(x => x.Name).NotEmpty().NotEmpty().WithMessage("Nome não pode ser nulo ou vazio");
            RuleFor(x => x.Phone).NotEmpty().NotEmpty().WithMessage("Telefone não pode ser nulo ou vazio");
            RuleFor(x => x.CheckedPassword).Equal(x => x.Password).WithMessage("A confirmação da senha não confere com a senha.");
        }
    }
}
