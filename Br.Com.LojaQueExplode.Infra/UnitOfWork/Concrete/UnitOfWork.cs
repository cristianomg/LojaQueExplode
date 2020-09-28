using Br.Com.LojaQueExplode.Infra.Context;
using Br.Com.LojaQueExplode.Infra.UnitOfWork.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Br.Com.LojaQueExplode.Infra.UnitOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LojaQueExplodeContext _context;
        public UnitOfWork(LojaQueExplodeContext context)
        {
            _context = context;
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
