﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
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

        // PUT api/<ItemsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ItemResponse>> Put(int id, [FromBody] UpdateItemRequest request)
        {
            var rawResult = await _itemsProcessor.Update(request, id);
            var response = _mapper.Map<ItemResponse>(rawResult);
            return response;
        }

        // DELETE api/<ItemsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _itemsProcessor.Delete(id);
            return Ok();
        }
    }
}
