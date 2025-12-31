using FerreteríaWeb_Backend.Models.DTOs;
using FerreteríaWeb_Backend.Services.Interfaces;
using FerreteríaWeb_Backend.DAOs.Interfaces;

namespace FerreteríaWeb_Backend.Services;

public class ProviderService : IProviderService
{
    private readonly IProviderDao _dao;

    public ProviderService(IProviderDao dao)
    {
        _dao = dao;
    }

    public Result<ProviderDto> AddProvider(ProviderDto provider)
    {
        Result<ProviderDto> result = new();

        var providerExists = _dao.ProviderExists(provider.Rfc);
        if(!providerExists.IsAccomplished)
        {
            //TODO: add logger
            Console.WriteLine(providerExists.InnerException?.ToString() ?? "Error al registrar proveedor");
            result.Message = "No es posible realizar la operación ahora. Inténtelo más tarde";
            result.IsAccomplished = false;
            return result;
        }

        if(providerExists.Data)
        {
            result.Message = "Ya existe un proveedor registrado con el mismo RFC";
            result.IsAccomplished = false;
            return result;
        }

        var createdProvider = _dao.AddProvider(provider);
        if (!createdProvider.IsAccomplished)
        {
            //TODO: add logger
            Console.WriteLine(result.InnerException?.ToString() ?? "Error al registrar proveedor");
            result.Message = "No es posible realizar la operación ahora. Inténtelo más tarde";
            result.IsAccomplished = false;
        }
        else
        {
            result.Data = createdProvider.Data;
        }

        return result;
    }

    public Result<ProviderDto?> GetProvider(int id)
    {
        Result<ProviderDto?> result = new();

        var provider = _dao.GetProvider(id);
        if(!provider.IsAccomplished)
        {
            //TODO: add logger
            Console.WriteLine(provider.InnerException?.ToString() ?? "Error al buscar proveedor");
            result.Message = "No es posible realizar la operación ahora. Inténtelo más tarde";
            result.IsAccomplished = false;
            return result;
        }

        result.Data = provider.Data;
        return result;
    }

    public Result<bool> UpdateProvider(ProviderDto provider)
    {
        Result<bool> result = new();

        var updateResult = _dao.UpdateProvider(provider);
        if (!updateResult.IsAccomplished)
        {
            //TODO: add logger
            Console.WriteLine(updateResult.InnerException?.ToString() ?? "Error al actualizar proveedor");
            result.Message = "No es posible realizar la operación ahora. Inténtelo más tarde";
            result.IsAccomplished = false;
            return result;
        }

        result.Data = updateResult.Data;
        return result;
    }
}