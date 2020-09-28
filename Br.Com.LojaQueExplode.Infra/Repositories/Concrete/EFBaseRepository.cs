using Br.Com.LojaQueExplode.Domain.Entities;
using Br.Com.LojaQueExplode.Infra.Context;
using Br.Com.LojaQueExplode.Infra.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Br.Com.LojaQueExplode.Infra.Repositories.Concrete
{
    public class EFBaseRepository<T> : IBaseRepository<T>
        where T : Entity
        {
            
            private readonly DbSet<T> _dbSet;
            public EFBaseRepository(LojaQueExplodeContext context)
            {
                _dbSet = context.Set<T>();
            }
            public T Update(T obj)
            {
                return _dbSet.Update(obj).Entity;
            }

            public void Delete(T obj)
            {
                _dbSet.Remove(obj);
            }
            public T Insert(T obj)
            {
                return _dbSet.Add(obj).Entity;
            }

            public T GetById(Guid id)
            {
                return _dbSet.Find(id);
            }

            public IQueryable<T> GetAll()
            {
                return _dbSet.AsQueryable();
            }

            public IQueryable<T> GetAllWithInclude(List<string> includes)
            {
                var result = _dbSet.AsQueryable();

                foreach (var include in includes)
                {
                    result = result.Include(include);
                }
                return result;
            }
        }
}
