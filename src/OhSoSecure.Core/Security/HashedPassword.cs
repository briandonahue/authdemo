using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace OhSoSecure.Core.Security
{
    public class HashedPassword
    {
        protected HashedPassword() {}

        public HashedPassword(string password)
        {
            PasswordSalt = CreateSalt();
            PasswordHash = CreateHash(password, PasswordSalt);
        }

        public HashedPassword(string passwordHash, string salt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = salt;
        }

        public virtual string PasswordHash { get; private set; }

        public virtual string PasswordSalt { get; private set; }

        public bool Matches(string password)
        {
            var hashAttempt = CreateHash(password, PasswordSalt);
            return PasswordHash == hashAttempt;
        }

        static string CreateHash(string password, string salt)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var saltBytes = Convert.FromBase64String(salt);
            var bytes = passwordBytes.Concat(saltBytes).ToArray();
            using (var sha256 = SHA256.Create())
            {
                return Convert.ToBase64String(sha256.ComputeHash(bytes));
            }
        }

        static string CreateSalt()
        {
            var saltBytes = new byte[32];
            using (var provider = RandomNumberGenerator.Create())
            {
                provider.GetNonZeroBytes(saltBytes);
            }
            var salt = Convert.ToBase64String(saltBytes);
            return salt;
        }
    }
}