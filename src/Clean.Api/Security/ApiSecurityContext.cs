using Clean.Api.DataAccess.Models;
using Clean.Api.DataAccess.Models.Interfaces;
using Clean.Api.LogicProcessors.Interfaces;
using Clean.Api.Security.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Api.Security
{
    public class ApiSecurityContext : ISecurityContext
    {
        public ApiSecurityContext(IHttpContextAccessor contextAccessor, IRepository<User> usersRepository)
        {
            _contextAccessor = contextAccessor;
            _usersRepository = usersRepository;
        }

        private readonly IHttpContextAccessor _contextAccessor;

        // We have to use a Repository rather than the UserProcessor to avoid a circular reference
        private readonly IRepository<User> _usersRepository;
        private User _cachedUser;

        public User CurrentUser
        {
            get
            {
                if (_cachedUser != null) return _cachedUser;

                //if (!_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
                //{
                //    throw new UnauthorizedAccessException();
                //}

                var username = _contextAccessor.HttpContext.User.Identity.Name;

                // this is not the best but hard to avoid due to a circular reference created when using IUserProcessor
                _cachedUser = _usersRepository.Query()
                                                .Include(x => x.Roles)
                                                    .ThenInclude(x => x.Role)
                                                        .FirstOrDefault(x => x.Username == username);

                if (_cachedUser == null) throw new UnauthorizedAccessException("User is not found");

                return _cachedUser;
            }
        }

        public async Task<string> GetCurrentUserToken()
        {
            if (!_contextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var tokenHeader)) return null;
            return tokenHeader[0].Replace("Bearer ", string.Empty);
        }

        public bool IsAdministrator => CurrentUser.Roles.Any(r => r.Role.Name == Roles.Administrator);

        public bool IsUserManager => CurrentUser.Roles.Any(r => r.Role.Name == Roles.Administrator || r.Role.Name == Roles.UserManager);


        public bool UserInRole(string roleName)
        {
            return CurrentUser.Roles.Any(r => r.Role.Name == roleName);
        }
    }
}
