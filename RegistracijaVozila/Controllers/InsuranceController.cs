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
    public class InsuranceController : ControllerBase
    {
        private readonly IInsuranceService insuranceService;

        public InsuranceController(IInsuranceService insuranceService)
        {
            this.insuranceService = insuranceService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInsuranceRequestDto request)
        {
            var result = await insuranceService.CreateInsuranceAsync(request);

            return CreatedAtAction(nameof(GetById), new { result.Data.Id }, result);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var result = await insuranceService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await insuranceService.GetInsuranceByIdAsync(id);

            if (!result.Success)
            {
                var parts = result.Message.Split(":", 2);

                return BadRequest(new ApiError
                {
                    ErrorCode = parts?[0],
                    Message = parts?[1].Length > 1 ? parts[1] : result.Message
                });
            }

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await insuranceService.DeleteAsync(id);

            if (!result.Success)
            {
                var parts = result.Message.Split(":", 2);

                return BadRequest(new ApiError
                {
                    ErrorCode = parts?[0],
                    Message = parts?[1].Length > 1 ? parts[1] : result.Message
                });
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateInsuranceRequestDto request)
        {
            var result = await insuranceService.UpdateAsync(request);

            if (!result.Success)
            {
                var parts = result.Message.Split(":", 2);

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
