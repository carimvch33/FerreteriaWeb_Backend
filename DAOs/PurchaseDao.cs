using FerreteríaWeb_Backend.DAOs.Interfaces;
using FerreteríaWeb_Backend.Data;
using FerreteríaWeb_Backend.Models.DTOs.Purchases;
using FerreteríaWeb_Backend.Models.DTOs;
using FerreteríaWeb_Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FerreteríaWeb_Backend.DAOs;

public class PurchaseDao : IPurchaseDao
{
    private readonly FerreteriaDbContext _context;

    public PurchaseDao(FerreteriaDbContext context)
    {
        _context = context;
    }

    public Result<PurchaseDto> AddPurchase(PurchaseDto purchase)
    {
        Result<PurchaseDto> result = new();
        Purchase newPurchase = new()
        {
            CreatedAt = DateTime.Now,
            EmployeeId = purchase.EmployeeId,
            ProviderId = purchase.ProviderId
        };

        try
        {
            _context.Purchases.Add(newPurchase);
            _context.SaveChanges();
        }
        catch (DbUpdateException error)
        {
            result.InnerException = error;
            result.IsAccomplished = false;
        }

        if (result.InnerException is null)
        {
            purchase.Id = newPurchase.Id;
            purchase.CreatedAt = newPurchase.CreatedAt;
            result.Data = purchase;
            result.IsAccomplished = true;
        }

        return result;
    }
}