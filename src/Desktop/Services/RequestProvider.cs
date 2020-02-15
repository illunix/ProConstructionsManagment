using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ProConstructionsManagment.Desktop.Services
{
    public class RequestProvider : IRequestProvider
    {
        public async Task<TResult> GetAsync<TResult>(string uri, string token = "")
        {
            var client = CreateHttpClient(token);
            var response = await client.GetAsync(uri);

            var serialized = await response.Content.ReadAsStringAsync();

            var result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized));

            return result;
        }

        public async Task<TResult> PostAsync<TResult>(string uri, TResult data, string token = "", string header = "")
        {
            var httpClient = CreateHttpClient(token);

            if (!string.IsNullOrEmpty(header))
                AddHeaderParameter(httpClient, header);

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await httpClient.PostAsync(uri, content);

            var serialized = await response.Content.ReadAsStringAsync();

            var result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized));

            return result;
        }

        private HttpClient CreateHttpClient(string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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