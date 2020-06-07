using System;
using System.Security.Cryptography;
using System.Text;

namespace art_shop_core.DAL.Security
{
    internal static class EncryptionHelper
    {
        private static byte[] GenerateSalt(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.ASCII.GetBytes(input));
            }
        }

        private static byte[] Combine(byte[] first, byte[] second)
        {
            var ret = new byte[first.Length + second.Length];

            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);

            return ret;
        }

        public static byte[] HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var combinedHash = Combine(Encoding.ASCII.GetBytes(password), GenerateSalt(password));

                return sha256.ComputeHash(combinedHash);
            }
        }

        public static bool ComparePasswords(string inputPassword, string storedPassword)
        {
            return Convert.ToBase64String(HashPassword(inputPassword)) == storedPassword;
        }
    }
}
