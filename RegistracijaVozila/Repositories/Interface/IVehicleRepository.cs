using RegistracijaVozila.Models.Domain;

namespace RegistracijaVozila.Repositories.Interface
{
    public interface IVehicleRepository
    {
        Task<Vozilo> AddAsync(Vozilo vozilo);

        Task<(List<Vozilo> Items, int TotalCount)> GetAllAsync(string? searchQuery = null, int pageSize = 1000, int pageNumber = 1);

        Task<Vozilo?> GetVehicleByIdAsync(Guid id);

        Task<Vozilo?> DeleteVehicleAsync(Guid id);

        Task<Vozilo?> UpdateVehicleAsync(Vozilo vozilo);
    }
}
