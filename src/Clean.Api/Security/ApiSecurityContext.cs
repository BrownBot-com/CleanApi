using Clean.Api.DataAccess.Models;
using Clean.Api.LogicProcessors.Interfaces;
using Clean.Api.Security.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Api.Security
{
    public class ApiSecurityContext : ISecurityContext
    {
        public ApiSecurityContext(IHttpContextAccessor contextAccessor, IUsersProcessor usersProcessor)
        {
            _contextAccessor = contextAccessor;
            _usersProcessor = usersProcessor;
        }

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUsersProcessor _usersProcessor;
        private User _cachedUser;

        public User CurrentUser
        {
            get
            {
                if (_cachedUser != null) return _cachedUser;

                if (!_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    throw new UnauthorizedAccessException();
                }

                var username = _contextAccessor.HttpContext.User.Identity.Name;
                _cachedUser = _usersProcessor.Get(username);

                if (_cachedUser == null) throw new UnauthorizedAccessException("User is not found");

                return _cachedUser;
            }
        }

        public bool IsAdministrator => CurrentUser.Roles.Any(r => r.Role.Name == Roles.Administrator);

        public bool IsUserManager => CurrentUser.Roles.Any(r => r.Role.Name == Roles.Administrator || r.Role.Name == Roles.UserManager);

        public bool UserInRole(string roleName)
        {
            return CurrentUser.Roles.Any(r => r.Role.Name == roleName);
        }
    }
}
