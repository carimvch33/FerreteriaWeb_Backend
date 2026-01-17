using FerreteríaWeb_Backend.DAOs.Interfaces;
using FerreteríaWeb_Backend.Data;
using FerreteríaWeb_Backend.Models.DTOs.Sales;
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

            foreach(SaleDetail detail in newDetails)
            {
                var product = _context.Products.Find(detail.ProductId);
                product!.Stock = product.Stock - detail.ProductQuantity;
            }
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

    public List<(PaymentMethod Method, decimal Total)> GetSalesByDateRange(DateTime from, DateTime to)
    {
        return _context.Sales
            .Where(s => s.CreatedAt >= from && s.CreatedAt <= to)
            .Select(s => new
            {
                s.PaymentMethod,
                Total = s.SaleDetails.Sum(d =>
                    d.ProductQuantity *
                    _context.Products
                        .Where(p => p.Id == d.ProductId)
                        .Select(p => p.Price)
                        .First()
                )
            })
            .AsEnumerable()
            .Select(x => (x.PaymentMethod, x.Total))
            .ToList();
    }

    public List<CashRegisterSaleDetailDto> GetSaleDetailsByDateRange(DateTime from, DateTime to)
    {
        return _context.Sales
            .Where(s => s.CreatedAt >= from && s.CreatedAt <= to)
            .Select(s => new CashRegisterSaleDetailDto
            {
                Date = s.CreatedAt,
                PaymentMethod = s.PaymentMethod.ToString(),
                Products = s.SaleDetails.Select(d => new CashRegisterProductDetailDto
                {
                    ProductName = _context.Products
                        .Where(p => p.Id == d.ProductId)
                        .Select(p => p.Name)
                        .First(),

                    Quantity = d.ProductQuantity,

                    UnitPrice = _context.Products
                        .Where(p => p.Id == d.ProductId)
                        .Select(p => p.Price)
                        .First(),

                    Subtotal = d.ProductQuantity *
                        _context.Products
                            .Where(p => p.Id == d.ProductId)
                            .Select(p => p.Price)
                            .First()
                }).ToList(),

                Total = s.SaleDetails.Sum(d =>
                    d.ProductQuantity *
                    _context.Products
                        .Where(p => p.Id == d.ProductId)
                        .Select(p => p.Price)
                        .First()
                )
            })
            .ToList();
    }

    public SaleTicketDto? GetSaleTicketById(int saleId)
    {
        return _context.Sales
            .Where(s => s.Id == saleId)
            .Select(s => new SaleTicketDto
            {
                Id = s.Id,
                Date = s.CreatedAt,
                PaymentMethod = s.PaymentMethod.ToString(),
                Total = s.SaleDetails.Sum(d =>
                    d.ProductQuantity *
                    _context.Products
                        .Where(p => p.Id == d.ProductId)
                        .Select(p => p.Price)
                        .First()
                ),
                Employee = s.Employee.Name + " " + s.Employee.LastName,
                Products = s.SaleDetails.Select(d => new SaleProductDto
                {
                    Name = _context.Products
                        .Where(p => p.Id == d.ProductId)
                        .Select(p => p.Name)
                        .First(),
                    Quantity = d.ProductQuantity,
                    Price = _context.Products
                        .Where(p => p.Id == d.ProductId)
                        .Select(p => p.Price)
                        .First()
                }).ToList()
            })
            .FirstOrDefault();
    }

}