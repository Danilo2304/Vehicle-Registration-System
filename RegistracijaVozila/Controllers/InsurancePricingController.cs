using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegistracijaVozila.Models.DTO;
using RegistracijaVozila.Services.Interface;

namespace RegistracijaVozila.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsurancePricingController : ControllerBase
    {
        private readonly IInsurancePricingService insurancePricingService;

        public InsurancePricingController(IInsurancePricingService insurancePricingService)
        {
            this.insurancePricingService = insurancePricingService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var result = await insurancePricingService.GetAllAsync();

            if (!result.Success)
            {
                var parts = result.Message.Split(":", 2);

                return BadRequest(new ApiError
                {
                    ErrorCode = parts?[0],
                    Message = parts?[1].Length > 1 ? parts?[1] : result.Message
                });
            }

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await insurancePricingService.GetByIdAsync(id);

            if (!result.Success)
            {
                var parts = result.Message.Split(":", 2);

                return BadRequest(new ApiError
                {
                    ErrorCode = parts?[0],
                    Message = parts?[1].Length > 1 ? parts?[1] : result.Message
                });
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInsurancePriceRequestDto request)
        {
            var result = await insurancePricingService.CreateAsync(request);

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
    }
}
