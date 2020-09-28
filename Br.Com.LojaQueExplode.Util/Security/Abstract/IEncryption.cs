using System;
using System.Collections.Generic;
using System.Text;

namespace Br.Com.LojaQueExplode.Util.Security.Abstract
{
    public interface IEncryption
    {
        string GenerateCryptgraphy(string password);
        bool CheckPassword(string password, string encryptedPassword);
    }
}
