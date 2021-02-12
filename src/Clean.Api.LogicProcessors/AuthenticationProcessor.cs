using AutoMapper;
using Clean.Api.Common.Exceptions;
using Clean.Api.Contracts.Authentication;
using Clean.Api.Contracts.Users;
using Clean.Api.DataAccess.Interfaces;
using Clean.Api.DataAccess.Models;
using Clean.Api.LogicProcessors.Interfaces;
using Clean.Api.Security;
using Clean.Api.Security.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Api.LogicProcessors
{
    public class AuthenticationProcessor : IAuthenticationProcessor
    {
        public AuthenticationProcessor(ITokenGenerator tokenBuilder, IUsersProcessor usersQueryProcessor, ISecurityContext context, IMapper mapper)
        {
            _random = new Random();
            _tokenBuilder = tokenBuilder;
            _usersQueryProcessor = usersQueryProcessor;
            _context = context;
            _mapper = mapper;
        }

        private readonly ITokenGenerator _tokenBuilder;
        private readonly IUsersProcessor _usersQueryProcessor;
        private readonly ISecurityContext _context;
        private readonly IMapper _mapper;
        private Random _random;

        public object TokenAuthOption { get; private set; }

        public TokenResponse Authenticate(string username, string password)
        {
            var user = _usersQueryProcessor.Get(username);

            if (user == null) throw new BadRequestException("Username or password incorrect");

            if (string.IsNullOrWhiteSpace(password) || !EncryptionHelper.VerifyPassword(password, user.PasswordHash))
            {
                throw new BadRequestException("Username or password incorrect");
            }

            var expiresIn = DateTime.Now + TokenAuthOptions.ExpiresSpan;
            var token = _tokenBuilder.Build(user.Username, user.Roles.Select(x => x.Role.Name).ToArray(), expiresIn);

            return new TokenResponse
            {
                Expires = expiresIn,
                Token = token,
                User = _mapper.Map<UserResponse>(user)
            };
        }

        public async Task<User> Register(RegisterRequest model)
        {
            var requestModel = new CreateUserRequest()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                Username = model.Username
            };

            var user = await _usersQueryProcessor.Create(requestModel);
            return user;
        }

        public async Task ChangePassword(ChangeUserPasswordRequest request)
        {
            await _usersQueryProcessor.ChangePassword(_context.CurrentUser.Id, request);
        }
    }
}
