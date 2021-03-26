using Clean.Api.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Api.Services.Interfaces
{
    public interface IAbnLookupService
    {
        Task<AbnResult> LookupAbn(string Abn);
    }
}
