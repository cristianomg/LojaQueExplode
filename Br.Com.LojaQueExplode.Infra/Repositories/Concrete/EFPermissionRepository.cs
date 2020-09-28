using Br.Com.LojaQueExplode.Domain.Entities;
using Br.Com.LojaQueExplode.Infra.Context;
using Br.Com.LojaQueExplode.Infra.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Com.LojaQueExplode.Infra.Repositories.Concrete
{
    public class EFPermissionRepository : EFBaseRepository<Permission>, IPermissionRepository
    {
        private readonly DbSet<Permission> _dbSet;
        public EFPermissionRepository(LojaQueExplodeContext context) : base(context)
        {
            _dbSet = context.Set<Permission>();
        }
        public Permission GetByName(string name)
        {
            return _dbSet.FirstOrDefault(x => x.Name == name);
        }
    }
}
