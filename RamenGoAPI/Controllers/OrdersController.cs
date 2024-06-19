using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public OrdersController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://api.tech.redventures.com.br/");
        _configuration = configuration;
    }

    [HttpPost("generate-id")]
    public async Task<ActionResult<string>> GenerateOrderId()
    {
        string apiKey = _configuration["API_KEY"];
        _httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);

        var response = await _httpClient.PostAsync("orders/generate-id", null);
        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode, new { error = "Erro ao criar o pedido!" });
        }

        var orderId = await response.Content.ReadAsStringAsync();
        return Ok(orderId);
    }
}
