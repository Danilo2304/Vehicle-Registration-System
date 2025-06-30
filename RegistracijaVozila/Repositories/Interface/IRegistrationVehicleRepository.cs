using RegistracijaVozila.Models.Domain;

namespace RegistracijaVozila.Repositories.Interface
{
    public interface IRegistrationVehicleRepository
    {
        Task<Registracija> AddRegistrationAsync(Registracija request);

        Task<List<Registracija>> GetAllAsync();

        Task<Registracija?> GetByIdAsync(Guid id);

        Task<Registracija?> DeleteAsync(Guid id);

        Task<Registracija?> UpdateAsync(Registracija request);
    }
}
