using Clean.Api.Contracts.Items;
using Clean.Api.DataAccess.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Api.LogicProcessors.Interfaces
{
    public interface IPriceListProcessor
    {
        IQueryable<PriceList> Query { get; }
        PriceList Get(int id);
        Task<PriceList> Create(CreatePriceListRequest request);
        Task<ItemPrice[]> LinkPriceListItems();
        Task Delete(int id);
    }
}
