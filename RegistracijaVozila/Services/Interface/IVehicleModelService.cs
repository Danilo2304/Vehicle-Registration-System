using RegistracijaVozila.Models.DTO;
using RegistracijaVozila.Results;

namespace RegistracijaVozila.Services.Interface
{
    public interface IVehicleModelService
    {
        Task<RepositoryResult<bool>> ValidateVehicleModelCreateRequestAsync(CreateVehicleModelRequestDto request);

        Task<RepositoryResult<bool>?> ValidateVehicleModelDeleteRequestAsync(Guid id);

        Task<RepositoryResult<bool>?> ValidateVehicleModelGetByIdAsync(Guid id);

        Task<RepositoryResult<bool>?> ValidateVehicleModelGetByBrandIdAsync(Guid id);

        Task<RepositoryResult<bool>?> ValidateVehicleModelUpdateRequestAsync(UpdateVehicleModelRequestDto request);

        Task<RepositoryResult<VehicleModelDto>> CreateVehicleModelAsync(CreateVehicleModelRequestDto request);

        Task<RepositoryResult<VehicleModelDto>> DeleteVehicleModelAsync(Guid id);

        Task<RepositoryResult<VehicleModelDto>> UpdateVehicleModelAsync(UpdateVehicleModelRequestDto request);

        Task<RepositoryResult<VehicleModelDto>> GetById(Guid id);

        Task<RepositoryResult<List<VehicleModelDto>>> GetByBrandId(Guid id);
    }
}
