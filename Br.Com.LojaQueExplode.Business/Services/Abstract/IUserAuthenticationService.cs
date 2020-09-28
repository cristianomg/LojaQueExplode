using Br.Com.LojaQueExplode.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Br.Com.LojaQueExplode.Business.Services.Abstract
{
    public interface IUserAuthenticationService
    {
        ResultAuthentication Execute(DTOUserCredentials userCredentials);

    }
}
