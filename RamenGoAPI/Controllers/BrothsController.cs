using Microsoft.AspNetCore.Mvc;
using System.Net.Http;


namespace RamenGoAPI.Controllers
{
    // Controlador para caldos
    [ApiController]
    [Route("api/broths")]
    public class BrothsController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public BrothsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://api.tech.redventures.com.br/");
            _httpClient.DefaultRequestHeaders.Add("x-api-key", "ZtVdh8XQ2U8pWI2gmZ7f796Vh8GllXoN7mr0djNf");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Broth>>> GetBroths()
        {
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
