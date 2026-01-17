using FerreteríaWeb_Backend.DAOs.Interfaces;
using FerreteríaWeb_Backend.Models.DTOs;
using FerreteríaWeb_Backend.Models.DTOs.Purchases;
using FerreteríaWeb_Backend.Services.Interfaces;

namespace FerreteríaWeb_Backend.Services;

public class PurchaseService : IPurchaseService
{
    private readonly IPurchaseDao _dao;
    private readonly IProductService _productService;

    public PurchaseService(IPurchaseDao dao, IProductService productService)
    {
        _dao = dao;
        _productService = productService;
    }

    public Result<PurchaseDto> AddPurchase(PurchaseDto purchase)
    {
        Result<PurchaseDto> result = new();

        foreach (PurchasedProductDto producto in purchase.Products)
        {
            _productService.AddInventory(producto.Id, new()
            {
               Quantity = producto.Quantity 
            });
        }

        var addPurchaseResult = _dao.AddPurchase(purchase);
        if (!addPurchaseResult.IsAccomplished)
        {
            Console.WriteLine($"Error al registrar compra: {addPurchaseResult.InnerException?.ToString()}");
            result.Message = "No es posible realizar la compra en este momento. Intente de nuevo más tarde";
            result.IsAccomplished = false;
        }
        else
        {
            result.Data = addPurchaseResult.Data;
            result.IsAccomplished = true;
        }

        return result;
    }
}