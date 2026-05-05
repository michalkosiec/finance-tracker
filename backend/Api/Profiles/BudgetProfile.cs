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
            CreateMap<BudgetCreateDto, Budget>();
            CreateMap<BudgetUpdateDto, Budget>();
        }
    }
}