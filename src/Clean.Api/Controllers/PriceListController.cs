using AutoMapper;
using AutoMapper.QueryableExtensions;
using Clean.Api.Contracts.Items;
using Clean.Api.Filters;
using Clean.Api.LogicProcessors.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clean.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceListController : ControllerBase
    {
        public PriceListController(IPriceListProcessor itemStockProcessor, IMapper mapper)
        {
            _itemStockProcessor = itemStockProcessor;
            _mapper = mapper;
        }

        private readonly IPriceListProcessor _itemStockProcessor;
        private readonly IMapper _mapper;

        // GET: api/<ItemsController>
        [HttpGet]
        [QueryableList]
        public IQueryable<PriceListResponse> Get()
        {
            var rawResult = _itemStockProcessor.Query;
            var response = rawResult.ProjectTo<PriceListResponse>(_mapper.ConfigurationProvider);
            return response;
        }

        // GET api/<ItemsController>/5
        [HttpGet("{id}")]
        public ActionResult<PriceListResponse> Get(int id)
        {
            var rawResult = _itemStockProcessor.Get(id);
            var response = _mapper.Map<PriceListResponse>(rawResult);
            return response;
        }


        [HttpPost]
        public async Task<ActionResult<PriceListResponse>> PostStock([FromBody] CreatePriceListRequest request)
        {
            var rawResult = await _itemStockProcessor.Create(request);
            var response = _mapper.Map<PriceListResponse>(rawResult);
            return response;
        }


        [HttpPost("Reprocess")]
        public async Task<ActionResult<ItemPriceResponse[]>> LinkPriceListItems()
        {
            var rawResult = await _itemStockProcessor.LinkPriceListItems();
            var response = _mapper.Map<ItemPriceResponse[]>(rawResult);
            return response;
        }

        // DELETE api/<ItemsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _itemStockProcessor.Delete(id);
            return Ok();
        }
    }
}
