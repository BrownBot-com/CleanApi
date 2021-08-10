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
    public class ItemDiscountGroupsController : ControllerBase
    {
        public ItemDiscountGroupsController(IItemDiscountGroupsProcessor processor, IMapper mapper)
        {
            _processor = processor;
            _mapper = mapper;
        }

        private readonly IItemDiscountGroupsProcessor _processor;
        private readonly IMapper _mapper;

        // GET: api/<ItemDiscountGroupsController>
        [HttpGet]
        [QueryableList]
        public IQueryable<ItemDiscountGroupResponse> Get()
        {
            var rawResult = _processor.Query;
            var response = rawResult.ProjectTo<ItemDiscountGroupResponse>(_mapper.ConfigurationProvider);
            return response;
        }

        // GET api/<ItemDiscountGroupsController>/5
        [HttpGet("{code}")]
        public ActionResult<ItemDiscountGroupResponse> Get(string code)
        {
            var rawResult = _processor.Get(code);
            var response = _mapper.Map<ItemDiscountGroupResponse>(rawResult);
            return response;
        }

        // POST api/<ItemDiscountGroupsController>
        [HttpPost]
        public async Task<ActionResult<ItemDiscountGroupResponse[]>> Post([FromBody] CreateItemDiscountGroupRequest[] request)
        {
            var rawResult = await _processor.Create(request);
            var response = _mapper.Map<ItemDiscountGroupResponse[]>(rawResult);
            return response;
        }

        // PUT api/<ItemDiscountGroupsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ItemDiscountGroupResponse>> Put(string code, [FromBody] UpdateItemDiscountGroupRequest request)
        {
            var rawResult = await _processor.Update(request, code);
            var response = _mapper.Map<ItemDiscountGroupResponse>(rawResult);
            return response;
        }

        // DELETE api/<ItemDiscountGroupsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string code)
        {
            await _processor.Delete(code);
            return Ok();
        }
    }
}
