using System.Text.Json;
using AutoMapper;
using BlazorApp.Features.TravelPlanner.Models;
using BlazorApp.Features.TravelPlanner.Services.Abstractions;
using Microsoft.Extensions.Caching.Memory;

namespace BlazorApp.Features.TravelPlanner.Services;

public class SlStopsAndMapsDataClient : IStopsAndMapsDataClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public SlStopsAndMapsDataClient(IHttpClientFactory httpClientFactory, IMapper mapper, IMemoryCache cache)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    public async Task<RouteStopsDto> GetStopsOnLineAsync(string lineNumber)
    {
        using (var client = _httpClientFactory.CreateClient("TravelPlanner"))
        {
            if (!_cache.TryGetValue<LinesDTO>("buslines", out var linesResult))
            {
                var linesResponse = await client.GetAsync("https://api.sl.se/api2/LineData.json?key=f18412b0164348ba85445d876debe0d4&model=jour&DefaultTransportModeCode=BUS");
                linesResult = JsonSerializer.Deserialize<LinesDTO>(await linesResponse.Content.ReadAsStringAsync());

                if (linesResult != null)
                {
                    _cache.Set<LinesDTO>(
                        "buslines",
                        linesResult,
                        new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromDays(1)));
                }
            }

            var interestingLines = linesResult?.ResponseData.Result.Where(l => l.LineNumber == lineNumber && l.DirectionCode == "1");

            if (interestingLines == null)
            {
                throw new NullReferenceException("No lines found");
            }

            var interestingLineStopNumbers = interestingLines.Select(l => l.JourneyPatternPointNumber).ToList();

            if (!_cache.TryGetValue<StopsDTO>("busstops", out var stops))
            {
                var x = await client.GetAsync("https://api.sl.se/api2/LineData.json?key=f18412b0164348ba85445d876debe0d4&model=stop");
                stops = JsonSerializer.Deserialize<StopsDTO>(await x.Content.ReadAsStringAsync());

                if (stops != null)
                {
                    _cache.Set<StopsDTO>(
                        "busstops",
                        stops,
                        new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromDays(1)));
                }
            }

            var interestingStops = stops?
                .ResponseData
                .Result
                .Where(s => interestingLineStopNumbers.Contains(s.StopPointNumber))
                .Select(_mapper.Map<StopsResultDto>)
                .ToList();

            if (interestingStops == null)
            {
                throw new NullReferenceException("No stops found");
            }

            return new RouteStopsDto
            {
                Box = new MapBox
                {
                    MinLon = interestingStops.Select(s => s.LocationEastingCoordinate).Min(),
                    MaxLon = interestingStops.Select(s => s.LocationEastingCoordinate).Max(),
                    MinLat = interestingStops.Select(s => s.LocationNorthingCoordinate).Min(),
                    MaxLat = interestingStops.Select(s => s.LocationNorthingCoordinate).Max()
                },
                Stops = interestingStops
            };
        }
    }
}