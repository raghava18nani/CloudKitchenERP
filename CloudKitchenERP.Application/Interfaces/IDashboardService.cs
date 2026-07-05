using CloudKitchenERP.Contracts.Dashboard;

namespace CloudKitchenERP.Application.Interfaces;

public interface IDashboardService
{
    Task<DashboardResponse> GetDashboardAsync();
}