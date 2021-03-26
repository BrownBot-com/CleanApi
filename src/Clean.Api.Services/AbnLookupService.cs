using Clean.Api.Services.Interfaces;
using Clean.Api.Services.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Clean.Api.Services
{
    public class AbnLookupService : IAbnLookupService
    {
        public AbnLookupService(IConfiguration config)
        {
            _config = config;
            _uriTemplate = _config["AbnService:Template"];
            _client = new HttpClient();
        }

        private readonly IConfiguration _config;

        private readonly HttpClient _client;

        private readonly string _uriTemplate;

        private const string _space = " ";

        public async Task<AbnResult> LookupAbn(string Abn)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, string.Format(_uriTemplate, Abn.Replace(_space, string.Empty)));
            var webResult = await _client.SendAsync(request);
            if(webResult.IsSuccessStatusCode)
            {
                return ExtractPayload(await webResult.Content.ReadAsStringAsync());
            }
            return null;
        }

        private AbnResult ExtractPayload(string payload)
        {
            // trim callback of the front and bracket at the end
            var trimmedPayload = payload.Substring(9);
            trimmedPayload = trimmedPayload.Substring(0, trimmedPayload.Length - 1);

            return JsonSerializer.Deserialize<AbnResult>(trimmedPayload);
        }
    }
}
