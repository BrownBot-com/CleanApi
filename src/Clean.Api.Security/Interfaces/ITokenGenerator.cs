using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Api.Security.Interfaces
{
    public interface ITokenGenerator
    {
        string Build(string username, string[] roles, DateTime expireDate);
    }
}
