using System;
using System.Security.Cryptography;
using System.Text;
namespace DataSaver
{
    public static class Coder
    {
        private static byte[] key = new byte[8] { 1, 3, 7, 9, 12, 77, 99, 88 };
        private static byte[] iv = new byte[8] { 12, 23, 75, 39, 112, 27, 9, 38 };

        public static string Encode(string text)
        {
            byte[] textInBytes = Encoding.UTF8.GetBytes(text);
            byte[] encryptedBytes = DES.Create().CreateEncryptor(key, iv).TransformFinalBlock(textInBytes, 0, textInBytes.Length);
            string encryptedText = Convert.ToBase64String(encryptedBytes);
            return encryptedText;
        }

        public static string Decode(string text)
        {
            byte[] textInBytes = Convert.FromBase64String(text);
            byte[] decryptedBytes = DES.Create().CreateDecryptor(key, iv).TransformFinalBlock(textInBytes, 0, textInBytes.Length);
            string decryptedText = Encoding.UTF8.GetString(decryptedBytes);
            return decryptedText;
        }
    }
}
