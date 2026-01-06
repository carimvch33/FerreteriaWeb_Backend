using FerreteríaWeb_Backend.Data;
using FerreteríaWeb_Backend.Models.DTOs;
using FerreteríaWeb_Backend.DAOs.Interfaces;
using Microsoft.EntityFrameworkCore;
using FerreteríaWeb_Backend.Models.Entities;

namespace FerreteríaWeb_Backend.DAOs;

public class ProviderDao : IProviderDao
{
    private readonly FerreteriaDbContext _context;

    public ProviderDao(FerreteriaDbContext context)
    {
        _context = context;
    }

    public Result<ProviderDto> AddProvider(ProviderDto provider)
    {
        Result<ProviderDto> result = new();
        Provider newProvider = new()
        {
            Rfc = provider.Rfc,
            Name = provider.Name,
            Phone = provider.Phone,
            Email = provider.Email,
            Address = provider.Address,
            Active = true
        };
        _context.Add(newProvider);

        try
        {    
            _context.SaveChanges();
        }
        catch (DbUpdateException error)
        {
            result.InnerException = error;
            result.IsAccomplished = false;
        }
        
        if (result.InnerException is null)
        {
            provider.Id = newProvider.Id;
            result.Data = provider;
            result.IsAccomplished = true;
        }

        return result;
    }

    public Result<List<ProviderDto>> GetAllProviders()
    {
        Result<List<ProviderDto>> result = new();
        List<ProviderDto> providers = [];

        try
        {
            var queryResult = _context.Providers.Where((p) => p.Id > 0);
            foreach (Provider p in queryResult)
            {
                providers.Add(new()
                {
                    Id = p.Id,
                    Rfc = p.Rfc,
                    Name = p.Name,
                    Phone = p.Phone,
                    Email = p.Email,
                    Address = p.Address,
                    Active = p.Active
                });
            }
        }
        catch (DbUpdateException error)
        {
            result.InnerException = error;
            result.IsAccomplished = false;
        }

        if (result.InnerException is null)
        {
            result.Data = providers;
            result.IsAccomplished = true;
        }

        return result;
    }

    public Result<ProviderDto?> GetProvider(int id)
    {
        Result<ProviderDto?> result = new();
        Provider? provider = null;

        try
        {
            provider = _context.Find<Provider>(id);
        }
        catch (DbUpdateException error)
        {
            result.InnerException = error;
            result.IsAccomplished = false;
        }

        if (result.InnerException is null)
        {
            if (provider is not null)
            {
                result.Data = new()
                {
                    Id = provider.Id,
                    Rfc = provider.Rfc,
                    Name = provider.Name,
                    Phone = provider.Phone,
                    Email = provider.Email,
                    Address = provider.Address,
                    Active = provider.Active
                };
            }
            result.IsAccomplished = true;
        }

        return result;
    }

    public Result<bool> ProviderExists(string rfc)
    {
        Result<bool> result = new();
        bool exists = false;
        
        try
        {
            exists = _context.Providers.Any((p) => p.Rfc.Equals(rfc));
        }
        catch (DbUpdateException error)
        {
            result.InnerException = error;
            result.IsAccomplished = false;
        }

        if (result.InnerException is null)
        {
            result.Data = exists;
            result.IsAccomplished = true;
        }

        return result;
    }

    public Result<bool> UpdateProvider(ProviderDto provider)
    {
        Result<bool> result = new();
        Provider? foundProvider = null;

        try
        {
            foundProvider = _context.Find<Provider>(provider.Id);
            if (foundProvider is not null)
            {
                foundProvider.Rfc = provider.Rfc;
                foundProvider.Name = provider.Name;
                foundProvider.Phone = provider.Phone;
                foundProvider.Email = provider.Email;
                foundProvider.Address = provider.Address;
                _context.Update(foundProvider);
                _context.SaveChanges();
            }
        }
        catch (DbUpdateException error)
        {
            result.InnerException = error;
            result.IsAccomplished = false;
        }

        if (result.InnerException is null)
        {
            result.Data = foundProvider is not null;
            result.IsAccomplished = true;
        }

        return result;
    }

    public Result<bool> UpdateProviderState(int id)
    {
        Result<bool> result = new();
        Provider? foundProvider = null;

        try
        {
            foundProvider = _context.Find<Provider>(id);
            if (foundProvider is not null)
            {
                foundProvider.Active = !foundProvider.Active;
                _context.Update(foundProvider);
                _context.SaveChanges();
            }
        }
        catch (DbUpdateException error)
        {
            result.InnerException = error;
            result.IsAccomplished = false;
        }

        if (result.InnerException is null)
        {
            result.Data = foundProvider is not null;
            result.IsAccomplished = true;
        }

        return result;
    }
}