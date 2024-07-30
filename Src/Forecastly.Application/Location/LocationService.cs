using Forecastly.Domain.Location;

namespace Forecastly.Application.Location;

public class LocationService(ILocationRepository locationRepository) : ILocationService
{
    public async Task<List<Domain.Location.Location>> FindLocationDataAsync(string city,
        CancellationToken cancellationToken = default)
    {
        return await locationRepository.GetLocationDataAsync(city, cancellationToken);
    }
}