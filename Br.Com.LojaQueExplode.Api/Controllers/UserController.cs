using AutoMapper;
using Br.Com.LojaQueExplode.Api.Models;
using Br.Com.LojaQueExplode.Api.Validations.User;
using Br.Com.LojaQueExplode.Business.DTOs;
using Br.Com.LojaQueExplode.Business.Exceptions;
using Br.Com.LojaQueExplode.Business.Services.Abstract;
using Br.Com.LojaQueExplode.Infra.Repositories.Abstract;
using Br.Com.LojaQueExplode.Infra.UnitOfWork.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Br.Com.LojaQueExplode.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ICreateUserService _createUserService;
        public UserController(IUnitOfWork unitOfWork, IUserRepository userRepository,
            ICreateUserService createUserService, IMapper mapper) : base(mapper)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _createUserService = createUserService;
        }

        [HttpPost(Name = "Criar Usuário")]
        [AllowAnonymous]
        public IActionResult CreateUser([FromBody] DTOCreateUser body)
        {
            try
            {
                var validacao = new CreateUserValidation();
                var resultadoValidacao = validacao.Validate(body);
                if (!resultadoValidacao.IsValid)
                    return BadRequest(resultadoValidacao.Errors);

                try
                {
                    var novoUsuario = _createUserService.Execute(body);

                    if (novoUsuario != null)
                    {
                        var dto = _mapper.Map<DTOUser>(novoUsuario);
                        return Created($"{ControllerContext.HttpContext.Request.Path.Value}", dto);

                    }

                    return BadRequest("Não foi possivel realizar o cadastro tente novamente.");
                }
                catch(ValidationOnServiceException ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ErroMessage);
            }
        }
    }
}
