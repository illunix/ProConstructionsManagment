using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProConstructionsManagment.Desktop.Services
{
    public class RequestProvider : IRequestProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RequestProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TResult> GetAsync<TResult>(string uri, string token = "")
        {
            var client = CreateHttpClient(token);
            var response = await client.GetAsync(uri);

            var serialized = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};

            var result = await Task.Run(() =>
                JsonSerializer.Deserialize<TResult>(serialized, options));

            return result;
        }

        public async Task<TResult> PostAsync<TResult>(string uri, TResult data, string token = "", string header = "")
        {
            var httpClient = CreateHttpClient(token);

            if (!string.IsNullOrEmpty(header))
                AddHeaderParameter(httpClient, header);

            var content = new StringContent(JsonSerializer.Serialize(data));
            var response = await httpClient.PostAsync(uri, content);

            var serialized = await response.Content.ReadAsStringAsync();

            var result = await Task.Run(() =>
                JsonSerializer.Deserialize<TResult>(serialized));

            return result;
        }

        private HttpClient CreateHttpClient(string token)
        {
            var client = _httpClientFactory.CreateClient();

            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }

        private void AddHeaderParameter(HttpClient httpClient, string parameter)
        {
            if (httpClient == null)
                return;

            if (string.IsNullOrEmpty(parameter))
                return;

            httpClient.DefaultRequestHeaders.Add(parameter, Guid.NewGuid().ToString());
        }
    }
}