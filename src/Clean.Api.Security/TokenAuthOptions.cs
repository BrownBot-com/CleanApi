using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Clean.Api.Security
{
    public class TokenAuthOptions
    {
        public static string Audience { get; } = "Clean.Api.Audience";

        public static string Issuer { get; } = "Clean.Api.Issuer";

        public static RsaSecurityKey Key { get; } = new RsaSecurityKey(GenerateKey());

        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);

        public static TimeSpan ExpiresSpan { get; } = TimeSpan.FromMinutes(30);

        public static string TokenType { get; } = "Bearer";

        public static RSAParameters GenerateKey()
        {
            using (var key = new RSACryptoServiceProvider(2048))
            {
                return key.ExportParameters(true);
            }
        }
    }
}
