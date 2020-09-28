using Br.Com.LojaQueExplode.Business.DTOs;
using Br.Com.LojaQueExplode.Business.Exceptions;
using Br.Com.LojaQueExplode.Business.Services.Abstract;
using Br.Com.LojaQueExplode.Domain.Entities;
using Br.Com.LojaQueExplode.Domain.Enums;
using Br.Com.LojaQueExplode.Infra.Repositories.Abstract;
using Br.Com.LojaQueExplode.Infra.UnitOfWork.Abstract;
using Br.Com.LojaQueExplode.Util.Security.Abstract;

namespace Br.Com.LojaQueExplode.Business.Services.Concrete
{
    public class CreateUserService : ICreateUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryption _encryption;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPermissionRepository _permissionRepository;
        public CreateUserService(IUnitOfWork unitOfWork, IUserRepository userRepository,
            IPermissionRepository permissionRepository, IEncryption encryption)
        {
            _encryption = encryption;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _permissionRepository = permissionRepository;
        }
        public User Execute(DTOCreateUser createUser)
        {
            if (string.IsNullOrEmpty(createUser.Name) || string.IsNullOrEmpty(createUser.Email) || string.IsNullOrEmpty(createUser.Password))
                throw new ValidationOnServiceException("Um ou mais campos estão invalidos.");

            if (createUser.Password != createUser.CheckedPassword)
                throw new ValidationOnServiceException("Confirmação de senha invalida.");

            var checkIfEmailExists =  _userRepository.GetByEmail(createUser.Email);

            if (checkIfEmailExists != null) 
                throw new ValidationOnServiceException("Email já está em uso.");

            var encryptedPassword = _encryption.GenerateCryptgraphy(createUser.Password);


            var permission = _permissionRepository.GetByName(nameof(PermissionsEnum.Common));

            var newUser = new User
            {
                Email = createUser.Email,
                Name = createUser.Name,
                Password = encryptedPassword,
                Phone = createUser.Phone,
                PermissionId = permission.Id
            };

            var createdUser = _userRepository.Insert(newUser);

            _unitOfWork.Save();

            return createdUser;
        }
    }
}
