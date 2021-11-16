using Br.Com.LojaQueExplode.Business.DTOs;
using Br.Com.LojaQueExplode.Business.Services.Abstract;
using Br.Com.LojaQueExplode.Infra.Repositories.Abstract;
using Br.Com.LojaQueExplode.Infra.UnitOfWork.Abstract;
using Br.Com.LojaQueExplode.Util.Security.Abstract;
using Br.Com.LojaQueExplode.Util.Smtp.Abstract;
using System;
using System.Threading.Tasks;

namespace Br.Com.LojaQueExplode.Business.Services.Concrete
{
    public class PasswordRecoveryService : IPasswordRecoveryService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISendMailService _sendMailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEncryption _encryption;
        public PasswordRecoveryService(IUserRepository userRepository,
                                       ISendMailService sendMailService,
                                       IUnitOfWork unitOfWork,
                                       IEncryption encryption)
        {
            _userRepository = userRepository;
            _sendMailService = sendMailService;
            _unitOfWork = unitOfWork;
            _encryption = encryption;
        }
        public async Task Execute(DTOPasswordRecovery dto)
        {
            if (dto is null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var user = _userRepository.GetByEmail(dto.Email);

            if (user == null)
                throw new ApplicationException("Usuário não encontrado.");

            var newPassword = Guid.NewGuid().ToString().Split('-')[0];
            var encryptedPassword = _encryption.GenerateCryptgraphy(newPassword);

            user.Password = encryptedPassword;

            var template = $@"
                   <p> Olá <b>{user.Name.ToUpper()}</b>     </p>
                   <p>Recebemos uma solicitação para redefinir sua senha do MyBuy.</p>
                   <p>Aqui está sua nova senha: <b>{newPassword}</b></p>
                   <p>Recomendamos que você troque essa senha pois ela é provisória.</p>
             

                   <p>Atenciosamente, equipe MyBuy.</p>";

            try
            {
                await _sendMailService.SendEmail(user.Email, template, "Envio de senha provisória", isHtml: true);

                _userRepository.Update(user);

                _unitOfWork.Save();
            }
            catch(Exception e)
            {
                throw new ApplicationException("Erro ao gerar nova senha.");
            }
        }
    }
}
