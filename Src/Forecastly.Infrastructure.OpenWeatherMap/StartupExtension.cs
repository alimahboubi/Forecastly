using Forecastly.Domain.Location;
using Forecastly.Domain.Weather;
using Forecastly.Infrastructure.OpenWeatherMap.Models;
using Forecastly.Infrastructure.OpenWeatherMap.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Forecastly.Infrastructure.OpenWeatherMap;

public static class StartupExtension
{
    public static IServiceCollection AddOpenWeatherMap(this IServiceCollection services,
        OpenWeatherMapConfiguration configuration)
    {
        services.AddWeatherHttpClient(configuration)
            .AddLocationHttpClient(configuration);
        return services;
    }

    private static IServiceCollection AddWeatherHttpClient(this IServiceCollection services,
        OpenWeatherMapConfiguration configuration)
    {
        services.AddHttpClient<IWeatherRepository, WeatherRepository>((sp, client) =>
        {
            client.BaseAddress = new Uri($"{configuration.BaseAddress}/data/3.0/onecall");
        });
        return services;
    }

    private static IServiceCollection AddLocationHttpClient(this IServiceCollection services,
        OpenWeatherMapConfiguration configuration)
    {
        services.AddHttpClient<ILocationRepository, LocationRepository>((sp, client) =>
        {
            client.BaseAddress = new Uri($"{configuration.BaseAddress}/geo/1.0/");
        });
        return services;
    }
}