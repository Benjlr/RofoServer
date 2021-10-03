using System.IO;
using System.Security.Cryptography;

namespace RofoServer.Core.Utils
{
    public class EncryptionService
    {
        public static byte[] Encrypt(byte[] input)
        {
            var pdb =
                new Rfc2898DeriveBytes("ajsghrtyescmmddkn", // Change this
                    new byte[] { 0x28, 0x45, 0x21, 0x52 }); // Change this
            MemoryStream ms = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);
            var cs = new CryptoStream(ms,
                aes.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.Close();
            return ms.ToArray();
        }
        public static byte[] Decrypt(byte[] input)
        {
            var pdb =
                new Rfc2898DeriveBytes("ajsghrtyescmmddkn", // Change this
                    new byte[] { 0x28, 0x45, 0x21, 0x52 }); // Change this
            var ms = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);
            var cs = new CryptoStream(ms,
                aes.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.Close();
            return ms.ToArray();
        }

    }
}