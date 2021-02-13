using Clean.Api.Contracts.Authentication;
using Clean.Api.Contracts.Users;
using Clean.Api.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Api.LogicProcessors.Interfaces
{
    public interface IUsersProcessor
    {
        IQueryable<User> Query { get; }
        User Get(int id);
        User Get(string username, bool bypassSecurity);
        Task<User> Create(CreateUserRequest model);
        Task<User> Create(RegisterRequest model);
        Task<User> Update(int id, UpdateUserRequest model);
        Task Delete(int id);
        Task ChangePassword(int id, ChangeUserPasswordRequest model);
    }
}
