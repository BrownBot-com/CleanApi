using Clean.Api.Contracts.Authentication;
using Clean.Api.Contracts.Users;
using Clean.Api.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Api.LogicProcessors.Interfaces
{
    public interface IAuthenticationProcessor
    {
        TokenResponse Authenticate(string username, string password);
        Task<User> Register(RegisterRequest model);
        Task ChangePassword(ChangeUserPasswordRequest requestModel);
    }
}
