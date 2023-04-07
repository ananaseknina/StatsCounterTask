using System;
using Microsoft.Extensions.DependencyInjection;
using StatsCounter.Contracts;
using StatsCounter.Contracts.StatsCounter.Contracts;
using StatsCounter.Services;

namespace StatsCounter.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddGitHubService(
            this IServiceCollection services,
            Uri baseApiUrl)
        {
            services.AddHttpClient();
            services.AddScoped<IGitHubService, GitHubService>();
            services.AddScoped<IStatsService, StatsService>();
            return services;
        }
    }
}