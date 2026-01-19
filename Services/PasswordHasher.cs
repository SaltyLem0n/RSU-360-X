using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace RSU_360_X.Services
{
    public static class PasswordHasher
    {
        public static bool Verify(string password, string hash, string salt, int iterations)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hash) || string.IsNullOrEmpty(salt))
                return false;

            // Convert salt to bytes
            byte[] saltBytes;
            try
            {
                saltBytes = Convert.FromBase64String(salt);
            }
            catch
            {
                return false;
            }

            // Derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            // Note: The provided data uses 50,000 iterations, so we use the passed 'iterations' param
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: iterations > 0 ? iterations : 50000,
                numBytesRequested: 256 / 8));

            return hashed == hash;
        }
    }
}
