using System.Text.Json;
using AutoMapper;
using BlazorApp.Features.Shared.Models;
using BlazorApp.Features.TravelPlanner.Models;
using BlazorApp.Features.TravelPlanner.Services.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace BlazorApp.Features.TravelPlanner.Services;

public class SlStopsAndMapsDataClient : IStopsAndMapsDataClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;
    private readonly Keys _keys;

    public SlStopsAndMapsDataClient(IHttpClientFactory httpClientFactory, IMapper mapper, IMemoryCache cache, IOptions<Keys> keys)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _keys = keys?.Value ?? throw new ArgumentNullException(nameof(keys));
    }

    public async Task<RouteStopsDto> GetStopsOnLineAsync(string lineNumber)
    {
        using (var client = _httpClientFactory.CreateClient("TravelPlanner"))
        {
            if (!_cache.TryGetValue<LinesDTO>("buslines", out var linesResult))
            {
                var linesResponse = await client.GetAsync($"/api2/LineData.json?key={_keys.ApiKeys.SlLineData}&model=jour&DefaultTransportModeCode=BUS");
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
                var x = await client.GetAsync($"/api2/LineData.json?key={_keys.ApiKeys.SlLineData}&model=stop");
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
                throw new KeyNotFoundException("No stops found");
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