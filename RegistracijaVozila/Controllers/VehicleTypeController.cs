using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegistracijaVozila.Models.Domain;
using RegistracijaVozila.Models.DTO;
using RegistracijaVozila.Repositories.Interface;
using RegistracijaVozila.Services.Interface;

namespace RegistracijaVozila.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTypeController : ControllerBase
    {
        private readonly IVehicleTypeRepository vehicleTypeRepository;
        private readonly IMapper mapper;
        private readonly IVehicleTypeService vehicleTypeService;

        public VehicleTypeController(IVehicleTypeRepository vehicleTypeRepository, IMapper mapper,
            IVehicleTypeService vehicleTypeService)
        {
            this.vehicleTypeRepository = vehicleTypeRepository;
            this.mapper = mapper;
            this.vehicleTypeService = vehicleTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var vehicleTypeDomainList = await vehicleTypeRepository.GetAllAsync();

            var vehicleTypeDtoList = mapper.Map<List<VehicleTypeDto>>(vehicleTypeDomainList);

            return Ok(vehicleTypeDtoList);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleTypeRequestDto request)
        {
            var result = await vehicleTypeService.CreateVehicleTypeAsync(request);

            if (!result.Success)
            {
                var parts = result.Message?.Split(":", 2);
                return BadRequest(new ApiError
                {
                    ErrorCode = parts?[0],
                    Message = parts?[1].Length > 1 ? parts[1] : result.Message
                });
            }



            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var vehicleTypeDomain = await vehicleTypeRepository.GetByIdAsync(id);

            if(vehicleTypeDomain == null)
            {
                return NotFound(new ApiError
                {
                    ErrorCode = "VEHICLE_TYPE_NOT_FOUND",
                    Message = $"Vehicle type with the Id {id} was not found"
                });
            }

            var response = mapper.Map<VehicleTypeDto>(vehicleTypeDomain);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await vehicleTypeService.DeleteVehicleTypeAsync(id);

            if (!result.Success)
            {
                var parts = result.Message?.Split(":", 2);
                return BadRequest(new ApiError
                {
                    ErrorCode = parts?[0],
                    Message = parts?[1].Length > 1 ? parts[1] : result.Message
                });
            }

            return Ok(result.Data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateVehicleTypeRequestDto request)
        {
            var result = await vehicleTypeService.UpdateVehicleTypeAsync(request);

            if (!result.Success)
            {
                var parts = result.Message?.Split(":", 2);
                return BadRequest(new ApiError
                {
                    ErrorCode = parts?[0],
                    Message = parts?[1].Length > 1 ? parts[1] : result.Message
                });

            }

            return Ok(result.Data);
        }
    }
}
