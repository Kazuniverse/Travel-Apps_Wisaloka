using System;
using System.Security.Cryptography;

namespace Pariwisata_Apps
{
    public static class PasswordHelper
    {
        private const int SaltSize = 16;  // bytes
        private const int HashSize = 32;  // bytes
        private const int DefaultIterations = 100000;

        // Membuat hash password baru
        public static string HashPassword(string password, int iterations = DefaultIterations)
        {
            // Buat salt acak
            byte[] salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            // Hash password
            byte[] hash;
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                hash = pbkdf2.GetBytes(HashSize);
            }

            // Format: iterasi.salt.hash (Base64)
            return $"{iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        // Verifikasi password
        public static bool VerifyPassword(string password, string storedHash)
        {
            var parts = storedHash.Split('.');
            if (parts.Length != 3)
                return false;

            int iterations = int.Parse(parts[0]);
            byte[] salt = Convert.FromBase64String(parts[1]);
            byte[] hash = Convert.FromBase64String(parts[2]);

            byte[] testHash;
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                testHash = pbkdf2.GetBytes(hash.Length);
            }

            // Bandingkan byte per byte (constant time)
            int diff = 0;
            for (int i = 0; i < hash.Length; i++)
            {
                diff |= hash[i] ^ testHash[i];
            }

            return diff == 0;
        }
    }
}