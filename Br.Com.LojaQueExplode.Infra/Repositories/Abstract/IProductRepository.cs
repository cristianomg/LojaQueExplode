using Br.Com.LojaQueExplode.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Br.Com.LojaQueExplode.Infra.Repositories.Abstract
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Product GetByName(string name);
    }
}
