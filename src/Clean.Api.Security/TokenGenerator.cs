using Clean.Api.Security.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace Clean.Api.Security
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly ConcurrentDictionary<string, RefreshToken> _usersRefreshTokens = new ConcurrentDictionary<string, RefreshToken>();

        private static string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public string BuildToken(string username, string[] roles, DateTime expireDate)
        {
            var handler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>();
            foreach (var userRole in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(username, "Bearer"), claims);

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = TokenAuthOptions.Issuer,
                Audience = TokenAuthOptions.Audience,
                SigningCredentials = TokenAuthOptions.SigningCredentials,
                Subject = identity,
                Expires = expireDate
            });

            return handler.WriteToken(securityToken);
        }

        public string BuildRefreshToken(string userName, DateTime expires)
        {
            var refreshToken = new RefreshToken()
            {
                Username = userName,
                Expires = expires,
                Token = GenerateRefreshTokenString()
            };

            _usersRefreshTokens.TryAdd(refreshToken.Token, refreshToken);

            return refreshToken.Token;
        }

        public (string,string) Refresh(string refreshToken, string accessToken, DateTime expireDate)
        {
            var handler = new JwtSecurityTokenHandler();

            var principal = handler.ValidateToken(accessToken, new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = TokenAuthOptions.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = TokenAuthOptions.Key,
                ValidateAudience = true,
                ValidAudience = TokenAuthOptions.Audience,
                ValidateLifetime = false, // we might be validating an expired token
                ClockSkew = TimeSpan.FromMinutes(1)
            }, out var securityToken);
            
            if(securityToken == null || !(securityToken as JwtSecurityToken).Header.Alg.Equals(SecurityAlgorithms.RsaSha256))
            {
                throw new UnauthorizedAccessException("Invalid token");
            }

            if(!_usersRefreshTokens.TryGetValue(refreshToken, out var storedRefreshToken))
            {
                throw new UnauthorizedAccessException("Invalid token");
            }

            if (storedRefreshToken.Username != principal.Identity.Name || storedRefreshToken.Expires < DateTime.Now)
            {
                throw new UnauthorizedAccessException("Invalid token");
            }

            // clean up the old refresh token as we're going to issue a new one
            _usersRefreshTokens.TryRemove(storedRefreshToken.Token, out var deletedToken);

            var roles = principal.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToArray();

            return (BuildToken(principal.Identity.Name, roles, expireDate), principal.Identity.Name);
        }

        public void RemoveExpiredRefreshTokens(DateTime now)
        {
            var expiredTokens = _usersRefreshTokens.Where(x => x.Value.Expires < now).ToList();
            foreach (var expiredToken in expiredTokens)
            {
                _usersRefreshTokens.TryRemove(expiredToken.Key, out _);
            }
        }

        public void RemoveRefreshTokenByUserName(string userName)
        {
            var refreshTokens = _usersRefreshTokens.Where(x => x.Value.Username == userName).ToList();
            foreach (var refreshToken in refreshTokens)
            {
                _usersRefreshTokens.TryRemove(refreshToken.Key, out _);
            }
        }
    }
}
