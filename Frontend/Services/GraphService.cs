using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Forward.Services
{
    public class GraphService : IGraphService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };

        public GraphService(HttpClient httpClient) {
            _httpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
        }

        public async Task<List<int>> GetPathFromBFS(int startPoint, int endPoint) {
            var result = await JsonSerializer.DeserializeAsync<List<int>>(await _httpClient.GetStreamAsync($"api/graph/BFS/{startPoint}/{endPoint}"), _options);
            return result;
        }
        public async Task<List<int>> GetPathFromDFS(int startPoint, int endPoint) {
            var response = await _httpClient.GetStreamAsync($"api/graph/DFS/{startPoint}/{endPoint}");
            var result = await JsonSerializer.DeserializeAsync<List<int>>(response);
            return result;
        }
    }
}
