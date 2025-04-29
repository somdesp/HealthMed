using System.Security.Cryptography;
using System.Text;

public class CryptoHelper
{
    // Tamanho da chave (em bits) - pode ser 128, 192 ou 256
    private static readonly int TamanhoChave = 256;
    // Tamanho do vetor de inicialização (IV) - AES geralmente usa 128 bits
    private static readonly int TamanhoIV = 128;

    // Método para criptografar a senha
    public static string CriptografarSenha(string senha, string chave)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.KeySize = TamanhoChave;
            aesAlg.BlockSize = TamanhoIV;
            aesAlg.Key = Encoding.UTF8.GetBytes(chave.PadRight(32, ' '));  // A chave deve ter 32 caracteres para 256 bits
            aesAlg.IV = new byte[16];  // Vetor de inicialização com 16 bytes (128 bits) - você pode gerar isso aleatoriamente também

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

    // Método para descriptografar a senha
    public static string DescriptografarSenha(string senhaCriptografada, string chave)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.KeySize = TamanhoChave;
            aesAlg.BlockSize = TamanhoIV;
            aesAlg.Key = Encoding.UTF8.GetBytes(chave.PadRight(32, ' '));  // A chave deve ter 32 caracteres para 256 bits
            aesAlg.IV = new byte[16];  // Deve ser o mesmo IV usado na criptografia

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(senhaCriptografada)))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }
    }
}
