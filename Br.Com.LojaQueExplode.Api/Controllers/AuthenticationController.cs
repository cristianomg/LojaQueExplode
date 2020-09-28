using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Br.Com.LojaQueExplode.Api.Models;
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
                var resultadoAutenticicacao = _authService.Execute(body);
                if (resultadoAutenticicacao != null)
                {
                    var dto = _mapper.Map<DTOResultAuthentication>(resultadoAutenticicacao);
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
