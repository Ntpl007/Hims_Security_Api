using Hims_Security_API.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Hims_Security_API.Security
{
    public class Encryption
    {
        public EncryptedDataVo EncryptAesManaged(string raw)
        {
            try
            {
                EncryptedDataVo obj = new EncryptedDataVo();

                // Create Aes that generates a new key and initialization vector (IV).
                // Same key must be used in encryption and decryption
                using (AesManaged aes = new AesManaged())
                {
                    // Encrypt string
                    obj.Key = Convert.ToBase64String(aes.Key);
                    obj.IV = Convert.ToBase64String(aes.IV);
                    byte[] encrypted = Encrypt(raw, aes.Key, aes.IV);
                    //   byte[] pwd3 = Convert.FromBase64String(raw);
                    // Print encrypted string

                    obj.EncryptedPassword = Convert.ToBase64String(encrypted);
                    //   byte[] pwd2 = System.Text.Encoding.UTF8.GetBytes(pwd);
                    //if(encrypted==pwd2)
                    //{
                    //    var s = "gg";
                    //}
                    //  string decrypted = Decrypt(encrypted, aes.Key, aes.IV);

                    return obj;
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

        static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            // Create a new AesManaged.
            using (AesManaged aes = new AesManaged())
            {
                // Create encryptor

                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                // Create MemoryStream
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream
                    // to encrypt
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            // Return encrypted data
            return encrypted;
        }
    }
}
