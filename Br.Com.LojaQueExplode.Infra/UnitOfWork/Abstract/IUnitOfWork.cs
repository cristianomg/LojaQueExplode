using System;
using System.Collections.Generic;
using System.Text;

namespace Br.Com.LojaQueExplode.Infra.UnitOfWork.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        int Save();
    }
}
