using System;
using System.Linq;
using System.Security.Cryptography;

namespace Orbital.Core
{
    internal class SecurePasswordHasher : ISecurePasswordHasher
    {
        readonly int _hashSize;
        readonly int _iterationCount;
        readonly int _saltSize;

        /// <param name="hashSize"></param>
        /// <param name="saltSize"></param>
        /// <param name="iterationCount">
        /// Iteration count. The higher it is the more secure the output hash is, at the cost of performance.
        /// </param>
        public SecurePasswordHasher(int hashSize = 20, int saltSize = 16, int iterationCount = 10000)
        {
            _hashSize = hashSize;
            _saltSize = saltSize;
            _iterationCount = iterationCount;
        }

        /// <summary>
        /// Create hash from human-readable password
        /// </summary>
        /// <param name="password">Password (in human-readable text) to hash</param>
        /// <returns>A hash (including salt) associated with the given password</returns>
        public string Hash(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[_saltSize]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, _iterationCount);
            var hash = pbkdf2.GetBytes(_hashSize);

            var combinedHashAndSalt = new byte[_saltSize + _hashSize];
            Array.Copy(salt, 0, combinedHashAndSalt, 0, _saltSize);
            Array.Copy(hash, 0, combinedHashAndSalt, _saltSize, _hashSize);

            return Convert.ToBase64String(combinedHashAndSalt);
        }

        /// <summary>
        /// Verify human-readable password against hash
        /// </summary>
        /// <param name="password">Password (in human-readable text) to verify</param>
        /// <param name="passwordHash">Hashed password</param>
        /// <returns>True if given passwordHash is generated from given password. Otherwise False.</returns>
        public bool Verify(string password, string passwordHash)
        {
            var hashAndSalt = Convert.FromBase64String(passwordHash);
            var hash = new byte[_hashSize];
            var salt = new byte[_saltSize];
            Array.Copy(hashAndSalt, 0, salt, 0, _saltSize);
            Array.Copy(hashAndSalt, _saltSize, hash, 0, _hashSize);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, _iterationCount);
            var computedHash = pbkdf2.GetBytes(_hashSize);

            return computedHash.SequenceEqual(hash);
        }
    }
}