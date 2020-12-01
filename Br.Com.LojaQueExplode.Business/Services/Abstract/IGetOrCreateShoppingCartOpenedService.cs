using Br.Com.LojaQueExplode.Domain.Entities;
using System;

namespace Br.Com.LojaQueExplode.Business.Services.Abstract
{
    public interface IGetOrCreateShoppingCartOpenedService
    {
        ShoppingCart Execute(Guid userId);
    }
}
