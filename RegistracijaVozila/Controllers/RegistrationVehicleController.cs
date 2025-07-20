using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
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
        private readonly IRegistrationVehicleService registrationVehicleService;
        private readonly IEmailService emailService;

        public RegistrationVehicleController(IRegistrationVehicleService registrationVehicleService,
            IEmailService emailService)
        {
            this.registrationVehicleService = registrationVehicleService;
            this.emailService = emailService;
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
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

            var confirmationData = await registrationVehicleService.GenerateConfirmation(result.Data.Id);
            var document = new ConfirmationRegistrationDocument(confirmationData.Data);
            var pdfBytes = document.GeneratePdf();
            await emailService.SendConfirmationEmailAsync(confirmationData.Data.Vlasnik.Email, pdfBytes);

            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result);
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] string? searchQuery, [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 1000)
        {
            var result = await 
                registrationVehicleService.GetAllAsync(searchQuery, pageNumber, pageSize);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await registrationVehicleService.GetByIdAsync(id);

            if (!result.Success)
            {
                var parts = result.Message?.Split(":", 2);

                return BadRequest(new ApiError
                {
                    ErrorCode = parts?[0],
                    Message = parts?[1].Length > 1 ? parts?[1] : result.Message
                });
            }

            return Ok(result);
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
            return Ok(result);
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

            return Ok(result);
        }

        [HttpGet("potvrda/{id}")]
        public async Task<IActionResult> GenerateConfirmation(Guid id)
        {
            var result = await registrationVehicleService.GenerateConfirmation(id);

            var document = new ConfirmationRegistrationDocument(result.Data);
            var pdfBytes = document.GeneratePdf();

            return File(pdfBytes, "application/pdf", "PotvrdaRegistracije.pdf");
        }

        [HttpGet("potvrda-mail/{id}")]
        public async Task<IActionResult> GenerateAndSendConfirmation(Guid id)
        {
            var result = await registrationVehicleService.GenerateConfirmation(id);

            var document = new ConfirmationRegistrationDocument(result.Data);
            var pdfBytes = document.GeneratePdf();

            await emailService.SendConfirmationEmailAsync(result.Data.Vlasnik.Email, pdfBytes);

            return Ok("PDF potvrda je poslata mejlom");
        }
    }
}



