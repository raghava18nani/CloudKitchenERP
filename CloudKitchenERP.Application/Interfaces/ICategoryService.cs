using CloudKitchenERP.Contracts.Category;

namespace CloudKitchenERP.Application.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryResponse>> GetAllAsync();

    Task<CategoryResponse?> GetByIdAsync(int id);

    Task<CategoryResponse> CreateAsync(CreateCategoryRequest request);

    Task<bool> UpdateAsync(UpdateCategoryRequest request);

    Task<bool> DeleteAsync(int id);
}