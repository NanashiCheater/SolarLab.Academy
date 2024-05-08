using AutoMapper;
using SolarLab.Academy.Contracts.Categories;
using SolarLab.Academy.Contracts.Users;
using SolarLab.Academy.Domain.Categories.Entity;
using SolarLab.Academy.Domain.Users.Entity;

namespace SolarLab.Academy.ComponentRegistrar.Mappers
{
    /// <summary>
    /// Профиль работы с категориями.
    /// </summary>
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();

            CreateMap<CreateCategoryRequest, Category>()
                .ForMember(s => s.Id, map => map.MapFrom(s => Guid.NewGuid()))
                .ForMember(s => s.Parent, map => map.Ignore())
                .ForMember(s => s.SubCategories, map => map.Ignore());
        }
    }
}