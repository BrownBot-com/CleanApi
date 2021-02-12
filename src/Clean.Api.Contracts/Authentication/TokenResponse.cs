using Clean.Api.Contracts.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Api.Contracts.Authentication
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public UserResponse User { get; set; }
        public DateTime Expires { get; set; }
    }
}
