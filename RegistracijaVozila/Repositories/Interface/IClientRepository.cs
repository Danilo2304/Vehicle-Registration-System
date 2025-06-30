using RegistracijaVozila.Models.Domain;

namespace RegistracijaVozila.Repositories.Interface
{
    public interface IClientRepository
    {
        Task<Klijent> AddClientAsync(Klijent klijent);

        Task<List<Klijent>> GetAllAsync();

        Task<Klijent?> GetClijentByIdAsync(Guid id);

        Task<Klijent?> UpdateClientAsync(Klijent client);

        Task<Klijent?> DeleteClientAsync(Guid id);
    }
}
