using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Contracts.Dashboard;
using CloudKitchenERP.Domain.Enums;
using CloudKitchenERP.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CloudKitchenERP.Persistence.Repositories;

public class DashboardRepository : IDashboardRepository
{
    private readonly ApplicationDbContext _context;

    public DashboardRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardResponse> GetDashboardAsync()
    {
        var today = DateTime.Today;
        return new DashboardResponse
        {
            TotalCustomers = await _context.Customers.CountAsync(),

            TotalCategories = await _context.Categories.CountAsync(),

            TotalMenuItems = await _context.MenuItems.CountAsync(),

            TotalOrders = await _context.Orders.CountAsync(),

            PendingOrders = await _context.Orders.CountAsync(x =>
                x.Status == OrderStatus.Pending),

            DeliveredOrders = await _context.Orders.CountAsync(x =>
                x.Status == OrderStatus.Delivered),

            TotalRevenue = await _context.Orders
                .Where(x => x.Status == OrderStatus.Delivered)
                .SumAsync(x => (decimal?)x.GrandTotal) ?? 0,

            TodayRevenue = await _context.Orders
                .Where(x => x.OrderDate.Date == today &&
                            x.Status == OrderStatus.Delivered)
                .SumAsync(x => (decimal?)x.GrandTotal) ?? 0,

            TodayOrders = await _context.Orders
                .CountAsync(x => x.OrderDate.Date == today)
        };
    }
}