using AutoMapper;
using Br.Com.LojaQueExplode.Api.Extensions;
using Br.Com.LojaQueExplode.Api.Models;
using Br.Com.LojaQueExplode.Business.DTOs;
using Br.Com.LojaQueExplode.Business.Exceptions;
using Br.Com.LojaQueExplode.Business.Services.Abstract;
using Br.Com.LojaQueExplode.Domain.Enums;
using Br.Com.LojaQueExplode.Infra.Repositories.Abstract;
using Br.Com.LojaQueExplode.Infra.UnitOfWork.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Br.Com.LojaQueExplode.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingCartOpendController : BaseController
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPurchaseStatusRepository _purchaseStatusRepository;
        private readonly IGetOrCreateShoppingCartOpenedService _getOrCreateShoppingCartService;
        private readonly IAddProductOnShoppingCartService _addProductOnShoppingCartService;
        private readonly IRemoveProductOnShoppingCartService _removeProductOnShoppingCartService;
        public ShoppingCartOpendController(IShoppingCartRepository shoppingCartRepository,
            IUserRepository userRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IPurchaseStatusRepository purchaseStatusRepository,
            IGetOrCreateShoppingCartOpenedService getOrCreateShoppingCartService,
            IAddProductOnShoppingCartService addProductOnShoppingCartService,
            IRemoveProductOnShoppingCartService removeProductOnShoppingCartService) : base(mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _purchaseStatusRepository = purchaseStatusRepository;
            _getOrCreateShoppingCartService = getOrCreateShoppingCartService;
            _addProductOnShoppingCartService = addProductOnShoppingCartService;
            _removeProductOnShoppingCartService = removeProductOnShoppingCartService;
        }
        [HttpGet]
        public IActionResult FindCartOpen()
        {
            var userId = HttpContext.GetUserID();
            try
            {
                var shoppingCartOpened = _getOrCreateShoppingCartService.Execute(userId);
                var dto = _mapper.Map<DTOShoppingCart>(shoppingCartOpened);

                dto.SubTotal = shoppingCartOpened.CalculateSubTotal();
                return Ok(dto);
            }
            catch (ValidationOnServiceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpPost]
        [Route("add-item")]
        public IActionResult AddItemOnCartOpen(DTOInsertProductOnShoppingCart model)
        {
            model.UserId = HttpContext.GetUserID();
            try
            {
                var shoppingcart = _addProductOnShoppingCartService.Execute(model);
                return Ok(_mapper.Map<DTOShoppingCart>(shoppingcart));
            }
            catch (ValidationOnServiceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("remove-item")]
        public IActionResult RemoveItemOnCartOpen(DTOInsertProductOnShoppingCart model)
        {
            model.UserId = HttpContext.GetUserID();
            try
            {
                try
                {
                    var shoppingcart = _removeProductOnShoppingCartService.Execute(model);
                    return Ok(_mapper.Map<DTOShoppingCart>(shoppingcart));
                }
                catch (ValidationOnServiceException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("close")]
        public IActionResult CloseShoppingCartOpened()
        {
            var userId = HttpContext.GetUserID();
            try
            {
                var shoppingcart = _getOrCreateShoppingCartService.Execute(userId);
                var purchaseStatus = _purchaseStatusRepository.GetByName(nameof(PurchaseStatusEnum.RequestedProducts));

                shoppingcart.PurchaseStatusId = purchaseStatus.Id;
                _shoppingCartRepository.Update(shoppingcart);
                var hasUpdate = _unitOfWork.Save();

                if (hasUpdate > 0)
                    return Ok();

                return StatusCode((int)HttpStatusCode.NotModified);
            }
            catch (ValidationOnServiceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
