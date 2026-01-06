using FerreteríaWeb_Backend.Models.DTOs;

namespace FerreteríaWeb_Backend.Services.Interfaces;

public interface IProviderService
{
    Result<ProviderDto> AddProvider(ProviderDto provider);
    Result<bool> UpdateProvider(ProviderDto provider);
    Result<bool> UpdateProviderState(int id);
    Result<ProviderDto?> GetProvider(int id);
    Result<List<ProviderDto>> GetAllProviders();
}