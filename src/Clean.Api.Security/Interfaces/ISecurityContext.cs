using Clean.Api.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Api.Security.Interfaces
{
    public interface ISecurityContext
    {
        User CurrentUser { get; }
        Task<string> GetCurrentUserToken();

        bool IsAdministrator { get; }
        bool IsUserManager { get; }

        bool UserInRole(string role);
    }
}
