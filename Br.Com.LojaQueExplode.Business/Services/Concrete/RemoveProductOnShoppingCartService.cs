using Br.Com.LojaQueExplode.Business.DTOs;
using Br.Com.LojaQueExplode.Business.Exceptions;
using Br.Com.LojaQueExplode.Business.Services.Abstract;
using Br.Com.LojaQueExplode.Domain.Entities;
using Br.Com.LojaQueExplode.Infra.Repositories.Abstract;
using Br.Com.LojaQueExplode.Infra.UnitOfWork.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Br.Com.LojaQueExplode.Business.Services.Concrete
{
    public class RemoveProductOnShoppingCartService : IRemoveProductOnShoppingCartService
    {
        private readonly IGetOrCreateShoppingCartOpenedService _getOrCreateShoppingService;
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RemoveProductOnShoppingCartService(
            IGetOrCreateShoppingCartOpenedService getOrCreateShoppingService,
            IProductRepository productRepository,
            IShoppingCartRepository shoppingCartRepository,
            IUnitOfWork unitOfWork)
        {
            _getOrCreateShoppingService = getOrCreateShoppingService;
            _productRepository = productRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _unitOfWork = unitOfWork;
        }
        public ShoppingCart Execute(DTOInsertProductOnShoppingCart model)
        {
            var shoppingCart = _getOrCreateShoppingService.Execute(model.UserId);

            var hasProductOnShoppingCart = shoppingCart.ProductShoppingCarts.FirstOrDefault(x => x.ProductId == model.ProductId);

            if (hasProductOnShoppingCart == null)
                throw new ValidationOnServiceException("O Produto não foi encotrado.");

            if (hasProductOnShoppingCart.Quantity == 1)
                shoppingCart.ProductShoppingCarts.Remove(hasProductOnShoppingCart);

            else
            {
                hasProductOnShoppingCart.Quantity -= model.Quantity;
            }

            var product = _productRepository.GetById(model.ProductId);

            product.Quantity += model.Quantity;

            _productRepository.Update(product);
            _shoppingCartRepository.Update(shoppingCart);

            _unitOfWork.Save();

            return shoppingCart;
        }
    }
}
