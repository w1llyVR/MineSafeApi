using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Cryptor
    {
        private readonly byte[] _salt = new byte[] { 0x26, 0x19, 0x81, 0x4E, 0x7F, 0x6D, 0x95, 0xFF, 0x26, 0x75, 0x64, 0x05, 0x26 };

        public byte[] GetEncodeBytes(string plainText)
        {
            return Encoding.UTF8.GetBytes(plainText);
        }

        public string GetDecodeBytes(byte[] encodeBytes)
        {
            return Encoding.UTF8.GetString(encodeBytes);
        }

        public string Encrypt(string plainText, byte[] publicKey)
        {
            using (var aes = new AesManaged { KeySize = 256, BlockSize = 128 })
            {
                var keyGenerator = new Rfc2898DeriveBytes(GetDecodeBytes(publicKey), _salt, 10000);
                aes.Key = keyGenerator.GetBytes(32);
                aes.IV = keyGenerator.GetBytes(16);

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] data = Encoding.UTF8.GetBytes(plainText);
                        cryptoStream.Write(data, 0, data.Length);
                        cryptoStream.FlushFinalBlock();

                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
        }

        public string Decrypt(string cipherText, byte[] publicKey)
        {
            using (var aes = new AesManaged { KeySize = 256, BlockSize = 128 })
            {
                var keyGenerator = new Rfc2898DeriveBytes(GetDecodeBytes(publicKey), _salt, 10000);
                aes.Key = keyGenerator.GetBytes(32);
                aes.IV = keyGenerator.GetBytes(16);

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        byte[] data = Convert.FromBase64String(cipherText);
                        cryptoStream.Write(data, 0, data.Length);
                        cryptoStream.FlushFinalBlock();

                        return Encoding.UTF8.GetString(memoryStream.ToArray()).Trim();
                    }
                }
            }
        }

    }
}
