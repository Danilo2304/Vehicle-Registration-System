using RegistracijaVozila.Models.DTO;
using RegistracijaVozila.Results;

namespace RegistracijaVozila.Services.Interface
{
    public interface IVehicleBrandService
    {
        Task<RepositoryResult<bool>> ValidateVehicleBrandCreateRequestAsync(CreateVehicleBrandRequestDto request);

        Task<RepositoryResult<bool>> ValidateVehicleBrandDeleteRequestAsync(Guid id);

        Task<RepositoryResult<bool>> ValidateVehicleBrandUpdateRequestAsync(UpdateVehicleBrandRequestDto request);

        Task<RepositoryResult<VehicleBrandDto>> CreateVehicleBrand(CreateVehicleBrandRequestDto request);

        Task<RepositoryResult<VehicleBrandDto>> DeleteVehicleBrand(Guid id);

        Task<RepositoryResult<VehicleBrandDto>> UpdateVehicleBrand(UpdateVehicleBrandRequestDto request);
    }
}
