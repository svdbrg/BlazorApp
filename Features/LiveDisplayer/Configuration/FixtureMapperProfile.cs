
using AutoMapper;
using BlazorApp.Features.LiveDisplayer.Data;

public class FixtureMapperProfile : Profile
{
    public FixtureMapperProfile()
    {
        CreateMap<FixtureDto, Fixture>()
            .ForMember(s => s.HomeTeam, opt => opt.MapFrom(d => d.teams[0].team.club.abbr))
            .ForMember(s => s.AwayTeam, opt => opt.MapFrom(d => d.teams[1].team.club.abbr))
            .ForMember(s => s.Result, opt => opt.MapFrom(d => $"{d.teams[0].score}-{d.teams[1].score}"))
            .ForMember(s => s.DateAndTime, opt => opt.MapFrom(d => DateTime.Parse(d.kickoff.label)));
    }
}