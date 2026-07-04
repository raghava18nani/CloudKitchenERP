using CloudKitchenERP.Contracts.Customer;

namespace CloudKitchenERP.Application.Interfaces;

public interface ICustomerService
{
    Task<CustomerResponse?> GetProfileAsync(int userId);

    Task<CustomerResponse> CreateAsync(int userId, CreateCustomerRequest request);

    Task<bool> UpdateAsync(int userId, UpdateCustomerRequest request);
}