using AutoMapper;
using Br.Com.LojaQueExplode.Api.Models;
using Br.Com.LojaQueExplode.Api.Validations.Product;
using Br.Com.LojaQueExplode.Business.DTOs;
using Br.Com.LojaQueExplode.Business.Exceptions;
using Br.Com.LojaQueExplode.Business.Services.Abstract;
using Br.Com.LojaQueExplode.Domain.Enums;
using Br.Com.LojaQueExplode.Infra.Repositories.Abstract;
using Br.Com.LojaQueExplode.Infra.UnitOfWork.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

namespace Br.Com.LojaQueExplode.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly ICreateProductService _createProductService;
        private readonly ICategoryRepository _categoryRepository;
        public ProductController(IProductRepository productRepository,
            ICreateProductService createProductService,
            IUnitOfWork unitOfWork,
            ICategoryRepository categoryRepository,
            IMapper mapper) : base(mapper)
        {
            _productRepository = productRepository;
            _createProductService = createProductService;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
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
        [HttpPut()]
        [Authorize(Roles = nameof(PermissionsEnum.Administration))]
        public IActionResult Update([FromBody] DTOUpdateProduct dto)
        {
            var product = _productRepository.GetById(dto.Id);
            
            if (product is null)
                return BadRequest("Produto não encontrado.");

            var category = _categoryRepository.GetById(dto.CategoryId);

            if (category is null)
                return BadRequest("Categoria não encontrada.");
            try
            {
                product.Category = category;
                product.Name = dto.Name;
                product.Description = dto.Description;
                product.Price = dto.Price;
                product.Quantity = dto.Quantity;

                _productRepository.Update(product);

                _unitOfWork.Save();

                return Ok();
            }

            catch(Exception e)
            {
                return BadRequest("erro ao atualizar produto.");
            }
        }
        [HttpGet(Name = "Listar-Produtos")]
        [Authorize]
        public IActionResult ListProduct([FromQuery] string category)
        {
            try
            {
                var products = _productRepository.GetAllWithInclude(x => x.Category,
                                                                    x => x.Photos);

                if(!string.IsNullOrEmpty(category))
                    products = products.Where(x=>x.Category.Name == category);

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
