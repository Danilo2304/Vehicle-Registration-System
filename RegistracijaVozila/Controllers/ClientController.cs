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

            return CreatedAtAction(nameof(GetClientById), new { id = result.Data.Id}, result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var clientDomainList = await clientRepository.GetAllAsync();

            if (clientDomainList == null)
            {
                return NotFound(new ApiError
                {
                    ErrorCode = "NO_CLIENT_LIST_FOUND",
                    Message = "No clients have been found in the database"
                });
            }

            var response = mapper.Map<List<ClientDto>>(clientDomainList);
            
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetClientById([FromRoute] Guid id)
        {
            var clientDomain = await clientRepository.GetClijentByIdAsync(id);

            if(clientDomain == null)
            {
                return NotFound(new ApiError
                {
                    ErrorCode = "CLIENT_NOT_FOUND",
                    Message = $"Client with the Id {id} was not found"
                });
            }


            var response = mapper.Map<ClientDto>(clientDomain);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await clientService.DeleteClientAsync(id);

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

            return Ok(result.Data);
        }

    }
}
