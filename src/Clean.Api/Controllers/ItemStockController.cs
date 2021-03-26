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
    public class ItemStockController : ControllerBase
    {
        public ItemStockController(IItemStockProcessor itemStockProcessor, IMapper mapper)
        {
            _itemStockProcessor = itemStockProcessor;
            _mapper = mapper;
        }

        private readonly IItemStockProcessor _itemStockProcessor;
        private readonly IMapper _mapper;

        // GET: api/<ItemsController>
        [HttpGet]
        [QueryableList]
        public IQueryable<ItemStockResponse> Get()
        {
            var rawResult = _itemStockProcessor.Query;
            var response = rawResult.ProjectTo<ItemStockResponse>(_mapper.ConfigurationProvider);
            return response;
        }

        // GET api/<ItemsController>/5
        [HttpGet("{id}")]
        public ActionResult<ItemStockResponse> Get(int id)
        {
            var rawResult = _itemStockProcessor.Get(id);
            var response = _mapper.Map<ItemStockResponse>(rawResult);
            return response;
        }


        [HttpPost]
        public async Task<ActionResult<ItemStockResponse[]>> PostStock([FromBody] CreateItemStockRequest[] request)
        {
            var rawResult = await _itemStockProcessor.Create(request);
            var response = _mapper.Map<ItemStockResponse[]>(rawResult);
            return response;
        }


        [HttpPost("Reprocess")]
        public async Task<ActionResult<ItemStockResponse[]>> ReprocessStock()
        {
            var rawResult = await _itemStockProcessor.Reprocess();
            var response = _mapper.Map<ItemStockResponse[]>(rawResult);
            return response;
        }


        // PUT api/<ItemsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ItemStockResponse>> Put(int id, [FromBody] UpdateItemStockRequest request)
        {
            var rawResult = await _itemStockProcessor.Update(request, id);
            var response = _mapper.Map<ItemStockResponse>(rawResult);
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
