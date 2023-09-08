using FoodDeliveryTracking.Services.Logger;
using System.Security.Cryptography;
using System.Text;

namespace FoodDeliveryTracking.Services.Encrypt.Implementations
{
    /// <summary>
    /// Provides encryption and decryption services using the AES (Advanced Encryption Standard) algorithm.
    /// </summary>
    public class EncryptService : IEncryptService
    {
        private ILoggerManager _loggerManager;

        /// <summary>
        /// Initialize a new instance of <see cref="EncryptService"/> class.
        /// </summary>
        /// <param name="loggerManager"></param>
        public EncryptService(ILoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
        }

        /// <inheritdoc/>
        public string DecryptAES(string passwordToDescypt)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(Program.AppSettings.SecretKey);
                aesAlg.IV = Encoding.UTF8.GetBytes(Program.AppSettings.InitialVector);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(passwordToDescypt)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        /// <inheritdoc/>
        public string EncryptAES(string passwordToEncrypt)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(Program.AppSettings.SecretKey);
                aesAlg.IV = Encoding.UTF8.GetBytes(Program.AppSettings.InitialVector);

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(passwordToEncrypt);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
    }
}
