using System;

namespace Br.Com.LojaQueExplode.Business.Exceptions
{
    public class ValidationOnServiceException : Exception
    {
        public ValidationOnServiceException(string message) : base(message)
        {

        }
    }
}
