using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace RamenGoAPI.Controllers
{
    // Controlador para caldos
    [ApiController]
    [Route("api/broths")]
    public class BrothsController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public BrothsController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://api.tech.redventures.com.br/");
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Broth>>> GetBroths()
        {
            string apiKey = _configuration["API_KEY"];
            _httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);

            var response = await _httpClient.GetAsync("broths");
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, new { error = "Failed to retrieve broths" });
            }

            var broths = await response.Content.ReadAsAsync<IEnumerable<Broth>>();
            return Ok(broths);
        }
    }
}
