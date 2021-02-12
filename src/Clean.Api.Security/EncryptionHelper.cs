using System;
using System.Collections.Generic;
using System.Text;
using BCrypt.Net;

namespace Clean.Api.Security
{
    public static class EncryptionHelper
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        }

        public static bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
        }
    }
}
