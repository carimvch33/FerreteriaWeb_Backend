using FerreteríaWeb_Backend.DAOs.Interfaces;
using FerreteríaWeb_Backend.Models.DTOs;
using FerreteríaWeb_Backend.Services.Interfaces;

namespace FerreteríaWeb_Backend.Services;

public class SaleService : ISaleService
{
    private readonly ISaleDao _dao;

    public SaleService(ISaleDao dao)
    {
        _dao = dao;
    }

    public Result<SaleDto> AddSale(SaleDto sale)
    {
        Result<SaleDto> result = new();

        if(!sale.IsValid() || sale.SaleDetails.Any((d) => !d.IsValid()))
        {
            result.Message = $"La cantidad de producto debe ser mayor a 0";
            result.IsAccomplished = false;
        }

        var addSaleResult = _dao.AddSale(sale, Enum.Parse<PaymentMethod>(sale.PaymentMethod, true));
        if(!addSaleResult.IsAccomplished)
        {
            Console.WriteLine(addSaleResult.InnerException?.ToString() ?? "Error al registrar venta");
            result.Message = "No es posible realizar la venta. Intente más tarde por favor";
            result.IsAccomplished = false;
        }
        else
        {
            result.Data = addSaleResult.Data;
        }

        return result;
    }
}