using AutoMapper;
using AutoMapper.QueryableExtensions;
using Clean.Api.Contracts.Brands;
using Clean.Api.Contracts.Items;
using Clean.Api.Filters;
using Clean.Api.LogicProcessors.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Clean.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        public BrandsController(IBrandsProcessor brandsProcessor, IMapper mapper)
        {
            _brandsProcessor = brandsProcessor;
            _mapper = mapper;
        }

        private readonly IBrandsProcessor _brandsProcessor;
        private readonly IMapper _mapper;

        [HttpGet]
        [QueryableList]
        public IQueryable<BrandResponse> Get()
        {
            var rawResult = _brandsProcessor.Query;
            var response = rawResult.ProjectTo<BrandResponse>(_mapper.ConfigurationProvider);
            return response;
        }

        [HttpGet("{code}")]
        public ActionResult<BrandResponse> Get(string code)
        {
            var rawResult = _brandsProcessor.Get(code);
            var response = _mapper.Map<BrandResponse>(rawResult);
            return response;
        }

        [HttpPost]
        public async Task<ActionResult<BrandResponse>> Post([FromBody] CreateBrandRequest request)
        {
            var rawResult = await _brandsProcessor.Create(request);
            var response = _mapper.Map<BrandResponse>(rawResult);
            return response;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BrandResponse>> Put(string code, [FromBody] UpdateBrandRequest request)
        {
            var rawResult = await _brandsProcessor.Update(request, code);
            var response = _mapper.Map<BrandResponse>(rawResult);
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string code)
        {
            await _brandsProcessor.Delete(code);
            return Ok();
        }
    }
}
