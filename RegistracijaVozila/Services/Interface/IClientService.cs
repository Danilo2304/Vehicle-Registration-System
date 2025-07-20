using RegistracijaVozila.Models.Domain;
using RegistracijaVozila.Models.DTO;
using RegistracijaVozila.Results;

namespace RegistracijaVozila.Services.Interface
{
    public interface IClientService
    {
        Task<RepositoryResult<bool>> ValidateClientCreateRequestAsync(CreateClientRequestDto request);

        Task<RepositoryResult<bool>> ValidateClientDeleteRequestAsync(Guid id);

        Task<RepositoryResult<bool>> ValidateClientUpdateRequestAsync(UpdateClientRequestDto request);

        Task<RepositoryResult<bool>> ValidateClientId(Guid id);

        Task<RepositoryResult<ClientDto>> CreateClientAsync(CreateClientRequestDto request);

        Task<RepositoryResult<ClientDto>> DeleteClientAsync(Guid id);

        Task<RepositoryResult<ClientDto>> UpdateClientAsync(UpdateClientRequestDto request);

        Task<RepositoryResult<PagedResult<ClientDto>>> GetClientsAsync
            (string? searchQuery = null, int pageNumber = 1, int pageSize = 1000);

        Task<RepositoryResult<ClientDto>> GetClijentByIdAsync(Guid id);

        
    }
}
