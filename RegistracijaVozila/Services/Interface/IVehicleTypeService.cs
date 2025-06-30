using RegistracijaVozila.Models.DTO;
using RegistracijaVozila.Results;

namespace RegistracijaVozila.Services.Interface
{
    public interface IVehicleTypeService
    {
        Task<RepositoryResult<bool>> ValidateVehicleTypeCreateRequestAsync(CreateVehicleTypeRequestDto request);

        Task<RepositoryResult<bool>> ValidateVehicleTypeDeleteRequestAsync(Guid id);

        Task<RepositoryResult<bool>> ValidateVehicleTypeUpdateRequestAsync(UpdateVehicleTypeRequestDto request);

        Task<RepositoryResult<VehicleTypeDto>> CreateVehicleTypeAsync(CreateVehicleTypeRequestDto request);

        Task<RepositoryResult<VehicleTypeDto>> DeleteVehicleTypeAsync(Guid id);

        Task<RepositoryResult<VehicleTypeDto>> UpdateVehicleTypeAsync(UpdateVehicleTypeRequestDto request);
    }
}
