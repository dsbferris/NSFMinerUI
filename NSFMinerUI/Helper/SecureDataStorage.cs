using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace NSFMinerUI.Helper
{
    public class SecureDataStorage
    {
        private const string SecretPath = "secret";
        private const string delimiter = "123456789123456789123456789";

        private static Aes GetAes()
        {
            var aes = Aes.Create();
            aes.Key = Convert.FromBase64String("itsASecret1234!");
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            return aes;
        }

        /// <summary>
        /// Remember to dispose SecureString
        /// </summary>
        /// <returns></returns>
        public static Tuple<string, SecureString> GetUsernameAndPassword()
        {
            using FileStream fs = File.OpenRead(SecretPath);

            using Aes aes = GetAes();
            byte[] iv = new byte[aes.IV.Length];

            //Reads IV value from beginning of the file.
            fs.Read(iv, 0, iv.Length);
            //apply iv to aes object
            aes.IV = iv;

            //Create a CryptoStream, pass it the FileStream, and encrypt it with the Aes class.  
            using var cs = new CryptoStream(
                fs,
                aes.CreateEncryptor(),
                CryptoStreamMode.Read);

            using StreamReader sr = new StreamReader(cs);
            var up = sr.ReadToEnd();
            var split = up.Split(delimiter);
            string username = split[0];
            SecureString password = new SecureString();
            foreach(char c in split[1])
            {
                password.AppendChar(c);
            }
            return new Tuple<string, SecureString>(username, password);
        }

        public static void SetUsernamePassword(string Username, string Password)
        {

            if (!File.Exists(SecretPath)) File.Create(SecretPath);
            using FileStream fs = File.OpenWrite(SecretPath);
            using Aes aes = GetAes();
            aes.GenerateIV();
            //Stores IV at the beginning of the file.
            //This information will be used for decryption.
            fs.Write(aes.IV, 0, aes.IV.Length);

            //Create a CryptoStream, pass it the FileStream, and encrypt it with the Aes class.  
            using var cs = new CryptoStream(
                fs,
                aes.CreateEncryptor(),
                CryptoStreamMode.Write);

            using StreamWriter sr = new StreamWriter(cs);
            sr.Write(Username + delimiter + Password);
        }
    }
}
