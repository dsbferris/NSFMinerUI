using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NSFMinerUI
{
    public class CryptoLib
    {
        private const string KeyString = "FerrisIsAGenuis1234!";

        private static byte[] Key => Encoding.Unicode.GetBytes(KeyString);

        //aes settings
        private static readonly PaddingMode Pad = PaddingMode.PKCS7;
        private static readonly CipherMode Mode = CipherMode.CBC;


        /// <summary>
        /// Creates Aes with global settings. IV is not managed here.
        /// </summary>
        /// <returns>Aes with applied key, padding and mode</returns>
        private static Aes GetAes()
        {
            var aes = Aes.Create();
            aes.Key = Key;
            aes.Padding = Pad;
            aes.Mode = Mode;
            return aes;
        }

        
        /// <summary>
        /// Encrypts a given file and stores it at given place
        /// </summary>
        /// <param name="originalpath">Path of the original file</param>
        /// <param name="cryptopath">Path of the enrypted file. Any existing file will be overwritten.</param>
        public static void EncryptFile(string originalpath, string cryptopath)
        {
            using FileStream fs = new FileStream(cryptopath, FileMode.Create);

            using Aes aes = GetAes();
            aes.GenerateIV();
            var iv = aes.IV;
            //Stores IV at the beginning of the file.
            //This information will be used for decryption.
            fs.Write(iv, 0, iv.Length);

            //Create a CryptoStream, pass it the FileStream, and encrypt it with the Aes class.  
            using var cs = new CryptoStream(
                fs,
                aes.CreateEncryptor(),
                CryptoStreamMode.Write);

            //opens filestream from original file
            using var sourceStream = File.OpenRead(originalpath);

            //copys data from original to cryptostream
            sourceStream.CopyTo(cs);
        }


        /// <summary>
        /// Decrypts a given file and stores it at given place
        /// </summary>
        /// <param name="originalpath">Path of the crypto file</param>
        /// <param name="cryptopath">Path of the decrypted file. Any existing file will be overwritten.</param>
        public static void DecryptFile(string cryptofile, string targetfile)
        {
            using var fs = File.OpenRead(cryptofile);
            //using FileStream fs = new FileStream(cryptofile, FileMode.Open);

            using Aes aes = GetAes();
            var iv = new byte[aes.IV.Length];

            //Reads IV value from beginning of the file.
            fs.Read(iv, 0, iv.Length);
            //apply iv to aes object
            aes.IV = iv;

            //Create a CryptoStream, pass it the file stream, and decrypt it with the Aes class and its key and IV.
            using var cs = new CryptoStream(
                fs,
                aes.CreateDecryptor(),
                CryptoStreamMode.Read);

            using var ds = File.Create(targetfile);
            //using var ds = new FileStream(targetfile, FileMode.Create);
            //var destinationStream = File.Create(targetfile);
            //source.CopyTo(cs);
            cs.CopyTo(ds);
            // destinationStream.Close();
        }

        /// <summary>
        /// Decrypts a stream to memory
        /// </summary>
        /// <param name="fs">The stream to be decrypted</param>
        /// <returns>MemoryStream of decrypted file. REMEMBER TO DISPOSE THIS STREAM!</returns>
        public static MemoryStream DecryptFile(Stream fs)
        {
            using var aes = GetAes();
            var iv = new byte[aes.IV.Length];

            //Reads IV value from beginning of the file.
            fs.Read(iv, 0, iv.Length);
            //apply iv to aes object
            aes.IV = iv;

            //Create a CryptoStream, pass it the file stream, and decrypt it with the Aes class and its key and IV.
            using var cs = new CryptoStream(
                fs,
                aes.CreateDecryptor(),
                CryptoStreamMode.Read);
            var ms = new MemoryStream();
            cs.CopyTo(ms);
            return ms;
        }


        public static void EnryptUsernamePassword(UsernamePassword up, string filepath)
        {
            throw new NotImplementedException();
        }

        public static UsernamePassword DecryptUsernamePassword(string filepath)
        {
            return new UsernamePassword() { Password = "accountPassword", Username = "Administrator" };
        }

    }
}
