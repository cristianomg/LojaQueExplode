using AutoMapper;
using AutoMapper.QueryableExtensions;
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

namespace Br.Com.LojaQueExplode.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingCartController : BaseController
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IPurchaseStatusRepository _purchaseStatusRepository;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository,
            IPurchaseStatusRepository purchaseStatusRepository, IMapper mapper) : base(mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _purchaseStatusRepository = purchaseStatusRepository;
        }
        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetAll(Guid userId)
        {
            try
            {
                var purchaseStatus = _purchaseStatusRepository.GetByName(nameof(PurchaseStatusEnum.Open));
                var shoppingCart = _shoppingCartRepository
                    .GetAllWithInclude(new List<string> { nameof(PurchaseStatus) })
                    .Where(x => x.PurchaseStatus.Code > purchaseStatus.Code);
                return Ok(_mapper.ProjectTo<DTOShoppingCart>(shoppingCart));
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ErroMessage);

            }
        }
    }
}
