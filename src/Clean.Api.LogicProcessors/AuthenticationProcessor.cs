using AutoMapper;
using Clean.Api.Common.Exceptions;
using Clean.Api.Contracts.Authentication;
using Clean.Api.Contracts.Users;
using Clean.Api.DataAccess.Models.Interfaces;
using Clean.Api.DataAccess.Models.Users;
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
        public AuthenticationProcessor(ITokenGenerator tokenBuilder, IUsersProcessor usersProcessor, ISecurityContext securityContext, IMapper mapper)
        {
            _random = new Random();
            _tokenGenerator = tokenBuilder;
            _usersProcessor = usersProcessor;
            _securityContext = securityContext;
            _mapper = mapper;
        }

        private readonly ITokenGenerator _tokenGenerator;
        private readonly IUsersProcessor _usersProcessor;
        private readonly ISecurityContext _securityContext;
        private readonly IMapper _mapper;
        private Random _random;

        public object TokenAuthOption { get; private set; }

        public TokenResponse Authenticate(string username, string password)
        {
            var user = _usersProcessor.Get(username, true);   

            if (user == null) throw new BadRequestException("Username or password incorrect");

            if (string.IsNullOrWhiteSpace(password) || !EncryptionHelper.VerifyPassword(password, user.PasswordHash))
            {
                throw new BadRequestException("Username or password incorrect");
            }

            var tokenExpires = DateTime.Now.Add(TokenAuthOptions.TokenExpiresSpan);
            var refreshTokenExpires = DateTime.Now.Add(TokenAuthOptions.RefreshTokenExpiresSpan);
            var token = _tokenGenerator.BuildToken(user.Username, user.Roles.Select(x => x.Role.Name).ToArray(), tokenExpires);

            return new TokenResponse
            {
                TokenExpires = tokenExpires.ToUniversalTime(),
                Token = token,
                RefreshTokenExpires = refreshTokenExpires.ToUniversalTime(),
                RefreshToken = _tokenGenerator.BuildRefreshToken(user.Username, refreshTokenExpires),
                User = _mapper.Map<UserResponse>(user)
            };
        }

        public async Task<TokenResponse> RefreshToken(string refreshToken)
        {
            //var user = _securityContext.CurrentUser;
            var accessToken = await _securityContext.GetCurrentUserToken();
            var tokenExpires = DateTime.Now + TokenAuthOptions.TokenExpiresSpan;
            var refreshTokenExpires = DateTime.Now.Add(TokenAuthOptions.RefreshTokenExpiresSpan);
            //user.Roles.Select(r => r.Role.Name).ToArray()
            var (token, username) = _tokenGenerator.Refresh(refreshToken, accessToken, tokenExpires);
            var user = _usersProcessor.Get(username, true);

            return new TokenResponse
            {
                TokenExpires = tokenExpires.ToUniversalTime(),
                Token = token,
                RefreshTokenExpires = refreshTokenExpires.ToUniversalTime(),
                RefreshToken = _tokenGenerator.BuildRefreshToken(username, refreshTokenExpires),
                User = _mapper.Map<UserResponse>(user)
            };
        }

        public async Task<User> Register(RegisterRequest request)
        {
            return await _usersProcessor.Create(request);
        }

        public async Task ChangePassword(ChangeUserPasswordRequest request)
        {
            await _usersProcessor.ChangePassword(_securityContext.CurrentUser.Id, request);
        }

        public void InvalidateRefreshToken(string userName)
        {
            _tokenGenerator.RemoveRefreshTokenByUserName(userName);
        }
    }
}
