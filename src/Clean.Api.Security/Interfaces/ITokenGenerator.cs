using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Api.Security.Interfaces
{
    public interface ITokenGenerator
    {
        string BuildToken(string username, string[] roles, DateTime expireDate);
        string BuildRefreshToken(string userName, DateTime expires);
        (string, string) Refresh(string refreshToken, string accessToken, DateTime expireDate);
        
        void RemoveExpiredRefreshTokens(DateTime now);
        void RemoveRefreshTokenByUserName(string userName);
    }
}
