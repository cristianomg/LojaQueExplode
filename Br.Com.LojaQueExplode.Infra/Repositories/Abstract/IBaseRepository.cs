using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Br.Com.LojaQueExplode.Infra.Repositories.Abstract
{
    public interface IBaseRepository<T>
    {
        T GetById(Guid id);
        IQueryable<T> GetAll();
        T Insert(T obj);
        void Delete(T obj);
        T Update(T obj);
        IQueryable<T> GetAllWithInclude(List<string> includes);
    }
}
