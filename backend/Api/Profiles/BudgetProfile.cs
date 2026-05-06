using System.Globalization;
using Api.Dtos.Budgets;
using Api.Models;
using AutoMapper;

namespace Api.Profiles
{
    public class BudgetProfile : Profile
    {
        public BudgetProfile()
        {
            CreateMap<Budget, BudgetReadDto>();

            CreateMap<BudgetCreateDto, Budget>().ForMember(dest => dest.Month, opt => opt.MapFrom(src => DateTime.SpecifyKind(
            DateTime.ParseExact(src.Month, "yyyy-MM", CultureInfo.InvariantCulture), 
            DateTimeKind.Utc
        )));
        
            CreateMap<BudgetUpdateDto, Budget>().ForMember(dest => dest.Month, opt => opt.MapFrom(src => DateTime.SpecifyKind(
            DateTime.ParseExact(src.Month, "yyyy-MM", CultureInfo.InvariantCulture), 
            DateTimeKind.Utc
        )));
        }
    }
}