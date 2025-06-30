using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegistracijaVozila.Models.Domain;
using RegistracijaVozila.Models.DTO;
using RegistracijaVozila.Repositories.Implementation;
using RegistracijaVozila.Repositories.Interface;
using RegistracijaVozila.Services.Interface;

namespace RegistracijaVozila.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleRepository vehicleRepository;
        private readonly IMapper mapper;
        private readonly IRegistrationVehicleRepository registrationVehicleRepository;
        private readonly IVehicleService vehicleService;

        public VehicleController(IVehicleRepository vehicleRepository, IMapper mapper,
            IRegistrationVehicleRepository registrationVehicleRepository, IVehicleService vehicleService)
        {
            this.vehicleRepository = vehicleRepository;
            this.mapper = mapper;
            this.registrationVehicleRepository = registrationVehicleRepository;
            this.vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var vehiclesDomainList = await vehicleRepository.GetAllAsync();

            var vehiclesDtoList = mapper.Map<List<VehicleDto>>(vehiclesDomainList);
            
            return Ok(vehiclesDtoList);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleRequestDto request)
        {
            var result = await vehicleService.CreateVehicleAsync(request);

            if (!result.Success)
            {
                var parts = result.Message?.Split(':', 2);
                return BadRequest(new ApiError
                {
                    ErrorCode = parts?[0],
                    Message = parts?.Length > 1 ? parts[1] : result.Message
                });
            }

            return CreatedAtAction(nameof(GetVehicleById), new { id = result.Data.Id }, result.Data);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetVehicleById([FromRoute] Guid id)
        {
            var vehicleDomain = await vehicleRepository.GetVehicleByIdAsync(id);

            if(vehicleDomain == null)
            {
                return NotFound(new ApiError
                {
                    ErrorCode = "VEHICLE_NOT_FOUND",
                    Message = $"Vehicle with the Id {id} was not found"
                });
            }

            var response = mapper.Map<VehicleDto>(vehicleDomain);

            return Ok(response);
            
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await vehicleService.DeleteVehicleAsync(id);

            if (!result.Success)
            {
                var parts = result.Message?.Split(':', 2);
                return BadRequest(new ApiError
                {
                    ErrorCode = parts[0],
                    Message = parts?.Length > 1 ? parts[1] : result.Message
                });
            }

            return Ok(result.Data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateVehicleDto updateRequest)
        {
            var result = await vehicleService.UpdateVehicleAsync(updateRequest);

            if (!result.Success)
            {
                var parts = result.Message?.Split(':', 2);
                return BadRequest(new ApiError
                {
                    ErrorCode = parts[0],
                    Message = parts?.Length > 1 ? parts[1] : result.Message
                });
            }

            return Ok(result.Data);
        }
    }
}




