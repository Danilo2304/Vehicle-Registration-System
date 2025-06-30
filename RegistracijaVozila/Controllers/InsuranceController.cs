using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegistracijaVozila.Models.Domain;
using RegistracijaVozila.Models.DTO;
using RegistracijaVozila.Repositories.Interface;

namespace RegistracijaVozila.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceController : ControllerBase
    {
        private readonly IInsuranceRepository insuranceRepository;
        private readonly IMapper mapper;

        public InsuranceController(IInsuranceRepository insuranceRepository, IMapper mapper)
        {
            this.insuranceRepository = insuranceRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInsuranceRequestDto request)
        {
            var insuranceDomain = mapper.Map<Osiguranje>(request);

            insuranceDomain = await insuranceRepository.CreateInsuranceAsync(insuranceDomain);

            var response = mapper.Map<InsuranceDto>(insuranceDomain);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var insuranceDomainList = await insuranceRepository.GetAllAsync();

            if (insuranceDomainList == null || !insuranceDomainList.Any())
            {
                return NotFound("No insurance records found");
            }

            var insuranceDtoList = mapper.Map<List<InsuranceDto>>(insuranceDomainList);
            

            return Ok(insuranceDtoList);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var insuranceDomain = await insuranceRepository.GetInsuranceByIdAsync(id);

            if(insuranceDomain == null)
            {
                return NotFound($"No insurance with the ID {id} was found.");
            }

            var response = mapper.Map<InsuranceDto>(insuranceDomain);

            return Ok(response);
        }
    }
}
