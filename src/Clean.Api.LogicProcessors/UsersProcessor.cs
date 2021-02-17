using Clean.Api.Common.Exceptions;
using Clean.Api.Contracts.Authentication;
using Clean.Api.Contracts.Users;
using Clean.Api.DataAccess.Models.Interfaces;
using Clean.Api.DataAccess.Models;
using Clean.Api.LogicProcessors.Interfaces;
using Clean.Api.Security;
using Clean.Api.Security.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Clean.Api.LogicProcessors
{
    public class UsersProcessor : IUsersProcessor
    {
        public UsersProcessor(IRepository<User> usersRepository, ISecurityContext securityContext)
        {
            _usersRepo = usersRepository;
            _securityContext = securityContext;
        }

        private readonly IRepository<User> _usersRepo;
        private readonly ISecurityContext _securityContext;

        private IQueryable<User> _filteredQuery
        {
            get
            {
                var baseQuery = _usersRepo.Query();
                
                if (_securityContext.IsUserManager)
                {
                    baseQuery = baseQuery.Where(x => !x.IsDeleted);
                }
                else
                {
                    baseQuery = baseQuery.Where(u => u.Id == _securityContext.CurrentUser.Id);
                }

                return baseQuery
                            .Include(x => x.Roles)
                                .ThenInclude(x => x.Role);
            }
        }

        public IQueryable<User> Query => _filteredQuery;

        public User Get(int id)
        {
            var user = _filteredQuery.FirstOrDefault(x => x.Id == id);

            if (user == null) throw new NotFoundException("User not found");

            return user;
        }

        public User Get(string username, bool bypassSecurity = false)
        {
            User user = null;

            if (bypassSecurity)
            {
                user = _usersRepo.Query()
                                    .Include(x => x.Roles)
                                        .ThenInclude(x => x.Role)
                                            .FirstOrDefault(x => x.Username == username);
            }
            else
            {
                user = Query.FirstOrDefault(x => x.Username == username);
            }

            if (user == null) throw new NotFoundException("User not found");

            return user;
        }

        public async Task<User> Create(CreateUserRequest request)
        {
            var username = request.Username.Trim().ToLower();

            if (_usersRepo.Query().Any(u => u.Username == username)) throw new BadRequestException("Username is already in use");

            var user = new User
            {
                Username = request.Username.Trim(),
                PasswordHash = EncryptionHelper.HashPassword(request.Password.Trim()),
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
            };

            SyncUserRoles(user, request.Roles);

            _usersRepo.Add(user);
            await _usersRepo.SaveAsync();

            return user;
        }

        public async Task<User> Create(RegisterRequest request)
        {
            var username = request.Username.Trim().ToLower();

            if (_usersRepo.Query().Any(u => u.Username == username)) throw new BadRequestException("Username is already in use");

            var user = new User
            {
                Username = request.Username.Trim(),
                PasswordHash = EncryptionHelper.HashPassword(request.Password.Trim()),
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
            };

            _usersRepo.Add(user);
            await _usersRepo.SaveAsync();

            return user;
        }

        public async Task<User> Update(int id, UpdateUserRequest model)
        {
            var user = EnsureAccessAndLoadUser(id);

            user.Username = model.Username;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            SyncUserRoles(user, model.Roles);

            await _usersRepo.SaveAsync();
            return user;
        }

        public async Task Delete(int id)
        {
            var user = EnsureAccessAndLoadUser(id);

            if (user.IsDeleted) return;
            user.IsDeleted = true;

            await _usersRepo.SaveAsync();
        }

        public async Task ChangePassword(int id, ChangeUserPasswordRequest model)
        {
            var user = EnsureAccessAndLoadUser(id);
            if (!EncryptionHelper.VerifyPassword(model.OldPassword, user.PasswordHash)) throw new ForbiddenException("Invalid password");

            user.PasswordHash = EncryptionHelper.HashPassword(model.NewPassword);
            await _usersRepo.SaveAsync();
        }


        private User EnsureAccessAndLoadUser(int id)
        {
            if (!_securityContext.IsUserManager && _securityContext.CurrentUser.Id != id) throw new ForbiddenException("Not your user id");
            var user = Query.FirstOrDefault(u => u.Id == id);

            if (user == null) throw new NotFoundException("User is not found");
            return user;
        }

        private void SyncUserRoles(User user, string[] roleNames)
        {
            if (!_securityContext.IsUserManager && roleNames.Length > 0) throw new ForbiddenException("You don't have permissions to add user roles");

            var currentUserAuthority = _securityContext.CurrentUser.Roles.Max(r => r.Role.Authority as int?) ?? 0;

            // clean up any missing roles
            user.Roles.RemoveAll(r => !roleNames.Contains(r.Role.Name));

            var existingRoles = user.Roles.Select(r => r.Role.Name);

            foreach (var roleName in roleNames)
            {
                // don't re-add existing roles
                if (existingRoles.Contains(roleName)) continue;

                var role = _usersRepo.Query<Role>().FirstOrDefault(x => x.Name == roleName);

                if (role == null) throw new NotFoundException($"Role - {roleName} is not found");
                if (role.Authority > currentUserAuthority) throw new ForbiddenException($"Role {roleName} has higher authority, you can't add yourself to a higher authority role");

                user.Roles.Add(new UserRole { User = user, Role = role });
            }
        }
    }
}
