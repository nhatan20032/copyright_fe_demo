using System;
using System.IO;
using System.Security.Cryptography;


namespace copyrights_fe.Helpers
{
    public static class Aeskey
    {
        public static void MyIntegrationTest()
        {
            Console.WriteLine("Enter text that needs to be encrypted..");
            AesManaged aes = new AesManaged();
            aes.GenerateKey();
            aes.GenerateIV();
            Console.WriteLine(aes.Key);


            string data = Console.ReadLine();
            EncryptAesManaged(data);
            Console.ReadLine();
        }

        private static void EncryptAesManaged(string data)
        {
            try
            {
                using (AesManaged aes = new AesManaged())
                {
                    byte[] encrypted = Encrypt(data, aes.Key, aes.IV);
                    //print 
                    Console.WriteLine($"Encrypted data: {System.Text.Encoding.UTF8.GetString(encrypted)}");
                    string decrypted = Decrypt(encrypted, aes.Key, aes.IV);
                    // Print decrypted string. It should be same as raw data    
                    Console.WriteLine($"Decrypted data: {decrypted}");
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            Console.ReadKey();
        }

        private static string Decrypt(byte[] cipherText, byte[] rgbKey, byte[] rgbIV)
        {
            string plaintext = null;
            // Create AesManaged    
            using (AesManaged aes = new AesManaged())
            {
                // Create a decryptor    
                ICryptoTransform decryptor = aes.CreateDecryptor(rgbKey, rgbIV);
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

        private static byte[] Encrypt(string plainText, byte[] rgbIV, byte[] rgbKey)
        {
            byte[] encrypted;
            using (AesManaged aes = new AesManaged())
            {
                // Create encryptor    
                ICryptoTransform encryptor = aes.CreateEncryptor(rgbKey, rgbIV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream    
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            return encrypted;
        }
    }
}
