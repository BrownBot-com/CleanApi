using Clean.Api.Contracts.Items;
using Clean.Api.DataAccess.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Api.LogicProcessors.Interfaces
{
    public interface IItemsProcessor
    {
        IQueryable<Item> Query { get; }
        Item Get(int id);
        Item Get(string code);
        Task<Item[]> Create(CreateItemRequest[] requests);
        Task<ItemStock[]> Create(CreateItemStockRequest[] requests);
        //Task<Item> Update(int id, UpdateItemRequest model);
        Task Delete(int id);
    }
}
