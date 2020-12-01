using Br.Com.LojaQueExplode.Domain.Entities;
using Br.Com.LojaQueExplode.Infra.Repositories.Abstract;

namespace Br.Com.LojaQueExplode.Infra.Repositories.Abstract
{
    public interface IPurchaseStatusRepository : IBaseRepository<PurchaseStatus>
    {
        PurchaseStatus GetByName(string name);
    }
}
