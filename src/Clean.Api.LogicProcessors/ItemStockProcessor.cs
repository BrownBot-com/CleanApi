using Clean.Api.Common.Exceptions;
using Clean.Api.Contracts.Items;
using Clean.Api.DataAccess.Models.Interfaces;
using Clean.Api.DataAccess.Models.Items;
using Clean.Api.LogicProcessors.Interfaces;
using Clean.Api.Security.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Api.LogicProcessors
{
    public class ItemStockProcessor : IItemStockProcessor
    {
        public ItemStockProcessor(IRepository<Item> itemsRepository, ISecurityContext securityContext)
        {
            _itemsRepository = itemsRepository;
            _securityContext = securityContext;
        }

        private IRepository<Item> _itemsRepository;
        private ISecurityContext _securityContext;

        public IQueryable<ItemStock> Query => _itemsRepository.Query<ItemStock>();

        public ItemStock Get(int id)
        {
            var item = Query.FirstOrDefault(i => i.Id == id);
            if (item == null) throw new NotFoundException("Item not found");
            return item;
        }

        private string CleanItemCode(string code)
        {
            return code.Trim().ToUpper().Replace(" ", string.Empty);
        }

        private string ParseFullDescription(string desc)
        {
            var newDescription = desc.Trim();
            if (newDescription.Length > 100)
            {
                newDescription = newDescription.Substring(0, 100);
            }
            return newDescription;
        }

        public async Task<ItemStock[]> Create(CreateItemStockRequest[] requests)
        {
            var result = new List<ItemStock>();
            Item lookupItem = null;
            int i = 0;
            foreach (var request in requests)
            {
                var itemCode = CleanItemCode(request.ItemCode);
                if (lookupItem == null || lookupItem.Code != itemCode)
                {
                    lookupItem = _itemsRepository.Query().FirstOrDefault(i => i.Code == itemCode || i.OldCode == itemCode);
                    //if (lookupItem == null) throw new NotFoundException($"ItemCode [{itemCode}] not found");
                }
                var itemStock = new ItemStock
                {
                    BranchCode = request.BranchCode.Trim(),
                    Current = request.Current,
                    ItemId = lookupItem == null ? 0 : lookupItem.Id,
                    ItemCode = itemCode,
                    LastOrdered = request.LastOrdered,
                    Max = request.Max,
                    Min = request.Min,
                    Bin  = request.Bin,
                    ImportNumber = request.ImportNumber
                };

                _itemsRepository.Add(itemStock);
                result.Add(itemStock);
                try
                {
                    await _itemsRepository.SaveAsync();
                }
                catch (Exception e)
                {
                    throw new BadRequestException($"Exception saving record Index [{i}] ItemCode [{itemCode}] Branch [{itemStock.BranchCode}] Message [{e.InnerException}]");
                }

                i++;
            }

            return result.ToArray();
        }

        public async Task<ItemStock> Update(UpdateItemStockRequest request, int id)
        {
            var item = Query.FirstOrDefault(i => i.Id == id);

            if (item == null) throw new NotFoundException("User is not found");

            item.Bin = request.Bin;
            item.BranchCode = request.BranchCode;
            item.Current = request.Current;
            item.ItemCode = request.ItemCode;
            item.LastOrdered = request.LastOrdered;
            item.Max = request.Max;
            item.Min = request.Min;
            item.Reprocess = request.Reprocess;

            await _itemsRepository.SaveAsync();
            return item;
        }

        public async Task Delete(int id)
        {
            var item = Query.FirstOrDefault(i => i.Id == id);
            if (item == null) throw new NotFoundException("Item not found");
            _itemsRepository.Remove(item);
            await _itemsRepository.SaveAsync();
        }

        public async Task<ItemStock[]> Reprocess()
        {
            var results = new List<ItemStock>();
            var hitList = _itemsRepository.Query<ItemStock>().Where(i => i.Reprocess == true).ToArray();
            foreach (var item in hitList)
            {
                var lookupItem = _itemsRepository.Query().FirstOrDefault(i => i.Code == item.ItemCode || i.OldCode == item.ItemCode);
                if(lookupItem != null)
                {
                    item.ItemId = lookupItem.Id;
                    item.Reprocess = false;
                    results.Add(item);
                }
                
            }
            await _itemsRepository.SaveAsync();
            return results.ToArray();
        }

    }
}
