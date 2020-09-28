using Br.Com.LojaQueExplode.Business.DTOs;
using Br.Com.LojaQueExplode.Domain.Entities;
using System.Threading.Tasks;

namespace Br.Com.LojaQueExplode.Business.Services.Abstract
{
    public interface ICreateUserService
    {
        User Execute(DTOCreateUser createUser);
    }
}
