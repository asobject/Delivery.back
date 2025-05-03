

using Domain.Interfaces;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Infrastructure.Services;

public class OllamaResponseGenerator(
    IHttpClientFactory httpClientFactory,
    IOptions<OllamaSettings> options) : IAiResponseGenerator
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Ollama");
    private readonly OllamaSettings _settings = options.Value;

    public async Task<string> GenerateResponseAsync(string prompt)
    {
        var request = new
        {
            model = _settings.Model,
            prompt,
            stream = false
        };

        var response = await _httpClient.PostAsJsonAsync(
            "/api/generate",
            request
        );

        var result = await response.Content
            .ReadFromJsonAsync<OllamaResponse>();

        return result?.Response ?? string.Empty;
    }

    private record OllamaResponse(string Response);
}