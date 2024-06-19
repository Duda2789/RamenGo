using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace RamenGoAPI.Controllers
{

    [ApiController]
    [Route("api/proteins")]
    public class ProteinsController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ProteinsController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://api.tech.redventures.com.br/");
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Protein>>> GetProteins()
        {
            string apiKey = _configuration["API_KEY"];
            _httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);

            var response = await _httpClient.GetAsync("proteins");
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, new { error = "Erro ao encontrar as proteínas!" });
            }

            var proteins = await response.Content.ReadAsAsync<IEnumerable<Protein>>();
            return Ok(proteins);
        }
    }
}
