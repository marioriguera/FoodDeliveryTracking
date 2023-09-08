namespace FoodDeliveryTracking.Services.Encrypt
{
    /// <summary>
    /// Defines methods for encrypting and decrypting data using the AES (Advanced Encryption Standard) algorithm.
    /// </summary>
    public interface IEncryptService
    {
        /// <summary>
        /// Encrypts the provided text using the AES algorithm.
        /// </summary>
        /// <param name="textToEncrypt">The text to be encrypted.</param>
        /// <returns>The encrypted text.</returns>
        string EncryptAES(string textToEncrypt);

        /// <summary>
        /// Decrypts the provided encrypted text using the AES algorithm.
        /// </summary>
        /// <param name="textToDecrypt">The encrypted text to be decrypted.</param>
        /// <returns>The decrypted text.</returns>
        string DecryptAES(string textToDecrypt);
    }

}
