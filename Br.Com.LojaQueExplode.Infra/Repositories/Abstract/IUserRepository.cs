using Br.Com.LojaQueExplode.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Br.Com.LojaQueExplode.Infra.Repositories.Abstract
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetByEmail(string email, List<string> includes = null);
    }
}
