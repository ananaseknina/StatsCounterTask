using StatsCounter.Models;
using System.Threading.Tasks;

namespace StatsCounter.Contracts
{
    namespace StatsCounter.Contracts
    {
        public interface IStatsService
        {
            Task<RepositoryStats> GetRepositoryStatsByOwnerAsync(string owner);
        }
    }
}
