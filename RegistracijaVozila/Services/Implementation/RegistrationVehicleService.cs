using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens.Experimental;
using RegistracijaVozila.Data;
using RegistracijaVozila.Models.Domain;
using RegistracijaVozila.Models.DTO;
using RegistracijaVozila.Repositories.Interface;
using RegistracijaVozila.Results;
using RegistracijaVozila.Services.Interface;

namespace RegistracijaVozila.Services.Implementation
{
    public class RegistrationVehicleService : IRegistrationVehicleService
    {
        private readonly RegistracijaVozilaDbContext appDbContext;
        private readonly IMapper mapper;
        private readonly IRegistrationVehicleRepository registrationVehicleRepository;

        public RegistrationVehicleService(RegistracijaVozilaDbContext appDbContext,
            IMapper mapper, IRegistrationVehicleRepository registrationVehicleRepository)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
            this.registrationVehicleRepository = registrationVehicleRepository;
        }

        public async Task<RepositoryResult<bool>>
            ValidateRegistrationCreateRequestAsync(CreateRegistrationVehicleRequestDto request)
        {


            if (await appDbContext.Registracije.AnyAsync(x => x.VoziloId == request.VoziloId))
            {
                return RepositoryResult<bool>.Fail("VEHICLE_ALREADY_REGISTERED: " +
                    "Registration cannot be done because vehicle is already registered");
            }

            if (request.DatumRegistracije > DateTime.Now)
            {
                return RepositoryResult<bool>.Fail("REGISTRATION_INVALID_DATE: Date of registration cannot be in the future");
            }

            if (request.CijenaRegistracije <= 0)
            {
                return RepositoryResult<bool>.Fail("REGISTRATION_INVALID_PRICE: " +
                    "Price of registration cannot be lower then 0");
            }

            if (!await appDbContext.Vozila.AnyAsync(x => x.Id == request.VoziloId))
            {
                return RepositoryResult<bool>.Fail($"REGISTRATION_VEHICLE_INVALID_ID: " +
                    $"Vehicle with the id {request.VoziloId} doesnt exist");
            }

            if(!await appDbContext.Klijenti.AnyAsync(x=> x.Id == request.KlijentId))
            {
                return RepositoryResult<bool>.Fail($"REGISTRATION_CLIENT_INVALID_ID: " +
                    $"Client with the id {request.KlijentId} doesnt exist");
            }

            return RepositoryResult<bool>.Ok(true);
        }

        public async Task<RepositoryResult<RegistrationVehicleDto>>
            CreateRegistrationAsync(CreateRegistrationVehicleRequestDto request)
        {
            var validationResult = await ValidateRegistrationCreateRequestAsync(request);

            if (!validationResult.Success)
            {
                return RepositoryResult<RegistrationVehicleDto>.Fail(validationResult.Message);
            }

            var domainRegistration = mapper.Map<Registracija>(request);

            domainRegistration.OsiguranjeRegistracija = request.OsiguranjeIds
                .Select(id => new OsiguranjeRegistracija
                {
                    OsiguranjeVozilaId = id
                }).ToList();

            domainRegistration = await registrationVehicleRepository.AddRegistrationAsync(domainRegistration);

            var response = mapper.Map<RegistrationVehicleDto>(domainRegistration);

            return RepositoryResult<RegistrationVehicleDto>.Ok(response);
        }

        public async Task<RepositoryResult<bool>?> ValidateRegistrationDeleteRequestAsync(Guid id)
        {
            if (!await appDbContext.Registracije.AnyAsync(x => x.Id == id))
            {
                return RepositoryResult<bool>.Fail($"REGISTRATION_INVALID_ID: Registration with the id {id} doesnt exist");
            }

            return RepositoryResult<bool>.Ok(true);
        }

        public async Task<RepositoryResult<RegistrationVehicleDto>> DeleteRegistrationAsync(Guid id)
        {
            var validationResult = await ValidateRegistrationDeleteRequestAsync(id);

            if (!validationResult.Success)
            {
                return RepositoryResult<RegistrationVehicleDto>.Fail(validationResult.Message);
            }

            var deletedRegistrationDomain = await registrationVehicleRepository.DeleteAsync(id);

            var response = mapper.Map<RegistrationVehicleDto>(deletedRegistrationDomain);

            return RepositoryResult<RegistrationVehicleDto>.Ok(response);
        }

        public async Task<RepositoryResult<bool>?>
            ValidateRegistrationUpdateRequestAsync(UpdateRegistrationVehicleRequestDto request)
        {

            if (!await appDbContext.Registracije.AnyAsync(x => x.Id == request.Id))
            {
                return RepositoryResult<bool>.Fail($"REGISTRATION_INVALID_ID: " +
                    $"Registration with the id {request.Id} doesnt exist");
            }

            if (await appDbContext.Registracije.AnyAsync(x => x.VoziloId == request.VoziloId && x.Id!=request.Id))
            {
                return RepositoryResult<bool>.Fail("VEHICLE_ALREADY_REGISTERED: " +
                    "Registration cannot be done because vehicle is already registered");
            }

            if (request.DatumRegistracije > DateTime.Now)
            {
                return RepositoryResult<bool>.Fail("REGISTRATION_INVALID_DATE: Date of registration cannot be in the future");
            }

            if (request.CijenaRegistracije <= 0)
            {
                return RepositoryResult<bool>.Fail("REGISTRATION_INVALID_PRICE: " +
                    "Price of registration cannot be lower then 0");
            }

            if (!await appDbContext.Vozila.AnyAsync(x => x.Id == request.VoziloId))
            {
                return RepositoryResult<bool>.Fail($"REGISTRATION_VEHICLE_INVALID_ID: " +
                    $"Vehicle with the id {request.VoziloId} doesnt exist");
            }

            if (!await appDbContext.Klijenti.AnyAsync(x => x.Id == request.KlijentId))
            {
                return RepositoryResult<bool>.Fail($"REGISTRATION_CLIENT_INVALID_ID: " +
                    $"Client with the id {request.KlijentId} doesnt exist");
            }

            return RepositoryResult<bool>.Ok(true);
        }

        public async Task<RepositoryResult<RegistrationVehicleDto>>
            UpdateRegistrationAsync(UpdateRegistrationVehicleRequestDto request)
        {
            var validationResult = await ValidateRegistrationUpdateRequestAsync(request);

            if (!validationResult.Success)
            {
                return RepositoryResult<RegistrationVehicleDto>.Fail(validationResult.Message);
            }

            var registrationDomain = mapper.Map<Registracija>(request);

            registrationDomain.OsiguranjeRegistracija = request.OsiguranjeIds
                .Select(id => new OsiguranjeRegistracija
                {
                    OsiguranjeVozilaId = id
                }).ToList();

            var result = await registrationVehicleRepository.UpdateAsync(registrationDomain);

            var response = mapper.Map<RegistrationVehicleDto>(result);

            return RepositoryResult<RegistrationVehicleDto>.Ok(response);
        }
    }
}






