using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Api.Security
{
    public static class Roles
    {
        public const string Administrator = "Administrator";
        public const string UserManager = "UserManager";
        public const string AdministratorOrUserManager = "Administrator,UserManager";
    }
}
