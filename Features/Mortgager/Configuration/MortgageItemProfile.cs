using AutoMapper;
using BlazorApp.Features.Mortgager.Data;

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