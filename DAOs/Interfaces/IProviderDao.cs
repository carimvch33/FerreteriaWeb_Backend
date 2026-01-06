using FerreteríaWeb_Backend.Models.DTOs;

namespace FerreteríaWeb_Backend.DAOs.Interfaces;

public interface IProviderDao
{
    Result<ProviderDto> AddProvider(ProviderDto provider);
    Result<bool> UpdateProvider(ProviderDto provider);
    Result<bool> UpdateProviderState(int id);
    Result<ProviderDto?> GetProvider(int id);
    Result<bool> ProviderExists(string rfc);
    Result<List<ProviderDto>> GetAllProviders(); 
}