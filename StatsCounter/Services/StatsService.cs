using System.Collections.Generic;
using System.Threading.Tasks;
using StatsCounter.Contracts;
using StatsCounter.Contracts.StatsCounter.Contracts;
using StatsCounter.Models;

namespace StatsCounter.Services
{
    public class StatsService : IStatsService
    {
        private readonly IGitHubService _gitHubService;

        public StatsService(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        public async Task<RepositoryStats> GetRepositoryStatsByOwnerAsync(string owner)
        {
            var repositoryInfos = await _gitHubService.GetRepositoryInfosByOwnerAsync(owner);
            var lettersDict = new Dictionary<char, int>();

            CountAverageValuesAndLetters(
            repositoryInfos,
            lettersDict,
            out long avgStargazers,
            out long avgWatchers,
            out long avgForks,
            out long avgSize);

            var repositoryStats = new RepositoryStats()
            {
                Owner = owner,
                AvgForks = avgForks,
                AvgSize = avgSize,
                AvgStargazers = avgStargazers,
                AvgWatchers = avgWatchers,
                Letters = lettersDict
            };

            return repositoryStats;
        }
        public void CountAverageValuesAndLetters(
            IEnumerable<RepositoryInfo> repoInfos,
            IDictionary<char, int> lettersDict,
            out long avgStargazers,
            out long avgWatchers,
            out long avgForks,
            out long avgSize)
        {
            avgStargazers = 0;
            avgWatchers = 0;
            avgForks = 0;
            avgSize = 0;
            int repoCount = 0;

            foreach (RepositoryInfo repoInfo in repoInfos)
            {
                repoCount += 1;
                avgStargazers += repoInfo.StargazersCount;
                avgWatchers += repoInfo.WatchersCount;
                avgForks += repoInfo.ForksCount;
                avgSize += repoInfo.Size;

                var name = repoInfo.Name.ToLowerInvariant();

                foreach (char letter in name)
                {
                    if (char.IsLetter(letter))
                    {
                        if (lettersDict.ContainsKey(letter))
                        {
                            lettersDict[letter] = lettersDict[letter] + 1;
                        }
                        else
                        {
                            lettersDict.Add(letter, 1);
                        }
                    }
                }
            }

            avgStargazers = avgStargazers / repoCount; //result will be rounded, makes no sense to have ex. 1,5 watchers in avg
            avgWatchers = avgWatchers / repoCount;
            avgForks = avgForks / repoCount;
            avgSize = avgSize / repoCount;
        }
    }
}