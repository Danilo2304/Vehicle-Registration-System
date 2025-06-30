using RegistracijaVozila.Models.DTO;
using RegistracijaVozila.Results;

namespace RegistracijaVozila.Services.Interface
{
    public interface IVehicleService
    {
        Task<RepositoryResult<bool>> ValidateVehicleCreateRequestAsync(CreateVehicleRequestDto request);

        Task<RepositoryResult<bool>?> ValidateVehicleDeleteRequestAsync(Guid id);

        Task<RepositoryResult<bool>?> ValidateVehicleUpdateRequestAsync(UpdateVehicleDto request);

        Task<RepositoryResult<VehicleDto>> CreateVehicleAsync(CreateVehicleRequestDto request);

        Task<RepositoryResult<VehicleDto>> DeleteVehicleAsync(Guid id);

        Task<RepositoryResult<VehicleDto>> UpdateVehicleAsync(UpdateVehicleDto request);
    }
}
