using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Api.Contracts.Authentication
{
    public class TokenRefreshRequest
    {
        public string RefreshToken { get; set; }
    }
}
