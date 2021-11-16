using AutoMapper;
using AutoMapper.QueryableExtensions;
using Br.Com.LojaQueExplode.Api.Extensions;
using Br.Com.LojaQueExplode.Api.Models;
using Br.Com.LojaQueExplode.Domain.Entities;
using Br.Com.LojaQueExplode.Domain.Enums;
using Br.Com.LojaQueExplode.Infra.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace Br.Com.LojaQueExplode.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingCartController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IPurchaseStatusRepository _purchaseStatusRepository;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository,
            IPurchaseStatusRepository purchaseStatusRepository, IMapper mapper,
            IProductRepository productRepository) : base(mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _purchaseStatusRepository = purchaseStatusRepository;
            _productRepository = productRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var userId = HttpContext.GetUserID();
                var purchaseStatus = _purchaseStatusRepository.GetByName(nameof(PurchaseStatusEnum.Open));
                var shoppingCarts = _shoppingCartRepository
                    .GetAllWithInclude(x=>x.PurchaseStatus,
                                       x => x.ProductShoppingCarts)
                    .Where(x => x.PurchaseStatus.Code > purchaseStatus.Code)
                    .ToList();
                foreach (var shoppingCart in shoppingCarts)
                {
                    foreach (var productShoppingCart in shoppingCart.ProductShoppingCarts)
                    {
                        var product = _productRepository.GetAllWithInclude(x => x.Photos)
                            .First(x => x.Id == productShoppingCart.ProductId);
                        productShoppingCart.Product = product;
                    }
                }
                return Ok(_mapper.Map<List<DTOShoppingCart>>(shoppingCarts));
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ErroMessage);

            }
        }
    }
}
