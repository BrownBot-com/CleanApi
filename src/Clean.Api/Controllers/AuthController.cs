using AutoMapper;
using Clean.Api.Contracts.Authentication;
using Clean.Api.Contracts.Users;
using Clean.Api.LogicProcessors.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(IAuthenticationProcessor processor, IMapper mapper)
        {
            _authProcessor = processor;
            _mapper = mapper;
        }

        private readonly IAuthenticationProcessor _authProcessor;
        private readonly IMapper _mapper;

        [HttpPost("Authenticate")]
        //        [ValidateModel]
        public TokenResponse Authenticate([FromBody] LoginRequest request)
        {
            return _authProcessor.Authenticate(request.Username, request.Password);
        }

        [HttpPost("Register")]
//        [ValidateModel]
        public async Task<UserResponse> Register([FromBody] RegisterRequest request)
        {
            var result = await _authProcessor.Register(request);
            var resultModel = _mapper.Map<UserResponse>(result);
            return resultModel;
        }

        [HttpPost("Password")]
        //[ValidateModel]
        [Authorize]
        public async Task ChangePassword([FromBody] ChangeUserPasswordRequest requestModel)
        {
            await _authProcessor.ChangePassword(requestModel);
        }
    }
}
