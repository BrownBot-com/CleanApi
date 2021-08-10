using Clean.Api.Common.Exceptions;
using Clean.Api.Contracts.Brands;
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
    public class BrandsProcessor : IBrandsProcessor
    {
        public BrandsProcessor(IRepository<Brand> brandsRepository, ISecurityContext securityContext)
        {
            _brandsRepository = brandsRepository;
            _securityContext = securityContext;
        }

        private IRepository<Brand> _brandsRepository;
        private ISecurityContext _securityContext;

        public IQueryable<Brand> Query => _brandsRepository.Query();
                                            //.Include(i => i.Stock);

        public Brand Get(string code)
        {
            var item = Query.FirstOrDefault(i => i.Code == code);
            if (item == null) throw new NotFoundException("Brand not found");
            return item;
        }

        public async Task<Brand> Create(CreateBrandRequest request)
        {
            var result = new Dictionary<string, Brand>();

            var brandCode = request.Code.Trim().ToUpper();

            if (_brandsRepository.Query().Any(u => u.Code == brandCode)) throw new BadRequestException($"Brand code [{brandCode}] is already in use");

            var newBrand = new Brand
            {
                Code = brandCode,
                Name = request.Name,
                DynamicsCode = request.DynamicsCode
            };

            _brandsRepository.Add(newBrand);

            await _brandsRepository.SaveAsync();

            return newBrand;
        }

        public async Task<Brand> Update(UpdateBrandRequest request, string code)
        {
            var item = Query.FirstOrDefault(i => i.Code == code);

            if (item == null) throw new NotFoundException($"Brand [{code}] not found");

            item.Name = request.Name;
            item.DynamicsCode = request.DynamicsCode;
            await _brandsRepository.SaveAsync();

            return item;
        }
        private string CleanBrandCode(string code)
        {
            return code.Trim().ToUpper().Replace(" ", string.Empty);
        }

        public async Task Delete(string code)
        {
            var item = Query.FirstOrDefault(i => i.Code == code);
            if (item == null) throw new NotFoundException($"Brand [{code}] not found");
            _brandsRepository.Remove(item);
            await _brandsRepository.SaveAsync();
        }

    }
}
