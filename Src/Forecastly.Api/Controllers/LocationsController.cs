using Forecastly.Application.Location;
using Forecastly.Application.Weather;
using Forecastly.Domain.Weather;
using Microsoft.AspNetCore.Mvc;

namespace Forecastly.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationsController(ILocationService locationService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Find([FromQuery] string city, CancellationToken cancellationToken)
    {
        var result = await locationService.FindLocationDataAsync(city, cancellationToken);
        return Ok(result);
    }
}