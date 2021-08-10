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
    public class PriceListProcessor : IPriceListProcessor
    {
        public PriceListProcessor(IRepository<Item> itemsRepository, ISecurityContext securityContext)
        {
            _itemsRepository = itemsRepository;
            _securityContext = securityContext;
        }

        private IRepository<Item> _itemsRepository;
        private ISecurityContext _securityContext;

        public IQueryable<PriceList> Query => _itemsRepository.Query<PriceList>().AsNoTracking();

        public PriceList Get(int id)
        {
            var item = Query.Include(p => p.Prices).FirstOrDefault(i => i.Id == id);
            if (item == null) throw new NotFoundException("Item not found");
            return item;
        }

        public async Task<PriceList> Create(CreatePriceListRequest request)
        {
            var result = new PriceList()
            {
                BrandCode = request.BrandCode,
                Date = request.Date
            };

            foreach (var price in request.Prices)
            {
                var itemCode = CleanItemCode(price.ItemCode);
                //if (lookupItem == null || lookupItem.Code != itemCode)
                //{
                //    lookupItem = _itemsRepository.Query().FirstOrDefault(i => i.Code == itemCode || i.OldCode == itemCode);
                //    //if (lookupItem == null) throw new NotFoundException($"ItemCode [{itemCode}] not found");
                //}
                var itemPrice = new ItemPrice
                {
                    Date = request.Date,
                    ItemCode = price.ItemCode,
                    Description = price.Description,
                    UnitCost = price.UnitCost,
                    UnitPrice = price.UnitPrice,
                    UnitQty = price.UnitQty,
                    PriceIncludesGST = price.PriceIncludesGST,
                    StockGroupCode = price.StockGroup
                };

                result.Prices.Add(itemPrice);
            }

            _itemsRepository.Add(result);
            try
            {
                await _itemsRepository.SaveAsync();
            }
            catch (Exception e)
            {
                throw new BadRequestException($"Exception saving price list record Message [{e.InnerException}]");
            }

            return result;
        }

        private string CleanItemCode(string code)
        {
            return code.Trim().ToUpper().Replace(" ", string.Empty);
        }

        public async Task Delete(int id)
        {
            var item = Query.FirstOrDefault(i => i.Id == id);
            if (item == null) throw new NotFoundException("Item not found");
            _itemsRepository.Remove(item);
            await _itemsRepository.SaveAsync();
        }


        public async Task<ItemPrice[]> LinkPriceListItems()
        {
            var results = new List<ItemPrice>();

            var hitList = _itemsRepository.Query<ItemPrice>().AsTracking().Where(i => i.ItemId == 0).ToArray();
            var itemsList = _itemsRepository.Query().AsNoTracking().ToDictionary(i => i.Code);
            
            foreach (var item in hitList)
            {

                Item lookupItem = null;
                if(!itemsList.TryGetValue(item.ItemCode, out lookupItem))
                {
                    lookupItem = itemsList.Values.FirstOrDefault(i => i.OldCode == item.ItemCode);
                }
                    
                if (lookupItem != null)
                {
                    item.ItemId = lookupItem.Id;
                    results.Add(item);
                }
                //else
                //{
                //    item.ItemId = 0;
                //}

            }
            await _itemsRepository.SaveAsync();
            return results.ToArray();
        }
    }
}
