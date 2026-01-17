using FerreteríaWeb_Backend.Models.DTOs;
using FerreteríaWeb_Backend.Models.DTOs.CashRegister;
using FerreteríaWeb_Backend.Models.DTOs.Sales;
using FerreteríaWeb_Backend.Models.Entities;

namespace FerreteríaWeb_Backend.Services.Interfaces;

public interface ISaleService
{
    Result<SaleDto> AddSale(SaleDto sale);
    Result<CashRegisterCutDto> GenerateCut(DateTime from, DateTime to, Employee employee);
    List<CashRegisterSaleDetailDto> GetCutDetails(DateTime from, DateTime to);
    SaleTicketDto? GetSaleTicket(int saleId);
}