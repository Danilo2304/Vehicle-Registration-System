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
            var response = await vehicleTypeService.GetAllAsync();

            return Ok(response);
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

            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await vehicleTypeService.GetById(id);

            if (!response.Success)
            {
                var parts = response.Message?.Split(":", 2);
                return BadRequest(new ApiError
                {
                    ErrorCode = parts?[0],
                    Message = parts?[1].Length > 1 ? parts[1] : response.Message
                });
            }

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

            return Ok(result);
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

            return Ok(result);
        }
    }
}
