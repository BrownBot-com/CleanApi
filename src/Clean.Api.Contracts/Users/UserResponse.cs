using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Api.Contracts.Users
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string[] Roles { get; set; } = new string[0];
    }
}
