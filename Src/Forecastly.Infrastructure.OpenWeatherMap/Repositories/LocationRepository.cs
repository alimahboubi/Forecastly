using System.Net.Http.Json;
using Forecastly.Domain.Location;
using Forecastly.Infrastructure.OpenWeatherMap.Models;

namespace Forecastly.Infrastructure.OpenWeatherMap.Repositories;

public class LocationRepository(HttpClient httpClient, OpenWeatherMapConfiguration configuration)
    : ILocationRepository
{
    public async Task<List<Location>> GetLocationDataAsync(string city, CancellationToken cancellationToken)
    {
        var query = $"direct?q={city}&limit=5&appid={configuration.ApiKey}";
        var locationResponse = await httpClient.GetFromJsonAsync<List<GeocodingResponse>>(query, cancellationToken);
        return locationResponse?.Select(e => new Location
        {
            Name = e.Name,
            Country = e.Country,
            Lat = e.Lat,
            Long = e.Lon
        }).ToList() ?? new List<Location>();
    }
}