using FerreteríaWeb_Backend.Models.DTOs;

namespace FerreteríaWeb_Backend.Services.Interfaces;

public interface IProviderService
{
    Result<ProviderDto> AddProvider(ProviderDto provider);
    Result<bool> UpdateProvider(ProviderDto provider);
    Result<ProviderDto?> GetProvider(int id);
}