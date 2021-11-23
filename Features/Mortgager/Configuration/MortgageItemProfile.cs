using AutoMapper;
using BlazorApp.Features.Mortgager.Data;

namespace BlazorApp.Features.Mortgager.Configuration;

public class MortgageItemProfile : Profile
{
    public MortgageItemProfile()
    {
        CreateMap<MortgageItem, MortgageItemDto>();
        CreateMap<MortgageItemDto, MortgageItem>();

        CreateMap<Expense, ExpenseDto>();
        CreateMap<ExpenseDto, Expense>();
    }
}