using Br.Com.LojaQueExplode.Business.DTOs;
using Br.Com.LojaQueExplode.Business.Services.Abstract;
using Br.Com.LojaQueExplode.Domain.Configurations;
using Br.Com.LojaQueExplode.Domain.Entities;
using Br.Com.LojaQueExplode.Domain.Enums;
using Br.Com.LojaQueExplode.Infra.Repositories.Abstract;
using Br.Com.LojaQueExplode.Util.Security.Abstract;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Br.Com.LojaQueExplode.Business.Services.Concrete
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IEncryption _encryption;
        private readonly BaseConfigurations _config;
        public UserAuthenticationService(IUserRepository userRepository, IEncryption encryption,
            BaseConfigurations config, IPermissionRepository permissionRepository)
        {
            _userRepository = userRepository;
            _permissionRepository = permissionRepository;
            _encryption = encryption;
            _config = config;
        }

        public ResultAuthentication Execute(DTOUserCredentials userCredentials)
        {
            var existingUser = _userRepository.GetByEmail(userCredentials.Email,
                includes: new List<string> { nameof(User.Permission) });

            var permission = _permissionRepository.GetById(existingUser.PermissionId);

            if (existingUser != null)
            {
                var correctPassword  = _encryption.CheckPassword(userCredentials.Password, existingUser.Password);

                if (correctPassword)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_config.SecretKey);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {

                            new Claim(ClaimTypes.Name, existingUser.Name),
                            new Claim(ClaimTypes.Role, permission.Name),

                        }),
                        Expires = DateTime.UtcNow.AddHours(3),

                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                    return new ResultAuthentication { User = existingUser, Token = tokenHandler.WriteToken(token), Permission = existingUser.Permission.Name };
                }
            }
            return default;
        }
    }
}
