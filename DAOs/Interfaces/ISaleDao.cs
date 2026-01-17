using FerreteríaWeb_Backend.Models.DTOs;
using FerreteríaWeb_Backend.Models.DTOs.Sales;

namespace FerreteríaWeb_Backend.DAOs.Interfaces;

public interface ISaleDao
{
    Result<SaleDto> AddSale(SaleDto sale, PaymentMethod paymentMethod);
    List<(PaymentMethod Method, decimal Total)> GetSalesByDateRange(DateTime from, DateTime to);
    List<CashRegisterSaleDetailDto> GetSaleDetailsByDateRange(DateTime from, DateTime to);
    SaleTicketDto? GetSaleTicketById(int saleId);
}