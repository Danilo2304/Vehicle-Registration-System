using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RegistracijaVozila.Data;
using RegistracijaVozila.Models.Domain;
using RegistracijaVozila.Models.DTO;
using RegistracijaVozila.Repositories.Implementation;
using RegistracijaVozila.Repositories.Interface;
using RegistracijaVozila.Results;
using RegistracijaVozila.Services.Interface;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RegistracijaVozila.Services.Implementation
{
    public class VehicleService : IVehicleService
    {
        private readonly RegistracijaVozilaDbContext appDbContext;
        private readonly IMapper mapper;
        private readonly IVehicleRepository vehicleRepository;

        public VehicleService(RegistracijaVozilaDbContext appDbContext, IMapper mapper, 
            IVehicleRepository vehicleRepository)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
            this.vehicleRepository = vehicleRepository;
        }

        public async Task<RepositoryResult<bool>> 
            ValidateVehicleCreateRequestAsync(CreateVehicleRequestDto request)
        {
            if (!await appDbContext.TipoviVozila.AnyAsync(t => t.Id == request.TipVozilaId))
                return RepositoryResult<bool>.Fail("TIP_NOT_FOUND: Vehicle type doesn't exist");

            if (!await appDbContext.MarkeVozila.AnyAsync(m => m.Id == request.MarkaVozilaId))
                return RepositoryResult<bool>.Fail("MARKA_NOT_FOUND: Vehicle brand doesn't exist");

            if (!await appDbContext.ModeliVozila.AnyAsync(m => m.Id == request.ModelVozilaId))
                return RepositoryResult<bool>.Fail("MODEL_NOT_FOUND: Vehicle model doesn't exist");

            var isValid = await appDbContext.ModeliVozila.Include(m => m.MarkaVozila)
                .AnyAsync(m => m.Id == request.ModelVozilaId &&
                               m.MarkaVozilaId == request.MarkaVozilaId &&
                               m.MarkaVozila.TipVozilaId == request.TipVozilaId);

            if (!isValid)
                return RepositoryResult<bool>.Fail("INVALID_COMBINATION: " +
                    "Model doesn't match brand and vehicle type");

            

            if (await appDbContext.Vozila.AnyAsync(x => x.BrojSasije == request.BrojSasije))
                return RepositoryResult<bool>.Fail("CHASSIS_NUMBER_EXISTS: Chassis number already used");

            if (request.GodinaProizvodnje < 1900 || request.GodinaProizvodnje > DateTime.Now.Year)
                return RepositoryResult<bool>.Fail("INVALID_YEAR: Invalid production year");

            if (request.DatumPrveRegistracije > request.DatumRegistracije)
                return RepositoryResult<bool>.Fail("DATE_MISMATCH: " +
                    "First registration must be before actual registration");

            if (request.SnagaMotora <= 0)
                return RepositoryResult<bool>.Fail("INVALID_ENGINE_POWER: " +
                    "Engine power must be greater than zero");

            if (request.GodinaProizvodnje > request.DatumPrveRegistracije.Year)
                return RepositoryResult<bool>.Fail("PRODUCTION_DATE_AFTER_FIRST_REGISTRATION: " +
                    "The car's production year must be before or equal to its first registration date.");

            return RepositoryResult<bool>.Ok(true);
        }

        public async Task<RepositoryResult<VehicleDto>> CreateVehicleAsync(CreateVehicleRequestDto request)
        {
            var validationResult = await ValidateVehicleCreateRequestAsync(request);
            if (!validationResult.Success)
                return RepositoryResult<VehicleDto>.Fail(validationResult.Message);

            var vehicleDomain = mapper.Map<Vozilo>(request);

            vehicleDomain = await vehicleRepository.AddAsync(vehicleDomain);

            var response = mapper.Map<VehicleDto>(vehicleDomain);

            return RepositoryResult<VehicleDto>.Ok(response, "New vehicle has successfully been created!");
        }


        public async Task<RepositoryResult<bool>?> ValidateVehicleDeleteRequestAsync(Guid id)
        {
            var existingVehicle = await appDbContext.Vozila.AnyAsync(x => x.Id == id);
            
            if(!existingVehicle)
            {
                return RepositoryResult<bool>.Fail($"VEHICLE_NOT_FOUND: Vehicle with Id {id} was not found");
            }

            var isRegistered = await appDbContext.Registracije.AnyAsync(x => x.VoziloId == id);

            if (isRegistered)
            {
                return RepositoryResult<bool>.Fail(
                    "VEHICLE_REGISTRATION_EXISTS: Vehicle can't be deleted because its registration still exists");
            }

            return RepositoryResult<bool>.Ok(true);
        }

        public async Task<RepositoryResult<VehicleDto>> DeleteVehicleAsync(Guid id)
        {
            var validationResult = await ValidateVehicleDeleteRequestAsync(id);

            if (!validationResult.Success)
            {
                return RepositoryResult<VehicleDto>.Fail(validationResult.Message);
            }

            var deletedVehicle = await vehicleRepository.DeleteVehicleAsync(id);

            var response = mapper.Map<VehicleDto>(deletedVehicle);

            return RepositoryResult<VehicleDto>.Ok(response, "Vehicle has successfully been deleted!");
        }

        public async Task<RepositoryResult<bool>?> ValidateVehicleUpdateRequestAsync(UpdateVehicleDto request)
        {
            if(request.Id == Guid.Empty)
            {
                return RepositoryResult<bool>.Fail("INVALID_VEHICLE_ID: " +
                    "Vehicle ID is required and cannot be empty");
            }

            if(!await appDbContext.Vozila.AnyAsync(x => x.Id == request.Id))
            {
                return RepositoryResult<bool>.Fail($"VEHICLE_NOT_FOUND: " +
                    $"Vehicle with the Id {request?.Id} was not found");
            }

            if (!await appDbContext.TipoviVozila.AnyAsync(t => t.Id == request.TipVozilaId))
                return RepositoryResult<bool>.Fail("TIP_NOT_FOUND: Vehicle type doesn't exist");

            if (!await appDbContext.MarkeVozila.AnyAsync(m => m.Id == request.MarkaVozilaId))
                return RepositoryResult<bool>.Fail("MARKA_NOT_FOUND: Vehicle brand doesn't exist");

            if (!await appDbContext.ModeliVozila.AnyAsync(m => m.Id == request.ModelVozilaId))
                return RepositoryResult<bool>.Fail("MODEL_NOT_FOUND: Vehicle model doesn't exist");

            var isValid = await appDbContext.ModeliVozila.Include(m => m.MarkaVozila)
                .AnyAsync(m => m.Id == request.ModelVozilaId &&
                               m.MarkaVozilaId == request.MarkaVozilaId &&
                               m.MarkaVozila.TipVozilaId == request.TipVozilaId);

            if (!isValid)
                return RepositoryResult<bool>.Fail("INVALID_COMBINATION: " +
                    "Model doesn't match brand and vehicle type");

            

            if (await appDbContext.Vozila.AnyAsync(x => x.BrojSasije == request.BrojSasije &&
            x.Id!=request.Id))
                return RepositoryResult<bool>.Fail("CHASSIS_NUMBER_EXISTS: Chassis number already used");

            if (request.GodinaProizvodnje < 1900 || request.GodinaProizvodnje > DateTime.Now.Year)
                return RepositoryResult<bool>.Fail("INVALID_YEAR: Invalid production year");

            if (request.DatumPrveRegistracije > request.DatumRegistracije)
                return RepositoryResult<bool>.Fail("DATE_MISMATCH: " +
                    "First registration must be before actual registration");

            if (request.SnagaMotora <= 0)
                return RepositoryResult<bool>.Fail("INVALID_ENGINE_POWER: " +
                    "Engine power must be greater than zero");

            if (request.GodinaProizvodnje > request.DatumPrveRegistracije.Year)
                return RepositoryResult<bool>.Fail("PRODUCTION_DATE_AFTER_FIRST_REGISTRATION: " +
                    "The car's production year must be before or equal to its first registration date.");

            return RepositoryResult<bool>.Ok(true);
        }

        public async Task<RepositoryResult<VehicleDto>> UpdateVehicleAsync(UpdateVehicleDto request)
        {
            var validationResut = await ValidateVehicleUpdateRequestAsync(request);

            if (!validationResut.Success)
            {
                return RepositoryResult<VehicleDto>.Fail(validationResut.Message);
            }

            var vehicleDomain = mapper.Map<Vozilo>(request);

            var updatedVehicle = await vehicleRepository.UpdateVehicleAsync(vehicleDomain);

            var response = mapper.Map<VehicleDto>(updatedVehicle);

            return RepositoryResult<VehicleDto>.Ok(response, "Vehicle has successfully been updated!");
        }

        public async Task<RepositoryResult<PagedResult<VehicleDto>>> GetAllAsync(string? searchQuery = null, int pageSize = 1000, int pageNumber = 1)
        {
            var (vehicles, totalCount) = await vehicleRepository.GetAllAsync(searchQuery, pageSize, pageNumber);

            var response = new PagedResult<VehicleDto>
            {
                Items = mapper.Map<List<VehicleDto>>(vehicles),
                TotalCount = totalCount
            };

            return RepositoryResult<PagedResult<VehicleDto>>.Ok(response);
        }

        public async Task<RepositoryResult<bool>?> ValidateGetVehicleByIdAsyncRequestAsync(Guid id)
        {
            if(!await appDbContext.Vozila.AnyAsync(x=>x.Id == id))
            {
                return RepositoryResult<bool>.Fail($"VEHICLE_NOT_FOUND: Vehicle with the Id {id} was not found");
            }

            return RepositoryResult<bool>.Ok(true);
        }

        public async Task<RepositoryResult<VehicleDto>> GetVehicleByIdAsync(Guid id)
        {
            var validationResult = await ValidateGetVehicleByIdAsyncRequestAsync(id);

            if (!validationResult.Success)
            {
                return RepositoryResult<VehicleDto>.Fail(validationResult.Message);
            }

            var vehicleDomain = await vehicleRepository.GetVehicleByIdAsync(id);

            var response = mapper.Map<VehicleDto>(vehicleDomain);

            return RepositoryResult<VehicleDto>.Ok(response);
        }
    }
}


