using AutoMapper;
using Br.Com.LojaQueExplode.Api.Models;
using Br.Com.LojaQueExplode.Api.Validations.Auth;
using Br.Com.LojaQueExplode.Business.DTOs;
using Br.Com.LojaQueExplode.Business.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Br.Com.LojaQueExplode.Api.Controllers
{
    [Route("api/v1/Auth")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : BaseController
    {
        private readonly IUserAuthenticationService _authService;
        private readonly IPasswordRecoveryService _passwordRecoveryService;
        public AuthenticationController(IMapper mapper,
                                        IUserAuthenticationService authService,
                                        IPasswordRecoveryService passwordRecoveryService)
                                    : base(mapper)
        {
            _authService = authService;
            _passwordRecoveryService = passwordRecoveryService;
        }
        [HttpPost(Name = "Obter-token-autenticacao")]
        [AllowAnonymous]
        public IActionResult GetAuthentication([FromBody] DTOUserCredentials body)
        {
            try
            {
                var validator = new UserAuthenticationValidation();
                var rusultValidation = validator.Validate(body);
                if (!rusultValidation.IsValid)
                    return BadRequest(rusultValidation.Errors);

                var resultAuthentication = _authService.Execute(body);
                if (resultAuthentication != null)
                {
                    var dto = _mapper.Map<DTOResultAuthentication>(resultAuthentication);
                    return Ok(dto);
                }
                return BadRequest("Email ou senha invalido tente novamente.");
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ErroMessage);

            }
        }
        [AllowAnonymous]
        [HttpPost("PasswordRecovery")]
        public async Task<IActionResult> PasswordRecovery([FromBody] DTOPasswordRecovery dto)
        {
            try
            {
                await _passwordRecoveryService.Execute(dto);

                return Ok();

            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
