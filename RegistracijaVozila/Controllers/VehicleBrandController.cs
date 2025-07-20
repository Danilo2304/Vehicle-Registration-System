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
            var response = await vehicleBrandService.GetListByType(id);

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

            return CreatedAtAction(nameof(GetById), new {id = result.Data.Id},result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await vehicleBrandService.GetById(id);

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

            return Ok(result);
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

            return Ok(result);
        }
    }
}
