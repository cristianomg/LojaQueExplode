using Br.Com.LojaQueExplode.Business.Exceptions;
using Br.Com.LojaQueExplode.Business.Services.Abstract;
using Br.Com.LojaQueExplode.Domain.Entities;
using Br.Com.LojaQueExplode.Domain.Enums;
using Br.Com.LojaQueExplode.Infra.Repositories.Abstract;
using Br.Com.LojaQueExplode.Infra.UnitOfWork.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Br.Com.LojaQueExplode.Business.Services.Concrete
{
    public class GetOrCreateShoppingCartService : IGetOrCreateShoppingCartOpenedService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPurchaseStatusRepository _purchaseStatusRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;


        public GetOrCreateShoppingCartService(
            IShoppingCartRepository shoppingCartRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IPurchaseStatusRepository purchaseStatusRepository,
            IProductRepository productRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _purchaseStatusRepository = purchaseStatusRepository;
            _productRepository = productRepository;
        }
        public ShoppingCart Execute(Guid userId)
        {
            var user = _userRepository.GetById(userId);

            if (user == null)
                throw new ValidationOnServiceException("Usuário não encontrado.");

            var shoppingCartOpen = _shoppingCartRepository
                .GetAllWithInclude(new List<string> { nameof(ShoppingCart.User), nameof(ShoppingCart.ProductShoppingCarts) })
                .FirstOrDefault(x => x.PurchaseStatus.Name == nameof(PurchaseStatusEnum.Open) && x.UserId == user.Id);

            if (shoppingCartOpen != null)
            {
                foreach (var productShoppingCart in shoppingCartOpen.ProductShoppingCarts)
                {
                    var product = _productRepository.GetAllWithInclude(new List<string> { nameof(Product.Photos) }).First(x => x.Id == productShoppingCart.ProductId);
                    productShoppingCart.Product = product;
                }
                return shoppingCartOpen;
            }
            else
            {
                var statusShoppingCart = _purchaseStatusRepository.GetByName(nameof(PurchaseStatusEnum.Open));
                var newShoppingCartOpen = _shoppingCartRepository.Insert(new ShoppingCart
                {
                    UserId = user.Id,
                    PurchaseStatusId = statusShoppingCart.Id
                });

                _unitOfWork.Save();
                return newShoppingCartOpen;
            }
        }
    }
}
