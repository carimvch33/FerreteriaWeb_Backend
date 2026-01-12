using FerreteríaWeb_Backend.Models.DTOs;
using FerreteríaWeb_Backend.Models.DTOs.Sales;

namespace FerreteríaWeb_Backend.Services.Interfaces;

public interface ISaleService
{
    Result<SaleDto> AddSale(SaleDto sale);
}