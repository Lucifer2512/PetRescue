    using System.Text;
    using System.Text.Json;

    namespace PetRescueFE
    {
        public class ApiService
        {
            private readonly HttpClient _httpClient;

            public ApiService(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            // GET request
            public async Task<T?> GetAsync<T>(string url)
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var responseData = await response.Content.ReadAsStringAsync();
                var a = JsonSerializer.Deserialize<T>(responseData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return JsonSerializer.Deserialize<T>(responseData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            // POST request
            public async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest data)
            {
                var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();

                var responseData = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TResponse>(responseData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            // PUT request
            public async Task<TResponse?> PutAsync<TRequest, TResponse>(string url, TRequest data)
            {
                var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync(url, content);
                response.EnsureSuccessStatusCode();

                var responseData = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TResponse>(responseData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            // DELETE request
            public async Task<bool> DeleteAsync(string url)
            {
                var response = await _httpClient.DeleteAsync(url);
                return response.IsSuccessStatusCode;
            }
        }
    }
