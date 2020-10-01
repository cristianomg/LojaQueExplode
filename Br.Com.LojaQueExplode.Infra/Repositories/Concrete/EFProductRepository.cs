using Br.Com.LojaQueExplode.Domain.Entities;
using Br.Com.LojaQueExplode.Infra.Context;
using Br.Com.LojaQueExplode.Infra.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Br.Com.LojaQueExplode.Infra.Repositories.Concrete
{
    public class EFProductRepository : EFBaseRepository<Product>, IProductRepository
    {
        private readonly DbSet<Product> _dbSet;
        public EFProductRepository(LojaQueExplodeContext context) : base(context)
        {
            _dbSet = context.Set<Product>();
        }

        public Product GetByName(string name)
        {
            return _dbSet.FirstOrDefault(x => x.Name == name);
        }
    }

}
