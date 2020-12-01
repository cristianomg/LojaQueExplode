using Br.Com.LojaQueExplode.Domain.Entities;
using Br.Com.LojaQueExplode.Infra.Context;
using Br.Com.LojaQueExplode.Infra.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Br.Com.LojaQueExplode.Infra.Repositories.Concrete
{
    public class EFPurchaseStatusRepository : EFBaseRepository<PurchaseStatus>, IPurchaseStatusRepository
    {
        public EFPurchaseStatusRepository(LojaQueExplodeContext context) : base(context)
        {

        }

        public PurchaseStatus GetByName(string name)
        {
            return this.GetAll().FirstOrDefault(x => x.Name == name);
        }
    }
}
