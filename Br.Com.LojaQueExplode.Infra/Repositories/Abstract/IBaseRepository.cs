using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Br.Com.LojaQueExplode.Infra.Repositories.Abstract
{
    public interface IBaseRepository<T>
    {
        T GetById(Guid id);
        IQueryable<T> GetAll();
        T Insert(T obj);
        void Delete(T obj);
        T Update(T obj);
        IQueryable<T> GetAllWithInclude(params Expression<Func<T, object>>[] includes);
    }
}
