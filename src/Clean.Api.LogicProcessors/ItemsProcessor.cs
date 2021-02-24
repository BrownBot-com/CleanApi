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
    public class ItemsProcessor : IItemsProcessor
    {
        public ItemsProcessor(IRepository<Item> itemsRepository, ISecurityContext securityContext)
        {
            _itemsRepository = itemsRepository;
            _securityContext = securityContext;
        }

        private IRepository<Item> _itemsRepository;
        private ISecurityContext _securityContext;

        public IQueryable<Item> Query => _itemsRepository.Query()
                                            .Include(i => i.Stock);

        public async Task<Item[]> Create(CreateItemRequest[] requests)
        {
            var result = new Dictionary<string,Item>();
            foreach (var request in requests)
            {
                var itemCode = request.Code.Trim().ToUpper();

                if (result.ContainsKey(itemCode)) continue;
                //if (_itemsRepository.Query().Any(u => u.Code == itemCode)) throw new BadRequestException($"Item code [{itemCode}] is already in use");

                var item = new Item
                {
                    FullCode = itemCode,
                    OldCode = request.OldCode.Trim().ToUpper(),
                    Description = ParseFullDescription(request.FullDescription),
                    FullDescription = request.FullDescription,
                    FullType = request.FullType,
                    SupplierCode = request.SupplierCode,
                    BrandCode = request.BrandCode
                };

                if(itemCode.Length > 20)
                {
                    item.Code = itemCode.Substring(0, 20);
                    item.Errors += $"Code too long [{itemCode.Length}]";
                }
                else
                {
                    item.Code = itemCode;
                }

                _itemsRepository.Add(item);
                result.Add(item.Code,item);
            }
            await _itemsRepository.SaveAsync();

            return result.Values.ToArray();
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
            foreach (var request in requests)
            {
                var itemCode = request.ItemCode.Trim().ToUpper();
                if (lookupItem == null || lookupItem.Code != itemCode)
                {
                    lookupItem = _itemsRepository.Query().FirstOrDefault(i => i.Code == itemCode);
                    if (lookupItem == null) throw new NotFoundException($"ItemCode [{itemCode}] not found");
                }
                var itemStock = new ItemStock
                {
                    BranchCode = request.BranchCode.Trim(),
                    Current = request.Current,
                    ItemId = lookupItem.Id,
                    LastOrdered = request.LastOrdered,
                    Max = request.Max,
                    Min = request.Min
                };

                _itemsRepository.Add(itemStock);
                result.Add(itemStock);
            }
            await _itemsRepository.SaveAsync();

            return result.ToArray();
        }

        public async Task Delete(int id)
        {
            var item = Query.FirstOrDefault(i => i.Id == id);
            if (item == null) throw new NotFoundException("Item not found");
            _itemsRepository.Remove(item);
            await _itemsRepository.SaveAsync();
        }

        public Item Get(int id)
        {
            var item = Query.FirstOrDefault(i => i.Id == id);
            if (item == null) throw new NotFoundException("Item not found");
            return item;
        }

        public Item Get(string code)
        {
            var item = Query.FirstOrDefault(i => i.Code == code);
            if (item == null) throw new NotFoundException("Item not found");
            return item;
        }
    }
}
