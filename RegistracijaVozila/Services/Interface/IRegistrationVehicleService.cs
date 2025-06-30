using RegistracijaVozila.Models.DTO;
using RegistracijaVozila.Results;

namespace RegistracijaVozila.Services.Interface
{
    public interface IRegistrationVehicleService
    {
        Task<RepositoryResult<bool>> 
            ValidateRegistrationCreateRequestAsync(CreateRegistrationVehicleRequestDto request);

        Task<RepositoryResult<bool>?> ValidateRegistrationDeleteRequestAsync(Guid id);

        Task<RepositoryResult<bool>?> 
            ValidateRegistrationUpdateRequestAsync(UpdateRegistrationVehicleRequestDto request);

        Task<RepositoryResult<RegistrationVehicleDto>> 
            CreateRegistrationAsync(CreateRegistrationVehicleRequestDto request);

        Task<RepositoryResult<RegistrationVehicleDto>> DeleteRegistrationAsync(Guid id);

        Task<RepositoryResult<RegistrationVehicleDto>> 
            UpdateRegistrationAsync(UpdateRegistrationVehicleRequestDto request);
    }
}
