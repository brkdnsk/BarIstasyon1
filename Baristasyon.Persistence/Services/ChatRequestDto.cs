using Baristasyon.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Baristasyon.Persistence.Services
{
    public class ChatService : IChatService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public ChatService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["OpenAI:ApiKey"] ?? throw new InvalidOperationException("OpenAI API anahtarı bulunamadı.");
        }

        public async Task<string> AskQuestionAsync(string question)
        {
            var request = new
            {
                model = "gpt-3.5-turbo",
                messages = new[] {
                    new { role = "system", content = "Sen kahve uzmanı bir asistansın." },
                    new { role = "user", content = question }
                }
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(result);
            return doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
        }
    }
}
