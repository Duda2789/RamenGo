using System;
using Microsoft.AspNetCore.Mvc;
using RamenGoAPI.Models;
using Newtonsoft.Json;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public OrdersController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://api.tech.redventures.com.br/");
        _httpClient.DefaultRequestHeaders.Add("x-api-key", "ZtVdh8XQ2U8pWI2gmZ7f796Vh8GllXoN7mr0djNf");
    }

    [HttpPost("generate-id")]
    public async Task<ActionResult<string>> GenerateOrderId()
    {
        var response = await _httpClient.PostAsync("orders/generate-id", null);
        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode, new { error = "Failed to generate order ID" });
        }

        var orderId = await response.Content.ReadAsStringAsync();
        return Ok(orderId);
    }
}
