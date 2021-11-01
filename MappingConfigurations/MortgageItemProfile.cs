using AutoMapper;
using BlazorApp.Data;

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