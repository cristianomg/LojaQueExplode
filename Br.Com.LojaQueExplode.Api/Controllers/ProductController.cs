using AutoMapper;
using Br.Com.LojaQueExplode.Api.Models;
using Br.Com.LojaQueExplode.Api.Validations.Product;
using Br.Com.LojaQueExplode.Business.DTOs;
using Br.Com.LojaQueExplode.Business.Exceptions;
using Br.Com.LojaQueExplode.Business.Services.Abstract;
using Br.Com.LojaQueExplode.Domain.Entities;
using Br.Com.LojaQueExplode.Domain.Enums;
using Br.Com.LojaQueExplode.Infra.Repositories.Abstract;
using Br.Com.LojaQueExplode.Infra.UnitOfWork.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Br.Com.LojaQueExplode.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly ICreateProductService _createProductService;
        public ProductController(IProductRepository productRepository,
            ICreateProductService createProductService, IMapper mapper) : base(mapper)
        {
            _productRepository = productRepository;
            _createProductService = createProductService;
        }
        [HttpPost(Name = "Criar-Produto")]
        [Authorize(Roles = nameof(PermissionsEnum.Administration))]
        public IActionResult CreateProduct([FromBody] DTOCreateProduct body)
        {
            try
            {
                var validator = new CreateProductValidation();
                var rusultValidation = validator.Validate(body);
                if (!rusultValidation.IsValid)
                    return BadRequest(rusultValidation.Errors);

                try
                {
                    var newProduct = _createProductService.Execute(body);

                    if (newProduct != null)
                    {
                        var dto = _mapper.Map<DTOProduct>(newProduct);
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
        [HttpGet(Name = "Listar-Produtos")]
        [Authorize]
        public IActionResult ListProduct()
        {
            try
            {
                var products = _productRepository.GetAllWithInclude(new List<string> { nameof(Product.Photos) });

                if (products != null && products.Any())
                {
                    var dto = _mapper.ProjectTo<DTOProduct>(products);
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
