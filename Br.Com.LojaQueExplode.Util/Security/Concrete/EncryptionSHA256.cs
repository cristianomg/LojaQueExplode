using Br.Com.LojaQueExplode.Util.Security.Abstract;
using System.Security.Cryptography;
using System.Text;

namespace Br.Com.LojaQueExplode.Util.Security.Concrete
{
    public class EncryptionSHA256 : IEncryption
    {
        private readonly HashAlgorithm _algoritmo;
        public EncryptionSHA256()
        {
            _algoritmo = SHA256.Create();
        }

        public string GenerateCryptgraphy(string password)
        {
            var encodedValue = Encoding.UTF8.GetBytes(password);
            var EncryptedPassword = _algoritmo.ComputeHash(encodedValue);

            var sb = new StringBuilder();
            foreach (var caracter in EncryptedPassword)
            {
                sb.Append(caracter.ToString("X2"));
            }
            return sb.ToString();
        }

        public bool CheckPassword(string password, string encryptedPassword)
        {
            var encryptedCheckedPassword = _algoritmo.ComputeHash(Encoding.UTF8.GetBytes(password));

            var sb = new StringBuilder();
            foreach (var caractere in encryptedCheckedPassword)
            {
                sb.Append(caractere.ToString("X2"));
            }

            return sb.ToString() == encryptedPassword;
        }
    }
}
