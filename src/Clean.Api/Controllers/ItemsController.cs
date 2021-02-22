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
    public class ItemsController : ControllerBase
    {
        public ItemsController(IItemsProcessor itemsProcessor, IMapper mapper)
        {
            _itemsProcessor = itemsProcessor;
            _mapper = mapper;
        }

        private readonly IItemsProcessor _itemsProcessor;
        private readonly IMapper _mapper;

        // GET: api/<ItemsController>
        [HttpGet]
        [QueryableList]
        public IQueryable<ItemResponse> Get()
        {
            var rawResult = _itemsProcessor.Query;
            var response = rawResult.ProjectTo<ItemResponse>(_mapper.ConfigurationProvider);
            return response;
        }

        // GET api/<ItemsController>/5
        [HttpGet("{id}")]
        public ActionResult<ItemResponse> Get(int id)
        {
            var rawResult = _itemsProcessor.Get(id);
            var response = _mapper.Map<ItemResponse>(rawResult);
            return response;
        }

        // POST api/<ItemsController>
        [HttpPost]
        public async Task<ActionResult<ItemResponse[]>> Post([FromBody] CreateItemRequest[] request)
        {
            var rawResult = await _itemsProcessor.Create(request);
            var response = _mapper.Map<ItemResponse[]>(rawResult);
            return response;
        }

        [HttpPost("Stock")]
        public async Task<ActionResult<ItemStockResponse[]>> PostStock([FromBody] CreateItemStockRequest[] request)
        {
            var rawResult = await _itemsProcessor.Create(request);
            var response = _mapper.Map<ItemStockResponse[]>(rawResult);
            return response;
        }

        //// PUT api/<ItemsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<ItemsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _itemsProcessor.Delete(id);
            return Ok();
        }
    }
}
