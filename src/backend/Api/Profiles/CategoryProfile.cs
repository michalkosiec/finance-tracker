using Api.Models;
using Api.Dtos.Categories;
using AutoMapper;

namespace Api.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryCreateDto, Category>().ForMember(dest => dest.UserId, opt => opt.Ignore());
            CreateMap<CategoryUpdateDto, Category>().ForMember(dest => dest.UserId, opt => opt.Ignore());
        }
    }
}