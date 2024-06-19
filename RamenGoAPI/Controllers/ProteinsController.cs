using Microsoft.AspNetCore.Mvc;

namespace RamenGoAPI.Controllers
{
    // Controlador para proteínas
    [ApiController]
    [Route("api/proteins")]
    public class ProteinsController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ProteinsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://api.tech.redventures.com.br/");
            _httpClient.DefaultRequestHeaders.Add("x-api-key", "ZtVdh8XQ2U8pWI2gmZ7f796Vh8GllXoN7mr0djNf");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Protein>>> GetProteins()
        {
            var response = await _httpClient.GetAsync("proteins");
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, new { error = "Failed to retrieve proteins" });
            }

            var proteins = await response.Content.ReadAsAsync<IEnumerable<Protein>>();
            return Ok(proteins);
        }
    }

}
