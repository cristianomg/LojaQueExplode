using Br.Com.LojaQueExplode.Business.DTOs;
using Br.Com.LojaQueExplode.Domain.Entities;

namespace Br.Com.LojaQueExplode.Business.Services.Abstract
{
    public interface IRemoveProductOnShoppingCartService
    {
        ShoppingCart Execute(DTOInsertProductOnShoppingCart model);
    }
}
