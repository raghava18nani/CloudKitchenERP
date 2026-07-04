using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Contracts.Customer;
using CloudKitchenERP.Domain.Entities;

namespace CloudKitchenERP.Infrastructure.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUserRepository _userRepository;

    public CustomerService(
        ICustomerRepository customerRepository,
        IUserRepository userRepository)
    {
        _customerRepository = customerRepository;
        _userRepository = userRepository;
    }

    public async Task<CustomerResponse?> GetProfileAsync(int userId)
    {
        var customer = await _customerRepository.GetByUserIdAsync(userId);

        if (customer == null)
            return null;

        return new CustomerResponse
        {
            Id = customer.Id,
            UserId = customer.UserId,
            FullName = $"{customer.User.FirstName} {customer.User.LastName}",
            Email = customer.User.Email ?? "",
            MobileNumber = customer.User.MobileNumber,
            AddressLine1 = customer.AddressLine1,
            AddressLine2 = customer.AddressLine2,
            City = customer.City,
            State = customer.State,
            Pincode = customer.Pincode,
            Landmark = customer.Landmark
        };
    }

    public async Task<CustomerResponse> CreateAsync(int userId, CreateCustomerRequest request)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
            throw new Exception("User not found.");

        var customer = new Customer
        {
            UserId = userId,
            AddressLine1 = request.AddressLine1,
            AddressLine2 = request.AddressLine2,
            City = request.City,
            State = request.State,
            Pincode = request.Pincode,
            Landmark = request.Landmark
        };

        await _customerRepository.AddAsync(customer);
        await _customerRepository.SaveChangesAsync();

        return await GetProfileAsync(userId)
               ?? throw new Exception("Customer creation failed.");
    }

    public async Task<bool> UpdateAsync(int userId, UpdateCustomerRequest request)
    {
        var customer = await _customerRepository.GetByUserIdAsync(userId);

        if (customer == null)
            return false;

        customer.AddressLine1 = request.AddressLine1;
        customer.AddressLine2 = request.AddressLine2;
        customer.City = request.City;
        customer.State = request.State;
        customer.Pincode = request.Pincode;
        customer.Landmark = request.Landmark;

        await _customerRepository.UpdateAsync(customer);
        await _customerRepository.SaveChangesAsync();

        return true;
    }
}