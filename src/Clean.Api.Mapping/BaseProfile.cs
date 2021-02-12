using AutoMapper;
using Clean.Api.Contracts.Users;
using Clean.Api.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clean.Api.Mapping
{
    public class BaseProfile : Profile
    {
        public BaseProfile()
        {
            CreateMap<User, UserResponse>()
                .ForMember(x => x.Roles, x => x.MapFrom(u => u.Roles.Select(r => r.Role.Name).ToArray()));

            //CreateMap<>
        }

    }
}
