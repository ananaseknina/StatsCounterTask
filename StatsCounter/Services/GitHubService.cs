using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
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
            var repositoryInfos = new List<RepositoryInfo>();
            try
            {
                var url = $"https://api.github.com/users/{owner}/repos";
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                var productValue = new ProductInfoHeaderValue("StatsCounter", "1.0");
                var commentValue = new ProductInfoHeaderValue("(+http://www.API.com/StatsCounter.html)");

                request.Headers.UserAgent.Add(productValue);
                request.Headers.UserAgent.Add(commentValue);

                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                var content = (await _httpClient.SendAsync(request)).Content;
                var data = await content.ReadAsStringAsync();
                repositoryInfos = JsonSerializer.Deserialize<List<RepositoryInfo>>(data);
            }
            catch(Exception e) 
            {
                throw new ArgumentException("skksskks"+e.Message);
            }
            
            return repositoryInfos;
        }
    }
}
