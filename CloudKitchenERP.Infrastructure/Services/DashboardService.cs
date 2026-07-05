using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Contracts.Dashboard;

namespace CloudKitchenERP.Infrastructure.Services;

public class DashboardService : IDashboardService
{
    private readonly IDashboardRepository _dashboardRepository;

    public DashboardService(IDashboardRepository dashboardRepository)
    {
        _dashboardRepository = dashboardRepository;
    }

    public async Task<DashboardResponse> GetDashboardAsync()
    {
        return await _dashboardRepository.GetDashboardAsync();
    }
}