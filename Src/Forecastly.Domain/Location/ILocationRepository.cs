namespace Forecastly.Domain.Location;

public interface ILocationRepository
{
    Task<List<Location>> GetLocationDataAsync(string city, CancellationToken cancellationToken = default);
}