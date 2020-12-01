using Br.Com.LojaQueExplode.Business.DTOs;
using Br.Com.LojaQueExplode.Business.Exceptions;
using Br.Com.LojaQueExplode.Business.Services.Abstract;
using Br.Com.LojaQueExplode.Domain.Entities;
using Br.Com.LojaQueExplode.Infra.Repositories.Abstract;
using Br.Com.LojaQueExplode.Infra.UnitOfWork.Abstract;
using System.Linq;

namespace Br.Com.LojaQueExplode.Business.Services.Concrete
{
    public class AddProductOnShoppingCartService : IAddProductOnShoppingCartService
    {
        private readonly IGetOrCreateShoppingCartOpenedService _getOrCreateShoppingService;
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddProductOnShoppingCartService(
            IGetOrCreateShoppingCartOpenedService getOrCreateShoppingService,
            IProductRepository productRepository,
            IShoppingCartRepository shoppingCartRepository,
            IUnitOfWork unitOfWork
            )
        {
            _getOrCreateShoppingService = getOrCreateShoppingService;
            _productRepository = productRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _unitOfWork = unitOfWork;
        }
        public ShoppingCart Execute(DTOInsertProductOnShoppingCart item)
        {

            var shoppingCart = _getOrCreateShoppingService.Execute(item.UserId);

            var product = _productRepository.GetById(item.ProductId);

            if (product.Quantity < item.Quantity)
                throw new ValidationOnServiceException("O produto selecionado não possue essa quantidade no estoque.");

            product.Quantity -= item.Quantity;

            var hasProductOnShoppingCart = shoppingCart.ProductShoppingCarts.FirstOrDefault(x => x.ProductId == product.Id);

            if (hasProductOnShoppingCart != null)
                hasProductOnShoppingCart.Quantity += item.Quantity;

            else
            {
                shoppingCart.ProductShoppingCarts.Add(new ProductShoppingCart
                {
                    ProductId = product.Id,
                    ShoppingCartId = shoppingCart.Id,
                    Quantity = item.Quantity

                });
            }

            _productRepository.Update(product);
            _shoppingCartRepository.Update(shoppingCart);

            _unitOfWork.Save();

            return shoppingCart;
        }
    }
}
