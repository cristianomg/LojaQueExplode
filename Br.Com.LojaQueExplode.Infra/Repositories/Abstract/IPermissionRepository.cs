using Br.Com.LojaQueExplode.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Br.Com.LojaQueExplode.Infra.Repositories.Abstract
{
    public interface IPermissionRepository : IBaseRepository<Permission>
    {
        Permission GetByName(string name);
    }
}
