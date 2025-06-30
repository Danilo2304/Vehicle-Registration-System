using RegistracijaVozila.Models.Domain;

namespace RegistracijaVozila.Repositories.Interface
{
    public interface IVehicleTypeRepository
    {
        Task<List<TipVozila>> GetAllAsync();

        Task<TipVozila> AddAsync(TipVozila tipVozila);

        Task<TipVozila?> GetByIdAsync(Guid id);

        Task<TipVozila?> DeleteAsync(Guid id);

        Task<TipVozila?> UpdateAsync(TipVozila tipVozila);
    }
}
