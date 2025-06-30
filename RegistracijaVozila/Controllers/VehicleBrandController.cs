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
    public class VehicleBrandController : ControllerBase
    {
        private readonly IVehicleBrandRepository vehicleBrandRepository;
        private readonly IMapper mapper;
        private readonly IVehicleBrandService vehicleBrandService;

        public VehicleBrandController(IVehicleBrandRepository vehicleBrandRepository, IMapper mapper, 
            IVehicleBrandService vehicleBrandService)
        {
            this.vehicleBrandRepository = vehicleBrandRepository;
            this.mapper = mapper;
            this.vehicleBrandService = vehicleBrandService;
        }

        [HttpGet("ListById/{id:guid}")]
        public async Task<IActionResult> List(Guid id)
        {
            var vehicleBrandDomainList = await vehicleBrandRepository.ListByTypeId(id);

            if (!vehicleBrandDomainList.Any())
            {
                return NotFound(new ApiError
                {
                    ErrorCode = "NO_BRANDS_FOUND",
                    Message = $"No vehicle brands found for type with Id {id}"
                });
            }

            var vehicleBrandDtoList = mapper.Map<List<VehicleBrandDto>>(vehicleBrandDomainList);

            return Ok(vehicleBrandDtoList);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleBrandRequestDto request)
        {
            var result = await vehicleBrandService.CreateVehicleBrand(request);

            if (!result.Success)
            {
                var parts = result.Message?.Split(":", 2);

                return BadRequest(new ApiError
                {
                    ErrorCode = parts?[0],
                    Message = parts?[1].Length > 1 ? parts[1] : result.Message
                });
            }

            return CreatedAtAction(nameof(GetById), new {id = result.Data.Id},result.Data);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var vehicleBrandDomain = await vehicleBrandRepository.GetByIdAsync(id);

            if (vehicleBrandDomain == null)
            {
                return NotFound(new ApiError
                {
                    ErrorCode = "VEHICLE_BRAND_NOT_FOUND",
                    Message = $"Vehicle brand with the Id {id} was not found"
                });
            }

            var response = mapper.Map<VehicleBrandDto>(vehicleBrandDomain);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await vehicleBrandService.DeleteVehicleBrand(id);

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
        public async Task<IActionResult> Update([FromBody] UpdateVehicleBrandRequestDto request)
        {
            var result = await vehicleBrandService.UpdateVehicleBrand(request);

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
