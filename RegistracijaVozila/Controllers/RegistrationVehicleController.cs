using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistracijaVozila.Data;
using RegistracijaVozila.Models.Domain;
using RegistracijaVozila.Models.DTO;
using RegistracijaVozila.Repositories.Interface;
using RegistracijaVozila.Services.Implementation;
using RegistracijaVozila.Services.Interface;

namespace RegistracijaVozila.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationVehicleController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IRegistrationVehicleRepository registrationVehicleRepository;
        private readonly RegistracijaVozilaDbContext appDbContext;
        private readonly IRegistrationVehicleService registrationVehicleService;

        public RegistrationVehicleController(IMapper mapper, 
            IRegistrationVehicleRepository registrationVehicleRepository, 
            RegistracijaVozilaDbContext appDbContext, IRegistrationVehicleService registrationVehicleService)
        {
            this.mapper = mapper;
            this.registrationVehicleRepository = registrationVehicleRepository;
            this.appDbContext = appDbContext;
            this.registrationVehicleService = registrationVehicleService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRegistrationVehicleRequestDto request)
        {
            var result = await registrationVehicleService.CreateRegistrationAsync(request);

            if (!result.Success)
            {
                var parts = result.Message?.Split(":", 2);

                return BadRequest(new ApiError
                {
                    ErrorCode = parts?[0],
                    Message = parts?[1].Length > 1 ? parts?[1] : result.Message
                });
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var registrationVehicleDomainList = await registrationVehicleRepository.GetAllAsync();

            var registrationVehicleDtoList = mapper.Map<List<RegistrationVehicleDto>>
                (registrationVehicleDomainList);

            return Ok(registrationVehicleDtoList);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var registrationVehicleDomain = await registrationVehicleRepository.GetByIdAsync(id);

            if (registrationVehicleDomain == null)
            {
                return NotFound(new ApiError
                {
                    ErrorCode = "REGISTRATION_VEHICLE_NOT_FOUND",
                    Message = $"Vehicle registration with the Id {id} was not found"
                });
            }

            var response = mapper.Map<RegistrationVehicleDto>(registrationVehicleDomain);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await registrationVehicleService.DeleteRegistrationAsync(id);

            if (!result.Success)
            {
                var parts = result.Message?.Split(":", 2);

                return BadRequest(new ApiError
                {
                    ErrorCode = parts?[0],
                    Message = parts?[1].Length > 1 ? parts?[1] : result.Message
                });
            }
            return Ok(result.Data);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateRegistrationVehicleRequestDto request)
        {
            var result = await registrationVehicleService.UpdateRegistrationAsync(request);

            if (!result.Success)
            {
                var parts = result.Message?.Split(":", 2);

                return BadRequest(new ApiError
                {
                    ErrorCode = parts?[0],
                    Message = parts?[1].Length > 1 ? parts?[1] : result.Message
                });
            }

            return Ok(result.Data);
        }
    }
}



