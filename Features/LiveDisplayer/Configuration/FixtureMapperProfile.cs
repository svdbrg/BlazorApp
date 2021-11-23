using AutoMapper;
using BlazorApp.Features.LiveDisplayer.Data;

namespace BlazorApp.Features.LiveDisplayer.Configuration;

public class FixtureMapperProfile : Profile
{
    public FixtureMapperProfile()
    {
        CreateMap<FixtureDto, Fixture>()
            .ForMember(s => s.HomeTeam, opt => opt.MapFrom(d => d.teams[0].team.club.abbr))
            .ForMember(s => s.AwayTeam, opt => opt.MapFrom(d => d.teams[1].team.club.abbr))
            .ForMember(s => s.Status, opt => opt.MapFrom(d => d.status))
            .ForMember(s => s.Result, opt => opt.MapFrom(d => $"{d.teams[0].score}-{d.teams[1].score}"))
            .ForMember(s => s.DateAndTime, opt => opt.MapFrom(d => DateTime.Parse(d.kickoff.label)));

        CreateMap<EntryDto, Team>()
            .ForMember(s => s.Position, opt => opt.MapFrom(d => d.position))
            .ForMember(s => s.Name, opt => opt.MapFrom(d => d.team.name))
            .ForMember(s => s.ShortName, opt => opt.MapFrom(d => d.team.shortName))
            .ForMember(s => s.Played, opt => opt.MapFrom(d => d.overall.played))
            .ForMember(s => s.GoalDifference, opt => opt.MapFrom(d => d.overall.goalsDifference))
            .ForMember(s => s.Points, opt => opt.MapFrom(d => d.overall.points));
    }
}