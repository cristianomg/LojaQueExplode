using AutoMapper;
using Br.Com.LojaQueExplode.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Br.Com.LojaQueExplode.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IMapper _mapper;
        protected readonly string ErroMessage = "Ops... Ocorreu um erro, tente novamente.";

        public BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
