using BlazorApp.Features.TravelPlanner.Models;

namespace BlazorApp.Features.TravelPlanner.Services.Abstractions;

public interface IStopsAndMapsDataClient
{
    Task<RouteStopsDto> GetStopsOnLineAsync(string lineNumber);
}