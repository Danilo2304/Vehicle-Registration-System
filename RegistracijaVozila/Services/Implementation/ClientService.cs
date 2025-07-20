using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RegistracijaVozila.Data;
using RegistracijaVozila.Models.Domain;
using RegistracijaVozila.Models.DTO;
using RegistracijaVozila.Repositories.Implementation;
using RegistracijaVozila.Repositories.Interface;
using RegistracijaVozila.Results;
using RegistracijaVozila.Services.Interface;

namespace RegistracijaVozila.Services.Implementation
{
    public class ClientService : IClientService
    {
        private readonly RegistracijaVozilaDbContext appDbContext;
        private readonly IMapper mapper;
        private readonly IClientRepository clientRepository;

        public ClientService(RegistracijaVozilaDbContext appDbContext, IMapper mapper, IClientRepository clientRepository)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
            this.clientRepository = clientRepository;
        }

        public async Task<RepositoryResult<bool>> ValidateClientCreateRequestAsync(CreateClientRequestDto request)
        {
            if (await appDbContext.Klijenti.AnyAsync(x => x.JMBG == request.JMBG))
            {
                return RepositoryResult<bool>.Fail("JMBG_EXISTS: A client with this social security number already exists");
            }

            if (await appDbContext.Klijenti.AnyAsync(x => x.BrojLicneKarte == request.BrojLicneKarte))
            {
                return RepositoryResult<bool>.Fail("ID_CARD_EXISTS: A client with this ID card number already exists");
            }

            if (await appDbContext.Klijenti.AnyAsync(x => x.Email == request.Email))
            {
                return RepositoryResult<bool>.Fail("EMAIL_EXISTS: A client with this email adress already exists");
            }

            return RepositoryResult<bool>.Ok(true);
        }

        public async Task<RepositoryResult<ClientDto>> CreateClientAsync(CreateClientRequestDto request)
        {
            var validationResult = await ValidateClientCreateRequestAsync(request);

            if (!validationResult.Success)
            {
                return RepositoryResult<ClientDto>.Fail(validationResult.Message);
            }

            var clientDomain = mapper.Map<Klijent>(request);

            clientDomain = await clientRepository.AddClientAsync(clientDomain);

            var response = mapper.Map<ClientDto>(clientDomain);

            return RepositoryResult<ClientDto>.Ok(response, "Client successfully created!");
        }

        public async Task<RepositoryResult<bool>> ValidateClientDeleteRequestAsync(Guid id)
        {
            var exists = await appDbContext.Klijenti.AnyAsync(x=>x.Id == id);

            if(!exists)
            {
                return RepositoryResult<bool>.Fail($"CLIENT_NOT_FOUND: Client with the Id {id} was not found");
            }

            var isOwner = await appDbContext.Registracije.AnyAsync(x=>x.KlijentId == id);

            if (isOwner)
            {
                return RepositoryResult<bool>.Fail
                    ("CLIENT_REGISTRATION_EXISTS: Client cant be deleted because his registration still exists");
            }

            return RepositoryResult<bool>.Ok(true);
        }

        public async Task<RepositoryResult<ClientDto>> DeleteClientAsync(Guid id)
        {
            var validationResult = await ValidateClientDeleteRequestAsync(id);

            if (!validationResult.Success)
            {
                return RepositoryResult<ClientDto>.Fail(validationResult.Message);
            }

            var existingClient = await clientRepository.DeleteClientAsync(id);

            var response = mapper.Map<ClientDto>(existingClient);

            return RepositoryResult<ClientDto>.Ok(response, "Client successfully deleted!");
        }

        public async Task<RepositoryResult<bool>> ValidateClientUpdateRequestAsync(UpdateClientRequestDto request)
        {
            var exists = await appDbContext.Klijenti.AnyAsync(x => x.Id == request.Id);

            if (!exists)
            {
                return RepositoryResult<bool>.Fail($"CLIENT_NOT_FOUND: Client with the Id {request.Id} was not found");
            }

            if (await appDbContext.Klijenti.AnyAsync(x => x.JMBG == request.JMBG && x.Id!=request.Id))
            {
                return RepositoryResult<bool>.Fail("JMBG_EXISTS: A client with this social security number already exists");
            }

            if (await appDbContext.Klijenti.AnyAsync(x => x.BrojLicneKarte == request.BrojLicneKarte && x.Id != request.Id))
            {
                return RepositoryResult<bool>.Fail("ID_CARD_EXISTS: A client with this ID card number already exists");
            }

            if (await appDbContext.Klijenti.AnyAsync(x => x.Email == request.Email && x.Id != request.Id))
            {
                return RepositoryResult<bool>.Fail("EMAIL_EXISTS: A client with this email adress already exists");
            }

            return RepositoryResult<bool>.Ok(true);
        }

        public async Task<RepositoryResult<ClientDto>> UpdateClientAsync(UpdateClientRequestDto request)
        {
            var validationResult = await ValidateClientUpdateRequestAsync(request);

            if (!validationResult.Success)
            {
                return RepositoryResult<ClientDto>.Fail(validationResult.Message);
            }

            var clientDomain = mapper.Map<Klijent>(request);

            clientDomain = await clientRepository.UpdateClientAsync(clientDomain);

            var response = mapper.Map<ClientDto>(clientDomain);

            return RepositoryResult<ClientDto>.Ok(response, "Client successfully updated!");
        }

        public async Task<RepositoryResult<PagedResult<ClientDto>>> GetClientsAsync
            (string? searchQuery = null, int pageNumber = 1, int pageSize = 1000)
        {
            var (clients, totalCount) = await clientRepository.GetAllAsync(searchQuery, pageNumber, pageSize);

            var response = new PagedResult<ClientDto>
            {
                Items = mapper.Map<List<ClientDto>>(clients),
                TotalCount = totalCount
            };


            return RepositoryResult<PagedResult<ClientDto>>.Ok(response, "Clients loaded successfully");
        }

        public async Task<RepositoryResult<bool>> ValidateClientId(Guid id)
        {
            if (!await appDbContext.Klijenti.AnyAsync(x => x.Id == id))
            {
                return RepositoryResult<bool>.Fail($"CLIENT_NOT_FOUND, Client with the Id {id} was not found");
            }

            return RepositoryResult<bool>.Ok(true);
        }

        public async Task<RepositoryResult<ClientDto>> GetClijentByIdAsync(Guid id)
        {
            var validationResult = await ValidateClientId(id);

            if (!validationResult.Success)
            {
                return RepositoryResult<ClientDto>.Fail(validationResult.Message);
            }

            var clientDomain = await clientRepository.GetClijentByIdAsync(id);

            var response = mapper.Map<ClientDto>(clientDomain);

            return RepositoryResult<ClientDto>.Ok(response);
        }

        
    }
}






