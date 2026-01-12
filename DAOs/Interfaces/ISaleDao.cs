using FerreteríaWeb_Backend.Models.DTOs.Sales;
using FerreteríaWeb_Backend.Models.DTOs;

namespace FerreteríaWeb_Backend.DAOs.Interfaces;

public interface ISaleDao
{
    Result<SaleDto> AddSale(SaleDto sale, PaymentMethod paymentMethod);
}