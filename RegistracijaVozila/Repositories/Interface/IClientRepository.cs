using RegistracijaVozila.Models.Domain;

namespace RegistracijaVozila.Repositories.Interface
{
    public interface IClientRepository
    {
        Task<Klijent> AddClientAsync(Klijent klijent);

        Task<(List<Klijent> Items, int TotalCount)> GetAllAsync
            (string? searchQuery = null, int pageNumber = 1, int pageSize = 1000);

        Task<Klijent?> GetClijentByIdAsync(Guid id);

        Task<Klijent?> UpdateClientAsync(Klijent client);

        Task<Klijent?> DeleteClientAsync(Guid id);
    }
}
