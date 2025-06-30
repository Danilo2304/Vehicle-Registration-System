using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RegistracijaVozila.Models.Domain;
using RegistracijaVozila.Models.DTO;
using RegistracijaVozila.Repositories.Interface;
using RegistracijaVozila.Services.Interface;

namespace RegistracijaVozila.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleModelController : ControllerBase
    {
        private readonly IVehicleModelRepository vehicleModelRepository;
        private readonly IMapper mapper;
        private readonly IVehicleModelService vehicleModelService;

        public VehicleModelController(IVehicleModelRepository vehicleModelRepository, IMapper mapper,
            IVehicleModelService vehicleModelService)
        {
            this.vehicleModelRepository = vehicleModelRepository;
            this.mapper = mapper;
            this.vehicleModelService = vehicleModelService;
        }

        [HttpGet("List/{id:guid}")]
        public async Task<IActionResult> List(Guid id)
        {
            var vehicleModelDomainList = await vehicleModelRepository.ListByBrandId(id);

            if (!vehicleModelDomainList.Any())
            {
                return NotFound(new ApiError
                {
                    ErrorCode = "NO_MODELS_FOUND",
                    Message = $"No vehicle models found for brand with Id {id}"
                });
            }

            var vehicleModelDtoList = mapper.Map<List<VehicleModelDto>>(vehicleModelDomainList);

            return Ok(vehicleModelDtoList);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleModelRequestDto request)
        {
            var result = await vehicleModelService.CreateVehicleModelAsync(request);

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
            var vehicleModelDomain = await vehicleModelRepository.GetByIdAsync(id);

            if (vehicleModelDomain == null)
            {
                return NotFound(new ApiError
                {
                    ErrorCode = "VEHICLE_MODEL_NOT_FOUND",
                    Message = $"Vehicle model with the Id {id} was not found"
                });
            }

            var response = mapper.Map<VehicleModelDto>(vehicleModelDomain);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await vehicleModelService.DeleteVehicleModelAsync(id);

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
        public async Task<IActionResult> Update([FromBody] UpdateVehicleModelRequestDto request)
        {
            var result = await vehicleModelService.UpdateVehicleModelAsync(request);

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
