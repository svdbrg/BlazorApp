using System.Text.Json;
using BlazorApp.Features.TravelPlanner.Models;
using BlazorApp.Features.TravelPlanner.Services.Abstractions;

namespace BlazorApp.Features.TravelPlanner.Services;

public class SlTravelPlanner : ITravelPlanner
{
    private readonly IHttpClientFactory _httpClientFactory;

    public SlTravelPlanner(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory ?? throw new NullReferenceException(nameof(httpClientFactory));
    }

    public async Task<NearbyStopsRoot> GetNearbyStops()
    {
        using (var client = _httpClientFactory.CreateClient("TravelPlanner"))
        {
             var response = await client.GetAsync($"/api2/nearbystopsv2.json?key=195841a58631431ebec7649bc57d98aa&originCoordLat=59.32315313171132&originCoordLong=18.20933732771809&maxNo=10&r=500&products=8");
             var data = JsonSerializer.Deserialize<NearbyStopsRoot>(await response.Content.ReadAsStreamAsync());

             return data ?? new();
        }
    }

    public async Task<TravelPlannerRoot> GetTravelPlan(string selectedStationOfOrigin)
    {
        using (var client = _httpClientFactory.CreateClient("TravelPlanner"))
        {
            var response = await client.GetAsync($"/api2/TravelplannerV3_1/trip.json?key=73b55d6e552c482cb7785110d34c22e8&destId=9192&originId={selectedStationOfOrigin}");
            var data = JsonSerializer.Deserialize<TravelPlannerRoot>(await response.Content.ReadAsStreamAsync());

            return data ?? new();
        }
    }
}