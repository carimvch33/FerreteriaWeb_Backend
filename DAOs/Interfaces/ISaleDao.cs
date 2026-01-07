using FerreteríaWeb_Backend.Models.DTOs;
using FerreteríaWeb_Backend.Models.Entities;

namespace FerreteríaWeb_Backend.DAOs.Interfaces;

public interface ISaleDao
{
    Result<SaleDto> AddSale(SaleDto sale, PaymentMethod paymentMethod);
}