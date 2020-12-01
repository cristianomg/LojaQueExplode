using AutoMapper;
using Br.Com.LojaQueExplode.Api.Models;
using Br.Com.LojaQueExplode.Api.Validations.Category;
using Br.Com.LojaQueExplode.Business.DTOs;
using Br.Com.LojaQueExplode.Business.Exceptions;
using Br.Com.LojaQueExplode.Business.Services.Abstract;
using Br.Com.LojaQueExplode.Domain.Enums;
using Br.Com.LojaQueExplode.Infra.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Br.Com.LojaQueExplode.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICreateCategoryService _createCategoryService;
        public CategoryController(ICategoryRepository categoryRepository,
            ICreateCategoryService createCategoryService, IMapper mapper) : base(mapper)
        {
            _categoryRepository = categoryRepository;
            _createCategoryService = createCategoryService;
        }

        [HttpPost(Name = "Criar-Categoria")]
        [Authorize(Roles = nameof(PermissionsEnum.Administration))]
        public IActionResult CreateCategory([FromBody] DTOCreateCategory body)
        {
            try
            {
                var validator = new CreateCategoryValidation();
                var rusultValidation = validator.Validate(body);
                if (!rusultValidation.IsValid)
                    return BadRequest(rusultValidation.Errors);

                try
                {
                    var newCategory = _createCategoryService.Execute(body);

                    if (newCategory != null)
                    {
                        var dto = _mapper.Map<DTOCategory>(newCategory);
                        return Created($"{ControllerContext.HttpContext.Request.Path.Value}", dto);

                    }

                    return BadRequest("Não foi possivel realizar o cadastro tente novamente.");
                }
                catch (ValidationOnServiceException ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ErroMessage);
            }
        }
        [HttpGet(Name = "Listar-Categoria")]
        [Authorize]
        public IActionResult ListCategory()
        {
            try
            {
                var categories = _categoryRepository.GetAll();

                if (categories != null && categories.Any())
                {
                    var dto = _mapper.Map<List<DTOCategory>>(categories);
                    return Ok(dto);
                }
                return NoContent();
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ErroMessage);
            }
        }
    }
}
