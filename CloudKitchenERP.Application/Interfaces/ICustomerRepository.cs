using CloudKitchenERP.Domain.Entities;

namespace CloudKitchenERP.Application.Interfaces;

public interface ICustomerRepository
{
    Task<Customer?> GetByUserIdAsync(int userId);

    Task AddAsync(Customer customer);

    Task UpdateAsync(Customer customer);

    Task SaveChangesAsync();
}