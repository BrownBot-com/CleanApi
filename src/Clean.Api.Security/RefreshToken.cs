using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Api.Security
{
    public class RefreshToken
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
