using FerreteríaWeb_Backend.Models.DTOs;

namespace FerreteríaWeb_Backend.Services.Interfaces;

public interface ISaleService
{
    Result<SaleDto> AddSale(SaleDto sale);
}