namespace CloudKitchenERP.Contracts.Dashboard;

public class DashboardResponse
{
    public int TotalCustomers { get; set; }

    public int TotalCategories { get; set; }

    public int TotalMenuItems { get; set; }

    public int TotalOrders { get; set; }

    public int PendingOrders { get; set; }

    public int DeliveredOrders { get; set; }

    public decimal TotalRevenue { get; set; }

    public decimal TodayRevenue { get; set; }

    public int TodayOrders { get; set; }
}