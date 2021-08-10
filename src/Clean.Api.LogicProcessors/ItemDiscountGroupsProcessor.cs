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
    public class ItemDiscountGroupsProcessor : IItemDiscountGroupsProcessor
    {
        public ItemDiscountGroupsProcessor(IRepository<ItemDiscountGroup> itemDiscountGroupsRepository, ISecurityContext securityContext)
        {
            _repository = itemDiscountGroupsRepository;
            _securityContext = securityContext;
        }

        private IRepository<ItemDiscountGroup> _repository;
        private ISecurityContext _securityContext;

        public IQueryable<ItemDiscountGroup> Query => _repository.Query();

        public ItemDiscountGroup Get(string code)
        {
            var item = Query.FirstOrDefault(i => i.Code == code);
            if (item == null) throw new NotFoundException("ItemDiscountGroup not found");
            return item;
        }

        public async Task<ItemDiscountGroup[]> Create(CreateItemDiscountGroupRequest[] requests)
        {
            var result = new List<ItemDiscountGroup>();

            foreach (var request in requests)
            {
                var groupCode = request.Code.Trim().ToUpper();

                //if (_repository.Query().Any(u => u.Code == groupCode)) throw new BadRequestException($"ItemDiscountGroup code [{groupCode}] is already in use");

                var newItemDiscountGroup = new ItemDiscountGroup
                {
                    Code = groupCode,
                    Description = request.Description,
                    Additional = request.Additional,
                    SupplierRef = request.SupplierRef
                };

                _repository.Add(newItemDiscountGroup);
                result.Add(newItemDiscountGroup);
            }
            await _repository.SaveAsync();

            return result.ToArray();
        }

        public async Task<ItemDiscountGroup> Update(UpdateItemDiscountGroupRequest request, string code)
        {
            var item = Query.FirstOrDefault(i => i.Code == code);

            if (item == null) throw new NotFoundException($"ItemDiscountGroup [{code}] not found");

            item.Code = request.Code;
            item.Description = request.Description;
            item.Additional = request.Additional;
            item.SupplierRef = request.SupplierRef;
            await _repository.SaveAsync();

            return item;
        }
        private string CleanItemDiscountGroupCode(string code)
        {
            return code.Trim().ToUpper().Replace(" ", string.Empty);
        }

        public async Task Delete(string code)
        {
            var item = Query.FirstOrDefault(i => i.Code == code);
            if (item == null) throw new NotFoundException($"ItemDiscountGroup [{code}] not found");
            _repository.Remove(item);
            await _repository.SaveAsync();
        }

    }
}
