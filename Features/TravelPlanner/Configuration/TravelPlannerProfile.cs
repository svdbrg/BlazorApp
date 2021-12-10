using AutoMapper;
using BlazorApp.Features.TravelPlanner.Models;

namespace BlazorApp.Features.TravelPlanner.Configuration;

public class TravelPlannerProfile : Profile
{
    public TravelPlannerProfile()
    {
        CreateMap<OriginDto, Stop>()
            .ForMember(s => s.Name, opt => opt.MapFrom(d => d.name))
            .ForMember(s => s.Longitude, opt => opt.MapFrom(d => d.lon))
            .ForMember(s => s.Latitude, opt => opt.MapFrom(d => d.lat))
            .ForMember(s => s.Time, opt => opt.MapFrom((src, dest) =>
            {
                if (string.IsNullOrWhiteSpace(src.date) || string.IsNullOrWhiteSpace(src.time))
                {
                    return DateTime.MinValue;
                }

                return DateTime.Parse($"{src.date} {src.time}");
            }));

        CreateMap<DestinationDto, Stop>()
            .ForMember(s => s.Name, opt => opt.MapFrom(d => d.name))
            .ForMember(s => s.Longitude, opt => opt.MapFrom(d => d.lon))
            .ForMember(s => s.Latitude, opt => opt.MapFrom(d => d.lat))
            .ForMember(s => s.Time, opt => opt.MapFrom((src, dest) =>
            {
                if (string.IsNullOrWhiteSpace(src.date) || string.IsNullOrWhiteSpace(src.time))
                {
                    return DateTime.MinValue;
                }

                return DateTime.Parse($"{src.date} {src.time}");
            }));

        CreateMap<LegDto, Leg>()
            .ForMember(s => s.Origin, opt => opt.MapFrom(d => d.Origin))
            .ForMember(s => s.Destination, opt => opt.MapFrom(d => d.Destination));

        CreateMap<TripDto, Trip>()
            .ForMember(s => s.Line, opt => opt.MapFrom(d => d.LegList.Leg.First().Product.name))
            .ForMember(s => s.Legs, opt => opt.MapFrom(d => d.LegList.Leg));

    }
}