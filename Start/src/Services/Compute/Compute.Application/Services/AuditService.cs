using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Compute.Application.Services
{
    public class AuditService : IAuditService
    {
        private readonly HttpClient _httpClient;
        private readonly string _uri;

        //A Typed Client is, effectively, a transient object
        //meaning that a new instance is created each time one is needed
        //and it will receive a new HttpClient instance each time it's constructed. 
        public AuditService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _uri = configuration["AuditServiceUri"] + "/api/v1/audit";
        }

        public async Task SubmitForAuditAsync(double number)
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(number), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_uri, httpContent);
        }
    }
}
