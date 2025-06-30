using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RegistracijaVozila.Data;
using RegistracijaVozila.Models.Domain;
using RegistracijaVozila.Models.DTO;
using RegistracijaVozila.Repositories.Interface;
using RegistracijaVozila.Results;
using RegistracijaVozila.Services.Interface;

namespace RegistracijaVozila.Services.Implementation
{
    public class VehicleTypeService : IVehicleTypeService
    {
        private readonly RegistracijaVozilaDbContext appDbContext;
        private readonly IMapper mapper;
        private readonly IVehicleTypeRepository vehicleTypeRepository;

        public VehicleTypeService(RegistracijaVozilaDbContext appDbContext, IMapper mapper, 
            IVehicleTypeRepository vehicleTypeRepository)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
            this.vehicleTypeRepository = vehicleTypeRepository;
        }

        public async Task<RepositoryResult<bool>> ValidateVehicleTypeCreateRequestAsync(CreateVehicleTypeRequestDto request)
        {

            var existingCategory = await appDbContext.TipoviVozila.AnyAsync(x =>
                x.Kategorija == request.Kategorija);


            if (existingCategory)
            {
                return RepositoryResult<bool>.Fail("VEHICLE_TYPE_CATEGORY_EXISTS: " +
                    "The type of category already exists");
            }

            var existingName = await appDbContext.TipoviVozila.AnyAsync(x => x.Naziv == request.Naziv);

            if (existingName)
            {
                return RepositoryResult<bool>.Fail("VEHICLE_TYPE_NAME_EXISTS: Vehicle type already exists");
            }

            return RepositoryResult<bool>.Ok(true);
        }



        public async Task<RepositoryResult<VehicleTypeDto>> CreateVehicleTypeAsync(CreateVehicleTypeRequestDto request)
        {
            var validationResult = await ValidateVehicleTypeCreateRequestAsync(request);

            if (!validationResult.Success)
            {
                return RepositoryResult<VehicleTypeDto>.Fail(validationResult.Message);
            }

            var vehicleDomain = mapper.Map<TipVozila>(request);

            vehicleDomain = await vehicleTypeRepository.AddAsync(vehicleDomain);

            var response = mapper.Map<VehicleTypeDto>(vehicleDomain);

            return RepositoryResult<VehicleTypeDto>.Ok(response);
        }

        public async Task<RepositoryResult<bool>> ValidateVehicleTypeDeleteRequestAsync(Guid id)
        {
            var hasBrands = await appDbContext.MarkeVozila.AnyAsync(x => x.TipVozilaId == id);

            if (hasBrands)
            {
                return RepositoryResult<bool>.Fail
                    ("TYPE_HAS_BRANDS: Vehicle type cannot be deleted because it has associated brands.");
            }

            var exists = await appDbContext.TipoviVozila.AnyAsync(x => x.Id == id);

            if (!exists)
            {
                return RepositoryResult<bool>.Fail($"VEHICLE_TYPE_NOT_FOUND: Vehicle type with the id {id} doesnt exist");
            }

            return RepositoryResult<bool>.Ok(true);
        }

        public async Task<RepositoryResult<VehicleTypeDto>> DeleteVehicleTypeAsync(Guid id)
        {
            var validationResult = await ValidateVehicleTypeDeleteRequestAsync(id);

            if (!validationResult.Success)
            {
                return RepositoryResult<VehicleTypeDto>.Fail(validationResult.Message);
            }

            var vehicleTypeDomain = await vehicleTypeRepository.DeleteAsync(id);

            var reponse = mapper.Map<VehicleTypeDto>(vehicleTypeDomain);

            return RepositoryResult<VehicleTypeDto>.Ok(reponse);
        }

        public async Task<RepositoryResult<bool>> ValidateVehicleTypeUpdateRequestAsync(UpdateVehicleTypeRequestDto request)
        {
            var exists = await appDbContext.TipoviVozila.AnyAsync(x => x.Id == request.Id);

            if (!exists)
            {
                return RepositoryResult<bool>.Fail($"VEHICLE_TYPE_NOT_FOUND: Vehicle with the id {request.Id} doesnt exist");
            }

            var existingCategory = await appDbContext.TipoviVozila.AnyAsync(x =>
                x.Kategorija == request.Kategorija && x.Id!=request.Id);


            if (existingCategory)
            {
                return RepositoryResult<bool>.Fail("VEHICLE_TYPE_CATEGORY_EXISTS: The type of category already exists");
            }

            var existingName = await appDbContext.TipoviVozila.AnyAsync(x => x.Naziv == request.Naziv && x.Id!=request.Id);

            if (existingName)
            {
                return RepositoryResult<bool>.Fail("VEHICLE_TYPE_NAME_EXISTS: Vehicle type already exists");
            }

            return RepositoryResult<bool>.Ok(true);
        }

        public async Task<RepositoryResult<VehicleTypeDto>> UpdateVehicleTypeAsync(UpdateVehicleTypeRequestDto request)
        {
            var validationResult = await ValidateVehicleTypeUpdateRequestAsync(request);

            if (!validationResult.Success)
            {
                return RepositoryResult<VehicleTypeDto>.Fail(validationResult.Message);
            }

            var vehicleTypeDomain = mapper.Map<TipVozila>(request);

            vehicleTypeDomain = await vehicleTypeRepository.UpdateAsync(vehicleTypeDomain);

            var response = mapper.Map<VehicleTypeDto>(vehicleTypeDomain);

            return RepositoryResult<VehicleTypeDto>.Ok(response);
        }
    }
}
