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
    public class VehicleBrandService : IVehicleBrandService
    {
        private readonly RegistracijaVozilaDbContext appDbContext;
        private readonly IVehicleBrandRepository vehicleBrandRepository;
        private readonly IMapper mapper;

        public VehicleBrandService(RegistracijaVozilaDbContext appDbContext, 
            IVehicleBrandRepository vehicleBrandRepository,
            IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.vehicleBrandRepository = vehicleBrandRepository;
            this.mapper = mapper;
        }

        public async Task<RepositoryResult<bool>> 
            ValidateVehicleBrandCreateRequestAsync(CreateVehicleBrandRequestDto request)
        {
            var existingBrand = await appDbContext.MarkeVozila.AnyAsync(
                x => x.Naziv.ToLower() == request.Naziv.ToLower() &&
                x.TipVozilaId == request.TipVozilaId);

            if (existingBrand)
            {
                return RepositoryResult<bool>.Fail("VEHICLE_BRAND_EXISTS: " +
                    "Brand already exists for the given vehicle type");
            }

            if(!await appDbContext.TipoviVozila.AnyAsync(x=>x.Id == request.TipVozilaId))
            {
                return RepositoryResult<bool>.Fail($"VEHICLE_TYPE_NOT_FOUND: Vehicle type with the id " +
                    $"{request.TipVozilaId} doesnt exist");
            }

            return RepositoryResult<bool>.Ok(true);
        }

        public async Task<RepositoryResult<VehicleBrandDto>> 
            CreateVehicleBrand(CreateVehicleBrandRequestDto request)
        {
            var validationResult = await ValidateVehicleBrandCreateRequestAsync(request);

            if (!validationResult.Success)
            {
                return RepositoryResult<VehicleBrandDto>.Fail(validationResult.Message);
            }

            var vehicleBrandDomain = mapper.Map<MarkaVozila>(request);
            var result = await vehicleBrandRepository.AddAsync(vehicleBrandDomain);

            var response = mapper.Map<VehicleBrandDto>(result);

            return RepositoryResult<VehicleBrandDto>.Ok(response);
        }

        public async Task<RepositoryResult<bool>> ValidateVehicleBrandDeleteRequestAsync(Guid id)
        {
            if(await appDbContext.ModeliVozila.AnyAsync(x=>x.MarkaVozilaId == id))
            {
                return RepositoryResult<bool>.Fail("VEHICLE_BRAND_HAS_MODELS: " +
                    " Vehicle brand has models and can't be deleted");
            }

            if(!await appDbContext.MarkeVozila.AnyAsync(x=>x.Id == id))
            {
                return RepositoryResult<bool>.Fail($"VEHICLE_BRAND_NOT_FOUND: " +
                    $"Vehicle brand with Id {id} was not found");
            }

            if(await appDbContext.Vozila.AnyAsync(x=>x.MarkaVozilaId == id))
            {
                return RepositoryResult<bool>.Fail("VEHICLE_BRAND_IN_USE: " +
                    "Cannot delete brand because it's assigned to existing vehicles");
            }

            return RepositoryResult<bool>.Ok(true);
        }

        public async Task<RepositoryResult<VehicleBrandDto>> DeleteVehicleBrand(Guid id)
        {
            var validationResult = await ValidateVehicleBrandDeleteRequestAsync(id);

            if (!validationResult.Success)
            {
                return RepositoryResult<VehicleBrandDto>.Fail(validationResult.Message);
            }

            var vehicleDomain = await vehicleBrandRepository.DeleteAsync(id);

            var response = mapper.Map<VehicleBrandDto>(vehicleDomain);

            return RepositoryResult<VehicleBrandDto>.Ok(response);
        }

        public async Task<RepositoryResult<bool>> 
            ValidateVehicleBrandUpdateRequestAsync(UpdateVehicleBrandRequestDto request)
        {
            if (!await appDbContext.MarkeVozila.AnyAsync(x => x.Id == request.Id))
            {
                return RepositoryResult<bool>.Fail($"VEHICLE_BRAND_NOT_FOUND: " +
                    $"Vehicle brand with the id {request.Id} doesnt exist");
            }

            var existingBrand = await appDbContext.MarkeVozila.AnyAsync(
                x => x.Naziv.ToLower() == request.Naziv.ToLower() &&
                x.TipVozilaId == request.TipVozilaId && x.Id!=request.Id);

            if (existingBrand)
            {
                return RepositoryResult<bool>.Fail("VEHICLE_BRAND_EXISTS: " +
                    "Brand already exists for the given vehicle type");
            }

            if (!await appDbContext.TipoviVozila.AnyAsync(x => x.Id == request.TipVozilaId))
            {
                return RepositoryResult<bool>.Fail($"VEHICLE_TYPE_NOT_FOUND: Vehicle type with the id " +
                    $"{request.TipVozilaId} doesnt exist");
            }

            return RepositoryResult<bool>.Ok(true);
        }

        public async Task<RepositoryResult<VehicleBrandDto>> 
            UpdateVehicleBrand(UpdateVehicleBrandRequestDto request)
        {
            var validationResult = await ValidateVehicleBrandUpdateRequestAsync(request);

            if (!validationResult.Success)
            {
                return RepositoryResult<VehicleBrandDto>.Fail(validationResult.Message);
            }

            var vehicleBrandDomain = mapper.Map<MarkaVozila>(request);

            var updatedBrandVehicle = await vehicleBrandRepository.UpdateAsync(vehicleBrandDomain);

            var response = mapper.Map<VehicleBrandDto>(updatedBrandVehicle);

            return RepositoryResult<VehicleBrandDto>.Ok(response);
        }
    }
}


