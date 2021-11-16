using Br.Com.LojaQueExplode.Business.DTOs;
using System.Threading.Tasks;

namespace Br.Com.LojaQueExplode.Business.Services.Abstract
{
    public interface IPasswordRecoveryService
    {
        Task Execute(DTOPasswordRecovery dto);
    }
}
