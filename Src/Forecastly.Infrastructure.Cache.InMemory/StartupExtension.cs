using Forecastly.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Forecastly.Infrastructure.Cache.InMemory;

public static class StartupExtension
{
    public static IServiceCollection AddInMemoryCache(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<ICacheService, InMemoryCacheService>();
        return services;
    }
}