using FerreteríaWeb_Backend.DAOs.Interfaces;
using FerreteríaWeb_Backend.Data;
using FerreteríaWeb_Backend.Models.DTOs;
using FerreteríaWeb_Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FerreteríaWeb_Backend.DAOs;

public class SaleDao : ISaleDao
{
    private readonly FerreteriaDbContext _context;

    public SaleDao(FerreteriaDbContext context)
    {
        _context = context;
    }

    public Result<SaleDto> AddSale(SaleDto sale, PaymentMethod paymentMethod)
    {
        Result<SaleDto> result = new();
        Sale newSale = new()
        {
            CreatedAt = DateTime.Now,
            PaymentMethod = paymentMethod,
            EmployeeId = sale.EmployeeId
        };
        List<SaleDetail> newDetails = [];

        using var transaction = _context.Database.BeginTransaction();
        try
        {
            _context.Sales.Add(newSale);
            _context.SaveChanges();
            
            newDetails = sale.SaleDetails.ConvertAll<SaleDetail>(
                (dto) => new()
                {
                    SaleId = newSale.Id,
                    ProductId = dto.ProductId,
                    ProductQuantity = dto.ProductQuantity
                }
            );
            _context.SaleDetails.AddRange(newDetails);
            _context.SaveChanges();

            transaction.Commit();
        }
        catch (DbUpdateException error)
        {
            result.InnerException = error;
            result.IsAccomplished = false;
        }

        if (result.InnerException is null)
        {
            sale.Id = newSale.Id;
            sale.CreatedAt = newSale.CreatedAt;
            sale.SaleDetails = newDetails.ConvertAll<SaleDetailDto>(
                (entity) => new()
                {
                    SaleId = sale.Id,
                    ProductId = entity.ProductId,
                    ProductQuantity = entity.ProductQuantity
                }
            );
            result.Data = sale;
            result.IsAccomplished = true;
        }

        return result;
    }
}