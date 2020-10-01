using Br.Com.LojaQueExplode.Business.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Br.Com.LojaQueExplode.Api.Validations.Product
{
    public class CreateProductValidation : AbstractValidator<DTOCreateProduct>
    {
        public CreateProductValidation()
        {

        }
    }
}
