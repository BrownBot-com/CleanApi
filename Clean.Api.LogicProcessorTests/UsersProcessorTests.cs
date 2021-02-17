using Clean.Api.Common.Exceptions;
using Clean.Api.Contracts.Authentication;
using Clean.Api.Contracts.Users;
using Clean.Api.DataAccess.Models;
using Clean.Api.DataAccess.Models.Interfaces;
using Clean.Api.LogicProcessors;
using Clean.Api.LogicProcessors.Interfaces;
using Clean.Api.Security.Interfaces;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Clean.Api.LogicProcessorTests
{
    public class UsersProcessorTests
    {
        public UsersProcessorTests()
        {
            _processor = new UsersProcessor(_usersRepository.Object, _securityContext.Object);
            _usersRepository.Setup(u => u.Query()).Returns(() => _users.AsQueryable());
        }

        private Mock<IRepository<User>> _usersRepository = new Mock<IRepository<User>>();
        private Mock<ISecurityContext> _securityContext = new Mock<ISecurityContext>();
        private Random _random = new Random();
        private List<User> _users = new List<User>();
        private IUsersProcessor _processor;

        [Fact]
        public void UserManagerCanQueryAllUserRecords()
        {
            _securityContext.Setup(s => s.IsUserManager).Returns(true);

            _users.Add(new User());
            _users.Add(new User());
            _users.Add(new User());

            var result = _processor.Query.ToArray();
            result.Count().Should().Be(3);
        }

        [Fact]
        public void UserManagerCanQueryAllUserRecordsExceptDeleted()
        {
            _securityContext.Setup(s => s.IsUserManager).Returns(true);

            _users.Add(new User());
            _users.Add(new User());
            _users.Add(new User() { IsDeleted = true });

            var result = _processor.Query.ToArray();
            result.Count().Should().Be(2);
        }

        [Fact]
        public void StandardUserCanOnlyQueryOwnRecord()
        {
            var currentUser = new User() { Id = 1 };
            _securityContext.Setup(s => s.IsUserManager).Returns(false);
            _securityContext.Setup(s => s.CurrentUser).Returns(currentUser);

            _users.Add(currentUser);
            _users.Add(new User());
            _users.Add(new User());

            var result = _processor.Query.ToArray();
            result.Count().Should().Be(1);
            result[0].Id.Should().Be(currentUser.Id);
        }

        [Fact]
        public void GetByIdShouldReturnCorrectUser()
        {
            var currentUser = new User() { Id = 1 };
            _securityContext.Setup(s => s.IsUserManager).Returns(true);
            _securityContext.Setup(s => s.CurrentUser).Returns(currentUser);

            _users.Add(currentUser);
            _users.Add(new User() { Id = 10 });
            _users.Add(new User() { Id = 20 });

            var result = _processor.Get(currentUser.Id);
            result.Should().NotBe(null);
            result.Id.Should().Be(currentUser.Id);
        }

        [Fact]
        public void GetByIdShouldReturnThrowExceptionIfNotFound()
        {
            var currentUser = new User() { Id = 1 };
            _securityContext.Setup(s => s.IsUserManager).Returns(true);
            _securityContext.Setup(s => s.CurrentUser).Returns(currentUser);

            _users.Add(currentUser);

            Action execute = () => _processor.Get(10);
            execute.Should().Throw<NotFoundException>();
        }

        [Fact]
        public void GetByUsernameShouldReturnCorrectUser()
        {
            var currentUser = new User() { Id = 1, Username = _random.Next().ToString() };
            _securityContext.Setup(s => s.IsUserManager).Returns(true);
            _securityContext.Setup(s => s.CurrentUser).Returns(currentUser);

            _users.Add(currentUser);
            _users.Add(new User() { Id = 10, Username = _random.Next().ToString() });
            _users.Add(new User() { Id = 20, Username = _random.Next().ToString() });

            var result = _processor.Get(currentUser.Username, false);
            result.Should().NotBe(null);
            result.Id.Should().Be(currentUser.Id);
        }

        [Fact]
        public void GetByUsernameShouldReturnThrowExceptionIfNonAdminQueriesOtherUser()
        {
            var currentUser = new User() { Id = 1, Username = _random.Next().ToString() };
            var otherUser = new User() { Id = 2, Username = _random.Next().ToString() };
            _securityContext.Setup(s => s.IsUserManager).Returns(false);
            _securityContext.Setup(s => s.CurrentUser).Returns(currentUser);

            _users.Add(currentUser);
            _users.Add(otherUser);

            Action execute = () => _processor.Get(otherUser.Username, false);
            execute.Should().Throw<NotFoundException>();
        }

        [Fact]
        public void GetByUsernameShouldReturnUserIfNonAdminQueriesOtherUserWithBypassSecuritySet()
        {
            var currentUser = new User() { Id = 1, Username = _random.Next().ToString() };
            var otherUser = new User() { Id = 2, Username = _random.Next().ToString() };
            _securityContext.Setup(s => s.IsUserManager).Returns(false);
            _securityContext.Setup(s => s.CurrentUser).Returns(currentUser);

            _users.Add(currentUser);
            _users.Add(otherUser);

            var result = _processor.Get(otherUser.Username, true);
            result.Should().NotBe(null);
            result.Should().Be(otherUser);
        }

        [Fact]
        public async void CreateShouldReturnSavedUser()
        {
            var currentUser = new User() 
            { 
                Id = 1, 
                Username = _random.Next().ToString(),
                FirstName = _random.Next().ToString(),
                LastName = _random.Next().ToString(),
            };

            _securityContext.Setup(s => s.IsUserManager).Returns(true);
            _securityContext.Setup(s => s.CurrentUser).Returns(currentUser);

            var newUserRequest = new CreateUserRequest()
            {
                Username = _random.Next().ToString(),
                FirstName = _random.Next().ToString(),
                LastName = _random.Next().ToString(),
                Password = _random.Next().ToString()
            };

            var result = await _processor.Create(newUserRequest);

            result.PasswordHash.Should().NotBeEmpty();
            result.Username.Should().Be(newUserRequest.Username);
            result.LastName.Should().Be(newUserRequest.LastName);
            result.FirstName.Should().Be(newUserRequest.FirstName);
        }

        [Fact]
        public void CreateShouldThrowExceptionIfUsernameTaken()
        {
            var newUserRequest = new CreateUserRequest()
            {
                Username = _random.Next().ToString()
            };

            _users.Add(new User() { Username = newUserRequest.Username });

            Action create = () => { var r = _processor.Create(newUserRequest).Result; };
            create.Should().Throw<BadRequestException>();
        }

        [Fact]
        public async void CreateFromRegisterShouldReturnSavedUser()
        {
            var newUserRequest = new RegisterRequest()
            {
                Username = _random.Next().ToString(),
                FirstName = _random.Next().ToString(),
                LastName = _random.Next().ToString(),
                Password = _random.Next().ToString()
            };

            var result = await _processor.Create(newUserRequest);

            result.PasswordHash.Should().NotBeEmpty();
            result.Username.Should().Be(newUserRequest.Username);
            result.LastName.Should().Be(newUserRequest.LastName);
            result.FirstName.Should().Be(newUserRequest.FirstName);
        }

        [Fact]
        public void CreateFromRegisterShouldThrowExceptionIfUsernameTaken()
        {
            var newUserRequest = new RegisterRequest()
            {
                Username = _random.Next().ToString()
            };

            _users.Add(new User() { Username = newUserRequest.Username });

            Action create = () => { var r = _processor.Create(newUserRequest).Result; };
            create.Should().Throw<BadRequestException>();
        }


    }
}
