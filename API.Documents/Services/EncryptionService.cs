using System.Security.Cryptography;
using System.Text;

namespace API.Documents.Services
{
    public class EncryptionService
    {
        public static string Key { private get; set; }

        public static string EncryptString(string value)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Get128BitString(Key));
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using MemoryStream memoryStream = new();
                using CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write);
                using (StreamWriter streamWriter = new(cryptoStream))
                {
                    streamWriter.Write(value);
                }

                array = memoryStream.ToArray();
            }

            return Convert.ToBase64String(array);
        }

        public static string DecryptString(string encryptedValue)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(encryptedValue);

            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(Get128BitString(Key));
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using MemoryStream memoryStream = new(buffer);
            using CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read);
            using StreamReader streamReader = new(cryptoStream);
            return streamReader.ReadToEnd();
        }

        public static string Get128BitString(string keyToConvert)
        {
            StringBuilder b = new();
            for (int i = 0; i < 16; i++)
                b.Append(keyToConvert[i % keyToConvert.Length]);
            return b.ToString();
        }
    }
}
