using Clean.Api.Contracts.Brands;
using Clean.Api.DataAccess.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Api.LogicProcessors.Interfaces
{
    public interface IBrandsProcessor
    {
        IQueryable<Brand> Query { get; }
        Brand Get(string code);
        Task<Brand> Create(CreateBrandRequest requests);
        Task<Brand> Update(UpdateBrandRequest request, string code);
        Task Delete(string code);
    }
}
