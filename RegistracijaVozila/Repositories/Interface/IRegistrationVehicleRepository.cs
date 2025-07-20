using RegistracijaVozila.Models.Domain;

namespace RegistracijaVozila.Repositories.Interface
{
    public interface IRegistrationVehicleRepository
    {
        Task<Registracija> AddRegistrationAsync(Registracija request);

        Task<(List<Registracija> Items, int TotalCount)> GetAllAsync(string? searchQuery = null,int pageNumber =1, int pageSize = 1000);

        Task<Registracija?> GetByIdAsync(Guid id);

        Task<Registracija?> DeleteAsync(Guid id);

        Task<Registracija?> UpdateAsync(Registracija request);
    }
}
