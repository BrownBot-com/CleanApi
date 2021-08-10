using Clean.Api.Contracts.Items;
using Clean.Api.DataAccess.Models.Items;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Api.LogicProcessors.Interfaces
{
    public interface IItemDiscountGroupsProcessor
    {
        IQueryable<ItemDiscountGroup> Query { get; }
        ItemDiscountGroup Get(string code);
        Task<ItemDiscountGroup[]> Create(CreateItemDiscountGroupRequest[] requests);
        Task<ItemDiscountGroup> Update(UpdateItemDiscountGroupRequest request, string code);
        Task Delete(string code);
    }
}