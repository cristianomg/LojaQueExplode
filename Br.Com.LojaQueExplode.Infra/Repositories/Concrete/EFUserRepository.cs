using Br.Com.LojaQueExplode.Domain.Entities;
using Br.Com.LojaQueExplode.Infra.Context;
using Br.Com.LojaQueExplode.Infra.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Br.Com.LojaQueExplode.Infra.Repositories.Concrete
{
    public class EFUserRepository : EFBaseRepository<User>, IUserRepository
    {
        private readonly DbSet<User> _dbSet;
        public EFUserRepository(LojaQueExplodeContext context) : base(context)
        {
            _dbSet = context.Set<User>();
        }
        public User GetByEmail(string email, List<string> includes = null)
        {
            var users = _dbSet.AsQueryable();
            if (includes != null && includes.Any())
                foreach(var include in includes)
                {
                    users.Include(include);
                }
            return users.FirstOrDefault(x => x.Email == email);
        }
    }
}
