using AutoMapper;
using ExampleProject.Model;

namespace ExampleProject.WebApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<ProductCategoryDTO, Product>()
                .ForMember(prod => prod.Id, opt => opt.MapFrom(dto => dto.ProductId))
                .ForMember(prod => prod.Category, opt => opt.MapFrom(dto => new Category(dto.CategoryId, dto.CategoryName == null ? "" : dto.CategoryName)));

            CreateMap<ProductCategoryDTO, Category>()
                .ForMember(cat => cat.Id, opt => opt.MapFrom(dto => dto.CategoryId))
                .ForMember(cat => cat.Name, opt => opt.MapFrom(dto => dto.CategoryName));

            CreateMap<Product, ProductCategoryDTO>()
                .ForMember(dto => dto.ProductId, opt => opt.MapFrom(prod => prod.Id))
                .ForMember(dto => dto.CategoryId, opt => opt.MapFrom(prod => prod.Category == null ? 0 : prod.Category.Id))
                .ForMember(dto => dto.CategoryName, opt => opt.MapFrom(prod => prod.Category == null ? "" : prod.Category.Name));
        }
    }
}
