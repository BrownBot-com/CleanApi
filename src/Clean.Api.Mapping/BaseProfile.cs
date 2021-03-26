using AutoMapper;
using Clean.Api.Contracts.AbnLookup;
using Clean.Api.Contracts.Items;
using Clean.Api.Contracts.Users;
using Clean.Api.DataAccess.Models.Items;
using Clean.Api.DataAccess.Models.Users;
using Clean.Api.Services.Models;
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
            CreateMap<Item, ItemResponse>();
            CreateMap<ItemStock, ItemStockResponse>();
            CreateMap<AbnResult, AbnLookupResult>();
            CreateMap<ItemPrice, ItemPriceResponse>();
            CreateMap<PriceList, PriceListResponse>();
        }

    }
}
