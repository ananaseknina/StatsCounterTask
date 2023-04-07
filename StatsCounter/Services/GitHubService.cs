using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Octokit;
using StatsCounter.Contracts;
using StatsCounter.Models;

namespace StatsCounter.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly HttpClient _httpClient;

        public GitHubService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        public async Task<IEnumerable<RepositoryInfo>> GetRepositoryInfosByOwnerAsync(string owner)
        {
            string accessToken = "";
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var data = (await _httpClient.GetStringAsync($"https://api.github.com/users/{owner}/repos"));
            var repositoryInfos = JsonSerializer.Deserialize<List<RepositoryInfo>>(data);
            return repositoryInfos;
        }
    }
}
