namespace Forecastly.Application.Location;

public interface ILocationService
{
    Task<List<Domain.Location.Location>> FindLocationDataAsync(string city, CancellationToken cancellationToken = default);
}