using System.Text.Json;
using AutoMapper;
using BlazorApp.Features.Shared.Models;
using BlazorApp.Features.TravelPlanner.Models;
using BlazorApp.Features.TravelPlanner.Services.Abstractions;
using Microsoft.Extensions.Options;

namespace BlazorApp.Features.TravelPlanner.Services;

public class SlTravelPlannerDataClient : ITravelPlannerDataClient
{
    private readonly IMapper _mapper;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly Keys _keys;

    public SlTravelPlannerDataClient(IHttpClientFactory httpClientFactory, IMapper mapper, IOptions<Keys> keys)
    {
        _httpClientFactory = httpClientFactory ?? throw new NullReferenceException(nameof(httpClientFactory));
        _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
        _keys = keys?.Value ?? throw new NullReferenceException(nameof(keys));
    }

    public async IAsyncEnumerable<NearbyStop> GetNearbyStops(double lon, double lat)
    {
        using (var client = _httpClientFactory.CreateClient("TravelPlanner"))
        {
            var response = await client.GetAsync($"/api2/nearbystopsv2.json?key={_keys.ApiKeys.SlNearbyStops}&originCoordLat={lat.ToString().Replace(",", ".")}&originCoordLong={lon.ToString().Replace(",", ".")}&maxNo=10&r=500&products=10");
            var data = JsonSerializer.Deserialize<NearbyStopsRoot>(await response.Content.ReadAsStreamAsync());

            foreach (var item in data?.stopLocationOrCoordLocation?.OrderBy(a => a.StopLocation.name)?.DistinctBy(s => s.StopLocation.mainMastExtId) ?? new List<StopLocationOrCoordLocationDto>())
            {
                yield return _mapper.Map<NearbyStop>(item.StopLocation);
            }
        }
    }

    public async Task<IEnumerable<Trip>> GetTravelPlan(string selectedStationOfOrigin, string workStation)
    {
        using (var client = _httpClientFactory.CreateClient("TravelPlanner"))
        {
            var response = await client.GetAsync($"/api2/TravelplannerV3_1/trip.json?key={_keys.ApiKeys.SlTravelPlanner}&destId={workStation}&originId={selectedStationOfOrigin}");
            var data = JsonSerializer.Deserialize<TravelPlannerRootDto>(await response.Content.ReadAsStreamAsync());
            var trips = data?.Trip?.Select(_mapper.Map<Trip>) ?? new List<Trip>();

            return trips.Where(t =>
                t.Legs.First().Origin.Time >= DateTime.Now &&
                t.Legs.First().Origin.Time < DateTime.Now.AddHours(1));
        }
    }

    public async Task<IEnumerable<string>> GetDeparturesForStop(string stopName)
    {
        stopName += "|Stockholm";

        using (var client = _httpClientFactory.CreateClient("Resrobot"))
        {
            var response = await client.GetAsync($"/v2.1/location.name?input={stopName}&format=json&accessId={_keys.ApiKeys.Resrobot}");
            var data = JsonSerializer.Deserialize<ResrobotDTO>(await response.Content.ReadAsStreamAsync());
            var extid = data?.stopLocationOrCoordLocation.First().StopLocation.extId;

            var departureResponse = await client.GetAsync($"/v2.1/departureBoard?id={extid}&format=json&accessId={_keys.ApiKeys.Resrobot}");
            var result = JsonSerializer.Deserialize<DeparturesDTO>(await departureResponse.Content.ReadAsStreamAsync());

            return result?.Departure?
                .Select(FormatListString)
                .Where(d => !string.IsNullOrWhiteSpace(d)) ?? new List<string>();
        }
    }

    private string FormatListString(Departure departure)
    {
        var minutesToDeparture = Math.Round(DateTime.Parse(departure.date + " " + departure.time).Subtract(DateTime.Now).TotalMinutes);
        var lineNumber = departure.ProductAtStop.displayNumber;
        var direction = departure.direction.Contains("(") ? 
            departure.direction
                .Substring(0, departure.direction.IndexOf('('))
                .Trim() 
            : departure.direction;

        if (minutesToDeparture < 1)
        {
            return string.Empty;
        }

        return $"{minutesToDeparture} mins - {lineNumber} {direction}";
    }
}