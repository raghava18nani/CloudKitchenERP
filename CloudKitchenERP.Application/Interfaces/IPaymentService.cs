using CloudKitchenERP.Contracts.Payment;

namespace CloudKitchenERP.Application.Interfaces;

public interface IPaymentService
{
    Task<PaymentResponse?> PayAsync(int userId, PayOrderRequest request);

    Task<List<PaymentResponse>> GetPaymentHistoryAsync(int userId);
}