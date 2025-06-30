using RegistracijaVozila.Models.Domain;

namespace RegistracijaVozila.Repositories.Interface
{
    public interface IVehicleRepository
    {
        Task<Vozilo> AddAsync(Vozilo vozilo);

        Task<List<Vozilo>> GetAllAsync();

        Task<Vozilo?> GetVehicleByIdAsync(Guid id);

        Task<Vozilo?> DeleteVehicleAsync(Guid id);

        Task<Vozilo?> UpdateVehicleAsync(Vozilo vozilo);
    }
}
