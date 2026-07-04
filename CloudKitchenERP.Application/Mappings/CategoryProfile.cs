using AutoMapper;
using CloudKitchenERP.Contracts.Category;
using CloudKitchenERP.Domain.Entities;

namespace CloudKitchenERP.Application.Mappings;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryResponse>();

        CreateMap<CreateCategoryRequest, Category>();

        CreateMap<UpdateCategoryRequest, Category>();
    }
}