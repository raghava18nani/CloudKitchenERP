using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Contracts.MenuItem;
using CloudKitchenERP.Domain.Entities;

namespace CloudKitchenERP.Infrastructure.Services;

public class MenuItemService : IMenuItemService
{
    private readonly IMenuItemRepository _repository;
    private readonly ICategoryRepository _categoryRepository;

    public MenuItemService(
        IMenuItemRepository repository,
        ICategoryRepository categoryRepository)
    {
        _repository = repository;
        _categoryRepository = categoryRepository;
    }

    public async Task<List<MenuItemResponse>> GetAllAsync()
    {
        var items = await _repository.GetAllAsync();

        return items.Select(x => new MenuItemResponse
        {
            Id = x.Id,
            CategoryId = x.CategoryId,
            CategoryName = x.Category.Name,
            Name = x.Name,
            Description = x.Description,
            Price = x.Price,
            IsVeg = x.IsVeg,
            IsAvailable = x.IsAvailable,
            PreparationTime = x.PreparationTime,
            ImageUrl = x.ImageUrl,
            IsBestSeller = x.IsBestSeller,
            IsTodaySpecial = x.IsTodaySpecial
        }).ToList();
    }

    public async Task<MenuItemResponse?> GetByIdAsync(int id)
    {
        var item = await _repository.GetByIdAsync(id);

        if (item == null)
            return null;

        return new MenuItemResponse
        {
            Id = item.Id,
            CategoryId = item.CategoryId,
            CategoryName = item.Category.Name,
            Name = item.Name,
            Description = item.Description,
            Price = item.Price,
            IsVeg = item.IsVeg,
            IsAvailable = item.IsAvailable,
            PreparationTime = item.PreparationTime,
            ImageUrl = item.ImageUrl,
            IsBestSeller = item.IsBestSeller,
            IsTodaySpecial = item.IsTodaySpecial
        };
    }

    public async Task<MenuItemResponse> CreateAsync(CreateMenuItemRequest request)
    {
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);

        if (category == null)
            throw new Exception("Category not found.");

        var menuItem = new MenuItem
        {
            CategoryId = request.CategoryId,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            IsVeg = request.IsVeg,
            IsAvailable = request.IsAvailable,
            PreparationTime = request.PreparationTime,
            ImageUrl = request.ImageUrl,
            IsBestSeller = request.IsBestSeller,
            IsTodaySpecial = request.IsTodaySpecial
        };

        await _repository.AddAsync(menuItem);
        await _repository.SaveChangesAsync();

        return new MenuItemResponse
        {
            Id = menuItem.Id,
            CategoryId = category.Id,
            CategoryName = category.Name,
            Name = menuItem.Name,
            Description = menuItem.Description,
            Price = menuItem.Price,
            IsVeg = menuItem.IsVeg,
            IsAvailable = menuItem.IsAvailable,
            PreparationTime = menuItem.PreparationTime,
            ImageUrl = menuItem.ImageUrl,
            IsBestSeller = menuItem.IsBestSeller,
            IsTodaySpecial = menuItem.IsTodaySpecial
        };
    }

    public async Task<bool> UpdateAsync(UpdateMenuItemRequest request)
    {
        var item = await _repository.GetByIdAsync(request.Id);

        if (item == null)
            return false;

        item.CategoryId = request.CategoryId;
        item.Name = request.Name;
        item.Description = request.Description;
        item.Price = request.Price;
        item.IsVeg = request.IsVeg;
        item.IsAvailable = request.IsAvailable;
        item.PreparationTime = request.PreparationTime;
        item.ImageUrl = request.ImageUrl;
        item.IsBestSeller = request.IsBestSeller;
        item.IsTodaySpecial = request.IsTodaySpecial;

        await _repository.UpdateAsync(item);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _repository.GetByIdAsync(id);

        if (item == null)
            return false;

        await _repository.DeleteAsync(item);
        await _repository.SaveChangesAsync();

        return true;
    }
}