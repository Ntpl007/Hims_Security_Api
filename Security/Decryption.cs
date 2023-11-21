using Hims_Security_API.SecurityDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Hims_Security_API.Security
{
    public class Decryption
    {
        public string DecryptAesManaged(TblUser data)
        {
            try
            {
                // Create Aes that generates a new key and initialization vector (IV).
                // Same key must be used in encryption and decryption

                using (AesManaged aes = new AesManaged())
                {

                    byte[] pwd = Convert.FromBase64String(data.EncryptedPassword); // System.Text.Encoding.UTF8.GetBytes(raw);
                    string decrypted = Decrypt(Convert.FromBase64String(data.EncryptedPassword), Convert.FromBase64String(data.EncryptedKey), Convert.FromBase64String(data.EncryptedIv));
                    return decrypted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;
            // Create AesManaged
            using (AesManaged aes = new AesManaged())
            {
                // Create a decryptor

                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);

                // Create the streams used for decryption.
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    // Create crypto stream
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read crypto stream
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
    }
}
