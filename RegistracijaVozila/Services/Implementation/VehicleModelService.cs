using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using RegistracijaVozila.Data;
using RegistracijaVozila.Models.Domain;
using RegistracijaVozila.Models.DTO;
using RegistracijaVozila.Repositories.Interface;
using RegistracijaVozila.Results;
using RegistracijaVozila.Services.Interface;
using System.ComponentModel.DataAnnotations;

namespace RegistracijaVozila.Services.Implementation
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly RegistracijaVozilaDbContext appDbContext;
        private readonly IMapper mapper;
        private readonly IVehicleModelRepository vehicleModelRepository;

        public VehicleModelService(RegistracijaVozilaDbContext appDbContext, IMapper mapper,
            IVehicleModelRepository vehicleModelRepository)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
            this.vehicleModelRepository = vehicleModelRepository;
        }

        public async Task<RepositoryResult<bool>> ValidateVehicleModelCreateRequestAsync(CreateVehicleModelRequestDto request)
        {
            var existingModel = await appDbContext.ModeliVozila.AnyAsync
                (x=>x.Naziv.ToLower() == request.Naziv.ToLower() && 
                x.MarkaVozilaId == request.MarkaVozilaId);

            if (existingModel)
            {
                return RepositoryResult<bool>.Fail("VEHICLE_MODEL_EXISTS: " +
                    "Model already exists for the given vehicle brand");
            }

            if(!await appDbContext.MarkeVozila.AnyAsync(x=>x.Id == request.MarkaVozilaId))
            {
                return RepositoryResult<bool>.Fail($"VEHICLE_BRAND_NOT_FOUND: Vehicle brand with the id " +
                    $"{request.MarkaVozilaId} doesnt exist");
            }

            return RepositoryResult<bool>.Ok(true);
        }

        public async Task<RepositoryResult<VehicleModelDto>> CreateVehicleModelAsync(CreateVehicleModelRequestDto request)
        {
            var validationResult = await ValidateVehicleModelCreateRequestAsync(request);

            if (!validationResult.Success)
            {
                return RepositoryResult<VehicleModelDto>.Fail(validationResult.Message);
            }

            var vehicleModelDomain = mapper.Map<ModelVozila>(request);

            vehicleModelDomain = await vehicleModelRepository.AddAsync(vehicleModelDomain);

            var response = mapper.Map<VehicleModelDto>(vehicleModelDomain);

            return RepositoryResult<VehicleModelDto>.Ok(response);
        }

        public async Task<RepositoryResult<bool>?> ValidateVehicleModelDeleteRequestAsync(Guid id)
        {
            if(!await appDbContext.ModeliVozila.AnyAsync(x=>x.Id==id))
            {
                return RepositoryResult<bool>.Fail($"VEHICLE_MODEL_NOT_FOUND: Vehicle MODEL with the id " +
                    $"{id} doesnt exist");
            }

            if(await appDbContext.Vozila.AnyAsync(x => x.ModelVozilaId == id))
            {
                return RepositoryResult<bool>.Fail("VEHICLE_MODEL_IN_USE: " +
                    "Cannot delete model because it's assigned to existing vehicles");
            }

            return RepositoryResult<bool>.Ok(true);
        }

        public async Task<RepositoryResult<VehicleModelDto>> DeleteVehicleModelAsync(Guid id)
        {
            var validationResult = await ValidateVehicleModelDeleteRequestAsync(id);

            if (!validationResult.Success)
            {
                return RepositoryResult<VehicleModelDto>.Fail(validationResult.Message);
            }

            var vehicleModelDeleted = await vehicleModelRepository.DeleteAsync(id);

            var response = mapper.Map<VehicleModelDto>(vehicleModelDeleted);

            return RepositoryResult<VehicleModelDto>.Ok(response);
        }

        public async Task<RepositoryResult<bool>?> ValidateVehicleModelUpdateRequestAsync(UpdateVehicleModelRequestDto request)
        {
            if(!await appDbContext.ModeliVozila.AnyAsync(x=>x.Id == request.Id))
            {
                return RepositoryResult<bool>.Fail($"VEHICLE_MODEL_NOT_FOUND: Vehicle model with the id " +
                    $"{request.Id} doesnt exist");
            }

            var existingModel = await appDbContext.ModeliVozila.AnyAsync
                (x => x.Naziv.ToLower() == request.Naziv.ToLower() &&
                x.MarkaVozilaId == request.MarkaVozilaId && x.Id!=request.Id);

            if (existingModel)
            {
                return RepositoryResult<bool>.Fail("VEHICLE_MODEL_EXISTS: " +
                    "Model already exists for the given vehicle brand");
            }

            if (!await appDbContext.MarkeVozila.AnyAsync(x => x.Id == request.MarkaVozilaId))
            {
                return RepositoryResult<bool>.Fail($"VEHICLE_BRAND_NOT_FOUND: Vehicle brand with the id " +
                    $"{request.MarkaVozilaId} doesnt exist");
            }

            return RepositoryResult<bool>.Ok(true);
        }

        public async Task<RepositoryResult<VehicleModelDto>> UpdateVehicleModelAsync(UpdateVehicleModelRequestDto request)
        {
            var validationResult = await ValidateVehicleModelUpdateRequestAsync(request);

            if (!validationResult.Success)
            {
                return RepositoryResult<VehicleModelDto>.Fail(validationResult.Message);
            }

            var vehicleModelDomain = mapper.Map<ModelVozila>(request);

            var updatedVehicleModelDomain = await vehicleModelRepository.
                UpdateAsync(vehicleModelDomain);

            var response = mapper.Map<VehicleModelDto>(updatedVehicleModelDomain);

            return RepositoryResult<VehicleModelDto>.Ok(response);
        }
    }
}






