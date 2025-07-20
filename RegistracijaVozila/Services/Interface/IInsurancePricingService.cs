using RegistracijaVozila.Models.DTO;
using RegistracijaVozila.Results;

namespace RegistracijaVozila.Services.Interface
{
    public interface IInsurancePricingService
    {
        Task<RepositoryResult<bool>> ValidateCreateAsync(CreateInsurancePriceRequestDto request);

        Task<RepositoryResult<bool>> ValidateGetAllAsync();

        Task<RepositoryResult<bool>> ValidateGetByIdAsync(Guid id);

        Task<RepositoryResult<bool>> ValidateGetByInsuranceIdAsync(Guid id);

        Task<RepositoryResult<InsurancePriceDto>> CreateAsync(CreateInsurancePriceRequestDto request);

        Task<RepositoryResult<List<InsurancePriceDto>>> GetAllAsync();

        Task<RepositoryResult<InsurancePriceDto>> GetByIdAsync(Guid id);

        Task<RepositoryResult<InsurancePriceDto>> GetByInsuranceIdAsync(Guid id);
    }
}
