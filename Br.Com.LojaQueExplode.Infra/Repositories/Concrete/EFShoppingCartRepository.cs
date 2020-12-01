using Br.Com.LojaQueExplode.Domain.Entities;
using Br.Com.LojaQueExplode.Infra.Context;
using Br.Com.LojaQueExplode.Infra.Repositories.Abstract;

namespace Br.Com.LojaQueExplode.Infra.Repositories.Concrete
{
    public class EFShoppingCartRepository : EFBaseRepository<ShoppingCart>, IShoppingCartRepository
    {
        public EFShoppingCartRepository(LojaQueExplodeContext context) : base(context)
        {

        }
    }
}
