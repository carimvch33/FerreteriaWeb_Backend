using FerreteríaWeb_Backend.Models.DTOs;
using FerreteríaWeb_Backend.Models.DTOs.Purchases;

namespace FerreteríaWeb_Backend.DAOs.Interfaces;

public interface IPurchaseDao
{
    Result<PurchaseDto> AddPurchase(PurchaseDto purchase);
}
