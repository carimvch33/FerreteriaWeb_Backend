using FerreteríaWeb_Backend.DAOs.Interfaces;
using FerreteríaWeb_Backend.Models.DTOs;
using FerreteríaWeb_Backend.Models.DTOs.CashRegister;
using FerreteríaWeb_Backend.Models.DTOs.Sales;
using FerreteríaWeb_Backend.Models.Entities;
using FerreteríaWeb_Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FerreteríaWeb_Backend.Services;

public class SaleService : ISaleService
{
    private readonly ISaleDao _saleDao;
    private readonly IProductDao _productDao;

    public SaleService(ISaleDao saleDao, IProductDao productDao)
    {
        _saleDao = saleDao;
        _productDao = productDao;
    }

    public Result<SaleDto> AddSale(SaleDto sale)
    {
        Result<SaleDto> result = new();

        if(!sale.IsValid() || sale.SaleDetails.Any((d) => !d.IsValid()))
        {
            result.Message = $"La cantidad de producto debe ser mayor a 0";
            result.IsAccomplished = false;
            return result;
        }

        
        Result<bool> productsValidationResult = AreProductsAvailable(sale.SaleDetails);
        if (!productsValidationResult.IsAccomplished || !productsValidationResult.Data)
        {
            result.InnerException = productsValidationResult.InnerException;
            result.IsAccomplished = false;
            result.Message = productsValidationResult.Message;
            return result;
        }

        var addSaleResult = _saleDao.AddSale(sale, Enum.Parse<PaymentMethod>(sale.PaymentMethod, true));
        if(!addSaleResult.IsAccomplished)
        {
            Console.WriteLine(addSaleResult.InnerException?.ToString() ?? "Error al registrar venta");
            result.Message = "No es posible realizar la venta. Intente más tarde por favor";
            result.IsAccomplished = false;
        }
        else
        {
            result.IsAccomplished = true;
            result.Data = addSaleResult.Data;
        }

        return result;
    }

    private Result<bool> AreProductsAvailable(List<SaleDetailDto> saleProducts)
    {
        Result<bool> result = new();
        
        bool AreProductsAvailable = !saleProducts.Any((detail) => {
            Product? product = null;
            try
            {
                product = _productDao.GetById(detail.ProductId);
            }
            catch (DbUpdateException error)
            {
                Console.WriteLine($"Error al buscar producto {detail.ProductId}. {error}");
                result.Message = "No es posible realizar la operación ahora. Inténtelo más tarde";
                result.InnerException = error;
            }

            if (product is null)
            {
                return true;
            }            
            if (!product!.IsActive)
            {
                result.Message = $"El producto {product.Name} no está disponible";
                return true;
            }
            else if (product.Stock < detail.ProductQuantity)
            {
                result.Message = $"El producto {product.Name} no tiene suficiente stock";
                return true;
            }

            return false;
        });

        result.Data = AreProductsAvailable;
        result.IsAccomplished = result.InnerException is null;
        return result;
    }

    public Result<CashRegisterCutDto> GenerateCut(DateTime from, DateTime to, Employee employee)
    {
        Result<CashRegisterCutDto> result = new();

        var sales = _saleDao.GetSalesByDateRange(from, to);

        decimal cash = 0, card = 0, transfer = 0;

        foreach (var sale in sales)
        {
            switch (sale.Method)
            {
                case PaymentMethod.Cash:
                    cash += sale.Total;
                    break;
                case PaymentMethod.BankCard:
                    card += sale.Total;
                    break;
                case PaymentMethod.Transfer:
                    transfer += sale.Total;
                    break;
            }
        }

        result.Data = new CashRegisterCutDto
        {
            From = from,
            To = to,
            GeneratedByEmployeeName = $"{employee.Name} {employee.LastName} {employee.SecondLastName}",
            TotalSales = sales.Count,
            CashTotal = cash,
            BankCardTotal = card,
            TransferTotal = transfer,
            GrandTotal = cash + card + transfer
        };

        result.IsAccomplished = true;
        return result;
    }
}