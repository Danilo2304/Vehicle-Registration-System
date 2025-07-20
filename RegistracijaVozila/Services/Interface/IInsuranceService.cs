using RegistracijaVozila.Models.DTO;
using RegistracijaVozila.Results;

namespace RegistracijaVozila.Services.Interface
{
    public interface IInsuranceService
    {

        Task<RepositoryResult<bool>> ValidateGetInsuranceByIdAsync(Guid id);

        Task<RepositoryResult<bool>> ValidateDeleteAsync(Guid id);

        Task<RepositoryResult<bool>> ValidateUpdateAsync(UpdateInsuranceRequestDto request);

        Task<RepositoryResult<InsuranceDto>> CreateInsuranceAsync(CreateInsuranceRequestDto request);

        Task<RepositoryResult<List<InsuranceDto>>> GetAllAsync();

        Task<RepositoryResult<InsuranceDto>> GetInsuranceByIdAsync(Guid id);

        Task<RepositoryResult<InsuranceDto>> DeleteAsync(Guid id);

        Task<RepositoryResult<InsuranceDto>> UpdateAsync(UpdateInsuranceRequestDto request);
    }
}


