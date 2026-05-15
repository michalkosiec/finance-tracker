using System.Globalization;
using Api.Dtos.Budgets;
using Api.Dtos.Stats;
using Api.Models;
using AutoMapper;

namespace Api.Profiles
{
    public class StatsProfile : Profile
    {
        public StatsProfile()
        {
            CreateMap<CategoryStats, CategoryStatsReadDto>();

            CreateMap<MonthlyStats, MonthlyStatsReadDto>();
        }
    }
}