using AutoMapper;
using Clean.Api.Contracts.AbnLookup;
using Clean.Api.Services.Interfaces;
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
    public class AbnLookupController : ControllerBase
    {
        public AbnLookupController(IAbnLookupService abnService, IMapper mapper)
        {
            _abnService = abnService;
            _mapper = mapper;
        }

        private readonly IAbnLookupService _abnService;
        private readonly IMapper _mapper;

        [HttpPost]
        public async Task<ActionResult<AbnLookupResponse>> Post([FromBody] AbnLookupRequest request)
        {
            var response = new AbnLookupResponse();

            foreach (var abn in request.Abns)
            {
                var abnResult = await _abnService.LookupAbn(abn);
                if(abnResult == null || abnResult.Abn.Length == 0)
                {
                    response.Results.Add(new AbnLookupResult() { Abn = abn, IsValid = false });
                }
                else
                {
                    var result = _mapper.Map<AbnLookupResult>(abnResult);
                    result.IsValid = true;
                    response.Results.Add(result);
                }
            }
            return response;
        }
    }
}
