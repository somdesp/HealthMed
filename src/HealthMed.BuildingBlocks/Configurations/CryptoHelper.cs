using System.Security.Cryptography;
using System.Text;

namespace HealthMed.BuildingBlocks.Configurations
{
    public static class CryptoHelper
    {
        private static readonly int TamanhoChave = 256;
        private static readonly int TamanhoIV = 128;

        public static string CriptografarSenha(string senha, string chave)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.KeySize = TamanhoChave;
                aesAlg.BlockSize = TamanhoIV;
                aesAlg.Key = Encoding.UTF8.GetBytes(chave.PadRight(32, ' '));
                aesAlg.IV = new byte[16];

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(senha);
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
    }
}
