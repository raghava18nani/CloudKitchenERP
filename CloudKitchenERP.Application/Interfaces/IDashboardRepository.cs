using CloudKitchenERP.Contracts.Dashboard;

namespace CloudKitchenERP.Application.Interfaces;

public interface IDashboardRepository
{
    Task<DashboardResponse> GetDashboardAsync();
}