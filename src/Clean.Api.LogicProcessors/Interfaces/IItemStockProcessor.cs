using Clean.Api.Contracts.Items;
using Clean.Api.DataAccess.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Api.LogicProcessors.Interfaces
{
    public interface IItemStockProcessor
    {
        IQueryable<ItemStock> Query { get; }
        ItemStock Get(int id);
        Task<ItemStock[]> Create(CreateItemStockRequest[] requests);
        Task<ItemStock> Update(UpdateItemStockRequest request, int id);
        Task<ItemStock[]> Reprocess();
        Task Delete(int id);
    }
}
