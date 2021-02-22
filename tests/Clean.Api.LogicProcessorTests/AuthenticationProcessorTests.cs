using AutoMapper;
using Clean.Api.Common.Exceptions;
using Clean.Api.DataAccess.Models;
using Clean.Api.DataAccess.Models.Users;
using Clean.Api.LogicProcessors;
using Clean.Api.LogicProcessors.Interfaces;
using Clean.Api.Mapping;
using Clean.Api.Security;
using Clean.Api.Security.Interfaces;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Clean.Api.LogicProcessorTests
{
    public class AuthenticationProcessorTests
    {
        public AuthenticationProcessorTests()
        {
            // only the auth processor needs the mapper
            _mapper = new MapperConfiguration(c => c.AddProfile<BaseProfile>()).CreateMapper();
            _processor = new AuthenticationProcessor(_tokenBuilder.Object, _usersProcessor.Object, _securityContext.Object, _mapper);


        }

        private Mock<ITokenGenerator> _tokenBuilder = new Mock<ITokenGenerator>();
        private Mock<IUsersProcessor> _usersProcessor = new Mock<IUsersProcessor>();
        private Mock<ISecurityContext> _securityContext = new Mock<ISecurityContext>();
        private Random _random = new Random();
        private IMapper _mapper;
        private IAuthenticationProcessor _processor;

        [Fact]
        public void AuthenticateShouldThrowIfUserPasswordIsWrong()
        {
            var username = "bob";
            var password = EncryptionHelper.HashPassword(_random.Next().ToString());

            _usersProcessor.Setup(p => p.Get(username, true)).Returns(new User() { Username = username, PasswordHash = password });

            Action execute = () => _processor.Authenticate(username, _random.Next().ToString());

            execute.Should().Throw<BadRequestException>();
        }

        [Fact]
        public void AuthenticateShouldThrowIfUserPasswordBlank()
        {
            var username = "bob";
            var password = EncryptionHelper.HashPassword(_random.Next().ToString());

            _usersProcessor.Setup(p => p.Get(username, true)).Returns(new User() { Username = username, PasswordHash = password });

            Action execute = () => _processor.Authenticate(username, string.Empty);

            execute.Should().Throw<BadRequestException>();
        }

        [Fact]
        public void AuthenticateShouldReturnTokenResponse()
        {
            var username = "bob";
            var password = _random.Next().ToString();
            var passwordHash = EncryptionHelper.HashPassword(password);
            _usersProcessor.Setup(p => p.Get(username, true)).Returns(new User() { Username = username, PasswordHash = passwordHash });

            
            var token = _random.Next().ToString();
            var refreshToken = _random.Next().ToString();
            _tokenBuilder.Setup(b => b.BuildToken(It.IsAny<string>(), It.IsAny<string[]>(), It.IsAny<DateTime>())).Returns(token);
            _tokenBuilder.Setup(b => b.BuildRefreshToken(username, It.IsAny<DateTime>())).Returns(refreshToken);

            var expiryDate = DateTime.Now.Add(TokenAuthOptions.TokenExpiresSpan).ToUniversalTime();
            var refreshTokenExpiryDate = DateTime.Now.Add(TokenAuthOptions.RefreshTokenExpiresSpan).ToUniversalTime();
            var result = _processor.Authenticate(username, password);

            result.Token.Should().Be(token);
            result.RefreshToken.Should().Be(refreshToken);
            result.User.Username.Should().Be(username);
            result.TokenExpires.Should().BeCloseTo(expiryDate, 500);
            result.RefreshTokenExpires.Should().BeCloseTo(refreshTokenExpiryDate, 500);
        }

        [Fact]
        public async void RefreshTokenShouldReturnTokenResponse()
        {
            var username = "bob";
            var password = _random.Next().ToString();
            var passwordHash = EncryptionHelper.HashPassword(password);
            _usersProcessor.Setup(p => p.Get(username, true)).Returns(new User() { Username = username, PasswordHash = passwordHash });

            var refreshToken = _random.Next().ToString();
            var accessToken = _random.Next().ToString();
            _securityContext.Setup(s => s.GetCurrentUserToken()).Returns(Task.FromResult(accessToken));

            var token = _random.Next().ToString();
            _tokenBuilder.Setup(b => b.Refresh(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns((token, username));
            _tokenBuilder.Setup(b => b.BuildRefreshToken(username, It.IsAny<DateTime>())).Returns(refreshToken);

            var expiryDate = DateTime.Now.Add(TokenAuthOptions.TokenExpiresSpan).ToUniversalTime();
            var refreshTokenExpiryDate = DateTime.Now.Add(TokenAuthOptions.RefreshTokenExpiresSpan).ToUniversalTime();
            var result = await _processor.RefreshToken(refreshToken);

            result.Token.Should().Be(token);
            result.RefreshToken.Should().Be(refreshToken);
            result.User.Username.Should().Be(username);
            result.TokenExpires.Should().BeCloseTo(expiryDate, 500);
            result.RefreshTokenExpires.Should().BeCloseTo(refreshTokenExpiryDate, 500);
        }
    }
}
