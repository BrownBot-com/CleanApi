using AutoMapper;
using AutoMapper.QueryableExtensions;
using Clean.Api.Contracts.Users;
using Clean.Api.DataAccess.Models;
using Clean.Api.Filters;
using Clean.Api.LogicProcessors.Interfaces;
using Clean.Api.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clean.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public UsersController(IUsersProcessor usersProcessor, IMapper mapper)
        {
            _usersProcessor = usersProcessor;
            _mapper = mapper;
        }

        private readonly IUsersProcessor _usersProcessor;
        private readonly IMapper _mapper;

        // GET: api/<UsersController>
        [HttpGet]
        [QueryableList]
        public IQueryable<UserResponse> Get()
        {
            var rawResult = _usersProcessor.Query;
            var response = rawResult.ProjectTo<UserResponse>(_mapper.ConfigurationProvider);
            return response;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public ActionResult<UserResponse> Get(int id)
        {
            var rawResult = _usersProcessor.Get(id);
            var response = _mapper.Map<UserResponse>(rawResult);
            return response;
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<UserResponse>> Post([FromBody] CreateUserRequest request)
        {
            var rawResult = await _usersProcessor.Create(request);
            var response = _mapper.Map<UserResponse>(rawResult);
            return response;
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponse>> Put(int id, [FromBody] UpdateUserRequest request)
        {
            var rawResult = await _usersProcessor.Update(id, request);
            var response = _mapper.Map<UserResponse>(rawResult);
            return response;
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _usersProcessor.Delete(id);
            return Ok();
        }
    }
}
