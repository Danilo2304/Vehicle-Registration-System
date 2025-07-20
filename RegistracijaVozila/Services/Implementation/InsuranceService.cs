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
    public class InsuranceService : IInsuranceService
    {
        private readonly IInsuranceRepository insuranceRepository;
        private readonly IMapper mapper;
        private readonly RegistracijaVozilaDbContext appDbContext;

        public InsuranceService(IInsuranceRepository insuranceRepository, IMapper mapper, RegistracijaVozilaDbContext appDbContext)
        {
            this.insuranceRepository = insuranceRepository;
            this.mapper = mapper;
            this.appDbContext = appDbContext;
        }

        public async Task<RepositoryResult<InsuranceDto>> CreateInsuranceAsync(CreateInsuranceRequestDto request)
        {
            var insuranceDomain = mapper.Map<Osiguranje>(request);

            insuranceDomain = await insuranceRepository.CreateInsuranceAsync(insuranceDomain);

            var response = mapper.Map<InsuranceDto>(insuranceDomain);

            return RepositoryResult<InsuranceDto>.Ok(response, "New insurance has been created!");
        }

        public async Task<RepositoryResult<List<InsuranceDto>>> GetAllAsync()
        {
            var insuranceDomainList = await insuranceRepository.GetAllAsync();

            var response = mapper.Map<List<InsuranceDto>>(insuranceDomainList);

            return RepositoryResult<List<InsuranceDto>>.Ok(response);
        }

        public async Task<RepositoryResult<bool>> ValidateGetInsuranceByIdAsync(Guid id)
        {
            if(!await appDbContext.Osiguranja.AnyAsync(x=>x.Id == id))
            {
                return RepositoryResult<bool>.Fail($"INVALID_INSURANCE_ID: Insurance with the" +
                    $" id {id} not found");
            }

            return RepositoryResult<bool>.Ok(true);
        }

        public async Task<RepositoryResult<InsuranceDto>> GetInsuranceByIdAsync(Guid id)
        {
            var validationResult = await ValidateGetInsuranceByIdAsync(id);

            if (!validationResult.Success)
            {
                return RepositoryResult<InsuranceDto>.Fail(validationResult.Message);
            }

            var insuranceDomain = await insuranceRepository.GetInsuranceByIdAsync(id);

            var response = mapper.Map<InsuranceDto>(insuranceDomain);

            return RepositoryResult<InsuranceDto>.Ok(response);
        }

        public async Task<RepositoryResult<bool>> ValidateDeleteAsync(Guid id)
        {
            if (!await appDbContext.Osiguranja.AnyAsync(x => x.Id == id))
            {
                return RepositoryResult<bool>.Fail($"INVALID_INSURANCE_ID: Insurance with the" +
                    $" id {id} not found");
            }

            return RepositoryResult<bool>.Ok(true);
        }

        public async Task<RepositoryResult<InsuranceDto>> DeleteAsync(Guid id)
        {
            var validationResult = await ValidateGetInsuranceByIdAsync(id);

            if (!validationResult.Success)
            {
                return RepositoryResult<InsuranceDto>.Fail(validationResult.Message);
            }

            var insuranceDomain = await insuranceRepository.DeleteAsync(id);

            var response = mapper.Map<InsuranceDto>(insuranceDomain);

            return RepositoryResult<InsuranceDto>.Ok(response, "Insurance has been deleted");
        }

        public async Task<RepositoryResult<bool>> ValidateUpdateAsync(UpdateInsuranceRequestDto request)
        {
            if (!await appDbContext.Osiguranja.AnyAsync(x => x.Id == request.Id))
            {
                return RepositoryResult<bool>.Fail($"INVALID_INSURANCE_ID: Insurance with the" +
                    $" id {request.Id} not found");
            }

            return RepositoryResult<bool>.Ok(true);
        }

        public async Task<RepositoryResult<InsuranceDto>> UpdateAsync(UpdateInsuranceRequestDto request)
        {
            var validationResult = await ValidateUpdateAsync(request);

            if (!validationResult.Success)
            {
                return RepositoryResult<InsuranceDto>.Fail(validationResult.Message);
            }

            var insuranceDomain = mapper.Map<Osiguranje>(request);

            var updatedInsurance = await insuranceRepository.UpdateAsync(insuranceDomain);

            var response = mapper.Map<InsuranceDto>(updatedInsurance);

            return RepositoryResult<InsuranceDto>.Ok(response, "Insurance has been updated!");
        }
    }
}





