using FerreteríaWeb_Backend.Models.DTOs.Purchases;
using FerreteríaWeb_Backend.Models.DTOs;

namespace FerreteríaWeb_Backend.Services.Interfaces;

public interface IPurchaseService
{
    Result<PurchaseDto> AddPurchase(PurchaseDto purchase);
}