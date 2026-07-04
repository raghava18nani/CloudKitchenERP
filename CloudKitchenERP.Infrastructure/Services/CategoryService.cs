using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Contracts.Category;
using CloudKitchenERP.Domain.Entities;

namespace CloudKitchenERP.Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CategoryResponse>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();

        return categories.Select(x => new CategoryResponse
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            IsActive = x.IsActive
        }).ToList();
    }

    public async Task<CategoryResponse?> GetByIdAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category == null)
            return null;

        return new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            IsActive = category.IsActive
        };
    }

    public async Task<CategoryResponse> CreateAsync(CreateCategoryRequest request)
    {
        var category = new Category
        {
            Name = request.Name,
            Description = request.Description,
            IsActive = true
        };

        await _repository.AddAsync(category);
        await _repository.SaveChangesAsync();

        return new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            IsActive = category.IsActive
        };
    }

    public async Task<bool> UpdateAsync(UpdateCategoryRequest request)
    {
        var category = await _repository.GetByIdAsync(request.Id);

        if (category == null)
            return false;

        category.Name = request.Name;
        category.Description = request.Description;
        category.IsActive = request.IsActive;

        await _repository.UpdateAsync(category);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category == null)
            return false;

        await _repository.DeleteAsync(category);
        await _repository.SaveChangesAsync();

        return true;
    }
}