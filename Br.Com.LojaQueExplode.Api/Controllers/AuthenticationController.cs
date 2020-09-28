using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Br.Com.LojaQueExplode.Api.Models;
using Br.Com.LojaQueExplode.Api.Validations.Auth;
using Br.Com.LojaQueExplode.Business.DTOs;
using Br.Com.LojaQueExplode.Business.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Br.Com.LojaQueExplode.Api.Controllers
{
    [Route("api/v1/Auth")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : BaseController
    {
        private readonly IUserAuthenticationService _authService;
        public AuthenticationController(IMapper mapper, IUserAuthenticationService authService) : base(mapper)
        {
            _authService = authService;
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
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ErroMessage);

            }
        }
    }
}
