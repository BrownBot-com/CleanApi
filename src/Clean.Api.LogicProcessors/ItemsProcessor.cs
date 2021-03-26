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
                    BrandCode = request.BrandCode,
                    DiscountGroup = request.DiscountGroup,
                    PriceListGroup = request.PriceListGroup,
                    StockGroup = request.StockGroup,
                    PurchaseQty = request.PurchaseQty
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

        public async Task<Item> Update(UpdateItemRequest request, int id)
        {
            var item = Query.FirstOrDefault(i => i.Id == id);

            if(item == null) throw new NotFoundException("User is not found");

            item.FullCode = request.FullCode;
            item.FullDescription = request.FullDescription;
            item.FullType = request.FullType;
            item.SupplierCode = request.SupplierCode;
            item.DiscountGroup = request.DiscountGroup;
            item.PriceListGroup = request.PriceListGroup;
            item.StockGroup = request.StockGroup;
            item.PurchaseQty = request.PurchaseQty;
            item.IsSoldInPacket = request.IsSoldInPacket;
            await _itemsRepository.SaveAsync();

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


        public async Task Delete(int id)
        {
            var item = Query.FirstOrDefault(i => i.Id == id);
            if (item == null) throw new NotFoundException("Item not found");
            _itemsRepository.Remove(item);
            await _itemsRepository.SaveAsync();
        }

    }
}
