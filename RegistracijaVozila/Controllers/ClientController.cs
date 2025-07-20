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
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository clientRepository;
        private readonly IMapper mapper;
        private readonly IRegistrationVehicleRepository registrationVehicleRepository;
        private readonly IClientService clientService;

        public ClientController(IClientRepository clientRepository, IMapper mapper,
            IRegistrationVehicleRepository registrationVehicleRepository, IClientService clientService)
        {
            this.clientRepository = clientRepository;
            this.mapper = mapper;
            this.registrationVehicleRepository = registrationVehicleRepository;
            this.clientService = clientService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClientRequestDto request)
        {
            var result = await clientService.CreateClientAsync(request);

            if (!result.Success)
            {
                var parts = result.Message?.Split(":", 2);

                return BadRequest(new ApiError
                {
                    ErrorCode = parts?[0],
                    Message = parts?[1].Length > 1 ? parts[1] : result.Message
                });
            }

            return CreatedAtAction(nameof(GetClientById), new { id = result.Data.Id}, result);
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] string? searchQuery,[FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 1000)
        {
            var response = await clientService.GetClientsAsync(searchQuery, pageNumber, pageSize);
            
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetClientById([FromRoute] Guid id)
        {
            var response = await clientService.GetClijentByIdAsync(id);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await clientService.DeleteClientAsync(id);

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

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateClientRequestDto updateRequest)
        {
            var result = await clientService.UpdateClientAsync(updateRequest);

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
