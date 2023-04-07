using StatsCounter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StatsCounter.Contracts
{
    public interface IGitHubService
    {
        Task<IEnumerable<RepositoryInfo>> GetRepositoryInfosByOwnerAsync(string owner);
    }
}
