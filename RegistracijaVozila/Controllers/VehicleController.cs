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
        private readonly IVehicleService vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] string? searchQuery, [FromQuery] int pageSize = 1000,
            [FromQuery] int pageNumber = 1)
        {
            var result = await vehicleService.GetAllAsync(searchQuery, pageSize, pageNumber);
            
            return Ok(result);
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

            return CreatedAtAction(nameof(GetVehicleById), new { id = result.Data.Id }, result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetVehicleById([FromRoute] Guid id)
        {
            var result  = await vehicleService.GetVehicleByIdAsync(id);

            if(!result.Success)
            {
                var parts = result.Message?.Split(':', 2);
                return BadRequest(new ApiError
                {
                    ErrorCode = parts?[0],
                    Message = parts?.Length > 1 ? parts[1] : result.Message
                });
            }

            return Ok(result);
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

            return Ok(result);
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

            return Ok(result);
        }
    }
}




