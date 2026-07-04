using CloudKitchenERP.Domain.Entities;

namespace CloudKitchenERP.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
